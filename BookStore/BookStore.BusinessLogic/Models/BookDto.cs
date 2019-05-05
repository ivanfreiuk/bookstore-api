using System;
using System.Collections.Generic;
using System.Text;
using BookStore.DataAccess.Entities;

namespace BookStore.BusinessLogic.Models
{
    public class BookDto
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int Mark { get; set; }

        public string Language { get; set; }

        public string Description { get; set; }

        public int PageSize { get; set; }

        public ICollection<AuthorDto> Authors { get; set; }

        public ICollection<CategoryDto> Categories { get; set; }
    }
}
