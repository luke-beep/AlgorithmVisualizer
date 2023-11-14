namespace AlgorithmVisualizer.Algorithms.Contract;

public interface IAlgorithm
{
    string Name
    {
        get;
    }
    Task ExecuteAsync();
}