using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MediaMarket.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _model;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _model = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            var query = _context.Set<T>().Where(criteria);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            var query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query.Include(include);
                }
            }

            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task<T> FindByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().AnyAsync(predicate);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
