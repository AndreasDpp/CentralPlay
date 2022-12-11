using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CentralPlay.Backend.Repository.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CentralPlay.Backend.Repository.Repositories
{
    public class AzureBlobStorageRepository: IAzureBlobStorageRepository
    {
        #region Fields

        private readonly BlobContainerClient _blobContainerClient;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="blobContainerClient"></param>
        public AzureBlobStorageRepository(BlobContainerClient blobContainerClient)
        {
            _blobContainerClient = blobContainerClient;
        }

        #endregion

        public async Task<List<BlobItem>> ListAsync()
        {
            var files = new List<BlobItem>();

            await foreach (BlobItem file in _blobContainerClient.GetBlobsAsync())
            {
                files.Add(file);
            }

            return files;
        }

        public async Task UploadAsync(IFormFile file)
        {
            BlobClient client = _blobContainerClient.GetBlobClient(file.FileName);

            await using (Stream? data = file.OpenReadStream())
            {
                await client.UploadAsync(data);
            }
        }

        public async Task<BlobDownloadInfo> DownloadAsync(string fileName)
        {
            BlobClient file = _blobContainerClient.GetBlobClient(fileName);

            var data = (await file.DownloadAsync()).Value;
            return data;
        }

        public async Task DeleteAsync(string fileName)
        {
            BlobClient file = _blobContainerClient.GetBlobClient(fileName);
            await file.DeleteAsync();
        }
    }
}
