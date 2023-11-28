using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{
    public class AdminAnimeRepository : IAdminAnimeRepository<Anime, int>
    {
        public Task<Anime> Add(Anime entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Anime entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Anime>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Anime> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Anime entity)
        {
            throw new NotImplementedException();
        }
    }
}
