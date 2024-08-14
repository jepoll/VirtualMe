using Core.Contracts.DAL.Repositories.AddressTables;
using Shared.DAL.EF;
using CoreDomainAT = Core.Domain.AddressTables;
using DALDTOAT = Core.DAL.DTO.AddressTables;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.EF.Repositories.AddressTables;

public class AvatarsActivityRepository : BaseEntityRepository<CoreDomainAT.AvatarsActivity, DALDTOAT.AvatarsActivity, AppDbContext>, IAvatarsActivityRepository
{
    public AvatarsActivityRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainAT.AvatarsActivity, DALDTOAT.AvatarsActivity>(mapper))
    {
    }
    public IEnumerable<DALDTOAT.AvatarsActivity> GetWithData()
    {
        return CreateQuery().Include("Avatar")
            .Include("Activity")
            .Select(e => Mapper.Map(e)).ToList()!;
    }

    public async Task<IEnumerable<DALDTOAT.AvatarsActivity>> GetWithDataAsync()
    {
        return await CreateQuery().Include("Avatar")
            .Include("Activity")
            .Select(e => Mapper.Map(e)).ToListAsync()!;
    }

    public DALDTOAT.AvatarsActivity GetByAvatarId(Guid id)
    {
        return Mapper.Map(CreateQuery().FirstOrDefault(e => e.AvatarId.Equals(id)))!;
    }
}