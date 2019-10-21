using System.Collections.Generic;

namespace PhoneBook.Models
{
    public class ContactViewModel
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<EntryViewModel> Entries { get; set; }
    }
}