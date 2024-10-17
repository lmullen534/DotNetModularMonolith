using ECommerce.Modules.Orders;
using ECommerce.Modules.Customers;
using ECommerce.Modules.Products;
using ECommerce.Modules.Orders.Endpoints;
using ECommerce.Modules.Customers.Endpoints;
using ECommerce.Modules.Products.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "E-Commerce Modular Monolith", Version = "v1" }));

// Register services
builder.Services.AddOrderModule(builder.Configuration);
builder.Services.AddCustomerModule(builder.Configuration);
builder.Services.AddProductModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "E-Commerce Modular Monolith with .NET 8!").WithTags("Home").WithName("Home");
app.MapOrderEndpoints();
app.MapCustomerEndpoints();
app.MapProductEndpoints();

app.Run();
