using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Core.Repositories
{
    /// <summary>
    /// IAdventureRepository
    /// </summary>
    public interface IAdventureRepository : IRepository<Adventure.Core.Models.Adventure>
    {
        Task<Adventure.Core.Models.Adventure> GetAdventureAsync();
        Task<Core.Models.Adventure> GetAdventureByIdAsync(int adeventureId);
    }
}
