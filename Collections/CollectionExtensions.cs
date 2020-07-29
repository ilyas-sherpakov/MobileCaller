using System;
using System.ComponentModel;
using System.Linq;

namespace MobileCaller.Collections
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Provide the deep clone of the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToClone"></param>
        /// <returns></returns>
        public static VariantList<T> Clone<T>(this VariantList<T> listToClone) where T : IChangeTracking, ICloneable
        {
            var v = new VariantList<T>();
            v.AddRange(listToClone.Select(item => (T)item.Clone()));
            return v;
        }
    }
}
