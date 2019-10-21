using PhoneBook.Models;
using PhoneBook.Services;
using System.Linq;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private IPhoneBookService phoneBookService = new PhoneBookService();

        // GET all contacts
        public ActionResult Index(string searchTerm)
        {
            var contacts = phoneBookService.GetContacts(searchTerm);
           
            if(!contacts.Any())
                ModelState.AddModelError(string.Empty, "An Error has occurred.");
            return View(contacts);
        }

        public ActionResult Create()
        {
            var contact = phoneBookService.CreateContact();
            return View(contact);
        }

        // Create a contact
        [HttpPost]
        public ActionResult Create(ContactViewModel contact)
        {
            int result = 0;
            if(ModelState.IsValid)
            {
                result = phoneBookService.CreateContact(contact);
                return RedirectToAction("Index");
            }

            if(result == 1)
            {
                ModelState.AddModelError(string.Empty, "An error has Occurred");
            }

            return View(contact);
        }
    }
}
