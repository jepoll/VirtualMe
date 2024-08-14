using Core.Contracts.BLL.Services.AddressTables;
using Core.Contracts.BLL.Services.Entities;
using Shared.Contracts.BLL;

namespace Core.Contracts.BLL;

public interface ICoreBLL : IBLL
{
    IAvatarOwnsInteriorService AvatarOwnsInterior { get; }
    IAvatarsActivityService AvatarsActivity { get; }
    IAvatarsTasksService AvatarsTasks { get; }
    IOwnsService Owns { get; }
    IRewardService Reward { get; }
    IActivityService Activity { get; }
    IActivityTypeService ActivityType { get; }
    IAvatarService Avatar { get; }
    IChatService Chat { get; }
    IInteriorService Interior { get; }
    IItemService Item { get; }
    ILogsService Logs { get; }
    IMessageService Message { get; }
    ITaskQuestService TaskQuest { get; }
    ITaskTypeService TaskType { get; }
}