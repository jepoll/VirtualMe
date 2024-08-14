using AutoMapper;

namespace Core.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Core.DAL.DTO.AddressTables.AvatarOwnsInterior, Core.BLL.DTO.AddressTables.AvatarOwnsInterior>().ReverseMap();
        CreateMap<Core.DAL.DTO.AddressTables.AvatarsActivity, Core.BLL.DTO.AddressTables.AvatarsActivity>().ReverseMap();
        CreateMap<Core.DAL.DTO.AddressTables.AvatarsTasks, Core.BLL.DTO.AddressTables.AvatarsTasks>().ReverseMap();
        CreateMap<Core.DAL.DTO.AddressTables.Owns, Core.BLL.DTO.AddressTables.Owns>().ReverseMap();
        CreateMap<Core.DAL.DTO.AddressTables.Reward, Core.BLL.DTO.AddressTables.Reward>().ReverseMap();
        
        CreateMap<Core.DAL.DTO.Entities.Activity, Core.BLL.DTO.Entities.Activity>().ReverseMap();
        CreateMap<Core.DAL.DTO.Entities.ActivityType, Core.BLL.DTO.Entities.ActivityType>().ReverseMap();
        CreateMap<Core.DAL.DTO.Entities.Avatar, Core.BLL.DTO.Entities.Avatar>().ReverseMap();
        CreateMap<Core.DAL.DTO.Entities.Chat, Core.BLL.DTO.Entities.Chat>().ReverseMap();
        CreateMap<Core.DAL.DTO.Entities.Interior, Core.BLL.DTO.Entities.Interior>().ReverseMap();
        CreateMap<Core.DAL.DTO.Entities.Item, Core.BLL.DTO.Entities.Item>().ReverseMap();
        CreateMap<Core.DAL.DTO.Entities.Logs, Core.BLL.DTO.Entities.Logs>().ReverseMap();
        CreateMap<Core.DAL.DTO.Entities.Message, Core.BLL.DTO.Entities.Message>().ReverseMap();
        CreateMap<Core.DAL.DTO.Entities.TaskQuest, Core.BLL.DTO.Entities.TaskQuest>().ReverseMap();
        CreateMap<Core.DAL.DTO.Entities.TaskType, Core.BLL.DTO.Entities.TaskType>().ReverseMap();
    }

}