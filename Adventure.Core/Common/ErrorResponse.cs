using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Adventure.Core.Common
{
    /// <summary>
    /// ErrorResponse
    /// </summary>
    public class ErrorResponse
    {
        public string Error { get; set; }
        public HttpStatusCode Code { get; set; }
    }
}
