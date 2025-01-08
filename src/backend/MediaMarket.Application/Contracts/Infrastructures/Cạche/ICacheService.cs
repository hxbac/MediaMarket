namespace MediaMarket.Application.Contracts.Infrastructures.Cạche
{
    public interface ICacheService
    {
        Task SetAsync<T>(string key, T obj);
        Task SetAsync<T>(string key, T obj, TimeSpan expiration);
        public Task<T?> GetValueAsync<T>(string key);
        Task<IEnumerable<T>> GetValuesAsync<T>(string key);
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func);
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func, TimeSpan expiration);
        Task RemoveAsync(string key);
    }
}
