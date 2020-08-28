using System;

namespace Common.Domain
{
    public abstract class DataEntity<T> : AggregateRoot<T>
    {
    }

    public abstract class PrimaryKeyEntity<T> : DataEntity<T>
    {
    }

    public abstract class DomainEntity : PrimaryKeyEntity<int>
    {

    }

    public abstract class GeneralEntity : DomainEntity
    {
        public DateTime CreateTime { get; set; }

    }
}
