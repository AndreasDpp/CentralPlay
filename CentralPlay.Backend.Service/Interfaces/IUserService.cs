using CentralPlay.Backend.Service.DTO;

namespace CentralPlay.Backend.Service.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Gets the user using the id.
        /// </summary>
        /// <param name="id">The id of the object (not the userId)</param>
        /// <returns>The requested user object</returns>
        Task<UserDTO> GetById(string id);

        /// <summary>
        /// Adds the user to the container
        /// </summary>
        /// <param name="userDTO">The user to add</param>
        /// <returns>Nothing if everything goes well. An exception if anything went wrong</returns>
        Task AddAsync(UserDTO userDTO);

        /// <summary>
        /// Updates the amount of models "binded" to the user
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <param name="amountOfModelsAdded">The amount of models added (default = 1)</param>
        /// <returns>Nothing if everything goes well. An exception if anything went wrong</returns>
        Task UpdateCountAsync(string userId, int amountOfModelsAdded = 1);
    }
}
