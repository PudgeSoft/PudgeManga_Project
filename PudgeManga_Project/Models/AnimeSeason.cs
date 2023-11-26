namespace PudgeManga_Project.Models
{
    public class AnimeSeason
    {

        public int SeasonId { get; set; }
        public int AnimeId { get; set; }
        public int SeasonNumber { get; set; }

        public ICollection<AnimeEpisode> AnimeEpisodes { get; set; }
        public Anime Anime { get; set; }

    }
}
