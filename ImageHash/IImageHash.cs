using SkiaSharp;
using System;

namespace ImageHash
{
    /// <summary>
    /// Interface for perceptual image hashing algorithms.
    /// </summary>
    internal interface IImageHash
    {
        /// <summary>Hash the image using the algorithm.</summary>
        /// <param name="image">image to calculate hash from.</param>
        /// <returns>hash value of the image.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="image"/> is <c>null</c>.</exception>
        ulong Hash(SKImage image);
    }
}
