namespace BLL.Models
{
    public class MediaFileModel
    {
        public int Id { get; set; }
        public string FileType { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public int PlaceId { get; set; }
    }
}