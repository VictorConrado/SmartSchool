namespace SmartSchoolAPI.v1.DTO_s
{
    public class DisciplinaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int? PrerequisitoId { get; set; }
        public DisciplinaDto Prerequisito { get; set; }
        public int ProfessorId { get; set; }
        public ProfessoresDto Professor { get; set; }
        public int CursoId { get; set; }
        public CursoDto Curso { get; set; }
        public IEnumerable<AlunoDto> AlunosDisciplinas { get; set; }
    }
}
