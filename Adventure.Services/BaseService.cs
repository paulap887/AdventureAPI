using Adventure.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Services
{
    /// <summary>
    /// BaseService
    /// </summary>
    public class BaseService
    { 
        public static void HandleException(Exception ex)
        {
            if (ex is KeyNotFoundException)
                throw new KeyNotFoundException(ex.Message);
            if (ex is ArgumentException)
                throw new ArgumentException(ex.Message);
            else
                throw new DbFailureException(ex.Message);
        }

    }
}
