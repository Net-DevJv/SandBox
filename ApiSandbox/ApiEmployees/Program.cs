using ApiEmployees.Utils;
using ApiEmployees.DataContext;
using Microsoft.EntityFrameworkCore;
using ApiEmployees.Service.EmployeesService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<DateTimeSchemaFilter>();
});

builder.Services.AddScoped<IEmployeesService, EmployeesService>();

builder.Services.AddDbContext<WebApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LoggingDatabase"));
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
});

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
