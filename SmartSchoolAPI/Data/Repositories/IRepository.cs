using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Data.Repositories
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class; 
        bool SaveChanges();
    }
}
