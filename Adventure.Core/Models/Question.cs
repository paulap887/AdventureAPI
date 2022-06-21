using Adventure.Core.Enum;
using Adventure.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Core.Models
{
    /// <summary>
    /// Question
    /// </summary>
    public class Question : BaseModel
    {
        public int Id { get; set; }
        public int AdventureId { get; set; } 
        public string QuestionDescription { get; set; }  
        public int? ParentQuestionId { get; set; }
        public Question ParentQuestion { get; set; } 
        public BaseEnums.ChoiceEnum ParentChoice { get; set; }
        public bool IsRootQuestion { get; set; }  
        public int DisplayOrder { get; set; }
        public Adventure Adventure { get; set; } 
        public List<Question> Questions { get; set; } 
        public List<UserAdventureQuestion> UserAdventureQuestions { get; set; } 
    }
}
