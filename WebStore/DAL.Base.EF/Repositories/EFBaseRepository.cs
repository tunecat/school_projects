using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.DAL.Base;
using Contracts.DAL.Base.Mappers;
using com.nipetu.webstore.Contracts.DAL.Base.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class EFBaseRepository<TDbContext, TDomainEntity, TDALEntity, TUser> : 
    EFBaseRepository<Guid, TDbContext, TDomainEntity, TDALEntity, TUser>
        where TUser : IdentityUser<Guid>
        where TDomainEntity : class, IDomainEntity<Guid>, new()
        where TDbContext : DbContext, IBaseEntityTracker
        where TDALEntity : class, IDomainEntity<Guid>, new()
    {
        public EFBaseRepository(TDbContext dbContext,  IBaseDALMapper<TDomainEntity, TDALEntity> mapper) : base(dbContext, mapper)
        {
        }
    }
    
    public class EFBaseRepository<TKey, TDbContext, TDomainEntity, TDALEntity, TUser> : IBaseRepository<TKey, TDALEntity>
        where TDomainEntity : class, IDomainEntity<TKey>, new()
        where TKey : IEquatable<TKey>, IComparable
        where TDbContext: DbContext, IBaseEntityTracker<TKey>
        where TDALEntity : class, IDomainEntity<TKey>, new()
        where TUser : IdentityUser<TKey>
    {
        protected readonly TDbContext RepoDbContext;
        protected readonly DbSet<TDomainEntity> RepoDbSet;
        protected readonly IBaseDALMapper<TDomainEntity, TDALEntity> Mapper;
        public EFBaseRepository(TDbContext dbContext ,IBaseDALMapper<TDomainEntity, TDALEntity> mapper)
        {
            RepoDbContext = dbContext;
            Mapper = mapper;
            RepoDbSet = RepoDbContext.Set<TDomainEntity>();
            if (RepoDbSet == null)
            {
               throw new ArgumentNullException(typeof(TDomainEntity).Name + " was not found as DBSet!");
            }
        }
        
        
        
        public virtual async Task<IEnumerable<TDALEntity>> AllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<TDALEntity> FirstOrDefaultAsync(TKey id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking); 
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
   
        public virtual TDALEntity Add(TDALEntity entity)
        {
            var domainEntity = Mapper.Map(entity);
            var trackedDomainEntity = RepoDbSet.Add(domainEntity).Entity;
            RepoDbContext.AddToEntityTracker(trackedDomainEntity, entity);
            return Mapper.Map(trackedDomainEntity);
        }

#pragma warning disable 1998
        public virtual async Task<TDALEntity> UpdateAsync(TDALEntity entity, object? userId = null)
#pragma warning restore 1998
        {
            return Mapper.Map(RepoDbSet.Update(Mapper.Map(entity)).Entity);
        }

        public virtual async Task<TDALEntity> RemoveAsync(TDALEntity entity, object? userId = null)
        {
            var domainEntity = Mapper.Map(entity);
            await CheckDomainEntityOwnership(domainEntity, userId);
            return Mapper.Map(RepoDbSet.Remove(domainEntity).Entity);
        }

        public virtual async Task<TDALEntity> RemoveAsync(TKey id, object? userId = null)
        {
            var query = PrepareQuery(userId, true);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (domainEntity == null)
            {
                throw new ArgumentException("Entity to be updated was not found in data source!");
            }
            return Mapper.Map(RepoDbSet.Remove(domainEntity).Entity);
            
        }

        public virtual async Task<bool> ExistsAsync(TKey id, object? userId = null)
        {
            var query = PrepareQuery(userId, true);
            var recordExists = await query.AnyAsync(e => e.Id.Equals(id));
            return recordExists;
        }
        
        
        protected async Task CheckDomainEntityOwnership(TDomainEntity entity, object? userId = null)
        {
            var recordExists = await ExistsAsync(entity.Id, userId);
            if (!recordExists)
            {
                throw new ArgumentException("Entity to be updated was not found in data source!");
            }
        }
        
        protected IQueryable<TDomainEntity> PrepareQuery(object? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            // Shall we disable entity tracking
            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            
            // userId != null and is this entity implementing IDomainEntityUser
            if (userId != null && typeof(IDomainEntityUser<TKey, TUser>).IsAssignableFrom(typeof(TDomainEntity)))
            {
                // accessing TDomainEntity.AppUserId via shadow property access
                query = query.Where(e =>
                    Microsoft.EntityFrameworkCore.EF.Property<TKey>(e, nameof(IDomainEntityUser<TKey, TUser>.UserId))
                        .Equals((TKey) userId));
            }

            return query;
        }
        
        
        
    }
    
}