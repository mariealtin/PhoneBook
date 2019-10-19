using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Models
{
    public class ContactsViewModel
    {
        public ICollection<ContactViewModel> Contacts { get; set; }
    }
}