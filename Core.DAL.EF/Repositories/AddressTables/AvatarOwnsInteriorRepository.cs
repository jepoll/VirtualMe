using AutoMapper;
using Core.Contracts.DAL.Repositories.AddressTables;
using Microsoft.EntityFrameworkCore;
using Shared.DAL.EF;
using CoreDomainAT = Core.Domain.AddressTables;
using DALDTOAT = Core.DAL.DTO.AddressTables;

namespace Core.DAL.EF.Repositories.AddressTables;

public class AvatarOwnsInteriorRepository : BaseEntityRepository<CoreDomainAT.AvatarOwnsInterior, DALDTOAT.AvatarOwnsInterior, AppDbContext>, IAvatarOwnsInteriorRepository
{
    public AvatarOwnsInteriorRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainAT.AvatarOwnsInterior, DALDTOAT.AvatarOwnsInterior>(mapper))
    {
    }
    public virtual IEnumerable<DALDTOAT.AvatarOwnsInterior> GetAllByAvatarsId(Guid id)
    {
        return CreateQuery(default, true).ToList()
            .Where(e => e.AvatarId.Equals(id))
            .Select(e => Mapper.Map(e))!;
    }

    public virtual IEnumerable<DALDTOAT.AvatarOwnsInterior> GetAllByInteriorId(Guid id)
    {
        return CreateQuery(default, true).ToList()
            .Where(e => e.InteriorId.Equals(id))
            .Select(e => Mapper.Map(e))!;
    }

    public override bool Exists(Guid avatarId, Guid interiorId)
    {
        return CreateQuery(default).Any(e => e.AvatarId.Equals(avatarId) && e.InteriorId.Equals(interiorId));
    }

    public virtual async Task<IEnumerable<DALDTOAT.AvatarOwnsInterior>> GetAllByAvatarsIdAsync(Guid id)
    {
        return (await CreateQuery(default, true).ToListAsync())
            .Where(e => e.AvatarId.Equals(id))
            .Select(e => Mapper.Map(e))!;
    }

    public virtual async Task<IEnumerable<DALDTOAT.AvatarOwnsInterior>> GetAllByInteriorIdAsync(Guid id)
    {
        return (await CreateQuery(default, true).ToListAsync())
            .Where(e => e.InteriorId.Equals(id))
            .Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<DALDTOAT.AvatarOwnsInterior>> GetAllByAvatarIdWithInteriorAsync(Guid id)
    {
        return (await CreateQuery().Include("Interior").Where(e => e.AvatarId.Equals(id))
            .Select(e => Mapper.Map(e)).ToListAsync()!)!;
    }

    public override async Task<bool> ExistsAsync(Guid avatarId, Guid interiorId)
    {
        return await CreateQuery(default)
            .AnyAsync(e => e.AvatarId.Equals(avatarId) && e.InteriorId.Equals(interiorId));
    }
}