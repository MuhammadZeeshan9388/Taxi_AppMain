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
    public partial class frmDrivertemplet :UI.SetupBase
    {

        SysPolicyBO objMaster;

        public struct COL_SMSTEMPLET
        {
            public static string ID = "Id";
            public static string POLICYID = "POLICYID";
            public static string Tempplet = "Tempplet";
        }

        public frmDrivertemplet()
        {
            InitializeComponent();
            InitializeConstructor();

        }
        RadDropDownMenu EditFare = null;
        private void InitializeConstructor()
        {

            objMaster = new SysPolicyBO();

            this.SetProperties((INavigation)objMaster);

            grdSMSTemplets.CellDoubleClick += new GridViewCellEventHandler(grdSMSTemplets_CellDoubleClick);


            EditFare = new RadDropDownMenu();

            EditFare.BackColor = Color.Orange;


            RadMenuItem EditFareItem1 = new RadMenuItem("Delete");  // 0 index
            EditFareItem1.ForeColor = Color.Red;
            EditFareItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);
            EditFareItem1.Click += new EventHandler(btnDelete_Click);
            EditFare.Items.Add(EditFareItem1);

            grdSMSTemplets.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdSMSTemplets_ContextMenuOpening);
            grdSMSTemplets.RowsChanging += new GridViewCollectionChangingEventHandler(grdSMSTemplets_RowsChanging);
        }

        void grdSMSTemplets_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            try
            {
                if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
                {
                    if (grdSMSTemplets.CurrentRow != null && grdSMSTemplets.CurrentRow is GridViewDataRowInfo)
                    {
                        int id = grdSMSTemplets.CurrentRow.Cells["Id"].Value.ToInt();

                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            var obj = db.Fleet_DriverTemplets.FirstOrDefault(c => c.Id == id);

                            if (obj != null)
                            {

                                db.Fleet_DriverTemplets.DeleteOnSubmit(obj);
                                db.SubmitChanges();

                            }
                        }
                    }


                }
            }
            catch
            {


            }
        }

        void grdSMSTemplets_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = EditFare;
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {

            grdSMSTemplets.CurrentRow.Delete();

            

        }

        void grdSMSTemplets_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdSMSTemplets.CurrentRow != null && grdSMSTemplets.CurrentRow is GridViewDataRowInfo)
            {
                this.SelectedTemplate = grdSMSTemplets.CurrentRow.Cells[COL_SMSTEMPLET.Tempplet].Value.ToStr();


            }


            this.Close();
        }
        private void frmDrivertemplet_Shown(object sender, EventArgs e)
        {
            
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
                        grdSMSTemplets.Rows[0].IsCurrent = true;
                    }
                }
            }
            catch
            {
            }
        }

        private void FormatSMSTempletGrid()
        {

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
            col.Width = 717;
            col.ReadOnly = true;
            col.Name = COL_SMSTEMPLET.Tempplet;
            grdSMSTemplets.Columns.Add(col);


            grdSMSTemplets.AllowAddNewRow = false;
            grdSMSTemplets.ShowGroupPanel = false;
            grdSMSTemplets.ShowRowHeaderColumn = false;

            grdSMSTemplets.CurrentRow = null;
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
                    grdSMSTemplets.CurrentRow = null;
                }
            }
            catch
            {
            }
        }


        private string _SelectedTemplate;

        public string SelectedTemplate
        {
            get { return _SelectedTemplate; }
            set { _SelectedTemplate = value; }
        }



        private void frmDrivertemplet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
            else if (e.KeyCode == Keys.Enter)
            {
                if(grdSMSTemplets.CurrentRow!=null && grdSMSTemplets.CurrentRow is GridViewDataRowInfo)
                {
                    this.SelectedTemplate = grdSMSTemplets.CurrentRow.Cells[COL_SMSTEMPLET.Tempplet].Value.ToStr();
                
                    
                }
              
                    
                 this.Close();
            }

        }

      


    }
}
