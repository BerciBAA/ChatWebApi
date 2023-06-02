using ChatWebApi.Models.Member;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatWebApi.Services.TokenGenerator
{
    public class ChatWebApiService : IChatWebApiService
    {
        private readonly IConfiguration _configuration;

        public ChatWebApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? GenerateToken(MemberDto memberDto) {

            if (memberDto.Name is null)
                return null;

            var token = _configuration.GetSection("AppSettings:Key").Value;

            if (token is null)
                return null;

            List<Claim> claims = new List<Claim>
            {
                new Claim("guid", memberDto.MemberId.ToString()),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var generatedToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(generatedToken);

            return jwt;
        }
    }
}
