namespace CloudPOS.Services
{
    public interface IUserService
    {
        Task<string> CreateUser(string username, string email);
    }
}
