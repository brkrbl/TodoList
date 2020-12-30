using Entities;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Abstract
{
    public interface IUserManager
    {
        public Task<List<Usertable>> GetAllUsers();
        public Task<Usertable> GetUserById(int id);
        public Task<Usertable> GetUserUsernameAndPassword(string username, string password);
        public Task DeleteUser(int id);
        public Task<Usertable> InsertUser(RegisterDto user);
        public Task<Usertable> UpdateUser(Usertable user);
    }
}
