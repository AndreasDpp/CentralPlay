using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace CentralPlay.Backend.Repository.Interfaces
{
    public interface IAzureBlobStorageRepository
    {
        Task<List<BlobItem>> ListAsync();

        Task UploadAsync(IFormFile file);

        Task<BlobDownloadInfo> DownloadAsync(string fileName);

        Task DeleteAsync(string fileName);
    }
}
