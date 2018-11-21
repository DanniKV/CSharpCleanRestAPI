using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CustomerApp.Infrastructure.Data;
using CustomerApp.Core.Entity;
using CustomerApp.Core.ApplicationService;
using System.Linq;
using System.Collections.Generic;
using CustomerApp.Core.DomainService;

namespace DKVRestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IUserService _userService;

        public TokenController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpPost]
         public ActionResult Login([FromBody] LoginInputModel model)
        {
            var user = _userService.GetUser(model.Username);

            if (user == null)
                return Unauthorized();

            if (!model.Password.Equals(user.Password))
                return Unauthorized();

            return Ok(new
            {
                username = user.Username,
                token = GenerateToken(user)
            });
        }

        // This method verifies that the password entered by a user corresponds to the stored
        // password hash for this user. The method computes a Hash-based Message Authentication
        // Code (HMAC) using the SHA512 hash function. The inputs to the computation is the
        // password entered by the user and the stored password salt for this user. If the
        // computed hash value is identical to the stored password hash, the password entered
        // by the user is correct, and the method returns true.


        /*   private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
           {
               using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
               {
                   var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                   for (int i = 0; i < computedHash.Length; i++)
                   {
                       if (computedHash[i] != storedHash[i]) return false;
                   }
               }
               return true;
           } */

        // This method generates and returns a JWT token for a user.
        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            if (user.AccessLvl == 0)
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    JwtSecurityKey.Key,
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null,
                    null,
                    claims.ToArray(),
                    DateTime.Now,
                    DateTime.Now.AddDays(1)));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}