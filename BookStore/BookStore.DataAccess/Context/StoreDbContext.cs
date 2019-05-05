using BookStore.DataAccess.Entities;
using BookStore.DataAccess.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Context
{
    public class StoreDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Mark> Marks { get; set; }

        public DbSet<Wish> Wishes { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region BookCategory

            builder.Entity<BookCategory>()
                .HasKey(bc => new {bc.BookId, bc.CategoryId});
            builder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);
            builder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

            #endregion

            #region BookAuthor

            builder.Entity<BookAuthor>()
                .HasKey(ba => new {ba.BookId, ba.AuthorId});
            builder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);
            builder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            #endregion

            #region Wish

            builder.Entity<Wish>().HasKey(w => new
            {
                w.BookId, w.UserId
            });

            #endregion

            #region Mark

            builder.Entity<Mark>().HasKey(w => new
            {
                w.BookId, w.UserId
            });

            #endregion
        }
    }
}
