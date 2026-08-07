using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service
{
    public class CasheService:ICasheService
    {
        private readonly ICasheRepository _casheRepository;

        public CasheService(ICasheRepository casheRepository)
        {
            _casheRepository = casheRepository;
        }

        public Task<string?> GetAsync(string cacheKey, CancellationToken ct = default)
        => _casheRepository.GetAsync(cacheKey, ct);

        public Task SetAsync(string cacheKey, string value, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var json = JsonSerializer.Serialize(value,new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return _casheRepository.SetAsync(cacheKey, json, timeToLive, ct);
        }
    }
}
