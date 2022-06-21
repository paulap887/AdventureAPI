using Adventure.Core;
using Adventure.Core.Repositories;
using Adventure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Data
{
    /// <summary>
    /// UnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdventureDbContext _context;
        private AdventureRepository adventureRepository;
        private QuestionRepository questionRepository; 
        private UserAdventureRepository userAdventureRepository;  
        private UserAdventureQuestionRepository userAdventureQuestionRepository;  

        public UnitOfWork()
        {
        }
        public UnitOfWork(AdventureDbContext context)
        {
            this._context = context;
        }

        public IAdventureRepository Adventure => adventureRepository = adventureRepository ?? new AdventureRepository(_context); 
        public IQuestionRepository Question => questionRepository = questionRepository ?? new QuestionRepository(_context); 
        public IUserAdventureRepository UserAdventure => userAdventureRepository = userAdventureRepository ?? new UserAdventureRepository(_context);
        public IUserAdventureQuestionRepository UserAdventureQuestion => userAdventureQuestionRepository = userAdventureQuestionRepository ?? new UserAdventureQuestionRepository(_context);
         
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
