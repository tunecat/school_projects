using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.nipetu.webstore.Contracts.DAL.Base;


namespace DAL.Base
{
    public abstract class BaseUnitOfWork<TKey> : IBaseUnitOfWork, IBaseEntityTracker<TKey> 
        where TKey : IEquatable<TKey>
    {
        private readonly Dictionary<Type, object> _repoCache = new Dictionary<Type, object>();

        private readonly Dictionary<IDomainEntity<TKey>, IDomainEntity<TKey>> _entityTracker =
            new Dictionary<IDomainEntity<TKey>, IDomainEntity<TKey>>();

        // Factory method
        public TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
        {
            if (_repoCache.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository) repo;
            }

            repo = repoCreationMethod()!;
            _repoCache.Add(typeof(TRepository), repo);
            return (TRepository) repo;
        }
        public abstract Task<int> SaveChangesAsync();

        public void AddToEntityTracker(IDomainEntity<TKey> internalEntity, IDomainEntity<TKey> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }
        
        protected void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }
    }
}