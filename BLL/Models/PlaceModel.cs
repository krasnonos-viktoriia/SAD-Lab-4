namespace BLL.Models
{
    //Модель для представлення інформації про визначне місце
    public class PlaceModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}