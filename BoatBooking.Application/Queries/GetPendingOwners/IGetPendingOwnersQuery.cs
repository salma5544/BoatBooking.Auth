using BoatBooking.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatBooking.Application.Queries.GetPendingOwners
{
    public interface IGetPendingOwnersQuery
    {
        public Task<List<UserDto>> ExecuteAsync();
    }
}
