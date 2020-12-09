using System;
using com.nipetu.webstore.Contracts.BLL.Base.Services;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using Domain;
using ItemCategory = BLL.App.DTO.ItemCategory;

namespace Contracts.BLL.App.Services
{
    public interface IItemCategoryService: IItemCategoryRepository<Guid, ItemCategory>
    {
    
    }
}