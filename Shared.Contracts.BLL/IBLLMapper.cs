using Shared.Contracts.DAL;

namespace Shared.Contracts.BLL;

public interface IBLLMapper<TLeftObject, TRightObject> : IDalMapper<TLeftObject, TRightObject>
    where TLeftObject : class
    where TRightObject : class
{
}
