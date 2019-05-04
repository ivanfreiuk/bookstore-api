using System.Collections.Generic;
using BookStore.DataAccess.Entities;

namespace BookStore.BusinessLogic.Models
{
    public class BookShortDetail
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}