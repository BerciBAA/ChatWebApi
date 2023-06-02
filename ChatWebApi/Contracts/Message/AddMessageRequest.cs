using ChatWebApi.Models.Member;
using Microsoft.AspNetCore.Mvc;

namespace ChatWebApi.Contracts.Message
{
    public class AddMessageRequest
    {

        public Guid FromId { get; set; }

        public Guid ToId { get; set; }

        public string Message { get; set; } = string.Empty;

    }
}
