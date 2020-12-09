namespace Contracts.BLL.Base.Mappers
{
    public interface IBaseBLLMapper<TLeftObject, TRightObject> : com.nipetu.webstore.Contracts.DAL.Base.Mappers.IBaseDALMapper<TLeftObject, TRightObject>
        where TLeftObject: class?, new()
        where TRightObject: class?, new()
    {
        
    }


}