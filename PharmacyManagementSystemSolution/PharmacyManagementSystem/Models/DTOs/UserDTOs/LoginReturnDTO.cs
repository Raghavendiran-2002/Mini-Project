﻿namespace PharmacyManagementSystem.Models.DTOs
{
    public class LoginReturnDTO
    {
        public int UserID { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
