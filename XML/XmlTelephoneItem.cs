using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MobileCaller.XML
{
    public class XmlTelephoneItem : IChangeTracking
    {
        #region Fields

        private string _telephone = String.Empty;
        private string _date = String.Empty;
        private string _dateActivated = String.Empty;
        private string _comment = String.Empty;
        private string _groupName = String.Empty;

        #endregion

        #region Constructors

        public XmlTelephoneItem()
        {
        }

        public XmlTelephoneItem(string telephone, string groupName, string date, string dateActivated, string comment)
        {
            Telephone = telephone;
            GroupName = groupName;
            Date = date;
            DateActivated = dateActivated;
            Comment = comment;
        }

        public XmlTelephoneItem(string telephone, string groupName, DateTime date)
        {
            Telephone = telephone;
            GroupName = groupName;
            Date = date.ToString("dd.MM.yyyy");
            DateActivated = String.Empty;
        }

        #endregion

        #region Implementing of IChangeTracking

        public bool IsChanged { get; private set; }

        public void AcceptChanges()
        {
            IsChanged = false;
        }

        #endregion

        #region Properties

        public string Telephone
        {
            get
            {
                Debug.Assert(_telephone != null);
                return _telephone;
            }
            set
            {
                var val = value ?? String.Empty;
                if (_telephone != val)
                {
                    _telephone = val.Length < 10 ? val.PadLeft(10, '0') : val;
                    IsChanged = true;
                }
            }
        }
        public string Date
        {
            get
            {
                Debug.Assert(_date != null);
                return _date;
            }
            set
            {
                var val = value ?? String.Empty;
                if (_date != val)
                {
                    _date = val;
                    IsChanged = true;
                }
            }
        }
        public string DateActivated
        {
            get
            {
                Debug.Assert(_dateActivated != null);
                return _dateActivated;
            }
            set
            {
                var val = value ?? String.Empty;
                if (_dateActivated != val)
                {
                    _dateActivated = val;
                    IsChanged = true;
                }
            }
        }
        public string Comment
        {
            get
            {
                Debug.Assert(_comment != null);
                return _comment;
            }
            set
            {
                var val = value ?? String.Empty;
                if (_comment != val)
                {
                    _comment = val;
                    IsChanged = true;
                }
            }
        }
        public string GroupName
        {
            get
            {
                Debug.Assert(_groupName != null);
                return _groupName;
            }
            set
            {
                var val = value ?? String.Empty;
                if (_groupName != val)
                {
                    _groupName = val;
                    IsChanged = true;
                }
            }
        }

        #endregion

        #region Methods

        public void SetDateActivated(DateTime date)
        {
            DateActivated = date.ToString("dd.MM.yyyy");
        }

        public bool IsActivated()
        {
            return DateActivated != String.Empty;
        }

        #endregion
    }
}
