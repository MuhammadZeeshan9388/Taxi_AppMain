using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Collections;
using Telerik.WinControls.UI;
using System.Linq;
using Utils;
using Telerik.WinControls.Enumerations;
using UI;

namespace Taxi_AppMain
{
    public partial class frmLister : Telerik.WinControls.UI.RadForm
    {

        private bool _IsAutoSizeRows;

        public bool IsAutoSizeRows
        {
            get { return _IsAutoSizeRows; }
            set { _IsAutoSizeRows = value; }
        }

        private object[] _RowData;

        public object[] RowData
        {
            get { return _RowData; }
            set { _RowData = value; }
        }


        private string[] _HiddenColumns;

        public string[] HiddenColumns
        {
            get { return _HiddenColumns; }
            set { _HiddenColumns = value; }
        }

        private List<object[]> _listofData;

        public List<object[]> ListofData
        {
            get { return _listofData; }
            set { _listofData = value; }
        }

        private string _pkField;

        public string PkField
        {
            get { return _pkField; }
            set { _pkField = value; }
        }


        private object _pkValue;

        public object PkValue
        {
            get { return _pkValue; }
            set { _pkValue = value; }
        }


        private bool _IsMultiSelect;

        public bool IsMultiSelect
        {
            get { return _IsMultiSelect; }
            set
            {

                _IsMultiSelect = value;

                if (grdLister.Columns.FindByFieldName(COLCheckBox) == null)
                {
                    CreateCheckBoxColumn();
                }

                grdLister.Columns[0].IsVisible = _IsMultiSelect;
                pnlFooter.Visible = _IsMultiSelect;

            }
        }



        private const string COLCheckBox = "COL_ChECKBOX";





        public frmLister()
        {
            InitializeComponent();


        }

        private void CreateCheckBoxColumn()
        {
            GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
            col.Width = 30;
            col.Name = "COLCheckBox";
            col.FieldName = "COLCheckBox";
            grdLister.Columns.Add(col);

        }


        public frmLister(IList datasource, string primaryfield)
        {

            InitializeComponent();
            this.grdLister.DataSource = datasource;
            this.grdLister.Columns[primaryfield].IsVisible = false;
            //       this.grdLister.BestFitColumns();
            this.PkField = primaryfield;
            GridFunctions.SetFilter(this.grdLister);

        }


        private void HideColumns()
        {
            if (HiddenColumns == null) return;

            foreach (string col in this.HiddenColumns)
            {
                grdLister.Columns[col].IsVisible = false;

            }


        }



        private string[] _BestFitColumns;

        public string[] BestFitColumns
        {
            get { return _BestFitColumns; }
            set { _BestFitColumns = value; }
        }


        private void SetBestFitColumns()
        {
            if (BestFitColumns == null) return;

            foreach (string col in this.BestFitColumns)
            {
                grdLister.Columns[col].BestFit();

            }
        }




        public frmLister(IList datasource, string primaryfield, bool MultiSelect)
        {

            InitializeComponent();
            this.IsMultiSelect = MultiSelect;
            chkSelectAll.Visible = MultiSelect;

            this.grdLister.DataSource = datasource;
            if (grdLister.Columns[primaryfield] != null)
                this.grdLister.Columns[primaryfield].IsVisible = false;


            this.grdLister.AllowAutoSizeColumns = true;
            this.grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

            this.PkField = primaryfield;
            this.ListofData = new List<object[]>();

            GridFunctions.SetFilter(this.grdLister);

            grdLister.MasterTemplate.AllowEditRow = true;
        }



        public frmLister(IList datasource, string primaryfield, bool MultiSelect, string[] hiddenFields, string[] bestfitColumns)
        {

            InitializeComponent();
            this.IsMultiSelect = MultiSelect;
            this.grdLister.DataSource = datasource;
            if (grdLister.Columns[primaryfield] != null)
                this.grdLister.Columns[primaryfield].IsVisible = false;


            this.grdLister.AllowAutoSizeColumns = true;
            this.grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

            this.PkField = primaryfield;
            this.BestFitColumns = bestfitColumns;
            this.ListofData = new List<object[]>();
            this.HiddenColumns = hiddenFields;
            GridFunctions.SetFilter(this.grdLister);

            grdLister.MasterTemplate.AllowEditRow = true;
        }



