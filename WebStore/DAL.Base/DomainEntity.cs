using System;
using com.nipetu.webstore.Contracts.DAL.Base;


namespace DAL.Base
{
    public class DomainEntity :  DomainEntity<Guid>, IDomainEntity
    {
    }

    public class DomainEntity<TKey> :  IDomainEntity<TKey> 
        where TKey : IEquatable<TKey>
    {
        //[Key]
        public virtual TKey Id { get; set; } = default!;
        /*
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string? ChangedBy { get; set; }
        public virtual DateTime ChangedAt { get; set; }
        */
    }

}