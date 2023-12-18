namespace PudgeManga_Project.Interfaces
{
    public interface IAdminSeasonRepository<T1, T2> where T1 : class
    {
        Task<T1> GetByIdAsync(T2 id);
        Task<T1> AddAsync(T1 entity);
        Task AddSeasonsToAnimeAsync(T2 id, T1 entity);
        Task DeleteAsync(T1 entity);
        Task UpdateAsync(T1 entity);
        Task<IEnumerable<T1>> GetSeasonsForAnimeAsync(int T2);
    }
}
