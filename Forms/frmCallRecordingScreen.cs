using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Utils;

namespace Taxi_AppMain
{
    public partial class frmCallRecordingScreen : Form
    {
        private long _ID;
        private string _Token;
        private string _PhoneNo;
        private string _fromDate;
        private string _tillDate;
        BackgroundWorker worker;
        string _filePath = string.Empty;

        public bool? IsFileDownloaded;
       
        public frmCallRecordingScreen(long bookingId, string token,string phoneNo, string fromDateTime,string tillDateTime)
        {
            InitializeConstructor();

            this._ID=bookingId;
            this._Token = token;
            this._PhoneNo = phoneNo;
            this._fromDate = fromDateTime;
            this._tillDate = tillDateTime;

            this._filePath = Application.StartupPath + "\\Recordings\\" + bookingId + "_" + phoneNo+".wav";

            this.Shown += new EventHandler(frmLoading_Shown);
            this.FormClosing += new FormClosingEventHandler(frmCallRecordingScreen_FormClosing);
        }

        void frmCallRecordingScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (worker != null)
                {
                    worker.Dispose();
                    worker = null;
                    pictureBox3.Dispose();


                }

            }
            catch
            {


            }
        }



        void frmLoading_Shown(object sender, EventArgs e)
        {
            InitializeComponent();

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);

            worker.RunWorkerAsync();
        }

       

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                if (e.Result != null && e.Result is bool)
                {

                    IsFileDownloaded = e.Result.ToBool();

                    Close();


                }
                else
                    Close();
            }
            catch
            {
                Close();

            }

        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            bool rtn = false;

            try
            {
                AsteriskCallRecording cls = new AsteriskCallRecording(AppVars.objPolicyConfiguration.CallRecordingToken.ToStr().Trim());
                e.Result= cls.DownloadFile(this._PhoneNo, this._fromDate, this._tillDate, this._filePath);

               // ClsAccStatment frm = new ClsAccStatment(this._Token);
              //   e.Result=  frm.downloadfile(

            }
            catch(Exception ex)
            {


                e.Result = false;
            }

         

        }


        private void InitializeConstructor()
        {
            //InitializeComponent();

        }

        private void frmLoading_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {

                if (worker != null)
                {
                    worker.CancelAsync();
                }

                IsFileDownloaded = null;
            }
            catch
            {


            }

            Close();
        }

       



    }
}
