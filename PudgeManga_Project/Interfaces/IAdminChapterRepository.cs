namespace PudgeManga_Project.Interfaces
{
	public interface IAdminChapterRepository
	{
        Task<IEnumerable<T1>> GetAll();
        Task<T1> GetById(T2 id);
        Task<T1> Add(T1 entity);
        Task Delete(T1 entity);
        Task UpdateAsync(T1 entity);
        Task Save();
    }
}
