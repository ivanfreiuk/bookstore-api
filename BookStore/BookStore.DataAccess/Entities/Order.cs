using System;
using System.Collections.Generic;
using BookStore.DataAccess.Identity;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.Entities
{
    public class Order: IIdentifier
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedDate { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}