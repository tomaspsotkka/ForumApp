/*using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        var users = userRepository.GetMany().ToList();

        if (!users.Any())
        {
            Console.WriteLine("No users found");
            return;
        }
        
        Console.Clear();
        Console.WriteLine("List of users:");
        foreach (var user in users)
        {
            Console.WriteLine($"{user.Id}. Username: {user.Username}");
        }
        Console.WriteLine("");
    }
}*/