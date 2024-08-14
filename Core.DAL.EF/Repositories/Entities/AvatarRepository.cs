using AutoMapper;
using Core.Contracts.DAL.Repositories.Entities;
using Core.Resources.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.DAL.EF;
using CoreDomainE = Core.Domain.Entities;
using DALDTOE = Core.DAL.DTO.Entities;

namespace Core.DAL.EF.Repositories.Entities;

public class AvatarRepository : BaseEntityRepository<CoreDomainE.Avatar, DALDTOE.Avatar, AppDbContext>, IAvatarRepository
{
    public AvatarRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainE.Avatar, DALDTOE.Avatar>(mapper))
    {
    }

    public IEnumerable<DALDTOE.Avatar> GetAvatarsWithUsers()
    {
        return CreateQuery().Include("AppUser")
            .Select(e => Mapper.Map(e))
            .ToList()!;
    }

    public async Task<IEnumerable<DALDTOE.Avatar>> GetAvatarsWithUsersAsync()
    {
        return (await CreateQuery(default, false).Include("AppUser")
            .Select(e => Mapper.Map(e))
            .ToListAsync())!;
    }

    public DALDTOE.Avatar GetById(Guid id)
    {
        return Mapper.Map(CreateQuery().FirstOrDefault(e => e.Id.Equals(id)))!;
    }

    public async Task<IEnumerable<DALDTOE.Avatar>> GetByUserId(Guid id)
    {
        var res = (await CreateQuery(noTracking: true).Where(e => e.AppUserId.Equals(id))
            .Select(e => Mapper.Map(e))
            .ToListAsync())!;

        return res;
    }

    public async Task<DALDTOE.Avatar?> GetAvatarUpdatedByUserId(Guid id)
    {
        var avatar = (await GetByUserId(id)).FirstOrDefault(e => e.IsActive);
        return avatar;
    }
    
}