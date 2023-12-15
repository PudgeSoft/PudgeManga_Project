using System.ComponentModel.DataAnnotations;

namespace PudgeManga_Project.Models
{
    public class Anime
    {
        [Key]
        public int AnimeId { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public string ImageUrl {  get; set; }
        public string Director { get; set; }
        public string Studio { get; set; }
        public string Dubbing {  get; set; }
        public string Type {  get; set; }
        public int ReleaseYear { get; set; }

        public ICollection<AnimeSeason> AnimeSeasons { get; set; }
        public ICollection<AnimeGenre> AnimeGenres { get; set; }
        public ICollection<RatingForManga> Ratings { get; set; }

    }
}
