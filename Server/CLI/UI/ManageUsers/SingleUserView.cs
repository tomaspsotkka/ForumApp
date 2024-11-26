/*using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class SingleUserView
{
    private readonly IUserRepository userRepository;

    public SingleUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Enter user id: ");
        var userId = Console.ReadLine();
        int id;
        Int32.TryParse(userId, out id);
        
        Console.Clear();
        var user = await userRepository.GetSingleAsync(id);
        Console.WriteLine($"{user.Id}. {user.Username} \n");
    }
}*/