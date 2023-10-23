using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using TaskApiCosmos.Models;
using TaskApiCosmos.Services.Interfaces;

namespace TaskApiCosmos.Services
{
    public class BlobStorageManager : IStorageManager
    {
        public readonly BlobStorageOptions _storageOptions;

        public BlobStorageManager(IOptions<BlobStorageOptions> options)
        {
            _storageOptions = options.Value;
        }

        public bool DeleteFile(string fileName)
        {
            var serviceClient = new BlobServiceClient(_storageOptions.ConnectionString);
            var contaionerClient = serviceClient.GetBlobContainerClient(_storageOptions.ContainerName);
            var response = contaionerClient.DeleteBlob(fileName);

            if (response.Status == 202)
                return true;

            return false;
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var serviceClient = new BlobServiceClient(_storageOptions.ConnectionString);
            var contaionerClient = serviceClient.GetBlobContainerClient(_storageOptions.ContainerName);
            var response = await contaionerClient.DeleteBlobAsync(fileName);

            if (response.Status == 202)
                return true;

            return false;
        }

        public string GetSignedUrl(string fileName)
        {
            var serviceClient = new BlobServiceClient(_storageOptions.ConnectionString);
            var containerClient = serviceClient.GetBlobContainerClient(_storageOptions.ContainerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            var signedUrl = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTime.Now.AddHours(2)).AbsoluteUri;

            return signedUrl;
        }

        public string? UploadFile(Stream stream, string fileName, string contentType)
        {
            try
            {
                var fileId = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                var serviceClient = new BlobServiceClient(_storageOptions.ConnectionString);
                var containerClient = serviceClient.GetBlobContainerClient(_storageOptions.ContainerName);
                BlobClient blobClient = containerClient.GetBlobClient(fileId);

                using (stream)
                {
                    blobClient.Upload(stream, false);
                }

                return fileId;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<string?> UploadFileAsync(Stream stream, string fileName, string contentType)
        {
            try
            {
                var fileId = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                var serviceClient = new BlobServiceClient(_storageOptions.ConnectionString);
                var containerClient = serviceClient.GetBlobContainerClient(_storageOptions.ContainerName);
                BlobClient blobClient = containerClient.GetBlobClient(fileId);

                using (stream)
                {
                    await blobClient.UploadAsync(stream, false);
                }

                return fileId;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}