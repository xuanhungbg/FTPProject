using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CopyFileViaFTP
{
    class INI_HELPER
    {
        public string INI_PATH;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);

        public string READ(string szSection, string szKey)
        {
            StringBuilder tmp = new StringBuilder(255);
            long i = GetPrivateProfileString(szSection, szKey, "", tmp, 255, INI_PATH);
            return tmp.ToString();
        }
        public void WRITE(string szSection, string szKey, string szData)
        {
            WritePrivateProfileString(szSection, szKey, szData, INI_PATH);
        }
    }
}
