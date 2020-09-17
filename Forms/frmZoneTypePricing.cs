using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Utils;
using Telerik.WinControls.UI;
using Taxi_AppMain.Classes;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmZoneTypePricing : UI.SetupBase
    {
        bool IsGridButton = false;
        Gen_ZonesType_PricingBO objGen_ZonesType_PricingBO; 
        public frmZoneTypePricing()
        {
            InitializeComponent();
            objGen_ZonesType_PricingBO = new Gen_ZonesType_PricingBO();
            this.SetProperties((INavigation)objGen_ZonesType_PricingBO);
            this.Load += new EventHandler(frmZoneTypePricing_Load);
            FillCombo();
            this.grdZone.ShowGroupPanel = false;
            this.grdZone.AllowAddNewRow = false;
            //this.grdZone.AllowDeleteRow = false;
           // this.grdZone.ShowColumnHeaders = false;
            
            this.grdZone.CellDoubleClick += new GridViewCellEventHandler(grdZone_CellDoubleClick);
            this.grdZone.CellFormatting += new CellFormattingEventHandler(grdZone_CellFormatting);
            grdZone.CommandCellClick += new CommandCellClickEventHandler(grdZone_CommandCellClick);
        }

        void grdZone_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name == "Edit")
                {
                    if (grdZone.CurrentRow != null && grdZone.CurrentRow is GridViewDataRowInfo)
                    {
                        int Id = grdZone.CurrentRow.Cells["Id"].Value.ToInt();
                        OnDisplayRecord(Id);
                    }
                    else
                    {
                        ENUtils.ShowMessage("Please select a record");
                    }
                }
                else if (gridCell.ColumnInfo.Name == "Delete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete this Plot? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        try
                        {
                            objGen_ZonesType_PricingBO.GetByPrimaryKey(grdZone.CurrentRow.Cells["Id"].Value.ToInt());
                            objGen_ZonesType_PricingBO.Delete(objGen_ZonesType_PricingBO.Current);
                            // ShowProgress(true);

                            if (grdZone.Rows.Count == 0)
                            {
                                IsGridButton = false;
                            }
                            RefreshData();

                        }
                        catch (Exception ex)
                        {
                            if (objGen_ZonesType_PricingBO.Errors.Count > 0)
                            {
                                ENUtils.ShowMessage(objGen_ZonesType_PricingBO.ShowErrors());
                            }
                            else
                            {
                                ENUtils.ShowMessage(ex.Message);
                            }


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void grdZone_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                if (e.CellElement is GridDataCellElement)
                {
                    if (e.Column is GridViewCommandColumn)
                    {

                        if (e.Column.Name == "Delete")
                        {
                            ((RadButtonElement)e.CellElement.Children[0]).Image = Resources.Resource1.delete;
                        }
                        else if (e.Column.Name == "Edit")
                        {
                            ((RadButtonElement)e.CellElement.Children[0]).Image =Resources.Resource1.edit2;
                        }
                    }
                }
            }
        
              catch
              {
              }
        }

        void grdZone_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (grdZone.CurrentRow != null && grdZone.CurrentRow is GridViewRowInfo)
                {
                    int Id = grdZone.CurrentRow.Cells["Id"].Value.ToInt();
                    objGen_ZonesType_PricingBO.GetByPrimaryKey(Id);
                    DisplayRecord();
                    //OnDisplayRecord(Id);
                   }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void frmZoneTypePricing_Load(object sender, EventArgs e)
        {
            PopulateData();
        }
        public void FillCombo()
        {
            ComboFunctions.FillZoneTypes(ddlFromZone);
            ComboFunctions.FillZoneTypes(ddlToZone);
        }

        private void btnAddPostCode_Click(object sender, EventArgs e)
        {
            Save();
        }
        public override void Save()
        {
            try
            {
                if (objGen_ZonesType_PricingBO.PrimaryKeyValue == null)
                {
                    objGen_ZonesType_PricingBO.New();

                }
                else
                {
                    objGen_ZonesType_PricingBO.Edit();
                }
                objGen_ZonesType_PricingBO.Current.FromZoneTypeId=ddlFromZone.SelectedValue.ToInt();
                objGen_ZonesType_PricingBO.Current.ToZoneTypeId=ddlToZone.SelectedValue.ToInt();
                objGen_ZonesType_PricingBO.Current.Price=spnPrice.Value.ToDecimal();
                objGen_ZonesType_PricingBO.Save();
                objGen_ZonesType_PricingBO.Clear();
                ClearFields();
                PopulateData();
            }
            catch(Exception ex)
            {
                if (objGen_ZonesType_PricingBO.Errors.Count > 0)
                    ENUtils.ShowMessage(objGen_ZonesType_PricingBO.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            ddlFromZone.Text = "";
            ddlToZone.Text = "";
            spnPrice.Value = 0;
            objGen_ZonesType_PricingBO.Clear();
            ddlFromZone.Focus();
        }
        public override void DisplayRecord()
        {
            if (objGen_ZonesType_PricingBO.Current == null) return;
            
            ddlFromZone.SelectedValue = objGen_ZonesType_PricingBO.Current.FromZoneTypeId.ToInt();
            ddlToZone.SelectedValue = objGen_ZonesType_PricingBO.Current.ToZoneTypeId.ToInt();
            spnPrice.Value = objGen_ZonesType_PricingBO.Current.Price.ToDecimal();
        }
        public override void PopulateData()
        {
            try
            {
                var list = (from a in General.GetQueryable<Gen_ZonesType_Pricing>(null)
                            select new
                            {
                                Id=a.Id,
                                FromPlot=a.Gen_ZoneType.ZoneType,
                                ToPlot=a.Gen_ZoneType1.ZoneType,
                                Price=a.Price
                            }).ToList();
                grdZone.DataSource = list;
                grdZone.Columns["Id"].IsVisible = false;
                grdZone.Columns["FromPlot"].Width = 180;
                grdZone.Columns["ToPlot"].Width = 180;
                grdZone.Columns["Price"].Width = 150;
                grdZone.Columns["FromPlot"].HeaderText = "From Plot";
                grdZone.Columns["ToPlot"].HeaderText = "To Plot";
                grdZone.Columns["Price"].HeaderText = "Price (£)";
                //£
                if (IsGridButton == false && grdZone.Rows.Count>0)
                {
                    GridButton();
                    IsGridButton = true;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public override void RefreshData()
        {
            try
            {
                PopulateData();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public void GridButton()
        {
            GridViewCommandColumn cmdEdit = new GridViewCommandColumn();
            cmdEdit.Name = "Edit";
           // cmdEdit.HeaderText = "Edit";
            cmdEdit.DefaultText = "Edit";
            cmdEdit.UseDefaultText = true;
            cmdEdit.TextImageRelation = TextImageRelation.ImageBeforeText;
            cmdEdit.TextAlignment = ContentAlignment.MiddleCenter;
            cmdEdit.Width = 80;
            grdZone.Columns.Add(cmdEdit);

            GridViewCommandColumn cmd = new GridViewCommandColumn();
            cmd.Name = "Delete";
            //cmd.HeaderText = "Delete";
            cmd.DefaultText = "Delete";
            cmd.UseDefaultText = true;
            cmd.TextImageRelation = TextImageRelation.ImageBeforeText;
            cmd.TextAlignment = ContentAlignment.MiddleCenter;
            cmd.Width = 80;
            grdZone.Columns.Add(cmd);
  
        }    
    }
}
