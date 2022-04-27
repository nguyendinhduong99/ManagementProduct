using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkyAPI.Models;
using PM.Data.Data;
using PM.Data.Models;
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
    }
}
