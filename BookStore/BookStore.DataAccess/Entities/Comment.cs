﻿using System;
using BookStore.DataAccess.Identity;
using BookStore.DataAccess.Repositories.Interfaces;

namespace BookStore.DataAccess.Entities
{
    public class Comment: IIdentifier
    {
        public int Id { get; set; }

        public string Headline { get; set; }

        public int Mark { get; set; }

        public string Content { get; set; }

        public DateTime PublicationDate { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
