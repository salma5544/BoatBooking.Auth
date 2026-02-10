using System.Text;
using BoatBooking.Application.Interfaces;
using BoatBooking.Domain.Entities;
using System.Security.Cryptography;
using BoatBooking.Domain.Enums;

namespace BoatBooking.Application.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRegisterUserCommand
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(RegisterUserCommand command)
        {
            var passwordHash = Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(command.Password))
            );

            var user = new User
            {
                FullName = command.FullName,
                Email = command.Email,
                PasswordHash = passwordHash,
                Role = command.Role,
                IsApproved = command.Role == UserRole.Admin || command.Role == UserRole.Customer,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
        }
    }
}

