using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace MobileCaller.XML
{
    public class XmlCountrySettings : IChangeTracking, ICloneable
    {
        #region Fields

        private string _countryName = String.Empty;
        private List<string> _groupNameList = new List<string>();

        #endregion

        #region Implementing of IChangeTracking

        public bool IsChanged { get; private set; }

        public void AcceptChanges()
        {
            IsChanged = false;
        }

        #endregion

        #region Implementing of IClonable

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        #region Properties

        public string CountryName
        {
            get
            {
                Debug.Assert(_countryName != null);
                return _countryName;
            }
            set
            {
                var val = value ?? String.Empty;
                if (_countryName != val)
                {
                    _countryName = val;
                    IsChanged = true;
                }
            }
        }

        public List<string> GroupNameList
        {
            get
            {
                Debug.Assert(_groupNameList != null);
                return _groupNameList;
            }
            set
            {
                var val = value ?? new List<string>();
                if (_groupNameList != val)
                {
                    _groupNameList = val;
                    IsChanged = true;
                }
            }
        }

        #endregion
    }
}
