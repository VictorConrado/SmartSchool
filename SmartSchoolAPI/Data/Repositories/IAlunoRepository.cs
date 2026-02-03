using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Data.Repositories
{
    public interface IAlunoRepository : IRepository
    {
        Aluno[] GetAllAlunos(bool includeProfessor);
        Aluno[] GetByDisciplinas(int disciplinaId, bool includeProfessor);
        Aluno GetAlunosById(int alunoId, bool includeProfessor);
    }
}
