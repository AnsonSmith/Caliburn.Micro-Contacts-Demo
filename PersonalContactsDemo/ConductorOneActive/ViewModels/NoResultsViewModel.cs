using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using ConductorOneActive.Framework.Results;


namespace ConductorOneActive.ViewModels
{
    public class NoResultsViewModel:Screen
    {
        private string searchText;

        public IResult AddPersonContact()
        {
            return  Show.Child<AddPersonContactViewModel>()
                .In<ShellViewModel>()
                .Configured(x => x.LastName = searchText);
        }


        public NoResultsViewModel WithTitle(string title)
        {
            this.searchText = title;
            return this;
        }
    }
}
