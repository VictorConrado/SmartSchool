using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.v1.DTO_s
{
    public class ProfessoresDto
    {

        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
