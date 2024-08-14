using AutoMapper;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.DAL.EF;
using CoreDomainE = Core.Domain.Entities;
using DALDTOE = Core.DAL.DTO.Entities;

namespace Core.DAL.EF.Repositories.Entities;

public class ActivityTypeRepository : BaseEntityRepository<CoreDomainE.ActivityType, DALDTOE.ActivityType, AppDbContext>, IActivityTypeReposirtory
{
    public ActivityTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainE.ActivityType, DALDTOE.ActivityType>(mapper))
    {
    }
    public DALDTOE.ActivityType GetById(Guid id)
    {
        return Mapper.Map(CreateQuery().First(e => e.Id.Equals(id)))!;
    }
}