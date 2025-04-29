using Domain.Entities.Enums;

namespace Domain.Entities
{
    // Клас, що представляє користувача з його даними та зв'язками з відгуками, питаннями та відвідуваннями.
    public class User
    {
        // Ідентифікатор користувача.
        public int Id { get; set; }

        // Ім'я користувача.
        public string Name { get; set; } = null!;

        // Електронна адреса користувача.
        public string Email { get; set; } = null!;

        // Роль користувача (наприклад, менеджер, клієнт).
        public Role Role { get; set; }

        // Колекція відгуків, залишених користувачем.
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        // Колекція питань, заданих користувачем.
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

        // Колекція відвідувань, здійснених користувачем.
        public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}