using Adventure.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Core.Services
{
    /// <summary>
    /// IUserAdventureService
    /// </summary>
    public interface IUserAdventureService
    {
        Task<Core.Models.UserAdventure> CreateUserAdventureAsync(Core.Models.UserAdventure request);

        Task<IEnumerable<UserAdventure>> GetUserAdventuresAsync(int userId); 
        Task<UserAdventure> CreateorUpdateUserAdventureQuestionAsync(UserAdventureQuestion request); 
        Task<UserAdventure> GetUserAdventuresByIdAsync(int id);
        Task<Question> GetNextQuestionAsync(int adventureId, int currentQuestionId, int choiceId); 
        Task<Question> GetRootQuestionAsync(int adventureId);
        Task<bool> CheckForEndReachedOrNot(Question thisQuestion, int adventureId, int userAdventureId);
    }
}
