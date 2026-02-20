using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data.Contexts;
using SmartSchoolAPI.Data.Repositories.Implementatios;
using SmartSchoolAPI.Data.Repositories.Interfaces;
using SmartSchoolAPI.Helpers;
using SmartSchoolAPI.Models;
public class AlunoRepository : Repository, IAlunoRepository
{
    public AlunoRepository(SmartContext _context) : base(_context) { }

    public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false)
    {
        IQueryable<Aluno> query = _context.Alunos;

        if (includeProfessor)
        {
            query = query
                .Include(a => a.AlunosDisciplinas)
                .ThenInclude(ad => ad.Disciplina)
                .ThenInclude(d => d.Professor);
        }

        query = query.AsNoTracking().OrderBy(a => a.Id);

        if (!string.IsNullOrEmpty(pageParams.Nome))
            query = query.Where(aluno => aluno.Nome
                                               .ToUpper()
                                               .Contains(pageParams.Nome.ToUpper()) ||
                                               aluno.Sobrenome
                                               .ToUpper()
                                               .Contains(pageParams.Nome.ToUpper()));

        if (pageParams.Matricula > 0)
            query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);

        if (pageParams.Ativo != null)
            query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0));

        return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
    }

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
