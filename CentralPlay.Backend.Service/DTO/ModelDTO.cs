using CentralPlay.Backend.Repository.Enums;
using CentralPlay.Backend.Service.DTO.Base;

namespace CentralPlay.Backend.Service.DTO
{
    public class ModelDTO: BaseDTO
    {
        public virtual string UserId { get; set; }

        public virtual string Name { get; set; }

        public virtual long FileSize { get; set; }

        public virtual string FileType { get; set; }

        public virtual ModelValidationEnum ValidFile { get; set; }
    }
}
