using AuthorizationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice
{
    public class ApiResult
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public User User { get; set; }
    }
}
