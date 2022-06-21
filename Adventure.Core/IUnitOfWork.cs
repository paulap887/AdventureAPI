using Adventure.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Core
{
    /// <summary>
    /// IUnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        IAdventureRepository Adventure { get; }   
        IQuestionRepository Question { get; }      
        IUserAdventureRepository UserAdventure { get; } 
        IUserAdventureQuestionRepository UserAdventureQuestion { get; }
        Task<int> CommitAsync();
    }
}
