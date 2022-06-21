using Adventure.Api.Controllers;
using Adventure.Api.Mapping;
using Adventure.Api.Resources;
using Adventure.Api.Resources.UserAdventure;
using Adventure.Core.Services;
using Adventure.Services.Mocks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace Adventure.Test
{
    /// <summary>
    /// User Adventure Controller Test
    /// </summary>
    public class UserAdventureControllerTest
    {
        private readonly UserAdventureController _controller;
        private readonly IUserAdventureService _service;
        private readonly IMapper _mapper;
        public UserAdventureControllerTest() 
        {
            _service = new UserAdventureMockService();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile()); mc.AddProfile(new UserAdventureMappingProfile());
                });
                _mapper = mappingConfig.CreateMapper();
            }
            _controller = new UserAdventureController(_service, _mapper);
        }

        [Fact]
        public async Task CreateUserAdventure_WhenCalled_ReturnsOkResult() 
        {
            // Arrange
            var requestResource = new UserAdventureRequestResource()
            {
                AdventureId = 3,
                UserId = 1001
            };
            // Act
            ActionResult<UserAdventureNextQuestionResource> createdResponse = await _controller.CreateNewUserAdventure(requestResource);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse.Result);
        } 

        [Fact]
        public async Task GetUserAdventures_WhenCalled_ReturnsOkResult() 
        {
            // Act 
            var okResult = await _controller.GetUserAdventures(1001);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public async Task GetAdventuresById_WhenCalled_ReturnsOkResult() 
        {
            // Act 
            var okResult = await _controller.GetUserAdventures(1);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        } 

        [Fact]
        public async Task CaptureUserChoices_WhenCalled_ReturnsOkResult()  
        {
            UserAdventureQuestionCaptureResource request = new UserAdventureQuestionCaptureResource()
            {
                ChoiceId = 2,
                QuestionId = 1,
                UserAdventureId = 1
            };
            // Act 
            var okResult = await _controller.CaptureUserChoices(request);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        } 
    }
}
