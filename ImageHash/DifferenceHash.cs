using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ImageHash
{
    /// <summary>
    /// Calculates a hash of an image using the difference hash algorithm.
    /// Transforms the image into a 9x8 grayscale image and computes the difference of each pixel with its right neighbor.
    /// </summary>
    /// <remarks>
    /// Algorithm specified by David Oftedal and slightly adjusted by Dr. Neal Krawetz.
    /// See <see href="http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html"/> for more information.
    /// </remarks>
    public class DifferenceHash : IImageHash
    {
        private readonly int WIDTH = 9;
        private readonly int HEIGHT = 8;

        private readonly double RED_WEIGHT = 0.299;
        private readonly double GREEN_WEIGHT = 0.587;
        private readonly double BLUE_WEIGHT = 0.114;

        public ulong Hash(SKImage image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            using (var bitmap = SKBitmap.FromImage(image))
            {
                // Resize the image
                SKBitmap resized = bitmap.Resize(new SKImageInfo(WIDTH, HEIGHT), SKFilterQuality.High);

                // Convert the image to grayscale
                var pixels = new byte[WIDTH * HEIGHT];
                var index = 0;
                for (var y = 0; y < HEIGHT; y++)
                {
                    for (var x = 0; x < WIDTH; x++)
                    {
                        var color = resized.GetPixel(x, y);
                        var gray = (byte)(color.Red * RED_WEIGHT + color.Green * GREEN_WEIGHT + color.Blue * BLUE_WEIGHT);
                        pixels[index++] = gray;
                    }
                }

                // Compute the difference between pixels and set the hash.
                // If pixel[i] > pixel[i+1] set the i-th bit to 1, otherwise to 0.
                var hash = 0UL;
                for (var i = 0; i < pixels.Length - 1; i++)
                {
                    hash |= (ulong)(pixels[i] > pixels[i + 1] ? 1 : 0) << i;
                }

                return hash;

            }
        }
    }
}