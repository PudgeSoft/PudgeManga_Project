namespace PudgeManga_Project.Models.Repositories
{
    public interface IMangaRepository<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetAll();
        Task<T1> GetById(T2 id);
       
    }
}
