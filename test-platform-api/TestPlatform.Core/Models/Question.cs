using System.Text.Json.Serialization;

namespace TestPlatform.Core.Models;

public class Question
{
    public int QuestionId { get; set; }

    public string Text { get; set; } = null!;

    public int TestId { get; set; }

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    [JsonIgnore] public virtual Test? Test { get; set; } = null!;
}