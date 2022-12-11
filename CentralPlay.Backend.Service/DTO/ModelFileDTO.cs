namespace CentralPlay.Backend.Service.DTO
{
    public class ModelFileDTO
    {
        public string? Status { get; set; }

        public bool Error { get; set; }

        public Stream Blob { get; set; }

    }
}
