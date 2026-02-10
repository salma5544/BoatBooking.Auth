using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatBooking.Application.Commands.RejectUser
{
    public interface IRejectOwnerCommand
    {
        Task ExecuteAsync(Guid ownerId);
    }
}
