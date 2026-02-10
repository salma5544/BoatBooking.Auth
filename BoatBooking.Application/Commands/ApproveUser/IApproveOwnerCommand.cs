using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatBooking.Application.Commands.ApproveUser
{
    public interface IApproveOwnerCommand
    {
        Task ExecuteAsync(Guid ownerId);
    }
}
