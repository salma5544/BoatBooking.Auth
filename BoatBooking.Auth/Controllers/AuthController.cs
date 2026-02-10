using BoatBooking.Application.Commands.LoginUser;
using BoatBooking.Application.Commands.RegisterUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IRegisterUserCommand _registerUserCommand;
    private readonly ILoginUserCommand _loginUserCommand;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
    IRegisterUserCommand registerUserCommand,
    ILoginUserCommand loginUserCommand,
    ILogger<AuthController> logger)
    {
        _registerUserCommand = registerUserCommand;
        _loginUserCommand = loginUserCommand;
        _logger = logger;
    }


    // Registration
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        await _registerUserCommand.ExecuteAsync(command);
        _logger.LogInformation("User attempting to register with email {Email}", command.Email);
        return Ok(new { message = "User registered successfully" });
    }

    // Login 
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var token = await _loginUserCommand.ExecuteAsync(command);
        _logger.LogInformation("User attempting to login with email {Email}", command.Email);
        return Ok(new { token });
    }

}