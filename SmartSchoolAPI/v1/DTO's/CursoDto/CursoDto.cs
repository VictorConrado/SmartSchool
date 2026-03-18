namespace SmartSchoolAPI.v1.DTO_s
{
    public class CursoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<DisciplinaDto>? Disciplinas { get; set; }
    }
}
