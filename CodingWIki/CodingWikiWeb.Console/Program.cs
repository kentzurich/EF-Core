using CodingWikiWeb.DataAccess.Data;
using CodingWikiWeb.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

Console.WriteLine("Hello World!");

//using (ApplicationDbContext context = new())
//{
//    context.Database.EnsureCreated();

//    if (context.Database.GetPendingMigrations().Count() > 0)
//        context.Database.Migrate();
//}

//AddBook();
//GetAllBooks();
//GetBook();
//UpdateBook();
//DeleteBook();

//async void DeleteBook()
//{
//    using var context = new ApplicationDbContext();
//    var books = await context.Books.FindAsync(8);
//    context.Books.Remove(books);
//    await context.SaveChangesAsync();
//}

//async void UpdateBook()
//{
//    using var context = new ApplicationDbContext();
//    //var books = context.Books.Find(7);
//    var books = await context.Books.Where(x => x.Publisher_Id == 1).ToListAsync();
//    //books.ISBN = "1010";
//    foreach (var book in books)
//        book.Price = 55.55m;
//    await context.SaveChangesAsync();
//}

//async void GetBook()
//{
//    using var context = new ApplicationDbContext();
//    //var inputTitle = "Cloudy Forest"; // auto create scalar variable
//    //var book = context.Fluent_Books.First();// expects a data. using select top 1
//    //var book = context.Fluent_Books.FirstOrDefault(); // if no records are found, it will return null. using select top 1
//    //var book = context.Books.FirstOrDefault(x => x.Title == inputTitle);
//    //var book = context.Books.Find(7); // for searching primary key
//    //var book = context.Books.Single(x => x.Publisher_Id == 2); // expects 1 element if more than 1 throws exception. can filter any column. selects top 2
//    //var books = context.Books.Where(x => x.ISBN.Contains("12"));// like operator in sql. "%value%"
//    //var book = context.Books.Where(x => EF.Functions.Like(x.ISBN, ("12%"))).Max(x => x.Price);// like operator in sql. "value%"
//    //var books = context.Books;//deferred execution. not directly executed
//    //var books = context.Books.OrderBy(x => x.Title)// asc order
//    //sorting data
//    //var books = context.Books.Where(x => x.Price > 10).OrderBy(x => x.Title).ThenByDescending(x => x.ISBN); // order by multiple columns
//    var books = await context.Books.Skip(0).Take(2).ToListAsync();

//    foreach (var book in books)
//        Console.WriteLine($"{book.Title} - {book.ISBN}");

//    var books1 = await context.Books.Skip(4).Take(1).ToListAsync();

//    foreach (var book in books1)
//        Console.WriteLine($"{book.Title} - {book.ISBN}");
//}

//void GetAllBooks()
//{
//    using var context = new ApplicationDbContext();
//    var books = context.Books.ToList();// query gets executed right away
//    foreach (var book in books)
//        Console.WriteLine($"{book.Title} - {book.ISBN}");
//}

//async void AddBook()
//{
//    Book book = new()
//    {
//        Title = "New EF Core Book",
//        ISBN = "999999",
//        Price = 10.93m,
//        Publisher_Id = 1
//    };
//    using var context = new ApplicationDbContext();
//    var books = await context.Books.AddAsync(book);
//    await context.SaveChangesAsync();
//}