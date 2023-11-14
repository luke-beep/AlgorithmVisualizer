using Spectre.Console;

namespace AlgorithmVisualizer.Utilities;

public class CanvasUtilities
{
    public static void ClearCanvas(Canvas canvas)
    {
        for (var x = 0; x < canvas.Width; x++)
        {
            for (var y = 0; y < canvas.Height; y++)
            {
                canvas.SetPixel(x, y, Color.Black);
            }
        }
    }

    public static void DrawBar(Canvas canvas, int position, int value, int maxHeight)
    {
        for (var i = 0; i < value && i < maxHeight; i++)
        {
            canvas.SetPixel(position, maxHeight - 1 - i, Color.White);
        }
    }

    public static void UpdateCanvas(Canvas canvas, int[] data, int maxHeight)
    {
        ClearCanvas(canvas);

        for (var i = 0; i < data.Length; i++)
        {
            DrawBar(canvas, i, data[i], maxHeight);
        }
    }
}