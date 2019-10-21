using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class ContactViewModel
    {
        public int ContactId { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        public List<EntryViewModel> Entries { get; set; }
    }
}