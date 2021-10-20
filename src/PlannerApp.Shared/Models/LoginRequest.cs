﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Models
{
    public class LoginRequest
    {

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]//Test.123
        public string Password { get; set; }
    }
}
