using System;
using System.Runtime.Versioning;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Globalization;
using System.Web;

namespace TimeFromInternet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /*
        public DateTime GetTime()
        {
            var myhttpwebrequest=webre
        }
        */

        public bool CheckforInternetConenction()
        {
            try
            {
                using (var client=new WebClient())
                using (var stream=client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //var task = new List<Task>();
            
            ////var a = new CultureInfo("uz-UZ");            
            //DateTime a=new DateTime();
            //MessageBox.Show(TimeZone.CurrentTimeZone.GetUtcOffset(a).ToString());
            //MessageBox.Show(Thread.CurrentThread.CurrentUICulture.Name);          
            var client = new TcpClient("time.nist.gov", 13);
            using (var streamreader = new StreamReader(client.GetStream()))
            {
                DateTime a=new DateTime();
                var response = streamreader.ReadToEnd();
                MessageBox.Show(response);//Grinvich
                var utcDatetime = response.Substring(7, 17);//17-01-15 15:08:10
                MessageBox.Show(utcDatetime.ToString());
                //DateTime.ParseExact(TimeZone.CurrentTimeZone.GetUtcOffset(a).ToString())
                //DateTime.UtcNow.Add(TimeSpan.Parse("05:00:00"));
                //var localdate = DateTime.ParseExact(DateTime.UtcNow.Add(TimeSpan.Parse("05:00:00")).ToString(), "yy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                var localdate = DateTime.ParseExact(DateTime.UtcNow.Add(TimeSpan.Parse("00:00:00")).ToString(), "dd.MM.yyyy HH:mm:ss", CultureInfo.DefaultThreadCurrentCulture, DateTimeStyles.AssumeUniversal);
                textBox1.Text = localdate.ToString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckforInternetConenction()) button1.Enabled = true;
            else button1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.UtcNow.Add(TimeSpan.Parse("05:00:00")).ToString());//15.01.2017 20:10:58
        }
    }
}
