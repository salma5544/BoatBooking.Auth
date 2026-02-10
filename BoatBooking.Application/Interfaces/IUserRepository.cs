using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoatBooking.Domain.Entities;

namespace BoatBooking.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<List<User>> GetPendingOwnersAsync();
        Task<List<User>> GetAllAsync();
    }
}

