using AutoMapper;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.DAL.EF;
using CoreDomainE = Core.Domain.Entities;
using DALDTOE = Core.DAL.DTO.Entities;

namespace Core.DAL.EF.Repositories.Entities;

public class MessageRepository : BaseEntityRepository<CoreDomainE.Message, DALDTOE.Message, AppDbContext>, IMessageRepository
{
    public MessageRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainE.Message, DALDTOE.Message>(mapper))
    {
    }
    
    public IEnumerable<DALDTOE.Message> GetAllMessages(Guid chatId)
    {
        return CreateQuery(default, true).ToList().FindAll(entity => entity.ChatId.Equals(chatId)).Select(e => Mapper.Map(e))!;
    }
}