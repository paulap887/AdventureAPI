using Adventure.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Core.Models
{
    /// <summary>
    /// UserAdventure
    /// </summary>
    public class UserAdventure : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int AdventureId { get; set; } 
        public Core.Models.Adventure Adventure { get; set; } 
        public List<UserAdventureQuestion> UserAdventureQuestions { get; set; } 
    }
}