        public frmLister(IList datasource, string primaryfield, bool MultiSelect, string[] hiddenFields)
        {
            try
            {

                InitializeComponent();
                this.IsMultiSelect = MultiSelect;

                chkSelectAll.Visible = MultiSelect;

                this.grdLister.DataSource = datasource;
                this.grdLister.Columns[primaryfield].IsVisible = false;
                //   this.grdLister.BestFitColumns();
                this.PkField = primaryfield;
                this.ListofData = new List<object[]>();
                this.HiddenColumns = hiddenFields;

                GridFunctions.SetFilter(this.grdLister);
                grdLister.MasterTemplate.AllowEditRow = true;

                if (grdLister.Columns.FindByFieldName("COLCheckBox") != null)
                {
                    grdLister.Columns["COLCheckBox"].AllowFiltering = false;


                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void grdLister_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.Row.Cells[this.PkField] != null)
                this.PkValue = e.Row.Cells[this.PkField].Value;


            if (IsMultiSelect == false)
            {
                this.RowData = new object[e.Row.Cells.Count];

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    this.RowData[i] = e.Row.Cells[i].Value;
                }

                this.Close();
            }


        }

        private void frmLister_Load(object sender, EventArgs e)
        {

            this.grdLister.AutoSizeRows = this.IsAutoSizeRows;

            this.grdLister.ShowGroupPanel = false;
            this.grdLister.AllowAutoSizeColumns = true;
            this.grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

            HideColumns();
            this.grdLister.MasterTemplate.ShowRowHeaderColumn = false;

            SetBestFitColumns();
        }

        private void btnPick_Click(object sender, EventArgs e)
        {



            PickRecords();

        }

        private void PickRecords()
        {

            ListofData.Clear();
            object[] obj;
            foreach (GridViewRowInfo row in grdLister.Rows.Where(c => c.Cells[0].Value.ToBool()))
            {
                obj = new object[grdLister.Columns.Count - 1];

                for (int i = 1; i < row.Cells.Count; i++)
                {
                    obj[i - 1] = row.Cells[i].Value;
                }

                ListofData.Add(obj);

            }

            this.Close();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);

        private void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
            {
                //    e.CellElement
                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;


                // e.CellElement.DrawBorder = false;
                e.CellElement.BackColor = Color.SteelBlue;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.Font = newFont;
                e.CellElement.ForeColor = Color.White;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }


            //if(e.CellElement is GridDataCellElement)
            //{
            //    e.CellElement.RowInfo.Height = 50;

            //}

        }

        private void chkSelectAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SelectAll(args.ToggleState);
        }

        public void SelectAll(ToggleState toggle)
        {
            try
            {
                bool SelectAll = toggle == ToggleState.On;


                try
                {

                    foreach (var item in grdLister.ChildRows)
                    {
                        item.Cells["COLCheckBox"].Value = SelectAll;

                    }
                }
                catch
                {
                    //foreach (var item in grdLister.Rows)
                    //{
                    //    item.Cells["COLCheckBox"].Value = SelectAll;

                    //}

                }



                //grdLister.Rows.ToList().ForEach(c => c.Cells["COLCheckBox"].Value = SelectAll);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }



        }

        private void frmLister_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                this.Close();
            }
            else if (e.KeyCode == Keys.Enter && IsMultiSelect == false)
            {
                if (grdLister.CurrentRow != null)
                {
                    try
                    {
                        grdLister.CurrentRow.Cells[0].Value = true;

                        this.RowData = new object[grdLister.CurrentRow.Cells.Count];

                        for (int i = 0; i < grdLister.CurrentRow.Cells.Count; i++)
                        {
                            this.RowData[i] = grdLister.CurrentRow.Cells[i].Value;
                        }
                        Close();
                    }
                    catch
                    {


                    }



                   // PickRecords();


                }
            }

        }
    }
}
