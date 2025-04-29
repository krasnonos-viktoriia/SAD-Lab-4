using Domain.Entities.Enums;

namespace Domain.Entities
{
    // Клас, що представляє медіафайл, який асоційовано з місцем (Place) і має тип файлу.
    public class MediaFile
    {
        // Ідентифікатор медіафайлу.
        public int Id { get; set; }

        // Тип медіафайлу.
        public FileType FileType { get; set; }

        // Шлях до файлу.
        public string FilePath { get; set; } = null!;

        // Зв'язок з місцем (Place), до якого належить медіафайл.
        public int PlaceId { get; set; }

        // Навігаційна властивість для місця (Place).
        public virtual Place Place { get; set; } = null!;
    }
}