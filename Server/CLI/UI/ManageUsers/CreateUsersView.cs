using System;
using Contracts;
using Entities;
namespace CLI.UI.ManageUsers;

public class CreateUsersView
{
    private readonly IUserRepository userRepository;

    public CreateUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    private async Task AddUserAsync(string username, string password)
    {
        User created = await userRepository.AddAsync(new User(username, password));
        Console.WriteLine($"User created with ID: {created.Id}");
    }
    public async Task StartAsync()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        await AddUserAsync(username, password);
    }
}
