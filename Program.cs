using AlgorithmVisualizer.Algorithms;
using AlgorithmVisualizer.Algorithms.Contract;
using Spectre.Console;

namespace AlgorithmVisualizer;

internal class Program
{
    private static async Task Main(string[] args)
    {
        InitializeConsole();

        while (true)
        {
            var size = PromptForSize().Result;
            var algorithm = PromptForAlgorithm().Result;
            var delay = PromptForDelay().Result;

            var data = RandomizeData(size);
            Canvas canvas = new(size, 100);
            switch (algorithm)
            {
                case "Bubble Sort":
                    await VisualizeBubbleSort(data, canvas, delay);
                    break;
                case "Quick Sort":
                    await VisualizeQuickSort(data, canvas, delay);
                    break;
                case "Insertion Sort":
                    await VisualizeInsertionSort(data, canvas, delay);
                    break;
                case "Merge Sort":
                    await VisualizeMergeSort(data, canvas, delay);
                    break;
                case "Heap Sort":
                    await VisualizeHeapSort(data, canvas, delay);
                    break;
            }
        }
    }

    private static Task<int> PromptForSize()
    {
        var sizePrompt = new SelectionPrompt<int>()
            .Title("Choose a size for the data set:")
            .PageSize(10)
            .AddChoices(10, 20, 30, 40, 50, 60, 70, 80, 90, 100);

        return Task.FromResult(AnsiConsole.Prompt(sizePrompt));
    }

    private static Task<string> PromptForAlgorithm()
    {
        var algorithmPrompt = new SelectionPrompt<string>()
            .Title("Choose a sorting algorithm to visualize:")
            .PageSize(10)
            .AddChoices("Bubble Sort", "Quick Sort", "Insertion Sort", "Merge Sort", "Heap Sort");

        return Task.FromResult(AnsiConsole.Prompt(algorithmPrompt));
    }

    private static Task<int> PromptForDelay()
    {
                var delayPrompt = new SelectionPrompt<int>()
            .Title("Choose a delay for the visualization:")
            .PageSize(10)
            .AddChoices(10, 20, 30, 40, 50, 60, 70, 80, 90, 100);

        return Task.FromResult(AnsiConsole.Prompt(delayPrompt));
    }
    

    private static void InitializeConsole()
    {
        AnsiConsole.Console.Clear(true);
        AnsiConsole.Write(
            new FigletText("Algorithm Visualizer")
                .Centered()
                .Color(Color.Blue));
    }

    private static async Task VisualizeBubbleSort(int[] data, Canvas canvas, int delay)
    {
        var bubbleSort = new BubbleSort(data, canvas, delay);
        await LoadVisualizer(bubbleSort);
        await bubbleSort.ExecuteAsync();
    }

    private static async Task VisualizeQuickSort(int[] data, Canvas canvas, int delay)
    {
        var quickSort = new QuickSort(data, canvas, delay);
        await LoadVisualizer(quickSort);
        await quickSort.ExecuteAsync();
    }

    private static async Task VisualizeInsertionSort(int[] data, Canvas canvas, int delay)
    {
        var insertionSort = new InsertionSort(data, canvas, delay);
        await LoadVisualizer(insertionSort);
        await insertionSort.ExecuteAsync();
    }

    private static async Task VisualizeMergeSort(int[] data, Canvas canvas, int delay)
    {
        var mergeSort = new MergeSort(data, canvas, delay);
        await LoadVisualizer(mergeSort);
        await mergeSort.ExecuteAsync();
    }

    private static async Task VisualizeHeapSort(int[] data, Canvas canvas, int delay)
    {
        var heapSort = new HeapSort(data, canvas, delay);
        await LoadVisualizer(heapSort);
        await heapSort.ExecuteAsync();
    }

    private static async Task LoadVisualizer(IAlgorithm algorithm) =>
        await AnsiConsole.Progress()
            .StartAsync(ctx =>
            {
                var task1 = ctx.AddTask($"[green]Initializing {algorithm.Name}[/]");
                var task2 = ctx.AddTask("[green]Initializing Canvas[/]");

                while (!ctx.IsFinished)
                {
                    Thread.Sleep(100);
                    task2.Increment(1.5);
                    task1.Increment(1.75);
                }

                Task.Delay(1000);
                return Task.CompletedTask;
            });

    private static int[] RandomizeData(int size)
    {
        var data = new int[size];
        Random random = new();
        for (var i = 0; i < data.Length; i++)
        {
            data[i] = random.Next(0, 50);
        }

        return data;
    }
}