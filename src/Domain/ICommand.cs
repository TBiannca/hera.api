namespace Domain;

public interface ICommand<in TInput, out TOutput>
{
    public TOutput Execute(TInput input);
}