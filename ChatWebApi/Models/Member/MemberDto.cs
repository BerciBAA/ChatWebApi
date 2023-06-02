using ChatWebApi.Models.Message;

namespace ChatWebApi.Models.Member
{
    public class MemberDto
    {
        public Guid MemberId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public DateTime? dateOfBirth { get; set; }

        public ICollection<MessageEntity> Messages { get; set; }
    }
}
