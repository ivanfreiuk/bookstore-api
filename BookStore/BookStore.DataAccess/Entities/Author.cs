using System.Collections.Generic;

namespace BookStore.DataAccess.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}