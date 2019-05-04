namespace BookStore.ViewModels
{
    public class CreateBookModel
    {
        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public int PageSize { get; set; }
    }
}