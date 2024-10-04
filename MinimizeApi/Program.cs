using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinimizeApi.AutoMapper;
using MinimizeApi.Data;
using MinimizeApi.Models.Dtos;
using MinimizeApi.Repositories;
using MinimizeApi.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAcountService, AccountService>();   

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Use the appropriate key from configuration

        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API with JWT Auth",
        Description = "Sample API using Swagger with JWT Authentication",
        TermsOfService = new Uri("https://example.com/terms"),
    });

    // Add JWT Authentication to Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        //In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>() // new string[] {} is also valid here
    }
});
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
        

    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();



//Authentication
app.MapPost("/register", async (RegisterDTO registerDTO, IAcountService acountService) =>
{
    return Results.Ok(await acountService.Register(registerDTO));
}).AllowAnonymous();



app.MapPost("/login", async (LoginDTO loginDTO, IAcountService acountService) =>
{
    return Results.Ok(await acountService.Login(loginDTO));
}).AllowAnonymous();


app.MapGet("/GetProducts", async (IProductService productService) =>
{
    return Results.Ok(await productService.GetAll());
}).RequireAuthorization();

app.MapGet("/GetProduct/{id:int}", async (IProductService productService, int id) =>
{
    return Results.Ok(await productService.GetById(id));
}).RequireAuthorization();

app.MapPost("/AddProduct", async (AddRequestDTO addRequestDTO, IProductService productService) =>
{
    return Results.Ok(await productService.Add(addRequestDTO));
}).RequireAuthorization();


app.MapPost("/UpdateProduct/{id:int}", async (UpdateRequestDTO updateRequestDTO, IProductService productService, int id) =>
{
    return Results.Ok(await productService.Update(updateRequestDTO, id));
});

app.MapDelete("/deleteProduct/{id:int}", async (IProductService productService, int id) => 
{
    return Results.Ok(await productService.Delete(id));
}).RequireAuthorization();


app.Run();