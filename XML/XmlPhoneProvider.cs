using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using MobileCaller.Collections;
using MobileCaller.Enums;
using MobileCaller.Utils;

namespace MobileCaller.XML
{
    class XmlPhoneProvider : IPhoneProvider, IChangeTracking
    {
        #region Constants

        private readonly string _fileName;
        // Current encoding is used to work with russian words
        private readonly Encoding _encoding = Encoding.GetEncoding("windows-1251");

        private const string XmlSchemaPath = "Resources.XmlPhoneProviderSchema.xsd";

        /// <summary>
        /// The namespace for the configuration document.
        /// </summary>
        private const string XmlNamespace = "urn:schemas-taycoon-com:xml-phone-provider";

        #endregion

        #region Fields

        private XmlNamespaceManager _namespaceManager;

        private XmlModemSettings _modemSettings = new XmlModemSettings();
        private XmlCallSettings _callSettings = new XmlCallSettings();
        private VariantList<XmlGroupSettings> _groupSettings = new VariantList<XmlGroupSettings>();
        private VariantList<XmlCountrySettings> _countrySettings = new VariantList<XmlCountrySettings>();
        private VariantList<XmlTelephoneItem> _telephoneItems = new VariantList<XmlTelephoneItem>();

        #endregion

        #region Constructors

        public XmlPhoneProvider(string workingDirectory)
        {
            _fileName = Path.Combine(workingDirectory, "data.xml");
        }

        #endregion

        #region Properties

        public XmlModemSettings ModemSettings
        {
            get { return _modemSettings; }
            set { _modemSettings = value; }
        }

        public XmlCallSettings CallSettings
        {
            get { return _callSettings; }
            set { _callSettings = value; }
        }

        public VariantList<XmlGroupSettings> GroupSettings
        {
            get { return _groupSettings; }
            set { _groupSettings = value; }
        }

        public VariantList<XmlCountrySettings> CountrySettings
        {
            get { return _countrySettings; }
            set { _countrySettings = value; }
        }

        public VariantList<XmlTelephoneItem> TelephoneItems
        {
            get { return _telephoneItems; }
            set { _telephoneItems = value; }
        }

        #endregion

        #region Implementing of IChangeTracking

        public bool IsChanged
        {
            get
            {
                return ModemSettings.IsChanged || CallSettings.IsChanged || GroupSettings.IsChanged || TelephoneItems.IsChanged;
            }
        }

        public void AcceptChanges()
        {
            CallSettings.AcceptChanges();
            ModemSettings.AcceptChanges();
            GroupSettings.AcceptChanges();
            TelephoneItems.AcceptChanges();
        }

        #endregion

        #region Methods

        private static void WriteElement(XmlWriter wr, string name, string text)
        {
            wr.WriteStartElement(name);
            wr.WriteRaw(text);
            wr.WriteEndElement();
        }

        public void SaveXml()
        {
            using (var output = new StreamWriter(new FileStream(_fileName, FileMode.Create), _encoding))
            {
                var settings = new XmlWriterSettings();

                // включаем отступ для элементов XML документа
                // (позволяет наглядно изобразить иерархию XML документа)
                settings.Indent = true;
                settings.IndentChars = "    "; // задаем отступ, здесь у меня 4 пробела

                // задаем переход на новую строку
                settings.NewLineChars = "\n";

                // Нужно ли опустить строку декларации формата XML документа
                // речь идет о строке вида "<?xml version="1.0" encoding="utf-8"?>"
                settings.OmitXmlDeclaration = false;

                using (XmlWriter wr = XmlWriter.Create(output, settings))
                {
                    //wr.WriteDocType("Application", "-//W3C//DTD XHTML 1.0 Transitional//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd", null);
                    wr.WriteStartElement("Application", XmlNamespace);

                    wr.WriteStartElement("ModemSettings");
                    WriteElement(wr, "Port", _modemSettings.ComPort);
                    WriteElement(wr, "BaudRate", _modemSettings.BaudRate.ToString());
                    wr.WriteEndElement();

                    wr.WriteStartElement("CallSettings");
                    WriteElement(wr, "WaitCall", _callSettings.WaitCall.ToString());
                    WriteElement(wr, "SendNotification", _callSettings.SendNotification.ToXmlString());
                    WriteElement(wr, "PlaySound", _callSettings.PlaySound.ToXmlString());
                    WriteElement(wr, "Repeatable", _callSettings.Repeatable.ToXmlString());
                    WriteElement(wr, "Shutdown", _callSettings.Shutdown.ToXmlString());
                    wr.WriteEndElement();

                    foreach (var groupSetting in _groupSettings)
                    {
                        wr.WriteStartElement("GroupSettings");
                        wr.WriteAttributeString("GroupName", groupSetting.GroupName);
                        WriteElement(wr, "WaitAnswer", groupSetting.WaitAnswer.ToString());
                        WriteElement(wr, "DoubleCheckOnTimeout", groupSetting.DoubleCheckOnTimeout.ToXmlString());
                        WriteElement(wr, "SMSRecipient", groupSetting.SmsRecipient);
                        WriteElement(wr, "SMSText", groupSetting.SmsText);
                        WriteElement(wr, "USSDText", groupSetting.UssdText);
                        WriteElement(wr, "NotificationType", groupSetting.NotificationType.ToString());
                        wr.WriteEndElement();
                    }

                    foreach (var countrySetting in _countrySettings)
                    {
                        wr.WriteStartElement("CountrySettings");
                        wr.WriteAttributeString("CountryName", countrySetting.CountryName);
                        foreach (var groupName in countrySetting.GroupNameList)
                        {
                            WriteElement(wr, "GroupName", groupName);
                        }
                        wr.WriteEndElement();
                    }

                    wr.WriteStartElement("Phones");
                    foreach (var telephoneItem in _telephoneItems)
                    {
                        wr.WriteStartElement("Phone");
                        wr.WriteAttributeString("Date", telephoneItem.Date);
                        wr.WriteAttributeString("DateActivated", telephoneItem.DateActivated);
                        wr.WriteAttributeString("Comment", telephoneItem.Comment);
                        wr.WriteAttributeString("GroupName", telephoneItem.GroupName);
                        wr.WriteRaw(telephoneItem.Telephone);
                        wr.WriteEndElement();
                    }
                    wr.WriteEndElement();

                    wr.WriteEndElement();
                }
            }
            AcceptChanges();
        }

