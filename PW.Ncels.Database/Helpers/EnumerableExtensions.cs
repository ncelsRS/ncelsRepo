using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc;

namespace PW.Ncels.Database.Helpers
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<FilterDescriptor> SelectMemberDescriptors(this IEnumerable<IFilterDescriptor> descriptors)
        {
            return descriptors.SelectRecursive(f => GetChildDescriptors(f)).OfType<FilterDescriptor>();
        }

        private static IEnumerable<IFilterDescriptor> GetChildDescriptors(IFilterDescriptor f)
        {
            if (f is CompositeFilterDescriptor)
            {
                return ((CompositeFilterDescriptor)f).FilterDescriptors;
            }
            return null;
        }

        public static IEnumerable<TSource> SelectRecursive<TSource>(this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TSource>> recursiveSelector)
        {
            Stack<IEnumerator<TSource>> stack = new Stack<IEnumerator<TSource>>();
            stack.Push(source.GetEnumerator());

            try
            {
                while (stack.Count > 0)
                {
                    if (stack.Peek().MoveNext())
                    {
                        TSource current = stack.Peek().Current;

                        yield return current;

                        IEnumerable<TSource> children = recursiveSelector(current);
                        if (children != null)
                        {
                            stack.Push(children.GetEnumerator());
                        }
                    }
                    else
                    {
                        stack.Pop().Dispose();
                    }
                }
            }
            finally
            {
                while (stack.Count > 0)
                {
                    stack.Pop().Dispose();
                }
            }
        }
    }
}