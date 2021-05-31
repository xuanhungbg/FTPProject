using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CopyFileViaFTP
{
    class ATC_FTPSupport
    {
        public static ATC_FTPSupport FTP;

        public string HOST = "";
        public string USER_NAME = "";
        public string PASSWORD = "";

        public bool CreateFolder(string dir)
        {
            bool IsCreated = true;
            try
            {
                WebRequest request = WebRequest.Create(dir);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Create Folder fail " + dir);
                Debug.WriteLine("Create Folder fail ex " + ex.ToString());
                IsCreated = false;
            }
            return IsCreated;
        }

        public bool DoesFtpDirectoryExist(string dirPath)
        {
            bool isexist = false;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirPath);
                request.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    isexist = true;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;
                    }
                }
            }
            return isexist;
        }

        public bool UploadFile(string _from_path, string _to_path)
        {
            //string From = @"F:\Kaushik\Test.xlsx";
            //string To = "ftp://192.168.1.103:24/directory/Test.xlsx";
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
                    client.UploadFile(_to_path, WebRequestMethods.Ftp.UploadFile, _from_path);
                }
               
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Upload fail " + ex.ToString());
                return false;
            }
        }

        public void DeleteFTPFolder(string Folderpath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Folderpath);
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            request.Credentials = new NetworkCredential(USER_NAME, PASSWORD); ;
            request.GetResponse().Close();
        }
    }
}
