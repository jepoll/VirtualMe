using Core.Contracts.DAL.Repositories.AddressTables;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.AddressTables;

public interface IAvatarOwnsInteriorService : IEntityRepository<Core.BLL.DTO.AddressTables.AvatarOwnsInterior>, IAvatarOwnsInteriorRepositoryShared<Core.BLL.DTO.AddressTables.AvatarOwnsInterior>
{
    
}