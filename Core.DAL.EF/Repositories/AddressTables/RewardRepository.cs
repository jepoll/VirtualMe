using AutoMapper;
using Core.Contracts.DAL.Repositories.AddressTables;
using Microsoft.EntityFrameworkCore;
using Shared.DAL.EF;
using CoreDomainAT = Core.Domain.AddressTables;
using DALDTOAT = Core.DAL.DTO.AddressTables;

namespace Core.DAL.EF.Repositories.AddressTables;

public class RewardRepository : BaseEntityRepository<CoreDomainAT.Reward, DALDTOAT.Reward, AppDbContext>, IRewardRepository
{
    public RewardRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainAT.Reward, DALDTOAT.Reward>(mapper))
    {
    }
    //TODO: custom methods
    public async Task<IEnumerable<DALDTOAT.Reward>> GetWithDataAsync()
    {
        return (await CreateQuery()
            .Include("Item")
            .Include("TaskQuest")
            .Include("TaskQuest.Activity")
            .Select(e => Mapper.Map(e)).ToListAsync()!)!;
    }

    public DALDTOAT.Reward GetByTaskQuestId(Guid id)
    {
        return Mapper.Map(CreateQuery().FirstOrDefault(e => e.TaskQuestId.Equals(id)))!;
    }
}