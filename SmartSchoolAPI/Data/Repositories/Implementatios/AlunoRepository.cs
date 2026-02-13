using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data.Contexts;
using SmartSchoolAPI.Data.Repositories.Implementatios;
using SmartSchoolAPI.Data.Repositories.Interfaces;
using SmartSchoolAPI.Models;
public class AlunoRepository : Repository, IAlunoRepository
{
    public AlunoRepository(SmartContext _context) : base(_context) { }

    public Aluno[] GetAllAlunos(bool includeProfessor = false)
    {
        IQueryable<Aluno> query = _context.Alunos;

        if (includeProfessor)
        {
            query = query
                .Include(a => a.AlunosDisciplinas)
                .ThenInclude(ad => ad.Disciplina)
                .ThenInclude(d => d.Professor);
        }

        return query
            .AsNoTracking()
            .OrderBy(a => a.Id)
            .ToArray();
    }

    public Aluno[] GetByDisciplinas(int disciplinaId, bool includeProfessor = false)
    {
        IQueryable<Aluno> query = _context.Alunos;

        if (includeProfessor)
        {
            query = query
                .Include(a => a.AlunosDisciplinas)
                .ThenInclude(ad => ad.Disciplina)
                .ThenInclude(d => d.Professor);
        }

        return query
            .AsNoTracking()
            .Where(a => a.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId))
            .OrderBy(a => a.Id)
            .ToArray();
    }

    public Aluno GetAlunosById(int alunoId, bool includeProfessor = false)
    {
        IQueryable<Aluno> query = _context.Alunos;

        if (includeProfessor)
        {
            query = query
                .Include(a => a.AlunosDisciplinas)
                .ThenInclude(ad => ad.Disciplina)
                .ThenInclude(d => d.Professor);
        }

        return query
            .AsNoTracking()
            .FirstOrDefault(a => a.Id == alunoId);
    }
}
