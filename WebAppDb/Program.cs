using Microsoft.Extensions.DependencyInjection;
using WebAppDb.Configuration;


namespace WebAppDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped(typeof(MapperConfig));

            // Add services to the container.
            // Replace this line:
            // builder.Services.AddAutoMapper(typeof(MapperConfig));

            // With this line:
            //builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperConfig>());
            builder.Services.AddRazorPages();   

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
