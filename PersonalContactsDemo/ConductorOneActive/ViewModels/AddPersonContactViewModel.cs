using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.ComponentModel;
using ConductorOneActive.Framework;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using ConductorOneActive.Framework.Results;

namespace ConductorOneActive.ViewModels
{
    public class AddPersonContactViewModel:Screen, IDataErrorInfo
    {

        private readonly IValidator validator;
        private string firstName;
        private string lastName;
        private bool wasSaved;


        public AddPersonContactViewModel(IValidator validator)
        {
            this.validator = validator;
        }

        [Required]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => CanAddPersonContact);
            }
        }

        [Required]
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => CanAddPersonContact);
            }
        }

        public bool CanAddPersonContact
        {
            get { return string.IsNullOrEmpty(Error); }
        }

        #region IDataErrorInfo Members

        public string this[string columnName]
        {
            get { return string.Join(Environment.NewLine, validator.Validate(this, columnName).Select(x => x.Message)); }
        }

        public string Error
        {
            get { return string.Join(Environment.NewLine, validator.Validate(this).Select(x => x.Message)); }
        }

        #endregion

        public IEnumerable<IResult> AddPersonContact()
        {
            //CommandResult add = new AddGameToLibrary
            //                        {
            //                            Title = Title,
            //                            Notes = Notes,
            //                            Rating = Rating
            //                        }.AsResult();

            wasSaved = true;

            //yield return add;
            yield return Show.Child<SearchScreenViewModel>().In<ShellViewModel>();
        }



        public override void CanClose(Action<bool> callback)
        {
            base.CanClose(result =>
                              {
                                  if (!result) callback(false);

                                  //Note: It is not a good practice to call MessageBox.Show from a non-View class.
                                  //Note: Consider implementing a MessageBoxService.
                                  result = wasSaved || MessageBox.Show(
                                      "Are you sure you want to cancel?  Changes will be lost.",
                                      "Unsaved Changes",
                                      MessageBoxButton.OKCancel
                                                            ) == MessageBoxResult.OK;

                                  callback(result);
                              });
        }

        public void CloseGame()
        {
            base.TryClose();
        }


    }
}
