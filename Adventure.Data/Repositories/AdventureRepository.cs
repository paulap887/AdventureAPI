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
    /// AdventureRepository
    /// </summary>
    internal class AdventureRepository : Repository<Adventure.Core.Models.Adventure>, IAdventureRepository
    {
        public AdventureRepository(AdventureDbContext context) 
                    : base(context)
        { }
        private AdventureDbContext context
        {
            get { return Context as AdventureDbContext; }
        }

        public Task<Core.Models.Adventure> GetAdventureAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Core.Models.Adventure> GetAdventureByIdAsync(int adeventureId) 
        {
            return AdventureDbContext.Adventure
                      .Where(a => a.Id == adeventureId)
                        .Include(a => a.Questions).ThenInclude(a => a.ParentQuestion)
                      .FirstOrDefaultAsync();
        }

        private AdventureDbContext AdventureDbContext
        {
            get { return Context as AdventureDbContext; }

        }
    }
}
