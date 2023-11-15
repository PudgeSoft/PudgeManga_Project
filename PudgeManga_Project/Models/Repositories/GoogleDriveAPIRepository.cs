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
        public async Task AddPhotoLinksToPagesWithChapters(IEnumerable<string> modifiedUrls, int chapterId)
        {
            throw new NotImplementedException();
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
