using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string street { get; set; } = default!;
        public string city { get; set; } = default!;
        public string country { get; set; } = default!;
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
