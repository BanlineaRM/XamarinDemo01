using System.Collections.Generic;
using BanlineaTest.Contacts.Models;

namespace BanlineaTest.Contacts.Net
{
    public class RegisterRequest 
    {
        public List<Contact> Contacts { get; set; }
        public Location Location { get; set; }
        public Author RegisteredBy { get; set; }
        public int Type => 1;
    }
}
