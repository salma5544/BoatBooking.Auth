using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoatBooking.Domain.Enums;

namespace BoatBooking.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public UserRole Role { get; set; }

        public bool IsApproved { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

