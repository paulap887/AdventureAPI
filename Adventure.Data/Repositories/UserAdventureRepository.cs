using Adventure.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Data.Repositories
{
    /// <summary>
    /// UserAdventureRepository
    /// </summary>
    public class UserAdventureRepository : Repository<Adventure.Core.Models.UserAdventure>, IUserAdventureRepository
    {
        public UserAdventureRepository(AdventureDbContext context)
                    : base(context)
        { }
        private AdventureDbContext context
        {
            get { return Context as AdventureDbContext; }
        }

        public async Task<List<Core.Models.UserAdventure>> GetByUserIdAsync(int userId)
        {
            return AdventureDbContext.UserAdventure
                       .Where(a => a.UserId == userId)
                        .Include(a => a.UserAdventureQuestions)
                        .ThenInclude(a=>a.Question)
                        .Include(a=>a.Adventure) 
                       .ToList();
        }

        public async Task<Core.Models.UserAdventure> GetByIdAsync(int id)  
        {
            return await AdventureDbContext.UserAdventure
                       .Where(a => a.Id == id)
                        .Include(a => a.UserAdventureQuestions)
                        .ThenInclude(a => a.Question)
                        .Include(a => a.Adventure)
                       .FirstOrDefaultAsync();
        }

        private AdventureDbContext AdventureDbContext
        {
            get { return Context as AdventureDbContext; }

        }

    }
}
