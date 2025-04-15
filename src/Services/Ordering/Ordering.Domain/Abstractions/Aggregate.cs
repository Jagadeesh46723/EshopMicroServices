using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions
{
    public class Aggregate<T> : Entity<T>, IAggregate<T>
    {
        private readonly List<IDomainevent> _domainEvents = [];

        public IReadOnlyList<IDomainevent> Domainevents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainevent domainevent)
        {
            _domainEvents.Add(domainevent);
        }
        public IList<IDomainevent> ClearDomainevents()
        {
            var dequeuedItems = _domainEvents.ToList();
            _domainEvents.Clear();
            return dequeuedItems;
        }
    }
}
