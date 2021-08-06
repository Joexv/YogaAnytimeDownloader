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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using RestSharp;
using System.Management.Automation;

namespace YogaAnytimeDownloader
{
    public partial class Form1 : Form
    {
        string PAINFO = "";
        string JSESSIONID = "";
        public string FullCookie = "COUNTRYCODE=US;" +
            "CURRENCY=840;" +
            "PA_REFERRER=https%3A%2F%2Fwww%2Eyogaanytime%2Ecom%2Fclass%2Dview%2F3848%2Fvideo%2FYoga%2DSlow%2DFlow%2Dfor%2Dthe%2DHips%2Dby%2DAlana%2DMitnick;" +
            "TG=100;" +
            "PA_REF=none;" +
            "PA_LANDING=%2Fclass%2Dview%2F3848%2Fvideo%2FYoga%2DSlow%2DFlow%2Dfor%2Dthe%2DHips%2Dby%2DAlana%2DMitnick; " +
            "CURRENCY_TEST=840;" +
            "LANGUAGEID=1;" +
            "PAINFO={PAINFO};" +
            "JSESSIONID={JSESSIONID};" +
            "VIDEO_QUALITY=med;" +
            "PA_N=0;" +
            "LPM=mx;" +
            "IRM=false;" +
            "PRP=M;" +
            "MENU_VERSION=0";
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://www.yogaanytime.com/show-season/46/Yoga-Good-Morning-Yoga-Season-1");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://www.yogaanytime.com/class-view/175/video/Yoga-Welcome-to-Good-Morning-Yoga-by-Alana-Mitnick");
        }

        private void DL_Video_Click(object sender, EventArgs e)
        {
            DownloadVideos(false);
        }
        private void DL_Season_Click(object sender, EventArgs e)
        {
            DownloadVideos(true);
        }

        private CookieContainer GenCookies()
        {
            CookieContainer cookiecontainer = new CookieContainer();
            string[] cookies = FullCookie.Replace("{PAINFO}", PAINFO).Replace("{JSESSIONID}", JSESSIONID).Split(';');
            foreach (string cookie in cookies)
                cookiecontainer.SetCookies(new Uri("http://www.yogaanytime.com"), cookie);
            return cookiecontainer;
        }

        private void startDownload(string URL, string FileName)
        {
            Thread thread = new Thread(() => {
                BetterWebClient client = new BetterWebClient(GenCookies());
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                if (FileName.Contains(".ts"))
                    client.DownloadFileAsync(new Uri(URL), PadName(FileName));
                else
                    client.DownloadFileAsync(new Uri(URL), FileName);
            });
            thread.Start();
        }

        private string PadName(string FileName)
        {
            for (int i = 1; i < 9999; i++)
            {
                if (FileName.Contains($"p-{i}.ts"))
                    return FileName.Replace($"p-{i}.ts", $"p-" + i.ToString().PadLeft(4, '0') + ".ts");
            }
            return FileName;
        }

