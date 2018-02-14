using System;
using System.Threading.Tasks;
using Germaine.Models;
using Microsoft.EntityFrameworkCore;

namespace Germaine.Data
{
    public class AuthRepository : IAuthRepository
    {
        
        private readonly DataContext _context;
        
        public AuthRepository(DataContext context)
        {
            _context = context;            
        }

        public async Task<User> Login(string username, string password)
        {
            //throw new System.NotImplementedException();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if(user == null)
                return null;
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            
            //login successful
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if(computeHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<Person> Profile(int id)
        {
            //throw new System.NotImplementedException();

            var user = await _context.Persons.FirstOrDefaultAsync(x => x.PersonID == id);
            
            //get profile
            return user;
        }

        public void ActivityLog(int id,string ip)
        {
            //throw new System.NotImplementedException();
            Activity user = new Activity();
                user.IPAddress = ip;
                user.TimeLoggedIn = DateTime.Now;
                // user.TimeLoggedOut = DateTime.Now;
                user.UserID = id;            

                _context.Activities.Add(user);
                _context.SaveChanges();
        }

        
        public async Task<User> Register(User user, string password)
        {
            // throw new System.NotImplementedException();
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PersonID = 3;
            user.Modules = "[1,2,2,2]";
            user.Role = "Admin";
            user.Token = "ASC3527HSDTEJSD7843YR";
            user.CreatedAt = DateTime.Now;
            user.ProfilePic = "https://picsum.photos/200/300/?random";

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // ActivityLog(user.UserID,ip);

            return user;
        }       

        public async Task<bool> UserExists(string username)
        {
            // throw new System.NotImplementedException();
            if(await _context.Users.AnyAsync(x => x.Username == username))
                return true;
                
            return false;
        }

        public  bool Password(User user, string password)
        {
            //password hashing 
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            
            // updateData.UserID = id;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;         
            user.PasswordReset = 1;

            _context.Update<User>(user);
            _context.SaveChanges();
            return true;
        }


         private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}