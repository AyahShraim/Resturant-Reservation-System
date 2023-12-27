namespace RestaurantReservation.Db.DataModels
{
    public class Customer
    {
        public int Id { get; set; }
        public required string FirstName { get; set; } 
        public required string LastName { get; set; }
        public string? Email { get; set; }
        public required string PhoneNumber { get; set; }

        public List<Reservation> Reservations { get; set; } = new();
    }
}
