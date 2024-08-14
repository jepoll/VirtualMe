using AutoMapper;

namespace Core.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Core.Domain.AddressTables.AvatarOwnsInterior, Core.DAL.DTO.AddressTables.AvatarOwnsInterior>().ReverseMap();
        CreateMap<Core.Domain.AddressTables.AvatarsActivity, Core.DAL.DTO.AddressTables.AvatarsActivity>().ReverseMap();
        CreateMap<Core.Domain.AddressTables.AvatarsTasks, Core.DAL.DTO.AddressTables.AvatarsTasks>().ReverseMap();
        CreateMap<Core.Domain.AddressTables.Owns, Core.DAL.DTO.AddressTables.Owns>().ReverseMap();
        CreateMap<Core.Domain.AddressTables.Reward, Core.DAL.DTO.AddressTables.Reward>().ReverseMap();
        
        CreateMap<Core.Domain.Entities.Activity, Core.DAL.DTO.Entities.Activity>().ReverseMap();
        CreateMap<Core.Domain.Entities.ActivityType, Core.DAL.DTO.Entities.ActivityType>().ReverseMap();
        CreateMap<Core.Domain.Entities.Avatar, Core.DAL.DTO.Entities.Avatar>().ReverseMap();
        CreateMap<Core.Domain.Entities.Chat, Core.DAL.DTO.Entities.Chat>().ReverseMap();
        CreateMap<Core.Domain.Entities.Interior, Core.DAL.DTO.Entities.Interior>().ReverseMap();
        CreateMap<Core.Domain.Entities.Item, Core.DAL.DTO.Entities.Item>().ReverseMap();
        CreateMap<Core.Domain.Entities.Logs, Core.DAL.DTO.Entities.Logs>().ReverseMap();
        CreateMap<Core.Domain.Entities.Message, Core.DAL.DTO.Entities.Message>().ReverseMap();
        CreateMap<Core.Domain.Entities.TaskQuest, Core.DAL.DTO.Entities.TaskQuest>().ReverseMap();
        CreateMap<Core.Domain.Entities.TaskType, Core.DAL.DTO.Entities.TaskType>().ReverseMap();
    }

}