using System;
using System.Collections.Generic;
using System.Text;
using Adventure.Core.Models.Common;

namespace Adventure.Core.Models
{
    /// <summary>
    /// Adventure
    /// </summary>
    public class Adventure : BaseModel
    {
        public int Id { get; set; }     
        public string Description { get; set; } 
        public List<Question> Questions { get; set; }   
        public List<UserAdventure> UserAdventures { get; set; } 

    }
}
