using Microsoft.EntityFrameworkCore;
using PudgeManga_Project.Data;
using PudgeManga_Project.Helpers;
using PudgeManga_Project.Interfaces;

namespace PudgeManga_Project.Models.Repositories
{
    public class GoogleDriveAPIRepository : IGoogleDriveAPIRepository<IFormFile>
    {
        private readonly ApplicationDBContext _context;
        public GoogleDriveAPIRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task AddFileLinksToPagesWithChapters(IEnumerable<string> modifiedPhotoLinks, int chapterId)
        {
            var chapter = await _context.Chapters
                .Include(p => p.Pages)
                .FirstOrDefaultAsync(p => p.ChapterId == chapterId);
            if (chapter != null)
            {
                int pageNumberCounter = 1;
                foreach (var fileLink in modifiedPhotoLinks)
                {
                    var newPage = new Page { ImageUrl = fileLink, ChapterId = chapterId, PageNumber = pageNumberCounter };
                    chapter.Pages.Add(newPage);
                    pageNumberCounter++;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddFileLinksToAnimeEpisodesWithSeasons(IEnumerable<string> modifiedPhotoLinks, int seasonId)
        {
            var season = await _context.AnimeSeasons
                .Include(ep => ep.AnimeEpisodes)
                .FirstOrDefaultAsync(p => p.AnimeSeasonId == seasonId);
            if (season != null)
            {
                int episodeNumberCounter = 1;
                foreach (var fileLink in modifiedPhotoLinks)
                {
                    var newEpisode = new AnimeEpisode { EpisodeUrl = fileLink, SeasonId = seasonId, EpisodeNumber = episodeNumberCounter };
                    season.AnimeEpisodes.Add(newEpisode);
                    episodeNumberCounter++;
                }
                await _context.SaveChangesAsync();
            }
        }

        public List<string> GetModifiedFileLinks(string folderId)
        {
            List<string> photoLinks = GoogleDriveAPIHelper.GetPhotoLinksInFolder(folderId);

            List<string> modifiedPhotoLinks = GoogleDriveAPIHelper.ModifyDriveUrls(photoLinks);

            return modifiedPhotoLinks;
        }

        public string UploadFileToGoogleDrive(IFormFile file, string folderId)
        {
            var service = GoogleDriveAPIHelper.GetService();

            var driveFile = new Google.Apis.Drive.v3.Data.File
            {
                Name = file.FileName,
                Parents = new List<string> { folderId } // Використовуйте тут ідентифікатор загальної теки
            };

            using (var stream = file.OpenReadStream())
            {
                var request = service.Files.Create(driveFile, stream, file.ContentType);

                var uploadResponse = request.Upload();
            }

            return folderId;
        }
        public string UploadFileStreamToGoogleDrive(Stream fileStream, string fileName, string folderId)
        {
            var service = GoogleDriveAPIHelper.GetService();

            var driveFile = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName,
                Parents = new List<string> { folderId } // Використовуйте тут ідентифікатор загальної теки
            };

            var request = service.Files.Create(driveFile, fileStream, "application/octet-stream");

            var uploadResponse = request.Upload();

            return folderId;
        }
        public string GetOrCreateFolder(string folderName)
        {
            try
            {
                var service = GoogleDriveAPIHelper.GetService();

                // Перевірте, чи тека вже існує
                var existingFolder = service.Files.List().Execute().Files.FirstOrDefault(f => f.Name == folderName && f.MimeType == "application/vnd.google-apps.folder");

                if (existingFolder != null)
                {
                    // Тека вже існує, поверніть її ідентифікатор
                    return existingFolder.Id;
                }
                else
                {
                    // Тека не існує, створіть нову
                    var driveFolder = new Google.Apis.Drive.v3.Data.File
                    {
                        Name = folderName,
                        MimeType = "application/vnd.google-apps.folder",
                        Parents = new List<string> { null }
                    };

                    var createFolderRequest = service.Files.Create(driveFolder);

                    var createdFolder = createFolderRequest.Execute();

                    return createdFolder.Id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при створенні/отриманні теки: {ex.Message}");
                return null;
            }
        }


    }

}
