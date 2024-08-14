using AutoMapper;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL.Services.AddressTables;
using Core.Contracts.BLL.Services.Entities;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.AddressTables;
using Core.Contracts.DAL.Repositories.Entities;
using Core.DAL.EF;
using Shared.BLL;

namespace Core.BLL.Services.Entities;

public class ActivityService : BaseEntityService<Core.DAL.DTO.Entities.Activity,
    Core.BLL.DTO.Entities.Activity, IActivityRepository>, IActivityService
{
    public ActivityService(ICoreUnitOfWork uow, IActivityRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.Entities.Activity, Core.BLL.DTO.Entities.Activity>(mapper))
    {
    }

    public Activity GetById(Guid id)
    {
        return Mapper.Map(Rep.GetById(id))!;
    }
}