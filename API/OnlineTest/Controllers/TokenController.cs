using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OnlineTest.Model;
using OnlineTest.Service.DTO;
using OnlineTest.Service.Interface;
using OnlineTest.Services.DTO;
using OnlineTest.Services.Interface;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserServices _userService;
        private readonly IRTokenService _rTokenService;
        private readonly IConfiguration _jwtConfig;
        private readonly IUserRolesServices _userRolesService;
        public TokenController(IUserServices userService, IRTokenService rTokenService, IConfiguration jwtConfig , IUserRolesServices userRolesService)
        {
            _userService = userService;
            _rTokenService = rTokenService;
            _jwtConfig = jwtConfig.GetSection("JWTConfig");
            _userRolesService = userRolesService;
        }

        [HttpPost]
        [Route("Auth")]
        public ActionResult Auth([FromBody] TokenDTO parameters)
        {
            switch (parameters.Grant_Type)
            {
                case "password" when string.IsNullOrEmpty(parameters.Username):
                    return BadRequest("Enter User Name");
                case "password" when string.IsNullOrEmpty(parameters.Username) && string.IsNullOrEmpty(parameters.Password):
                    return BadRequest("Enter Username & Password.");
                case "password" when !string.IsNullOrEmpty(parameters.Username) && !string.IsNullOrEmpty(parameters.Password):
                    return Login(parameters);
                case "password" when !string.IsNullOrEmpty(parameters.Username) && parameters.Password == "":
                    return BadRequest("Enter Password.");
                case "password":
                    return BadRequest("Something Went Wrong, Try Again.");
                case "refresh_token":
                    return RefreshToken(parameters);
                default:
                    return BadRequest("Invalid grant_type.");
            }
        }

        private ActionResult Login(TokenDTO parameters)
        {
            var sessionModel = _userService.IsUserExists(parameters);
            if (sessionModel == null)
            {
                return BadRequest("Invalid Username or Password.");
            }

            var refreshToken = Guid.NewGuid().ToString().Replace("-", "");

            var rToken = new RToken
            {
                RefreshToken = refreshToken,
                IsStop = 0,
                UserId = sessionModel.Id,
                CreatedDate = DateTime.Now
            };

            //store the refresh_token
            if (_rTokenService.AddToken(rToken))
            {
                return Ok(GetJwt(sessionModel, refreshToken));
            }
            else
            {
                return BadRequest("Failed to Add Token.");
            }
        }

        private ActionResult RefreshToken(TokenDTO parameters)
        {
            var token = _rTokenService.GetToken(parameters.Refresh_Token);

            if (token == null)
            {
                return BadRequest("Can not refresh token.");
            }

            if (token.IsStop == 1)
            {
                return BadRequest("Refresh token has expired.");
            }

            var refreshToken = Guid.NewGuid().ToString().Replace("-", "");

            token.IsStop = 1;
            var updateFlag = _rTokenService.ExpireToken(token);
            var addFlag = _rTokenService.AddToken(new RToken
            {
                RefreshToken = refreshToken,
                IsStop = 0,
                CreatedDate = DateTime.UtcNow,
                UserId = token.UserId
            });

            if (updateFlag && addFlag)
            {
                return Ok(GetJwt(token.UserId , refreshToken));
            }
            else
            {
                return BadRequest("Can not expire token or a new token");
            }
        }

        private string GetJwt(UserDTO sessionModel, string refreshToken)
        {
            var now = DateTime.UtcNow;
            var role = _userRolesService.GetById(sessionModel.Id);
            var claims = new[]
            {
                new Claim("Email", sessionModel.Email),
                new Claim("Jti", Guid.NewGuid().ToString()),
                new Claim("Iat", now.ToUniversalTime().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
                new Claim("UserId",Convert.ToString(sessionModel.Id)),
                new Claim("Name",Convert.ToString(sessionModel.Email)),
                new Claim("Role" , role.Contains(1) ? "Admin" : role.Contains(0) ? "User" : "")
            };

            var symmetricKeyAsBase64 = _jwtConfig["SecretKey"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: _jwtConfig["Issuer"],
                audience: _jwtConfig["Audience"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(1)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = "Bearer " + encodedJwt,
                username = sessionModel.Email ?? "",
                expires_in = (int)TimeSpan.FromHours(1).TotalSeconds,
                refresh_token = refreshToken,
                user_Id = sessionModel.Id
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        private string GetJwt(int id ,string refreshToken)
        {
            var now = DateTime.UtcNow;
            var role = _userRolesService.GetById(id);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, refreshToken),
                new Claim("Jti", Guid.NewGuid().ToString()),
                new Claim("Iat", now.ToUniversalTime().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
                new Claim("Role" , role.Contains(1) ? "Admin" : role.Contains(0) ? "User" : ""),
            };

            var symmetricKeyAsBase64 = _jwtConfig["SecretKey"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: _jwtConfig["Issuer"],
                audience: _jwtConfig["Audience"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromHours(1)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = "Bearer " + encodedJwt,
                expires_in = (int)TimeSpan.FromHours(1).TotalSeconds,
                refresh_token = refreshToken
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
