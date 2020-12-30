using BL.Abstract;
using Entities;
using Entities.Dto;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Manager
{
    public class UserManager : IUserManager
    {
        private readonly DAL.Abstract.IUserRepository _userRepository;

        public UserManager()
        {
            _userRepository = new DAL.UserRepository();
        }

        public async Task<List<Usertable>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<Usertable> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }


        public async Task<Usertable> GetUserUsernameAndPassword(string username, string password)
        {
            if( username == null || password == null)
                return null;
            else
                return await _userRepository.GetUserUsernameAndPassword(username, password);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task<Usertable> InsertUser(RegisterDto user)
        {
            List<Usertable> userList = new List<Usertable>();
            userList = await GetAllUsers();
            if ( user.Email == null || user.Firstname == null || user.Lastname == null || user.Username == null || user.Pwd == null)
            {
                return null;
            }
            else
            {
                foreach( var item in userList)
                {
                    if( user.Username == item.Username)
                    {
                        return null;
                    }
                }
                Usertable newUser = new Usertable();
                newUser.Firstname = user.Firstname;
                newUser.Lastname = user.Lastname;
                newUser.Email = user.Email;
                newUser.Username = user.Username;
                newUser.Pwd = user.Pwd;
                return await _userRepository.InsertUser(newUser);
            }
        }

        public async Task<Usertable> UpdateUser(Usertable user)
        {
            return await _userRepository.UpdateUser(user); 
        }

        

    }
}
