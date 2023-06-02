namespace ChatWebApi.Contracts.Message
{
    public class GetMessageBySizeResponse
    {
        public DateTime SendDateTime { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
