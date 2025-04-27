using System.Text.Json;
using Docentes.Application.Services;
using StackExchange.Redis;

namespace Docentes.Infrastructure.services;

public class CacheService : ICacheService
{
   private readonly IConnectionMultiplexer _connectionMultiplexer;
   private readonly IDatabase _database;

   public CacheService(IConnectionMultiplexer connectionMultiplexer)
   {
      _connectionMultiplexer = connectionMultiplexer;
      _database = _connectionMultiplexer.GetDatabase();
   }

   public async Task<T?> GetCacheValueAsync<T>(string key)
   {
      var value = await _database.StringGetAsync(key);
      if (value.IsNullOrEmpty)
      {
         return default;
      }
      return JsonSerializer.Deserialize<T>(value!);
   }

   public async Task SetCacheValueAsync<T>(string key, T value, TimeSpan? expirationTime = null)
   {
      var json = JsonSerializer.Serialize(value);
      await _database.StringSetAsync(key, json, expirationTime);
   }
}