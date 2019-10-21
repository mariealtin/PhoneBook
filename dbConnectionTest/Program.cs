using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbConnectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new PhoneBookContext())
            {
                var contacts = context.Contacts.FirstOrDefault(x => x.LastName == "Rabbit");

                Console.WriteLine(contacts.FirstName + " " + contacts.LastName);
                foreach(var i in contacts.Entries)
                {
                    Console.WriteLine(i.ContactNum + " " + i.Descr);
                }

                //var query = from c in context.Contacts
                //            orderby c.LastName
                //            select c;

                //foreach(var c in contacts)
                //{
                //    Console.WriteLine(String.Format("{0}, {1}", c.LastName, c.FirstName));
                //}
            }
            Console.ReadKey();
            
        }
    }
}
