namespace PersonalContactsDemo.ViewModels
{
    using Caliburn.Micro;
    using PersonalContactsDemo.Framework.Results;
    using System.Collections.Generic;

    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        private readonly SearchScreenViewModel firstScreen;
        

        public ShellViewModel(SearchScreenViewModel screen)
        {
            firstScreen = screen;
        
        }

        public IResult AddPersonContact()
        {
            return Show.Child<AddPersonContactViewModel>()
                .In<ShellViewModel>();
        }

        public IEnumerable<IResult> Back()
        {
            yield return Show.Child<SearchScreenViewModel>().In<ShellViewModel>();

        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            ActivateItem(firstScreen);
        
        }

    }
}
