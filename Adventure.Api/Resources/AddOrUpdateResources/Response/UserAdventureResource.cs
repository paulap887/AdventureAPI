using System.Collections.Generic;

namespace Adventure.Api.Resources
{
    /// <summary>
    /// UserAdventureResource
    /// </summary>
    public class UserAdventureResource
    { 
        public int UserAdventureId { get; set; }    
        public int AdventureId { get; set; }
        public string AdventureDescription { get; set; } 
        public List<UserAdventureQuestionResource> Questions { get; set; }
    }
}
