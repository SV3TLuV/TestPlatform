using AutoBogus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.Features.Tests.Commands.Create;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;
using TestPlatform.Persistence.Context;
using TestPlatform.Tests.Common;

namespace TestPlatform.Tests.Tests.Feature.Tests.Commands;

public class CreateTestCommandHandlerTest
{
    [Theory]
    [MemberData(nameof(Data))]
    public async Task Test_Handle_AddTest(
        CreateTestCommand command)
    {
        // Arrange
        var context = TestContainer.Resolve<TestDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<CreateTestCommand, int>>();
        await context.Database.EnsureCreatedAsync();

        // Act
        await handler.Handle(command, default);

        var testDbo = await context
            .Set<Test>()
            .ToArrayAsync();

        await context.Database.EnsureDeletedAsync();

        // Assert
        Assert.Contains(testDbo, t => t.Name == command.Name && t.Description == command.Description);
    }

    public static IEnumerable<object[]> Data =>
        Enumerable.Range(1, 10)
            .Select(count => new object[]
            {
                new AutoFaker<CreateTestCommand>()
                    .Ignore(e => e.Questions)
                    .RuleFor(e => e.Name, f => f.Random
                        .String2(4, 10, "qwertyuiopasdfghjklzxcvbnm"))
                    .RuleFor(e => e.Description, f => f.Random
                        .String2(4, 100, "qwertyuiopasdfghjklzxcvbnm"))
                    .RuleFor(e => e.UserId, f => f.Random
                        .Int(1, 10))
                    .Generate()
            });
}