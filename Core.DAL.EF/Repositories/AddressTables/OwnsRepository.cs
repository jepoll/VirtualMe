using AutoMapper;
using Core.Contracts.DAL.Repositories.AddressTables;
using Core.DAL.DTO.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.DAL.EF;
using CoreDomainAT = Core.Domain.AddressTables;
using DALDTOAT = Core.DAL.DTO.AddressTables;
using Item = Core.Domain.Entities.Item;

namespace Core.DAL.EF.Repositories.AddressTables;

public class OwnsRepository : BaseEntityRepository<CoreDomainAT.Owns, DALDTOAT.Owns, AppDbContext>, IOwnsRepository
{
    public OwnsRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainAT.Owns, DALDTOAT.Owns>(mapper))
    {
    }
    public IEnumerable<DALDTOAT.Owns> GetAllByAvatarId(Guid avatarId)
    {
        return CreateQuery().Include("Item").Where(e => e.AvatarId.Equals(avatarId))
            .Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<DALDTOAT.Owns>> GetAllByAvatarIdAsync(Guid avatarId)
    {
        return (await CreateQuery().Include("Item").Where(e => e.AvatarId.Equals(avatarId))
            .Select(e => Mapper.Map(e))
            .ToListAsync())!;
    }

    public DALDTOAT.Owns GetByAvatarAndItemIds(Guid avatarId, Guid itemId)
    {
        return Mapper.Map(CreateQuery().Where(e => e.AvatarId.Equals(avatarId))
            .FirstOrDefault(e => e.ItemId.Equals(itemId)))!;
    }

    public DALDTOAT.Owns GetByIdString(string id)
    {
        return Mapper.Map(CreateQuery().FirstOrDefault(e => e.Id.ToString().Equals(id)))!;
    }

    public async Task<IEnumerable<DALDTOAT.Owns>> GetAllWIthDataAsync()
    {
        return (await CreateQuery().Include("Avatar").Include("Item").ToListAsync())
            .Select(e => Mapper.Map(e))
            .ToList()!;
    }

    public async Task<DALDTOAT.Owns> GetByIdWithData(Guid id)
    {
        return Mapper.Map(await CreateQuery().Include("Avatar")
            .Include("Item")
            .FirstOrDefaultAsync(e => e.Id.Equals(id)))!;
    }
}