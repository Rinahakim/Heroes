﻿using System.ComponentModel.DataAnnotations;

namespace Heroes.Models
{
    public class SignupModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
