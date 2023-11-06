using InsuranceProject.Data;
using InsuranceProject.Repository;
using InsuranceProject.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace InsuranceProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddTransient(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            builder.Services.AddTransient<IAgentService, AgentService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<ICommisionWithdrawalService, CommisionWithdrawalService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();

            builder.Services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("connString"));
            });
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


            app.MapControllers();

            app.Run();
        }
    }
}