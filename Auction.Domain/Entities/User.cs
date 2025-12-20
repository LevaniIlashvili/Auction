using Auction.Domain.Exceptions;

namespace Auction.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    private User() { }

    private User(Guid id, string username, string passwordHash)
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
    }

    public static User Create(Guid id, string username, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new DomainException("Username cannot be empty.");

        if (username.Length < 3)
            throw new DomainException("Username must be at least 3 characters long.");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("Password hash is required.");

        return new User(id, username, passwordHash);
    }

    public void UpdateUsername(string newUsername)
    {
        if (string.IsNullOrWhiteSpace(newUsername) || newUsername.Length < 3)
            throw new DomainException("Invalid username.");

        Username = newUsername;
    }
}