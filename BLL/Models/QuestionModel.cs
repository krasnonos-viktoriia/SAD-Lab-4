namespace BLL.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string? Answer { get; set; }

        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public UserModel? User { get; set; }
        public PlaceModel? Place { get; set; }
    }
}