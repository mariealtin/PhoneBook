using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Database.Entities
{
    [Table("entry")]
    public partial class Entry
    {
        [Key]
        public int EntryId { get; set; }

        [Required]
        [StringLength(20)]
        public string Descr { get; set; }

        [Required]
        [StringLength(15)]
        public string ContactNum { get; set; }

        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}