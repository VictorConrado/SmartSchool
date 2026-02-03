using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Data.Contexts
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

            modelBuilder.Entity<Professor>()
                .HasData(new List<Professor>
                {
                        new(1, "Lauro"),
                        new(2, "Roberto"),
                        new(3, "Ronaldo"),
                        new(4, "Rodrigo"),
                        new(5, "Alexandre"),
                });

            modelBuilder.Entity<Disciplina>()
                .HasData(new List<Disciplina>
                {
                        new(1, "Matemática", 1),
                        new(2, "Física", 2),
                        new(3, "Português", 3),
                        new(4, "Inglês", 4),
                        new(5, "Programação", 5)
                });

            modelBuilder.Entity<Aluno>()
                .HasData(new List<Aluno>
                {
                        new(1, "Marta", "Kent", "33225555"),
                        new(2, "Paula", "Isabela", "3354288"),
                        new(3, "Laura", "Antonia", "55668899"),
                        new(4, "Luiza", "Maria", "6565659"),
                        new(5, "Lucas", "Machado", "565685415"),
                        new(6, "Pedro", "Alvares", "456454545"),
                        new(7, "Paulo", "José", "9874512")
                });

            modelBuilder.Entity<AlunoDisciplina>()
                .HasData(new List<AlunoDisciplina>
                {
                        new() {AlunoId = 1, DisciplinaId = 2 },
                        new() {AlunoId = 1, DisciplinaId = 4 },
                        new() {AlunoId = 1, DisciplinaId = 5 },
                        new() {AlunoId = 2, DisciplinaId = 1 },
                        new() {AlunoId = 2, DisciplinaId = 2 },
                        new() {AlunoId = 2, DisciplinaId = 5 },
                        new() {AlunoId = 3, DisciplinaId = 1 },
                        new() {AlunoId = 3, DisciplinaId = 2 },
                        new() {AlunoId = 3, DisciplinaId = 3 },
                        new() {AlunoId = 4, DisciplinaId = 1 },
                        new() {AlunoId = 4, DisciplinaId = 4 },
                        new() {AlunoId = 4, DisciplinaId = 5 },
                        new() {AlunoId = 5, DisciplinaId = 4 },
                        new() {AlunoId = 5, DisciplinaId = 5 },
                        new() {AlunoId = 6, DisciplinaId = 1 },
                        new() {AlunoId = 6, DisciplinaId = 2 },
                        new() {AlunoId = 6, DisciplinaId = 3 },
                        new() {AlunoId = 6, DisciplinaId = 4 },
                        new() {AlunoId = 7, DisciplinaId = 1 },
                        new() {AlunoId = 7, DisciplinaId = 2 },
                        new() {AlunoId = 7, DisciplinaId = 3 },
                        new() {AlunoId = 7, DisciplinaId = 4 },
                        new() {AlunoId = 7, DisciplinaId = 5 }
                });
        }
    }
}
