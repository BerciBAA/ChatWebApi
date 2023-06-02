using ChatWebApi.Models.Member;

namespace ChatWebApi.Models.Message
{
    public class MessageDto
    {
        public Guid MessageId { get; set; }
     
        public Guid FromId { get; set; }

        public Guid ToId { get; set; }

        public DateTime SendDateTime { get; set; }

        public string Message { get; set; } = string.Empty;

        public Guid MemberId { get; set; }

        public virtual MemberEntity Member { get; set; }
    }
}
