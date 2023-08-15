using CodingWikiWeb.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingWikiWeb.DataAccess.FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> modelBuilder)
        {
            modelBuilder.Property(x => x.ISBN).HasMaxLength(50); // setting a maxlengt of a property
            modelBuilder.Property(x => x.ISBN).IsRequired(); // setting is required
            modelBuilder.Ignore(x => x.PriceRange);// not mapped
            modelBuilder.HasKey(x => x.BookId);// define primary key
            modelBuilder
                .HasOne(x => x.Publishers)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.Publisher_Id); // one to many relation
        }
    }
}
