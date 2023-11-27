using System.ComponentModel.DataAnnotations.Schema;

namespace PudgeManga_Project.Models
{
    public class AnimeEpisode
    {
        public int EpisodeId { get; set; }

        [ForeignKey("AnimeSeason")]
        public int SeasonId { get; set; }


        public AnimeSeason AnimeSeason { get; set; }
    }
}
