using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //[Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
          
        }
     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser()
        {
            Console.WriteLine("end point executed");
            return Ok(await _userRepository.GetUsersAsync());
            
        }
        
        [HttpGet("{username}")]
        public async Task<ActionResult<AppUser>> GetUser(string username) 
        {
            return await _userRepository.GetUserByUsernameAsync(username);
            
        }
    } 
}