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
        public async Task AddFileLinksToPagesWithChaptersAsync(IEnumerable<string> modifiedPhotoLinks, int chapterId)
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

        public async Task<List<string>> GetModifiedFileLinksAsync(string folderId)
        {
            List<string> photoLinks = await GoogleDriveAPIHelper.GetPhotoLinksInFolderAsync(folderId);

            List<string> modifiedPhotoLinks = await GoogleDriveAPIHelper.ModifyDriveUrlsAsync(photoLinks);

            return modifiedPhotoLinks;
        }

        public async Task<string> UploadFileToGoogleDriveAsync(IFormFile file, string folderName)
        {
            var service = await GoogleDriveAPIHelper.GetServiceAsync();

            var folderId = await GoogleDriveAPIHelper.GetOrCreateFolderIdAsync(folderName);

            var driveFile = new Google.Apis.Drive.v3.Data.File
            {
                Name = file.FileName,
                Parents = new List<string> { folderId }
            };

            using (var stream = file.OpenReadStream())
            {
                var request = service.Files.Create(driveFile, stream, file.ContentType);

                var uploadResponse = await request.UploadAsync();
            }

            return folderId;
        }


    }

}
