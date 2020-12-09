﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.BLL.Base.Mappers;
using com.nipetu.webstore.Contracts.BLL.Base.Services;
using com.nipetu.webstore.Contracts.DAL.Base;
using com.nipetu.webstore.DAL.Base.Mappers;

using com.nipetu.webstore.Contracts.DAL.Base.Repositories;


namespace BLL.Base.Services
{
    public class BaseEntityService<TServiceRepository, TUnitOfWork, TMapper, TDALEntity, TBLLEntity> :
        BaseEntityService<Guid, TServiceRepository, TUnitOfWork, TMapper, TDALEntity, TBLLEntity>, IBaseEntityService<TBLLEntity>
        where TBLLEntity : class, IDomainEntity<Guid>, new()
        where TDALEntity : class, IDomainEntity<Guid>, new()
        where TUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
        where TServiceRepository : IBaseRepository<Guid, TDALEntity>
        where TMapper : IBaseBLLMapper<TDALEntity, TBLLEntity>
    {
        public BaseEntityService(TUnitOfWork unitOfWork, TMapper mapper,
            TServiceRepository serviceRepository) : base(unitOfWork, mapper, serviceRepository)
        {
        }
    }
    
    public class BaseEntityService<TKey, TServiceRepository, TUnitOfWork, TMapper, TDALEntity, TBLLEntity> : IBaseEntityService<TKey, TBLLEntity>
        where TKey : IEquatable<TKey>
        where TBLLEntity : class, IDomainEntity<TKey>, new()
        where TDALEntity : class, IDomainEntity<TKey>, new()
        where TUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker<TKey>
        where TServiceRepository : IBaseRepository<TKey, TDALEntity>
        where TMapper : IBaseBLLMapper<TDALEntity, TBLLEntity>
    {
        protected readonly TUnitOfWork UOW;
        protected readonly TMapper Mapper;
        protected readonly TServiceRepository Repository;

        public BaseEntityService(TUnitOfWork unitOfWork, TMapper mapper,
            TServiceRepository serviceRepository)
        {
            UOW = unitOfWork;
            Repository = serviceRepository;
            Mapper = mapper;
        }

        public virtual async Task<IEnumerable<TBLLEntity>> AllAsync(object? userId = null, bool noTracking = true)
        {
            var dalEntities = await Repository.AllAsync(userId, noTracking);
            var result = dalEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<TBLLEntity> FirstOrDefaultAsync(TKey id, object? userId = null,
            bool noTracking = true)
        {
            var dalEntity = await Repository.FirstOrDefaultAsync(id, userId, noTracking);
            var result = Mapper.Map(dalEntity);
            return result;
        }

        public virtual TBLLEntity Add(TBLLEntity entity)
        {
            var dalEntity = Mapper.Map(entity);
            var trackedDALEntity = Repository.Add(dalEntity);
            UOW.AddToEntityTracker(trackedDALEntity, entity);
            var result = Mapper.Map(trackedDALEntity);
            
            return result;
        }

        public virtual async Task<TBLLEntity> UpdateAsync(TBLLEntity entity, object? userId = null)
        {
            var dalEntity = Mapper.Map(entity);
            var resultDALEntity = await Repository.UpdateAsync(dalEntity, userId);
            var result = Mapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<TBLLEntity> RemoveAsync(TBLLEntity entity, object? userId = null)
        {
            var dalEntity = Mapper.Map(entity);
            var resultDALEntity = await Repository.RemoveAsync(dalEntity, userId);
            var result = Mapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<TBLLEntity> RemoveAsync(TKey id, object? userId = null)
        {
            var resultDALEntity = await Repository.RemoveAsync(id, userId);
            var result = Mapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<bool> ExistsAsync(TKey id, object? userId = null)
        {
            var result = await Repository.ExistsAsync(id, userId);
            return result;
        }
    }
}
