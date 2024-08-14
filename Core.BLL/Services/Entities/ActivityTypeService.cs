using AutoMapper;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL.Services.Entities;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.Entities;
using Core.DAL.EF;
using Shared.BLL;

namespace Core.BLL.Services.Entities;

public class ActivityTypeService : BaseEntityService<Core.DAL.DTO.Entities.ActivityType,
    Core.BLL.DTO.Entities.ActivityType, IActivityTypeReposirtory>, IActivityTypeService
{
    public ActivityTypeService(ICoreUnitOfWork uow, IActivityTypeReposirtory repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.Entities.ActivityType, Core.BLL.DTO.Entities.ActivityType>(mapper))
    {
    }

    public ActivityType GetById(Guid id)
    {
        return Mapper.Map(Rep.GetById(id))!;
    }
}