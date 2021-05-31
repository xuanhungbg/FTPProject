using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyFileViaFTP
{
    public partial class Form1 : Form
    {
        INI_HELPER INI;
        bool isConfig = false;
        string _source_log_path = "";
        string _ftp_log_path = "";
        //string _log_ext = "";
        string _log_file_prefix = "";
        string _source_image_path = "";
        string _ftp_image_path = "";

        bool isGiveLogRunning = false;
        bool isGiveImgRunning = false;

        public Form1()
        {
            InitializeComponent();
            tm_Clock.Start();
            tm_Process.Start();
            INI = new INI_HELPER();
            ATC_FTPSupport.FTP = new ATC_FTPSupport();
            TraceListener debugListener = new MyTraceListener(rtb_Monitor);
            Debug.Listeners.Add(debugListener);
            //Trace.Listeners.Add(debugListener);
        }

        private void btn_Config_Click(object sender, EventArgs e)
        {
            if (isGiveImgRunning || isGiveLogRunning)
            {
                MessageBox.Show("Please stop first");
                return;
            }
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "INI files|*.ini";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                INI.INI_PATH = theDialog.FileName;
                INI_Config();
            }
        }

        private void INI_Config()
        {
            try
            {
                ATC_FTPSupport.FTP.HOST = INI.READ("COPYTOFTP", "host");
                ATC_FTPSupport.FTP.USER_NAME = INI.READ("COPYTOFTP", "user_name");
                ATC_FTPSupport.FTP.PASSWORD = INI.READ("COPYTOFTP", "password");
                _source_log_path = INI.READ("COPYTOFTP", "source_log_path");
                _ftp_log_path = INI.READ("COPYTOFTP", "ftp_log_path");
                //_log_ext = INI.READ("COPYTOFTP", "log_ext");
                _log_file_prefix = INI.READ("COPYTOFTP", "log_file_name");
                _source_image_path = INI.READ("COPYTOFTP", "source_image_path");
                _ftp_image_path = INI.READ("COPYTOFTP", "ftp_image_path");
                Debug.WriteLine("config success!");
                isConfig = true;
                CreateFTPFolder();
                //rtb_Monitor.AppendText(DoesFtpDirectoryExist(HOST + _ftp_log_path).ToString());
                MessageBox.Show("config success!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("config fail !" + ex.ToString());
                isConfig = false;
                MessageBox.Show("config fail!");
            }
        }

        private void CreateFTPFolder()
        {
            string dir = ATC_FTPSupport.FTP.HOST;

            string [] _ftp_log_path_arr = _ftp_log_path.Split('/');
            for (int i = 0; i< _ftp_log_path_arr.Length; i++)
            {
                if (_ftp_log_path_arr[i] == "") continue;
                dir += @"/" + _ftp_log_path_arr[i];
                if (!ATC_FTPSupport.FTP.DoesFtpDirectoryExist(dir))
                {
                    if (!ATC_FTPSupport.FTP.CreateFolder(dir))
                    {
                        MessageBox.Show(string.Format("Create folder {0} fail. ", dir));
                        return;
                    }

                }
            }
            dir = ATC_FTPSupport.FTP.HOST;
            string[] _ftp_image_path_arr = _ftp_image_path.Split('/');
            for (int i = 0; i < _ftp_image_path_arr.Length; i++)
            {
                if (_ftp_image_path_arr[i] == "") continue;
                dir += @"/" + _ftp_image_path_arr[i];
                if (!ATC_FTPSupport.FTP.DoesFtpDirectoryExist(dir))
                {
                    if (!ATC_FTPSupport.FTP.CreateFolder(dir))
                    {
                        MessageBox.Show(string.Format("Create folder {0} fail. ", dir));
                        return;
                    }

                }
            }
            Debug.WriteLine("Create FTP folder success");
        }

        private void btn_Log_Click(object sender, EventArgs e)
        {
            if (!isConfig)
            {
                MessageBox.Show("Please config first!");
                return;
            }
            //ATC_FTPSupport.FTP.UploadFile(@"C:\CopyToFtp\Project\Log\log_aaaaa.log", ATC_FTPSupport.FTP.HOST + @"/" + _ftp_log_path + @"/log_aaaaa.log");
            if ("GIVE LOG".Equals(btn_Log.Text))
            {
                btn_Log.Text = "STOP";
                Debug.WriteLine("Start Give log");
                DateTime t = DateTime.Now + new TimeSpan(0, 10, 0);
                Debug.WriteLine(string.Format("Time run to give log {0}", t.ToString("MM/dd/yyyy HH:") + Convert.ToInt32(t.ToString("mm"))/10 +"0:59"));
                isGiveLogRunning = true;
            } else
            {
                Debug.WriteLine("Stop Give log");
                btn_Log.Text = "GIVE LOG";
                isGiveLogRunning = false;
            }

        }

        private void tm_Clock_Tick(object sender, EventArgs e)
        {
            lb_Time.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            //lb_Time.Text = DateTime.Now.ToString("yyMMdd");
        }

        private void tm_Process_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToString("mm:ss").EndsWith("3:59"))
            {
                DateTime _time = DateTime.Now - new TimeSpan(0, 1, 0);
                if (isGiveLogRunning)
                {
                    UploadLogToFTP(_time);
                }
            }
        }

        private void UploadLogToFTP(DateTime time)
        {
            string _file_Name = string.Format(_log_file_prefix, time.ToString("yyyyMMdd"));
            Debug.WriteLine("Uploading log file: " + _file_Name);
            string _path = _source_log_path + @"/" + _file_Name;
            string _ftp_path = ATC_FTPSupport.FTP.HOST + @"/" + _ftp_log_path + @"/" + _file_Name;
            if (!File.Exists(_path))
            {
                Debug.WriteLine("Up log fail wrong log path " + _path);
                return;
            }
            if (ATC_FTPSupport.FTP.UploadFile(_path, _ftp_path))
            {
                Debug.WriteLine("Up log success to " + _ftp_path);
            }
        }

        private void btn_Image_Click(object sender, EventArgs e)
        {
            if (!isConfig)
            {
                MessageBox.Show("Please config first!");
                return;
            }
            if ("GIVE IMAGE".Equals(btn_Image.Text))
            {
                btn_Image.Text = "STOP";
                Debug.WriteLine("Start Give Image");

                FileSystemWatcher _imageWatcher = new FileSystemWatcher
                {
                     Path = _source_image_path,
                     NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName,
                     EnableRaisingEvents = true,
                     Filter = "*.*",
                     IncludeSubdirectories = true,
                };
                _imageWatcher.Created += new FileSystemEventHandler(image_created);
                isGiveLogRunning = true;
            }
            else
            {
                Debug.WriteLine("Stop Give Image");
                btn_Image.Text = "GIVE IMAGE";
                isGiveLogRunning = false;
            }
        }

        private void image_created(object sender, FileSystemEventArgs e)
        {
            string _source_path = e.FullPath;
            Debug.WriteLine(_source_path);
        }
    }

    public class MyTraceListener : TraceListener
    {
        private RichTextBox output;

        public MyTraceListener(RichTextBox output)
        {
            this.Name = "Trace";
            this.output = output;
        }


        public override void Write(string message)
        {

            Action append = delegate () {
                if (output.Lines.Count() >= 500)
                {
                    List<string> lines = output.Lines.ToList();
                    lines.RemoveRange(1, 200);
                    output.Lines = lines.ToArray();
                }
                output.AppendText(string.Format("[{0}] ", DateTime.Now.ToString()));
                output.AppendText(message);
                output.ScrollToCaret();
            };
            if (output.InvokeRequired)
            {
                output.BeginInvoke(append);
            }
            else
            {
                append();
            }

        }

        public override void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }
    }
}
