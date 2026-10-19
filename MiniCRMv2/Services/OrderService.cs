using Microsoft.EntityFrameworkCore;
using MiniCRMv2.Data;
using MiniCRMv2.Entities;

namespace MiniCRMv2.Services;

public class OrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddOrderAsync(
        int customerId,
        Order order,
        CancellationToken token)
    {
        if (!await _context.Customers.AnyAsync(c => c.Id == customerId, token))
        {
            return false;
        }
        order.CustomerId = customerId;
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(token);
        return true;
    }
    
    public async Task<List<Order>> GetCustomerOrdersAsync(
        int customerId,
        CancellationToken token)
    {
        List<Order> orders = await _context.Orders.Where(o => o.CustomerId == customerId).AsNoTracking().OrderByDescending(o => o.OrderDate).ToListAsync(token);
        return orders;
    }
    
    public async Task<Order?> GetOrderByIdAsync(
        int orderId,
        CancellationToken token)
    {
        Order? order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == orderId, token);
        return order;
    }
    
    public async Task<bool> UpdateOrderAsync(
        int orderId,
        string productName,
        decimal amount,
        DateTime orderDate,
        CancellationToken token)
    {
        Order? order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId, token);
        if (order == null)
        {
            return false;
        }

        order.ProductName = productName;
        order.Amount = amount;
        order.OrderDate = orderDate;
        await _context.SaveChangesAsync(token);
        return true;
    }
    
    public async Task<bool> DeleteOrderAsync(
        int orderId,
        CancellationToken token)
    {
        Order? order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId,token);
        if (order is null)
        {
            return false;
        }
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(token);
        return true;
    }
}