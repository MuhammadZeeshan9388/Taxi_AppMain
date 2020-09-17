using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Microsoft.Reporting.WinForms;
using System.Net.Mail;
using System.IO;
using Utils;
using Telerik.WinControls.UI;
using Taxi_Model;
using System.Linq;
using System.Drawing.Imaging;
using System.Net.NetworkInformation;
using System.Net.Configuration;
using System.Net;


namespace Taxi_AppMain
{
    public partial class frmClientEmail : UI.SetupBase
    {
        string filepath = string.Empty;
        List<Attachment> Attach = new List<Attachment>();
        List<string> attachment = new List<string>();
        string[] files = new string[10];
        public frmClientEmail()
        {
            InitializeComponent();
        }



        private void btnUpload_Click(object sender, EventArgs e)
        {

            try
            {

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Multiselect = true;


                if (lbxFiles.Items.Count == 5)
                {
                    ENUtils.ShowMessage("Cannot Attach more than 5 files");
                    return;

                }


                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //  lblFiles.Text = dlg.FileName.ToString();
                    //filepath = dlg.FileName.ToStr();



                    if (File.Exists(dlg.FileName))
                    {
                        Int64 fileSizeInBytes = new FileInfo(dlg.FileName).Length;
                        var kbs = fileSizeInBytes / 1024;


                        if (fileSizeInBytes < 1024 && fileSizeInBytes > 0)
                            kbs = 1;



                        var mb = kbs / 1024;

                        if (mb > 2)
                        {
                            ENUtils.ShowMessage("Cannot Attach file more than 2 MB");
                            return;

                        }

                        RadListDataItem item = new RadListDataItem();

                        item.Text = dlg.SafeFileName + " (" + kbs + " kb)";
                        item.Value = dlg.FileName;

                        lbxFiles.Items.Add(item);

                    }


                }
            }
            catch (Exception ex)
            {


            }
        }

      
        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            try

            {


                if (AppVars.objPolicyConfiguration.UserName.ToStr().Trim() == string.Empty || AppVars.objPolicyConfiguration.SmtpHost.ToStr().Trim() == string.Empty)
                {

                    ENUtils.ShowMessage("Email Configurations is not defined in settings.");
                    return;

                }
                

                if (txtSubject.Text.Trim() != "" && txtMessage.Text.Trim() != "")
                {
                    for (int i = 0; i < lbxFiles.Items.Count; i++)
                    {
                        Attach.Add(new Attachment(lbxFiles.Items[i].Value.ToString()));
                      
                    }



                    string body =  txtMessage.Text;

                    body += "<br><br><b>" + "Note : This email is posted from Despatch System from " + AppVars.objSubCompany.CompanyName.ToStr() + "<b>";

                                   
                    body = body.Replace("\n", "<br>");

               //     body += "</body></html>";

                    Taxi_AppMain.Email.Send(txtSubject.Text, body, AppVars.objPolicyConfiguration.UserName.ToStr().Trim(), "support@eurosofttech.co.uk,danish85_dj@hotmail.com,imran@eurosofttech.co.uk,cases@eurosofttech.co.uk", Attach);
                    //lblMsg.Text = "Email sent successfully.";
                    //this.Close();
                    RadDesktopAlert alert = new RadDesktopAlert();
                    alert.CaptionText = "Email sent successfully";

                    alert.ContentText = txtMessage.Text;
                    alert.ContentImage = Resources.Resource1.email;
                    alert.Show();
                    this.Close();
                     
                }
                else
                {
                   
                    lblMsg.Text = "Please Fill All the Fields...";
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            lbxFiles.Items.Remove(lbxFiles.SelectedItem);
        }


    }
}
