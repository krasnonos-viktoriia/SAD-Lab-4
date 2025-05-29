namespace API.DTOs
{
    public class VisitDto
    {
        public int? Id { get; set; }
        public DateTime VisitDate { get; set; }

        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public string? UserName { get; set; }
        public string? PlaceName { get; set; }
    }
}
