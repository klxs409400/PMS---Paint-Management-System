using Microsoft.EntityFrameworkCore;
using PaintStore.API.Database;
using PaintStore.API.Repositories;
using PaintStore.API.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<PaintStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<UserRepository>(); // 和数据库打交道
builder.Services.AddScoped<PaintProductRepository>(); // 和数据库打交道
builder.Services.AddScoped<OrderRepository>(); // 和数据库打交道
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PaintProductService>();
builder.Services.AddScoped<OrderService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
