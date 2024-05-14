using Datos.Models;
using Microsoft.EntityFrameworkCore;
using Negocios.Clases;
using Negocios.Interfaces;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuracion cadena de conexion
builder.Services.AddDbContext<PosDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSql")));

//Configurar las interfaces para que el controlador las pueda exponer
builder.Services.AddScoped<ICliente, LogicaCliente>();
builder.Services.AddScoped<IEmpresa, LogicaEmpresa>();
builder.Services.AddScoped<IEmpleado, LogicaEmpleado>();
builder.Services.AddScoped<IProducto, LogicaProducto>();
builder.Services.AddScoped<ISucursal, LogicaSucursal>();
builder.Services.AddScoped<IVenta, LogicaVenta>();
builder.Services.AddScoped<IVentaDetalle, LogicaVentaDetalle>();


////evita las referencias ciclicas el codigo de abajo
//builder.Services.AddControllersWithViews().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//    // Otras opciones...
//});

//politicas de dominio
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaCliente",
        policy =>
        {
            //policy.WithOrigins("http://192.168.100.18") // Agrega tu dirección IP fija aquí
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//agrega politicas CORS
app.UseCors("PoliticaCliente");

app.UseAuthorization();

app.MapControllers();

app.Run();
