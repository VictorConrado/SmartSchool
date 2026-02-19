using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data.Repositories.Interfaces;
using SmartSchoolAPI.v1.DTO_s;
using SmartSchoolAPI.v1.DTO_s.AlunosDto;
using SmartSchoolAPI.Models;


namespace SmartSchoolAPI.v1.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar operações relacionadas a alunos (CRUD).
    /// </summary>

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunosController : ControllerBase
    {

        private readonly IAlunoRepository _repo;

        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do controlador de alunos. Injeta o repositório de alunos e o AutoMapper.
        /// </summary>
        /// <param name="repo">Repositório de alunos.</param>
        /// <param name="mapper">Instância do AutoMapper para mapeamento de DTOs.</param>
        public AlunosController(IAlunoRepository repo, IMapper mapper)
        {
            /// O AutoMapper é uma biblioteca que facilita a conversão de objetos de um tipo para outro, eliminando a necessidade de escrever código manual para mapear as propriedades entre os objetos. Ele é especialmente útil em cenários onde você tem classes de domínio (modelos) e classes de transferência de dados (DTOs) que possuem estruturas semelhantes, mas não são idênticas.
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Método para obter todos os alunos. O resultado é mapeado para uma coleção de objetos do tipo "AlunoDto" antes de ser retornado como resposta HTTP.
        /// </summary>
        /// <returns>Retorna Ok com a lista de AlunoDto.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(result));
        }

        /// <summary>
        /// Método para obter um aluno específico com base no ID fornecido. O resultado é mapeado para um objeto do tipo "AlunoDto" antes de ser retornado como resposta HTTP.
        /// </summary>
        /// <param name="id">Identificador do aluno.</param>
        /// <returns>Retorna Ok com o AlunoDto se encontrado; caso contrário, BadRequest.</returns>
        [HttpGet("byId/{id}")]
        public IActionResult GetAlunosById(int id)
        {

            var aluno = _repo.GetAlunosById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        /// <summary>
        /// Cria um novo aluno a partir do DTO de registro fornecido.
        /// </summary>
        /// <param name="model">Dados para registro do aluno (AlunoRegistrarDto).</param>
        /// <returns>Retorna Created com o AlunoDto criado se a operação for bem-sucedida; caso contrário, BadRequest.</returns>
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

        /// <summary>
        /// Atualiza todos os dados de um aluno existente identificado pelo ID fornecido.
        /// </summary>
        /// <param name="id">Identificador do aluno a ser atualizado.</param>
        /// <param name="model">Dados de atualização (AlunoRegistrarDto).</param>
        /// <returns>Retorna Created com o AlunoDto atualizado se a operação for bem-sucedida; caso contrário, BadRequest.</returns>
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

        /// <summary>
        /// Atualiza parcialmente os dados de um aluno existente identificado pelo ID fornecido.
        /// </summary>
        /// <param name="id">Identificador do aluno a ser atualizado.</param>
        /// <param name="model">Dados parciais de atualização (AlunoRegistrarDto).</param>
        /// <returns>Retorna Created com o AlunoDto atualizado se a operação for bem-sucedida; caso contrário, BadRequest.</returns>
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

        /// <summary>
        /// Remove um aluno existente identificado pelo ID fornecido.
        /// </summary>
        /// <param name="id">Identificador do aluno a ser removido.</param>
        /// <returns>Retorna Ok com mensagem de confirmação se a remoção for bem-sucedida; caso contrário, BadRequest.</returns>
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
