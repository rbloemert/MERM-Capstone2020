using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Azure;
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

    public class StorageService
    {
        // we should REALLY centralize connection strings somewhere, not leave them lying about!!!
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
        public static Response<ShareFileUploadInfo> UploadFileToStorage(Stream fileStream, string fileName)
        {
            ShareFileClient cl = new ShareFileClient(connectionString, "projectcreator", fileName);
            cl.Create(fileStream.Length);
            fileStream.Position = 0;
            return cl.Upload(fileStream); ;
        }

        public static async Task<bool> UploadFileToStorageAsync(Stream fileStream, string fileName)
        {
            ShareFileClient cl = new ShareFileClient(connectionString, "projectcreator", fileName);
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

    }
}