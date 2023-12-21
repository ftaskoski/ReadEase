using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using userController.Controllers;
using WebApplication1.Controllers;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

IApplicationBuilder applicationBuilder = app.UseRouting().UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Urls.Add("http://localhost:5000");

app.Run();

