using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace PudgeManga_Project.Helpers
{
    public class GoogleDriveAPIHelper
    {
        public static async Task<DriveService> GetServiceAsync()
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = "ya29.a0AfB_byC8DcEEOu41IO34_JdhqVRBN8ie3WWVep7k3wgp0Bp9pFLlYeO-EvRDIskkKB2kfoI11lgTk2OWnZAD2gECBF9lk1s34KEIVu4XSlmencAaC0rF7y1UX-_nRNNnrQ9ICTnDkpqUpC7ReHK77tmpE17KRtEfcTvVaCgYKAWYSARASFQHGX2Mi_H8f7UAnbyyNbEplkm-XUw0171",
                RefreshToken = "1//049yfNpQ37dctCgYIARAAGAQSNwF-L9IrMp8o00sokY6vH7PFqfzh1A8m5W0_hkoTZla1Kt3uKlG2p06Fh5b2WVEf4nqZOC-riUo"
            };

            var applicationName = "googleDrive test"; 
            var username = "melnyk.vladyslav2@lll.kpi.ua"; 

            var clientSecrets = new ClientSecrets
            {
                ClientId = "150486450119-mkabhooict2nq0blfg797sqrrvda6pro.apps.googleusercontent.com",
                ClientSecret = "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"
            };

            var scopes = new[] { DriveService.Scope.Drive };

            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = clientSecrets,
                Scopes = scopes,
                DataStore = new FileDataStore(applicationName)
            });

            var credential = new UserCredential(flow, username, tokenResponse);

            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            return service;
        }

        public static async Task<string> CreateFolderAsync(string folderName)
        {
            try
            {
                var service = await GetServiceAsync();

                var driveFolder = new Google.Apis.Drive.v3.Data.File
                {
                    Name = folderName,
                    MimeType = "application/vnd.google-apps.folder",
                    Parents = new List<string> { null }
                };

                var createFolderRequest = service.Files.Create(driveFolder);

                var createdFolder = await createFolderRequest.ExecuteAsync();

                return createdFolder.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating folder: {ex.Message}");
                return null;
            }
        }

        public static async Task<List<string>> GetPhotoLinksInFolderAsync(string folderId)
        {
            try
            {
                var service = await GetServiceAsync();

                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.Q = $"'{folderId}' in parents";
                listRequest.Fields = "files(id, name, webViewLink)";

                FileList fileList = await listRequest.ExecuteAsync();

                List<string> photoLinks = new List<string>();

                if (fileList.Files != null)
                {
                    foreach (var file in fileList.Files)
                    {
                        photoLinks.Add(file.WebViewLink);
                    }
                }

                return photoLinks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting files: {ex.Message}");
                return null;
            }
        }



        public static async Task<List<string>> ModifyDriveUrlsAsync(List<string> originalUrls)
        {
            try
            {
                List<string> modifiedUrls = new List<string>();

                foreach (var originalUrl in originalUrls)
                {
                    Uri uri = new Uri(originalUrl);
                    string fileId = GetFileIdFromUrl(uri);

                    string modifiedUrl = $"https://drive.google.com/uc?export=view&id={fileId}";

                    modifiedUrl = modifiedUrl.TrimEnd('/');

                    modifiedUrls.Add(modifiedUrl);
                }

                return modifiedUrls;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error modifying URLs: {ex.Message}");
                return originalUrls;
            }
        }

        public static async Task<string> GetOrCreateFolderIdAsync(string folderName)
        {
            try
            {
                var service = await GetServiceAsync();

                // Пошук папки за ім'ям та батьківським ID
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.Q = $"name='{folderName}' and mimeType='application/vnd.google-apps.folder'";
                listRequest.Fields = "files(id)";
                FileList fileList = await listRequest.ExecuteAsync();

                if (fileList.Files.Count > 0)
                {
                    // Папка знайдена, повертаємо ідентифікатор
                    return fileList.Files[0].Id;
                }
                else
                {
                    // Папка не знайдена, створюємо нову
                    return await CreateFolderAsync(folderName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting/creating folder: {ex.Message}");
                return null;
            }
        }

        public static string GetFileIdFromUrl(Uri uri)
        {
            string[] segments = uri.Segments;
            int indexOfD = Array.IndexOf(segments, "d/");

            if (indexOfD >= 0 && indexOfD + 1 < segments.Length)
            {
                return segments[indexOfD + 1];
            }

            throw new InvalidOperationException("Invalid Google Drive URL");
        }

    }
}

