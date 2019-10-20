using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private const string BASE_ADDRESS = "http://localhost:51846/api/phonebook";

        // GET all contacts
        public ActionResult Index(string searchTerm)
        {
            // TODO: improve this parameter string, possibly use uribuilder
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
                
                if(result.IsSuccessStatusCode)
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
            return View();
        }

        // Create a contact
        [HttpPost]
        public ActionResult Create(ContactViewModel contact)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_ADDRESS);

                var post = client.PostAsJsonAsync<ContactViewModel>("phonebook", contact);
                post.Wait();

                var result = post.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "An error has Occurred");

            return View(contact);
        }
    }
}
