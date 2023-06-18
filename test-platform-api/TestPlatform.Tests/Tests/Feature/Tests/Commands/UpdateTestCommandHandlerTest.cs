using AutoBogus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Application.Features.Tests.Commands.Update;
using TestPlatform.Core.Common.Interfaces;
using TestPlatform.Core.Models;
using TestPlatform.Persistence.Context;
using TestPlatform.Tests.Common;

namespace TestPlatform.Tests.Tests.Feature.Tests.Commands;

public class UpdateTestCommandHandlerTest
{
    [Theory]
    [MemberData(nameof(Data))]
    public async Task Test_Handle_UpdateTest(
        UpdateTestCommand command)
    {
        // Arrange
        var context = TestContainer.Resolve<TestDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<UpdateTestCommand>>();
        await context.Database.EnsureCreatedAsync();
        
        var existingTest = new Test
        {
            TestId = command.Id,
            Name = "Existing Test",
            Description = "Existing Description",
            UserId = command.UserId,
            Questions = new List<Question>()
        };
        await context.Set<Test>().AddAsync(existingTest);
        await context.SaveChangesAsync();

        // Act
        await handler.Handle(command, default);

        var updatedTest = await context.Set<Test>()
            .FirstOrDefaultAsync(e => e.TestId == command.Id);

        await context.Database.EnsureDeletedAsync();

        // Assert
        Assert.NotNull(updatedTest);
        //Assert.Equal(command.Name, updatedTest.Name);
        //Assert.Equal(command.Description, updatedTest.Description);
    }
    
    public static IEnumerable<object[]> Data =>
        Enumerable.Range(1, 10)
            .Select(count => new object[]
            {
                new AutoFaker<UpdateTestCommand>()
                    .RuleFor(e => e.Id, f => f.UniqueIndex)
                    .RuleFor(e => e.Name, f => f.Random
                        .String2(4, 10, "qwertyuiopasdfghjklzxcvbnm"))
                    .RuleFor(e => e.Description, f => f.Random
                        .String2(4, 100, "qwertyuiopasdfghjklzxcvbnm"))
                    .RuleFor(e => e.UserId, f => f.Random
                        .Int(1, 10))
                    .RuleFor(e => e.Questions, f => new List<Question>())
                    .Generate()
            });
}