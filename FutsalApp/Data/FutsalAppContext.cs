using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FutsalApp.Models;

namespace FutsalApp.Data
{
    public class FutsalAppContext : DbContext
    {
        public FutsalAppContext (DbContextOptions<FutsalAppContext> options)  : base(options)
        {
        }

        public DbSet<FutsalApp.Models.Karyawan> Karyawan { get; set; } = default!;

        public DbSet<FutsalApp.Models.Lapangan>? Lapangan { get; set; }

        public DbSet<FutsalApp.Models.Member>? Member { get; set; }

        public DbSet<FutsalApp.Models.Booking>? Booking { get; set; }
    }
}
