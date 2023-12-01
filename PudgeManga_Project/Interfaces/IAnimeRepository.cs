namespace PudgeManga_Project.Interfaces
{
    public interface IAnimeRepository<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetAllAsync();
        Task<T1> GetByIdAsync(T2 id);
    }
}
