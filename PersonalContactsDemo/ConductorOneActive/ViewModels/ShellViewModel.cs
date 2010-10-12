namespace ConductorOneActive.ViewModels
{
    using Caliburn.Micro;

    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        private readonly SearchScreenViewModel firstScreen;
        

        public ShellViewModel(SearchScreenViewModel screen)
        {
            firstScreen = screen;
        
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            ActivateItem(firstScreen);
        
        }

    }
}
