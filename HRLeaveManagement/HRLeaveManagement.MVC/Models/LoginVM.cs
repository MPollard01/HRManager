﻿using System.ComponentModel.DataAnnotations;

namespace HRLeaveManagement.MVC.Models
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
