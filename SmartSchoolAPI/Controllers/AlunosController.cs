using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data;
using SmartSchoolAPI.Models;


namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly SmartContext _context;

        public AlunosController(SmartContext context)
        {
            _context = context;
        }
        // /api/Alunos
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        // api/aluno/byId
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {

            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");
            return Ok(aluno);
        }

        //  /api/Alunos/nome
        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobrenome) 
        {
            var aluno = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.
            Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if ( aluno == null) return BadRequest("O aluno não foi encontrado");
            return Ok(aluno);
        }

        // /api/Alunos/nome
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        // /api/Alunos/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var putAluno = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (putAluno == null) return BadRequest("Aluno não encontrado");

            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        // /api/Alunos/id
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var pathAluno = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (pathAluno == null) return BadRequest("Aluno não encontrado");
           
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        // /api/Alunos/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removeAluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if(removeAluno == null) return BadRequest("Aluno não encontrado");
           
            _context.Remove(removeAluno);
            _context.SaveChanges();
            return Ok($"Aluno {removeAluno.Nome} foi deletado");

         }
    }
}
