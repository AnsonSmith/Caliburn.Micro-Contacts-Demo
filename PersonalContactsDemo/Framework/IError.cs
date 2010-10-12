using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalContactsDemo.Framework
{
    public interface IError
    {
        /// <summary>
        /// Gets the name of the invalid property.
        /// </summary>
        /// <value>The name of the property.</value>
        string Key { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>The message.</value>
        string Message { get; }
    }
}
