using Contracts.DAL.Base.Mappers;

namespace DAL.Base.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> : IBaseDALMapper<TLeftObject, TRightObject>
        where TLeftObject : class, new()
        where TRightObject : class, new()
    {
        public TRightObject Map(TLeftObject inObject)
        {
            return inObject as TRightObject ?? default!;
        }

        public TLeftObject Map(TRightObject inObject)
        {
            return inObject as TLeftObject ?? default!;
        }
        
    }
}