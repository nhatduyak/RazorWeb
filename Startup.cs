using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tich_hop_EntityFramework.models;

namespace Tich_hop_EntityFramework
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
            services.AddRazorPages();
            services.AddDbContext<MyBlogContext>(options =>
            {
                string strsql = Configuration.GetConnectionString("MyBlogContext");
                options.UseSqlServer(strsql);

            });

            // //Đăng ký Identity
            // services.AddIdentity<AppUser, IdentityRole>()
            //             .AddEntityFrameworkStores<MyBlogContext>()
            //             .AddDefaultTokenProviders();

           // Đăng ký Identity - với giao diện mặc định của Identity
                 services.AddDefaultIdentity<AppUser>()
                        .AddEntityFrameworkStores<MyBlogContext>()
                        .AddDefaultTokenProviders();



            // Truy cập IdentityOptions
            services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });


            // //Dịch vụ Kiểm tra đăng nhập
            // SignInManager<AppUser> s;
            // UserManager<AppUser> u;
        }
    }
}


/* 
Tích Hợp EntityFramework vào dự án

dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 5.0.4


Create, Read,Update, Delete (CRUD)

thay vì tạo thủ công các trang razor trong dotnet có lệnh để phát sinh ra các trang cho 1 model nào đó

dotnet aspnet-codegenerator razorpage -m Tich_hop_EntityFramework.models.Article -dc Tich_hop_EntityFramework.models.MyBlogContext -outDir Pages/Blog -udl --referenceScriptLibraries

phân trang Index

 */

 /* 
    Identity
    /Identity/Account/Login
     /Identity/Account/manage


     Để phát sinh các trang cho thư viện Identity User 

     dotnet aspnet-codegenerator identity -dc Tich_hop_EntityFramework.models.MyBlogContext.cs

  */