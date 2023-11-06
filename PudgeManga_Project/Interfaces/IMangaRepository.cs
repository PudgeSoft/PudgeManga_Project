namespace PudgeManga_Project.Interfaces
{
    public interface IMangaRepository<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetAll();
        Task<T1> GetById(T2 id);
        Task<T1> GetByIdChapters(T2 id);
        Task<T1> GetByIdComments(T2 id);
        Task<T1> GetByIdReading(T2 id , int chapterNumber);
    }
}
