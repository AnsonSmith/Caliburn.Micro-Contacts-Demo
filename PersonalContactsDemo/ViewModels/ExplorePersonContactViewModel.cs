using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersonalContactsDemo.Models;
using Caliburn.Micro;

namespace PersonalContactsDemo.ViewModels
{
    public class ExplorePersonContactViewModel:Screen
    {
        private PersonContactInfo person;

        public string FirstName
        {
            get { return person == null ? String.Empty : person.FirstName; }
        }

        public string LastName
        {
            get { return person == null ? String.Empty : person.LastName; }
        }

        public String HomePhone
        {
            get{return person == null ? String.Empty : person.HomePhone;}
        }

        public String WorkPhone
        {
            get { return person == null ? String.Empty : person.WorkPhone; }
        }

        public String MobilePhone
        {
            get { return person == null ? String.Empty : person.MobilePhone; }
        }

        public String EmailAddress
        {
            get { return person == null ? String.Empty : person.Email; }
        }

        public void WithPerson(PersonContactInfo personContact)
        {
            this.person = personContact;
            NotifyOfPropertyChange(() => FirstName);
            NotifyOfPropertyChange(() => LastName);
        }


        public void ClosePerson()
        {
            base.TryClose();
        }

    }
}
