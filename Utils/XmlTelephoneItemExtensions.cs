using System;
using System.Collections.Generic;
using MobileCaller.XML;
using System.Linq;

namespace MobileCaller.Utils
{
    public static class XmlTelephoneItemExtensions
    {
        public static int NonActivatedCount(this IEnumerable<XmlTelephoneItem> listTelephones)
        {
            return listTelephones.Count(x => x.DateActivated == String.Empty);
        }
    }
}
