using System.Threading.Tasks;
using Germaine.Models;

namespace Germaine.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string username, string password);

        bool Password(User user, string password);

        Task<Person> Profile(int id);

        Task<bool> UserExists(string username);

        void ActivityLog(int id, string ip);

    }
}