using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;
    
    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Enter username: ");
        string? username = Console.ReadLine();
        
        Console.WriteLine("Enter password: ");
        string? password = Console.ReadLine();

        Console.Clear();
        var newUser = new User(username, password);
        await userRepository.AddAsync(newUser);
        
        Console.WriteLine($"User with Id: {newUser.Id} has been created !");
    }
}