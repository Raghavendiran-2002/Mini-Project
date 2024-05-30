﻿using System.ComponentModel.DataAnnotations;

namespace PharmacyManagementSystem.Services
{
    public class UpdateUserDTO
    {
        [Required(ErrorMessage = "User id cannot be empty")]
        public int UserId { get; set; }
   
        public string Username { get; set; }
     
        public string Email { get; set; }

        public string FullName { get; set; }
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}