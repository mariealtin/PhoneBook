using PhoneBook.Models;
using System.Collections.Generic;

namespace PhoneBook.Services
{
    interface IPhoneBookService
    {
        IEnumerable<ContactViewModel> GetContacts(string searchTerm);

        ContactViewModel CreateContact();

        int CreateContact(ContactViewModel contact);

    }
}
