using AutoMapper;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL.Services.Entities;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.Entities;
using Core.DAL.EF;
using Shared.BLL;

namespace Core.BLL.Services.Entities;

public class InteriorService : BaseEntityService<Core.DAL.DTO.Entities.Interior,
    Core.BLL.DTO.Entities.Interior, IInteriorRepository>, IInteriorService
{
    public InteriorService(ICoreUnitOfWork uow, IInteriorRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.Entities.Interior, Core.BLL.DTO.Entities.Interior>(mapper))
    {
    }

    public Interior GetById(Guid id)
    {
        return Mapper.Map(Rep.GetById(id))!;
    }
}