using CodingWikiWeb.DataAccess.FluentConfig;
using CodingWikiWeb.Model;
using CodingWikiWeb.Model.StoredProcAndView;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;

namespace CodingWikiWeb.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthorMap> BookAuthorMaps { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        //rename to Fluent_BookDetails
        public DbSet<Fluent_BookDetail> BookDetails_Fluent { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }
        public DbSet<Fluent_BookAuthorMap> Fluent_BookAuthorMaps { get; set; }
        public DbSet<MainBookDetails> MainBookDetails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer("Data Source=LAPTOP-5EIU9CFF\\SQLEXPRESS;Database=db_EFCore;Trusted_Connection=True;TrustServerCertificate=True")
        //        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(x => x.Price).HasPrecision(10, 5);
            modelBuilder.Entity<BookAuthorMap>().HasKey(x => new { x.Author_Id, x.Book_Id });

            modelBuilder.ApplyConfiguration(new FluentAuthorConfig());
            modelBuilder.ApplyConfiguration(new FluentBookAuthorMapConfig());
            modelBuilder.ApplyConfiguration(new FluentBookConfig());
            modelBuilder.ApplyConfiguration(new FluentBookDetailConfig());
            modelBuilder.ApplyConfiguration(new FluentPublisherConfig());
            
            modelBuilder.Entity<Book>().HasData(
                    new Book { BookId = 1, Title = "Spider without Duty", ISBN = "1234", Price = 10.99m, Publisher_Id = 1 },
                    new Book { BookId = 2, Title = "Fortune of time", ISBN = "321", Price = 11.99m, Publisher_Id = 2 }
                );

            var bookList = new Book[]
            {
                new Book { BookId = 3, Title = "Fake Sunday", ISBN = "77281", Price = 20.99m, Publisher_Id = 3 },
                new Book { BookId = 4, Title = "Cookie Jar", ISBN = "CC2213", Price = 25.99m, Publisher_Id = 1 },
                new Book { BookId = 5, Title = "Cloudy Forest", ISBN = "BV1231", Price = 40.99m, Publisher_Id = 2 }
            };

            modelBuilder.Entity<Book>().HasData(bookList);

            modelBuilder.Entity<Publisher>().HasData(
                    new Publisher { Publisher_Id = 1, Name = "Pub 1 Jimmy", Location = "Chicago" },
                    new Publisher { Publisher_Id = 2, Name = "Pub 2 John", Location = "New York" },
                    new Publisher { Publisher_Id = 3, Name = "Pub 3 Ben", Location = "Hawaii" }
                );

            modelBuilder.Entity<MainBookDetails>().HasNoKey().ToView("GetMainBookDetails");
        }
    }
}
