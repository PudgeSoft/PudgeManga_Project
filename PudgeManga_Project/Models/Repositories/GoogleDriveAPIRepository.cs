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
        public async Task AddPhotoLinksToPagesWithChapters(IEnumerable<string> modifiedPhotoLinks, int chapterId)
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

        public async Task<List<string>> GetModifiedPhotoLinks(string folderId)
        {
            List<string> photoLinks = GoogleDriveAPIHelper.GetPhotoLinksInFolder(folderId);

            List<string> modifiedPhotoLinks = GoogleDriveAPIHelper.ModifyDriveUrls(photoLinks);
            return modifiedPhotoLinks;
        }

        public string UploadPhotoToGoogleDrive(IFormFile file, string folderName)
        {
            string parent = null;
            var service = GoogleDriveAPIHelper.GetService();

            var folderId = GoogleDriveAPIHelper.CreateFolder(parent, folderName);
            
            var driveFile = new Google.Apis.Drive.v3.Data.File
            {
                Name = file.FileName,
                Parents = new List<string> { folderId }
            };


            using (var stream = file.OpenReadStream())
            {
                var request = service.Files.Create(driveFile, stream, file.ContentType);
                request.Upload();
            }
            return folderId;
        }
    }

}
