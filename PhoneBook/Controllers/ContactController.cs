using PhoneBook.Database;
using PhoneBook.Database.Entities;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PhoneBook.Controllers
{
    public class ContactController : ApiController
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
                });

                context.SaveChanges();
            }
            
            return Ok();
        }

    }
}
