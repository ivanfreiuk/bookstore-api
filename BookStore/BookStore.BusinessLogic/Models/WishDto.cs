namespace BookStore.BusinessLogic.Models
{
    public class WishDto
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public BookDto Book { get; set; }

        public int BookCount { get; set; }
    }
}