﻿using System.Security.Cryptography.X509Certificates;

namespace PudgeManga_Project.Models
{
    public class AnimeGenre
    {
        public int AnimeId {  get; set; }
        public Anime Anime { get; set; }

        public int GenreId {  get; set; }
        public GenreForAnime GenreForAnime {  get; set; }
    }
}
