using System.Text;
using System.Text.Json.Serialization;
using BankingApp.DTOs;
using BankingApp.Models;
using BankingApp.Repositories;
using BankingApp.Repository;
using BankingApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;


namespace BankingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.MaxDepth = 64;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Database
            builder.Services.AddDbContext<BankingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IBankRepository, BankRepository>();
            builder.Services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<ISalaryDisbursementRepository, SalaryDisbursementRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            //builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

            // Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IBankService, BankService>();
            builder.Services.AddScoped<IBeneficiaryService, BeneficiaryService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<ISalaryDisbursementService, SalaryDisbursementService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            //builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // JWT Authentication
            var jwtKey = builder.Configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
                throw new InvalidOperationException("JWT Key is missing in appsettings.json");

            var keyBytes = Encoding.UTF8.GetBytes(jwtKey);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                };
            });

            // Cloudinary settings
            builder.Services.Configure<CloudinarySettings>(
      builder.Configuration.GetSection("CloudinarySettings")
  );

            // Register DocumentService with DI
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();


            // Optionally, register Cloudinary itself as singleton if you want to inject it directly
            //builder.Services.AddSingleton(x =>
            //{
            //    var config = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            //    var account = new CloudinaryDotNet.Account(config.CloudName, config.ApiKey, config.ApiSecret);
            //    var cloudinary = new Cloudinary(account) { Api = { Secure = true } };
            //    return cloudinary;
            //});

            var app = builder.Build();

            // Configure middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // must come BEFORE UseAuthorization
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
