using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelcukElektromobilWebsite.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }

        public Contact()
        {

        }

    }
}
