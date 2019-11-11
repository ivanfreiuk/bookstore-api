using System.Collections.Generic;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.Entities
{
    public class Author: IIdentifier
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}