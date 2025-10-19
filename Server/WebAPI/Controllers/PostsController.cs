using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;
using DTOs;
using Contracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository postRepository;

        public PostsController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        // create
    [HttpPost]
    public async Task<ActionResult<PostDto>> Create([FromBody] CreatePostDto dto)
    {
        var post = new Post(dto.Title, dto.Body, dto.UserId);
        var created = await postRepository.AddAsync(post);

        var result = new PostDto
        {
            Id = created.Id,
            Title = created.Title,
            Body = created.Body,
            UserId = created.UserId
        };

        return CreatedAtAction(nameof(GetSingle), new { id = result.Id }, result);
    }

    // update
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] CreatePostDto dto)
    {
        var updated = new Post(dto.Title, dto.Body, dto.UserId) { Id = id };
        await postRepository.UpdateAsync(updated);
        return NoContent();
    }

    // getSingle
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostDto>> GetSingle(int id)
    {
        var post = await postRepository.GetSingleAsync(id);
        var dto = new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            UserId = post.UserId
        };
        return Ok(dto);
    }

    // getMany
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetMany([FromQuery] string? titleContains, [FromQuery] int? userId)
    {
        var posts = await postRepository.GetManyAsync();

        if (!string.IsNullOrEmpty(titleContains))
            posts = posts.Where(p => p.Title.Contains(titleContains, StringComparison.OrdinalIgnoreCase));

        if (userId.HasValue)
            posts = posts.Where(p => p.UserId == userId);

        var list = posts.Select(p => new PostDto
        {
            Id = p.Id,
            Title = p.Title,
            Body = p.Body,
            UserId = p.UserId
        });

        return Ok(list);
    }

        // delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await postRepository.DeleteAsync(id);
            return NoContent();
        }
    
    }
}
