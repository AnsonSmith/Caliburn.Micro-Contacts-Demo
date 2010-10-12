using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;

namespace ConductorOneActive.Models
{
    public class FakeBackend : IBackend
    {
        //The 'Notes' are taken from the Game Spot reviews for each of these games.
        private readonly List<PersonContactInfo> people = new List<PersonContactInfo>
                                                    {
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Anson",
                                                                LastName = "Smith"
                                                            },
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Tony",
                                                                LastName = "Vann"
                                                            },
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Mark",
                                                                LastName = "Reaux"
                                                            },
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Stephanie",
                                                                LastName = "Lee"
                                                            },
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Sravanthi",
                                                                LastName = "Munagala"
                                                            },
                                                        new PersonContactInfo
                                                            {
                                                                Id = Guid.NewGuid(),
                                                                FirstName = "Suphannee",
                                                                LastName = "Sae Chai"
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

        //public void Handle(GetGame getGame, Action<GameDTO> reply)
        //{
        //    reply(
        //        (from game in _games
        //         where game.Id == getGame.Id
        //         select game).FirstOrDefault()
        //        );
        //}

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
