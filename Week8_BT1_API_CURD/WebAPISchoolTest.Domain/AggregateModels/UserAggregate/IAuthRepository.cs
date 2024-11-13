namespace WebAPISchoolTest.Domain.AggregateModels.UserAggregate
{
    public interface IAuthRepository
    {
        Task<string> Register(User user);
        Task<string> Login(string username, string password);
    }
}
