using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.v1.DTO_s
{
    public class AlunoDto
    {

        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public bool Ativo { get; set; }
    }
}
