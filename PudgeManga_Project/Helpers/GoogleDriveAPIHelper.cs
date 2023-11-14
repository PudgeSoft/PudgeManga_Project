﻿using Google.Apis.Auth.OAuth2.Flows;
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
        public static DriveService GetService()
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = "ya29.a0AfB_byC8DcEEOu41IO34_JdhqVRBN8ie3WWVep7k3wgp0Bp9pFLlYeO-EvRDIskkKB2kfoI11lgTk2OWnZAD2gECBF9lk1s34KEIVu4XSlmencAaC0rF7y1UX-_nRNNnrQ9ICTnDkpqUpC7ReHK77tmpE17KRtEfcTvVaCgYKAWYSARASFQHGX2Mi_H8f7UAnbyyNbEplkm-XUw0171",
                RefreshToken = "1//049yfNpQ37dctCgYIARAAGAQSNwF-L9IrMp8o00sokY6vH7PFqfzh1A8m5W0_hkoTZla1Kt3uKlG2p06Fh5b2WVEf4nqZOC-riUo"
            };

            var applicationName = "googleDrive test"; // Використовуйте назву проекту в Google Cloud
            var username = "melnyk.vladyslav2@lll.kpi.ua"; // Використовуйте свій електронний адрес

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

        public static string CreateFolder(string parent, string folderName)
        {
            try
            {
                var service = GetService();

                // Створення об'єкта, що представляє папку в Google Drive
                var driveFolder = new Google.Apis.Drive.v3.Data.File
                {
                    Name = folderName,
                    MimeType = "application/vnd.google-apps.folder",
                    Parents = new List<string> { parent }
                };

                // Створення команди для створення папки
                var createFolderRequest = service.Files.Create(driveFolder);

                // Виконання команди та отримання інформації про створений файл (папку)
                var createdFolder = createFolderRequest.Execute();

                // Повернення ідентифікатора створеної папки
                return createdFolder.Id;
            }
            catch (Exception ex)
            {
                // Обробка помилок (виведення або логування)
                Console.WriteLine($"Error creating folder: {ex.Message}");
                return null; // або киньте власний виняток, відповідно до вашого сценарію
            }
        }

        private static List<string> GetPhotoLinksInFolder(string folderId)
        {
            try
            {
                var service = GetService();

                // Запит для отримання файлів у вказаній папці
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.Q = $"'{folderId}' in parents"; // Обмеження результатів до вказаної папки
                listRequest.Fields = "files(id, name, webViewLink)"; // Отримати лише необхідні поля

                // Виклик запиту і отримання результатів
                FileList fileList = listRequest.Execute();

                // Створення списку посилань на фото
                List<string> photoLinks = new List<string>();

                // Додавання посилань на фото до списку
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
                Console.WriteLine($"Помилка при отриманні файлів: {ex.Message}");
                return null;
            }
        }


        private static List<string> ModifyDriveUrls(List<string> originalUrls)
        {
            try
            {
                // Створення списку для зберігання модифікованих посилань
                List<string> modifiedUrls = new List<string>();

                foreach (var originalUrl in originalUrls)
                {
                    Uri uri = new Uri(originalUrl);
                    string fileId = GetFileIdFromUrl(uri);

                    // Побудова нового URL зі зміненим ідентифікатором
                    string modifiedUrl = $"https://drive.google.com/uc?export=view&id={fileId}";

                    // Видалення зайвого символу "/" в кінці URL, якщо він присутній
                    modifiedUrl = modifiedUrl.TrimEnd('/');

                    // Додавання модифікованого посилання до списку
                    modifiedUrls.Add(modifiedUrl);
                }

                return modifiedUrls;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при модифікації посилань: {ex.Message}");
                return originalUrls;
            }
        }

        private static string GetFileIdFromUrl(Uri uri)
        {
            // Отримання ідентифікатора файлу з URL
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

