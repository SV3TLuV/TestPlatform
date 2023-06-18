namespace TestPlatform.Core.Models;

public class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public string? RefreshToken { get; set; } = null!;

    public virtual ICollection<Test> Tests { get; } = new List<Test>();
}