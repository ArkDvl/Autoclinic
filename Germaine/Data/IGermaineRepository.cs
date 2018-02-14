using System.Collections.Generic;
using System.Threading.Tasks;
using Germaine.Models;

namespace Germaine.Data
{
    public interface IGermaineRepository
    {
        void Add<T>(T entity) where T: class;

        void Delete<T>(T entity) where T: class;

        Task<bool> SaveAll();

        //users manipulation
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUser(int id);

    }
}