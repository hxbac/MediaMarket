using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MediaMarket.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<T> FirstOrFailAsync<T>(this IQueryable<T> query)
            where T : class
        {
            var result = await query.FirstOrDefaultAsync();

            if (result == null)
            {
                throw new EntityNotFoundException();
            }

            return result;
        }

        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
            where T : class
        {
            if (source == null)
            {
                throw new Exception("Empty");
            }

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await source.AsNoTracking().CountAsync();
            if (count == 0) return PaginatedResult<T>.Success(new List<T>(), count, pageNumber, pageSize);
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }

        public static async Task<PaginatedResult<TTarget>> ToPaginatedListAsync<TSource, TTarget>(this IQueryable<TSource> source, int pageNumber, int pageSize, IMapper mapper)
            where TSource : class
            where TTarget : class
        {
            if (source == null)
            {
                throw new Exception("Empty");
            }

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await source.AsNoTracking().CountAsync();
            if (count == 0) return PaginatedResult<TTarget>.Success(new List<TTarget>(), count, pageNumber, pageSize);
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var mappedItems = items.Select(mapper.Map<TTarget>).ToList();
            return PaginatedResult<TTarget>.Success(mappedItems, count, pageNumber, pageSize);
        }
    }
}
