using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;

namespace PersonalContactsDemo.Models
{
    public class FakeBackend : IBackend
    {
        private readonly List<PersonContactInfo> people = new List<PersonContactInfo>
                                                    {
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Townes",
                                                                LastName = "Van Zandt",
                                                                HomePhone = "111-111-1111",
                                                                WorkPhone = "222-222-2222",
                                                                MobilePhone = "333-333-3333",
                                                                Email = "Townes@PanchoAndLefty.com"
                                                            },
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Johnny",
                                                                LastName = "Cash",
                                                                HomePhone = "444-444-4444",
                                                                WorkPhone = "555-555-5555",
                                                                MobilePhone = "666-666-6666",
                                                                Email = "Man@InBlack.net"
                                                            },
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Waylon",
                                                                LastName = "Jennings",
                                                                HomePhone = "777-777-7777",
                                                                WorkPhone = "888-888-8888",
                                                                MobilePhone = "999-999-9999",
                                                                Email = "Watasha@outlaws.net"
                                                            },
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Willie",
                                                                LastName = "Nelson",
                                                                HomePhone = "111-111-1111",
                                                                WorkPhone = "222-222-2222",
                                                                MobilePhone = "333-333-3333",
                                                                Email = "RedheadedStranger@outlaws.net"
                                                            },
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Merle",
                                                                LastName = "Haggard",
                                                                HomePhone = "444-444-4444",
                                                                WorkPhone = "555-555-5555",
                                                                MobilePhone = "666-666-6666",
                                                                Email = "Okie@Muskogee.com"
                                                            },
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Billy Joe",
                                                                LastName = "Shaver",
                                                                HomePhone = "777-777-7777",
                                                                WorkPhone = "888-888-8888",
                                                                MobilePhone = "999-999-9999",
                                                                Email = "FiveAndDimer@Corsicana.com"
                                                            },
                                                    };

        private readonly IEnumerable<MethodInfo> _methods =
            typeof(FakeBackend).GetMethods().Where(x => x.Name == "Handle");

        #region IBackend Members

        public void Send<TResponse>(IQuery<TResponse> query, Action<TResponse> reply)
        {
            Invoke(query, query, reply);
        }

        public void Send(ICommand command)
        {
            Invoke(command, command);
        }

        #endregion

        private void Invoke(object request, params object[] args)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                Thread.Sleep(1000);

                Type requestType = request.GetType();
                MethodInfo handler =
                    _methods.Where(
                        x =>
                        requestType.IsAssignableFrom(
                            x.GetParameters().First().ParameterType)).First();

                handler.Invoke(this, args);
            });
        }

        public void Handle(SearchContacts search, Action<IEnumerable<SearchResult>> reply)
        {
            reply(
                from person in people
                where person.FirstName.ToLower().Contains(search.SearchText.ToLower()) || person.LastName.ToLower().Contains(search.SearchText.ToLower())
                orderby person.FirstName
                select new SearchResult
                {
                    Id = person.Id,
                    PersonName = String.Format("{0} {1}",person.FirstName, person.LastName) 
                });
        }


        public void Handle(AddPersonToContacts addPerson)
        {
            var person = new PersonContactInfo
            {
                Id = Guid.NewGuid(),
                FirstName = addPerson.FirstName,
                LastName = addPerson.LastName,
                HomePhone =  addPerson.HomePhone,
                WorkPhone = addPerson.WorkPhone,
                MobilePhone = addPerson.MobilePhone,
                Email = addPerson.EmailAddress
            };

            this.people.Add(person);
        }

        public void Handle(OpenPerson getPerson, Action<PersonContactInfo> reply)
        {
            reply(
                (from person in this.people
                 where person.Id == getPerson.Id
                 select person).FirstOrDefault()
                );
        }

        //public void Handle(AddGameToLibrary addGame)
        //{
        //    var game = new GameDTO
        //    {
        //        Id = Guid.NewGuid(),
        //        Title = addGame.Title,
        //        Notes = addGame.Notes,
        //        Rating = addGame.Rating,
        //        AddedOn = DateTime.Now,
        //    };

        //    _games.Add(game);
        //}

        //public void Handle(CheckGameIn checkIn)
        //{
        //    GameDTO game = _games.FirstOrDefault(x => x.Id == checkIn.Id);
        //    if (game != null)
        //        game.Borrower = null;
        //}

        //public void Handle(CheckGameOut checkOut)
        //{
        //    GameDTO game = _games.FirstOrDefault(x => x.Id == checkOut.Id);
        //    if (game != null)
        //        game.Borrower = checkOut.Borrower;
        //}
    }
}
