using ECommerce.Modules.Orders.Services;
using ECommerce.Modules.Products.Services;
using ECommerce.Modules.Customers.Services;
using ECommerce.Common.Interfaces;
using ECommerce.Common.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Modules.Orders.Domain;
using ECommerce.Modules.Products.Domain;
using ECommerce.Modules.Customers.Domain;
using ECommerce.Modules.Orders;
using ECommerce.Modules.Customers;
using ECommerce.Modules.Products;
using ECommerce.Modules.Orders.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddOrderModule(builder.Configuration);
builder.Services.AddCustomerModule(builder.Configuration);
builder.Services.AddProductModule(builder.Configuration);

// Use in-memory repository (for simplicity)
builder.Services.AddSingleton(typeof(IRepository<>), typeof(InMemoryRepository<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "E-Commerce Modular Monolith with .NET 8!");
app.MapOrderEndpoints();

app.MapPost("/products", async (IProductService productService,
    Product product, ILogger<Program> logger) =>
{
    logger.LogInformation("Creating products");
    await productService.AddProductAsync(product);
    logger.LogInformation("Product created");

    return Results.Ok("Product created!");
});

app.MapGet("/products", async (IProductService productService, ILogger<Program> logger) =>
{
    var products = await productService.GetAllProductsAsync();
    logger.LogInformation($"Number of products to be returned: {products.Count}");
    return Results.Ok(products);
});

app.MapGet("/products/{id}", async (IProductService productService, Guid id) =>
{
    var product = await productService.GetProductByIdAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.MapPost("/customers", async (ICustomerService customerService,
    Customer customer, ILogger<Program> logger) =>
{
    logger.LogInformation("Creating customer");
    await customerService.AddCustomerAsync(customer);
    logger.LogInformation("Customer created");

    return Results.Ok("Customer created!");
});

app.MapGet("/customers", async (ICustomerService customerService, ILogger<Program> logger) =>
{
    var customers = await customerService.GetAllCustomersAsync();
    logger.LogInformation($"Number of customers to be returned: {customers.Count}");
    return Results.Ok(customers);
});

app.MapGet("/customers/{id}", async (ICustomerService customerService, Guid id) =>
{
    var customer = await customerService.GetCustomerByIdAsync(id);
    return customer is not null ? Results.Ok(customer) : Results.NotFound();
});

app.Run();
