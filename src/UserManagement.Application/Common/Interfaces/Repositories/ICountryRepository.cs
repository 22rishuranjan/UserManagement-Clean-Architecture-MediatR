
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Common.Interfaces
{
    public interface ICountryRepository
    {
        Task<Country> GetByIdAsync(int id);
        IQueryable<Country> GetAllAsync();
        Task CreateAsync(Country country);
        Task UpdateAsync(Country country);
        Task DeleteAsync(int id);
    }
}
