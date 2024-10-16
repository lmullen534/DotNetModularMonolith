// ECommerce.Common/Domain/Entity.cs
namespace ECommerce.Common.Domain;

public abstract class Entity
{
  public Guid Id { get; protected set; }
}
