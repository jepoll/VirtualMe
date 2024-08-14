using Core.Contracts.DAL.Repositories.AddressTables;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.Entities;


public interface IMessageService : IEntityRepository<Core.BLL.DTO.Entities.Message>
{
    public IEnumerable<Core.BLL.DTO.Entities.Message> GetAllMessages(Guid chatId);
}