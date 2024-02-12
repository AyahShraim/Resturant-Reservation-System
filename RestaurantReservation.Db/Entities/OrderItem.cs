﻿namespace RestaurantReservation.Db.Entities
{
    public class OrderItem : BaseEntity
    {
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null!;
    }
}
