using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using practise.BookStoreApp.Data;
using practise.BookStoreApp.Repository;

namespace practise.BookStoreApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("Data Source=.\\SQLExpress;Initial Catalog=BookStore;Integrated Security=True"));


            services.AddControllersWithViews();
#if DEBUG

            services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(option =>
            {
                option.HtmlHelperOptions.ClientValidationEnabled = true;
            });
#endif
            services.AddScoped<BookRepository, BookRepository>();
            services.AddScoped<LanguageRepository, LanguageRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.Use(async (context, next)=> 
            //{
            //    await context.Response.WriteAsync("Hellow from first middleware");

            //    await next();

            //    await context.Response.WriteAsync("Hellow from first middleware response");

            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hellow from second middleware");

            //    await next();

            //    await context.Response.WriteAsync("Hellow from second middleware response");
            //});
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hellow from third middleware");

            //    await next();

            //});
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider
                = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
                RequestPath = "/MyStaticFiles"

            }) ;
            
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                //endpoints.MapControllerRoute(
                //    name: "Default",
                //    pattern: "bookStore/{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/lora", async context =>
                {
                    if (env.IsDevelopment())
                    {
                        await context.Response.WriteAsync("Hello Lora dev!");
                    }
                    
                });
            });
        }
    }
}
