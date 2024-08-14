using AutoMapper;
using Shared.Contracts.BLL;

namespace Core.BLL;

public class BllDalMapper<TLeftObject, TRightObject> : IBLLMapper<TLeftObject, TRightObject> 
    where TLeftObject : class 
    where TRightObject : class
{
    private readonly IMapper _mapper;

    public BllDalMapper(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public TLeftObject? Map(TRightObject? inObject)
    {
        return _mapper.Map<TLeftObject>(inObject);
    }

    public TRightObject? Map(TLeftObject? inObject)
    {
        return _mapper.Map<TRightObject>(inObject);
    }
}
