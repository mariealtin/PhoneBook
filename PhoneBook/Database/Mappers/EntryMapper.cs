using PhoneBook.Database.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PhoneBook.Database.Mappers
{
    public class EntryMapper : IEntityMapper
    {
        private EntityTypeConfiguration<Entry> config;

        public EntryMapper(EntityTypeConfiguration<Entry> entityTypeConfiguration)
        {
            this.config = entityTypeConfiguration;
        }

        public void MapEntity()
        {
            this.config.Property(p => p.EntryId).HasColumnName("entry_id");
            this.config.Property(p => p.Descr).HasColumnName("descr");
            this.config.Property(p => p.ContactNum).HasColumnName("contact_num");
            this.config.Property(p => p.ContactId).HasColumnName("contact_id");
        }
    }
}