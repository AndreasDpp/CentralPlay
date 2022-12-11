namespace CentralPlay.Backend.Service.DTO
{
    public class StorageBlobDTO
    {
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public Stream? Content { get; set; }
    }
}
