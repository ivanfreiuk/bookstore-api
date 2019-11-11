using System.Collections.Generic;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.Entities
{
    public class Category: IIdentifier
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}