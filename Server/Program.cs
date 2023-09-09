using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Repositories;
using System.Text;

#region Services

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();

#region Context

builder.Services.AddDbContext<MyApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#endregion \Context

#region Repositories

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

#endregion \Repositories

#region JWT

builder.Services.AddAuthentication("Bearer").AddJwtBearer(option =>
{
    option.TokenValidationParameters = new()
    {
        ValidateIssuer=true,
        ValidateAudience=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"])),
    };
});

#endregion \JWT

#endregion \Services


#region Ppipeline

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
{
    policy.WithOrigins("https://localhost:7063/", "http://localhost:7063/")
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType);
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion Ppipeline