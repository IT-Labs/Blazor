using System;
using System.Collections.Generic;

namespace Core.Framework.Extensions
{
    public static class ListExtensions
    {
        public static bool Equal(this List<Guid> list1, List<Guid> list2)
        {
            if (list1.Count != list2.Count)
                return false;

            foreach(var item in list1)
            {
                if (!list2.Contains(item))
                    return false;
            }
            return true;
        }
    }
}
