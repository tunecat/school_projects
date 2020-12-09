using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.BLL.Base.Services;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface ICategoryService : ICategoryRepositoryCustom<Guid, Category>
    {
        Task<IEnumerable<Category>> GetAllSubCategoriesAsync();
        Task<IEnumerable<Category>> GetAllParentCategoriesAsync();

    }
}