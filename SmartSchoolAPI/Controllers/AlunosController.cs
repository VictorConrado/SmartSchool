using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data.Repositories.Interfaces;
using SmartSchoolAPI.DTO_s;
using SmartSchoolAPI.DTO_s.AlunosDto;
using SmartSchoolAPI.Models;


namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {

        private readonly IAlunoRepository _repo;

        private readonly IMapper _mapper;

        public AlunosController(IAlunoRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // /api/Alunos
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(result));
        }

        // api/aluno/byId
        [HttpGet("byId/{id}")]
        public IActionResult GetAlunosById(int id)
        {

            var aluno = _repo.GetAlunosById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        // /api/Alunos
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        { 
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não cadastrado");
        }

        // /api/Alunos/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var putAluno = _repo.GetAlunosById(id, false);
            if (putAluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, putAluno);

            _repo.Update(putAluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(putAluno));
            }
            return BadRequest("Aluno não atualizado");
        }

        // /api/Alunos/id
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            var patchAluno = _repo.GetAlunosById(id, false);
            if (patchAluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, patchAluno);

            _repo.Update(patchAluno);
            if (_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(patchAluno));
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
