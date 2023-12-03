namespace PudgeManga_Project.Interfaces
{
    public interface IGoogleDriveAPIRepository<T1> where T1 : IFormFile
    {
        Task AddFileLinksToPagesWithChapters(IEnumerable<string> modifiedPhotoLinks, int chapterId);
        List<string> GetModifiedFileLinks(string folderId);
        string UploadFileToGoogleDrive(T1 file, string folderName);
        string GetOrCreateFolder(string folderName);
    }

}
