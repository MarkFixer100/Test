using Application;
using Application.interfaces;
using Application.Use_Case;
using Domain.IReposotory;
using Infostructure;
using Infostructure.Data;
using Infostructure.PerfumeRepository;
using Infostructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var jwtSecretKey =  builder.Configuration["JwtOptions:SecretKey"];

var jwtIssuer =  builder.Configuration["JwtOptions:Issuer"];

var jwtAudience =  builder.Configuration["JwtOptions:Audience"];

var ExpTime = builder.Configuration.GetValue<int>("JwtOptions:ExpirationMinutes", 60);




builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IReposotoryPerfume , PerfumeRepository>();
builder.Services.AddScoped<PerfumeCase>();
builder.Services.AddScoped<IStudentRepository , StudentRepository>();
builder.Services.AddScoped<StudentCase>();
builder.Services.AddScoped<IProducts , ProductRepository>();
builder.Services.AddScoped<ProductCase>();
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<CategoryCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtService>();
builder.Services.Configure<JwtOptions>(options =>
{
    options.SecretKey = jwtSecretKey ?? "ключь null";
    options.Issuer = jwtIssuer ?? "Издатель Null";
    options.Audience = jwtAudience ?? string.Empty;
    options.ExpirationMinutes = ExpTime;
});
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,           // Add this
        ValidAudience = jwtAudience,       // Add this
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
    };

});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConection") , b => b.MigrationsAssembly("Infostructure"));
});
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
            
    });
});
// Learn more about configuring  https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
