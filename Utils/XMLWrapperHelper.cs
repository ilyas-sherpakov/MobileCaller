using System;
using MobileCaller.Enums;

namespace MobileCaller.Utils
{
    static class XmlWrapperHelper
    {
        /// <summary>
        /// Return the name of group for first numbers of phone
        /// </summary>
        /// <param name="telephone">Telephone number</param>
        /// <returns>Group of name</returns>
        /// <see cref="http://ktozvonit.com.ua/operators/">Receive info about telephone diapason</see>
        public static string ResolvePhoneGroupName(string telephone)
        {
            var groupName = String.Empty;
            switch (telephone.Substring(0, 6))
            {
                case "+38093":
                case "+38063":
                case "+38073":
                    groupName = Operator.Life;
                    break;
                case "+38066":
                case "+38095":
                case "+38050":
                case "+38099":
                    groupName = Operator.MTC;
                    break;
                case "+38068": // Абоненти «Beeline» означають абонентів телекомунікаційної мережі «Київстар», яким присвоєні телефонні номери з кодом NDC 068.
                case "+38067":
                case "+38096":
                case "+38097":
                case "+38098":
                    groupName = Operator.Kievstar;
                    break;
            }
            return groupName;
        }

        /// <summary>
        /// Change phone to international format.
        /// </summary>
        /// <param name="telephone">Source local or international number.</param>
        /// <returns>Target international number.</returns>
        public static string ResolvePhoneNumber(string telephone)
        {
            if (!telephone.StartsWith("+"))
            {
                return string.Format("+38{0}", telephone);
            }
            return telephone;
        }
    }
}
