﻿using System.ComponentModel.DataAnnotations;

namespace JWTMicroservice.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string EmailId { get; set; }
    }
}
