

using Microsoft.EntityFrameworkCore;
using MiniCRMv2.Data;
using MiniCRMv2.Entities;
using MiniCRMv2.Services;

using AppDbContext context = new AppDbContext();

CustomerService customerService = new CustomerService(context);
OrderService orderService = new OrderService(context);

Customer customer = new Customer
{
    Name = "Anna",
    Email = "anna@test.pl",
    Age = 32,
    City = "Bydgoszcz"
};

bool added = await customerService.AddCustomerAsync(customer, CancellationToken.None);
Customer? anna = await context.Customers
    .FirstOrDefaultAsync(c => c.Email =="anna@test.pl",
        CancellationToken.None);
if (anna is not null)
{
    Order order = new Order()
        {
            ProductName = "Laptop",
            Amount = 4500,
            OrderDate = DateTime.Now
        };
    bool orderAdded = await orderService.AddOrderAsync(anna.Id, order, CancellationToken.None);
    if (orderAdded)
    {
        Console.WriteLine("Dodano zamówienie");
    }
    else
    {
        Console.WriteLine("Nie udało się dodawać zamówienia");
    }
}
if (added)
{
    Console.WriteLine("Dodano klienta");
}
else
{
    Console.WriteLine("Klient z takim e-mailem już istnieje");
}

Customer? customerWithOrders =
    await customerService.GetCustomerWithOrdersAsync(
        anna.Id,
        CancellationToken.None);

if (customerWithOrders is not null)
{
    Console.WriteLine($"Klient: {anna.Name}");
    foreach (Order order in customerWithOrders.Orders)
    {
        Console.WriteLine($"Zamówienie: {order.ProductName}, {order.Amount}, {order.OrderDate}");
    }
}
























