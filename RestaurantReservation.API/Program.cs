using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.API.Authentication;
using RestaurantReservation.API.Common.MiddleWares;
using RestaurantReservation.API.Filters;
using RestaurantReservation.API.Models.Authentication;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Repositories;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    options.Filters.Add(typeof(ValidationFilter))
).AddNewtonsoftJson();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/restaurantReservation.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<RestaurantReservationDbContext>(
    dbContextOptions => dbContextOptions.UseSqlServer(
        builder.Configuration["ConnectionStrings:RestaurantReservationDb"])
);

builder.Services.AddSingleton(provider =>
{
    return new SigningConfiguration(builder.Configuration["JWT:Secret"]);

});
builder.Services.Configure<JwtConfiguration>(options => builder.Configuration.GetSection("JWT").Bind(options));
builder.Services.AddScoped<IJwtTokenService, JwtTokenGenerator>();

var jwtConfiguration = builder.Services.BuildServiceProvider().GetService<IOptions<JwtConfiguration>>().Value;
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfiguration.Issuer,
            ValidAudience = jwtConfiguration.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(jwtConfiguration.Secret))
        };
    });

builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<RestaurantRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<TableRepository>();
builder.Services.AddScoped<MenuItemRepository>();
builder.Services.AddScoped<OrderItemRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<ReservationRepository>();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1,0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
    options.ReportApiVersions = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
