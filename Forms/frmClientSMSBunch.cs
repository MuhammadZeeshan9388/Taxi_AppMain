using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using Telerik.WinControls.UI;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Taxi_AppMain.Classes;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using System.Collections;
using Microsoft.ReportingServices.ReportRendering;

namespace Taxi_AppMain
{
    public partial class frmClientSMSBunch : UI.SetupBase
    {
        // var list = null;
        // CompanyCostCenterBO objMaster = null;

        //SMSBunchesBO objSMSBunches;
      //  bool IsSMSTemplate = false;


        List<Customer> list;
        public frmClientSMSBunch(string MessageTemplate)
        {

            InitializeComponent();
            //  objSMSBunches = new SMSBunchesBO();
            // this.SetProperties((INavigation)objSMSBunches);

            FormatBunchGrid();
            txtMessage.Text = MessageTemplate;
            grdCustomerBunch.CommandCellClick += new CommandCellClickEventHandler(grdCustomerBunch_CommandCellClick);
            grdCustomerBunch.CellDoubleClick += new GridViewCellEventHandler(grdCustomerBunch_CellDoubleClick);
        }



        void grdCustomerBunch_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {

                string MessageTemplate = txtMessage.Text.Trim();
                if (string.IsNullOrEmpty(MessageTemplate))
                {
                    ENUtils.ShowMessage("Required : Message Template");
                    return;
                }

                if (grdCustomerBunch.CurrentRow != null)
                {
                    int MessageTemplateId = 0;

                    int BunchNo = grdCustomerBunch.CurrentRow.Cells["BunchNo"].Value.ToInt();
                    string BunchName = grdCustomerBunch.CurrentRow.Cells["BunchName"].Value.ToStr();

                    int SkipCustomer = (BunchNo * spnBunchValue.Value.ToInt());

                    var list2 = (list.Skip(SkipCustomer).Take(spnBunchValue.Value.ToInt())).ToList();

                    int TotalNo = list2.Count;

                    string MobileNo = string.Empty;

                    int PickUpTotal = 0;
                    foreach (var item in list2)
                    {
                        MobileNo += item.MobileNo + ",";
                        PickUpTotal++;
                    }

                    SMSAllForm(MessageTemplateId, MessageTemplate, MobileNo, BunchName, TotalNo, PickUpTotal, spnBunchValue.Value.ToInt());
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void ShowPickCustomerBunch(int bunch, int BunchValues)
        {
            try
            {
                int SkipCustomer = (bunch * BunchValues);

                var list2 = (list.Skip(SkipCustomer).Take(spnBunchValue.Value.ToInt())).ToList().Distinct();
                var list3 = (from a in list2
                             select new
                             {
                                 Name = a.Name,
                                 MobileNo = a.MobileNo
                             }).ToList();
                List<object[]> obj =General.ShowCustomerBunch(list3, "Id");
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


       

        void grdCustomerBunch_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                if (grdCustomerBunch.CurrentRow != null)
                {
                    int BunchNo = grdCustomerBunch.CurrentRow.Cells["BunchNo"].Value.ToInt();

                    ShowPickCustomerBunch(BunchNo, spnBunchValue.Value.ToInt());
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void frmClientSMSBunch_Load(object sender, EventArgs e)
        {
            SMSCustomerBunch(spnBunchValue.Value);
            GridButton();
           // grdCustomerBunch.CurrentRow.Cells["BunchName"].IsSelected = true;
           
           // IsSMSTemplate = true;
        }
        public struct COLS
        {
            public static string BunchName = "BunchName";
            public static string BunchNo = "BunchNo";
        }
        public void FormatBunchGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.BunchName;
            col.HeaderText = "Bunch Name";
            col.Width = 200;
            col.ReadOnly = true;
            grdCustomerBunch.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.BunchNo;
            col.IsVisible = false;
            grdCustomerBunch.Columns.Add(col);
        }
        private void GridButton()
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 135;
            col.Name = "btnShowCustomer";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Show Customer";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdCustomerBunch.Columns.Add(col);
        }
        //private void CreateCheckBoxColumn()
        //{
        //    GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
        //    col.Width = 30;
        //    col.Name = "COLCheckBox";
        //    col.FieldName = "COLCheckBox";
        //    grdCustomerBunch.Columns.Add(col);
        //}
        private void SMSCustomerBunch(decimal BunchValue)
        {
            try
            {
                list = (General.GetQueryable<Customer>(c => c.MobileNo != null && c.MobileNo.Length > 10).OrderBy(a => a.Name).ToList());

                int cnt = list.Count / BunchValue.ToInt();

                grdCustomerBunch.RowCount = cnt;
                for (int i = 0; i < cnt; i++)
                {
                    grdCustomerBunch.Rows[i].Cells["BunchName"].Value = "Bunch  " + (i + 1);
                    grdCustomerBunch.Rows[i].Cells["BunchNo"].Value = i;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        public void SelectAll(ToggleState toggle)
        {
            try
            {
                bool SelectAll = toggle == ToggleState.On;

                grdCustomerBunch.Rows.ToList().ForEach(c => c.Cells["COLCheckBox"].Value = SelectAll);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            try
            {
                string MessageTemplate = txtMessage.Text.Trim();
                if (string.IsNullOrEmpty(MessageTemplate))
                {
                    ENUtils.ShowMessage("Required : Message Template");
                    return;
                }

                if (grdCustomerBunch.CurrentRow != null)
                {
                    int MessageTemplateId = 0;
                   
                    int BunchNo = grdCustomerBunch.CurrentRow.Cells["BunchNo"].Value.ToInt();
                    string BunchName = grdCustomerBunch.CurrentRow.Cells["BunchName"].Value.ToStr();

                    int SkipCustomer = (BunchNo * spnBunchValue.Value.ToInt());

                    var list2 = (list.Skip(SkipCustomer).Take(spnBunchValue.Value.ToInt())).ToList();

                    int TotalNo = list2.Count;




                    string MobileNo = string.Empty;

                     int PickUpTotal = 0;
                    foreach (var item in list2)
                    {
                        MobileNo += item.MobileNo + ",";
                         PickUpTotal++;
                    }
                    SMSAllForm(MessageTemplateId, MessageTemplate, MobileNo, BunchName, TotalNo, PickUpTotal, spnBunchValue.Value.ToInt());
                    
                }
                else
                {
                    ENUtils.ShowMessage("Required : Bunch Name");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        
        public void SMSAllForm(int TemplateID, string MessageTemplate, string MobileNo, string BunchName, int TotalNo, int PickUpTotal, int BunchValues)
        {
            try
            {
                frmSMSAll frm = (frmSMSAll)Application.OpenForms.Cast<Form>().FirstOrDefault(c => c.Name == "frmSMSAll");

                if (frm != null)
                {
                    frm.SMSTo(TemplateID, MessageTemplate, MobileNo, BunchName, TotalNo, PickUpTotal, BunchValues);
                }

                this.Close();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        //public override void Save()
        //{
        //    try
        //    {
        //        if (objSMSBunches.PrimaryKeyValue == null)
        //        {
        //            objSMSBunches.New();
        //        }
        //        else
        //        {
        //            objSMSBunches.Edit();
        //        }

        //        objSMSBunches.Current.MessageTemplate = txtMessage.Text.ToStr().Trim();

        //        objSMSBunches.Current.MessageValue = spnBunchValue.Value.ToInt();

        //        objSMSBunches.Save();

        //    }
        //    catch (Exception ex)
        //    {
        //        if (objSMSBunches.Errors.Count > 0)
        //            ENUtils.ShowMessage(objSMSBunches.ShowErrors());
        //        else
        //        {
        //            ENUtils.ShowMessage(ex.Message);
        //        }
        //    }
        //}




        private void btnRecreateBunch_Click(object sender, EventArgs e)
        {
            try
            {
                SMSCustomerBunch(spnBunchValue.Value);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void frmClientSMSBunch_Shown(object sender, EventArgs e)
        {
           // grdCustomerBunch.Rows[0].IsSelected = true;
            if (grdCustomerBunch.Rows.Count > 0)
            {
                grdCustomerBunch.Rows[0].IsCurrent = true;
            }
        }
    }
}
