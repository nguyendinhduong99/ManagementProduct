using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PM.Common.Paging;
using PM.Data.Data;
using PM.Data.Models;
using PM.Data.Models.Dtos;
using PM.Repository.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PM.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly AppSettings _app;
        public UserRepository(AppDbContext db, IOptions<AppSettings> app)
        {
            _db = db;
            _app = app.Value;
        }
        public User Authenticate(string username, string password)
        {
            var user = _db.Users.SingleOrDefault(n => n.UserName == username && n.Password == password);
            if (user == null) return null;

            //if founded => generate JWT Token
            //cbi tạo 1 biến xử lý token, key, description của thằng token
            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_app.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //mô tả token = {tiêu đề + ngày hết hạn + ký thông tin xác thực}
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //tạo token -> ghi token vừa ghi
            var token = tokenHandle.CreateToken(tokenDescriptor);
            user.Token = tokenHandle.WriteToken(token);
            user.Password = "éo cho xem :v";

            return user;
        }

        public bool DeleteUser(User user)
        {
            _db.Users.Remove(user);
            return Save();
        }

        public User GetUserById(int userId)
        {
            return _db.Users.FirstOrDefault(n => n.Id == userId);
        }

        public ICollection<User> GetUsers(string sortBy, string searchString, int? pageNumber)
        {
            var allData = _db.Users.OrderBy(n => n.UserName).ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "desc":
                        allData = allData.OrderByDescending(n => n.UserName).ToList();
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                allData = allData.Where(n => n.UserName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            //Paging
            int pageSize = 5;
            allData = PaginatedList<User>.Create(allData.AsQueryable(), pageNumber ?? 1, pageSize);
            return allData;
            
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.SingleOrDefault(n => n.UserName == username);
            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User Register(string username, string password)
        {
            var account = new User()
            {
                UserName = username,
                Password = password,
                Role = "DEV"
            };
            _db.Users.Add(account);
            _db.SaveChanges();
            account.Password = "";
            return account;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePass(User user)
        {
            _db.Users.Update(user);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _db.Users.Update(user);
            return Save();
        }

        public bool UserExist(string username)
        {
            var result = _db.Users.Any(n => n.UserName.ToLower().Trim() == username.ToLower().Trim());
            return result;
        }

        public bool UserExist(int id)
        {
            var result = _db.Users.Any(n => n.Id == id);
            return result;
        }
    }
}
