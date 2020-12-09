using com.nipetu.webstore.Contracts.BLL.Base.Mappers;

namespace BLL.Base.Mappers
{
    public class BaseBLLMapper<TLeftObject, TRightObject> : com.nipetu.webstore.DAL.Base.Mappers.BaseDALMapper<TLeftObject, TRightObject>
        where TRightObject : class, new()
        where TLeftObject : class, new()
    {
    }
}