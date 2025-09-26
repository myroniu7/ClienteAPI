using Microsoft.EntityFrameworkCore;
using ClienteAPI.Data.Entities;

namespace ClienteAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes");
                entity.HasKey(e => e.Identificacion);
                entity.Property(e => e.Identificacion).HasColumnName("identificacion");
                entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(50);
                entity.Property(e => e.Correo).HasColumnName("correo").HasMaxLength(50);
            });
        }
    }
}