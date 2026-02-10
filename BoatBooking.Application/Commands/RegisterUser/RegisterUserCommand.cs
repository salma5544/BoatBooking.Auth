using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoatBooking.Domain.Enums;

namespace BoatBooking.Application.Commands.RegisterUser
{
    public class RegisterUserCommand
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; }
    }
}

