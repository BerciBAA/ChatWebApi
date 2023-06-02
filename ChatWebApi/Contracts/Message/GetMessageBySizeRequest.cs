namespace ChatWebApi.Contracts.Message
{
    public class GetMessageBySizeRequest
    {
        public Guid FromId { get; set; }

        public Guid ToId { get; set; }

        public int Page { get; set; }

        public int Size { get; set; }
    }
}
