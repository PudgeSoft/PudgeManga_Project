using PudgeManga_Project.Models;

namespace PudgeManga_Project.ViewModels.AnimeViewModels
{
    public class AnimeDetailsViewModel
    {
        public Anime Anime { get; set; }
        public IEnumerable<AnimeSeason> AnimeSeasons { get; set; }
    }
}
