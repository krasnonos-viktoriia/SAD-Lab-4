namespace Domain.Entities
{
    // Клас, що представляє відгук користувача про місце, включаючи коментар, рейтинг та дату створення.
    public class Review
    {
        // Ідентифікатор відгуку.
        public int Id { get; set; }

        // Текстовий коментар до відгуку.
        public string Comment { get; set; } = null!;

        // Рейтинг, що ставиться користувачем (від 1 до 5).
        public int Rating { get; set; }

        // Дата створення відгуку.
        public DateTime CreatedAt { get; set; }

        // Ідентифікатор користувача, який залишив відгук.
        public int UserId { get; set; }

        // Зв'язок з користувачем, який залишив відгук.
        public virtual User User { get; set; } = null!;

        // Ідентифікатор місця, для якого залишено відгук.
        public int PlaceId { get; set; }

        // Зв'язок з місцем, для якого залишено відгук.
        public virtual Place Place { get; set; } = null!;
    }
}
