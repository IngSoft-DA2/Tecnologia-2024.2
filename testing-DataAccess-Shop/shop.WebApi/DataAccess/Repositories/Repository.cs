using Microsoft.EntityFrameworkCore;

namespace shop.WebApi.DataAccess.Repositories
{
    public sealed class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;

        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            _entities.Add(entity);

            _context.SaveChanges();

            return entity;
        }

        public List<TEntity> GetAll()
        {
            return _entities.ToList();
        }
    }
}
