using System.Text;
using BoatBooking.Application.Interfaces;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace BoatBooking.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : ILoginUserCommand
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> ExecuteAsync(LoginUserCommand command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user == null)
                throw new Exception("Invalid email or password");

            var passwordHash = Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(command.Password))
            );

            if (user.PasswordHash != passwordHash)
                throw new Exception("Invalid email or password");

            if (!user.IsApproved)
                throw new Exception("User not approved yet");

            //Generate JWT
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}

