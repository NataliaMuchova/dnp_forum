using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOs;
using Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // create
        [HttpPost] 
        public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
        {
            await VerifyUserNameAvailableAsync(request.UserName);
            User user = new(request.UserName, request.Password);
            User created = await userRepository.AddAsync(user);
            UserDto dto = new()
            {
                Id = created.Id,
                UserName = created.Username
            };
            return Created($"/users/{dto.Id}", dto);

        }

        private async Task VerifyUserNameAvailableAsync(string userName)
        {
            var users = await userRepository.GetManyAsync(); // your interface returns Task<IQueryable<User>>
            if (users.Any(u => u.Username == userName))
                throw new InvalidOperationException("Username already taken.");
        }

        // update
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] CreateUserDto dto)
        {
            var user = new User(dto.UserName, dto.Password) { Id = id };
            await userRepository.UpdateAsync(user);
            return NoContent();
        }

        // getSingle
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetSingle(int id)
        {
            var user = await userRepository.GetSingleAsync(id);
            var dto = new UserDto { Id = user.Id, UserName = user.Username };
            return Ok(dto);
        }

        // getMany
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetMany([FromQuery] string? contains)
        {
            var users = await userRepository.GetManyAsync();
            if (!string.IsNullOrEmpty(contains))
                users = users.Where(u => u.Username.Contains(contains, StringComparison.OrdinalIgnoreCase));

            var list = users.Select(u => new UserDto { Id = u.Id, UserName = u.Username });
            return Ok(list);
        }

        // delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await userRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
