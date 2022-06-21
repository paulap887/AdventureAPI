namespace Adventure.Api.Resources
{
    /// <summary>
    /// UserAdventureQuestionResource
    /// </summary>
    public class UserAdventureQuestionResource 
    { 
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public bool IsRootQuestion { get; set; } 
        public int DisplayOrder { get; set; }
        public int ParentQuestionId { get; set; }
        public int ParentChoiceId { get; set; }
        public int SelectedChoiceId { get; set; } 
    }
}
