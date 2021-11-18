using MerchandiseService.Domain.Models;
using MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MerchandiseService.Infrastructure.Repositories.Infrastructure.Implementation
{
    internal class ChangeTracker : IChangeTracker
    {
        private readonly ConcurrentBag<Entity> _entities;

        public ChangeTracker()
        {
            _entities = new ConcurrentBag<Entity>();
        }

        public IEnumerable<Entity> TrackedEntities => _entities.ToArray();

        public void Track(Entity entity)
        {
            _entities.Add(entity);
        }
    }
}
