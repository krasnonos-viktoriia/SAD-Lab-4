using Domain.Entities.Enums;

namespace BLL.Models
{
    public class MediaFileModel
    {
        public int Id { get; set; }
        //public string FileType { get; set; }
        //public FileType FileType { get; set; } //= null!;
        public string FilePath { get; set; } = null!;
        public int PlaceId { get; set; }
        public PlaceModel? Place { get; set; }

        public FileTypeEnum FileType { get; set; }

        public enum FileTypeEnum
        {
            Photo,
            Video,
            Audio,
            Link,
            Other
        }
    }
}