using System;

namespace BookStore.BusinessLogic.Models
{
    public class CartItemDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Quantity { get; set; }

        public bool IsOrdered { get; set; }

        public DateTime CreatedDate { get; set; }

        public int BookId { get; set; }

        public BookDto Book { get; set; }

        public int? OrderId { get; set; }
    }
}
