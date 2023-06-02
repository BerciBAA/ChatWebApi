namespace ChatWebApi.Contracts.Member
{
    public class LoginMemberRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
