using Adventure.Core.Enum;
using Adventure.Core.Models;
using Adventure.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Services.Mocks
{
    /// <summary>
    /// UserAdventureMockService
    /// </summary>
    public class UserAdventureMockService : AdventureMockService, IUserAdventureService
    {
        internal readonly List<Core.Models.UserAdventure> _userAdventures;

        public UserAdventureMockService()
        {
            _userAdventures = new List<UserAdventure>()
            {
                new UserAdventure()
                {
                    Id = 1,
                    AdventureId = 1,
                    UserId = 1001,
                    UserAdventureQuestions = new List<UserAdventureQuestion>()
                    {
                        new UserAdventureQuestion()
                        {
                            QuestionId = 1,
                            ChoiceId = 1
                        },
                        new UserAdventureQuestion()
                        {
                            QuestionId = 2,
                            ChoiceId = 2
                        }
                    }
                }
            };
        }
        public async Task<UserAdventure> CreateorUpdateUserAdventureQuestionAsync(UserAdventureQuestion request)
        {
            try
            {
                var _userAdventure = _userAdventures.FirstOrDefault(a => a.Id == request.UserAdventureId);

                if (_userAdventure == null)
                    throw new ArgumentException("Invalid User Adventure");


                var _userAdventureQuestion = _userAdventure.UserAdventureQuestions.FirstOrDefault(a => a.UserAdventureId
                                            == request.UserAdventureId && a.QuestionId == request.QuestionId);

                if (_userAdventureQuestion != null)
                {
                    var index = _userAdventure.UserAdventureQuestions.IndexOf(_userAdventureQuestion);
                    _userAdventureQuestion.ChoiceId = request.ChoiceId;
                    _userAdventureQuestion.UpdatedDate = DateTime.UtcNow;
                    if (index >= 0)
                        _userAdventure.UserAdventureQuestions[index] = request;
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
                    _userAdventure.UserAdventureQuestions.Add(_userAdventureQuestion);
                }
                 

                return _userAdventure;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }

        public async Task<UserAdventure> CreateUserAdventureAsync(UserAdventure request)
        {
            try
            {
                _userAdventures.Add(request);
                return request;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }

        public async Task<Question> GetNextQuestionAsync(int adventureId, int currentQuestionId, int choiceId)
        {
            Core.Models.Adventure thisAdventure = _adventures.FirstOrDefault(a => a.Id == adventureId);

            if (thisAdventure != null)
            {
                BaseEnums.ChoiceEnum chosedChoiceEnum = (BaseEnums.ChoiceEnum)choiceId;

                var questions = thisAdventure.Questions.OrderBy(a => a.DisplayOrder);

                var nextQuestion =  thisAdventure.Questions.FirstOrDefault(a => a.ParentChoice == chosedChoiceEnum && a.ParentQuestionId == currentQuestionId);

                return nextQuestion;
            }
            return null;
        }

        public async Task<bool> CheckForEndReachedOrNot(Question thisQuestion, int adventureId, int userAdventureId)
        {
            Core.Models.Adventure thisAdventure = _adventures.FirstOrDefault(a => a.Id == adventureId);

            if (thisAdventure != null)
            {
                var lastQuestion = thisAdventure.Questions.FirstOrDefault(a => (a.ParentChoice == BaseEnums.ChoiceEnum.Yes || a.ParentChoice == BaseEnums.ChoiceEnum.No)
                                         && a.ParentQuestionId == thisQuestion.Id);

                if (lastQuestion == null)
                {
                    // User Adventure 
                    Core.Models.UserAdventure userAdventure = _userAdventures.FirstOrDefault(a=>a.Id == userAdventureId);

                    userAdventure.UserAdventureQuestions.Add(new UserAdventureQuestion()
                    {
                        ChoiceId = 0,
                        QuestionId = thisQuestion.Id,
                        CreatedDate = DateTime.UtcNow,
                        UserAdventureId = userAdventure.Id
                    }); 
                }
            }
            return true;
        }

        public async Task<Question> GetRootQuestionAsync(int adventureId)
        {
            Core.Models.Adventure thisAdventure = _adventures.FirstOrDefault(a => a.Id == adventureId);

            if (thisAdventure != null)
            {
                var questions = thisAdventure.Questions.OrderBy(a => a.DisplayOrder);

                return thisAdventure.Questions.FirstOrDefault(a => a.IsRootQuestion == true);
            }
            return null;
        }

        public async Task<IEnumerable<UserAdventure>> GetUserAdventuresAsync(int userId)
        {
            try
            {
                return _userAdventures.Where(a => a.UserId == userId).AsEnumerable();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }

        public async Task<UserAdventure> GetUserAdventuresByIdAsync(int id)
        {
            try
            {
                return _userAdventures.FirstOrDefault(a => a.Id == id);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }
    }
}
