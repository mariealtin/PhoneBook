namespace dbConnectionTest
{
    using System.Data.Entity;

    public partial class PhoneBookContext : DbContext
    {
        public PhoneBookContext()
            : base("name=phoneBookContext")
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //TODO: these entities should have separate column-mappers
            var contact = modelBuilder.Entity<Contact>();
            contact.Property(p => p.ContactId).HasColumnName("contact_id");
            contact.Property(p => p.FirstName).HasColumnName("first_name");
            contact.Property(p => p.LastName).HasColumnName("last_name");

            var entry = modelBuilder.Entity<Entry>();
            entry.Property(p => p.EntryId).HasColumnName("entry_id");
            entry.Property(p => p.Descr).HasColumnName("descr");
            entry.Property(p => p.ContactNum).HasColumnName("contact_num");
            entry.Property(p => p.ContactId).HasColumnName("contact_id");
        }
    }
}
