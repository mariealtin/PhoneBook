using PhoneBook.Database.Entities;
using PhoneBook.Database.Mappers;
using System.Data.Entity;

namespace PhoneBook.Database
{
    public partial class PhoneBookContext : DbContext
    {
        public PhoneBookContext() : base("name=dbPhoneBook")
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var contact = new EntityMapperContext(new ContactMapper(modelBuilder.Entity<Contact>()));
            contact.Map();
                        
            var entry = new EntityMapperContext(new EntryMapper(modelBuilder.Entity<Entry>()));
            entry.Map();

        }
    }
}