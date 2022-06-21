using Adventure.Core;
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
    /// AdventureMockService
    /// </summary>
    public class AdventureMockService : BaseService, IAdventureService
    {

        internal readonly List<Core.Models.Adventure> _adventures;

        public AdventureMockService()
        {
            _adventures = new List<Core.Models.Adventure>()
            {
                new Core.Models.Adventure()
                {
                     Id = 1,
                    Description = "Adventure 1",
                    Questions =  new List<Question>()
                    {
                        new Question()
                        {
                            Id = 1,
                            QuestionDescription = "Root Question 1",
                            IsRootQuestion = true,
                            ParentChoice  = BaseEnums.ChoiceEnum.Yes,
                            ParentQuestionId = null,
                            DisplayOrder = 1,
                        },
                          new Question()
                        {
                              Id = 2,
                            QuestionDescription = "Question 2",
                            IsRootQuestion = false,
                            ParentChoice  = BaseEnums.ChoiceEnum.No,
                            ParentQuestionId = 1,
                            DisplayOrder = 1,
                        }
                    },
                    CreatedDate = DateTime.Now,
                },
                  new Core.Models.Adventure()
                {
                      Id = 2,
                    Description = "Adventure 2",
                    Questions =  new List<Question>()
                    {
                        new Question()
                        {
                            Id = 3,
                            QuestionDescription = "Root Question",
                            IsRootQuestion = true,
                            ParentChoice  = BaseEnums.ChoiceEnum.Yes,
                            ParentQuestionId = null,
                            DisplayOrder = 1,
                        },
                          new Question()
                        {
                            Id = 4,
                            QuestionDescription = "Question 2",
                            IsRootQuestion = false,
                            ParentChoice  = BaseEnums.ChoiceEnum.Yes,
                            ParentQuestionId = 3,
                            DisplayOrder = 2,
                        }
                    },
                    CreatedDate = DateTime.Now,
                }
            };
        }

        public async Task<Core.Models.Adventure> CreateAdventureAsync(Core.Models.Adventure request)
        {
            request.Id = _adventures.Count() + 1;
            request.Questions = new List<Question>();
            _adventures.Add(request);
            return request;
        }

        public async Task<Core.Models.Question> CreateOrUpdateQuestionAsync(Core.Models.Question request)
        {
            try
            {
                var adventureDataFromDb = _adventures.FirstOrDefault(a => a.Id == request.AdventureId);
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
                    adventureDataFromDb.Questions.Add(request);
                else
                {
                    var index = adventureDataFromDb.Questions.IndexOf(request);
                    if (index >= 0)
                        adventureDataFromDb.Questions[index] = request;
                }

                return request;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return null;
            }
        }

        public async  Task<Core.Models.Adventure> GetAdventureByIdAsync(int adventureId)
        {
            var advFromDb = _adventures.FirstOrDefault(a => a.Id == adventureId);
            if (advFromDb != null)
                advFromDb.Questions.OrderBy(a => a.DisplayOrder);
            else
                throw new ArgumentException("Invalid Adevnture");

            return advFromDb;
        }
    } 
}
