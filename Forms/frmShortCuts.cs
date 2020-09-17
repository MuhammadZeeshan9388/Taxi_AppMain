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
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls;
using System.Net;
using UI;
using System.Xml.Linq;
using Telerik.WinControls.Enumerations;
using System.Data.Linq;
using System.Xml;

namespace Taxi_AppMain
{
    public partial class frmShortCuts : UI.SetupBase
    {
        Font newBigFont = new Font("Tahoma", 14, FontStyle.Bold);
        public frmShortCuts()
        {
            InitializeComponent();
            FormatGrid();
            this.Load += new EventHandler(frmFormShortCuts_Load);
            this.tv_Forms.SelectedNodeChanged += new RadTreeView.RadTreeViewEventHandler(tv_Forms_SelectedNodeChanged);
            this.tv_Forms.NodeMouseDown += new RadTreeView.TreeViewMouseEventHandler(tv_Forms_NodeMouseDown);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmShortCuts_KeyDown);
            this.grdFormShortCuts.ViewCellFormatting+=new CellFormattingEventHandler(grdFormShortCuts_ViewCellFormatting);
        }

        void frmShortCuts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();

            }
        }

        void tv_Forms_NodeMouseDown(object sender, RadTreeViewMouseEventArgs e)
        {
            string Node = e.Node.Text;
            LoadGridFormShortKeys(Node);
        }

        

        void tv_Forms_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
        //    string Node = e.Node.Text;
          //  LoadGridFormShortKeys(Node);
        }

        private void LoadGridFormShortKeys(string Node)
        {

            try
            {
                var list =(from a in General.GetQueryable<UM_FormShortCut>(c => c.FormTitle.ToLower()==Node.ToProperCase())
                           orderby a.ShortKey
                               select new
                               {
                                   a.Id,
                                   a.ShortKey,
                                   a.FormTitle,
                                   a.Description,
                                   a.BackColor
                               }).ToList();

                grdFormShortCuts.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    grdFormShortCuts.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                    grdFormShortCuts.Rows[i].Cells[COLS.FormTitle].Value = list[i].FormTitle;
                    grdFormShortCuts.Rows[i].Cells[COLS.ShortKey].Value = list[i].ShortKey;
                    grdFormShortCuts.Rows[i].Cells[COLS.Description].Value = list[i].Description.ToStr().ToLower().ToProperCase();
                    //grdFormShortCuts.Rows[i].Cells[COLS.BackgroundColor].Value = list[i].BackColor;
                    grdFormShortCuts.Rows[i].Cells[COLS.BackgroundColorValue].Value = list[i].BackColor;
                }

                if (grdFormShortCuts.Rows.Count > 0 && grdFormShortCuts.Rows.Where(c => c.Cells[COLS.BackgroundColorValue].Value.ToInt() == 0).Count() != grdFormShortCuts.RowCount)
                {
                    grdFormShortCuts.ShowColumnHeaders = false;
               
                    grdFormShortCuts.Columns[COLS.Description].Width = 350;
                    grdFormShortCuts.Columns[COLS.BackgroundColor].IsVisible = true;
                    grdFormShortCuts.Columns[COLS.ShortKey].IsVisible = false;
                }
                else
                {
                    grdFormShortCuts.ShowColumnHeaders = true;
                   
                    grdFormShortCuts.Columns[COLS.Description].Width = 350;

                    grdFormShortCuts.Columns[COLS.ShortKey].IsVisible = true;
                    grdFormShortCuts.Columns[COLS.BackgroundColor].IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        
        public struct COLS
        {
            public static string Id = "Id";
            public static string FormTitle = "FormTitle";
            public static string ShortKey = "ShortKey";
            public static string Description = "Description";
            public static string BackgroundColor = "BackgroundColor";
            public static string BackgroundColorValue = "BackgroundColorValue";
            //public static string BackgroundColorOldValue = "BackgroundColorOldValue";

        }
        public void FormatGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
            grdFormShortCuts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.BackgroundColor;
            col.ReadOnly = true;
            col.HeaderText = "Background Color";
            col.Width = 150;
            grdFormShortCuts.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.FormTitle;
            col.ReadOnly = true;
            col.IsVisible = false;
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
            col.Name = COLS.BackgroundColorValue;
            col.ReadOnly = false;
            col.HeaderText = " ";
            col.IsVisible = false;
            grdFormShortCuts.Columns.Add(col);





            //GridViewCommandColumn colPick = new GridViewCommandColumn();
            //colPick.UseDefaultText = true;
            //colPick.HeaderText = " ";
            //colPick.Name = "Edit";
            //colPick.DefaultText = "Edit";
            //grdFormShortCuts.Columns.Add(colPick);



            //GridViewCommandColumn colClear = new GridViewCommandColumn();
            //colClear.UseDefaultText = true;
            //colClear.HeaderText = " ";
            //colClear.Name = "Delete";
            //colClear.DefaultText = "Delete";
            //grdFormShortCuts.Columns.Add(colClear);
             
             //Font font = new Font("Tahonma", 10, FontStyle.Bold);
             //this.groupBox1.Font = font;

            grdFormShortCuts.TableElement.RowHeight = 30;
        
            this.grdFormShortCuts.CommandCellClick += new CommandCellClickEventHandler(grdFormShortCuts_CommandCellClick);
            
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
                    frmFormShortCuts frm = new frmFormShortCuts();
                    frm.OnDisplayRecord(Id);
                    frm.ShowDialog();
                    frm.Dispose();
                }
            }


            else if (name == "delete")
            {
                int Id = grdFormShortCuts.CurrentRow.Cells["Id"].Value.ToInt();
                if (Id > 0)
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        var obj = db.UM_FormShortCuts.FirstOrDefault(c => c.Id == Id);
                        db.UM_FormShortCuts.DeleteOnSubmit(obj);
                        db.SubmitChanges();
                        grdFormShortCuts.CurrentRow.Delete();
                    }
                }
            }

        }

    

  
        void frmFormShortCuts_Load(object sender, EventArgs e)
        {
            radGroupBox1.Font = newBigFont;
            PopulateData();
            this.radGroupBox1.BringToFront();
        }
        public override void PopulateData()
        {
            try
            {
                var list = General.GetQueryable<UM_FormShortCut>(c => c.Id > 0).OrderBy(c=>c.FormTitle).ToList();
                var list2 = list.GroupBy(c => c.FormTitle.ToProperCase()).ToList();
                Font font = new Font("Tahonma", 12, FontStyle.Bold);
                tv_Forms.Font = font;
                tv_Forms.SpacingBetweenNodes = 15;
                tv_Forms.Nodes.Clear();
                foreach (var form in list2)
                {
                    tv_Forms.Nodes.Add(form.Key);
                }
                tv_Forms.ExpandAll();
                if (tv_Forms.Nodes.Count > 0)
                {
                    string Node = tv_Forms.Nodes[0].Text;
                    LoadGridFormShortKeys(Node);
                    tv_Forms.Nodes[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
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

    }
}
