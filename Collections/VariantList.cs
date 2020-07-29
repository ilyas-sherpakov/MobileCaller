using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections;

namespace MobileCaller.Collections
{
    public class VariantList<T> : List<T>, IList<T>, IChangeTracking where T : IChangeTracking
    {
        private bool _isChanged;

        #region Implementing of IList

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            IsChanged = true;
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            IsChanged = true;
        }

        #endregion

        #region Implementing of ICollection

        public new void Add(T item)
        {
            base.Add(item);
            IsChanged = true;
        }

        public new void Clear()
        {
            if (this.Any())
            {
                base.Clear();
                IsChanged = true;
            }
        }

        public new bool Remove(T item)
        {
            var retVal = base.Remove(item);
            IsChanged = true;
            return retVal;
        }

        public new void CopyTo(T[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        #endregion

        #region Implementing of IChangeTracking

        public bool IsChanged 
        {
            get
            { 
                return _isChanged || this.Any(e => e.IsChanged);
            }
            private set 
            { 
                _isChanged = value; 
            }
        }
        public void AcceptChanges()
        {
            ForEach(e => e.AcceptChanges());
            IsChanged = false;
        }

        #endregion

        #region Implementing of IEnumerable

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
