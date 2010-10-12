using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.ComponentModel;
using PersonalContactsDemo.Framework;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using PersonalContactsDemo.Framework.Results;
using PersonalContactsDemo.Models;

namespace PersonalContactsDemo.ViewModels
{
    public class AddPersonContactViewModel:Screen, IDataErrorInfo
    {

        private readonly IValidator validator;
        private string firstName;
        private string lastName;
        private string homePhone;
        private string workPhone;
        private string mobilePhone;
        private string emailAddress;

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

        [Required]
        [RegularExpression(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$")]
        public string HomePhone
        {
            get { return homePhone; }
            set
            {
                homePhone = value;
                NotifyOfPropertyChange(() => HomePhone);
                NotifyOfPropertyChange(() => CanAddPersonContact);
            }
        }

        [RegularExpression(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$")]
        public string WorkPhone
        {
            get { return workPhone; }
            set
            {
                workPhone = value;
                NotifyOfPropertyChange(() => WorkPhone);
                NotifyOfPropertyChange(() => CanAddPersonContact);
            }
        }


        [RegularExpression(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$")]
        public string MobilePhone
        {
            get { return mobilePhone; }
            set
            {
                mobilePhone = value;
                NotifyOfPropertyChange(() => MobilePhone);
                NotifyOfPropertyChange(() => CanAddPersonContact);
            }
        }



        [RegularExpression(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$")]
        public string EmailAddress
        {
            get { return emailAddress; }
            set
            {
                emailAddress = value;
                NotifyOfPropertyChange(() => EmailAddress);
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
            CommandResult add = new AddPersonToContacts
                                    {
                                        FirstName = this.FirstName,
                                        LastName = this.LastName,
                                        HomePhone = this.HomePhone,
                                        WorkPhone = this.WorkPhone,
                                        MobilePhone = this.MobilePhone,
                                        EmailAddress = this.EmailAddress

                                    }.AsResult();

            wasSaved = true;

            yield return add;
            this.ClosePerson();
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

        public void ClosePerson()
        {
            base.TryClose();
        }


    }
}
