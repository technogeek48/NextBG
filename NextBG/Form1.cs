using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Shell32;

namespace NextBG
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public static Type typeActiveDesktop = Type.GetTypeFromCLSID(new Guid("{75048700-EF1F-11D0-9888-006097DEACF9}"));
        public IActiveDesktop ad = (IActiveDesktop)Activator.CreateInstance(typeActiveDesktop);

        public String folderToSearch { get; set; }
        public String[] imageList { get; set; }
        public int currentBG = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Stop();
        }

        public void nextBG()
        {

            if (currentBG <= imageList.Length)
            {
                string strFile = imageList[currentBG];

                int dwReserved = 0;
                ad.SetWallpaper(strFile, dwReserved);

                try
                {
                    ad.ApplyChanges(AD_APPLY.ALL | AD_APPLY.FORCE | AD_APPLY.BUFFERED_REFRESH);
                    currentBG++;
                }
                catch (Exception e)
                {
                    
                    Console.Out.WriteLine(e.ToString());
                }
                
            }
            else
            {
                currentBG = 0;
                imageList.Reverse();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Interval = (int)nudInterval.Value;
        }

        private void nudInterval_ValueChanged(object sender, EventArgs e)
        {
            //nothing
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            nextBG();
        }

        private void btnTEST_Click(object sender, EventArgs e)
        {
            string strFile = "C:\\Users\\M.Sakellaropoulos\\LOCAL_FOLDER\\BBB_bgSKY_BLUE.png";
            int dwReserved = 0;
            ad.SetWallpaper(strFile, dwReserved);
            ad.ApplyChanges(AD_APPLY.ALL | AD_APPLY.FORCE | AD_APPLY.BUFFERED_REFRESH);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Choose folder for image search.

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowDialog();

            if (dlg.SelectedPath != null)
            {
                this.folderToSearch = dlg.SelectedPath;
            }

            this.imageList = Directory.GetFiles(this.folderToSearch);

        }

        
    }
}
