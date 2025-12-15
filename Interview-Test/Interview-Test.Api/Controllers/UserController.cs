using Interview_Test.Infrastructure;
using Interview_Test.Models;
using Interview_Test.Repositories;
using Interview_Test.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interview_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await _repository.GetUsers());
    }


    [HttpGet("GetUserById/{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        return Ok(await _repository.GetUserById(id));
    }
    
    [HttpPost("CreateUser")]
    public async Task<IActionResult> GetUserById([FromBody] UserModel user)
    {
        // [FromBody] UserModel user
        //UserModel user = Interview_Test.Repositories.Data.Users[1];
        int result = await _repository.CreateUser(user);
        if (result == 100) return Ok("Duplicate user");
        return Ok("Add new user success");
    }
}