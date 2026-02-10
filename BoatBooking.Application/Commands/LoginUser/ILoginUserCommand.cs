using BoatBooking.Application.Commands.RegisterUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatBooking.Application.Commands.LoginUser
{
    public interface ILoginUserCommand
    {
        Task<string> ExecuteAsync(LoginUserCommand command);
    }
}
