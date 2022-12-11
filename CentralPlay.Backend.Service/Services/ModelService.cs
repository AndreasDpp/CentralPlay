using AutoMapper;
using CentralPlay.Backend.Repository.Domain.Entities;
using CentralPlay.Backend.Repository.Enums;
using CentralPlay.Backend.Repository.Interfaces;
using CentralPlay.Backend.Service.DTO;
using CentralPlay.Backend.Service.Interfaces;

namespace CentralPlay.Backend.Service.Services
{
    public class ModelService : IModelService
    {
        #region Fields

        private readonly IModelRepository _modelRepository;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="modelRepository"></param>
        /// <param name="storageService"></param>
        /// <param name="mapper"></param>
        public ModelService(IModelRepository modelRepository,
            IStorageService storageService
            , IMapper mapper)
        {
            _modelRepository = modelRepository;
            _storageService = storageService;
            _mapper = mapper;
        }

        #endregion

        public async Task<ModelDTO> GetByIdAsync(string id)
        {
            var query = _modelRepository.GetQueryable()
                .Where(m => m.Id == id);

            var result = await _modelRepository.GetEnumerableAsync(query);

            return _mapper.Map<ModelDTO>(result.SingleOrDefault());
        }

        public async Task<IEnumerable<ModelDTO>> GetByUserId(string id)
        {
            var query = _modelRepository.GetQueryable()
                .Where(m => m.UserId == id);

            var result = await _modelRepository.GetEnumerableAsync(query);

            return _mapper.Map<IEnumerable<ModelDTO>>(result);
        }

        public async Task AddAsync(ModelDTO modelDTO)
        {
            var model = _mapper.Map<Model>(modelDTO);

            await _modelRepository.AddAsync(model);

            modelDTO.Id = model.Id;
        }

        public async Task UpdateValidState(string id, ModelValidationEnum state)
        {
            var model = await GetByIdAsync(id);

            model.ValidFile = state;

            await _modelRepository.UpdateAsync(model.Id, _mapper.Map<Model>(model));
        }

        public async Task<ModelFileDTO> GetModelFileAsync(string fileName, string userId)
        {
            var result = new ModelFileDTO();

            var model = await GetByIdAsync(fileName.Split(".")[0]);

            if (model == null)
            {
                result.Error = true;
                result.Status = "Invalid model";
                return result;
            }
            else if (model.ValidFile != ModelValidationEnum.Valid)
            {
                result.Error = true;
                result.Status = "Invalid file";
                return result;
            }

            // Checks if the user owns the file
            if (model.UserId == userId)
            {
                var blobResponse = await _storageService.DownloadAsync(fileName);

                if(!blobResponse.Error)
                {
                    result.Blob = blobResponse.Blob.Content;
                    return result;
                }

                result.Error = true;
                result.Status = blobResponse.Status;
            }
            else
            {
                result.Error = true;
                result.Status = "Forbidden access";
            }

            return result;
        }
    }
}
