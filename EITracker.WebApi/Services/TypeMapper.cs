namespace ODataDemo.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.OData.Query;
    using Microsoft.OData.UriParser;

    public class TypeMapper : ITypeMapper
    {
        private readonly IMapper mapper;

        public TypeMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IQueryable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source, ODataQueryOptions<TDestination> options)
        {
            List<string> expand = new List<string>();
            if (options.SelectExpand != null && options.SelectExpand.SelectExpandClause != null && options.SelectExpand.SelectExpandClause.SelectedItems != null)
            {
                foreach (var item in options.SelectExpand.SelectExpandClause.SelectedItems)
                {
                    this.GetExpandList(item, expand);
                }
            }

            ProjectionExpression expression = new ProjectionExpression(source, mapper.ConfigurationProvider.ExpressionBuilder);
            var projection = expression.To<TDestination>(null, expand.ToArray());
            options.ApplyTo(projection);
            return projection;
        }

        private void GetExpandList(SelectItem item, List<string> expand, string prefix = "")
        {
            string navigationSourceName = null;
            if (item is ExpandedReferenceSelectItem)
            {
                navigationSourceName = $"{prefix}.{(item as ExpandedReferenceSelectItem).NavigationSource.Name}".Trim('.');
                expand.Add(navigationSourceName);
            }

            if (!string.IsNullOrEmpty(navigationSourceName) && item is ExpandedNavigationSelectItem)
            {
                foreach (var innerItems in (item as ExpandedNavigationSelectItem).SelectAndExpand.SelectedItems)
                {
                    this.GetExpandList(innerItems, expand, navigationSourceName);
                }
            }
        }

        public TModel Map<TEntity, TModel>(TEntity entity)
            where TEntity : class
            where TModel : class
        {
            return this.mapper.Map<TEntity, TModel>(entity);
        }

        public void Map<TModel, TEntity>(TModel model, TEntity entity)
            where TModel : class
            where TEntity : class
        {
            entity = this.mapper.Map<TModel, TEntity>(model, entity);
        }

        public TDestination Map<TDestination>(object source)
        {
            return this.mapper.Map<TDestination>(source);
        }
    }
}
