using Candidate.Identity.Dependancy;
using Candidate.Infrastructure.Dependancy;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyServices();

builder.Services.AddIdentityServices(configuration);

builder.Services.AddInfrastructureService(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
           //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins seperated with comma
           .SetIsOriginAllowed(origin => true)); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
