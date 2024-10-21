using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController
{
    private readonly IPostRepository postRepository;

    public PostsController(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    [HttpPost]
    public async Task<IResult> CreatePost([FromBody] PostDto request)
    {
        try
        {
            Post post = new Post()
            {
                Body = "Hahah",
                Id = 1,
                Title = "Some post"
            };
            Console.WriteLine("Post created!");
            await postRepository.AddAsync(post);
            return Results.Created($"posts/{post.Id}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdatePost([FromRoute] int id, [FromBody] PostDto request)
    {
        try
        {
            Post post = await postRepository.GetSingleAsync(id);
            if (post == null)
            {
                return Results.NotFound();
            }

            post.Title = request.Title;
            post.Body = request.Body;

            await postRepository.UpdateAsync(post);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IResult> GetSinglePost([FromRoute] int id)
    {
        try
        {
            Post post = await postRepository.GetSingleAsync(id);
            return Results.Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    public async Task<IResult> GetManyPosts()
    {
        try
        {
            IQueryable<Post> posts =  postRepository.GetMany();
            return Results.Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeletePost([FromRoute] int id)
    {
        try
        {
            await postRepository.DeleteAsync(id);
            return Results.NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}