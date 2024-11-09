using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepository;

    public PostsController(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    [HttpGet("test-exception")]
    public ActionResult TestException()
    {
        throw new Exception("Test exception");
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> CreatePost([FromBody] PostDto request)
    {
        try
        {
            Post post = new(request.Title, request.Body, request.UserId);
            Console.WriteLine("Post created!");
            await postRepository.AddAsync(post);
            return Created($"posts/{post.Id}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PostDto>> UpdatePost([FromRoute] int id, [FromBody] PostDto request)
    {
        try
        {
            Post post = await postRepository.GetSingleAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.Title = request.Title;
            post.Body = request.Body;

            await postRepository.UpdateAsync(post);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetSinglePost([FromRoute] int id)
    {
        try
        {
            Post post = await postRepository.GetSingleAsync(id);
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<PostDto>> GetManyPosts()
    {
        try
        {
            IQueryable<Post> posts =  postRepository.GetMany();
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PostDto>> DeletePost([FromRoute] int id)
    {
        try
        {
            await postRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}