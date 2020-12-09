﻿﻿using System;
using com.nipetu.webstore.Contracts.DAL.Base;


namespace DAL.Base
{
    public abstract class DomainBaseEntity : IDomainBaseEntity
    {
        public virtual Guid Id { get; set; } = default!;
    }
}