using EITracker.DbContext;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ODataDemo.Services;
using System.Collections;
using System.Reflection;

namespace EITracker.WebApi.Controllers
{
    public class BaseController : ODataController
    {
        private readonly ITypeMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly LibraryDbContext databaseContext;
        private readonly ApplicationDbContext userContext;
        public BaseController(LibraryDbContext dbContext, ITypeMapper mapper)
        {
            this.databaseContext = dbContext;
            this._mapper = mapper;
        }
        public BaseController(LibraryDbContext dbContext, ITypeMapper mapper, ApplicationDbContext userContext)
           : this(dbContext, mapper)
           => this.userContext = userContext;
        public BaseController(LibraryDbContext dbContext, ITypeMapper mapper, ApplicationDbContext userContext, IConfiguration configuration)
       : this(dbContext, mapper, userContext)
       => this._configuration = configuration;

        protected async Task<IActionResult> Delete<TEntity>(IQueryable<TEntity> source)
        {
            var entity = await source.SingleOrDefaultAsync();
            if (entity == null)
            {
                return NotFound();
            }

            this.databaseContext.Remove(entity);
            await this.databaseContext.SaveChangesAsync();
            return NoContent();
        }

        protected async Task<IActionResult> Patch<TEntity, TModel>(IQueryable<TEntity> source, Delta<TModel> delta, ODataQueryOptions<TModel> options = null, CancellationToken token = default(CancellationToken), Func<TEntity, CancellationToken, Task> afterSave = null)
         where TEntity : class
         where TModel : class
        {
            if (delta == null)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = await source.FirstOrDefaultAsync();
            return await this.PatchEntity(entity, delta, token, afterSave);
        }
        protected async Task<IActionResult> PatchEntity<TEntity, TModel>(TEntity entity, Delta<TModel> delta, CancellationToken token = default(CancellationToken), Func<TEntity, CancellationToken, Task> afterSave = null, Func<TEntity, CancellationToken, Task> beforeSave = null)
          where TEntity : class
          where TModel : class
        {
            if (entity == null)
            {
                return this.NotFound();
            }

            TModel model = default(TModel);
            model = this._mapper.Map<TEntity, TModel>(entity);
            PropertyInfo modifiedProperty = typeof(TModel).GetProperty("ModifiedTime", typeof(DateTimeOffset), new Type[] { });
            PropertyInfo modifiedByIdProperty = typeof(TModel).GetProperty("ModifiedById", typeof(Guid), new Type[] { });

            if (modifiedProperty == null)
            {
                modifiedProperty = typeof(TModel).GetProperty("Modified", typeof(DateTimeOffset), new Type[] { });
            }

            if (modifiedProperty != null)
            {
                modifiedProperty.SetValue(model, DateTimeOffset.UtcNow);
            }
            // TODO
            //if (modifiedByIdProperty != null)
            //{
            //    modifiedByIdProperty.SetValue(model, this.userContext.UserId);
            //}

            if (beforeSave != null)
            {
                await beforeSave(entity, token).ConfigureAwait(false);
            }

            delta.Patch(model);

            this._mapper.Map<TModel, TEntity>(model, entity);

            await this.databaseContext.SaveChangesAsync().ConfigureAwait(false);

            if (afterSave != null)
            {
                await afterSave(entity, token).ConfigureAwait(false);
            }

            model = this._mapper.Map<TEntity, TModel>(entity);
            return this.Updated(model);
        }

        protected void ConfigureEntitiesForUpdate(object o)
        {
            this.ConfigureEntities(o, false);
        }

        protected void ConfigureEntitiesForInsert(object o)
        {
            this.ConfigureEntities(o, true);
        }

        private void ConfigureEntities(object o, bool isSetCreatedAndModified)
        {
            if (o == null)
            {
                return;
            }

            DateTimeOffset timestamp = DateTimeOffset.UtcNow;
            Guid userId = Guid.Empty;//this.userContext.UserId;  // TODO

            string[] guidToSet = isSetCreatedAndModified ? new[] { "CreatedById", "ModifiedById" } : new[] { "ModifiedById" };
            string[] dateTimeOffsetToSet = isSetCreatedAndModified ? new[] { "Created", "Modified" } : new[] { "Modified" };
            string[] dateTimeToSet = isSetCreatedAndModified ? new[] { "CreatedTime", "ModifiedTime" } : new[] { "ModifiedTime" };

            var properties = o.GetType().GetRuntimeProperties();
            foreach (var property in properties.Where(p => p.PropertyType == typeof(Guid) && guidToSet.Contains(p.Name) && p.GetMethod != null && p.SetMethod != null))
            {
                // No matter what value is returned, this must point to the current user.
                var value = (Guid)property.GetMethod.Invoke(o, null);
                property.SetMethod.Invoke(o, new object[] { userId });
            }

            foreach (var property in properties.Where(p => p.PropertyType == typeof(DateTimeOffset) && dateTimeOffsetToSet.Contains(p.Name) && p.GetMethod != null && p.SetMethod != null))
            {
                // No matter what value is returned, this must point to the current time.
                var value = (DateTimeOffset)property.GetMethod.Invoke(o, null);
                property.SetMethod.Invoke(o, new object[] { timestamp });
            }

            foreach (var property in properties.Where(p => p.PropertyType == typeof(DateTime) && dateTimeToSet.Contains(p.Name) && p.GetMethod != null && p.SetMethod != null))
            {
                // No matter what value is returned, this must point to the current time.
                var value = (DateTime)property.GetMethod.Invoke(o, null);
                property.SetMethod.Invoke(o, new object[] { DateTime.UtcNow });
            }

            foreach (var property in properties.Where(p => p.GetMethod != null && p.PropertyType.GetInterfaces().Any(t => t == typeof(IEnumerable))))
            {
                // Collection
                var value = property.GetMethod.Invoke(o, null) as IEnumerable;

                // If the collection has a value and members, update them.
                if (value != null)
                {
                    foreach (var item in value)
                    {
                        this.ConfigureEntities(item, isSetCreatedAndModified);
                    }
                }
            }
          
        }

        /// <summary>
        /// Get value for a key from querystring
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetQueryStringValue(string key)
        {
            Microsoft.AspNetCore.Http.IQueryCollection keyValuePairs = this.ControllerContext.HttpContext.Request.Query;
            return keyValuePairs.ContainsKey(key) ? keyValuePairs[key].ToString() : null;
        }

        /// <summary>
        /// Get SQLParameter as per data passed
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private SqlParameter GetSqlParameter(string paramName, Guid value)
        {
            return new SqlParameter()
            {
                ParameterName = paramName,
                SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                Direction = System.Data.ParameterDirection.Input,
                Value = value
            };
        }
    }
}
