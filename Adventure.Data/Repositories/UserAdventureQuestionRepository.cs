using Adventure.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Data.Repositories
{
    /// <summary>
    /// UserAdventureQuestionRepository
    /// </summary>
    public class UserAdventureQuestionRepository : Repository<Adventure.Core.Models.UserAdventureQuestion>, IUserAdventureQuestionRepository
    {
        public UserAdventureQuestionRepository(AdventureDbContext context) 
                    : base(context)
        { }
        private AdventureDbContext context
        {
            get { return Context as AdventureDbContext; }
        }
    }
}
