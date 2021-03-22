using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialFirstApi.Entities
{
    //Basically the Model
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName{ get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
