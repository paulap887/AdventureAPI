using Adventure.Api.Validator;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Adventure.Api.Resources
{
    /// <summary>
    /// AdventureQuestionResource
    /// </summary>
    public class AdventureQuestionResource : AdventureResource
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number (AdventureId)")]
        public int AdventureId { get; set; }

        [RequiredNotEmpty]
        public List<AdventureQuestionUpdateResource> Questions { get; set; }
    }
}
