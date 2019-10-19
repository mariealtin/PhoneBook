using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Models
{
    public class EntryViewModel
    {
        public int EntryId { get; set; }
        public string Descr { get; set; }
        public string ContactNum { get; set; }
        public int ContactId { get; set; }
    }
}