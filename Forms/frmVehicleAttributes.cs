using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Telerik.WinControls.UI;
using Taxi_BLL;
using Taxi_Model;
using DAL;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using System.IO;
using Microsoft.Reporting.WinForms;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    
    public partial class frmVehicleAttributes : UI.SetupBase
    {
        public int CurrentVehicleId { get; set; }
        public string CurrentAttributeValues { get; set; }
        public string OldAttributeValues { get; set; }
        VehicleTypeBO objMaster;
        public frmVehicleAttributes()
        {
            InitializeComponent();
            objMaster = new VehicleTypeBO();
            FormatGrid();
            this.Load += new EventHandler(frmVehicleAttributes_Load);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.Shown += new EventHandler(frmVehicleAttributes_Shown);
        }

        void frmVehicleAttributes_Shown(object sender, EventArgs e)
        {



           


                
                if (!string.IsNullOrEmpty(OldAttributeValues))
                {
                    string[] values = OldAttributeValues.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < values.Length; i++)
                    {
                        // values[i] = values[i].Trim();
                        int Id = values[i].Trim().ToInt();
                        for (int j = 0; j < grdLister.Rows.Count; j++)
                            if (grdLister.Rows[j].Cells[COLS.Id].Value.ToInt() == Id)
                            {
                                grdLister.Rows[j].Cells["Check"].Value = true;
                            }
                    }
                }
           
           
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }
        public struct COLS
        {
            public static string Id = "Id";
            public static string VehicleType = "VehicleType";
            public static string AttributeValues = "AttributeValues";
        }
        private void FormatGrid()
        {
                GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
                col.Width = 60;
                col.AutoSizeMode = BestFitColumnMode.None;
                col.HeaderText = "";
                col.Name = "Check";            
                grdLister.Columns.Add(col);
                GridViewTextBoxColumn tcol = new GridViewTextBoxColumn();
                tcol.Name = COLS.Id;
                tcol.IsVisible = false;
                grdLister.Columns.Add(tcol);
                tcol = new GridViewTextBoxColumn();
                tcol.Name = COLS.VehicleType;
                tcol.HeaderText = "Vehicle Type"; ;
                tcol.Width = 140;
                tcol.ReadOnly = true;
                grdLister.Columns.Add(tcol);
              
        }

        void frmVehicleAttributes_Load(object sender, EventArgs e)
        {
            PopulateData();
        }
        public override void PopulateData()
        {
            try
            {
                var list = (from a in General.GetQueryable<Fleet_VehicleType>(null)
                            select new
                            {
                                Id = a.Id,
                                VehicleType = a.VehicleType,
                                AttributeValues = a.AttributeValues
                            }).ToList();
                grdLister.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    grdLister.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                    grdLister.Rows[i].Cells[COLS.VehicleType].Value = list[i].VehicleType;

                    grdLister.Rows[i].Cells["Check"].Value = null;
                }
               
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public override void Save()
        {
            try
            {
                if (grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool()).Count() > 0)
                {
                    objMaster.GetByPrimaryKey(CurrentVehicleId);
                    objMaster.Edit();
                    CurrentAttributeValues = string.Join(",", grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true).Select(c => c.Cells[COLS.Id].Value.ToStr()).ToArray<string>());


                    if (CurrentAttributeValues.ToStr().Trim().Length > 0 && CurrentAttributeValues.StartsWith(",") == false && CurrentAttributeValues.EndsWith(",") == false)
                        CurrentAttributeValues = "," + CurrentAttributeValues + ",";


                    objMaster.Current.AttributeValues = CurrentAttributeValues;
                    objMaster.Save();
                    this.Close();

                    // objMaster.Clear();
                }
                else
                {
                    ENUtils.ShowMessage("Required atleast one Vehicle Type");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void chkAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SelectAll(args.ToggleState);
        }


        private void SelectAll(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                for (int i = 0; i < grdLister.Rows.Count; i++)
                {
                    grdLister.Rows[i].Cells["Check"].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < grdLister.Rows.Count; i++)
                {
                    grdLister.Rows[i].Cells["Check"].Value = false;
                }
            }

        }
    }
}
