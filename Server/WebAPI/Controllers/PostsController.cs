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
    private readonly IUserRepository userRepository;

    public PostsController(IPostRepository postRepository, IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }
    
    
    [HttpGet("test-exception")]
    public ActionResult TestException()
    {
        throw new Exception("Test exception");
    }

    [HttpPost]
    public async Task<ActionResult<CreatePostDto>> CreatePost([FromBody] CreatePostDto request)
    {
        try
        {
            Post post = new(request.Title, request.Body, request.UserId);
            Console.WriteLine("Post created!");
            Post created = await postRepository.AddAsync(post);
            CreatePostDto dto = new()
            {
                Title = created.Title,
                Body = created.Body,
                UserId = created.UserId
            };
            return Created($"/Posts/", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PostDto>>? UpdatePost([FromRoute] int id, [FromBody] PostDto request)
    {
        try
        {
            Post post = await postRepository.GetSingleAsync(id);
            if (post == null)
            {
                return NotFound($"Post with ID {post.Id} not found");
            }

            post.Title = request.Title;
            post.Body = request.Body;

            await postRepository.UpdateAsync(post);
            return Ok($"Post with ID {post.Id} successfully updated.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("by-id/{id}")]
    public async Task<ActionResult<PostDto>> GetSinglePost([FromRoute] int id)
    {
        try
        {
            Post post = await postRepository.GetSingleAsync(id);
            if (post is null)
            {
                return NotFound($"Post with ID '{id}' not found");

            }
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("by-title/{title}")]
    public async Task<ActionResult<PostDto>> GetSinglePost([FromRoute] string title)
    {
        try
        {
            Post post = await postRepository.GetSingleAsync(title);
            if (post is null)
            {
                throw new InvalidOperationException(
                    $"Post with Title '{title}' not found"); 
            }
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("all")]
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
            bool deleted = await postRepository.DeleteAsync(id);
            if (deleted is false)
            {
                return NotFound($"Post with ID '{id}' not found");
            }
            return Ok($"Post with ID {id} successfully deleted."); //succesfully deleted (hopefully)
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}