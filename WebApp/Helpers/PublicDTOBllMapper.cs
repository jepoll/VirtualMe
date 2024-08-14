using AutoMapper;
using Core.DTO.v1_0.AddressTables;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Helpers;

public class PublicDTOBllMapper<TLeftObject, TRightObject> : IAppMapper<TLeftObject, TRightObject>
    where TLeftObject : class where TRightObject : class
{
    private readonly IMapper _mapper;
    
    public PublicDTOBllMapper(IMapper mapper)
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
