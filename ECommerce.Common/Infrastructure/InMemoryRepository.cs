using ECommerce.Common.Domain;
using ECommerce.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace ECommerce.Common.Infrastructure
{
  public class InMemoryRepository<T> : IRepository<T> where T : Entity
  {
    private  List<T> _entities = [];
    private readonly ILogger<InMemoryRepository<T>> _logger;

    public InMemoryRepository(ILogger<InMemoryRepository<T>> logger)
    {
      _logger = logger;
    }

    public Task<T> GetByIdAsync(Guid id) => Task.FromResult(_entities.FirstOrDefault(e => e.Id == id));

    public Task AddAsync(T entity)
    {
      _logger.LogInformation($"Adding entity of type {typeof(T).Name} with ID {entity.Id}");
      _entities.Add(entity);
      _logger.LogInformation($"number of entities: {_entities.Count}");
      return Task.CompletedTask;
    }

    public Task SaveChangesAsync() => Task.CompletedTask;

    public Task<List<T>> GetAllAsync()
    {
      
      return Task.FromResult(_entities);
    }

  }
}
