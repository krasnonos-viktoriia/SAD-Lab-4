namespace API.DTOs
{
    public class MediaFileDto
    {
        public int? Id { get; set; }
        public string FilePath { get; set; } = null!;
        public int PlaceId { get; set; }

        public string FileType { get; set; } = null!; // "Photo", "Video", "Audio", "Link", "Other"
        public string? PlaceName { get; set; }
    }
}
