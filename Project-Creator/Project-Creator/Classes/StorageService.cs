using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;

namespace Project_Creator
{
    // Not sure what to call this, but this handles all file functionality
    // https://docs.microsoft.com/en-us/azure/storage/blobs/storage-upload-process-images?tabs=dotnet
    // This class is static, eliminating the need to create a new instance of the class when the class' methods are needed..

    // If you get any errors here, you'll need the NuGet package 'Azure.Storage.File.Shares'
    // Right click 'Project-Creator', select Manage NuGet packages

    //! Azure Storage interface class.
    /*!
     *  Used to interface with the Azure Blob Storage service.
     */
    public class StorageService
    {
        public static string baseUrl = "https://mjackson9891.blob.core.windows.net/"; //!< The URL which connents to Azure Storage.
        public static string account_image = "account-image"; //!< The folder name for account image storage.
        public static string project_image = "project-image"; //!< The folder name for project image storage.
        public static string timeline_image = "timeline-image"; //!< The folder name for timeline image storage.
        public static string timeline_file = "timeline-file"; //!< The folder name for timeline files storage.
        public static string temp_storage = "temp-storage"; //!< The folder name for temporary storage.
        // we should REALLY centralize connection strings somewhere, not leave them lying about!!!
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=mjackson9891;AccountKey=K97XpZJ95OzGG1UuUSmtfw0pv+cYo8e7N9uHVZSXrKHS+lYZqBLx4cd2mdH/xBwzJnNnVRUHwUt7HMm7BZ7cgw==;EndpointSuffix=core.windows.net"; //!< The connection string to Azure Storage.

        /*returns true if uploaded, false if not*/
        /*!
         *  Uploads a file to Azure Storage.
         *  @param fileStream the file information to upload
         *  @param fileName the name of the file being uploaded
         *  @param fileContainer the folder to upload the file to
         *  @param contentType the type of file being uploaded
         *  @return the URL to the file uploaded otherwise null
         */
        public static string UploadFileToStorage(Stream fileStream, string fileName, string fileContainer, string contentType)
        {
            switch (fileContainer) {
                case ("account-image"):
                case ("project-image"):
                case ("timeline-image"):
                case ("timeline-file"):
                case ("temp-storage"):
                    BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(fileContainer);
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);
                    try {
                        blobClient.Upload(fileStream, overwrite: true);
                        blobClient.SetHttpHeadersAsync(new BlobHttpHeaders {
                            ContentType = contentType
                        });
                        return (StorageService.baseUrl + fileContainer + "/" + fileName);
                    } catch {
                        return (null);
                    }
                default:
                    return null;
            }
            
        }

        /*!
         *  Deletes a file from Azure Storage.
         *  @param fileName the name of the file to delete.
         *  @param fileContainer the folder to find the file to delete
         *  @return true if deleted otherwise false
         */
        public static bool DeleteFileFromStorage(string fileName, string fileContainer) {
            switch (fileContainer) {
                case ("account-image"):
                case ("project-image"):
                case ("timeline-image"):
                case ("timeline-file"):
                case ("temp-storage"):
                    BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(fileContainer);
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);
                    try {
                        return (blobClient.DeleteIfExists());
                    } catch {
                        return (false);
                    }
                default:
                    return false;
            }
        }


        /*methods for fileshare upload
        /*
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=projectcreatorstorage;AccountKey=8RPokT/yDpeXw67OPX0o/b2ml+mMMjdUVaOVfzq6CZtIOiN6OWaSiaEvsPQ1p3W84AdkWLap9WiuVZ8DOsVKZw==;EndpointSuffix=core.windows.net";

        // One of the following paths MUST precede file names:
        // account_image
        // project_image
        // timeline_file
        // timeline_image
        //
        // example of valid path: "project_image/image1.png"
        // fileStream is a stream to the actual file iteself
        // fileName is what the file name should be on the server
        public static Response<ShareFileUploadInfo> UploadFileToStorage(Stream fileStream, string fileName) {
            ShareFileClient cl = new ShareFileClient(connectionString, "projectcreator", fileName);
            cl.Create(fileStream.Length);
            fileStream.Position = 0;
            return cl.Upload(fileStream); ;
        }

        public static async Task<bool> UploadFileToStorageAsync(Stream fileStream, string fileName)
        {
            ShareFileClient cl = new ShareFileClient(connectionString, "projectcreator", fileName);
            cl.Create(fileStream.Length);
            fileStream.Position = 0;
            await cl.UploadAsync(fileStream);
            return await Task.FromResult(true);
        }

        // You can access the downloaded file through returned value.Content
        public static Response<ShareFileDownloadInfo> DownloadFileFromStorage(string fileName)
        {
            ShareFileClient cl = new ShareFileClient(connectionString, "projectcreator", fileName);
            return cl.Download();
        }

        // commented out until a good way to report both progress and return downloaded files is found; out params disallowed for async operations
        //public static async Task<bool> DownloadFileFromStorageAsync(string fileName)
        //{
        //    ShareFileClient cl = new ShareFileClient(connectionString, "projectcreator", fileName);
        //    await cl.DownloadAsync();
        //    return await Task.FromResult(true);
        //}

        public static bool DoesFileExistOnStorage(string fileName)
        {
            ShareFileClient cl = new ShareFileClient(connectionString, "projectcreator", fileName);
            return cl.Exists();
        }
        */

    }
}