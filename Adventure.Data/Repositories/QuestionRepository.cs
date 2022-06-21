using Adventure.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventure.Data.Repositories
{
    /// <summary>
    /// QuestionRepository
    /// </summary>
    internal class QuestionRepository : Repository<Adventure.Core.Models.Question>, IQuestionRepository
    {
        public QuestionRepository(AdventureDbContext context)
                    : base(context)
        { } 
        private AdventureDbContext context
        {
            get { return Context as AdventureDbContext; }
        }
    }
}
