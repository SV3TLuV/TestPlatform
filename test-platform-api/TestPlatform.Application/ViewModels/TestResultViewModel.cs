namespace TestPlatform.Application.ViewModels;

public class TestResultViewModel
{
    public int TestId { get; set; }
    public int CountRightAnswers { get; set; }
    public int CountAnswers { get; set; }
    public int Percent => (int)Math.Round(100.0 / CountAnswers * CountRightAnswers);
}