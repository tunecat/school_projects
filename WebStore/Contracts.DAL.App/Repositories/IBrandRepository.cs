﻿﻿using System;using com.nipetu.webstore.Contracts.DAL.Base;
using com.nipetu.webstore.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

 namespace Contracts.DAL.App.Repositories
 {
     public interface IBrandRepository : IBrandRepository<Guid, Brand>, IBaseRepository<Brand>
     {
     }
     
     public interface IBrandRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity>
         where TDALEntity : class, IDomainEntity<TKey>, new()
         where TKey : IEquatable<TKey>
     {
     }
 }