using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BoatBooking.Application.Interfaces;
using BoatBooking.Domain.Enums;
using BoatBooking.Application.Commands.ApproveUser;
using BoatBooking.Application.Queries.GetPendingOwners;
using BoatBooking.Application.Commands.RejectUser;


[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IGetPendingOwnersQuery _getPendingOwnersQuery;
    private readonly IApproveOwnerCommand _approveOwnerCommand;
    private readonly IRejectOwnerCommand _rejectOwnerCommand;
    private readonly ILogger<AdminController> _logger;


    public AdminController(
        IGetPendingOwnersQuery getPendingOwnersQuery,
        IApproveOwnerCommand approveOwnerCommand,IRejectOwnerCommand rejectOwnerCommand ,ILogger<AdminController> logger)
    {
        _getPendingOwnersQuery = getPendingOwnersQuery;
        _approveOwnerCommand = approveOwnerCommand;
        _rejectOwnerCommand = rejectOwnerCommand;
        _logger = logger;
    }

    [HttpGet("pending-owners")]
    public async Task<IActionResult> GetPendingOwners()
    {
        var result = await _getPendingOwnersQuery.ExecuteAsync();
        return Ok(result);
    }

    [HttpPost("approve-owner/{id}")]
    public async Task<IActionResult> ApproveOwner(Guid id)
    {
        await _approveOwnerCommand.ExecuteAsync(id);
        _logger.LogWarning("Admin approved owner with id {OwnerId}", id);
        return Ok(new { message = "Owner approved" });
    }

    [HttpPost("reject-owner/{id}")]
    public async Task<IActionResult> RejectOwner(Guid id)
    {
        await _rejectOwnerCommand.ExecuteAsync(id);
        _logger.LogWarning("Admin rejected owner with id {OwnerId}", id);
        return Ok(new { message = "Owner rejected" });
    }

}