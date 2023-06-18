using MediatR;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Features.Tests.Commands.Create;

public sealed record CreateTestCommand : IRequest<int>, IMapWith<Test>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int UserId { get; set; }
    public ICollection<Question> Questions { get; set; } = null!;
}