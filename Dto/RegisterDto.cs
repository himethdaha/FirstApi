using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialFirstApi.Dto
{
    public class RegisterDto
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="UserName can't be emplty")]
        public string Username{ get; set; }

        [Required]
        [MinLength(6, ErrorMessage ="Password Should contain at least 6 characters")]
        public string Password{ get; set; }
    }
}
