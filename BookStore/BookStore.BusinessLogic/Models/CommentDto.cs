using System;

namespace BookStore.BusinessLogic.Models
{
    public class CommentDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Headline { get; set; }

        public double Mark { get; set; }
        
        public string Content { get; set; }
        
        public DateTime PublicationDate { get; set; }

    }
}