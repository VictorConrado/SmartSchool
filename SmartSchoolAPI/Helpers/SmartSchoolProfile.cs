using SmartSchoolAPI.DTO_s;
using SmartSchoolAPI.Models;
using AutoMapper;
using SmartSchoolAPI.DTO_s.AlunosDto;

namespace SmartSchoolAPI.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"))
                .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => src.DataNascimento.GetIdadeAtual()))
                ;

            CreateMap<AlunoDto, Aluno>();

            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

            CreateMap<Professor, ProfessoresDto>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"));

            CreateMap<ProfessoresDto, Professor>();

            CreateMap<Professor, ProfessoresRegistrarDto>().ReverseMap();
        }
    }
}
