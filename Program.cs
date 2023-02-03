using ExtendFieldDemo.DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DefaultDbContext>((s, opt) =>
{
    opt.UseSqlite("Data Source=xxx.db");
    opt.ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactoryDesignTimeSupport>();
});

builder.Services.AddDbContext<ExtendFieldDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

ExtendFieldDbContext? service = app.Services.CreateScope().ServiceProvider.GetService<ExtendFieldDbContext>();
var fieldInfos = service.ExtendFieldModels
    .ToList()
    .GroupBy(o => o.TableName);
foreach (var item in fieldInfos)
{
    ExtendFieldDbContext.ExtendInfo.TryAdd(item.Key!, item.Select(o => o).ToList());
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
