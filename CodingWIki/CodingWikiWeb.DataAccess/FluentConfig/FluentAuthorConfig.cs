using CodingWikiWeb.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingWikiWeb.DataAccess.FluentConfig
{
    public class FluentAuthorConfig : IEntityTypeConfiguration<Fluent_Author>
    {
        public void Configure(EntityTypeBuilder<Fluent_Author> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Author_Id);
            modelBuilder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Property(x => x.LastName).IsRequired();
            modelBuilder.Ignore(x => x.FullName);
        }
    }
}
