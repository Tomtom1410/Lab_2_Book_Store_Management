using BookStoresAPI.DataAccess;
using BookStoresAPI.DataAccess.Interface;
using BookStoresAPI.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookStoreContext>(option => option.UseInMemoryDatabase("BookLists"));
builder.Services.AddControllers().AddOData(option => option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));
builder.Services.AddTransient<IBookRepositories, BookRepositoties>();
builder.Services.AddTransient<IPressRepositories, PressRepositories>();
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

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    //var entityBookSet = builder.EntitySet<Book>("Books");
    //entityBookSet.EntityType.HasKey(entity => entity.Id);
    builder.EntitySet<Press>("Press");
    builder.EntitySet<Book>("Books");
    return builder.GetEdmModel();
}


