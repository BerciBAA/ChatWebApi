namespace ChatWebApi
    .Contracts.Member
{
    public class LoginMemberResponse : MemberBase
    {
        public string Jwt { get; set; } = string.Empty;
    }
}
