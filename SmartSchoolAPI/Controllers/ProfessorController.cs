using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data.Contexts;
using SmartSchoolAPI.Data.Repositories.Interfaces;
using SmartSchoolAPI.DTO_s;
using SmartSchoolAPI.DTO_s.AlunosDto;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {

        private readonly IProfessorRepository _repo;

        private readonly IMapper _mapper;

        public ProfessorController(IProfessorRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            
        }

        // /api/professor
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
                return Ok(_mapper.Map<IEnumerable<ProfessoresDto>>(result));
        }

        // api/professor/byId
        [HttpGet("byId/{id}")]
        public IActionResult GetProfessorById(int id)
        {

            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("O professor não foi encontrado");

            var professorDto = _mapper.Map<ProfessoresDto>(professor);
            return Ok(professorDto);
        }


        // /api/professor
        [HttpPost]
        public IActionResult Post(ProfessoresRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessoresDto>(professor));
            }
            return BadRequest("Professor não cadastrado");
        }

        // /api/Professors/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessoresRegistrarDto model)
        {
            var putProfessor = _repo.GetProfessorById(id, false);
            if (putProfessor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, putProfessor);

            _repo.Update(putProfessor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessoresDto>(putProfessor));
            }
            return BadRequest("Professor não atualizado");
        }

        // /api/professor/id
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessoresRegistrarDto model)
        {
            var patchProfessor = _repo.GetProfessorById(id, false);
            if (patchProfessor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, patchProfessor);

            _repo.Update(patchProfessor);
            if (_repo.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessoresDto>(patchProfessor));
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
