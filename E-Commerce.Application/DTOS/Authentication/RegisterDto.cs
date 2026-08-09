using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOS.Authentication
{
    public class RegisterDto
    {
        [Required,EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string password { get; set; } = default!;
        [Required]
        public string userName { get; set; } = default!;
        [Required]
        public string DisplayName { get; set; } = default!;
        [Phone]
        public string Phonenumber { get; set; }

    }
}
