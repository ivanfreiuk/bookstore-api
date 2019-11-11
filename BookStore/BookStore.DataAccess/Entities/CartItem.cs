using System;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.Entities
{
    public class CartItem: IIdentifier
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Quantity { get; set; }

        public bool IsOrdered { get; set; }

        public DateTime CreatedDate { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public int? OrderId { get; set; }
    }
}