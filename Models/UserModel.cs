﻿using ReadEase_C_.Interface;

namespace WebApplication1.Models
{
    public class UserModel : IUserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}