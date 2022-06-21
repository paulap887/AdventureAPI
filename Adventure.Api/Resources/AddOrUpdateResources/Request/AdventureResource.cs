using Adventure.Api.Validator;
using System.ComponentModel.DataAnnotations;

namespace Adventure.Api.Resources
{
    /// <summary>
    /// AdventureResource
    /// </summary>
    public class AdventureResource
    {
        [MaxLength(100)]
        [RequiredNotEmpty]
        public string AdventureTitle { get; set; } 
     
    }
}
