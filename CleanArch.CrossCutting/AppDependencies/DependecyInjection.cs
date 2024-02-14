using CleanArch.Domain.Abstractions;
using CleanArch.Infrastructure.Context;
using CleanArch.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.CrossCutting.AppDependencies
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            var sqliteConnectionString = "DataSource=app.db;Cache=Shared";

            services.AddDbContext<AppDbContext>(options =>
                                        options.UseSqlite(connectionString: sqliteConnectionString));


            //IDbconnection as a single instance
            services.AddSingleton<IDbConnection>(provider =>
            {
                var connection = new SqliteConnection(sqliteConnectionString);

                connection.Open();
                return connection;
            });


            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMemberDapperRepository, MemberDapperRepository>();

            var myhandlers = AppDomain.CurrentDomain.Load("CleanArch.Application");
            services.AddMediatR(_ => _.RegisterServicesFromAssemblies(myhandlers));

            return services;
        }
    }
}
