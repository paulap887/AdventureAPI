using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Core.Models.Common
{
    /// <summary>
    /// BaseModel
    /// </summary>
    public class BaseModel
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
