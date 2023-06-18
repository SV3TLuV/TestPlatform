using System.Text.Json.Serialization;

namespace TestPlatform.Core.Models;

public class Answer
{
    public int AnswerId { get; set; }

    public string Text { get; set; } = null!;

    public bool IsRight { get; set; }

    public int QuestionId { get; set; }

    public int TestId { get; set; }

    [JsonIgnore] public virtual Question? Question { get; set; }
}