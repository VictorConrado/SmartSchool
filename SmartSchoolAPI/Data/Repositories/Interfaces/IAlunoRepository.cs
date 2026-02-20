using SmartSchoolAPI.Helpers;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Data.Repositories.Interfaces
{
    public interface IAlunoRepository : IRepository
    {
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor);
        Aluno[] GetAllAlunos(bool includeProfessor);
        Aluno[] GetByDisciplinas(int disciplinaId, bool includeProfessor);
        Aluno GetAlunosById(int alunoId, bool includeProfessor);
    }
}
