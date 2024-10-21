using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController
{
    private readonly ICommentRepository commentRepository;

    public CommentsController(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    [HttpPost]
    public async Task<IResult> CreateComment([FromBody] CommentDto request)
    {
        try
        {
            Comment comment = new Comment()
            {
                Content = "First Comment",
                PostId = 1,
                UserId = 1
            };
            Console.WriteLine("Comment created!");
            await commentRepository.AddAsync(comment);
            return Results.Created($"comments/{comment.Id}", comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateComment([FromRoute] int id, [FromBody] CommentDto request)
    {
        try
        {
            Comment comment = await commentRepository.GetSingleAsync(id);
            if (comment == null)
            {
                return Results.NotFound();
            }

            comment.Content = request.Content;
            await commentRepository.UpdateAsync(comment);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetSingleComment([FromRoute] int id)
    {
        try
        {
            Comment comment = await commentRepository.GetSingleAsync(id);
            return Results.Ok(comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet]
    public async Task<IResult> GetManyComments()
    {
        try
        {
            IQueryable<Comment> comments = commentRepository.GetMany();
            return Results.Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteComment([FromRoute] int id)
    {
        try
        {
            await commentRepository.DeleteAsync(id);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}