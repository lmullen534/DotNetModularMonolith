namespace ECommerce.Modules.Orders.Result;

public class OrderResult
{
  public bool Success { get; set; }
  public string Message { get; set; } = string.Empty; // Message for success or error
  public string ErrorCode { get; set; } = string.Empty; // Optional error code for specific failures
  public Guid? OrderId { get; set; } // Optional ID of the created order
}

