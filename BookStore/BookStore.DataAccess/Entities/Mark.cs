namespace BookStore.DataAccess.Entities
{
    public class Mark
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public int MarkValue { get; set; }
    }
}