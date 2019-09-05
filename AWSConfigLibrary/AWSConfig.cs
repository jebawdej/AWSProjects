using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AWSConfigLibrary
{
    public class AWSConfig
    {
        public string path;
        // string Path = System.Environment.CurrentDirectory+"\\"+"DVSAConfigFile.cfg";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Path">Path to the config file</param>
        public AWSConfig(string Path)
        {
            path = Path;
        }
        public void WriteConfigValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        public string ReadConfigValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, this.path);
            return temp.ToString();
        }

        public void CreatePfxFile(string filename, string Passw)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "openssl.exe";
            startInfo.Arguments = String.Format("");
            startInfo.Verb = "runas";
            process.StartInfo = startInfo;
            process.Start();
        }

    }
}
