using RestaurantReservation.Db.Enums;

namespace RestaurantReservation.Db.ViewsModels

{
    public class EmployeesWithRestaurantDetails
    {
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public EmployeePosition Position { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantPhone { get; set; }
    }
}
