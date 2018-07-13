using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClientAPI.Data.Shared.DTOs;
using ClientAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ClientAPI.Data.Shared {
    public class JWTGenerateToken {
        public object GenerateJwtToken (string email, int id, UserDTO user, IConfiguration _config) {

            var claims = new [] {
                new Claim (JwtRegisteredClaimNames.Sub, user.Username),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ()),
            };

            var jwtkey = _config["Jwt:Key"];
            var issuer = _config["Jwt:JwtIssuer"];

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_config["Jwt:Key"]));

            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken (
                _config["Jwt:JwtIssuer"],
                _config["Jwt:JwtIssuer"],
                claims,
                expires : DateTime.Now.AddMinutes (30),
                signingCredentials : creds);
            var tokenString = new JwtSecurityTokenHandler ().WriteToken (token);
            return tokenString;
        }
        // public object GenerateJwtToken(string email, int id, ApplicationUser user, IConfiguration _config)
        // {
        //     var tokenHandler = new JwtSecurityTokenHandler();

        //     var tokenVal = _config.GetSection("AppSettings:Token").Value;

        //     var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);

        //     var tokenDescriptor = new SecurityTokenDescriptor {
        //         Subject = new ClaimsIdentity(new Claim[]{
        //             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //             new Claim(ClaimTypes.Name, user.Email)
        //         }),

        //         Expires = DateTime.Now.AddDays(1),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //         SecurityAlgorithms.HmacSha512Signature)
        //     };

        //     var token = tokenHandler.CreateToken(tokenDescriptor);

        //     var tokenString = tokenHandler.WriteToken(token);

        //     return tokenString;
        // }
    }
}