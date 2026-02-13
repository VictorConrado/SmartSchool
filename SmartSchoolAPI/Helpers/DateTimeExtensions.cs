using AutoMapper;

namespace SmartSchoolAPI.Helpers
{
    public static class DateTimeExtensions
    {
      public static int GetIdadeAtual(this DateTime dateTime)
        {
            var dataAtual = DateTime.UtcNow;
            int idade = dataAtual.Year - dateTime.Year;

            if (dataAtual < dateTime.AddYears(idade))
                idade--;
            
            return idade;
        }
    }
}
