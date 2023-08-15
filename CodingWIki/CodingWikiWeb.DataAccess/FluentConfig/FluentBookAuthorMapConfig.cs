using CodingWikiWeb.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingWikiWeb.DataAccess.FluentConfig
{
    public class FluentBookAuthorMapConfig : IEntityTypeConfiguration<Fluent_BookAuthorMap>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookAuthorMap> modelBuilder)
        {
            //many to many
            modelBuilder
                .HasOne(x => x.Book)
                .WithMany(x => x.Fluent_BookAuthorMap)
                .HasForeignKey(x => x.Book_Id); // 1 to many
            modelBuilder
               .HasOne(x => x.Author)
               .WithMany(x => x.Fluent_BookAuthorMap)
               .HasForeignKey(x => x.Author_Id); // 1 to many
            modelBuilder.HasKey(x => new { x.Author_Id, x.Book_Id });
        }
    }
}
