using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data.Contexts;
using SmartSchoolAPI.Data.Repositories.Interfaces;
using SmartSchoolAPI.v1.DTO_s;
using SmartSchoolAPI.v1.DTO_s.AlunosDto;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.v1.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações CRUD relacionadas a professores.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {

        private readonly IProfessorRepository _repo;

        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa uma nova instância do controlador ProfessorController.
        /// </summary>
        /// <param name="repo">Repositório de Professores para acesso a dados.</param>
        /// <param name="mapper">AutoMapper para conversão entre entidades e DTOs.</param>
        public ProfessorController(IProfessorRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        /// <summary>
        /// Retorna todos os professores.
        /// </summary>
        /// <returns>IActionResult contendo a lista de ProfessoresDto (HTTP 200) ou erro apropriado.</returns>
        // /api/professor
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessoresDto>>(result));
        }

        /// <summary>
        /// Retorna um professor pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do professor.</param>
        /// <returns>IActionResult com o ProfissionaisDto correspondente (HTTP 200) ou BadRequest se não encontrado.</returns>
        // api/professor/byId
        [HttpGet("byId/{id}")]
        public IActionResult GetProfessorById(int id)
        {

            var professor = _repo.GetProfessorById(id, false);
            if (professor == null) return BadRequest("O professor não foi encontrado");

            var professorDto = _mapper.Map<ProfessoresDto>(professor);
            return Ok(professorDto);
        }


        /// <summary>
        /// Cria um novo professor a partir do DTO de registro.
        /// </summary>
        /// <param name="model">Dados do professor a serem registrados.</param>
        /// <returns>IActionResult com o recurso criado (HTTP 201) ou BadRequest em caso de falha.</returns>
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

        /// <summary>
        /// Atualiza completamente um professor existente identificado pelo id.
        /// </summary>
        /// <param name="id">Identificador do professor a ser atualizado.</param>
        /// <param name="model">DTO com os novos dados do professor.</param>
        /// <returns>IActionResult com o recurso atualizado (HTTP 201) ou BadRequest em caso de erro.</returns>
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

        /// <summary>
        /// Aplica atualização parcial em um professor existente.
        /// </summary>
        /// <param name="id">Identificador do professor a ser parcialmente atualizado.</param>
        /// <param name="model">DTO contendo os campos a serem atualizados.</param>
        /// <returns>IActionResult com o recurso atualizado (HTTP 201) ou BadRequest em caso de erro.</returns>
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

        /// <summary>
        /// Remove um professor pelo seu identificador.
        /// </summary>
        /// <param name="id">Identificador do professor a ser removido.</param>
        /// <returns>IActionResult indicando sucesso (HTTP 200) com mensagem ou BadRequest em caso de falha.</returns>
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
