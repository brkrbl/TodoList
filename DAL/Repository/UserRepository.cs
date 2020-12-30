using DAL.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        public async Task<List<Usertable>> GetAllUsers()
        {
            using (var db = new DatabaseContext())
            {
                return await db.Usertables.ToListAsync();
            }
        }
        public async Task<Usertable> GetUserById(int id)
        {
            using (var db = new DatabaseContext())
            {
                return await db.Usertables.FindAsync(id);
            }
        }

        public async Task<Usertable> GetUserUsernameAndPassword(string username,string password)
        {
            using (var db = new DatabaseContext())
            {
                return await db.Usertables.FirstOrDefaultAsync(x => x.Username == username && x.Pwd == password);
            }
        }

        public async Task DeleteUser(int id)
        {
            
            using (var db = new DatabaseContext())
            {
                var deleteUser = await GetUserById(id);
                db.Usertables.Remove(deleteUser);
                await db.SaveChangesAsync();
            }
        }
        public async Task<Usertable> InsertUser(Usertable user)
        {
            using (var db = new DatabaseContext())
            {
                db.Usertables.Add(user);
                await db.SaveChangesAsync();
                return user;
            }
        }
        public async Task<Usertable> UpdateUser(Usertable user)
        {
            using (var db = new DatabaseContext())
            {
                db.Usertables.Update(user);
                await db.SaveChangesAsync();
                return user;
            }
        }
    }
}
