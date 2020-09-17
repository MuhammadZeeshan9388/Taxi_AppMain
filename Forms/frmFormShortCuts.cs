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

namespace Taxi_AppMain
{
    public partial class frmFormShortCuts : UI.SetupBase
    {

        FormShortCutsBO objMaster;
        public frmFormShortCuts()
        {
            InitializeComponent();
            FormatGrid();
            objMaster = new FormShortCutsBO();
            this.SetProperties((INavigation)objMaster);
            this.btnSaveClose.Click += new EventHandler(btnSaveClose_Click);
            this.Load += new EventHandler(frmFormShortCuts_Load);
            this.btnNew.Click += new EventHandler(btnNew_Click);
        }

        void btnNew_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        void frmFormShortCuts_Load(object sender, EventArgs e)
        {
            PopulateData();
        }

        void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (OnSave())
            {
                this.Close();
            }
        }
  
        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;

            txtFormTitle.Text = objMaster.Current.FormTitle.ToStr().Trim();
            txtShortKey.Text = objMaster.Current.ShortKey.ToStr().Trim();
            txtDescription.Text = objMaster.Current.Description.ToStr().Trim();
            if ((objMaster.Current.BackColor) != null)
            {
                Color clr = Color.FromArgb(objMaster.Current.BackColor.ToInt());

                (txtBgColor.RootElement.Children[0] as RadTextBoxElement).BackColor = clr;
                txtBgColor.Tag = clr.ToArgb();
            }
            if ((objMaster.Current.BackColor) == null || objMaster.Current.BackColor == 0)
            {
                ClearColor(txtBgColor);
            }

        }
        public bool OnSave()
        {
            try
            {
                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    objMaster.Edit();
                }
                objMaster.Current.FormTitle = txtFormTitle.Text.Trim();
                objMaster.Current.ShortKey = txtShortKey.Text.Trim();
                objMaster.Current.Description = txtDescription.Text.Trim();
                objMaster.Current.BackColor = txtBgColor.Tag.ToInt();
               
                objMaster.Save();
                General.RefreshListWithoutSelected<frmFormShortCuts>("frmFormShortCuts1");
                ClearFields();
                return true;
            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
                return false;
            }
            
        }
        public override void Save()
        {
            OnSave();
            PopulateData();
        }

        private void ClearFields()
        {
            ClearColor(txtBgColor);
            //txtFormTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtShortKey.Text = string.Empty;
            txtShortKey.Focus();
            objMaster.Clear();
        }

        private void btnPickBgColor_Click(object sender, EventArgs e)
        {
            SetColor(txtBgColor);
        }

        private void btnClearBgColor_Click(object sender, EventArgs e)
        {
            ClearColor(txtBgColor);
        }

        private void SetColor(RadTextBox txt)
        {
            if (DialogResult.OK == colorDialog1.ShowDialog())
            {
                (txt.RootElement.Children[0] as RadTextBoxElement).BackColor = colorDialog1.Color;
                txt.Tag = colorDialog1.Color.ToArgb();
            }

        }
        private void ClearColor(RadTextBox txt)
        {

            (txt.RootElement.Children[0] as RadTextBoxElement).BackColor = Color.White;
             txt.Tag = null;


        }
        public struct COLS
        {
            public static string Id = "Id";
            public static string FormTitle = "FormTitle";
            public static string ShortKey = "ShortKey";
            public static string Description = "Description";
            public static string BackgroundColor = "BackgroundColor";
            public static string BackgroundColorValue = "BackgroundColorValue";

        }
        public void FormatGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
            grdFormShortCuts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.FormTitle;
            col.ReadOnly = true;
            col.Width = 150;
            col.HeaderText = "Form Title";
            grdFormShortCuts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.ShortKey;
            col.ReadOnly = true;
            col.Width = 150;
            col.HeaderText = "Short Key";
            grdFormShortCuts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.Description;
            col.ReadOnly = true;
            col.Width = 200;
            col.HeaderText = "Description";
            grdFormShortCuts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.BackgroundColor;
            col.ReadOnly = true;
            col.HeaderText = " ";
            col.Width = 100;
            grdFormShortCuts.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = COLS.BackgroundColorValue;
            col.ReadOnly = false;
            col.HeaderText = " ";
            col.IsVisible = false;
            grdFormShortCuts.Columns.Add(col);

            GridViewCommandColumn colPick = new GridViewCommandColumn();
            colPick.UseDefaultText = true;
            colPick.HeaderText = " ";
            colPick.Name = "Edit";
            colPick.DefaultText = "Edit";
            grdFormShortCuts.Columns.Add(colPick);



            GridViewCommandColumn colClear = new GridViewCommandColumn();
            colClear.UseDefaultText = true;
            colClear.HeaderText = " ";
            colClear.Name = "Delete";
            colClear.DefaultText = "Delete";
            grdFormShortCuts.Columns.Add(colClear);


            grdFormShortCuts.TableElement.RowHeight = 40;

            this.grdFormShortCuts.CellDoubleClick += new GridViewCellEventHandler(grdFormShortCuts_CellDoubleClick);
            this.grdFormShortCuts.ViewCellFormatting += new CellFormattingEventHandler(grdFormShortCuts_ViewCellFormatting);
            this.grdFormShortCuts.CommandCellClick += new CommandCellClickEventHandler(grdFormShortCuts_CommandCellClick);
        }

        void grdFormShortCuts_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            int Id = e.Row.Cells["Id"].Value.ToInt();
            if (Id > 0)
            {
                OnDisplayRecord(Id);
            }
        }

        void grdFormShortCuts_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            string name = gridCell.ColumnInfo.Name.ToLower();

            GridViewRowInfo row = gridCell.RowElement.RowInfo;


            if (name == "edit")
            {
                int Id = grdFormShortCuts.CurrentRow.Cells["Id"].Value.ToInt();
                if (Id > 0)
                {
                    OnDisplayRecord(Id);
                }
            }


            else if (name == "delete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Short Key ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    try
                    {
                        int Id = grdFormShortCuts.CurrentRow.Cells["Id"].Value.ToInt();
                        if (Id > 0)
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                var obj = db.UM_FormShortCuts.FirstOrDefault(c => c.Id == Id);
                                db.UM_FormShortCuts.DeleteOnSubmit(obj);
                                db.SubmitChanges();
                            }
                            grdFormShortCuts.CurrentRow.Delete();
                            ClearFields();
                        }
                    }
                    catch (Exception ex)
                    {
                        ENUtils.ShowMessage(ex.Message);
                    }
                }
            }

        }
        void grdFormShortCuts_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                if (e.CellElement is GridDataCellElement)
                {
                    e.CellElement.DrawFill = false;

                    if (e.Column.Name == "BackgroundColor")
                    {

                        e.CellElement.NumberOfColors = 1;


                        int bgColor = e.Row.Cells["BackgroundColorValue"].Value.ToInt();

                        if (bgColor != 0)
                        {

                            e.CellElement.BackColor = Color.FromArgb(bgColor);
                            e.CellElement.DrawFill = true;
                        }
                        else
                        {
                            e.CellElement.BackColor = Color.White;
                            e.CellElement.DrawFill = true;

                        }
                    }
                }
            }
            catch
            {

            }
        }

        public override void PopulateData()
        {
            try
            {
                var list = General.GetQueryable<UM_FormShortCut>(c => c.Id > 0).OrderBy(c => c.FormTitle).ToList();
                grdFormShortCuts.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    grdFormShortCuts.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                    grdFormShortCuts.Rows[i].Cells[COLS.FormTitle].Value = list[i].FormTitle;
                    grdFormShortCuts.Rows[i].Cells[COLS.ShortKey].Value = list[i].ShortKey;
                    grdFormShortCuts.Rows[i].Cells[COLS.Description].Value = list[i].Description;
                    //grdFormShortCuts.Rows[i].Cells[COLS.BackgroundColor].Value = list[i].BackColor;
                    grdFormShortCuts.Rows[i].Cells[COLS.BackgroundColorValue].Value = list[i].BackColor;
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
    

    }
}
