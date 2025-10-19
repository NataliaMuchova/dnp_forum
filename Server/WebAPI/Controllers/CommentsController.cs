using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities;
using DTOs;
using Contracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;

        public CommentsController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        // create
        [HttpPost]
        public async Task<ActionResult<CommentDto>> Create([FromBody] CreateCommentDto dto)
        {
            var comment = new Comment(dto.Body, dto.UserId, dto.PostId);
            var created = await commentRepository.AddAsync(comment);

            var result = new CommentDto
            {
                Id = created.Id,
                Body = created.Body,
                UserId = created.UserId,
                PostId = created.PostId
            };

            return CreatedAtAction(nameof(GetSingle), new { id = result.Id }, result);
        }

        // update
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] CreateCommentDto dto)
        {
            var updated = new Comment(dto.Body, dto.UserId, dto.PostId) { Id = id };
            await commentRepository.UpdateAsync(updated);
            return NoContent();
        }

        // getSingle
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CommentDto>> GetSingle(int id)
        {
            var comment = await commentRepository.GetSingleAsync(id);
            var dto = new CommentDto
            {
                Id = comment.Id,
                Body = comment.Body,
                UserId = comment.UserId,
                PostId = comment.PostId
            };
            return Ok(dto);
        }

        // getMany
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetMany([FromQuery] int? postId, [FromQuery] int? userId)
        {
            var comments = await commentRepository.GetManyAsync();

            if (postId.HasValue)
                comments = comments.Where(c => c.PostId == postId);

            if (userId.HasValue)
                comments = comments.Where(c => c.UserId == userId);

            var list = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Body = c.Body,
                UserId = c.UserId,
                PostId = c.PostId
            });

            return Ok(list);
        }

        // delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await commentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
