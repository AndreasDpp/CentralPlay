namespace CentralPlay.Backend.Service.DTO
{
    public class StorageBlobResponseDTO
    {
        public string? Status { get; set; }

        public bool Error { get; set; }

        public StorageBlobDTO Blob { get; set; }

        public StorageBlobResponseDTO()
        {
            Blob = new StorageBlobDTO();
        }
    }
}
