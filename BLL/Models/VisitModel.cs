namespace BLL.Models
{
    public class VisitModel
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }

        public int UserId { get; set; }
        public int PlaceId { get; set; }
    }
}