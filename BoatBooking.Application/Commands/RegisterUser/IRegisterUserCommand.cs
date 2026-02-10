using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatBooking.Application.Commands.RegisterUser
{
    public interface IRegisterUserCommand
    {
        Task ExecuteAsync(RegisterUserCommand command);
    }
}
