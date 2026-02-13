using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data.Contexts;
using SmartSchoolAPI.Data.Repositories.Interfaces;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Data.Repositories.Implementatios
{
    public class ProfessorRepository : Repository, IProfessorRepository
    {
        public ProfessorRepository(SmartContext context) : base(context) { }

        public Professor[] GetAllProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query
                    .Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }

            return query
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .ToArray();
        }

        public Professor[] GetByDisciplinas(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query
                    .Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }

            return query
                .AsNoTracking()
                .Where(p => p.Disciplinas.Any(d =>
                    d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)))
                .OrderBy(p => p.Id)
                .ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query
                    .Include(p => p.Disciplinas)
                    .ThenInclude(d => d.AlunosDisciplinas)
                    .ThenInclude(ad => ad.Aluno);
            }

            return query
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == professorId);
        }
    }
}
