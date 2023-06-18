namespace TestPlatform.Core.Common.Exceptions;

public class NotFoundException : TestException
{
    public NotFoundException(string name)
        : base($"Entity \"{name}\" was not found.")
    {
    }
    
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" with key \"{key}\" was not found.")
    {
    }
}