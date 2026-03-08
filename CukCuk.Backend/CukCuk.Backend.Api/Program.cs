using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Entities;
using CukCuk.Backend.Core.Interfaces.Database;
using CukCuk.Backend.Core.Interfaces.File;
using CukCuk.Backend.Core.Interfaces.Repository;
using CukCuk.Backend.Core.Interfaces.Service;
using CukCuk.Backend.Core.Middlewares;
using CukCuk.Backend.Core.Services;
using CukCuk.Backend.Core.Validators;
using CukCuk.Backend.Infrastructure.Database;
using CukCuk.Backend.Infrastructure.File;
using CukCuk.Backend.Infrastructure.Repository;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure database options
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("ConnectionStrings"));


// đăng ký DI
// database
builder.Services.AddScoped<IDbConnectionFactory, ConnectionFactory>();

// inventory item
builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
builder.Services.AddScoped<IInventoryItemService, InventoryItemService>();

// inventory item category
builder.Services.AddScoped<IInventoryItemCategoryRepository, InventoryItemCategoryRepository>();
builder.Services.AddScoped<IInventoryItemCategoryService, InventoryItemCategoryService>();

// unit
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IUnitService, UnitService>();

// inventory item addition
builder.Services.AddScoped<IInventoryItemAdditionRepository, InventoryItemAdditionRepository>();
builder.Services.AddScoped<IInventoryItemAdditionService, InventoryItemAdditionService>();

// kitchen
builder.Services.AddScoped<IKitchenRepository, KitchenRepository>();
builder.Services.AddScoped<IKitchenService, KitchenService>();

// inventory item type
builder.Services.AddScoped<IInventoryItemTypeRepository, InventoryItemTypeRepository>();
builder.Services.AddScoped<IInventoryItemTypeService, InventoryItemTypeService>();


// validator cho DTO
builder.Services.AddScoped<IValidator<InventoryItemRequestDTO>, InventoryItemDtoValidator>();
builder.Services.AddScoped<IValidator<UnitDTO>, UnitDtoValidator>();
builder.Services.AddScoped<IValidator<InventoryItemAdditionDTO>, InventoryItemAdditionDtoValidator>();
builder.Services.AddScoped<IValidator<InventoryItemCategoryDTO>, InventoryItemCategoryDtoValidator>();
builder.Services.AddScoped<IValidator<InventoryItemTypeDTO>, InventoryItemTypeDtoValidator>();
builder.Services.AddScoped<IValidator<KitchenDTO>, KitchenDtoValidator>();

// lưu trữ file
builder.Services.AddSingleton<IFileStorageService>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    return new LocalFileStorageService(env.WebRootPath);
});

// cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:8080") // Điền port Frontend của bạn vào đây
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// middlewares
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

app.UseMiddleware<ExceptionMiddleware>();

// truy cập file tĩnh
app.UseStaticFiles();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
