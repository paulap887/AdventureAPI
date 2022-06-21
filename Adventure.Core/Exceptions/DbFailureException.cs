using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Core.Exceptions
{
    /// <summary>
    /// Custom Exceptions 
    /// </summary>

    [Serializable]
    public class DbFailureException : Exception
    {
        public DbFailureException()
        {
        }

        public DbFailureException(string name)
            : base($"{name}")
        {
        }
    }
}
