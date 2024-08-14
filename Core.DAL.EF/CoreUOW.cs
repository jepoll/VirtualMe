using Core.Contracts.DAL;
using Core.DAL.EF.Repositories;
using Core.Domain.Identity;
using AutoMapper;
using Core.Contracts.DAL.Repositories.AddressTables;
using Core.Contracts.DAL.Repositories.Entities;
using Core.DAL.EF.Repositories.AddressTables;
using Core.DAL.EF.Repositories.Entities;
using Shared.Contracts.DAL;
using Shared.DAL.EF;

namespace Core.DAL.EF;

public class CoreUOW : BaseUnitOfWork<AppDbContext>, ICoreUnitOfWork
{
    private readonly IMapper _mapper;
    public CoreUOW(AppDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private IAvatarsActivityRepository? _avatarsActivities;
    public IAvatarsActivityRepository AvatarsActivities => _avatarsActivities ?? new AvatarsActivityRepository(UOWDbContext, _mapper);

    private IAvatarsTasksRepository? _avatarsTasks;
    public IAvatarsTasksRepository AvatarsTasks => _avatarsTasks ?? new AvatarsTasksRepository(UOWDbContext, _mapper);
    
    private IOwnsRepository? _owns;
    public IOwnsRepository Owns => _owns ?? new OwnsRepository(UOWDbContext, _mapper);

    private IRewardRepository? _rewards;
    public IRewardRepository Rewards => _rewards ?? new RewardRepository(UOWDbContext, _mapper);

    private IActivityRepository? _activities;
    public IActivityRepository Activities => _activities ?? new ActivityRepository(UOWDbContext, _mapper);

    private IActivityTypeReposirtory? _activityTypes;
    public IActivityTypeReposirtory ActivityTypes => _activityTypes ?? new ActivityTypeRepository(UOWDbContext, _mapper);

    private IAvatarRepository? _avatars;
    public IAvatarRepository Avatars => _avatars ?? new AvatarRepository(UOWDbContext, _mapper);

    private IChatRepository? _chats;
    public IChatRepository Chats => _chats ?? new ChatRepository(UOWDbContext, _mapper);

    private IAvatarOwnsInteriorRepository? _avatarOwnsInterior;
    public IAvatarOwnsInteriorRepository AvatarOwnsInterior =>
        _avatarOwnsInterior ?? new AvatarOwnsInteriorRepository(UOWDbContext, _mapper);

    private IInteriorRepository? _interiors;
    public IInteriorRepository Interiors => _interiors ?? new InteriorRepository(UOWDbContext, _mapper);

    private IItemRepository? _items;
    public IItemRepository Items => _items ?? new ItemRepository(UOWDbContext, _mapper);

    private IMessageRepository? _messages;
    public IMessageRepository Messages => _messages ?? new MessageRepository(UOWDbContext, _mapper);

    private ITaskQuestRepository? _taskQuests;
    public ITaskQuestRepository TaskQuests => _taskQuests ?? new TaskQuestRepository(UOWDbContext, _mapper);

    private ITaskTypeRepository? _taskTypes;
    public ITaskTypeRepository TaskTypes => _taskTypes ?? new TaskTypeRepository(UOWDbContext, _mapper);

    private ILogsRepository? _logs;
    public ILogsRepository Logs => _logs ?? new LogsRepository(UOWDbContext, _mapper);
    
    private IEntityRepository<AppUser>? _users;
    public IEntityRepository<AppUser> Users => _users ??
                                               new BaseEntityRepository<AppUser, AppUser, AppDbContext>(UOWDbContext,
                                                   new DalDomainMapper<AppUser, AppUser>(_mapper));

}