using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Context;

namespace PharmacyManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            #region contexts
            builder.Services.AddDbContext<DBPharmacyContext>(
                        options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
                    );
            #endregion

            #region repositories

            #endregion

            #region services

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
