using Microsoft.AspNetCore.Identity;
using Console.ApplicationCore.Interfaces;
using System;

namespace Console.ApplicationCore.Entities
{
    public class Role : IdentityRole<int>, IAuditable
    {
        public string Description { get; set; }
        public DateTime CreateAtUtc { get; set; }
        public DateTime? LastModifiedAtUtc { get; set; }
    }
}
