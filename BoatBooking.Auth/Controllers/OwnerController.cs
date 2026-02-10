using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Owner")]
public class OwnerController : ControllerBase
{
    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        return Ok(new { message = "Welcome, approved Owner!" });
    }
}

