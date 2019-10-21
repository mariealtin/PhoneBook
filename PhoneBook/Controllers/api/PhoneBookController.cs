using PhoneBook.Database;
using PhoneBook.Database.Entities;
using PhoneBook.Models;
using PhoneBook.Services.api;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PhoneBook.Controllers
{
    public class PhoneBookController : ApiController
    {
        private IContactService contactService = new ContactService();
        public IHttpActionResult GetAllContacts()
        {
            IList<ContactViewModel> contacts = contactService.GetContacts(null);

            if(!contacts.Any())
            {
                return NotFound();
            }
 
            return Ok(contacts);
        }

        public IHttpActionResult GetAllContacts(string searchTerm)
        {
            IList<ContactViewModel> contacts = contactService.GetContacts(searchTerm);

            if (!contacts.Any())
            {
                return NotFound();
            }

            return Ok(contacts);
        }

        // This is for edit existing contact, not implemented 
        public IHttpActionResult GetContact(int contactId)
        {
            ContactViewModel contact = contactService.GetContact(contactId);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        public IHttpActionResult PostContact(ContactViewModel contact)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            int result = contactService.AddContact(contact);
            if (result == 1)
            {
                return BadRequest("One or more errors occurred during save");
            }
            
            return Ok();
        }
    }
}
