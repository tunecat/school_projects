﻿using com.nipetu.webstore.Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface IItemServiceMapper: IBaseBLLMapper<DALAppDTO.Item, BLLAppDTO.Item>
    {
    }
}