﻿namespace ChatWebApi.Contracts.Member
{
    public class RegisterMemberRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordAgain { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
