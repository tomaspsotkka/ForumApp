using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepository;

    public CommentsController(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    [HttpPost]
    public async Task<ActionResult<CreateCommentDto>> CreateComment([FromBody] CreateCommentDto request)
    {
        try
        {
            Comment comment = new (request.UserId, request.Content, request.PostId);
            Comment created = await commentRepository.AddAsync(comment);
            CreateCommentDto dto = new()
            {
                Content = created.Content,
                PostId = created.PostId,
                UserId = created.UserId
            };
            return Created($"/Comments/", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateCommentDto>> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto request)
    {
        try
        {
            Comment comment = await commentRepository.GetSingleAsync(id);
            if (comment == null)
            {
                return NotFound($"Comment with ID {comment.Id} not found");
            }
            comment.Content = request.Content;
            await commentRepository.UpdateAsync(comment);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetSingleComment([FromRoute] int id)
    {
        try
        {
            Comment? comment = await commentRepository.GetSingleAsync(id);
            if (comment == null)
            {
                return NotFound($"Comment with ID {comment.Id} not found");
            }
            CommentDto dto = new CommentDto()
            {
                Content = comment.Content,
                PostId = comment.PostId,
                UserId = comment.UserId
            };
            return Ok(comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<CommentDto>> GetManyComments()
    {
        try
        {
            IQueryable<Comment> comments = commentRepository.GetMany();
            if (comments == null)
            {
                return NotFound("No comments found");
            }
            return Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CommentDto>> DeleteComment([FromRoute] int id, int postId)
    {
        try
        {
            bool deleted = await commentRepository.DeleteAsync(id, postId);
            if (deleted is false)
            {
                return NotFound($"Comment with ID {id} not found");
            }
            return Ok($"Comment with ID {id} deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}