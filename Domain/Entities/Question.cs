namespace Domain.Entities
{
    // Клас, що представляє питання, яке може бути задано користувачем для певного місця.
    public class Question
    {
        // Ідентифікатор питання.
        public int Id { get; set; }

        // Текст питання.
        public string Text { get; set; } = null!;

        // Відповідь на питання (може бути відсутньою).
        public string? Answer { get; set; }

        // Ідентифікатор користувача, який задав питання (тільки менеджер може створити).
        public int UserId { get; set; }

        // Зв'язок з користувачем, який створив питання.
        public virtual User User { get; set; } = null!;

        // Ідентифікатор місця, до якого відноситься питання.
        public int PlaceId { get; set; }

        // Зв'язок з місцем, для якого задано питання.
        public virtual Place Place { get; set; } = null!;
    }
}