using Adventure.Api.Validator;
using System.ComponentModel.DataAnnotations;

namespace Adventure.Api.Resources
{
    /// <summary>
    /// AdventureQuestionUpdateResource
    /// </summary>
    public class AdventureQuestionUpdateResource  
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number (AdventureId)")]
        public int AdventureId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number (QuestionId)")]
        public int QuestionId { get; set; } 

        [MaxLength(100)]
        [RequiredNotEmpty]
        public string Description { get; set; }

        [Required]
        public bool IsRootQuestion { get; set; } 

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid Number (QuestionId)")]
        public int DisplayOrder { get; set; } 

        public int ParentQuestionId { get; set; } 

        public int ParentChoiceId { get; set; } 
    }
}
