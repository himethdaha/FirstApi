using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialFirstApi.Data;
using SocialFirstApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialFirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        //Get data From the Database and for that we need to use Dependency Injection
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //ENDPOINTS

        //EndPoint to get all users
        //ActionResult - What we are returning
        //<IEnumerable<AppUser>> - Return Type
        // GetUsers() - Method name
        /*SYNCHRONOUS CODE
         when a request is made to the database that thread is blocked 
        until the database fullfills the request*/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        //EndPoint to get a specific user
        //"{id}" - Root Parameter
        //When they hit this endpoint - api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

    
    }
}
