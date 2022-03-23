namespace DirectoryApp.Application.Cache;

public interface ICache
{
    public Task<bool> IsKeyExistAsync(string key);
    public Task SetToCacheAsync<T>(string key, T data);
    
    public Task<T> ReadFromCacheAsync<T>(string key);
    public Task<bool> RemoveFromCacheAsync(string key);

}