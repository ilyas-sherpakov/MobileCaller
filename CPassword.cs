using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using MobileCaller.Localization;

namespace MobileCaller
{
    /// <summary>
    /// This type is needed to control if program is licensed to work with IMEI mobile.
    /// </summary>
    /// <remarks>The approach is used from 
    /// <a href = "http://www.rsdn.ru/article/files/Classes/passwd.xml">Класс для работы с паролями в среде .NET</a>
    /// </remarks>
    internal static class CPassword
    {
        #region Fields

        private const string Password = "PNrXhxIZ";
        private const string RegFileName = "Reg.dat";

        private static string m_login;
        private static string m_email;
        private static string m_imei;

        private static byte[] m_salt;       // синхропосылка - из всех составляющих регистрации (login, e-mail, IMEI)
        private static byte[] m_hash;       // хэш пароля

        #endregion

        #region Properties

        public static string WorkingDirectory { get; set; }

        public static string User
        {
            get { return m_login; }
        }

        public static string IMEI
        {
            get { return m_imei; }
        }

        #endregion

        /// <summary>
        /// Два строковых аргумента используются для преобразования синхропосылки и 
        /// хэша в двоичное представление и сохраняется в соответствующем поле класса
        /// </summary>
        /// <param name="salt">Синхропосылка</param>
        /// <param name="hash">Хэш из base64</param>
        public static void UpdateHash(string salt, string hash)
        {
            m_salt = new byte[Encoding.UTF8.GetMaxByteCount(salt.Length)];
            Encoding.UTF8.GetBytes(salt.ToCharArray(), 0, salt.Length,
                  m_salt, 0);
            try
            {
                m_hash = Convert.FromBase64String(hash);
            }
            catch (FormatException)
            {
                // miss exception and this sign for Validation function that user doesn't have license
            }
        }

        private static bool OpenRegFile(string fileName)
        {
            bool bResult = true;
            if (!File.Exists(fileName))
            {
                bResult = false;
            }
            else
            {
                var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.Default))
                {
                    string line;
                    int i = 0;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        switch (i)
                        {
                            case 0:
                                {
                                    m_login = line;
                                    break;
                                }
                            case 1:
                                {
                                    m_email = line;
                                    break;
                                }
                            case 2:
                                {
                                    m_imei = line;
                                    line = m_login + m_email + m_imei + "'";
                                    m_salt = new byte[Encoding.UTF8.GetMaxByteCount(line.Length)];
                                    Encoding.UTF8.GetBytes(line.ToCharArray(), 0, line.Length, m_salt, 0);
                                    break;
                                }
                            case 3:
                                {
                                    try
                                    {
                                        m_hash = Convert.FromBase64String(line);
                                    }
                                    catch (FormatException)
                                    {
                                        // miss exception and this sign for Validation function that user doesn't have license
                                    }
                                    break;
                                }
                        }
                        ++i;
                    }
                }
            }

            if ((m_hash == null) || (m_salt == null))
            {
                bResult = false;
            }

            return bResult;
        }

        public static void GetRegistrationInfo()
        {
            OpenRegFile(Path.Combine(WorkingDirectory, RegFileName));
        }

        /// <summary>
        /// Update registration information
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="email">User e-mail</param>
        /// <param name="imei">Mobile phone IMEI</param>
        public static void UpdateRegistrationInfo(string login, string email, string imei)
        {
            m_login = login;
            m_email = email;
            m_imei = imei;

            SaveRegFile(Path.Combine(WorkingDirectory, RegFileName), login + 
                Environment.NewLine + email + 
                Environment.NewLine + imei + 
                Environment.NewLine + Convert.ToBase64String(m_hash));
        }

        private static void SaveRegFile(string fileName, string regData)
        {
            try
            {
                using (var streamWriter = new StreamWriter(fileName))
                {
                    streamWriter.WriteLine(regData);
                }
            }
            catch (Exception)
            {
                var culture = Application.CurrentCulture;
                MessageBox.Show(String.Format(ResourceManagerProvider.GetLocalizedString("MSG_FILE_WAS_NOT_CREATED", culture), fileName),
                                ResourceManagerProvider.GetLocalizedString("MSG_SYSTEM_ERROR_TITLE", culture),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Вычисляет хэш на основе открытого пароля, используя внутренний метод HashPassword,
        /// после чего сравнивает байты вычисленного хэша с байтами сохраненного/загруженного хэша.
        /// </summary>
        /// <returns>Признак совпадения всех до одного байтов хэша. Если совпали, то пароль верный.</returns>
        public static bool Verify()
        {
            if ((m_hash == null) || (m_salt == null))
            {
                return false;
            }
            byte[] hash = HashPassword();

            if (hash.Length == m_hash.Length)
            {
                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != m_hash[i])
                        return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Записывает вычисленную синхропосылку и скрытый пароль в поток на основе массива байтов, 
        /// а затем вычисляет хэш содержимого потока. В качестве хэш-функции используется алгоритм SHA-256.
        /// </summary>
        /// <returns>хэш содержимого потока для сравнения</returns>
        private static byte[] HashPassword()
        {
            char[] password = Password.ToCharArray();
            byte[] hash;
            // Create the array with length enough to put calculated hash
            byte[] data = new byte[m_salt.Length + Encoding.UTF8.GetMaxByteCount(password.Length)];
            try
            {
                // Copy salt at start of array
                Array.Copy(m_salt, 0, data, 0, m_salt.Length);
                // Copy secure password after salt converting to UTF-8
                int byteCount = Encoding.UTF8.GetBytes(password, 0, password.Length, data, m_salt.Length);
                // Compute hash for array
                using (HashAlgorithm alg = new SHA256Managed())
                {
                    hash = alg.ComputeHash(data, 0, m_salt.Length + byteCount);
                }
            }
            finally
            {
                // Clear array to avoid open password leak for hackers
                Array.Clear(data, 0, data.Length);
            }
            return hash;
        }
    }
}
