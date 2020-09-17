using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Utils;
using Telerik.WinControls.UI;

namespace Taxi_AppMain
{
    public partial class frmManageKeys : Form
    {
        public frmManageKeys()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmManageKeys_Load);
            this.btnSave.Click+=new EventHandler(btnSave_Click);
            this.grdETAKeys.CommandCellClick += new Telerik.WinControls.UI.CommandCellClickEventHandler(grdETAKeys_CommandCellClick);
        }

        void grdETAKeys_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                if (grdETAKeys.Rows.Count > 0 && grdETAKeys.CurrentRow != null)
                {
                    GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                    if (gridCell.ColumnInfo.Name.ToLower() == "column2")
                    {
                        grdETAKeys.CurrentRow.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void frmManageKeys_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void Display()
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                     string APIKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='etakeys'").FirstOrDefault().ToStr().Trim();
                     if (APIKey.Contains("<etakeys>"))
                     {
                         string[] arr = new string[] { "<etakeys>" };
                         var list = APIKey.Split(arr, StringSplitOptions.None).ToList();
                         grdETAKeys.RowCount = list.Count;
                         for (int i = 0; i < grdETAKeys.Rows.Count; i++)
                         {
                             grdETAKeys.Rows[i].Cells["column1"].Value = list[i].ToStr();
                         }
                     }
                     else
                     {
                        var row= grdETAKeys.Rows.AddNew();
                        row.Cells["column1"].Value = APIKey;

                     }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (grdETAKeys.Rows.Count == 0)
                //{
                //    ENUtils.ShowMessage("Required : Keys ");
                //    return;
                //}
                string key = "etakeys";
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.ExecuteCommand("delete from mapkeys where maptype='"+key+"'");

                    int cnt = 10;
                    string ApiKey = string.Join("<etakeys>", grdETAKeys.Rows.Select(c => c.Cells["column1"].Value.ToStr().ToUpper()).ToArray<string>());

                    db.ExecuteCommand("insert into mapkeys (Id,MapType,APIKey) values (" + cnt + ",'"+key+"','"+ApiKey+"')");
                    //foreach (var item in grdETAKeys.Rows)
                    //{
                        //column1

                        //db.ExecuteCommand("insert into mapkeys values("+cnt+",'etakeys from mapkeys where maptype='etakeys'");

                        
                    //}



                    //string key = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='etakeys'").FirstOrDefault().ToStr().Trim();


                    //if (key.ToStr().Trim().Length > 0)
                    //{
                    //    foreach (var item in key.ToStr().Trim().Split(new string[] { "<etakey>" }, StringSplitOptions.RemoveEmptyEntries))
                    //    {
                    //        ETAKeys.Add(item);
                    //    }
                    //}
                }
                ENUtils.ShowMessage("Key Saved successfully");
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnGetKey_Click(object sender, EventArgs e)
        {
            try
            {
            frmAdminPwd frm = new frmAdminPwd("cabtreasure321!");
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frm.ReturnValue1.ToStr().ToLower().Trim() == "ok")
            {
                        WebService web = new WebService("http://favouritehatfield.co.uk/Service2.asmx");
                        web.AddParameter("keyType", "etakey");                    // Case Sensitive! To avoid typos, just copy the WebMethod's signature and paste it
                        web.Invoke("GetKeyForDispatch");
                        //var s = web.ResultString;
                        var keyvalue = web.EtaKey;

                      var row=  grdETAKeys.Rows.AddNew();

                      row.Cells["column1"].Value = keyvalue;
            }
            frm.Dispose();
            }
            catch
            {
            }
        }
    }
}
