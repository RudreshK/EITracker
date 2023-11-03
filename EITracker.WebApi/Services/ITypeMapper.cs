namespace ODataDemo.Services
{
    using System.Linq;
    using Microsoft.AspNet.OData.Query;

    public interface ITypeMapper
    {
        IQueryable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source, ODataQueryOptions<TDestination> options);

        TDestination Map<TDestination>(object source);

        TModel Map<TEntity, TModel>(TEntity entity)
            where TEntity : class
            where TModel : class;

        void Map<TModel, TEntity>(TModel model, TEntity entity)
            where TModel : class
            where TEntity : class;
    }
}
