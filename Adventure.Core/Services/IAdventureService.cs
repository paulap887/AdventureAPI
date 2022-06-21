using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Adventure.Core.Models;

namespace Adventure.Core.Services
{
    /// <summary>
    /// IAdventureService
    /// </summary>
    public interface IAdventureService 
    {
        Task<Core.Models.Adventure> GetAdventureByIdAsync(int adventureId); 
        Task<Core.Models.Adventure> CreateAdventureAsync(Core.Models.Adventure request);
        Task<Core.Models.Question> CreateOrUpdateQuestionAsync(Core.Models.Question request); 
    }
}
