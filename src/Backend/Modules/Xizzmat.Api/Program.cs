using Xizzmat.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddExternalServices(builder.Configuration);

var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
