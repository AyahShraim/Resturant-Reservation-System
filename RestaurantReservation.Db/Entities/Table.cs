namespace RestaurantReservation.Db.Entities
{
    public class Table : BaseEntity
    {
        public int Capacity { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;

        public List<Reservation> Reservations { get; set; } = new();
    }
}
