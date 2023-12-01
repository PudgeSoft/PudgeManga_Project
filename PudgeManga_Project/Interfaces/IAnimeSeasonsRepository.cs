namespace PudgeManga_Project.Interfaces
{
    public interface IAnimeSeasonsRepository<T1, T2> where T1 : class
    {
        Task<IEnumerable<T1>> GetSeasonsForAnimeAsync(int T2);
        Task<int> GetTotalAnimeSeasonsAsync(int mangaId);
    }
}
