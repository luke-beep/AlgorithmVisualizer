using AlgorithmVisualizer.Algorithms.Contract;
using AlgorithmVisualizer.Utilities;
using Spectre.Console;

namespace AlgorithmVisualizer.Algorithms;

public class QuickSort : IAlgorithm
{
    private readonly int[] _data;
    private readonly int _delay;
    private readonly Canvas _canvas;

    public QuickSort(int[] data, Canvas canvas, int delay)
    {
        _data = data;
        _canvas = canvas;
        _delay = delay;

        CanvasUtilities.UpdateCanvas(_canvas, _data, _canvas.Height);
    }

    public string Name => "Quick Sort";

    public async Task ExecuteAsync()
    {
        await AnsiConsole.Live(_canvas)
            .StartAsync(async ctx =>
            {
                await QuickSortRecursive(ctx, 0, _data.Length - 1);
            });
    }

    private async Task QuickSortRecursive(LiveDisplayContext ctx, int low, int high)
    {
        while (true)
        {
            if (low < high)
            {
                var pivotIndex = Partition(low, high);
                CanvasUtilities.UpdateCanvas(_canvas, _data, _canvas.Height);
                ctx.Refresh();
                await Task.Delay(_delay);
                await QuickSortRecursive(ctx, low, pivotIndex - 1);
                low = pivotIndex + 1;
                continue;

            }

            break;
        }
    }

    private int Partition(int low, int high)
    {
        var pivot = _data[high];
        var i = (low - 1);

        for (var j = low; j < high; j++)
        {
            if (_data[j] >= pivot)
            {
                continue;
            }

            i++;
            Swap(i, j);
        }
        Swap(i + 1, high);
        return i + 1;
    }

    private void Swap(int i, int j)
    {
        (_data[i], _data[j]) = (_data[j], _data[i]);
    }

}