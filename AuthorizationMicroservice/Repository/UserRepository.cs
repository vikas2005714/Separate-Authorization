using AuthorizationMicroservice;
using AuthorizationMicroservice.Data;
using AuthorizationMicroservice.IRepository;
using AuthorizationMicroservice.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Repository
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContex db;
        private readonly AppSettings _appSettings;

        public UserRepository(ApplicationDbContex _db,IOptions<AppSettings> appSetting )
        {
            db = _db;
            _appSettings = appSetting.Value;


        }
        public ApiResult Authenticate(string username, string password)
        {
            var apiresult = new ApiResult();
            var user = db.users.SingleOrDefault(x => x.UserName == username && x.Password == password);
            if(user == null) {
                apiresult.Message = "User Is Not Found";
                apiresult.Status = "Error";
                apiresult.User = null;
                return apiresult;
            }

            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokendescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokendescriptor);
            user.Token = tokenhandler.WriteToken(token);
            apiresult.Message = "You Login Sucessfully";
            apiresult.Status = "Success";
            apiresult.User = user;
            return apiresult;

        }

        public bool IsuniqueuserName(string username)
        {
            var user = db.users.Select(x => x.UserName == username);
            if(user == null) {
                return true;
            }
            return false;
        }

        public User Register(string username, string password)
        {
            User userobj = new User { 
                UserName = username,
                Password = password
            };

            db.users.Add(userobj);
            db.SaveChanges();
            userobj.Password = "";
            return userobj;


        }
    }
}
