using AutoMapper;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.DAL.EF;
using CoreDomainE = Core.Domain.Entities;
using DALDTOE = Core.DAL.DTO.Entities;

namespace Core.DAL.EF.Repositories.Entities;

public class ActivityRepository : BaseEntityRepository<CoreDomainE.Activity, DALDTOE.Activity, AppDbContext>, IActivityRepository
{
    public ActivityRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainE.Activity, DALDTOE.Activity>(mapper))
    {
    }

    public DALDTOE.Activity GetById(Guid id)
    {
        return Mapper.Map(CreateQuery().FirstOrDefault(e => e.Id.Equals(id)))!;
    }
}