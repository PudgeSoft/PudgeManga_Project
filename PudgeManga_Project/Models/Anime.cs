namespace PudgeManga_Project.Models
{
    public class Anime
    {
        public int AnimeId { get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public string ImageUrl {  get; set; }
        public string Director { get; set; }
        public string Studio { get; set; }
        public string Dubbing {  get; set; }
        public string Type {  get; set; }
        public int ReleaseYear { get; set; }
        public int NumberOfEpisodes {  get; set; }
        public int GenreId {  get; set; }

        public ICollection<AnimeGenre> AnimeGenres { get; set; }

    }
}
