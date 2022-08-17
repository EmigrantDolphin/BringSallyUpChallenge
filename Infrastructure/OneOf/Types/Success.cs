namespace Infrastructure.OneOf.Types;

public readonly struct Success { }

//TODO: maybe make a nuget package for this
public readonly struct Success<T>
{
    public Success(T value)
    {
        Value = value;
    }
    
    public T Value { get; }
}
