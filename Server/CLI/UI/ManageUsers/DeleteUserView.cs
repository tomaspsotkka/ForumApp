using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class DeleteUserView
{
    private readonly IUserRepository userRepository;

    public DeleteUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("Choose id: ");
        var id = Console.ReadLine();
        int userId;
        Int32.TryParse(id, out userId);
        
        Console.Clear();
        await userRepository.DeleteAsync(userId);
        Console.WriteLine($"Post with id {userId} was deleted.");
    }
}