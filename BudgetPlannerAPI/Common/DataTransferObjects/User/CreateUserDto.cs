﻿namespace Common.DataTransferObjects.User
{
    public class CreateUserDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
