using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public IQueryable<Country> GetAllAsync()
        {
            return _context.Countries;
        }

        public async Task CreateAsync(Country user)
        {
            await _context.Countries.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Country user)
        {
            _context.Countries.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var country = await GetByIdAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }
}
