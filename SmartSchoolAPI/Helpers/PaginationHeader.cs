namespace SmartSchoolAPI.Helpers
{
    public class PaginationHeader
    {
        public int CurrentPage { get; set; }
        public int ItensPerPage { get; set; }
        public int TotalItens { get; set; }
        public int TotalPages { get; set; }
    
    
        public PaginationHeader(int currentPage, int itensPerPage, int totalItens, int totalPages)
        {
            CurrentPage = currentPage;
            ItensPerPage = itensPerPage;
            TotalItens = totalItens;
            TotalPages = totalPages;
        }
    }
}
