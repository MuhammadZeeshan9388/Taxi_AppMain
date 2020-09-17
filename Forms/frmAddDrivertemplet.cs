using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using DAL;
using Taxi_Model;
using Telerik.WinControls.UI;
using Utils;
using System.Threading;
using System.Diagnostics;
using Telerik.WinControls;
using Telerik.WinControls.UI.Docking;
using System.Data.SqlClient;
using Taxi_AppMain.Classes;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using Telerik.WinControls.Enumerations;
using System.IO;
using System.Collections;
using Taxi_AppMain.Forms;
using UI;
using System.Web.UI;


namespace Taxi_AppMain
{
    public partial class frmAddDrivertemplet :UI.SetupBase
    {

        SysPolicyBO objMaster;

        public struct COL_SMSTEMPLET
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string Tempplet = "Tempplet";
        }

        public frmAddDrivertemplet()
        {
            InitializeComponent();
            InitializeConstructor();

        }
        private void InitializeConstructor()
        {

            objMaster = new SysPolicyBO();

            this.SetProperties((INavigation)objMaster);
        }
        private void frmDrivertemplet_Shown(object sender, EventArgs e)
        {
            TxtSMSTemplet.Select();
        }

        private void frmDrivertemplet_Load(object sender, EventArgs e)
        {
            try
            {
                
                
                FormatSMSTempletGrid();
                Gen_SysPolicy obj = General.GetQueryable<Gen_SysPolicy>(null).FirstOrDefault();
                if (obj != null)
                {
                    objMaster.GetByPrimaryKey(obj.Id);
                    Display();

                    if (grdSMSTemplets.Rows.Count > 0)
                    {
                        //grdSMSTemplets.Rows[0].IsCurrent = true;
                        //grdSMSTemplets.CurrentRow = null;
                    }
                }
                TxtSMSTemplet.Select();
            }
            catch
            {
            }
        }

        private void FormatSMSTempletGrid()
        {
            grdSMSTemplets.CellDoubleClick += new GridViewCellEventHandler(grdSMSTemplets_CellDoubleClick);
            grdSMSTemplets.CommandCellClick += new CommandCellClickEventHandler(grdSMSTemplets_CommandCellClick);

            grdSMSTemplets.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SMSTEMPLET.ID;
            grdSMSTemplets.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_SMSTEMPLET.POLICYID;
            grdSMSTemplets.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Templates Message";
            col.Width = 640;
            col.ReadOnly = true;
            col.Name = COL_SMSTEMPLET.Tempplet;
            grdSMSTemplets.Columns.Add(col);

            grdSMSTemplets.AddDeleteColumn();

            grdSMSTemplets.AllowAddNewRow = false;
            grdSMSTemplets.ShowGroupPanel = false;
            grdSMSTemplets.ShowRowHeaderColumn = false;

            grdSMSTemplets.CurrentRow = null;
        }
        private void btnAddSMSTemplet_Click(object sender, EventArgs e)
        {
            try
            {

                string error = string.Empty;
                string Templet = TxtSMSTemplet.Text.ToStr();



                if (Templet == "")
                {
                    error += "Required :  Templet";
                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }


                GridViewRowInfo row = null;

                if (grdSMSTemplets.CurrentRow != null)
                {
                    row = grdSMSTemplets.CurrentRow;
                }

                else
                {
                    row = grdSMSTemplets.Rows.AddNew();
                }

                row.Cells[COL_SMSTEMPLET.Tempplet].Value = Templet;

                ClearTemplet();

                TxtSMSTemplet.Select();

            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void ClearTemplet()
        {
            TxtSMSTemplet.Text = string.Empty;
            grdSMSTemplets.CurrentRow = null;
            TxtSMSTemplet.Select();
        }
        

        private void btnClearSMSTemplet_Click(object sender, EventArgs e)
        {
            ClearTemplet();
        }
        void grdSMSTemplets_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (e.Row is GridViewDataRowInfo)
                {
                    TxtSMSTemplet.Text = e.Row.Cells[COL_SMSTEMPLET.Tempplet].Value.ToStr();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }


        }
        void grdSMSTemplets_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Record? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {

                        RadGridView grid = gridCell.GridControl;
                        grid.CurrentRow.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void SaveData()
        {

            try
            {
                Gen_SysPolicy obj = General.GetQueryable<Gen_SysPolicy>(null).FirstOrDefault();
                if (obj != null)
                {
                    objMaster.GetByPrimaryKey(obj.Id);
                    objMaster.Edit();
                }

                string[] skipProperties = { "Gen_SysPolicy" };
                IList<Fleet_DriverTemplet> savedListTemp = objMaster.Current.Fleet_DriverTemplets;
                List<Fleet_DriverTemplet> listofDetailTemp = (from r in grdSMSTemplets.Rows

                                                              select new Fleet_DriverTemplet
                                                              {
                                                                  Id = r.Cells[COL_SMSTEMPLET.ID].Value.ToInt(),
                                                                  Templets = r.Cells[COL_SMSTEMPLET.Tempplet].Value.ToString(),
                                                                  SysPolicyId = r.Cells[COL_SMSTEMPLET.POLICYID].Value.ToInt(),

                                                              }).ToList();


                Utils.General.SyncChildCollection(ref savedListTemp, ref listofDetailTemp, "Id", skipProperties);

                objMaster.Save();

                Display();
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void Display()
        {
            try
            {
                if (objMaster.Current != null)
                {
                    grdSMSTemplets.Rows.Clear();

                    GridViewRowInfo objRowTemplet = null;
                    foreach (var item in objMaster.Current.Fleet_DriverTemplets)
                    {
                        objRowTemplet = grdSMSTemplets.Rows.AddNew();
                        objRowTemplet.Cells[COL_SMSTEMPLET.ID].Value = item.Id;
                        objRowTemplet.Cells[COL_SMSTEMPLET.POLICYID].Value = item.SysPolicyId;
                        objRowTemplet.Cells[COL_SMSTEMPLET.Tempplet].Value = item.Templets;

                    }
                    ClearTemplet();
                    grdSMSTemplets.CurrentRow = null;
                }
            }
            catch
            {
            }
        }

        private void frmDrivertemplet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                Save();
                this.Close();

            }

        }

      


    }
}
