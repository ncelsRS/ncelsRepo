using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.Prism.Controllers
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Kendo.Mvc.FilterDescriptor> SelectMemberDescriptors(this IEnumerable<Kendo.Mvc.IFilterDescriptor> descriptors)
        {
            return descriptors.SelectRecursive(f => GetChildDescriptors(f)).OfType<Kendo.Mvc.FilterDescriptor>();
        }

        private static IEnumerable<Kendo.Mvc.IFilterDescriptor> GetChildDescriptors(Kendo.Mvc.IFilterDescriptor f)

        {
            if (f is Kendo.Mvc.CompositeFilterDescriptor)
            {
                return ((Kendo.Mvc.CompositeFilterDescriptor)f).FilterDescriptors;
            }
            return null;
        }

        public static IEnumerable<TSource> SelectRecursive<TSource>(this IEnumerable<TSource> source,Func<TSource, IEnumerable<TSource>> recursiveSelector)
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