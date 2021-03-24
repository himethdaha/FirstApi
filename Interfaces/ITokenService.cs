using SocialFirstApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialFirstApi.Interfaces
{
    public interface ITokenService
    {

        //JWT are strings
        public string CreateToken(AppUser user);
    }
}
