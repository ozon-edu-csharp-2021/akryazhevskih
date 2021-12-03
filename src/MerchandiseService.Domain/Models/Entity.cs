using System;
using System.Collections.Generic;
using MediatR;

namespace MerchandiseService.Domain.Models
{
    public class Entity
    {
        private int? _requestedHashCode;
        private List<INotification> _domainEvents;

        public virtual long Id { get; protected set; }

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public static bool operator ==(Entity left, Entity right)
        {
            if (object.Equals(left, null))
                return object.Equals(right, null) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return this.Id == default(long);
        }

        public override bool Equals(object obj)
        {
            if (obj is not Entity entity)
                return false;

            if (object.ReferenceEquals(this, entity))
                return true;

            if (this.GetType() != entity.GetType())
                return false;

            if (entity.IsTransient() || this.IsTransient())
                return false;
            else
                return entity.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = HashCode.Combine(Id, 31);

                return _requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }
    }
}