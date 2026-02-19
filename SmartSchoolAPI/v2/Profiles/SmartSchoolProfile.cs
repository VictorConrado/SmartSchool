using SmartSchoolAPI.v2.DTO_s;
using SmartSchoolAPI.Models;
using AutoMapper;
using SmartSchoolAPI.v2.DTO_s.AlunosDto;
using SmartSchoolAPI.Helpers;

namespace SmartSchoolAPI.v2.Profiles
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

        }
    }
}
