using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalContactsDemo.Models
{
    public class AddPersonToContacts:ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string EmailAddress { get; set; }
        
    }
}

