using Adventure.Api.Resources;
using Adventure.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Adventure.Core.Enum.BaseEnums;

namespace Adventure.Api.Controllers
{
    /// <summary>
    /// Adventure Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdventureController : Controller
    {
        private readonly IAdventureService _adventureService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="adventureService"></param>
        /// <param name="mapper"></param>
        public AdventureController(IAdventureService adventureService, IMapper mapper)
        {
            this._mapper = mapper;
            this._adventureService = adventureService;
        }

        /// <summary>
        ///  Get Adventure By Id 
        /// </summary>
        /// <param name="adventureId">adventureId</param>
        /// <returns></returns>
        [HttpGet("{adventureId}")]
        public async Task<ActionResult<AdventureQuestionResource>> GetAdventureById(int adventureId)
        {
            AdventureQuestionResource response = await GetAdventureDetail(adventureId).ConfigureAwait(false);

            return Ok(response);
        }

        /// <summary>
        ///  Get All Available Choices 
        /// </summary>
        /// <returns></returns>
        [HttpGet("available-choices")]
        public ActionResult<List<ChoiceResource>> GetAllAvailableChoices()
        {
            if (ModelState.IsValid)
            {
                List<ChoiceResource> _choices = new List<ChoiceResource>();

                foreach (int i in Enum.GetValues(typeof(ChoiceEnum)))
                {
                    ChoiceResource resource = new ChoiceResource()
                    {
                        ChoiceId = i,
                        ChoiceDescription = Enum.GetName(typeof(ChoiceEnum), i)
                    };
                    _choices.Add(resource);
                }

                return Ok(_choices);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        ///  Create New Adventure - First API to call While creating new Adventure 
        ///  Response - Entire Adventure Model including questions 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult<AdventureQuestionResource>> CreateNewAdventure([FromBody] AdventureResource request)
        {
            if (ModelState.IsValid)
            {
                var adventureDataToCreate = _mapper.Map<AdventureResource, Adventure.Core.Models.Adventure>(request);

                var data = await _adventureService.CreateAdventureAsync(adventureDataToCreate).ConfigureAwait(false);

                var response = await GetAdventureDetail(data.Id).ConfigureAwait(false);

                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        ///  Update Adventure / Questions  - After posting Adventure API, call this PUT API for updating questions for Adventure 
        ///  Response - Entire Adventure Model including questions 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("question")]
        public async Task<ActionResult<AdventureQuestionResource>> CreateUpdateQuestion([FromBody] AdventureQuestionUpdateResource request)
        {
            if (ModelState.IsValid)
            {
                var questionDataToCreate = _mapper.Map<AdventureQuestionUpdateResource, Adventure.Core.Models.Question>(request);

                var data = await _adventureService.CreateOrUpdateQuestionAsync(questionDataToCreate).ConfigureAwait(false);

                AdventureQuestionResource response = await GetAdventureDetail(request.AdventureId).ConfigureAwait(false);

                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        #region Private Methods 

        /// <summary>
        ///  Common method for getting details of adventure 
        /// </summary>
        /// <param name="adventureId"></param>
        /// <returns></returns>
        private async Task<AdventureQuestionResource> GetAdventureDetail(int adventureId)
        {
            var adventureData = await _adventureService.GetAdventureByIdAsync(adventureId).ConfigureAwait(false);

            var response = _mapper.Map<Core.Models.Adventure, AdventureQuestionResource>(adventureData);
            return response;
        }

        #endregion
    }
}
