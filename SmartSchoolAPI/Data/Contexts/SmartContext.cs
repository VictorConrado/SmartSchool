using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Data.Contexts
{
    public class SmartContext : DbContext
    {
        public SmartContext(DbContextOptions<SmartContext> options) : base(options) { }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<AlunoCurso> AlunosCursos { get; set; }
        public DbSet<AlunoDisciplina> AlunosDisciplinas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Professor> Professores { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AlunoDisciplina>()
                .HasKey(AD => new { AD.AlunoId, AD.DisciplinaId });

            builder.Entity<AlunoCurso>()
                .HasKey(AD => new { AD.AlunoId, AD.CursoId });

            builder.Entity<Professor>().HasData(
                    new Professor { Id = 1, Registro = 1, Nome = "Lauro", Sobrenome = "Oliveira", Telefone = "11999990001", DataInicio = new DateTime(2020, 1, 1), Ativo = true },
                    new Professor { Id = 2, Registro = 2, Nome = "Roberto", Sobrenome = "Soares", Telefone = "11999990002", DataInicio = new DateTime(2020, 1, 1), Ativo = true },
                    new Professor { Id = 3, Registro = 3, Nome = "Ronaldo", Sobrenome = "Marconi", Telefone = "11999990003", DataInicio = new DateTime(2020, 1, 1), Ativo = true },
                    new Professor { Id = 4, Registro = 4, Nome = "Rodrigo", Sobrenome = "Carvalho", Telefone = "11999990004", DataInicio = new DateTime(2020, 1, 1), Ativo = true },
                    new Professor { Id = 5, Registro = 5, Nome = "Alexandre", Sobrenome = "Montanha", Telefone = "11999990005", DataInicio = new DateTime(2020, 1, 1), Ativo = true }
                );

            builder.Entity<Curso>()
                .HasData(new List<Curso>{
                    new Curso(1, "Tecnologia da Informação"),
                    new Curso(2, "Sistemas de Informação"),
                    new Curso(3, "Ciência da Computação")
                });

            builder.Entity<Disciplina>()
                .HasData(new List<Disciplina>{
                    new Disciplina(1, "Matemática", 1, 1),
                    new Disciplina(2, "Matemática", 1, 3),
                    new Disciplina(3, "Física", 2, 3),
                    new Disciplina(4, "Português", 3, 1),
                    new Disciplina(5, "Inglês", 4, 1),
                    new Disciplina(6, "Inglês", 4, 2),
                    new Disciplina(7, "Inglês", 4, 3),
                    new Disciplina(8, "Programação", 5, 1),
                    new Disciplina(9, "Programação", 5, 2),
                    new Disciplina(10, "Programação", 5, 3)
                });

            builder.Entity<Aluno>()
                .HasData(new List<Aluno>(){
                    new Aluno(1, 1, "Marta", "Kent", "33225555", new DateTime(2005, 5, 28)),
                    new Aluno(2, 2, "Paula", "Isabela", "3354288", new DateTime(2005, 5, 28)),
                    new Aluno(3, 3, "Laura", "Antonia", "55668899", new DateTime(2005, 5, 28)),
                    new Aluno(4, 4, "Luiza", "Maria", "6565659", new DateTime(2005, 5, 28)),
                    new Aluno(5, 5, "Lucas", "Machado", "565685415", new DateTime(2005, 5, 28)),
                    new Aluno(6, 6, "Pedro", "Alvares", "456454545", new DateTime(2005, 5, 28)),
                    new Aluno(7, 7, "Paulo", "José", "9874512", new DateTime(2005, 5, 28))
                });


            builder.Entity<AlunoDisciplina>()
                .HasData(new List<AlunoDisciplina>() {
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 5 }
                });
        }
    }
}