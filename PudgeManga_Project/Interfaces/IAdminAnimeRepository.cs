namespace PudgeManga_Project.Interfaces
{
    public interface IAdminAnimeRepository<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetAllAsync();
        Task<T1> GetByIdAsync(T2 id);
        Task<T1> AddAsync(T1 entity);
        Task DeleteAsync(T1 entity);
        Task UpdateAsync(T1 entity);
        Task SaveAsync();
    }
}
