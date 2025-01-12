using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserManagement.Application.Common.Interfaces;

namespace UserManagement.Application.Core.Users.Query
{
    public record GetAllUsersQuery : IRequest<IEnumerable<UserDto>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _repository;
        private readonly IConnectionMultiplexer _redis;

        public GetAllUsersQueryHandler(IUserRepository repository, IConnectionMultiplexer redis)
        {
            _repository = repository;
            _redis = redis;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {

            var db = _redis.GetDatabase();
            var cacheKey = "YourRecords";

            // Try to get data from Redis cache
            var cachedData = await db.StringGetAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                // Deserialize data from Redis
                return JsonSerializer.Deserialize<IEnumerable<UserDto>>(cachedData);
            }

            var users =  _repository.GetAllAsync();
            var data = users.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Country = user.Country.Name
            }).ToList();

            // Cache the data with an expiration time (e.g., 5 minutes)
            var serializedData = JsonSerializer.Serialize(data);
            await db.StringSetAsync(cacheKey, serializedData, TimeSpan.FromMinutes(5));

            return data;
        }
    }
}

