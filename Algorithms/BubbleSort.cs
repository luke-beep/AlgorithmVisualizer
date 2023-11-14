using AlgorithmVisualizer.Algorithms.Contract;
using AlgorithmVisualizer.Utilities;
using Spectre.Console;

namespace AlgorithmVisualizer.Algorithms;

public class BubbleSort : IAlgorithm
{
    private readonly int[] _data;
    private readonly int _delay;
    private readonly Canvas _canvas;

    public BubbleSort(int[] data, Canvas canvas, int delay)
    {
        _data = data;
        _canvas = canvas;
        _delay = delay;

        CanvasUtilities.UpdateCanvas(_canvas, _data, _canvas.Height);
    }

    public string Name => "Bubble Sort";

    public async Task ExecuteAsync()
    {
        await AnsiConsole.Live(_canvas)
            .StartAsync(async ctx =>
            {
                var n = _data.Length;
                for (var i = 0; i < n - 1; i++)
                {
                    for (var j = 0; j < n - i - 1; j++)
                    {
                        if (_data[j] <= _data[j + 1])
                        {
                            continue;
                        }

                        (_data[j], _data[j + 1]) = (_data[j + 1], _data[j]);

                        CanvasUtilities.UpdateCanvas(_canvas, _data, _canvas.Height);
                        ctx.Refresh();
                        await Task.Delay(_delay);
                    }
                }
            });
    }
}
