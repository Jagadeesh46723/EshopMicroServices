using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions
{
    public interface IDomainevent : INotification
    {
        Guid Eventid => Guid.NewGuid();

        public DateTime OccuredOn => DateTime.Now;

        public string EventType => GetType().AssemblyQualifiedName ?? string.Empty;
    }
}
