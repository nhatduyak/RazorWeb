using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Tich_hop_EntityFramework.models
{
    public class MyBlogContext:IdentityDbContext<AppUser>
    {
        public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
        {
            //....
            

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder); 

            //xữ lý bỏ đi phần ASPNET_____TABLE (Trong khi khởi tạo ra từ identity)

            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tablename=entityType.GetTableName();
                if(tablename.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tablename.Substring(6));
                }
            }


        } 
        public DbSet<Article> articles{set;get;}
    }


}