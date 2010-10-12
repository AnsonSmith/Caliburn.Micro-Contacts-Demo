using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConductorOneActive.Framework
{
    public interface IBusyService
    {
        void MarkAsBusy(object sourceViewModel, object busyViewModel);
        void MarkAsNotBusy(object sourceViewModel);
    }
}
