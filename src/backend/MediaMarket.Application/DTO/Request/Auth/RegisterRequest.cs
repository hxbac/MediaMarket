﻿namespace MediaMarket.Application.DTO.Request.Auth
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
