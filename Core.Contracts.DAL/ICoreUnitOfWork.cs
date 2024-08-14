using Core.Contracts.DAL.Repositories.AddressTables;
using Core.Contracts.DAL.Repositories.Entities;
using Core.Domain.Identity;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL;

public interface ICoreUnitOfWork : IUnitOfWork
{
    IAvatarsActivityRepository AvatarsActivities { get; }
    IAvatarsTasksRepository AvatarsTasks { get; }
    IOwnsRepository Owns { get; }
    IAvatarOwnsInteriorRepository AvatarOwnsInterior { get; }
    IRewardRepository Rewards { get; }
    IActivityRepository Activities { get; }
    IActivityTypeReposirtory ActivityTypes { get; }
    IAvatarRepository Avatars { get; }
    IChatRepository Chats { get; }
    IInteriorRepository Interiors { get; }
    IItemRepository Items { get; }
    IMessageRepository Messages { get; }
    ITaskQuestRepository TaskQuests { get; }
    ITaskTypeRepository TaskTypes { get; }
    ILogsRepository Logs { get; }
    IEntityRepository<AppUser> Users { get; }

}