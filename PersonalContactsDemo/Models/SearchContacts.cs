using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalContactsDemo.Models
{
    public class SearchContacts:IQuery<IEnumerable<SearchResult>>
    {
        public string SearchText { get; set; }
    }
}
