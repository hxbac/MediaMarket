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
    }
}
