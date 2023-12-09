using System.ComponentModel.DataAnnotations;

namespace PudgeManga_Project.Models
{
    public class AnimeSeason
    {
        [Key]
        public int AnimeSeasonId { get; set; }
        public int AnimeId { get; set; }
        public int SeasonNumber { get; set; }
        public string Title { get; set; }

        public ICollection<AnimeEpisode> AnimeEpisodes { get; set; }
        public ICollection<AnimeSeasonComment> AnimeSeasonComments { get; set; }
        public Anime Anime { get; set; }

    }
}
