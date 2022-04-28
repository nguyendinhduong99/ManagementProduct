using PM.Data.Models;
using PM.Data.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.Repository.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        User Authenticate(string username, string password);
        User Register(string username, string password);

        bool UserExist(string username);
        bool UserExist(int id);
        ICollection<User> GetUsers(string sortBy, string searchString, int? pageNumber);
        User GetUserById(int userId);
        bool UpdateUser(User user);
        bool UpdatePass(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}