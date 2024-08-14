using AutoMapper;
using Core.BLL.Services;
using Core.BLL.Services.AddressTables;
using Core.BLL.Services.Entities;
using Core.Contracts.BLL;
using Core.Contracts.BLL.Services.AddressTables;
using Core.Contracts.BLL.Services.Entities;
using Core.Contracts.DAL;
using Core.DAL.DTO.AddressTables;
using Core.DAL.EF;
using Shared.BLL;
using Shared.Contracts.DAL;

namespace Core.BLL;

public class CoreBLL : BaseBLL<AppDbContext>, ICoreBLL
{
    private readonly IMapper _mapper;
    private readonly ICoreUnitOfWork _uow;
    
    public CoreBLL(ICoreUnitOfWork uow, IMapper mapper) : base(uow)
    {
        _mapper = mapper;
        _uow = uow;
    }

    private IAvatarOwnsInteriorService _avatarOwnsInterior;
    public IAvatarOwnsInteriorService AvatarOwnsInterior => _avatarOwnsInterior ?? new AvatarOwnsInteriorService(_uow, _uow.AvatarOwnsInterior, _mapper);
    private IAvatarsActivityService _avatarsActivity;
    public IAvatarsActivityService AvatarsActivity =>
        _avatarsActivity ?? new AvatarsActivityService(_uow, _uow.AvatarsActivities, _mapper);

    private IAvatarsTasksService _avatarsTasks;
    public IAvatarsTasksService AvatarsTasks => _avatarsTasks ?? new AvatarsTasksService(_uow, _uow.AvatarsTasks, _mapper);
    private IRewardService _reward;
    public IRewardService Reward => _reward ?? new RewardService(_uow, _uow.Rewards, _mapper);
    private IActivityService _activity;
    public IActivityService Activity => _activity ?? new ActivityService(_uow, _uow.Activities, _mapper);
    private IActivityTypeService _activityType;
    public IActivityTypeService ActivityType => _activityType ?? new ActivityTypeService(_uow, _uow.ActivityTypes, _mapper);
    private IAvatarService _avatar;
    public IAvatarService Avatar => _avatar ?? new AvatarService(_uow, _uow.Avatars, _mapper);
    private IChatService _chat;
    public IChatService Chat => _chat ?? new ChatService(_uow, _uow.Chats, _mapper);
    private IInteriorService _interior;
    public IInteriorService Interior => _interior ?? new InteriorService(_uow, _uow.Interiors, _mapper);
    private IItemService _item;
    public IItemService Item => _item ?? new ItemService(_uow, _uow.Items, _mapper);
    private ILogsService _logs;
    public ILogsService Logs => _logs ?? new LogsService(_uow, _uow.Logs, _mapper);
    private IMessageService _message;
    public IMessageService Message => _message ?? new MessageService(_uow, _uow.Messages, _mapper);
    private ITaskQuestService _taskQuest;
    public ITaskQuestService TaskQuest => _taskQuest ?? new TaskQuestService(_uow, _uow.TaskQuests, _mapper);
    private ITaskTypeService _taskType;
    public ITaskTypeService TaskType => _taskType ?? new TaskTypeService(_uow, _uow.TaskTypes, _mapper);
    private IOwnsService _owns;
    public IOwnsService Owns => _owns ?? new OwnsService(_uow, _uow.Owns, _mapper);
}