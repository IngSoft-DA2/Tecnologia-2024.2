namespace shop.WebApi.DataAccess.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity Add(TEntity product);

        List<TEntity> GetAll();
    }
}
