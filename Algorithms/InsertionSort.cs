using AlgorithmVisualizer.Algorithms.Contract;
using AlgorithmVisualizer.Utilities;
using Spectre.Console;

namespace AlgorithmVisualizer.Algorithms;

public class InsertionSort : IAlgorithm
{
    private readonly int[] _data;
    private readonly int _delay;
    private readonly Canvas _canvas;

    public InsertionSort(int[] data, Canvas canvas, int delay)
    {
        _data = data;
        _canvas = canvas;
        _delay = delay;

        CanvasUtilities.UpdateCanvas(_canvas, _data, _canvas.Height);
    }

    public string Name => "Insertion Sort";

    public async Task ExecuteAsync()
    {
        await AnsiConsole.Live(_canvas)
            .StartAsync(async ctx =>
            {
                var n = _data.Length;
                for (var i = 1; i < n; ++i)
                {
                    var key = _data[i];
                    var j = i - 1;

                    while (j >= 0 && _data[j] > key)
                    {
                        _data[j + 1] = _data[j];
                        j -= 1;
                    }

                    _data[j + 1] = key;

                    CanvasUtilities.UpdateCanvas(_canvas, _data, _canvas.Height);
                    ctx.Refresh();
                    await Task.Delay(_delay);
                }
            });
    }
}