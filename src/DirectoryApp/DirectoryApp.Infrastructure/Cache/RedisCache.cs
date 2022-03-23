using DirectoryApp.Application.Cache;
using DirectoryApp.Application.ConfigurationModel;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DirectoryApp.Infrastructure.Cache;

public class RedisCache : ICache
{
    private readonly RedisConfiguration _config;
    private IConnectionMultiplexer _connection;
    private IDatabase _database;

    public RedisCache(RedisConfiguration config)
    {
        _config = config;
        StartConnection();
    }


    public void StartConnection()
    {
        var redisOptions = new ConfigurationOptions()
        {
            EndPoints = { _config.Host, _config.Port },
            DefaultDatabase = _config.DatabaseIndex,
        };

        try
        {
            this._connection = ConnectionMultiplexer.Connect(redisOptions);
            this._database = this._connection.GetDatabase();
        }
        catch (Exception)
        {
            throw new ApplicationException("Could not connect to redis.");
        }
    }


    private void CheckConnection()
    {
        if (!this._connection.IsConnected)
        {
            this.StartConnection();
        }
    }



    public async Task<bool> IsKeyExistAsync(string key)
    {
        CheckConnection();
        return await this._database.KeyExistsAsync(key);
        
    }

    public async Task<T> ReadFromCacheAsync<T>(string key)
    {
        CheckConnection();
        var value = default(T);

        if (await this.IsKeyExistAsync(key))
        {
            string cacheValue = await _database.StringGetAsync(key);
            value = JsonConvert.DeserializeObject<T>(cacheValue);
        }

        return value;
    }

    public async Task<bool> RemoveFromCacheAsync(string key)
    {
        CheckConnection();

        if(await this.IsKeyExistAsync(key))
        {
            await _database.KeyDeleteAsync(key);
        }

        return true;
    }

    public async Task SetToCacheAsync<T>(string key, T data)
    {
        await this._database.StringSetAsync(key, JsonConvert.SerializeObject(data));
    }
}