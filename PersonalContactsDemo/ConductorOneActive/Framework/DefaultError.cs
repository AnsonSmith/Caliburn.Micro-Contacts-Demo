﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConductorOneActive.Framework
{
    public class DefaultError : IError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultError"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="message">The message.</param>
        public DefaultError(object instance, string propertyName, string message)
        {
            Instance = instance;
            Key = propertyName;
            Message = message;
        }

        /// <summary>
        /// Gets the invalid instance.
        /// </summary>
        /// <value>The instance.</value>
        public object Instance { get; private set; }

        #region IError Members

        /// <summary>
        /// Gets the name of the invalid property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; private set; }

        #endregion    
    }
}
