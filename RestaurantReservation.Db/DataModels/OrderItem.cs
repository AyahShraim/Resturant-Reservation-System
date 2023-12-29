﻿namespace RestaurantReservation.Db.DataModels
{
    public class OrderItem
    {
        public int Id { get; set; }
        public required int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
