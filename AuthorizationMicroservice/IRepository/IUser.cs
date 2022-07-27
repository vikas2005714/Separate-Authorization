using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationMicroservice.Models;

namespace AuthorizationMicroservice.IRepository
{
    public interface IUser
    {
        bool IsuniqueuserName(string username);
        ApiResult Authenticate(string username, string password);
        User Register(string username, string password);
    }
}
