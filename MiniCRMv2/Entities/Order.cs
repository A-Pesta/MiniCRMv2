namespace MiniCRMv2.Entities;

public class Order
 {
     public int Id { get; set; }
     public string ProductName { get; set; }
     public decimal Amount { get; set; }
     public DateTime OrderDate { get; set; }
     public int CustomerId { get; set; }
     public Customer Customer { get; set; }
 }