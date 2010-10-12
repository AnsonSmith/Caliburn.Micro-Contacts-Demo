using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConductorOneActive.Models;
using Caliburn.Micro;

namespace ConductorOneActive.ViewModels
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
            //QueryResult<PersonContactInfo> getPerson = new GetGame
            //{
            //    Id = _result.Id
            //}.AsResult();

            //yield return Show.Busy();
            //yield return getGame;
            //yield return Show.Child<ExploreGameViewModel>().In<IShell>()
            //    .Configured(x => x.WithGame(getGame.Response));
            //yield return Show.NotBusy();
            return null;
        }
    }
}
