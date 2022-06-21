 

namespace Adventure.Api.Resources
{
    /// <summary>
    /// UserAdventureNextQuestionResource
    /// </summary>
    public class UserAdventureNextQuestionResource
    {
        public int UserAdventureId { get; set; } 
        public UserAdventureQuestionResource NextQuestion { get; set; } 
        public bool isEndReached { get; set; }
    }
}
