using CentralPlay.Backend.Repository.Enums;
using CentralPlay.Backend.Service.DTO;

namespace CentralPlay.Backend.Service.Interfaces
{
    public interface IModelService
    {
        /// <summary>
        /// Gets the model using the id.
        /// </summary>
        /// <param name="id">The id of the object (not the userId)</param>
        /// <returns>The requested model object</returns>
        Task<ModelDTO> GetByIdAsync(string id);

        /// <summary>
        /// Gets the models binded to the user, by the user id.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>The requested user object</returns>
        Task<IEnumerable<ModelDTO>> GetByUserId(string id);

        /// <summary>
        /// Adds the model to the container
        /// </summary>
        /// <param name="modelDTO">The model to add</param>
        /// <returns>Nothing if everything goes well. An exception if anything went wrong</returns>
        Task AddAsync(ModelDTO modelDTO);

        /// <summary>
        /// Updates the state of the validation of the tile
        /// </summary>
        /// <param name="id">The id of the model</param>
        /// <param name="state">The new state of the file validation</param>
        /// <returns>Nothing if everything goes well. An exception if anything went wrong</returns>
        Task UpdateValidState(string id, ModelValidationEnum state);

        /// <summary>
        /// Validates if the user owns the file and gets the file from the storage.
        /// </summary>
        /// <param name="filename">The name of the file with extension</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Error message or the blob file stream</returns>
        Task<ModelFileDTO> GetModelFileAsync(string filename, string userId);
    }
}
