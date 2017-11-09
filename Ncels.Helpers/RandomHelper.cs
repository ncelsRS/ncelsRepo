using System;
using System.Collections.Generic;
using System.Linq;

namespace Ncels.Helpers
{
    public static class RandomHelper
    {
        private static Random _random;
        public static Random RandomObject
        {
            get
            {
                if (_random == null)
                {
                    _random = new Random();
                }
                return _random;
            }
        }

        public static int Random(this int data)
        {
            var randomPos = RandomObject.Next(0, data);
            return randomPos;
        }

        public static double RandomDouble(this double data)
        {
            var randomPos = data * RandomObject.NextDouble();
            return randomPos;
        }

        public static T Random<T>(this IList<T> data)
        {
            if (data.Any())
            {
                var randomPos = RandomObject.Next(0, data.Count());
                return data[randomPos];
            }
            return data.FirstOrDefault();
        }
    }
}
