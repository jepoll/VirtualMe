using DALDTOE = Core.DAL.DTO.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.Entities;

public interface IMessageRepository : IEntityRepository<DALDTOE.Message>
{
    public IEnumerable<DALDTOE.Message> GetAllMessages(Guid chatId);
}