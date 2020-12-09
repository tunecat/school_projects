using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mapper;
using com.nipetu.webstore.BLL.Base.Mappers;
using com.nipetu.webstore.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ItemCategoryService: BaseEntityService<IItemCategoryRepository, IAppUnitOfWork, IItemCategoryServiceMapper ,DAL.App.DTO.ItemCategory, BLL.App.DTO.ItemCategory>, IItemCategoryService
             {
         
                 public ItemCategoryService(IAppUnitOfWork unitOfWork)
                     : base(unitOfWork, new ItemCategoryServiceMapper(), unitOfWork.ItemCategories)
                 {
                 }
                 
             }
}