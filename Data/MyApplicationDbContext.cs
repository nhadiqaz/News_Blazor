using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MyApplicationDbContext:DbContext
    {
        public MyApplicationDbContext(DbContextOptions<MyApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasData(
                new Post("چگونه از اشخاص مطالبی که دوست نداریم دوری کنیم ؟",
                 "متن ساختگی با تولید سادگی نامفهوم تولید سادگی از صنعت متن ساختگی با تولید سادگی",
                 "متن ساختگی با تولید سادگی نامفهوم تولید سادگی از صنعت متن ساختگی با تولید سادگمتن ساختگی با تولید سادگی نامفهوم تولید سادگی از صنعت متن ساختگی با تولید سادگ",
                 "1",
                 "بخش ویژه-انجمن-اتاق خبر")
                {
                    PostId = 1
                }
                );
        }
    }
}
