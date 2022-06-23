using Microsoft.AspNetCore.Identity;
using System;

namespace Console.ApplicationCore.Entities
{
    public class User : IdentityUser<int>
    {
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
    }
}
