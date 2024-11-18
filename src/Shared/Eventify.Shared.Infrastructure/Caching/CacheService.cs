﻿using System.Buffers;
using System.Text.Json;
using Eventify.Shared.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace Eventify.Shared.Infrastructure.Caching;

public sealed class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken = default)
    {
        var bytes = await _cache.GetAsync(cacheKey, cancellationToken);

        return bytes is null ? default : Deserialize<T>(bytes);
    }

    public Task RemoveAsync(string cacheKey, CancellationToken cancellationToken = default)
        => _cache.RemoveAsync(cacheKey, cancellationToken);

    public Task SetAsync<T>(string cacheKey, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var bytes = Serialize(value);

        return _cache.SetAsync(cacheKey, bytes, options: CacheOptions.Create(expiration), cancellationToken);
    }

    private static T? Deserialize<T>(byte[] bytes) => JsonSerializer.Deserialize<T>(bytes!);

    private static byte[] Serialize<T>(T value)
    {
        var buffer = new ArrayBufferWriter<byte>();
        using var writter = new Utf8JsonWriter(buffer);
        JsonSerializer.Serialize(writter, value);

        return buffer.WrittenSpan.ToArray();
    }
}