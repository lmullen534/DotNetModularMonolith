using ECommerce.Common.Interfaces;
using ECommerce.Modules.Customers.Domain;

namespace ECommerce.Modules.Customers.Services;

public class CustomerService : ICustomerService
{
  private readonly IRepository<Customer> _customerRepository;

  public CustomerService(IRepository<Customer> customerRepository)
  {
    _customerRepository = customerRepository;
  }

  public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
  {
    return await _customerRepository.GetByIdAsync(customerId);
  }

  public async Task<List<Customer>> GetAllCustomersAsync()
  {
    return await _customerRepository.GetAllAsync();
  }

  public async Task AddCustomerAsync(Customer customer)
  {
    await _customerRepository.AddAsync(customer);
  }
}
