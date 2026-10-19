using MiniCRMv2.Data;
using Microsoft.EntityFrameworkCore;
using MiniCRMv2.Entities;
using MiniCRMv2.Models;

namespace MiniCRMv2.Services;

public class CustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddCustomerAsync(
        Customer customer,
        CancellationToken token)
    {
        if (await _context.Customers.AnyAsync(c => c.Email == customer.Email, token))
        {
            return false;
        }
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(token);
        return true;
    }
    
    public async Task<Customer?> GetCustomerByIdAsync(
        int customerId,
        CancellationToken token)
    {
        Customer? customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == customerId, token);
        return customer;
    }
    
    public async Task<List<Customer>> GetCustomersAsync(
        CancellationToken token)
    {
        List<Customer> customer = await _context.Customers
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(token);
        return customer;
    }
    
    public async Task<bool> UpdateCustomerAsync(
        int customerId,
        string name,
        string email,
        int age,
        string city,
        CancellationToken token)
    {
        Customer? customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId, token);
        if (customer is null)
        {
            return false;
        }
        customer.Name = name;
        customer.Email = email;
        customer.Age = age;
        customer.City = city;  
        await _context.SaveChangesAsync(token);
        return true;
    }
    
    public async Task<bool> DeleteCustomerAsync(
        int customerId,
        CancellationToken token)
    {
        Customer? customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId, token);
        if (customer is null)
        {
            return false;
        }
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(token);
        return true;
    }
    
    public async Task<Customer?> GetCustomerWithOrdersAsync(
        int customerId,
        CancellationToken token)
    {
        return await _context.Customers.AsNoTracking().Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == customerId, token);
    }
    
    public async Task<PagedResult<Customer>> SearchCustomersAsync(
        string? searchText,
        int pageNumber,
        int pageSize,
        CancellationToken token)
    {
        IQueryable<Customer> query = _context.Customers
            .AsNoTracking();
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query
                .Where(c => c.Name.Contains(searchText) || c.Email.Contains(searchText) || c.City.Contains(searchText));
        }
        int totalCount = await query.CountAsync(token);
        List<Customer> customers = await query
            .OrderBy(c => c.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);
        return new PagedResult<Customer>
        {
            Items = customers,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}




























