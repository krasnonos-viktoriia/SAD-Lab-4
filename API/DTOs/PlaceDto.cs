﻿namespace API.DTOs
{
    public class PlaceDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
