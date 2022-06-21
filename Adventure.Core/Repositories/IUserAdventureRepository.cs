using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Core.Repositories
{
    /// <summary>
    /// IUserAdventureRepository
    /// </summary>
    public interface IUserAdventureRepository : IRepository<Adventure.Core.Models.UserAdventure>
    {
        Task<List<Core.Models.UserAdventure>> GetByUserIdAsync(int userId);

        Task<Core.Models.UserAdventure> GetByIdAsync(int id);
    }
}
