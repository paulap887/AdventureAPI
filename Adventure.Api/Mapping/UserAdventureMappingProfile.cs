using Adventure.Api.Resources;
using Adventure.Api.Resources.UserAdventure;
using AutoMapper;

namespace Adventure.Api.Mapping
{
    /// <summary>
    /// UserAdventureMappingProfile
    /// </summary>
    public class UserAdventureMappingProfile : Profile
    {
        /// <summary>
        /// UserAdventureMappingProfile
        /// </summary>
        public UserAdventureMappingProfile()
        {
            CreateMap<UserAdventureRequestResource, Adventure.Core.Models.UserAdventure>();
            CreateMap<Adventure.Core.Models.UserAdventure, UserAdventureRequestResource>();

            CreateMap<Adventure.Core.Models.UserAdventure, UserAdventureResource>()
                .ForMember(src => src.UserAdventureId, opt => opt.MapFrom(dest => dest.Id))
                .ForMember(src => src.AdventureId, opt => opt.MapFrom(dest => dest.Adventure.Id))
                .ForMember(src => src.AdventureDescription, opt => opt.MapFrom(dest => dest.Adventure.Description))
                .ForMember(src => src.Questions, opt => opt.MapFrom(dest => dest.UserAdventureQuestions));

            CreateMap<Adventure.Core.Models.UserAdventureQuestion, UserAdventureQuestionResource>()
            .ForMember(src => src.QuestionId, opt => opt.MapFrom(dest => dest.QuestionId))
            .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.Question.QuestionDescription))
            .ForMember(src => src.IsRootQuestion, opt => opt.MapFrom(dest => dest.Question.IsRootQuestion))
            .ForMember(src => src.DisplayOrder, opt => opt.MapFrom(dest => dest.Question.DisplayOrder))
            .ForMember(src => src.ParentQuestionId, opt => opt.MapFrom(dest => dest.Question.ParentQuestionId))
            .ForMember(src => src.ParentChoiceId, opt => opt.MapFrom(dest => dest.Question.ParentChoice))
            .ForMember(src => src.SelectedChoiceId, opt => opt.MapFrom(dest => dest.ChoiceId));


            CreateMap<Adventure.Core.Models.UserAdventure, UserAdventureQuestionCaptureResource>();
            CreateMap<Adventure.Core.Models.UserAdventureQuestion, UserAdventureQuestionCaptureResource>();
            CreateMap<UserAdventureQuestionCaptureResource, Adventure.Core.Models.UserAdventureQuestion>();

            CreateMap<Adventure.Core.Models.Question, UserAdventureQuestionResource>()
                  .ForMember(src => src.QuestionId, opt => opt.MapFrom(dest => dest.Id))
                  .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.QuestionDescription))
                  .ForMember(src => src.IsRootQuestion, opt => opt.MapFrom(dest => dest.IsRootQuestion))
                  .ForMember(src => src.DisplayOrder, opt => opt.MapFrom(dest => dest.DisplayOrder))
                  .ForMember(src => src.ParentQuestionId, opt => opt.MapFrom(dest => dest.ParentQuestionId))
                  .ForMember(src => src.ParentChoiceId, opt => opt.MapFrom(dest => dest.ParentChoice))
                  .ForMember(src => src.SelectedChoiceId, opt => opt.MapFrom(dest => 0));
        }
    }
}
