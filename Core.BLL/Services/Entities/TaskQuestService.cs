using AutoMapper;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL.Services.Entities;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.Entities;
using Core.DAL.EF;
using Shared.BLL;

namespace Core.BLL.Services.Entities;

public class TaskQuestService : BaseEntityService<Core.DAL.DTO.Entities.TaskQuest,
    Core.BLL.DTO.Entities.TaskQuest, ITaskQuestRepository>, ITaskQuestService
{
    public TaskQuestService(ICoreUnitOfWork uow, ITaskQuestRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.Entities.TaskQuest, Core.BLL.DTO.Entities.TaskQuest>(mapper))
    {
    }

    public async Task<IEnumerable<TaskQuest>> GetWithDataAsync()
    {
        return (await Rep.GetWithDataAsync()).Select(e => Mapper.Map(e)).ToList()!;
    }

    public TaskQuest GetByIdWithData(Guid id)
    {
        return Mapper.Map(Rep.GetByIdWithData(id))!;
    }
}