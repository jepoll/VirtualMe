using AutoMapper;
using Core.Contracts.BLL.Services.Entities;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.Entities;
using Core.DAL.EF;
using Shared.BLL;

namespace Core.BLL.Services.Entities;

public class TaskTypeService : BaseEntityService<Core.DAL.DTO.Entities.TaskType,
    Core.BLL.DTO.Entities.TaskType, ITaskTypeRepository>, ITaskTypeService
{
    public TaskTypeService(ICoreUnitOfWork uow, ITaskTypeRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.Entities.TaskType, Core.BLL.DTO.Entities.TaskType>(mapper))
    {
    }
}