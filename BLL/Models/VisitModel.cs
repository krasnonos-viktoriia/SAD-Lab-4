namespace BLL.Models
{
    //Модель для представлення візиту користувача до місця
    public class VisitModel
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }

        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public UserModel? User { get; set; }
        public PlaceModel? Place { get; set; }
    }
}