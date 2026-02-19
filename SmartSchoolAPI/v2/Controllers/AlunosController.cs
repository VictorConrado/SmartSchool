using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data.Repositories.Interfaces;
using SmartSchoolAPI.v2.DTO_s;
using SmartSchoolAPI.v2.DTO_s.AlunosDto;
using SmartSchoolAPI.Models;


namespace SmartSchoolAPI.v2.Controllers
{
    /// <summary>
    /// Controller da API (versão 2.0) responsável pelas operações relacionadas a Alunos.
    /// Fornece endpoints para listar, consultar, atualizar (PUT/PATCH) e deletar alunos.
    /// </summary>

    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunosController : ControllerBase
    {

        private readonly IAlunoRepository _repo;

        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do controller de Alunos (v2).
        /// Injeta as dependências necessárias: repositório de alunos e AutoMapper.
        /// </summary>
        /// <param name="repo">Repositório responsável pelas operações de dados de Aluno.</param>
        /// <param name="mapper">Instância do AutoMapper para conversão entre Model e DTO.</param>
        public AlunosController(IAlunoRepository repo, IMapper mapper)
        {
            /// O AutoMapper é uma biblioteca que facilita a conversão de objetos de um tipo para outro, eliminando a necessidade de escrever código manual para mapear as propriedades entre os objetos. Ele é especialmente útil em cenários onde você tem classes de domínio (modelos) e classes de transferência de dados (DTOs) que possuem estruturas semelhantes, mas não são idênticas.
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// Obtém todos os alunos.
        /// Retorna a coleção de alunos mapeada para IEnumerable&lt;AlunoDto&gt;.
        /// </summary>
        /// <returns>HTTP 200 com a lista de AlunoDto.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.GetAllAlunos(true);
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(result));
        }

        /// <summary>
        /// Obtém um aluno pelo ID fornecido.
        /// Se o aluno for encontrado, retorna um AlunoDto; caso contrário, retorna BadRequest informando que o aluno não foi encontrado.
        /// </summary>
        /// <param name="id">ID do aluno a ser consultado.</param>
        /// <returns>HTTP 200 com AlunoDto ou HTTP 400 se não encontrado.</returns>
        [HttpGet("byId/{id}")]
        public IActionResult GetAlunosById(int id)
        {

            var aluno = _repo.GetAlunosById(id, false);
            if (aluno == null) return BadRequest("O Aluno não foi encontrado");

            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }

        /// <summary>
        /// Atualiza completamente os dados de um aluno existente identificado pelo ID.
        /// Mapeia os dados do AlunoRegistrarDto para a entidade existente e persiste a alteração.
        /// Retorna Created com o Aluno atualizado em caso de sucesso; caso contrário, retorna BadRequest.
        /// </summary>
        /// <param name="id">ID do aluno a ser atualizado.</param>
        /// <param name="model">DTO com os dados para atualização do aluno.</param>
        /// <returns>HTTP 201 (Created) com AlunoDto atualizado ou HTTP 400 se falhar.</returns>
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
        /// Aplica uma atualização parcial ou alternativa aos dados de um aluno existente identificado pelo ID.
        /// O comportamento atual mapeia o AlunoRegistrarDto sobre a entidade existente e salva as alterações.
        /// Retorna Created com o Aluno atualizado em caso de sucesso; caso contrário, retorna BadRequest.
        /// </summary>
        /// <param name="id">ID do aluno a ser atualizado.</param>
        /// <param name="model">DTO com os campos a serem atualizados.</param>
        /// <returns>HTTP 201 (Created) com AlunoDto atualizado ou HTTP 400 se falhar.</returns>
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
        /// Remove um aluno existente identificado pelo ID.
        /// Se o aluno existir, realiza a deleção e retorna uma mensagem de confirmação; caso contrário, retorna BadRequest.
        /// </summary>
        /// <param name="id">ID do aluno a ser deletado.</param>
        /// <returns>HTTP 200 com mensagem de sucesso ou HTTP 400 se falhar.</returns>
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
