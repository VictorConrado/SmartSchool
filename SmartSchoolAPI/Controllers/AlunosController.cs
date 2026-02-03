using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data.Repositories;
using SmartSchoolAPI.Models;


namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {

        private readonly IAlunoRepository _repo;

        public AlunosController(IAlunoRepository repo)
        {
            _repo = repo;
        }

        // /api/Alunos
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(result);
        }

        // api/aluno/byId
        [HttpGet("byId/{id}")]
        public IActionResult GetAlunosById(int id)
        {

            var aluno = _repo.GetAlunosById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");
            return Ok(aluno);
        }

        // /api/Alunos/nome
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        { 
            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não cadastrado");
        }

        // /api/Alunos/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var putAluno = _repo.GetAlunosById(id, false);
           
            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não cadastrado");
        }

        // /api/Alunos/id
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var putAluno = _repo.GetAlunosById(id, false);
            if (putAluno == null) return BadRequest("Aluno não encontrado");

            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Ok(aluno);
            }
            return BadRequest("Aluno não atualizado");
        }

        // /api/Alunos/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removeAluno = _repo.GetAlunosById(id, false);
            if (removeAluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(removeAluno);
            if (_repo.SaveChanges())
            {
                return Ok($"Aluno {removeAluno.Nome} foi deletado");
            }
            return BadRequest("Aluno não foi deletado");
        }
    }
}
