using AlgorithmVisualizer.Algorithms.Contract;
using AlgorithmVisualizer.Utilities;
using Spectre.Console;

namespace AlgorithmVisualizer.Algorithms;

public class HeapSort : IAlgorithm
{
    private readonly int[] _data;
    private readonly int _delay;
    private readonly Canvas _canvas;

    public HeapSort(int[] data, Canvas canvas, int delay)
    {
        _data = data;
        _canvas = canvas;
        _delay = delay;

        CanvasUtilities.UpdateCanvas(_canvas, _data, _canvas.Height);
    }

    public string Name => "Heap Sort";

    public async Task ExecuteAsync()
    {
        await AnsiConsole.Live(_canvas)
            .StartAsync(async ctx =>
            {
                var n = _data.Length;

                for (var i = n / 2 - 1; i >= 0; i--)
                {
                    Heap(n, i);
                }

                for (var i = n - 1; i > 0; i--)
                {
                    Swap(0, i);

                    Heap(i, 0);

                    CanvasUtilities.UpdateCanvas(_canvas, _data, _canvas.Height);
                    ctx.Refresh();
                    await Task.Delay(_delay);
                }
            });
    }

    private void Heap(int n, int i)
    {
        while (true)
        {
            var largest = i;
            var left = 2 * i + 1;
            var right = 2 * i + 2;

            if (left < n && _data[left] > _data[largest])
            {
                largest = left;
            }

            if (right < n && _data[right] > _data[largest])
            {
                largest = right;
            }

            if (largest == i)
            {
                return;
            }

            Swap(i, largest);

            i = largest;
        }
    }

    private void Swap(int i, int j)
    {
        (_data[i], _data[j]) = (_data[j], _data[i]);
    }
}