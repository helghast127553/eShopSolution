using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Utilities.Constants;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<EShopDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstants.MAIN_CONNECTION_STRING)));

builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddTransient<IPublicProductService, PublicProductService>();
builder.Services.AddTransient<IManageProductService, ManageProductService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
