using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Data
{
    public class SmartContext : DbContext
    {
        public SmartContext(DbContextOptions<SmartContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<AlunoDisciplina> AlunosDisciplinas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoDisciplina>()
                   .HasKey(AD => new { AD.AlunoId, AD.DisciplinaId });

            modelBuilder.Entity<AlunoDisciplina>()
                        .HasOne(ad => ad.Aluno)
                        .WithMany(a => a.AlunosDisciplinas)
                        .HasForeignKey(ad => ad.AlunoId);

            modelBuilder.Entity<AlunoDisciplina>()
                        .HasOne(ad => ad.Disciplina)
                        .WithMany(d => d.AlunosDisciplinas)
                        .HasForeignKey(ad => ad.DisciplinaId);
        }
    }
}
