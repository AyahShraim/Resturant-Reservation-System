namespace RestaurantReservation.Db.DataModels
{
    public class Restaurant
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string OpeningHours { get; set; }

        public List<Table> Tables { get; set; } = new();
        public List<Reservation> Reservations { get; set; } = new();
        public List<Employee> Employees { get; set; } = new();
        public List<MenuItem> MenuItems { get; set; } = new();
    }
}
