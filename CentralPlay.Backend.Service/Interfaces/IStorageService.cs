using CentralPlay.Backend.Service.DTO;
using Microsoft.AspNetCore.Http;

namespace CentralPlay.Backend.Service.Interfaces
{
    public interface IStorageService
    {
        /// <summary>
        /// This method returns a list of all files located in the container
        /// </summary>
        /// <returns>Blobs in a list</returns>
        Task<List<StorageBlobDTO>> ListAsync();

        /// <summary>
        /// This method uploads a file submitted with the request
        /// </summary>
        /// <param name="file">File for upload</param>
        /// <returns>Blob with status</returns>
        Task<StorageBlobResponseDTO> UploadAsync(IFormFile file);

        /// <summary>
        /// This method downloads a file with the specified filename
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <returns>Blob</returns>
        Task<StorageBlobResponseDTO> DownloadAsync(string fileName);

        /// <summary>
        /// This method deleted a file with the specified filename
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <returns>Blob with status</returns>
        Task<StorageBlobResponseDTO> DeleteAsync(string fileName);


    }
}
