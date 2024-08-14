using AutoMapper;

namespace WebApp.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        // CreateMap<Core.BLL.DTO.Entities.Avatar, Core.DTO.v1_0.Entities.Avatar>().ReverseMap();
        CreateMap<Core.BLL.DTO.AddressTables.AvatarOwnsInterior, Core.DTO.v1_0.AddressTables.AvatarOwnsInterior>().ReverseMap();
        CreateMap<Core.BLL.DTO.AddressTables.AvatarsActivity, Core.DTO.v1_0.AddressTables.AvatarsActivity>().ReverseMap();
        CreateMap<Core.BLL.DTO.AddressTables.AvatarsTasks, Core.DTO.v1_0.AddressTables.AvatarsTasks>().ReverseMap();
        CreateMap<Core.BLL.DTO.AddressTables.Owns, Core.DTO.v1_0.AddressTables.Owns>().ReverseMap();
        CreateMap<Core.BLL.DTO.AddressTables.Reward, Core.DTO.v1_0.AddressTables.Reward>().ReverseMap();
        
        CreateMap<Core.BLL.DTO.Entities.Activity, Core.DTO.v1_0.Entities.Activity>().ReverseMap();
        CreateMap<Core.BLL.DTO.Entities.ActivityType, Core.DTO.v1_0.Entities.ActivityType>().ReverseMap();
        CreateMap<Core.BLL.DTO.Entities.Avatar, Core.DTO.v1_0.Entities.Avatar>().ReverseMap();
        CreateMap<Core.BLL.DTO.Entities.Chat, Core.DTO.v1_0.Entities.Chat>().ReverseMap();
        CreateMap<Core.BLL.DTO.Entities.Interior, Core.DTO.v1_0.Entities.Interior>().ReverseMap();
        CreateMap<Core.BLL.DTO.Entities.Item, Core.DTO.v1_0.Entities.Item>().ReverseMap();
        CreateMap<Core.BLL.DTO.Entities.Logs, Core.DTO.v1_0.Entities.Logs>().ReverseMap();
        CreateMap<Core.BLL.DTO.Entities.Message, Core.DTO.v1_0.Entities.Message>().ReverseMap();
        CreateMap<Core.BLL.DTO.Entities.TaskQuest, Core.DTO.v1_0.Entities.TaskQuest>().ReverseMap();
        CreateMap<Core.BLL.DTO.Entities.TaskType, Core.DTO.v1_0.Entities.TaskType>().ReverseMap();
    }
}
