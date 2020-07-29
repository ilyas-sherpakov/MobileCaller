using MobileCaller.Collections;

namespace MobileCaller.XML
{
    public interface IPhoneProvider
    {
        XmlModemSettings ModemSettings { get; set; }
        XmlCallSettings CallSettings { get; set; }
        VariantList<XmlGroupSettings> GroupSettings { get; set; }
        VariantList<XmlCountrySettings> CountrySettings { get; set; }
        VariantList<XmlTelephoneItem> TelephoneItems { get; set; }

        bool IsChanged { get; }

        void LoadXml();
        void SaveXml();
    }
}
