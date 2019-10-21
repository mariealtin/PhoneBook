using PhoneBook.Database;
using PhoneBook.Database.Entities;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook.Services.api
{
    public class ContactService : IContactService
    {
        private IList<ContactViewModel> queryResults = null;

        public int AddContact(ContactViewModel contact)
        {
            IEnumerable<Entry> entries = null;
            if (contact.Entries.Any())
            {
                entries = contact.Entries.Select(e => new Entry()
                {
                    Descr = e.Descr,
                    ContactNum = e.ContactNum
                });
            }

            using (var context = new PhoneBookContext())
            {
                context.Contacts.Add(new Contact()
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Entries = entries.ToList()
                });

                try
                {
                    context.SaveChanges();
                    return 0;
                }
                catch
                {
                    return 1;
                }
            }
        }

        public ContactViewModel GetContact(int contactId)
        {
            try
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
                return contact;
            }
            catch
            {
                return null;
            }
        }

        public IList<ContactViewModel> GetContacts(string searchTerm)
        {
            if (String.IsNullOrEmpty(searchTerm))
                queryResults = getAll();
            else
            {
                try
                {
                    using (var context = new PhoneBookContext())
                    {
                        queryResults = context.Contacts
                            .Where(x => x.FirstName.Contains(searchTerm) || x.LastName.Contains(searchTerm))
                            .Select(x => new ContactViewModel()
                            {
                                ContactId = x.ContactId,
                                FirstName = x.FirstName,
                                LastName = x.LastName
                            }).ToList<ContactViewModel>();

                        if (queryResults.Any())
                        {
                            foreach (var contact in queryResults)
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
                }
                catch
                {
                    return null;
                }
            }
            return queryResults;
        }

        private IList<ContactViewModel> getAll()
        {
            try
            {
                using (var context = new PhoneBookContext())
                {
                    queryResults = context.Contacts
                        .Select(x => new ContactViewModel()
                        {
                            ContactId = x.ContactId,
                            FirstName = x.FirstName,
                            LastName = x.LastName
                        })
                        .OrderBy(x => x.LastName)
                        .ToList<ContactViewModel>();

                    if (queryResults.Any())
                    {
                        foreach (var contact in queryResults)
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
                return queryResults;
            }
            catch
            {
                return null;
            }
        }
    }
}