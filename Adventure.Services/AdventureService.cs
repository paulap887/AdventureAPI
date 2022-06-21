using Adventure.Core;
using Adventure.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Services
{
    /// <summary>
    /// AdventureService
    /// </summary>
    public class AdventureService : BaseService, IAdventureService
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AdventureService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Adventure By Id Async
        /// </summary>
        /// <param name="adventureId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Core.Models.Adventure> GetAdventureByIdAsync(int adventureId)
        {
            var advFromDb = await _unitOfWork.Adventure.GetAdventureByIdAsync(adventureId);
            if (advFromDb != null)
                advFromDb.Questions.OrderBy(a => a.DisplayOrder);
            else
                throw new ArgumentException("Invalid Adevnture");

            return advFromDb;
        }

        /// <summary>
        /// Create Adventure
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Core.Models.Adventure> CreateAdventureAsync(Core.Models.Adventure request)
        {
            try
            {
                await _unitOfWork.Adventure.AddAsync(request);
                await _unitOfWork.CommitAsync();
                return request;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// Create Or Update Question
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Core.Models.Question> CreateOrUpdateQuestionAsync(Core.Models.Question request) 
        {
            try
            {
                var adventureDataFromDb = await _unitOfWork.Adventure.GetAdventureByIdAsync(request.AdventureId).ConfigureAwait(false);
                if (adventureDataFromDb == null)
                    throw new KeyNotFoundException("Data not found");

                // Validations  
                var rootQues = adventureDataFromDb.Questions.FirstOrDefault(a => a.IsRootQuestion);
                if (rootQues != null)
                    if (request.IsRootQuestion == true && request.Id != rootQues.Id)
                        throw new ArgumentException("Multiple Roots specified for an Adventure Questions");

                if (rootQues == null && request.IsRootQuestion == false)
                    throw new ArgumentException("Please add root Question fo this Adventure");

                if (!request.IsRootQuestion)
                {
                    if (request.ParentChoice == 0)
                        throw new ArgumentException("Invalid Choice");
                    if (request.ParentQuestionId == 0)
                        throw new ArgumentException("Invalid Parent Question");
                    if (adventureDataFromDb.Questions.Count() > 0)
                    {
                        if (!adventureDataFromDb.Questions.Select(a => a.Id).ToList().Contains(Convert.ToInt32(request.ParentQuestionId)))
                            throw new ArgumentException("Invalid Parent Question");
                    }
                }

                 
                if (request.Id == 0)
                    await _unitOfWork.Question.AddAsync(request);
                else
                    _unitOfWork.Question.Update(request);
                await _unitOfWork.CommitAsync();

                return request;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }  
    }
}
