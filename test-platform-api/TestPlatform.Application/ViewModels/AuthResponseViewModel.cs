namespace TestPlatform.Application.ViewModels;

public class AuthResponseViewModel
{
    public required TokenPairViewModel Tokens { get; set; }
    public required UserViewModel User { get; set; }
}