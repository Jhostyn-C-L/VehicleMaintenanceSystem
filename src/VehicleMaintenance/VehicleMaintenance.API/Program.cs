using Microsoft.EntityFrameworkCore;
using VehicleMaintenance.API.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VehicleMaintenanceDBContext>(options => 
{ options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();

    app.UseSwaggerUI();
}
app.UseSwaggerUI();

app.MapControllers();

app.Run();
