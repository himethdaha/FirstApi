using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialFirstApi.Entities;
using SocialFirstApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialFirstApi.Services
{
    public class TokenService : ITokenService
    {
        //_key is created to make sure our token is signed and signature is verified
        private readonly SymmetricSecurityKey _key;

        //Ctor to inject configuration
        public TokenService(IConfiguration config)
        {
            //_key is a string so got to make it a byte[]
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            //Logic to create the token
            //Claims we put inside the token
            var claims = new List<Claim>
            {
                //NameId to store username
                //We'll be doing everything thorugh the username in our jwt
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //Describe the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            //Token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Return the written token to whoever needs it
            return tokenHandler.WriteToken(token);
        }

       
    }
}
