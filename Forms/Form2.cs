using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Xml;

using Utils;


using System.Net;

namespace Taxi_AppMain.Forms
{
    public partial class Form2 : Form
    {

        private string msg = "";
     //   int COUNT = 100;
     //   MockIntegerDataSource dataSource;
        public Form2()
        {
            InitializeComponent();




            this.Load += new EventHandler(Form2_Load);

        }

     
        void Form2_Load(object sender, EventArgs e)
        {

          
          



           //using (TaxiDataContext db = new TaxiDataContext())
           //{

           //    Gen_SubCompany obj= db.Gen_SubCompanies.FirstOrDefault();


           //    obj.CompanyFooterLogo = arr;
           //    db.SubmitChanges();
           //}

           //Gen_SubCompany obj = new Gen_SubCompany();
           //obj.CompanyFooterLogo = arr;
           //obj.BlcNumber = "a";
            //for (int i = 0; i < 50; i++)
            //{
            //  GridViewRowInfo row=    radGridView1.Rows.AddNew();


            //  row.Cells["Column1"].Value = "aaaadasdasdas";
            //  row.Cells["Column2"].Value = "aaaadasdasdas";

            //  row.Cells["Column3"].Value = "aaaadasdasdas";

            //  row.Cells["Column4"].Value = "aaaadasdasdas";
            //  row.Cells["Column5"].Value = "aaaadasdasdas";
            //  row.Cells["Column6"].Value = "aaaadasdasdas";

            //  row.Cells["Column7"].Value = "aaaadasdasdas";

            //  row.Cells["Column8"].Value = "aaaadasdasdas";

            //  row.Cells["Column9"].Value = "aaaadasdasdas";



            //}

            //msg = "request pda=60261=114=JobId:60261:Pickup:SUDBURY HEIGHTS AVENUE GREENFORD UB6 0LY:Destination:HEATHROW TERMINAL 5, TW6 2GA:PickupDateTime:30/03/2015   22:07:Cust:DG:Mob:5443 :Fare:0.00:Vehicle:Saloon:Account: :Lug:0:Passengers:0:Journey:O/W:Payment:Cash:Special: :Extra:1:Via: :Did:114=1=157";

            //str = new StringBuilder();


            //timer1 = new System.Windows.Forms.Timer();
            //timer1.Tick += new EventHandler(timer1_Tick);
            //timer1.Interval = 5000;

            //bool success=false;
         

            // new Thread(delegate()
            //        {


            //            for (int i = 0; i < 50000; i++)
            //            {



            //                DateTime? RequestTime = DateTime.Now;
            //                success = SendPDAMessage(msg);
            //                DateTime? ResponseTime = DateTime.Now;


            //                if (ResponseTime.Value.Subtract(RequestTime.Value).Seconds >= 5)
            //                {

            //                    str.Append(string.Format("Request : {0:dd/MM/yyyy hh:mm:ss}", RequestTime) + Environment.NewLine);

            //                    str.Append(string.Format("Response : {0:dd/MM/yyyy hh:mm:ss}", ResponseTime) + Environment.NewLine);



            //                    if (this.InvokeRequired)
            //                    {
            //                        this.BeginInvoke(new ShowData(ShowView));

            //                    }
            //                    else
            //                    {

            //                        ShowView();
            //                    }


            //                }
            //            }

            //        }).Start();
              
                    

            

            
        }


     

     


       
    }
}
