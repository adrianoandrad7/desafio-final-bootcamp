using Microsoft.EntityFrameworkCore;
using WebApiDesafioFinal.Domain;

namespace WebApiDesafioFinal.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
        public DbSet<Curso> Cursos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Curso>(c =>
            {
                c.ToTable("Cursos");
                c.HasKey(c => c.Id);
                c.Property(c => c.Titulo).HasColumnType("varchar(40)").IsRequired();
                c.Property(c => c.Semestres).IsRequired();
                c.Property(c => c.Status).IsRequired();
            });
        }
    }
} 
