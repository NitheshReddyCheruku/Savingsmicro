
using Microsoft.EntityFrameworkCore;
using SavingsInvestmentService.Services;
using Savingsmicro.Data;
using Savingsmicro.Services;
using System.Text.Json.Serialization;

namespace Savingsmicro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("default")); });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // Your Angular app's URL
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
            builder.Services.AddScoped<ITransactionService, TransactionService>();

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

            app.UseAuthorization();
            app.UseCors("AllowAngularApp");


            app.MapControllers();

            app.Run();
        }
    }
}
