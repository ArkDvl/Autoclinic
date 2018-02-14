using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Germaine.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
// using System.Data.Entity;

namespace Germaine.Data
{
    public class GermaineRepository : IGermaineRepository
    {
        
        private readonly DataContext _context;
        
        public GermaineRepository(DataContext context)
        {
            _context = context;            
        }

        public void Add<T>(T entity) where T: class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T: class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        //users manipulation
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(x => x.Person).Include(x => x.Activity).ToListAsync();
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(x => x.Person).Include(x => x.Activity).FirstOrDefaultAsync(u => u.UserID == id);
            return user;
        }

    }
}