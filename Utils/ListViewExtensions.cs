using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using MobileCaller.XML;
using System.Windows.Forms;

namespace MobileCaller.Utils
{
    public static class ListViewExtensions
    {
        #region Private Method Extensions

        private static void UpdateDateActivated(this ListViewItem item, IPhoneProvider wrapper, DateTime? date)
        {
            var xmlItem = wrapper.TelephoneItems.Find(x => x.Telephone == item.Text);
            if (date.HasValue)
            {
                xmlItem.SetDateActivated(date.Value);
                item.ImageIndex = 0;
            }
            else
            {
                xmlItem.DateActivated = String.Empty;
                item.ImageIndex = -1;
            }
            item.SubItems[2].Text = xmlItem.DateActivated;
        }

        private static void UpdateComment(this ListViewItem item, IPhoneProvider wrapper, string comment)
        {
            var xmlItem = wrapper.TelephoneItems.Find(x => x.Telephone == item.Text);
            xmlItem.Comment = comment;
            item.SubItems[3].Text = xmlItem.Comment;
        }

        #endregion

        public static ListViewItem FindItem(this ListView listView, string telephone)
        {
            return listView.Items.Cast<ListViewItem>().FirstOrDefault(item => item.Text == telephone);
        }

        public static void AddTelephone(this ListView listView, XmlTelephoneItem item)
        {
            var aItem = new ListViewItem(item.Telephone, item.DateActivated != String.Empty ? 0 : -1);
            var aDateItem = new ListViewItem.ListViewSubItem(aItem, item.Date);
            aItem.SubItems.Add(aDateItem);
            var aDateActivatedItem = new ListViewItem.ListViewSubItem(aItem, item.DateActivated);
            aItem.SubItems.Add(aDateActivatedItem);
            var aCommentItem = new ListViewItem.ListViewSubItem(aItem, item.Comment);
            aItem.SubItems.Add(aCommentItem);

            listView.Items.Add(aItem);
        }

        public static int ActivatedCount(this ListView listView)
        {
            return listView.Items.Cast<ListViewItem>().Count(item => item.ImageIndex != -1);
        }

        public static void SelectActivated(this ListView listView)
        {
            listView.SelectedItems.Clear();
            foreach (ListViewItem item in listView.Items)
            {
                if (item.ImageIndex != -1)
                {
                    item.Selected = true;
                }
            }
            listView.Select();
        }

        public static void SelectAllItems(this ListView listView)
        {
            foreach (ListViewItem item in listView.Items)
            {
                item.Selected = true;
            }
        }

        public static void UpdateDateActivated(this ListView listView, IPhoneProvider wrapper, string telephone, DateTime? date)
        {
            var item = listView.FindItem(telephone);
            item.UpdateDateActivated(wrapper, date);
        }

        public static void UpdateComment(this ListView listView, IPhoneProvider wrapper, string telephone, string comment)
        {
            var item = listView.FindItem(telephone);
            item.UpdateComment(wrapper, comment);
        }

        public static void UpdateDateActivatedSelected(this ListView listView, IPhoneProvider wrapper, DateTime? date)
        {
            if (listView.SelectedItems.Count != 0)
            {
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    item.UpdateDateActivated(wrapper, date);
                }
            }
        }

        public static IEnumerable<string> DeleteSelected(this ListView listView)
        {
            foreach (ListViewItem item in listView.SelectedItems)
            {
                listView.Items.Remove(item);
                yield return item.Text;
            }
        }

        public static void FilterItems(this ListView listView, List<XmlTelephoneItem> items, string groupName)
        {
            listView.Items.Clear();
            foreach (var item in items)
            {
                if (item.GroupName == groupName)
                {
                    listView.AddTelephone(item);
                }
            }
        }

        public static void ResetFilterItems(this ListView listView, List<XmlTelephoneItem> items)
        {
            listView.Items.Clear();
            foreach (var item in items)
            {
                listView.AddTelephone(item);
            }
        }

        public static IEnumerable<string> SelectedItems(this ListView listView)
        {
            foreach (ListViewItem item in listView.SelectedItems)
            {
                yield return item.Text;
            }
        }

        public static IEnumerable<string> NotCalledItems(this ListView listView)
        {
            foreach (ListViewItem item in listView.Items)
            {
                if (item.BackColor != Color.Aqua && item.BackColor != Color.Red)
                {
                    yield return item.Text;
                }
            }
        }
    }
}
