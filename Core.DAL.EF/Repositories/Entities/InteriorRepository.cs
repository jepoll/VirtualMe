using AutoMapper;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.DAL.EF;
using CoreDomainE = Core.Domain.Entities;
using DALDTOE = Core.DAL.DTO.Entities;

namespace Core.DAL.EF.Repositories.Entities;

public class InteriorRepository : BaseEntityRepository<CoreDomainE.Interior, DALDTOE.Interior, AppDbContext>, IInteriorRepository
{
    public InteriorRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainE.Interior, DALDTOE.Interior>(mapper))
    {
    }
    
    public DALDTOE.Interior GetById(Guid id)
    {
        return Mapper.Map(CreateQuery().First(e => e.Id.Equals(id)))!;
    }
}