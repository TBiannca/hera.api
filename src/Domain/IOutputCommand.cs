namespace Domain;

public interface IOutputCommand<out TOutput>
{
    public TOutput Execute();
}