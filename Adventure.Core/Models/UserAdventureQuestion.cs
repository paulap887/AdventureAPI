using Adventure.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Core.Models
{
    /// <summary>
    /// UserAdventureQuestion
    /// </summary>
    public class UserAdventureQuestion : BaseModel
    {
        public int Id { get; set; } 
        public int UserAdventureId { get; set; } 
        public int QuestionId { get; set; }
        public int ChoiceId { get; set; } 
        public Core.Models.UserAdventure UserAdventure { get; set; } 
        public Core.Models.Question Question { get; set; } 

    }
}
