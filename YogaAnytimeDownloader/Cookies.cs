using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace YogaAnytimeDownloader
{
    public static class Cookies
    {
        // Credits to https://www.codeproject.com/Articles/330142/Cookie-Quest-A-Quest-to-Read-Cookies-from-Four-Pop
        #region FireFox
        private static string GetFireFoxCookiePath()
        {
            string s = Environment.GetFolderPath(
                             Environment.SpecialFolder.ApplicationData);
            s += @"\Mozilla\Firefox\Profiles\";

            try
            {
                DirectoryInfo di = new DirectoryInfo(s);
                DirectoryInfo[] dir = di.GetDirectories("*.default-release");
                if (dir.Length != 1)
                    return string.Empty;

                s += dir[0].Name + @"\" + "cookies.sqlite";
                Console.WriteLine(s);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }

            if (!File.Exists(s))
                return string.Empty;

            return s;
        }

        public static bool GetCookie_FireFox(string strHost, string strField, ref string Value)
        {
            Value = string.Empty;
            bool fRtn = false;
            string strPath, strTemp, strDb;
            strTemp = string.Empty;

            // Check to see if FireFox Installed
            strPath = GetFireFoxCookiePath();
            if (string.Empty == strPath) // Nope, perhaps another browser
                return false;

            try
            {
                // First copy the cookie jar so that we can read the cookies 
                // from unlocked copy while
                // FireFox is running
                strTemp = strPath + ".temp";
                strDb = "Data Source=" + strTemp;

                File.Copy(strPath, strTemp, true);

                // Now open the temporary cookie jar and extract Value from the cookie if
                // we find it.
                using (SQLiteConnection conn = new SQLiteConnection(strDb))
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT value FROM moz_cookies WHERE host LIKE '%" +
                            strHost + "%' AND name LIKE '%" + strField + "%';";

                        conn.Open();
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Value = reader.GetString(0);
                                if (!Value.Equals(string.Empty))
                                {
                                    fRtn = true;
                                    break;
                                }
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Value = string.Empty;
                fRtn = false;
            }

            // All done clean up
            if (string.Empty != strTemp)
            {
                //File.Delete(strTemp);
            }
            return fRtn;
        }
        #endregion

        #region Chrome

        ///
        ///  @brief Get the path to the Chrome cookie file.
        /// 
        ///  @return string The path if successful, otherwise an empty string
        /// 
        private static string GetChromeCookiePath()
        {
            string s = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);
            s += @"\Google\Chrome\User Data\Default\cookies";

            if (!File.Exists(s))
                return string.Empty;

            return s;
        }

        ///
        ///  @brief Get the Value from the Chrome cookie file.
        /// 
        ///  @param strHost The host or website name.
        ///  @param strField The cookie field name.
        ///  @param Value a string to recieve the Field Value if any found.
        /// 
        ///  @return bool true if successful
        /// 
        public static bool GetCookie_Chrome(string strHost, string strField, ref string Value)
        {
            Value = string.Empty;
            bool fRtn = false;
            string strPath, strDb;

            // Check to see if Chrome Installed
            strPath = GetChromeCookiePath();
            if (string.Empty == strPath) // Nope, perhaps another browser
                return false;

            try
            {
                strDb = "Data Source=" + strPath + ";pooling=false";

                using (SQLiteConnection conn = new SQLiteConnection(strDb))
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT value FROM cookies WHERE host_key LIKE '%" +
                            strHost + "%' AND name LIKE '%" + strField + "%';";

                        conn.Open();
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Value = reader.GetString(0);
                                if (!Value.Equals(string.Empty))
                                {
                                    fRtn = true;
                                    break;
                                }
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception)
            {
                Value = string.Empty;
                fRtn = false;
            }
            return fRtn;
        }

        #endregion //Chrome
    }
}
