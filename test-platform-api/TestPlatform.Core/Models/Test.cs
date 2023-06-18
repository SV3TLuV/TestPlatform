using System.Text.Json.Serialization;

namespace TestPlatform.Core.Models;

public class Test
{
    public int TestId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [JsonIgnore] public virtual User? User { get; set; } = null!;
}