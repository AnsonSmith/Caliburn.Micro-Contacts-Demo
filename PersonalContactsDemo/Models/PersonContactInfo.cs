using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalContactsDemo.Models
{
    public class PersonContactInfo
    {
        public Guid Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String HomePhone { get; set; }
        public String WorkPhone { get; set; }
        public String MobilePhone { get; set; }
        public String Email { get; set; }

    }
}
