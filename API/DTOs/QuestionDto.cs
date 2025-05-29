namespace API.DTOs
{
    public class QuestionDto
    {
        public int? Id { get; set; }
        public string Text { get; set; } = null!;
        public string? Answer { get; set; }

        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public string? UserName { get; set; }
        public string? PlaceName { get; set; }
    }
}
