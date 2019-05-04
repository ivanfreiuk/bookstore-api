using System.Collections.Generic;

namespace BookStore.DataAccess.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public int PageSize { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}