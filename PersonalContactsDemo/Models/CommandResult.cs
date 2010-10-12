using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace PersonalContactsDemo.Models
{
    public class CommandResult : IResult
    {
        private readonly ICommand _command;

        public CommandResult(ICommand command)
        {
            _command = command;
        }


        public IBackend Bus { get; set; }

        #region IResult Members

        public void Execute(ActionExecutionContext context)
        {
            Bus.Send(_command);
            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        #endregion
    }
 }
