namespace Common.DataTransferObjects.Authentication
{
    public class AuthIdentity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;

        public AuthIdentity(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

        public AuthIdentity() { }
    }
}
