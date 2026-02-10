using BoatBooking.Application.DTOs;
using BoatBooking.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatBooking.Application.Queries.GetPendingOwners
{
    public class GetPendingOwnersQueryHandler : IGetPendingOwnersQuery
    {
        private readonly IUserRepository _userRepository;

        public GetPendingOwnersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> ExecuteAsync()
        {
            var users = await _userRepository.GetPendingOwnersAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role.ToString(),
                IsApproved = u.IsApproved
            }).ToList();
        }
    }
}
