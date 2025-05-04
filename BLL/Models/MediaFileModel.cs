using Domain.Entities.Enums;

namespace BLL.Models
{
    //Модель для представлення медіа файлів, пов'язаних з місцями
    public class MediaFileModel
    {
        public int Id { get; set; }
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