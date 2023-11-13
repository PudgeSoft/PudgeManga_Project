namespace PudgeManga_Project.Interfaces
{
	public interface IAdminChapterRepository<T1, T2> where T1 : class
    {
        Task<T1> GetById(T2 id);
        Task<T1> Add(T1 entity);
        Task AddChapterToMangaAsync(T2 id,T1 entity);
        Task Delete(T1 entity);
        Task UpdateAsync(T1 entity);
        Task<IEnumerable<T1>> GetChaptersForManga(int T2);
    }
}
