using MediatR;
using TestPlatform.Application.ViewModels;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Tests.Commands.CheckTest;

public sealed class CheckTestCommand : IMapWith<Test>, IRequest<TestResultViewModel>
{
    public required int TestId { get; set; }
    public required ICollection<QuestionViewModel> Questions { get; set; }
}