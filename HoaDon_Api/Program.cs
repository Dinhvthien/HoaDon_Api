using HoaDon_Api.PayLoads.Converters;
using HoaDon_Api.PayLoads.DataResponses;
using HoaDon_Api.PayLoads.Responses;
using HoaDon_Api.Services.Implements;
using HoaDon_Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IHoaDonService, HoaDonService>();
builder.Services.AddSingleton<HoaDonConverter>();
builder.Services.AddSingleton<ResponseObject<DataResponseHoaDon>>();
builder.Services.AddSingleton<ResponseList<DataResponseHoaDon>>();


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
