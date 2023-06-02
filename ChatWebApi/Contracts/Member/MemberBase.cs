namespace ChatWebApi
    
    .Contracts.Member
{
    public class MemberBase
    {
        public Guid MemberId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
