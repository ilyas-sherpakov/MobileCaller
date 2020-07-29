using System.Collections;
using System.Windows.Forms;
using System;

namespace MobileCaller.Utils
{
    public class ListViewColumnSorter : IComparer
    {
        public ListViewColumnSorter(int colIndex)
        {
            SortOrder = SortOrder.None;
            SortColumn = colIndex;
        }

        public int SortColumn { get; set; }
        public SortOrder SortOrder { get; set; }

        private static int CompareSrings(ListViewItem.ListViewSubItem a, ListViewItem.ListViewSubItem b)
        {
            return String.Compare(a.Text, b.Text);
        }

        private static int CompareDates(ListViewItem.ListViewSubItem a, ListViewItem.ListViewSubItem b)
        {
            DateTime dt1, dt2;
            if (!DateTime.TryParse(a.Text, out dt1))
                dt1 = DateTime.MinValue;
            if (!DateTime.TryParse(b.Text, out dt2))
                dt2 = DateTime.MinValue;
            return DateTime.Compare(dt1, dt2);
        }

        public int Compare(object a, object b)
        {
            // Cast the objects to be compared to ListViewItem objects  
            var itemA = (ListViewItem)a;
            var itemB = (ListViewItem)b;
            var compareResult = 0;

            if (SortColumn == 0)
            {
                // telephone
                compareResult = CompareSrings(itemA.SubItems[SortColumn], itemB.SubItems[SortColumn]);
            }
            else
            {
                compareResult = CompareDates(itemA.SubItems[SortColumn], itemB.SubItems[SortColumn]);
            }

            if (SortOrder == SortOrder.Descending)
            {
                // Invert the value returned by Compare.
                compareResult *= -1;
            }

            return compareResult;
        }
    }
}
