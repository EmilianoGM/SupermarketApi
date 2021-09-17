using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SupermarketApi.DataAccess;
using SupermarketApi.Abstractions.Applications;
using SupermarketApi.Application;
using SupermarketApi.Abstractions.Repositories;
using SupermarketApi.Repository;
using SupermarketApi.Abstractions.Contexts;

namespace SupermarketAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //Add db context 
            services.AddDbContext<SupermarketDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("SupermarketConnectionString"));
            });
            //Add scoped for depency injection
            //App
            services.AddScoped(typeof(IApplication<>), typeof(Application<>));
            services.AddScoped(typeof(ICategoryApplication), typeof(CategoryApplication));
            services.AddScoped(typeof(IProductApplication), typeof(ProductApplication));
            services.AddScoped(typeof(IOrderApplication), typeof(OrderApplication));

            //Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            //DataAcces
            services.AddScoped(typeof(IDataBaseContext<>), typeof(DataBaseContext<>));
            services.AddScoped(typeof(ICategoryDbContext), typeof(CategoryDbContext));
            services.AddScoped(typeof(IProductDbContext), typeof(ProductDbContext));
            services.AddScoped(typeof(IOrderDbContext), typeof(OrderDbContext));
            services.AddScoped(typeof(IOrderProductsDbContext), typeof(OrderProductsDbContext));
            /*
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();*/
            services.AddAutoMapper(typeof(Startup));
            /*services.AddScoped<IUriService, UriService>(o =>
            {
                var accesor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });*/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())    
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
