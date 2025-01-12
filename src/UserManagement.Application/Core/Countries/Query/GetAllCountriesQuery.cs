using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Application.Core.Users.Query;

namespace UserManagement.Application.Core.Countries.Query
{

    public record GetAllCountriesQuery : IRequest<IEnumerable<CountryDto>>;

    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, IEnumerable<CountryDto>>
    {
        private readonly ICountryRepository _repository;
        private readonly IConnectionMultiplexer _redis;

        public GetAllCountriesQueryHandler(ICountryRepository repository, IConnectionMultiplexer redis)
        {
            _repository = repository;
            _redis = redis;
        }

        public async Task<IEnumerable<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {

            var db = _redis.GetDatabase();
            var cacheKey = "YourCountry";

            // Try to get data from Redis cache
            var cachedData = await db.StringGetAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                // Deserialize data from Redis
                return JsonSerializer.Deserialize<IEnumerable<CountryDto>>(cachedData);
            }

            var countries = _repository.GetAllAsync();
            var data = countries.Select(country => new CountryDto
            {
                Id = country.Id,
                Code = country.Code,
                Name = country.Name,
               
            }).ToList();

            // Cache the data with an expiration time (e.g., 5 minutes)
            var serializedData = JsonSerializer.Serialize(data);
            await db.StringSetAsync(cacheKey, serializedData, TimeSpan.FromMinutes(5));

            return data;
        }
    }
}
