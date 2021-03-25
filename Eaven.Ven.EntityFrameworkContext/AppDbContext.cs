using Eaven.Ven.Domain;
using Eaven.Ven.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Eaven.Ven.EntityFrameworkContext
{
    public class AppDbContext : DataDbContext<AppDbContext>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();//启用延迟加载
            optionsBuilder.LogTo(Console.WriteLine);//日志
            optionsBuilder.EnableDetailedErrors();//更详细的查询错误（以性能为代价）
        }
        #region App用户
        /// App用户
        /// </summary>
        public DbSet<AppUser> AppUser { get; set; }
        /// <summary>
        /// app用户收货地址
        /// </summary>
        public DbSet<AppUserAddress> AppUserAddress { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region app用户
            modelBuilder.Entity<AppUser>(b =>
            {
                b.Property(t => t.Disable).HasConversion(new BoolToZeroOneConverter<Int16>());
            });
            modelBuilder.Entity<AppUserAddress>(b =>
            {
                b.Property(t => t.Default).HasConversion(new BoolToZeroOneConverter<Int16>());
            });
            #endregion
        }

    }
}
