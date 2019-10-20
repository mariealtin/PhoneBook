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
        // GET all contacts without search criteria
        public ActionResult Index(string searchTerm)
        {
            string queryPath = "phonebook";
            if (!String.IsNullOrEmpty(searchTerm))
            {
                queryPath += String.Format(("?searchTerm={0}"), searchTerm);
            }
            var contacts = Enumerable.Empty<ContactViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51846/api/");
                
                var response = client.GetAsync(queryPath);
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
    }
}
