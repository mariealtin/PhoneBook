using PhoneBook.Models;
using System.Collections.Generic;

namespace PhoneBook.Services.api
{
    interface IContactService
    {
        IList<ContactViewModel> GetContacts(string searchTerm);

        ContactViewModel GetContact(int id);

        int AddContact(ContactViewModel contact);
    }
}
