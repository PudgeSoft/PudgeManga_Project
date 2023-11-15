namespace PudgeManga_Project.Interfaces
{
    public interface IGoogleDriveAPIRepository<T1> where T1 : IFormFile
    {
        Task AddPhotoLinksToPagesWithChapters(IEnumerable<string> modifiedUrls, int chapterId);
        Task<List<string>> GetModifiedPhotoLinks(string folderId);
        string UploadPhotoToGoogleDrive(T1 file, string folderName);
        
       
       
    }
}
