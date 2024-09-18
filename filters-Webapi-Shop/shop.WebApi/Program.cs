using shop.WebApi.Common;
using shop.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(CustomExceptionFilter));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;
services.AddSingleton<IProductService, ProductService>();

services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
