using PhoneBook.Database;
using PhoneBook.Database.Entities;
using PhoneBook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PhoneBook.Controllers
{
    public class PhoneBookController : ApiController
    {
        public IHttpActionResult GetAllContacts()
        {
            IList<ContactViewModel> contacts = null;

            using (var context = new PhoneBookContext())
            {
                contacts = context.Contacts
                    .Select(x => new ContactViewModel()
                    {
                        ContactId = x.ContactId,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    })
                    .OrderBy(x => x.LastName)
                    .ToList<ContactViewModel>();

                if (contacts.Any())
                {
                    foreach (var contact in contacts)
                    {
                        var entries = context.Entries
                            .Where(x => x.ContactId == contact.ContactId)
                            .Select(e => new EntryViewModel()
                            {
                                ContactId = e.ContactId,
                                EntryId = e.EntryId,
                                Descr = e.Descr,
                                ContactNum = e.ContactNum
                            })
                            .OrderBy(e => e.Descr)
                            .ToList<EntryViewModel>();
                        contact.Entries = entries.ToList<EntryViewModel>();
                    }
                }
            }

            if(!contacts.Any())
            {
                return NotFound();
            }
 
            return Ok(contacts);
        }

        public IHttpActionResult GetAllContacts(string searchTerm)
        {
            IList<ContactViewModel> contacts = null;

            using (var context = new PhoneBookContext())
            {
                contacts = context.Contacts
                    .Where(x => x.FirstName.Contains(searchTerm) || x.LastName.Contains(searchTerm))
                    .Select(x => new ContactViewModel()
                    {
                        ContactId = x.ContactId,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    }).ToList<ContactViewModel>();

                if (contacts.Any())
                {
                    foreach (var contact in contacts)
                    {
                        var entries = context.Entries
                            .Where(x => x.ContactId == contact.ContactId)
                            .Select(e => new EntryViewModel()
                            {
                                ContactId = e.ContactId,
                                EntryId = e.EntryId,
                                Descr = e.Descr,
                                ContactNum = e.ContactNum
                            }).ToList<EntryViewModel>();
                        contact.Entries = entries.ToList<EntryViewModel>();
                    }
                }
            }

            if (!contacts.Any())
            {
                return NotFound();
            }

            return Ok(contacts);
        }

        public IHttpActionResult GetContact(int contactId)
        {
            ContactViewModel contact = null;

            using (var context = new PhoneBookContext())
            {
                contact = context.Contacts
                    .Where(x => x.ContactId == contactId)
                    .Select(x => new ContactViewModel()
                    {
                        ContactId = x.ContactId,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    }).FirstOrDefault();

                if (contact != null)
                {
                    var entries = context.Entries
                        .Where(x => x.ContactId == contact.ContactId)
                        .Select(e => new EntryViewModel()
                        {
                            ContactId = e.ContactId,
                            EntryId = e.EntryId,
                            Descr = e.Descr,
                            ContactNum = e.ContactNum
                        }).ToList<EntryViewModel>();
                    contact.Entries = entries.ToList<EntryViewModel>();
                }
            }

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

            using(var context = new PhoneBookContext())
            {
                context.Contacts.Add(new Contact()
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName
                    //, Entries = (ICollection<Entry>)contact.Entries
                });

                context.SaveChanges();
            }
            
            return Ok();
        }
    }
}
