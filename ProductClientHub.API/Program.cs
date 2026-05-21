using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Filters;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.Delete;
using ProductClientHub.API.UseCases.Clients.GetAll;
using ProductClientHub.API.UseCases.Clients.GetById;
using ProductClientHub.API.UseCases.Clients.Register;
using ProductClientHub.API.UseCases.Clients.Update;
using ProductClientHub.API.UseCases.Products.Delete;
using ProductClientHub.API.UseCases.Products.Register;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.API.UseCases.Products.SharedValidator;
using FluentValidation;
using ProductClientHub.Communication.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProductClientHub.API.Infrastructure.Security;
using ProductClientHub.API.UseCases.Login;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddDbContext<ProductClientHubDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var signingKey = builder.Configuration.GetValue<string>("Settings:Jwt:SigningKey");
var expirationTime = builder.Configuration.GetValue<double>("Settings:Jwt:ExpirationTimeInMinutes");

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!))
    };
});

builder.Services.AddScoped<TokenController>(config => new TokenController(expirationTime, signingKey!));

// Validators
builder.Services.AddScoped<IValidator<RequestClientJson>, RequestClientValidator>();
builder.Services.AddScoped<IValidator<RequestProductJson>, RequestProductValidator>();

// Use Cases
builder.Services.AddScoped<DoLoginUseCase>();
builder.Services.AddScoped<RegisterClientUseCase>();
builder.Services.AddScoped<UpdateClientUseCase>();
builder.Services.AddScoped<GetAllClientsUseCase>();
builder.Services.AddScoped<GetClientByIdUseCase>();
builder.Services.AddScoped<DeleteClientUseCase>();

builder.Services.AddScoped<RegisterProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
