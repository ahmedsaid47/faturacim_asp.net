﻿using faturacim.Domain.Entities;
using faturacim.Domain.Interfaces;
using faturacim.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace faturacim.Infrastructure.Repositories
{
    /// <summary>
    /// Kullanıcı verisi için Entity Framework Core repository implementasyonu.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }

}
