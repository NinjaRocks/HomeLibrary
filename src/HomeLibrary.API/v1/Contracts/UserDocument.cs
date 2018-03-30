namespace HomeLibrary.API.v1.Contracts
{
    public class UserDocument
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}