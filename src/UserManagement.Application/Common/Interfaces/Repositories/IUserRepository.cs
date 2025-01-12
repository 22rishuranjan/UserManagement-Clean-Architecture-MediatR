

using Microsoft.EntityFrameworkCore;

using UserManagement.Domain.Entities;

namespace UserManagement.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        IQueryable<User> GetAllAsync();
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