        private string CurlDL(string URL)
        {
            var client = new RestClient(URL);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("", "");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:90.0) Gecko/20100101 Firefox/90.0");
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            request.AddHeader("Accept-Language", "en-US,en;q=0.5");
            request.AddHeader("Referer", "https://www.yogaanytime.com/");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Cookie", FullCookie.Replace("{PAINFO}", PAINFO).Replace("{JSESSIONID}", JSESSIONID));
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            request.AddHeader("Sec-Fetch-Dest", "document");
            request.AddHeader("Sec-Fetch-Mode", "navigate");
            request.AddHeader("Sec-Fetch-Site", "same-origin");
            request.AddHeader("Sec-Fetch-User", "?1");
            request.AddHeader("Cache-Control", "max-age=0");
            IRestResponse response = client.Execute(request);
            return response.ToString();
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                DLabel.Text = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive;
                VideoPB.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                DLabel.Text = "Completed";
                VideoPB.Value = 0;
            });
        }

        string VideoFolder = "";
        private void DownloadVideos(bool isSeason = false)
        {
            DL_Video.Enabled = false;
            DL_Season.Enabled = false;
            List<string> URLs = new List<string>();
            if (isSeason)
            {
                Console.WriteLine("Parsing Season...");
                List<string> Season = HTML_Parser.FilterVideoURLs(HTML_Parser.GetHTML(textBox1.Text, GenCookies()));
                foreach(string url in Season)
                {
                    Console.WriteLine($"Pulling video from: {url}");
                    List<string> Found = HTML_Parser.GetVideoURLs(HTML_Parser.GetHTML(url, GenCookies()));
                    URLs.AddRange(Found);
                }
            }
            else
            {
                URLs = HTML_Parser.GetVideoURLs(HTML_Parser.GetHTML(textBox2.Text, GenCookies()));
            }
                
            foreach (string url in URLs)
            {
                Console.WriteLine("Currently downloading - " + url);
                Media DL = HTML_Parser.ConvertJS2JSON(HTML_Parser.GetHTML(url, GenCookies()));
                Console.WriteLine("Playlist URL - " + DL.playlist);

                Directory.CreateDirectory(Application.StartupPath + $"\\{DL.title}\\");

                using (BetterWebClient web1 = new BetterWebClient(GenCookies()))
                {
                    web1.DownloadFile(DL.playlist, Application.StartupPath + $"\\{DL.title}\\" + $"h264-{qualityPicker.Text}.m3u8");
                }

                foreach (string video in File.ReadAllLines(Application.StartupPath + $"\\{DL.title}\\" + $"h264-{qualityPicker.Text}.m3u8"))
                {
                    if(!video.Contains("#EXT"))
                        startDownload(DL.playlist.Replace($"h264-{qualityPicker.Text}.m3u8?subtitles=en", video), Application.StartupPath + $"\\{DL.title}\\" + video);
                }
                VideoFolder = Application.StartupPath + $"\\{DL.title}\\";
            }
            DL_Video.Enabled = true;
            DL_Season.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("FFMPEG.exe"))
            {
               // mergeVideo.Visible = false;
            }
        }

        private void MergeVideos(string Folder)
        {
            File.Delete($"{Folder}\\All.ts");
            File.Delete($"{Folder}\\mylist.txt");
            File.Delete($"{Folder}\\merged.mp4");
            //Create list of all TS files for FFMPEG
            // (for %i in (*.ts) do @echo file '%i') > mylist.txt

            //Merge the files
            // C:\Users\HPD\source\repos\YogaAnytimeDownloader\YogaAnytimeDownloader\bin\Debug\ffmpeg -f concat -i mylist.txt -c copy all.ts

            //Convert to MP4
            // C:\Users\HPD\source\repos\YogaAnytimeDownloader\YogaAnytimeDownloader\bin\Debug\ffmpeg -i all.ts -acodec copy -vcodec copy all.mp4


            string List_CMD = $"{Application.StartupPath}\\ffmpeg -f concat -i mylist.txt -c copy all.ts -y";
            string MP4_CMD = $"{Application.StartupPath}\\ffmpeg -i all.ts -acodec copy -vcodec copy Merged.mp4 -y";
            MessageBox.Show("Merging the video. The application will apear inactive in some cases. Please wait for it to finish.\n\nProcess will begin once you hit OK");
            Application.DoEvents();

            //If Powershell exists run it through there instead. Allows for computers running UNC file paths to be able to properly download 
            bool NotWorking = false;
            if (Directory.Exists(@"C:\Windows\System32\WindowsPowerShell") && !NotWorking)
            {
                string Full_Powershell = $"cd \"{Folder}\"; " +
                    $"Get-ChildItem(\"{Folder}\") | " +
                    "where {$_.extension -eq \".ts\"} | " +
                    "select -expandproperty name | " +
                    "foreach ($_) { \"file $_\"} | " +
                    "Set-Content mylist.txt; " +
                    $"& {List_CMD};" +
                    $"& {MP4_CMD}";
                Console.WriteLine(Full_Powershell);

                PowerShell ps = PowerShell.Create();
                ps.AddScript(Full_Powershell, true).Invoke();
                ps.Invoke();
            }
            else
            {
                string Full_CMD = $"/C cd \"{Folder}\" & (for %i in (*.ts) do @echo file '%i') > \"{Folder}\\mylist.txt\" & {List_CMD} & {MP4_CMD} & @echo Done & pause";
                Console.WriteLine(Full_CMD);
                ProcessStartInfo ProcessInfo;
                Process Process;
                ProcessInfo = new ProcessStartInfo("cmd.exe", Full_CMD);
                ProcessInfo.UseShellExecute = true;
                Process = Process.Start(ProcessInfo);
                Process.WaitForExit();
                Process.Dispose();
            }
            File.WriteAllText($"{Folder}\\mylist.txt", File.ReadAllText($"{Folder}\\mylist.txt").Replace("file all.ts", ""));

            DialogResult dialogResult = MessageBox.Show("Done! Would you like to remove the old unmerged TS files?", "Done", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                foreach (String file in Directory.GetFiles(Folder, "*.ts", SearchOption.AllDirectories))
                    File.Delete(file);
                foreach (String file in Directory.GetFiles(Folder, "*.m3u8", SearchOption.AllDirectories))
                    File.Delete(file);
                File.Delete("mylist.txt");
            }
                
        }

        private void fromFireFoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Cookies.GetCookie_FireFox("yogaanytime", "JSESSIONID", ref JSESSIONID) && Cookies.GetCookie_FireFox("yogaanytime", "PAINFO", ref PAINFO))
            {
                MessageBox.Show("Cookies obtained.");
                Console.WriteLine(JSESSIONID);
                Console.WriteLine(PAINFO);
            }
            else
            {
                MessageBox.Show("Failed. Make sure that you are signed into Yogaanytime. If it still wont work try Chrome.");
            }
        }

        private void fromChromeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Cookies.GetCookie_Chrome("yogaanytime", "JSESSIONID", ref JSESSIONID) && Cookies.GetCookie_Chrome("yogaanytime", "PAINFO", ref PAINFO))
            {
                MessageBox.Show("Cookies obtained.");
                Console.WriteLine(JSESSIONID);
                Console.WriteLine(PAINFO);
            }
            else
            {
                MessageBox.Show("Failed. Make sure that you are signed into Yogaanytime. If it still wont work try FireFox.");
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = false;
            folderDlg.Description = "Select the folder where your video is located";
            folderDlg.SelectedPath = Application.StartupPath;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
                MergeVideos(folderDlg.SelectedPath);

            folderDlg.Dispose();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string Help_MSG = "Step 1: Sign into YogaAnytime.com in FireFox or Google Chrome\n" +
                "Step 2: Click Get Auth Cookie - From *Browser*\n" +
                "Step 3: Insert a URL into the single video or Season textbox." +
                "Step 4: Hit Download\n\n" +
                "To convert the downloaded video into something more managable, click Convert Video then select the folder where your video is located. (Typically in this applications folder)\n\n" +
                "If the program is unable to get the cookies from Chrome or FireFox try clearing all Cookies from your browser, closing your browser, and then repeating the steps from Step 1.";
            MessageBox.Show(Help_MSG, "Help");
        }
    }
}
