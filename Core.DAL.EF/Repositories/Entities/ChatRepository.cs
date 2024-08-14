using AutoMapper;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.DAL.EF;
using CoreDomainE = Core.Domain.Entities;
using DALDTOE = Core.DAL.DTO.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL.EF.Repositories.Entities;

public class ChatRepository : BaseEntityRepository<CoreDomainE.Chat, DALDTOE.Chat, AppDbContext>, IChatRepository
{
    public ChatRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainE.Chat, DALDTOE.Chat>(mapper))
    {
    }

    public override bool Exists(Guid avatar1Id, Guid avatar2Id)
    {
        return CreateQuery(default).Any(e => (e.Avatar1Id.Equals(avatar1Id) && e.Avatar2Id.Equals(avatar2Id)) ||
                                             (e.Avatar1Id.Equals(avatar2Id) && e.Avatar2Id.Equals(avatar1Id)));
    }

    public override async Task<bool> ExistsAsync(Guid avatar1Id, Guid avatar2Id)
    {
        return await CreateQuery(default)
            .AnyAsync(e => (e.Avatar1Id.Equals(avatar1Id) && e.Avatar2Id.Equals(avatar2Id)) ||
                           (e.Avatar1Id.Equals(avatar2Id) && e.Avatar2Id.Equals(avatar1Id)));
    }
}