using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook_Parker
{
    public class Person
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
    }
}
