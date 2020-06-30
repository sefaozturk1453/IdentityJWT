using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityJWT.Data;
using IdentityJWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace IdentityJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private UserManager<ApplicationUser> userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var user = await userManager.FindByNameAsync(model.Username);
            if(user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {

                var userRoles = await userManager.GetRolesAsync(user); //user a ait değişkenleri userRoles e atadık. tüm roller atandı

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in userRoles) //userRoles içine atanan tüm roller tek tek claims a eklendi
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("UzuncaBirSifreee"));

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:44393",
                    audience: "https://localhost:44393",
                    expires: DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    message = "Giriş Başarılı"
                });
            
            }
            else
            {
                return BadRequest(new
                {
                    message = "Kullanıcı adı ve parola yanlış"
                });
            }



        }
    }
}
