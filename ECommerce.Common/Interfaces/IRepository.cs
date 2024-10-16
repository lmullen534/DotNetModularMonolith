using ECommerce.Common.Domain;

namespace ECommerce.Common.Interfaces;

public interface IRepository<T> where T : Entity
{
  Task<T> GetByIdAsync(Guid id);
  Task AddAsync(T entity);
  Task SaveChangesAsync();
  Task<List<T>> GetAllAsync();
}
