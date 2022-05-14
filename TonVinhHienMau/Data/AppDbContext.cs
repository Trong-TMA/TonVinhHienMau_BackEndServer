﻿using Microsoft.EntityFrameworkCore;
using TonVinhHienMau.Models;

namespace TonVinhHienMau.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<NguoiHienMau> NguoiHienMau { get; set; }
        public DbSet<DonVi> DonVi { get; set; }
        public DbSet<DotTonVinh> DotTonVinh {get;set;}

    }
}
