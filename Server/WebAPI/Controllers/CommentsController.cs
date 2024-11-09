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
    public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CommentDto request)
    {
        try
        {
            Comment comment = new Comment()
            {
                Content = request.Content,
                PostId = request.PostId,
                UserId = request.UserId
            };
            Console.WriteLine("Comment created!");
            await commentRepository.AddAsync(comment);
            return Created($"comments/{comment.Id}", comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CommentDto>> UpdateComment([FromRoute] int id, [FromBody] CommentDto request)
    {
        try
        {
            Comment comment = await commentRepository.GetSingleAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            comment.Content = request.Content;
            await commentRepository.UpdateAsync(comment);
            return NoContent();
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
            Comment comment = await commentRepository.GetSingleAsync(id);
            return Ok(comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<CommentDto>> GetManyComments()
    {
        try
        {
            IQueryable<Comment> comments = commentRepository.GetMany();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CommentDto>> DeleteComment([FromRoute] int id)
    {
        try
        {
            await commentRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}