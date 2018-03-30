using System.Data.Entity.ModelConfiguration;

namespace HomeLibrary.API.Domain.Mappings
{
    public class BookShelfMap : EntityTypeConfiguration<BookShelf>
    {
        public BookShelfMap()
        {
            ToTable("TBookShelf");

            HasKey(t => t.Id);
            Property(t => t.Id).
                HasColumnName("BookShelfId");

            Property(t => t.Name).
                HasColumnName("Name").
                HasMaxLength(255).
                IsRequired();
        }
    }
}