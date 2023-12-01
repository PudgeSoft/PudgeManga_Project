namespace PudgeManga_Project.Interfaces
{
    public interface IGoogleDriveAPIRepository<T1> where T1 : IFormFile
    {
        Task AddFileLinksToPagesWithChaptersAsync(IEnumerable<string> modifiedPhotoLinks, int chapterId);
        Task<List<string>> GetModifiedFileLinksAsync(string folderId);
        Task<string> UploadFileToGoogleDriveAsync(T1 file, string folderName);
        
       
       
    }
}
