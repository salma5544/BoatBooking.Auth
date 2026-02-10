using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Customer")]
public class CustomerController : ControllerBase
{
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok(new { message = "Welcome, Customer!" });
    }
}
