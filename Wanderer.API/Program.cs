using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Wanderer.API.Config;
using Wanderer.API.Middlewares;
using Wanderer.Application;
using Wanderer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("firebase_key.json"),
});

builder.Services.AddCustomAuthorization(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .WithMethods("POST")
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<FirebaseAuthenticationMiddleware>();

app.MapControllers();

app.Run();
