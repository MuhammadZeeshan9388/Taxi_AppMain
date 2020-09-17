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
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmServiceCharges : UI.SetupBase
    {
        ServiceChargesBO objServiceCharges = null;
        public frmServiceCharges()
        {
            InitializeComponent();
            objServiceCharges = new ServiceChargesBO();
            this.SetProperties((INavigation)objServiceCharges);
            this.grdServiceCharges.AllowAddNewRow = true;
            FormatServiceChargesGrid();
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.grdServiceCharges.CommandCellClick += new CommandCellClickEventHandler(grdServiceCharges_CommandCellClick);
            this.Load += new EventHandler(frmServiceCharges_Load);
            this.grdServiceCharges.ViewCellFormatting += new CellFormattingEventHandler(grdServiceCharges_ViewCellFormatting);
        }
        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);


        private Color _HeaderRowBackColor = Color.SteelBlue;

        public Color HeaderRowBackColor
        {
            get { return _HeaderRowBackColor; }
            set { _HeaderRowBackColor = value; }
        }


        private Color _HeaderRowBorderColor = Color.DarkSlateBlue;

        public Color HeaderRowBorderColor
        {
            get { return _HeaderRowBorderColor; }
            set { _HeaderRowBorderColor = value; }
        }

        string cellValue = string.Empty;
        RadButtonElement button = null;
        void grdServiceCharges_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
            {
                //    e.CellElement
                e.CellElement.BorderColor = _HeaderRowBorderColor;
                e.CellElement.BorderColor2 = _HeaderRowBorderColor;
                e.CellElement.BorderColor3 = _HeaderRowBorderColor;
                e.CellElement.BorderColor4 = _HeaderRowBorderColor;


                // e.CellElement.DrawBorder = false;
                e.CellElement.BackColor = _HeaderRowBackColor;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.Font = newFont;
                e.CellElement.ForeColor = Color.White;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }
            else if (e.CellElement is GridDataCellElement)
            {


                if (e.CellElement.ColumnInfo is GridViewCommandColumn)
                {
                    // This is how we get the RadButtonElement instance from the cell
                    button = (RadButtonElement)e.CellElement.Children[0];

                    if (button.Text == "Edit")
                    {
                        button.Image = Resources.Resource1.edit2;
                    }
                    if (button.Text == "Delete")
                    {

                        button.Image = Resources.Resource1.delete;

                    }
                }
            }
        }



        void frmServiceCharges_Load(object sender, EventArgs e)
        {

            grdServiceCharges.AddNewRowPosition = SystemRowPosition.Bottom;
            DisplayRecord();   
        }
        public override void DisplayRecord()
        {
            try
            {
                var list = (from a in General.GetQueryable<Gen_ServiceCharge>(null)
                            orderby a.Id
                            select new
                            {
                                Id=a.Id,
                                FromValue = a.FromValue,
                                ToValue = a.ToValue,
                                ServiceChargeAmount = a.ServiceChargeAmount,
                                ServiceChargePercent = a.ServiceChargePercent,
                                AmountWise = a.AmountWise,
                                IsAccount=a.IsAccount
                            }).ToList();
                grdServiceCharges.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    grdServiceCharges.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                    grdServiceCharges.Rows[i].Cells[COLS.Account].Value = list[i].IsAccount.ToBool();
                    grdServiceCharges.Rows[i].Cells[COLS.FromValue].Value = list[i].FromValue;
                    grdServiceCharges.Rows[i].Cells[COLS.ToValue].Value = list[i].ToValue;
                    grdServiceCharges.Rows[i].Cells[COLS.ServiceChargeAmount].Value = list[i].ServiceChargeAmount.ToDecimal();
                    grdServiceCharges.Rows[i].Cells[COLS.ServiceChargePercent].Value = list[i].ServiceChargePercent;
                    grdServiceCharges.Rows[i].Cells[COLS.AmountWise].Value = list[i].AmountWise;
                }
                              
                //var list2=(objServiceCharges.Current.Ge)
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void grdServiceCharges_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                string name = gridCell.ColumnInfo.Name.ToLower();

                GridViewRowInfo row = gridCell.RowElement.RowInfo;
                
                if (name == "btndelete")
                {
                    int Id=row.Cells[COLS.Id].Value.ToInt();
                    if (Id > 0)
                    {

                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.stp_RunProcedure("delete from gen_servicecharges where id=" + Id);

                        }

                        grdServiceCharges.CurrentRow.Delete();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        public override void Save()
        {
            try
            {
                for (int i = 0; i < grdServiceCharges.RowCount; i++)
                {
                    int Id = grdServiceCharges.Rows[i].Cells[COLS.Id].Value.ToInt();
                    if (Id > 0)
                    {
                        objServiceCharges.GetByPrimaryKey(Id);
                    }
                    if (objServiceCharges.PrimaryKeyValue == null)
                    {
                        objServiceCharges.New();
                    }
                    else
                    {
                        objServiceCharges.Edit();
                    }

                    objServiceCharges.Current.FromValue = grdServiceCharges.Rows[i].Cells[COLS.FromValue].Value.ToDecimal();
                    objServiceCharges.Current.ToValue = grdServiceCharges.Rows[i].Cells[COLS.ToValue].Value.ToDecimal();

                    objServiceCharges.Current.IsAccount = grdServiceCharges.Rows[i].Cells[COLS.Account].Value.ToBool();
                    if (grdServiceCharges.Rows[i].Cells[COLS.AmountWise].Value.ToBool())
                    {
                        objServiceCharges.Current.ServiceChargeAmount = grdServiceCharges.Rows[i].Cells[COLS.ServiceChargeAmount].Value.ToDecimal();
                        objServiceCharges.Current.AmountWise = grdServiceCharges.Rows[i].Cells[COLS.AmountWise].Value.ToBool();
                        objServiceCharges.Current.ServiceChargePercent = 0;
                    }
                    else
                    {
                        objServiceCharges.Current.ServiceChargePercent = grdServiceCharges.Rows[i].Cells[COLS.ServiceChargePercent].Value.ToDecimal();
                        objServiceCharges.Current.AmountWise = false;
                        objServiceCharges.Current.ServiceChargeAmount = 0;
                    }
                    objServiceCharges.Save();
                    objServiceCharges.Clear();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                if (objServiceCharges.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objServiceCharges.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }
        public struct COLS
        {
            public static string Id = "Id";
            public static string Account = "Account";
            public static string FromValue = "FromValue";
            public static string ToValue = "ToValue";
            public static string ServiceChargePercent = "ServiceChargePercent";
            public static string ServiceChargeAmount = "ServiceChargeAmount";
            public static string AmountWise = "AmountWise";
            //ServiceChargePercent
        }


        private void FormatServiceChargesGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdServiceCharges.Columns.Add(col);


            GridViewCheckBoxColumn colcHK = new GridViewCheckBoxColumn();
            colcHK.Name = COLS.Account;
            colcHK.HeaderText = "Account";
            colcHK.Width = 70;
            grdServiceCharges.Columns.Add(colcHK);


            GridViewDecimalColumn dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.FromValue;
            dcol.HeaderText = "From";
            dcol.Width = 90;
            grdServiceCharges.Columns.Add(dcol);

            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.ToValue;
            dcol.HeaderText = "Till";
            dcol.Width = 90;
            grdServiceCharges.Columns.Add(dcol);

            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.ServiceChargePercent;
            dcol.HeaderText = "Service Charge Percent";
            dcol.Width = 170;
            grdServiceCharges.Columns.Add(dcol);


            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS.AmountWise;
            cbcol.HeaderText = "Amount Wise";
            cbcol.Width = 120;
            grdServiceCharges.Columns.Add(cbcol);
            //grdServiceCharges.AddEditColumn();


            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.ServiceChargeAmount;
            dcol.HeaderText = "Service Charge Amount";
            dcol.Width = 170;
            grdServiceCharges.Columns.Add(dcol);

            grdServiceCharges.AddDeleteColumn();

            grdServiceCharges.ShowRowHeaderColumn = false;
            //grdServiceCharges.Columns["btnEdit"].Width = 100;
            grdServiceCharges.Columns["btnDelete"].Width = 80;


        }
    }
}
