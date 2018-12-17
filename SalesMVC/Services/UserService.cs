using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SalesMVC.Models;
using SalesMVC.Helpers;

namespace SalesMVC.Services
{
    public interface IUserService
    {
        Task<UserSys> Authenticate(string username, string password);
        Task<IEnumerable<UserSys>> GetAll();
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<UserSys> _users = new List<UserSys>
        {
            new UserSys { Id = 1, Email = "Test", Login = "test", Password = "test" }
        };

        public async Task<UserSys> Authenticate(string email, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Email == email && x.Password == password));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            user.Password = null;
            return user;
        }

        public async Task<IEnumerable<UserSys>> GetAll()
        {
            // return users without passwords
            return await Task.Run(() => _users.Select(x => {
                x.Password = null;
                return x;
            }));
        }
    }
}