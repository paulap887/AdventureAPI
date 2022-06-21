using Adventure.Core;
using Adventure.Core.Enum;
using Adventure.Core.Models;
using Adventure.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Services
{
    /// <summary>
    /// User Adventure Service
    /// </summary>
    public class UserAdventureService : BaseService, IUserAdventureService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserAdventureService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create User Adventure
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserAdventure> CreateUserAdventureAsync(UserAdventure request)
        {
            try
            {
                await _unitOfWork.UserAdventure.AddAsync(request);
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
        /// CreateorUpdate UserAdventure Question
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserAdventure> CreateorUpdateUserAdventureQuestionAsync(UserAdventureQuestion request)
        {
            try
            { 
                var _userAdventure = await _unitOfWork.UserAdventure.SingleOrDefaultAsync(a => a.Id == request.UserAdventureId);

                if (_userAdventure == null)
                    throw new ArgumentException("Invalid User Adventure");


                var _userAdventureQuestion = await _unitOfWork.UserAdventureQuestion.SingleOrDefaultAsync(a => a.UserAdventureId
                                             == request.UserAdventureId && a.QuestionId == request.QuestionId);

                if (_userAdventureQuestion != null)
                {
                    _userAdventureQuestion.ChoiceId = request.ChoiceId;
                    _userAdventureQuestion.UpdatedDate = DateTime.UtcNow;
                    _unitOfWork.UserAdventureQuestion.Update(_userAdventureQuestion);
                }
                else
                {
                    _userAdventureQuestion = new UserAdventureQuestion()
                    {
                        ChoiceId = request.ChoiceId,
                        QuestionId = request.QuestionId,
                        CreatedDate = DateTime.UtcNow,
                        UserAdventureId = request.UserAdventureId
                    }; 
                    await _unitOfWork.UserAdventureQuestion.AddAsync(_userAdventureQuestion);
                }  

                await _unitOfWork.CommitAsync();

                return _userAdventure;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        } 

        /// <summary>
        /// Get User Adventures
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserAdventure>> GetUserAdventuresAsync(int userId)
        {
            try
            {
                return await _unitOfWork.UserAdventure.GetByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }


        /// <summary>
        /// GetUserAdventures By Id Async
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserAdventure> GetUserAdventuresByIdAsync(int id) 
        {
            try
            {
                return await _unitOfWork.UserAdventure.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Next Question
        /// </summary>
        /// <param name="adventureId"></param>
        /// <param name="currentQuestionId"></param>
        /// <param name="choiceId"></param>
        /// <returns></returns>
        public async Task<Question> GetNextQuestionAsync(int adventureId, int currentQuestionId, int choiceId) 
        {
            Core.Models.Adventure thisAdventure = await _unitOfWork.Adventure.GetAdventureByIdAsync(adventureId);

            if(thisAdventure != null)
            {
                BaseEnums.ChoiceEnum chosedChoiceEnum = (BaseEnums.ChoiceEnum)choiceId;

                var questions = thisAdventure.Questions.OrderBy(a => a.DisplayOrder);

                return thisAdventure.Questions.FirstOrDefault(a => a.ParentChoice == chosedChoiceEnum && a.ParentQuestionId == currentQuestionId);
            }
            return null;
        }
        public async Task<bool> CheckForEndReachedOrNot(Question thisQuestion, int adventureId, int userAdventureId) 
        {
            Core.Models.Adventure thisAdventure = await _unitOfWork.Adventure.GetAdventureByIdAsync(adventureId);

            if (thisAdventure != null)
            {
                var lastQuestion  =   thisAdventure.Questions.FirstOrDefault(a => (a.ParentChoice == BaseEnums.ChoiceEnum.Yes || a.ParentChoice == BaseEnums.ChoiceEnum.No)
                                            && a.ParentQuestionId == thisQuestion.Id);

                if (lastQuestion == null)
                {
                    // User Adventure 
                    Core.Models.UserAdventure userAdventure = await _unitOfWork.UserAdventure.GetByIdAsync( userAdventureId);

                    userAdventure.UserAdventureQuestions.Add(new UserAdventureQuestion()
                    {
                        ChoiceId = 0,
                        QuestionId = thisQuestion.Id,
                        CreatedDate = DateTime.UtcNow,
                        UserAdventureId = userAdventure.Id
                    });

                    await _unitOfWork.CommitAsync();
                }
            }
            return true;
        }

        /// <summary>
        /// Get Root Question
        /// </summary>
        /// <param name="adventureId"></param>
        /// <returns></returns>
        public async Task<Question> GetRootQuestionAsync(int adventureId) 
        {
            Core.Models.Adventure thisAdventure = await _unitOfWork.Adventure.GetAdventureByIdAsync(adventureId);

            if (thisAdventure != null)
            { 
                var questions = thisAdventure.Questions.OrderBy(a => a.DisplayOrder);

                return thisAdventure.Questions.FirstOrDefault(a => a.IsRootQuestion == true);
            }
            return null;
        }
    }
}
