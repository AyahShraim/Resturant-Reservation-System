﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Db.Entities
{
    public class MenuItem : BaseEntity
    {

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
