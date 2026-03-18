using AutoMapper;
using SmartSchoolAPI.Models;
using SmartSchoolAPI.v1.DTO_s;
using SmartSchoolAPI.v1.DTO_s.AlunosDto;
using SmartSchoolAPI.Helpers;
using System.Linq;

namespace SmartSchoolAPI.v1.Profiles
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"))
                .ForMember(dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataNascimento.GetIdadeAtual()));

            CreateMap<AlunoDto, Aluno>();

            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

            CreateMap<Aluno, AlunoPatchDto>().ReverseMap();

            CreateMap<Professor, ProfessoresDto>()
                .ForMember(dest => dest.Nome,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"));

            CreateMap<ProfessoresDto, Professor>();

            CreateMap<Professor, ProfessoresRegistrarDto>().ReverseMap();

            CreateMap<Disciplina, DisciplinaDto>()
                .ForMember(dest => dest.AlunosDisciplinas,
                    opt => opt.MapFrom(src =>
                        src.AlunosDisciplinas.Select(ad => ad.Aluno)));

            CreateMap<DisciplinaDto, Disciplina>();

            CreateMap<Curso, CursoDto>().ReverseMap();

            CreateMap<AlunoDisciplina, AlunoDto>()
                .ConvertUsing((src, dest, context) =>
                    context.Mapper.Map<AlunoDto>(src.Aluno));

        }
    }
}