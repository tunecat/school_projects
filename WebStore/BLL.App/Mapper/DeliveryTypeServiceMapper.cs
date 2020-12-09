using com.nipetu.webstore.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;

namespace BLL.App.Mapper
{ 
    public class DeliveryTypeServiceMapper : BaseBLLMapper<DAL.App.DTO.DeliveryType, BLL.App.DTO.DeliveryType>, IDeliveryTypeServiceMapper
    {
        public DeliveryTypeServiceMapper()
        {
            
        }
    }
}