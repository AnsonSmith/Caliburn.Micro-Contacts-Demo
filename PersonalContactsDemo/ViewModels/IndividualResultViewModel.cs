using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersonalContactsDemo.Models;
using Caliburn.Micro;
using PersonalContactsDemo.Framework.Results;

namespace PersonalContactsDemo.ViewModels
{
    public class IndividualResultViewModel : Screen
    {
        private readonly int _number;
        private readonly SearchResult _result;

        public IndividualResultViewModel(SearchResult result, int number)
        {
            _result = result;
            _number = number;
        }

        public int Number
        {
            get { return _number; }
        }

        public string PersonName
        {
            get { return _result.PersonName; }
        }

        public IEnumerable<IResult> Open()
        {
            QueryResult<PersonContactInfo> getPerson = new OpenPerson()
            {
                Id = _result.Id
            }.AsResult();

            yield return Show.Busy();
            yield return getPerson;
            yield return Show.Child<ExplorePersonContactViewModel>().In<ShellViewModel>()
                .Configured(x => x.WithPerson(getPerson.Response));
            yield return Show.NotBusy();
        }
    }
}
