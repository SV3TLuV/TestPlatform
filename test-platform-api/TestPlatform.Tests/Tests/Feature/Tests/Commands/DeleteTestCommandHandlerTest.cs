using AutoBogus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.Features.Tests.Commands.Delete;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;
using TestPlatform.Persistence.Context;
using TestPlatform.Tests.Common;

namespace TestPlatform.Tests.Tests.Feature.Tests.Commands;

public class DeleteTestCommandHandlerTest
{
    [Theory]
    [MemberData(nameof(Data))]
    public async Task Test_Handle_DeleteTest(
        DeleteTestCommand command)
    {
        // Arrange
        var context = TestContainer.Resolve<TestDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<DeleteTestCommand>>();
        await context.Database.EnsureCreatedAsync();

        var test = new Test
        {
            TestId = command.Id,
            Name = "Test",
            Description = "Test description",
            UserId = command.Id,
            Questions = new List<Question>()
        };

        await context.Set<Test>().AddAsync(test);
        await context.SaveChangesAsync();

        // Act
        await handler.Handle(command, default);
        var result = await context.Set<Test>().ToArrayAsync();
        await context.Database.EnsureDeletedAsync();

        // Assert
        Assert.Empty(result);
    }
    
    public static IEnumerable<object[]> Data =>
        Enumerable.Range(1, 10)
            .Select(count => new object[]
            {
                new AutoFaker<DeleteTestCommand>()
                    .RuleFor(e => e.Id, f => f.UniqueIndex)
                    .Generate()
            });
}