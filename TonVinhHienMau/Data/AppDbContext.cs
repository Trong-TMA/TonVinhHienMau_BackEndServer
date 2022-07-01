using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TonVinhHienMau.Data.Models;
using TonVinhHienMau.Models;

namespace TonVinhHienMau.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<HoGiaDinh>()
                     .HasKey(c => c.Id);
        }
        public DbSet<NguoiHienMau> NguoiHienMau { get; set; }
        public DbSet<DonVi> DonVi { get; set; }
        public DbSet<DotTonVinh> DotTonVinh { get; set; }
        public DbSet<MoiQuanHe> MoiQuanHe { get; set; }
        public virtual DbSet<HoGiaDinh> HoGiaDinh {get;set;}
        public DbSet<ApplicationUser> ApplicationUsers {get; set;}
    }
}
