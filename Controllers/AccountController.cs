using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialFirstApi.Data;
using SocialFirstApi.Dto;
using SocialFirstApi.Entities;
using SocialFirstApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialFirstApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        //Method to register a new user
        //Asynchronous task because we're conencting with the database
        /*When the HttpRequest is post. We send data in the body of the request
         which is sent to our api*/
        //When we send soemthing in the body of a request it should be sent out as an object to recieve the properties
        [HttpPost("register")]
        //public async Task<ActionResult<AppUser>> Register(string username, string password)
        //Before JWT
        //public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            //First check if the user already exists
            if(await UserExists(registerDto.Username))
            {
                return BadRequest("UserName Already Taken");
            }
            //Instantiating the hashing algorithm
            //using - calls the dispose method of the class
            //Any class that uses the dispose method implements the Idsisposable interface
            using var hmac = new HMACSHA512();

            //Create new user
            var user = new AppUser()
            {
                UserName = registerDto.Username.ToLower(),
                /*Encoding - because our password is a string and it needs to be parsed into
                 a byte array*/
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                /*HMACSHA512 class provides a random key when instantiated for the 1st time
                and that can be used for the Salt*/
                PasswordSalt = hmac.Key
            };

            //Adding the user to the database
            //But the 'Add' keyword just tracks the entity
            _context.Users.Add(user);
            //This is where we actually call the database and save the user
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        //Method to login a new user
        [HttpPost("login")]
        //Before adding the JWT
        //public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            
            //First thing is to get the user from the database
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if(user == null)
            {
                return Unauthorized("Invalid username");
            }
            //Next check for the Password
            //Calculate the computed hash for their password using the password salt
            /*HMACSHA512 key is going to be the PasswordSalt. Cos we will get the same computed hash
             of the password becuase we're giving it the same key that was used when the password was hashed
            in the fisrt place*/
            using var hmac = new HMACSHA512(user.PasswordSalt);
            //Then, we need to workout the password hash that's contained in the loginDto
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            //Since PasswordHash is a byte[] we need to loop over
             for(int i=0;i<computedHash.Length;i++)
             {
                if(computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Password is Not Valid");
                }

                
             }
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
           
        }
        //Helper method to check if username exists
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x=> x.UserName ==  username.ToLower());
        }
    }
}