        public void LoadXml()
        {
            try
            {
                using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open), _encoding))
                {
                    var document = new XmlDocument();
                    document.Load(reader, new XmlSchemaToken(Assembly.GetExecutingAssembly(), XmlSchemaPath, XmlNamespace));
                    _namespaceManager = new XmlNamespaceManager(document.NameTable);
                    _namespaceManager.AddNamespace("tn", XmlNamespace);

                    var modemSettingsElem = (XmlElement)document.SelectSingleNode("/tn:Application/tn:ModemSettings", _namespaceManager);
                    if (modemSettingsElem == null)
                        modemSettingsElem = (XmlElement)document.FirstChild.SelectSingleNode("ModemSettings");
                    FromXmlElement(modemSettingsElem, _modemSettings);

                    var callSettingsElem = (XmlElement)document.SelectSingleNode("/tn:Application/tn:CallSettings", _namespaceManager);
                    if (callSettingsElem == null)
                        callSettingsElem = (XmlElement)document.FirstChild.SelectSingleNode("CallSettings");
                    FromXmlElement(callSettingsElem, _callSettings);

                    var groupSettingsElems = document.SelectNodes("/tn:Application/tn:GroupSettings", _namespaceManager);
                    if (groupSettingsElems == null || groupSettingsElems.Count == 0)
                        groupSettingsElems = document.FirstChild.SelectNodes("GroupSettings");
                    FromXmlElements(groupSettingsElems, _groupSettings);

                    var countrySettingsElems = document.SelectNodes("/tn:Application/tn:CountrySettings", _namespaceManager);
                    if (countrySettingsElems == null || countrySettingsElems.Count == 0)
                        countrySettingsElems = document.FirstChild.SelectNodes("CountrySettings");
                    FromXmlElements(countrySettingsElems, _countrySettings);

                    var phonesElem = document.SelectSingleNode("/tn:Application/tn:Phones", _namespaceManager);
                    if (phonesElem == null)
                        phonesElem = document.FirstChild.SelectSingleNode("Phones");
                    if (phonesElem != null)
                        FromXmlElements(phonesElem.ChildNodes, _telephoneItems);
                }
            }
            catch (FileNotFoundException)
            {
                AddMissingGroupSetting();
                AddMissingCountrySetting();
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message + ex.StackTrace);
            }
            AcceptChanges();
        }

        private void AddMissingGroupSetting()
        {
            if (!_groupSettings.Exists(x => x.GroupName == Operator.MTC))
            {
                var settings = new XmlGroupSettings();
                settings.GroupName = Operator.MTC;
                settings.WaitAnswer = 25;
                settings.DoubleCheckOnTimeout = true;
                settings.SmsRecipient = "7422";
                settings.SmsText = "%PHONE%";
                _groupSettings.Add(settings);
            }
            if (!_groupSettings.Exists(x => x.GroupName == Operator.Life))
            {
                var settings = new XmlGroupSettings();
                settings.GroupName = Operator.Life;
                settings.WaitAnswer = 35;
                settings.DoubleCheckOnTimeout = false;
                settings.SmsRecipient = "1470";
                settings.SmsText = "38%PHONE%";
                _groupSettings.Add(settings);
            }
            if (!_groupSettings.Exists(x => x.GroupName == Operator.Kievstar))
            {
                var settings = new XmlGroupSettings();
                settings.GroupName = Operator.Kievstar;
                settings.WaitAnswer = 15;
                settings.DoubleCheckOnTimeout = true;
                settings.UssdText = "*143*%PHONE%#";
                settings.NotificationType = NotificationType.USSD;
                _groupSettings.Add(settings);
            }
        }

        private void AddMissingCountrySetting()
        {
            if (!_countrySettings.Exists(x => x.CountryName == Country.Ukraine))
            {
                var settings = new XmlCountrySettings();
                settings.CountryName = Country.Ukraine;
                settings.GroupNameList.Add(Operator.MTC);
                settings.GroupNameList.Add(Operator.Life);
                settings.GroupNameList.Add(Operator.Kievstar);
                _countrySettings.Add(settings);
            }
            if (!_countrySettings.Exists(x => x.CountryName == Country.Russia))
            {
                var settings = new XmlCountrySettings();
                settings.CountryName = Country.Russia;
                _countrySettings.Add(settings);
            }
        }

        private void FromXmlElement(XmlElement elem, XmlModemSettings modemSettings)
        {
            if (elem == null)
                return;
            var portElem = (XmlElement)elem.SelectSingleNode("tn:Port", _namespaceManager);
            if (portElem == null)
                portElem = (XmlElement)elem.SelectSingleNode("Port");
            if (portElem != null)
            {
                modemSettings.ComPort = portElem.InnerText;
            }
            var baudRateElem = (XmlElement)elem.SelectSingleNode("tn:BaudRate", _namespaceManager);
            if (baudRateElem == null)
                baudRateElem = (XmlElement)elem.SelectSingleNode("BaudRate");
            if (baudRateElem != null)
            {
                modemSettings.BaudRate = Int32.Parse(baudRateElem.InnerText);
            }
        }

        private void FromXmlElement(XmlElement elem, XmlCallSettings callSettings)
        {
            if (elem == null)
                return;
            var waitCallElem = (XmlElement)elem.SelectSingleNode("tn:WaitCall", _namespaceManager);
            if (waitCallElem == null)
                waitCallElem = (XmlElement)elem.SelectSingleNode("WaitCall");
            if (waitCallElem != null)
            {
                callSettings.WaitCall = Int32.Parse(waitCallElem.InnerText);
            }
            var sendNotificationElem = (XmlElement)elem.SelectSingleNode("tn:SendNotification", _namespaceManager);
            if (sendNotificationElem == null)
                sendNotificationElem = (XmlElement)elem.SelectSingleNode("SendNotification");
            if (sendNotificationElem != null)
            {
                callSettings.SendNotification = Boolean.Parse(sendNotificationElem.InnerText);
            }
            var playSoundElem = (XmlElement)elem.SelectSingleNode("tn:PlaySound", _namespaceManager);
            if (playSoundElem == null)
                playSoundElem = (XmlElement)elem.SelectSingleNode("PlaySound");
            if (playSoundElem != null)
            {
                callSettings.PlaySound = Boolean.Parse(playSoundElem.InnerText);
            }
            var repeatableElem = (XmlElement)elem.SelectSingleNode("tn:Repeatable", _namespaceManager);
            if (repeatableElem == null)
                repeatableElem = (XmlElement)elem.SelectSingleNode("Repeatable");
            if (repeatableElem != null)
            {
                callSettings.Repeatable = Boolean.Parse(repeatableElem.InnerText);
            }
            var shutdownElem = (XmlElement)elem.SelectSingleNode("tn:Shutdown", _namespaceManager);
            if (shutdownElem == null)
                shutdownElem = (XmlElement)elem.SelectSingleNode("Shutdown");
            if (shutdownElem != null)
            {
                callSettings.Shutdown = Boolean.Parse(shutdownElem.InnerText);
            }
        }

        private void FromXmlElements(XmlNodeList elems, IList<XmlGroupSettings> groupSettings)
        {
            if (elems != null)
            {
                foreach (XmlElement elem in elems)
                {
                    var newGroupSettings = new XmlGroupSettings();
                    FromXmlElement(elem, newGroupSettings);
                    groupSettings.Add(newGroupSettings);
                }
            }
            AddMissingGroupSetting();
        }

        private void FromXmlElement(XmlElement elem, XmlGroupSettings groupSettings)
        {
            if (elem == null)
                return;
            groupSettings.GroupName = elem.GetAttribute("GroupName");
            var waitAnswerElem = (XmlElement)elem.SelectSingleNode("tn:WaitAnswer", _namespaceManager);
            if (waitAnswerElem == null)
                waitAnswerElem = (XmlElement)elem.SelectSingleNode("WaitAnswer");
            if (waitAnswerElem != null)
            {
                groupSettings.WaitAnswer = Int32.Parse(waitAnswerElem.InnerText);
            }
            var doubleCheckOnTimeoutElem = (XmlElement)elem.SelectSingleNode("tn:DoubleCheckOnTimeout", _namespaceManager);
            if (doubleCheckOnTimeoutElem == null)
                doubleCheckOnTimeoutElem = (XmlElement)elem.SelectSingleNode("DoubleCheckOnTimeout");
            if (doubleCheckOnTimeoutElem != null)
            {
                groupSettings.DoubleCheckOnTimeout = Boolean.Parse(doubleCheckOnTimeoutElem.InnerText);
            }
            var smsRecipientElem = (XmlElement)elem.SelectSingleNode("tn:SMSRecipient", _namespaceManager);
            if (smsRecipientElem == null)
                smsRecipientElem = (XmlElement)elem.SelectSingleNode("SMSRecipient");
            if (smsRecipientElem != null)
            {
                groupSettings.SmsRecipient = smsRecipientElem.InnerText;
            }
            var smsTextElem = (XmlElement)elem.SelectSingleNode("tn:SMSText", _namespaceManager);
            if (smsTextElem == null)
                smsTextElem = (XmlElement)elem.SelectSingleNode("SMSText");
            if (smsTextElem != null)
            {
                groupSettings.SmsText = smsTextElem.InnerText;
            }
            var ussdTextElem = (XmlElement)elem.SelectSingleNode("tn:USSDText", _namespaceManager);
            if (ussdTextElem == null)
                ussdTextElem = (XmlElement)elem.SelectSingleNode("USSDText");
            if (ussdTextElem != null)
            {
                groupSettings.UssdText = ussdTextElem.InnerText;
            }
            var notificationTypeElem = (XmlElement)elem.SelectSingleNode("tn:NotificationType", _namespaceManager);
            if (notificationTypeElem == null)
                notificationTypeElem = (XmlElement)elem.SelectSingleNode("NotificationType");
            if (notificationTypeElem != null)
            {
                groupSettings.NotificationType = (NotificationType)Enum.Parse(typeof(NotificationType), notificationTypeElem.InnerText);
            }
        }

        private void FromXmlElements(XmlNodeList elems, IList<XmlCountrySettings> countrySettings)
        {
            if (elems != null)
            {
                foreach (XmlElement elem in elems)
                {
                    var newCountrySettings = new XmlCountrySettings();
                    FromXmlElement(elem, newCountrySettings);
                    countrySettings.Add(newCountrySettings);
                }
            }
            AddMissingCountrySetting();
        }

        private void FromXmlElement(XmlElement elem, XmlCountrySettings countrySettings)
        {
            if (elem == null)
                return;
            countrySettings.CountryName = elem.GetAttribute("CountryName");
            var groupNameElems = elem.SelectNodes("tn:GroupName", _namespaceManager);
            if (groupNameElems == null || groupNameElems.Count == 0)
                groupNameElems = elem.SelectNodes("GroupName");
            foreach (XmlElement groupNameElem in groupNameElems)
            {
                if (groupNameElem != null)
                {
                    countrySettings.GroupNameList.Add(groupNameElem.InnerText);
                }
            }
        }

        private static void FromXmlElements(XmlNodeList elems, IList<XmlTelephoneItem> telephoneItems)
        {
            if (elems != null)
            {
                foreach (XmlElement elem in elems)
                {
                    var newTelephoneItem = new XmlTelephoneItem();
                    FromXmlElement(elem, newTelephoneItem);
                    telephoneItems.Add(newTelephoneItem);
                }
            }
        }

        private static void FromXmlElement(XmlElement elem, XmlTelephoneItem telephoneItem)
        {
            if (elem == null)
                return;
            telephoneItem.Date = elem.GetAttribute("Date");
            telephoneItem.DateActivated = elem.GetAttribute("DateActivated");
            telephoneItem.Comment = elem.GetAttribute("Comment");
            telephoneItem.GroupName = elem.GetAttribute("GroupName");
            telephoneItem.Telephone = XmlWrapperHelper.ResolvePhoneNumber(elem.InnerText);

            if (telephoneItem.GroupName == String.Empty)
                telephoneItem.GroupName = XmlWrapperHelper.ResolvePhoneGroupName(telephoneItem.Telephone);
        }

        #endregion
    }
}

