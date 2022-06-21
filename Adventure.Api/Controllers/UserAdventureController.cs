using Adventure.Api.Resources;
using Adventure.Api.Resources.UserAdventure;
using Adventure.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adventure.Api.Controllers
{
    /// <summary>
    ///  User Adventure
    /// </summary>
    [Route("api/user-adventure")]
    public class UserAdventureController : Controller
    {
        private readonly IUserAdventureService _userAdventureService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="covidService"></param>
        /// <param name="mapper"></param>
        public UserAdventureController(IUserAdventureService userAdventureService, IMapper mapper)
        {
            this._mapper = mapper;
            this._userAdventureService = userAdventureService;
        }


        /// <summary>
        ///  First API to call for creating new Adventure capturing for a User
        ///  Response -  Root (First) Question in case of New User Adventure Creation 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult<UserAdventureNextQuestionResource>> CreateNewUserAdventure([FromBody] UserAdventureRequestResource request)
        {

            if (ModelState.IsValid)
            {
                var adventureDataToCreate = _mapper.Map<UserAdventureRequestResource, Adventure.Core.Models.UserAdventure>(request);

                var data = await _userAdventureService.CreateUserAdventureAsync(adventureDataToCreate).ConfigureAwait(false);

                var rootQuestion = await _userAdventureService.GetRootQuestionAsync(request.AdventureId);


                UserAdventureNextQuestionResource nextQuestionResponse = new UserAdventureNextQuestionResource()
                {
                    UserAdventureId = data.Id
                };

                nextQuestionResponse.isEndReached = await _userAdventureService.CheckForEndReachedOrNot(rootQuestion, data.AdventureId, data.Id);


                var questionResponse = _mapper.Map<Adventure.Core.Models.Question, UserAdventureQuestionResource>(rootQuestion);

                nextQuestionResponse.NextQuestion = questionResponse;

                return Ok(nextQuestionResponse);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        ///  After the Creation this API will be called to capture the choices for rest of Questions 
        ///  inside Adventure
        ///  Response -  Next Consecutive  Question, If reaches end then  "isEndReached" will be set to true 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("")]
        public async Task<ActionResult<UserAdventureNextQuestionResource>> CaptureUserChoices([FromBody] UserAdventureQuestionCaptureResource request)
        {
            UserAdventureNextQuestionResource nextQuestionResponse = new UserAdventureNextQuestionResource()
            {
                UserAdventureId = request.UserAdventureId
            };

            if (ModelState.IsValid)
            {
                var adventureDataToCreate = _mapper.Map<UserAdventureQuestionCaptureResource, Adventure.Core.Models.UserAdventureQuestion>(request);

                var responseFromDB = await _userAdventureService.CreateorUpdateUserAdventureQuestionAsync(adventureDataToCreate).ConfigureAwait(false);


                var nextQuestion = await _userAdventureService.GetNextQuestionAsync(responseFromDB.AdventureId, request.QuestionId, request.ChoiceId);
                 
                nextQuestionResponse.isEndReached = await _userAdventureService.CheckForEndReachedOrNot(nextQuestion, responseFromDB.AdventureId, responseFromDB.Id);


                var questionResponse = _mapper.Map<Adventure.Core.Models.Question, UserAdventureQuestionResource>(nextQuestion);

                nextQuestionResponse.NextQuestion = questionResponse;

                return Ok(nextQuestionResponse);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        ///  Get All Adventure completed by particular user 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserAdventureResource>>> GetUserAdventures(int userId)
        {
            var responseFromDb = await _userAdventureService.GetUserAdventuresAsync(userId).ConfigureAwait(false);

            var response = _mapper.Map<IEnumerable<Core.Models.UserAdventure>, IEnumerable<UserAdventureResource>>(responseFromDb);

            return Ok(response);
        }

        /// <summary>
        ///  Get User Adenture details Completed by UserAdventureId 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAdventureResource>> GetUserAdventureById(int id)
        {
            var responseFromDb = await _userAdventureService.GetUserAdventuresByIdAsync(id).ConfigureAwait(false);

            var response = _mapper.Map<Core.Models.UserAdventure, UserAdventureResource>(responseFromDb);

            return Ok(response);
        }

    }
}
