using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data.Contexts;
using SmartSchoolAPI.Data.Repositories;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorRepository _repo;

        public ProfessorController(IProfessorRepository repo)
        {
            _repo = repo;
        }

        // /api/professor
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
                return Ok(result);
        }

        // api/professor/byId
        [HttpGet("byId/{id}")]
        public IActionResult GetProfessorById(int id)
        {

            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("O professor não foi encontrado");
            return Ok(professor);
        }


        // /api/professor
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não cadastrado");
        }

        // /api/Professors/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var putProfessor = _repo.GetProfessorById(id, false);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não cadastrado");
        }

        // /api/professor/id
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var putProfessor = _repo.GetProfessorById(id, false);
            if (putProfessor == null) return BadRequest("Professor não encontrado");

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não atualizado");
        }

        // /api/Professor/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removeProfessor = _repo.GetProfessorById(id, false);
            if (removeProfessor == null) return BadRequest("Professor não encontrado");

            _repo.Delete(removeProfessor);
            if (_repo.SaveChanges())
            {
                return Ok($"Professor {removeProfessor.Nome} foi deletado");
            }
            return BadRequest("Professor não foi deletado");
        }

    }
}
