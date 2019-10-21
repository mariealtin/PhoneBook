using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private const string BASE_ADDRESS = "http://localhost:51846/api/phonebook";

        // GET all contacts
        public ActionResult Index(string searchTerm)
        {
            string urlString = "";
            if (!String.IsNullOrEmpty(searchTerm))
            {
                urlString = String.Format(("?searchTerm={0}"), searchTerm);
            }

            var contacts = Enumerable.Empty<ContactViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_ADDRESS);

                var response = client.GetAsync(urlString);
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsAsync<IList<ContactViewModel>>();
                    content.Wait();
                    contacts = content.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error has occurred.");
                }

            }
            return View(contacts);
        }

        public ActionResult Create()
        {
            // TODO: Ideally user should be able to add as many numbers as he likes, this base set is just for demo
            var contact = new ContactViewModel();
            contact.Entries = new List<EntryViewModel>();
            contact.Entries.Add(new EntryViewModel { Descr = "Home" });
            contact.Entries.Add(new EntryViewModel { Descr = "Mobile" });
            contact.Entries.Add(new EntryViewModel { Descr = "Work" });
            return View(contact);
        }

        // Create a contact
        [HttpPost]
        public ActionResult Create(ContactViewModel contact)
        {
            if(ModelState.IsValid)
            {
                contact.Entries.RemoveAll(x => String.IsNullOrEmpty(x.ContactNum));

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BASE_ADDRESS);

                    var post = client.PostAsJsonAsync<ContactViewModel>("phonebook", contact);
                    post.Wait();

                    var result = post.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "An error has Occurred");

            return View(contact);
        }
    }
}
