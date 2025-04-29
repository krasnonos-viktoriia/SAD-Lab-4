namespace Domain.Entities
{
    // Клас, що представляє місце з різними атрибутами та зв'язками з іншими сутностями.
    public class Place
    {
        // Ідентифікатор місця.
        public int Id { get; set; }

        // Назва місця.
        public string Name { get; set; } = null!;

        // Адреса місця.
        public string Address { get; set; } = null!;

        // Опис місця.
        public string Description { get; set; } = null!;

        // Зв'язки з відгуками, питаннями, медіафайлами та відвідуваннями місця.
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public virtual ICollection<MediaFile> MediaFiles { get; set; } = new List<MediaFile>();
        public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}