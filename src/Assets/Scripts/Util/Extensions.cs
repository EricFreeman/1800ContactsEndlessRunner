using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Util
{
    public static class Extensions
    {
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var e in enumerable)
            {
                action(e);
            }
        }

        public static string ToFormat(this string s, params object[] parmaters)
        {
            return string.Format(s, parmaters);
        }

        public static T Random<T>(this List<T> e)
        {
            var random = new Random();
            var index = random.Next(0, e.Count());

            return e[index];
        }
    }
}