using Microsoft.EntityFrameworkCore;

namespace _2daPracticaProgramada_OscarNaranjoZuniga.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Lista> Listas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .Property(u => u.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Lista>()
                .Property(l => l.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Tarea>()
                .Property(t => t.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
        }
    }
}