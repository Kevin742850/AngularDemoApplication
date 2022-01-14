using PharmacyManagementSystem;
using PharmacyManagementSystem.Contracts;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Implementation;
using PharmacyManagementSystem.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
string angularAppURL = "http://localhost:4200";
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});


builder.Services.Configure<IISServerOptions>(options =>
{
	options.AllowSynchronousIO = true;
});
// Add services to the container.
builder.Services.AddMvc(options =>
{
    //options.Filters.Add(new ValidateModelStateAttribute());
    //options.Filters.Add(new ValidateIPAddressAttribute());
}).AddJsonOptions(options =>
{
    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();

});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(typeof(ILog<>),typeof(MMSLog<>));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
string JWTTokenKey=builder.Configuration.GetValue<string>("AppSettings:JwtSecret");
var key = Encoding.ASCII.GetBytes(JWTTokenKey);
var encKey = Encoding.ASCII.GetBytes(JWTTokenKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        TokenDecryptionKey = new SymmetricSecurityKey(encKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
//builder.Services.AddOpenIddict()
//    .AddServer(options =>
//    {
//        options.DisableAccessTokenEncryption();
//    });
//builder.Services.AddSwaggerGen();


builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please add bearer token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
         new OpenApiSecurityScheme{
         Reference=new OpenApiReference{
         Type=ReferenceType.SecurityScheme,
         Id="Bearer"
         }
         },
         new string[]{ }

        }
    }
    );
});
builder.Services.AddDbContext<DataContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{

    options.AddPolicy(name: myAllowSpecificOrigins, builder =>
    {
        builder.WithOrigins(angularAppURL).AllowAnyMethod().AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);

 
app.UseMiddleware<LoggerMiddleware>();
app.UseAuthentication();
app.MapControllers();
app.UseAuthorization();
app.Run();
