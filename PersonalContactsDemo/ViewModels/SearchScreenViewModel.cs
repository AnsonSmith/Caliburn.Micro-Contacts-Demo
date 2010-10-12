using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using PersonalContactsDemo.Framework.Results;
using System.Threading;
using PersonalContactsDemo.Models;

namespace PersonalContactsDemo.ViewModels
{
    public class SearchScreenViewModel:Screen
    {

        private readonly NoResultsViewModel noResults;
        private readonly ResultsViewModel results;
        
        private String searchText;
        public String SearchText
        {
            get { return searchText; }
            set
            {
                //Only want to fire Notifications if the value actually changes
                if (!String.Equals(value, SearchText))
                {
                    searchText = value;
                    NotifyOfPropertyChange(() => SearchText);
                    NotifyOfPropertyChange(() => CanExecuteSearch);
                }
            }
        }



        private object searchResults;
        public object SearchResults
        {
            get { return searchResults; }
            set
            {
                searchResults = value;
                NotifyOfPropertyChange(() => SearchResults);
            }
        }

        public SearchScreenViewModel(NoResultsViewModel noResults, ResultsViewModel results)
        {
            this.noResults = noResults;
            this.results = results;
        }

        public IEnumerable<IResult> ExecuteSearch()
        {

            QueryResult<IEnumerable<SearchResult>> search = new SearchContacts
            {
                SearchText = SearchText
            }.AsResult();
            
            yield return Show.Busy();
            yield return search;

            int resultCount = search.Response.Count();

            if (resultCount == 0)
                SearchResults = noResults.WithTitle(SearchText);
            else
            {
                SearchResults = results.With(search.Response);
            }

            yield return Show.NotBusy();

        }


        public bool CanExecuteSearch
        {
            get { return !string.IsNullOrEmpty(SearchText); }
        }

        protected override void OnActivate()
        {
            this.SearchText = null;
            this.SearchResults = null;
            base.OnActivate();
        }
    }
}
