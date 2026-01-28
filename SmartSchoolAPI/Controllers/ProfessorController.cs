using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context)
        {
            _context = context;
        }

        // /api/professor
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }

        //  /api/professor/nome
        [HttpGet("ByName")]
        public IActionResult GetByName(string nome)
        {
            var professorName = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Nome.Contains(nome));
            if (professorName == null) return BadRequest("O Professor não foi encontrado");

            return Ok(nome);
        }

        // /api/professor
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        // /api/professor/id
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var patchProfessor = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (patchProfessor == null) return BadRequest("Professor não encontrado");
            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        // /api/Professor/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteProfessor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if (deleteProfessor == null) return BadRequest("Professor não encontrado");
            _context.Remove(deleteProfessor);
            _context.SaveChanges();
            return Ok($"Professor {deleteProfessor.Nome} foi deletado com sucesso.");
        }

    }
}
