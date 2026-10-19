# Mini CRM v2

Mini CRM is a console-based customer relationship management application built with C#, .NET 10, Entity Framework Core and SQL Server.

The project was created as a practical exercise in building a data-driven application with Entity Framework Core, asynchronous programming and relational databases.

## Features

### Customer Management
- Add customers
- Get customer by ID
- List customers
- Update customers
- Delete customers
- Prevent duplicate email addresses
- Search customers by name, email or city
- Paginate customer results

### Order Management
- Add orders to customers
- Get order by ID
- Get all orders for a customer
- Update orders
- Delete orders
- Customer-to-Order one-to-many relationship

### Data Access
- Entity Framework Core
- SQL Server
- EF Core migrations
- LINQ queries
- Change tracking
- `AsNoTracking()` for read-only queries
- Eager loading with `Include()`
- Async database operations
- `CancellationToken`
- Generic `PagedResult<T>`

## Tech Stack

- C#
- .NET 10
- Entity Framework Core
- SQL Server
- LINQ
- Docker
- Git

## Project Structure

```text
MiniCRMv2
├