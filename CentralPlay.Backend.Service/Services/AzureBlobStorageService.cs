using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CentralPlay.Backend.Repository.Interfaces;
using CentralPlay.Backend.Service.DTO;
using CentralPlay.Backend.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CentralPlay.Backend.Service.Services
{
    public class AzureBlobStorageService : IStorageService
    {
        #region Fields

        private readonly IAzureBlobStorageRepository _azureBlobStorageRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="azureBlobStorageRepository"></param>
        public AzureBlobStorageService(IAzureBlobStorageRepository azureBlobStorageRepository)
        {
            _azureBlobStorageRepository = azureBlobStorageRepository;
        }

        #endregion

        public async Task<List<StorageBlobDTO>> ListAsync()
        {
            var files = new List<StorageBlobDTO>();

            var blobFiles = await _azureBlobStorageRepository.ListAsync();

            blobFiles.ForEach(blob => files.Add(new StorageBlobDTO
            {
                Name = blob.Name,
                ContentType = blob.Properties.ContentType
            }));

            return files;
        }

        public async Task<StorageBlobResponseDTO> UploadAsync(IFormFile file)
        {
            StorageBlobResponseDTO response = new();

            try
            {
                await _azureBlobStorageRepository.UploadAsync(file);

                response.Status = $"File {file.FileName} Uploaded Successfully";
                response.Error = false;
            }
            // If the file already exists, we catch the exception and do not upload it
            catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                response.Status = $"File with name { file.FileName } already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }
            // If we get an unexpected error, we catch it here and return the error message
            catch (RequestFailedException ex)
            {
                response.Status = $"Unexpected error: { ex.StackTrace }. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }

            return response;
        }

        public async Task<StorageBlobResponseDTO> DownloadAsync(string fileName)
        {
            StorageBlobResponseDTO response = new();

            try
            {
                var data = await _azureBlobStorageRepository.DownloadAsync(fileName);

                Stream blobContent = data.Content;

                response.Blob = new StorageBlobDTO { Content = blobContent, ContentType = data.ContentType };

            }
            catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                response.Status = $"File was not found with name { fileName }";
                response.Error = true;
                return response;
            }

            return response;
        }

        public async Task<StorageBlobResponseDTO> DeleteAsync(string fileName)
        {
            StorageBlobResponseDTO response = new();

            try
            {
                await _azureBlobStorageRepository.DeleteAsync(fileName);
                response.Status = $"File: {fileName} has been successfully deleted.";
            }
            catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                response.Status = $"File was not found with name { fileName }";
                response.Error = true;
                return response;
            }

            return response;
        }
    }
}
