using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddAuthorization(options =>
      {
        options.AddPolicy("WriteAccess", policy => policy.RequireClaim("permissions", "create:term", "update:term"));
        options.AddPolicy("DeleteAccess", policy => policy.RequireClaim("permissions", "delete:term"));
      });

builder.Services.AddControllers();
builder.Services.AddSwaggerService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
