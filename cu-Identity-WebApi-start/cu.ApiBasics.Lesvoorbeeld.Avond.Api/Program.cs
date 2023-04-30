using cu.ApiBAsics.Lesvoorbeeld.Avond.Core.Entities;
using cu.ApiBAsics.Lesvoorbeeld.Avond.Core.Interfaces.Repositories;
using cu.ApiBAsics.Lesvoorbeeld.Avond.Core.Interfaces.Services;
using cu.ApiBAsics.Lesvoorbeeld.Avond.Core.Services;
using cu.ApiBasics.Lesvoorbeeld.Avond.Infrastructure.Data;
using cu.ApiBasics.Lesvoorbeeld.Avond.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//configure identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options =>
    {
        options.SignIn.RequireConfirmedEmail = false;//only for development!
                                                     //only for development
                                                     //set some flexible password rules
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredLength = 3;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDatabase")));
//allow cors
builder.Services.AddCors(options =>
options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
}));
//add authentication and authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["JWTConfiguration:Issuer"],
        ValidAudience = builder.Configuration["JWTConfiguration:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["JWTConfiguration:SigninKey"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "admin");
    });
    //must be admin or customer
    options.AddPolicy("customer", policy =>
    {
        policy.RequireAssertion(context =>
        {
            if (context.User.HasClaim(ClaimTypes.Role, "admin")
            || context.User.HasClaim(ClaimTypes.Role, "customer"))
            {
                return true;
            }
            return false;
        });
    });
});

//repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
//services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IImageService, ImageService>();
//swagger
builder.Services.AddSwaggerGen(c => {
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter the token below:"
        });
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
                                 new string[] {}
                         }
                    });
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "cu.ApiBasics.Lesvoorbeeld.Avond.Api", Version = "v1" });
    }
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
