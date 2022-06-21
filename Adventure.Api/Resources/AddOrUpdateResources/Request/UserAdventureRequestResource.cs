using Adventure.Api.Validator;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Adventure.Api.Resources.UserAdventure
{
    /// <summary>
    /// UserAdventureRequestResource
    /// </summary>
    public class UserAdventureRequestResource
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number (UserId)")]
        public int UserId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number (AdventureId)")]
        public int AdventureId  { get; set; } 
    }
}
