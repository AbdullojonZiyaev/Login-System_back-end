using Microsoft.IdentityModel.Tokens;
using StorageAPI;
using StorageAPI.DBO;
using StorageAPI.Interfase;
using StorageAPI.Interfase.implementations;
using StorageAPI.Midleware;
using Microsoft.AspNetCore.Http;
using StorageAPI.Servises;
using StorageAPI.Servises.implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IGetUserInfo,GetUserInfoService>();
builder.Services.AddTransient<ILogin,LoginService>();
builder.Services.AddTransient<IRegister, RegisterService>();
builder.Services.AddTransient<IRefreshToken, RefreshTokenService>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "cors1",
      builder =>
      {
          builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200");
      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("cors1");
app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ErrorHandlerMidleware>();

app.Run();
