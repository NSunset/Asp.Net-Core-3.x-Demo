using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain
{
    public abstract class AggregateRoot<T>
    {
        public T Id { get; set; }
    }
}
