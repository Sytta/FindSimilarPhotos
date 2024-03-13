using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageHash
{
    public static class CompareHash
    {
        public static double Similarity(ulong hash1, ulong hash2)
        {
            return (64 - HammingDistance(hash1, hash2)) * 100 / 64.0;
        }

        public static ulong HammingDistance(ulong x, ulong y)
        {
            var z = x ^ y;
            ulong count = 0;
            while (z > 0)
            {
                count += z & 1;
                z >>= 1;
            }

            return count;
        }
    }
}
