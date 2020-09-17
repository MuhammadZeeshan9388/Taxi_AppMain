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
    public partial class frmPickClientSMSBunch : UI.SetupBase
    {
        
        bool IsSMSTemplate = false;
        bool IsCurrentRow = false;
        int BunchValue = 0;
        
        List<Customer> list;
        public frmPickClientSMSBunch()
        {

            InitializeComponent();
           
            FormatBunchGrid();
            grdCustomerBunch.CommandCellClick += new CommandCellClickEventHandler(grdCustomerBunch_CommandCellClick);
            grdCustomerBunch.CellDoubleClick += new GridViewCellEventHandler(grdCustomerBunch_CellDoubleClick);
            ddlMessageTemplate.SelectedValueChanged += new EventHandler(ddlMessageTemplate_SelectedValueChanged);
         
           
        }

        void ddlMessageTemplate_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsSMSTemplate == false)
                    return;
                IsCurrentRow = false;
                int MessageTemplateId = ddlMessageTemplate.SelectedValue.ToInt();

                var listSMSBunchDetail = (from a in General.GetQueryable<SMSBunches_Detail>(c => c.BunchId == MessageTemplateId)
                            select new
                            {
                                BunchNo = a.BunchNo,
                                BunchValue = a.SMSBunch.MessageValue
                            }).ToList();
               
              //  ConditionalFormattingObject obj = null;
                if (listSMSBunchDetail.Count > 0)
                {
                    for (int i = 0; i < this.grdCustomerBunch.Rows.Count; i++)
                    {
                        this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.CustomizeFill = true;
                        this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.CustomizeBorder = true;
                        this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.BackColor = Color.Transparent;
                        this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.BorderGradientStyle = GradientStyles.Solid;
                        this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                    }

                    foreach (var item in listSMSBunchDetail)
                    {
                        //obj = new ConditionalFormattingObject(item.BunchNo, ConditionTypes.Greater, "30", "", true);
                        //obj.CellForeColor = Color.Black;
                        //obj.RowBackColor = Color.White;
                        //this.grdCustomerBunch.Columns["BunchName"].ConditionalFormattingObjectList.Add(obj);
                        BunchValue = item.BunchValue.ToInt();
                        break;
                    }
                
                    SMSCustomerBunch(BunchValue);
                    

                    for (int i = 0; i < this.grdCustomerBunch.Rows.Count; i++)
                    {
                        foreach (var item in listSMSBunchDetail)
                        {
                            if (grdCustomerBunch.Rows[i].Cells["BunchName"].Value.ToStr() == item.BunchNo.ToStr())
                            {
                                this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.CustomizeFill = true;
                                this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.CustomizeBorder = true;
                                this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.BackColor = Color.LightGreen;
                                this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.BorderGradientStyle = GradientStyles.Solid;
                                this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                                
                            }
                            //else if( IsCurrentRow==false)
                            //{
                            //    grdCustomerBunch.Rows[i+1].IsCurrent = true;
                            //    IsCurrentRow = true;
                            //}
                            
                        }
                     
                    }
                }
                else
                {
                    for (int i = 0; i < this.grdCustomerBunch.Rows.Count; i++)
                    {
                            this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.CustomizeFill = true;
                            this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.CustomizeBorder = true;
                            this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.BackColor = Color.Transparent;
                            this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.BorderGradientStyle = GradientStyles.Solid;
                            this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                    }
                    grdCustomerBunch.Rows[0].IsCurrent = true;
                }
                
                for (int i = 0; i <this.grdCustomerBunch.Rows.Count; i++)
                {
                    if (this.grdCustomerBunch.Rows[i].Cells["BunchName"].Style.BackColor == Color.Transparent == true)
                    {
                        grdCustomerBunch.Rows[i].IsCurrent = true;
                        IsCurrentRow = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        void grdCustomerBunch_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                int MessageTemplateId = ddlMessageTemplate.SelectedValue.ToInt();
                if (MessageTemplateId == 0)
                {
                    ENUtils.ShowMessage("Required : Message Template");
                    return;
                }

                if (grdCustomerBunch.CurrentRow != null)
                {
                    string MessageTemplate = ddlMessageTemplate.SelectedItem.ToStr();
                    int BunchNo = grdCustomerBunch.CurrentRow.Cells["BunchNo"].Value.ToInt();
                    string BunchName = grdCustomerBunch.CurrentRow.Cells["BunchName"].Value.ToStr();

                    int SkipCustomer = (BunchNo * BunchValue);

                    var list2 = (list.Skip(SkipCustomer).Take(BunchValue)).ToList();

                    int TotalNo = list2.Count;

                    string MobileNo = string.Empty;

                    int PickUpTotal = 0;
                    foreach (var item in list2)
                    {
                        MobileNo += item.MobileNo + ",";
                        PickUpTotal++;
                    }
                    SMSAllForm(MessageTemplateId, MessageTemplate, MobileNo, BunchName, TotalNo, PickUpTotal, BunchValue);
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void ShowCustomerBunch(int bunch,int BunchValues)
        {
            try
            {
                int SkipCustomer = (bunch * BunchValues);

                var list2 = (list.Skip(SkipCustomer).Take(BunchValues)).ToList().Distinct();
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
                    int MessageTemplateId = ddlMessageTemplate.SelectedValue.ToInt();
                    if (MessageTemplateId == 0)
                    {
                        ENUtils.ShowMessage("Required : Message Template");
                        return;
                    }
                    int BunchNo = grdCustomerBunch.CurrentRow.Cells["BunchNo"].Value.ToInt();
                    ShowCustomerBunch(BunchNo, BunchValue);
                   // SMSCustomerBunch(BunchValue);
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void frmClientSMSBunch_Load(object sender, EventArgs e)
        {
            SMSCustomerBunch(200);
            GridButton();

            FillCombo();
            IsSMSTemplate = true;
            //grdCustomerBunch.CurrentRow.Cells["BunchName"].IsSelected = true;
        }
        public void FillCombo()
        {
            ComboFunctions.FillSMSTemplate(ddlMessageTemplate);
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
        private void SMSCustomerBunch(int BunchValue)
        {
            try
            {
                list = (General.GetQueryable<Customer>(c => c.MobileNo != null && c.MobileNo.Length > 10).OrderBy(a => a.Name).ToList());

                int cnt = list.Count / BunchValue;

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
                int MessageTemplateId = ddlMessageTemplate.SelectedValue.ToInt();
 
                if (MessageTemplateId==0)
                {
                    ENUtils.ShowMessage("Required : Message Template");
                    return;
                }

                if (grdCustomerBunch.CurrentRow != null)
                {
                    string MessageTemplate = ddlMessageTemplate.SelectedItem.ToStr();
                    int BunchNo = grdCustomerBunch.CurrentRow.Cells["BunchNo"].Value.ToInt();
                    string BunchName = grdCustomerBunch.CurrentRow.Cells["BunchName"].Value.ToStr();

                    int SkipCustomer = (BunchNo * 200);

                    var list2 = (list.Skip(SkipCustomer).Take(200)).ToList();

                    int TotalNo = list2.Count;

                    string MobileNo = string.Empty;

                     int PickUpTotal = 0;
                    foreach (var item in list2)
                    {
                        MobileNo += item.MobileNo + ",";
                         PickUpTotal++;
                    }
                    SMSAllForm(MessageTemplateId, MessageTemplate, MobileNo, BunchName, TotalNo, PickUpTotal, BunchValue);
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
        
        public void SMSAllForm(int TemplateID, string MessageTemplate, string MobileNo, string BunchName, int TotalNo, int PickUpTotal,int BunchValue)
        {
            try
            {
                frmSMSAll frm = (frmSMSAll)Application.OpenForms.Cast<Form>().FirstOrDefault(c => c.Name == "frmSMSAll");

                if (frm != null)
                {
                    frm.SMSTo(TemplateID, MessageTemplate, MobileNo, BunchName, TotalNo, PickUpTotal, BunchValue);
                }

                this.Close();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
    }
}

               // SMSCustomerBunch(spnBunchValue.Value);
                //if (grdCustomerBunch.CurrentRow != null)
                //{
                //    // ListofData.Clear();
                //    //int MessageTemplateId = ddlSMSTemplate.SelectedValue.ToInt();
                //    //string MessageTemplate = ddlSMSTemplate.SelectedItem.ToStr();
                //    int BunchNo = grdCustomerBunch.CurrentRow.Cells["BunchNo"].Value.ToInt();
                //    string BunchName = grdCustomerBunch.CurrentRow.Cells["BunchName"].Value.ToStr();

                //    int SkipCustomer = (BunchNo * spnBunchValue.Value.ToInt());

                //    var list2 = (list.Skip(SkipCustomer).Take(spnBunchValue.Value.ToInt())).ToList();

                //    int TotalNo = list2.Count;




                //    string MobileNo = string.Empty;

                //    int PickUpTotal = 0;
                //    foreach (var item in list2)
                //    {
                //        MobileNo += item.MobileNo + ",";
                //        PickUpTotal++;
                //    }
                    //SMSAllForm(MessageTemplateId, MessageTemplate, MobileNo, BunchName, TotalNo, PickUpTotal);
                    //frmSMSAll frm = (frmSMSAll)Application.OpenForms.Cast<Form>().FirstOrDefault(c => c.Name == "frmSMSAll");

                    //// frmSMSAll(MobileNo,"",4);
                    //if (frm != null)
                    //{
                    //    frm.SMSTo(MessageTemplateId, MobileNo, BunchName, TotalNo, PickUpTotal);

                    //}
               // }
         
 
