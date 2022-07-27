using AuthorizationMicroservice.IRepository;
using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class UsersController : ControllerBase
    {
        private IUser _repository;
       

        public UsersController(IUser repository)
        {
            _repository = repository;
            

        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("UserLogin")]
        public ActionResult<ApiResult> Login([FromBody] UserDTO user)
        {


            try {
                var data = _repository.Authenticate(user.UserName, user.Password);
                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch(Exception ex) {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }


        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Register")]
        public IActionResult Register([FromBody] User user)
        {

            var data = _repository.IsuniqueuserName(user.UserName);
            if(data) {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "Username Already exist" });
            }

            var userdata = _repository.Register(user.UserName, user.Password);


            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
