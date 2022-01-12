﻿using Microsoft.AspNetCore.Identity;
using System;

namespace DiverseBookApp.Models
{
    public class ApplicationUsers : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
