using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PudgeManga_Project.Models
{
    public class AnimeEpisode
    {
        [Key]
        public int AnimeEpisodeId { get; set; }

        [ForeignKey("AnimeSeason")]
        public int SeasonId { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeUrl { get; set; }
        public AnimeSeason AnimeSeason { get; set; }
    }
}
