using CodingWikiWeb.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingWikiWeb.DataAccess.FluentConfig
{
    public class FluentBookDetailConfig : IEntityTypeConfiguration<Fluent_BookDetail>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookDetail> modelBuilder)
        {
            //name of table
            modelBuilder.ToTable("Fluent_BookDetails"); // rename a table

            //name of columns
            modelBuilder.Property(x => x.NumberOfChapters).HasColumnName("NoOfChapters"); // rename a column

            //primary key
            modelBuilder.HasKey(x => x.BookDetail_Id);// define primary key

            //other validation
            modelBuilder.Property(x => x.NumberOfChapters).IsRequired(); // setting is required
            
            //relations
            modelBuilder
                .HasOne(x => x.Books)
                .WithOne(x => x.BookDetails)
                .HasForeignKey<Fluent_BookDetail>(x => x.Book_Id); // one to one relation
        }
    }
}
