using PhoneBook.Database.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PhoneBook.Database.Mappers
{
    public class ContactMapper : IEntityMapper
    {
        private EntityTypeConfiguration<Contact> config;

        public ContactMapper(EntityTypeConfiguration<Contact> entityTypeConfiguration)
        {
            this.config = entityTypeConfiguration;
        }

        public void MapEntity()
        {
            this.config.Property(p => p.ContactId).HasColumnName("contact_id");
            this.config.Property(p => p.FirstName).HasColumnName("first_name");
            this.config.Property(p => p.LastName).HasColumnName("last_name");
        }
    }
}