using Adventure.Api.Controllers;
using Adventure.Api.Mapping;
using Adventure.Api.Resources;
using Adventure.Core.Services;
using Adventure.Services.Mocks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace Adventure.Test
{
    /// <summary>
    /// Adventure Controller Test
    /// </summary>
    public class AdventureControllerTest
    {
        private readonly AdventureController _controller;
        private readonly IAdventureService _service;
        private readonly IMapper _mapper;
        public AdventureControllerTest()
        {
            _service = new AdventureMockService();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile()); mc.AddProfile(new UserAdventureMappingProfile());
                });
                _mapper = mappingConfig.CreateMapper();
            }
            _controller = new AdventureController(_service, _mapper);
        }

        [Fact]
        public async Task GetAdventure_WhenCalled_ReturnsOkResult()
        {
            // Act 
            var okResult = await _controller.GetAdventureById(1);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetChoices_WhenCalled_ReturnsOkResult()
        {
            // Act 
            var okResult = _controller.GetAllAvailableChoices();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task PostAdventure_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var requestResource = new AdventureResource()
            {
                AdventureTitle = "New Adventure",
            };
            // Act
            ActionResult<AdventureQuestionResource> createdResponse = await _controller.CreateNewAdventure(requestResource);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse.Result);
        }

        [Fact]
        public async Task PostQuestion_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var requestResource = new AdventureQuestionUpdateResource()
            {
                AdventureId = 1,
                Description = "Question 3",
                DisplayOrder = 3,
                IsRootQuestion = false,
                ParentChoiceId = 2,
                ParentQuestionId = 2,
                QuestionId = 6
            };
            // Act
            ActionResult<AdventureQuestionResource> createdResponse = await _controller.CreateUpdateQuestion(requestResource);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse.Result);
        }
    }
}
