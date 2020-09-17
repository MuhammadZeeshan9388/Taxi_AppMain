using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using Taxi_AppMain;
using Taxi_BLL;
using Taxi_Model;
using Utils;
using System.Diagnostics;
using System.Linq;

namespace Taxi_AppMain
{
    public partial class frmLicenseKey : Form
    {


        public frmLicenseKey()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmLicenseKey_Load);
        }

       // private Gen_SysPolicy_LCompany  objLicCompany=null;

        void frmLicenseKey_Load(object sender, EventArgs e)
        {
         
            try
            {
                //objLicCompany = General.GetObject<Gen_SysPolicy_LCompany>(c => c.AddOn != null);


                //if (objLicCompany != null)


                //{
                //email>>emailcredentials
                txtContact2.Text = "Email : " + Program.objLic.OtherInformation1.ToStr().Split(new string[] { ">>" }, StringSplitOptions.None)[0];

                    //phonenumber>>websitename<<url
                    string phones = Program.objLic.OtherInformation2.ToStr().Trim().Split(new string[]{">>"},StringSplitOptions.None)[0];
                    string[] phoneArr = null;
                    if (phones.Contains(","))
                        phoneArr = phones.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
                    else
                        phoneArr = new string[1]{phones};



                    if(phoneArr.Length==1)
                     txtPhone1.Text ="Phone : "+ phoneArr[0].ToStr();

                    else if (phoneArr.Length > 1)
                    {
                        txtPhone1.Text ="Phone 1 : " + phoneArr[0].ToStr();
                        txtPhone2.Text ="Phone 2 : "+ phoneArr[1].ToStr();
                        txtPhone2.Visible=true;
                    }


                    

                    new Thread(SendExpiryEmail).Start();


                    string webDetails=  Program.objLic.OtherInformation2.ToStr().Trim().Split(new string[] { ">>" }, StringSplitOptions.None)[1];

                    txtWebiteLink.Text = webDetails.ToStr().Trim().Split(new string[] { "<<" }, StringSplitOptions.None)[0].ToStr();
                    txtWebiteLink.LinkClicked += new LinkLabelLinkClickedEventHandler(txtWebiteLink_LinkClicked);
                    LinkLabel.Link link = new LinkLabel.Link();
                    link.LinkData = webDetails.ToStr().Trim().Split(new string[] { "<<" }, StringSplitOptions.None)[1].ToStr();
                    txtWebiteLink.Links.Add(link);
           //     }

            }
            catch (Exception ex)
            {

                
            }        
        }

        delegate void UIDelegate();
        private void SendExpiryEmail()
        {
            try
            {

              

                if (AppVars.objPolicyConfiguration != null)
                {
                    Taxi_AppMain.Email.EmailLicenseExpiry(null, AppVars.objPolicyConfiguration.DefaultClientId,DateTime.Now.ToStr(),Program.objLic.ExpiryDateTime.ToStr(), "During Application Running");
                    btnOk.Invoke(new UIDelegate(VisibleOK));
                }
                else
                {
                    Gen_SysPolicy_Configuration objPolicy = General.GetQueryable<Gen_SysPolicy_Configuration>(null).FirstOrDefault();
                    if (objPolicy != null)
                        Taxi_AppMain.Email.EmailLicenseExpiry(null, objPolicy.DefaultClientId, DateTime.Now.ToStr(), Program.objLic.ExpiryDateTime.ToStr(), "Starting the Application");

                    btnOk.Invoke(new UIDelegate(VisibleOK));
                }
            }
            catch (Exception ex)
            {
                btnOk.Invoke(new UIDelegate(VisibleOK));
            }        
        }

        private void VisibleOK()
        {
            try
            {

                btnOk.Visible = true;
            }
            catch
            {


            }
        }

        void txtWebiteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {

                Process.Start(e.Link.LinkData as string);
            }
            catch
            {


            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        

    
    }
}
