using com.nipetu.webstore.Contracts.BLL.Base.Mappers;

namespace BLL.Base.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> :  com.nipetu.webstore.DAL.Base.Mappers.IdentityMapper<TLeftObject, TRightObject>
        where TLeftObject : class, new() 
        where TRightObject : class, new()
    {
    }
}