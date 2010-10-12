using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using PersonalContactsDemo.Models;

namespace PersonalContactsDemo.ViewModels
{
    public class ResultsViewModel : Screen
    {
        public ResultsViewModel()
        {
            Results = new ObservableCollection<IndividualResultViewModel>();
        }

        public string Message
        {
            get
            {
                if (Results.Count == 1)
                    return "1 Match Found";
                return Results.Count + " Matches Found";
            }
        }

        public ObservableCollection<IndividualResultViewModel> Results { get; private set; }

        public ResultsViewModel With(IEnumerable<SearchResult> searchResults)
        {
            Results.Clear();

            int number = 1;

            foreach (SearchResult result in searchResults)
            {
                Results.Add(new IndividualResultViewModel(result, number));
                number++;
            }

            NotifyOfPropertyChange(() => Message);
            return this;
        }
    }
}
