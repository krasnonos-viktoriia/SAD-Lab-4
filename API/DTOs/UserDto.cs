﻿namespace API.DTOs
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!; // "Regular" або "Manager"
    }
}
