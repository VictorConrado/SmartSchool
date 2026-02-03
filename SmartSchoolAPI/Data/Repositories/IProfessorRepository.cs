using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Data.Repositories
{
    public interface IProfessorRepository : IRepository
    {
        Professor[] GetAllProfessores(bool includeAlunos);
        Professor[] GetByDisciplinas(int disciplinaId, bool includeAlunos);
        Professor GetProfessorById(int professorId, bool includeAlunos);
    }
}
