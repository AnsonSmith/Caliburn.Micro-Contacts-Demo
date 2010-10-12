using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalContactsDemo.Models
{
    public class OpenPerson:IQuery<PersonContactInfo>
    {
        public Guid Id { get; set; }
    }
}
