
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Text.Json;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConnectionMultiplexer _redis;

        public UserRepository(ApplicationDbContext context, IConnectionMultiplexer redis)
        {
            _context = context;
            _redis = redis;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Country) // Include the Country in the query
                .AsNoTracking() 
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public IQueryable<User> GetAllAsync()
        {

            return _context.Users
                .Include(u => u.Country); // Include the Country in the query
                
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
