using ParkyAPI.Models;
using PM.Data.Models;
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
        User Register(string usernamem, string password);
    }
}
