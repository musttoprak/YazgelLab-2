using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using yazgel_proje.Models;

namespace yazgel_proje
{
    

    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Ogretmen> Ogretmen { get; set; }
        public DbSet<Kisit> Kisit { get; set; }
        public DbSet<DersProgrami> DersProgrami{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ogretmen>(e => e.HasKey(x => x.ogretmenId));
            modelBuilder.Entity<Kisit>(e => e.HasKey(x => x.kisitId));
            modelBuilder.Entity<DersProgrami>(e => e.HasKey(x => x.dersProgramiId));
 
        }
    }
}
