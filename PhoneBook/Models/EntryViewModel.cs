using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class EntryViewModel
    {
        public int EntryId { get; set; }

        [StringLength(20)]
        public string Descr { get; set; }

        [StringLength(15)]
        public string ContactNum { get; set; }
        public int ContactId { get; set; }
    }
}