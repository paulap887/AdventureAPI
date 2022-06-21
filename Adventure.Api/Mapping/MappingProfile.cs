using Adventure.Api.Resources;
using AutoMapper;
using System;

namespace Adventure.Api.Mapping
{
    /// <summary>
    ///  Mappings 
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        ///  Mappings 
        /// </summary>
        public MappingProfile()
        {
            CreateMap<AdventureResource, Adventure.Core.Models.Adventure>()
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.AdventureTitle));

            CreateMap<AdventureQuestionUpdateResource, Adventure.Core.Models.Question>()
                 .ForMember(src => src.Id, opt => opt.MapFrom(dest => dest.QuestionId))
                 .ForMember(src => src.DisplayOrder, opt => opt.MapFrom(dest => (dest.IsRootQuestion ? 1 : Convert.ToInt32(dest.DisplayOrder))))
                .ForMember(src => src.QuestionDescription, opt => opt.MapFrom(dest => dest.Description))
                .ForMember(src => src.ParentQuestionId, opt => opt.MapFrom(dest => (dest.IsRootQuestion ? null : (int?)dest.ParentQuestionId)))
                .ForMember(src => src.ParentChoice, opt => opt.MapFrom(dest => (dest.IsRootQuestion ? null : (int?)dest.ParentChoiceId)));

            CreateMap<AdventureQuestionResource, Adventure.Core.Models.Adventure>()
               .ForMember(src => src.Id, opt => opt.MapFrom(dest => dest.AdventureId))
               .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.AdventureTitle)); 

            CreateMap<Adventure.Core.Models.Adventure, AdventureQuestionResource>()
               .ForMember(src => src.AdventureId, opt => opt.MapFrom(dest => dest.Id))
               .ForMember(src => src.AdventureTitle, opt => opt.MapFrom(dest => dest.Description));

            CreateMap<Adventure.Core.Models.Question, AdventureQuestionUpdateResource>()
                .ForMember(src => src.QuestionId, opt => opt.MapFrom(dest => dest.Id)) 
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.QuestionDescription))
                .ForMember(src => src.ParentQuestionId, opt => opt.MapFrom(dest => dest.ParentQuestionId))
                .ForMember(src => src.ParentChoiceId, opt => opt.MapFrom(dest => dest.ParentChoice));  
        }
    }
}
