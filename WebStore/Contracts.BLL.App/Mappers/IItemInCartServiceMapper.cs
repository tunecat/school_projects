using com.nipetu.webstore.Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    
    public interface IItemInCartServiceMapper: IBaseBLLMapper<DALAppDTO.ItemInCart, BLLAppDTO.ItemInCart>
    {
    }
}