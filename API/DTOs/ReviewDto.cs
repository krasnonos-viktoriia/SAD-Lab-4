namespace API.DTOs
{
    public class ReviewDto
    {
        public int? Id { get; set; }
        public string Comment { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public string? UserName { get; set; }
        public string? PlaceName { get; set; }
    }
}
