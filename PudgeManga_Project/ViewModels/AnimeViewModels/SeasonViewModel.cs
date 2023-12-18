namespace PudgeManga_Project.ViewModels.AnimeViewModels
{
    public class SeasonViewModel
    {
        public int SeasonId { get; set; }
        public int SeasonNumber { get; set; }
        public string Title { get; set; }
        public List<EpisodeViewModel> Episodes { get; set; }
    }
}
