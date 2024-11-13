using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPISchoolTest.Domain.AggregateModels.UserAggregate
{
    public interface IAuthRepository
    {
        Task<string> Register(User user);
        Task<string> Login(string username, string password);
    }
}
