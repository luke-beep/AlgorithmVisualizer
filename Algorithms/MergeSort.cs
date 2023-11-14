using AlgorithmVisualizer.Algorithms.Contract;
using AlgorithmVisualizer.Utilities;
using Spectre.Console;

namespace AlgorithmVisualizer.Algorithms;

public class MergeSort : IAlgorithm
{
    private readonly int[] _data;
    private readonly int _delay;
    private readonly Canvas _canvas;

    public MergeSort(int[] data, Canvas canvas, int delay)
    {
        _data = data;
        _canvas = canvas;
        _delay = delay;

        CanvasUtilities.UpdateCanvas(_canvas, _data, _canvas.Height);
    }

    public string Name => "Merge Sort";

    public async Task ExecuteAsync()
    {
        await AnsiConsole.Live(_canvas)
            .StartAsync(async ctx =>
            {
                await MergeSortRecursive(0, _data.Length - 1, ctx);
            });
    }

    private async Task MergeSortRecursive(int left, int right, LiveDisplayContext ctx)
    {
        if (left < right)
        {
            var middle = left + (right - left) / 2;

            await MergeSortRecursive(left, middle, ctx);
            await MergeSortRecursive(middle + 1, right, ctx);

            Merge(left, middle, right);
            CanvasUtilities.UpdateCanvas(_canvas, _data, _canvas.Height);
            ctx.Refresh();
            await Task.Delay(_delay);
        }
    }

    private void Merge(int left, int middle, int right)
    {
        var n1 = middle - left + 1;
        var n2 = right - middle;

        var l = new int[n1];
        var r = new int[n2];
        int i, j;

        for (i = 0; i < n1; ++i)
        {
            l[i] = _data[left + i];
        }

        for (j = 0; j < n2; ++j)
        {
            r[j] = _data[middle + 1 + j];
        }

        i = 0;
        j = 0;
        var k = left;
        while (i < n1 && j < n2)
        {
            if (l[i] <= r[j])
            {
                _data[k] = l[i];
                i++;
            }
            else
            {
                _data[k] = r[j];
                j++;
            }
            k++;
        }

        while (i < n1)
        {
            _data[k] = l[i];
            i++;
            k++;
        }

        while (j < n2)
        {
            _data[k] = r[j];
            j++;
            k++;
        }
    }
}