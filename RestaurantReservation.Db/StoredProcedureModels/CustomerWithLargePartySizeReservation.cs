namespace RestaurantReservation.Db.StoredProcedureModels
{
    public class CustomerWithLargePartySizeReservation
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public int PartySize { get; set; }
    }
}
