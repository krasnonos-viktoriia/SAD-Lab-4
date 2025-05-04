namespace BLL.Models
{
    //Модель для представлення відгуку користувача про місце
    public class ReviewModel
    {
        public int Id { get; set; }
        public string Comment { get; set; } = null!;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public UserModel? User { get; set; }
        public PlaceModel? Place { get; set; }
    }
}