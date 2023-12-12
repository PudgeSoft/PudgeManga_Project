using PudgeManga_Project.Models;

namespace PudgeManga_Project.ViewModels.AnimeViewModels
{
    public class AnimeDetailsViewModel
    {
        public Anime Anime { get; set; }
        public List<AnimeSeason> Seasons { get; set; }
        public AnimeSeason SelectedSeason { get; set; }
        public List<AnimeEpisode> Episodes { get; set; }
        public double AverageRating { get; set; }
    }

}
