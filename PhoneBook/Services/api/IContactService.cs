using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.api
{
    interface IContactService
    {
        IList<ContactViewModel> GetContacts(string searchTerm);

        ContactViewModel GetContact(int id);

        int AddContact(ContactViewModel contact);
    }
}
