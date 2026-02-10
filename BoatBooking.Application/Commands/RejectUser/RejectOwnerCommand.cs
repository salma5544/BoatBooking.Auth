using BoatBooking.Application.Interfaces;
using BoatBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatBooking.Application.Commands.RejectUser
{
    public class RejectOwnerCommand : IRejectOwnerCommand
    {
        private readonly IUserRepository _userRepository;

        public RejectOwnerCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(Guid ownerId)
        {
            var user = await _userRepository.GetByIdAsync(ownerId);

            if (user == null || user.Role != UserRole.Owner)
                throw new Exception("Invalid owner");

            user.IsApproved = false;
            await _userRepository.UpdateAsync(user);
        }
    }

}
