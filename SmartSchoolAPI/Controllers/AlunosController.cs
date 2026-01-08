using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolAPI.Models;


namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>()
        {
            new Aluno()
            {
              Id = 1,
              Nome = "Marcos",
              Sobrenome = "Antonio",
              Telefone = "33225555"
            },
             new Aluno()
            {
              Id = 2,
              Nome = "Nicolas",
              Sobrenome = "Carlos",
              Telefone = "55725555"
            },
             new Aluno()
            {
              Id = 3,
              Nome = "Julia",
              Sobrenome = "Santos",
              Telefone = "33825555"
            },
        };

        public AlunosController() { }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }

        // api/aluno/byId
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {

            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");
            return Ok(aluno);
        }

        // api/aluno/nome
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, String nome)
        {
          return Ok($"Aluno {nome} foi deletado");

         }
    }
}
