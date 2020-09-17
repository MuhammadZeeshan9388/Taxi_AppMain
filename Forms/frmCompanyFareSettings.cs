using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using DAL;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls.UI;
using Telerik.WinControls;
namespace Taxi_AppMain
{
    public partial class frmCompanyFareSettings : UI.SetupBase
    {
        CompanyFareSettingsBO objCompanyFareSettings;
        public frmCompanyFareSettings()
        {
            InitializeComponent();
            objCompanyFareSettings = new CompanyFareSettingsBO();
            this.SetProperties((INavigation)objCompanyFareSettings);
            FillCombo();
            this.Load += new EventHandler(frmCompanyFareSettings_Load);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnExitForm.Click += new EventHandler(btnExitForm_Click);
            ddlActionType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlActionType_SelectedIndexChanged);
            this.KeyDown += new KeyEventHandler(frmCompanyFareSettings_KeyDown);
            this.grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            this.grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            optDiscount.ToggleStateChanged += new StateChangedEventHandler(optDiscount_ToggleStateChanged);
            this.grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            this.btnNew.Click += new EventHandler(btnNew_Click);
        }

        void btnNew_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row != null && e.Row is GridViewRowInfo)
            {
                int Id = e.Row.Cells["Id"].Value.ToInt();
                objCompanyFareSettings.GetByPrimaryKey(Id);
                DisplayRecord();
            }
        }

        void optDiscount_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (optDiscount.IsChecked)
            {
                lblActionType.Text = "Discount Type";
                lblRate.Text = "Discount Rate";
            }
            else
            {
                lblActionType.Text = "Increment Type";
                lblRate.Text = "Increment Rate";
            }
        }

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            RadGridView grid = gridCell.GridControl;
            if (gridCell.ColumnInfo.Name == "btnDelete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Fare Settings ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    int Id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                    objCompanyFareSettings.GetByPrimaryKey(Id);
                    objCompanyFareSettings.Delete(objCompanyFareSettings.Current);
                    grid.CurrentRow.Delete();
                }
            }
            else if (gridCell.ColumnInfo.Name == "btnEdit")
            {
                int Id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                objCompanyFareSettings.GetByPrimaryKey(Id);
                DisplayRecord();

            }

        }
        public override void DisplayRecord()
        {
            try
            {
                if (objCompanyFareSettings.Current == null) return;
                ddlAccount.SelectedValue = objCompanyFareSettings.Current.CompanyId;
                ddlActionType.Text = objCompanyFareSettings.Current.ActionType;
                if (objCompanyFareSettings.Current.OperatorType.ToInt() == 1)
                {
                    optIncrement.IsChecked = true;
                }
                else
                {
                    optDiscount.IsChecked = true;
                }
                dtpFromDate.Value = objCompanyFareSettings.Current.FromDate;
                dtpTillDate.Value = objCompanyFareSettings.Current.TillDate;
                if (ddlActionType.Text == "Amount")
                {
                    numAmountRate.Minimum = 0;
                    numAmountRate.Maximum = 10000;
                }
                else
                {
                    numAmountRate.Minimum = 0;
                    numAmountRate.Maximum = 100;
                }
                numAmountRate.Value = objCompanyFareSettings.Current.Amount.ToDecimal();
            }
            catch (Exception ex)
            { }
        }
        RadButtonElement button = null;
        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);
        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);
        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {

            if (e.CellElement is GridHeaderCellElement)
            {
                //    e.CellElement
                //e.CellElement.BorderColor = _HeaderRowBorderColor;
                //e.CellElement.BorderColor2 = _HeaderRowBorderColor;
                //e.CellElement.BorderColor3 = _HeaderRowBorderColor;
                //e.CellElement.BorderColor4 = _HeaderRowBorderColor;


                //// e.CellElement.DrawBorder = false;
                //e.CellElement.BackColor = _HeaderRowBackColor;
                //e.CellElement.NumberOfColors = 1;
                e.CellElement.Font = newFont;
                //e.CellElement.ForeColor = Color.Black;
               // e.CellElement.DrawFill = true;

                //e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }
            if (e.CellElement is GridDataCellElement)
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

                e.CellElement.Font = oldFont;
            }
        }

        void frmCompanyFareSettings_Load(object sender, EventArgs e)
        {
            PopulateData();
        }

        void frmCompanyFareSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //this.Close();
            }
        }



        void ddlActionType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (ddlActionType.Text.ToStr() == "Percent")
            {
                numAmountRate.Value = 0;
                numAmountRate.Minimum = 0;
                numAmountRate.Maximum = 100;
            }
            else
            {
                numAmountRate.Value = 0;
                numAmountRate.Minimum = 0;
                numAmountRate.Maximum = 10000;
            }


        }

        void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void FillCombo()
        {
            ComboFunctions.FillCompanyCombo(ddlAccount);
        }

        private void ClearFields()
        {
            ddlAccount.SelectedValue = null;
            numAmountRate.Value = 0;
            objCompanyFareSettings.Clear();
            dtpFromDate.Value = null;
            dtpTillDate.Value = null;
            optIncrement.IsChecked = true;
            ddlAccount.Focus();
        }
        public override void Save()
        {
            try
            {
                int OperatorType = 0;
                if (optIncrement.IsChecked)
                {
                    OperatorType = 1;
                }
                else
                {
                    OperatorType = 2;
                }
                if (objCompanyFareSettings.PrimaryKeyValue == null)
                {
                    objCompanyFareSettings.New();
                }
                else
                {
                    objCompanyFareSettings.Edit();

                }
                TimeSpan fromTime = TimeSpan.Zero;
                if (dtpFromDate.Value != null)
                {
                    int H = dtpFromDate.Value.Value.Hour;
                    int M = dtpFromDate.Value.Value.Minute;
                    TimeSpan.TryParse(H + ":" + M + ":00", out fromTime);
                }
                TimeSpan tillTime = TimeSpan.Zero;
                if (dtpTillDate.Value != null)
                {
                    int HT = dtpTillDate.Value.Value.Hour;
                    int MT = dtpTillDate.Value.Value.Minute;
                    TimeSpan.TryParse(HT + ":" + MT + ":59", out tillTime);
                }
                objCompanyFareSettings.Current.CompanyId = ddlAccount.SelectedValue.ToIntorNull();
                objCompanyFareSettings.Current.FromDate = dtpFromDate.Value.ToDateorNull() + fromTime;
                objCompanyFareSettings.Current.TillDate = dtpTillDate.Value.ToDateorNull() + tillTime;
                objCompanyFareSettings.Current.OperatorType = OperatorType;
                objCompanyFareSettings.Current.ActionType = ddlActionType.Text.Trim();
                objCompanyFareSettings.Current.Amount = numAmountRate.Value.ToDecimal();
                objCompanyFareSettings.Save();
                ClearFields();
                PopulateData();
            }
            catch (Exception ex)
            {
                if (objCompanyFareSettings.Errors.Count > 0)
                    ENUtils.ShowMessage(objCompanyFareSettings.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }


        }
        public override void PopulateData()
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    //grdLister.DataSource = db.stp_GetCompanyFareSettings();

                    var list = (from a in db.stp_GetCompanyFareSettings()
                                orderby a.CompanyName
                                select new
                                {
                                    Id = a.Id,
                                    CompanyName = a.CompanyName,
                                    FromDate = a.FromDate,
                                    TillDate = a.TillDate,
                                    OperatorType = a.OperatorType,
                                    ActionType = a.ActionType,
                                    Amount = a.Amount,
                                }).ToList();
                    grdLister.DataSource = list;
                    grdLister.Columns["Id"].IsVisible = false;
                    grdLister.Columns["CompanyName"].HeaderText = "Company";
                    grdLister.Columns["OperatorType"].HeaderText = "Action";
                    grdLister.Columns["ActionType"].HeaderText = "";
                    grdLister.Columns["FromDate"].HeaderText = "From Date";
                    grdLister.Columns["TillDate"].HeaderText = "Till Date";
                    grdLister.Columns["Amount"].Width = 120;
                    grdLister.Columns["CompanyName"].Width = 220;
                    grdLister.Columns["OperatorType"].Width = 140;
                    grdLister.Columns["ActionType"].Width = 140;
                    grdLister.Columns["FromDate"].Width = 150; ;
                    grdLister.Columns["TillDate"].Width = 150;
                    (grdLister.Columns["FromDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";
                    (grdLister.Columns["TillDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";
                    if (grdLister.Columns.Contains("btnEdit") == false)
                    {
                        GridButton();
                    }
                }

            }
            catch (Exception ex)
            { }
        }
        private void GridButton()
        {
            GridViewCommandColumn colbtn = new GridViewCommandColumn();
            colbtn.Width = 100;
            colbtn.Name = "btnEdit";
            colbtn.UseDefaultText = true;
            colbtn.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            colbtn.DefaultText = "Edit";
            colbtn.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(colbtn);
            colbtn = new GridViewCommandColumn();
            colbtn.Width = 100;
            colbtn.Name = "btnDelete";
            colbtn.UseDefaultText = true;
            colbtn.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            colbtn.DefaultText = "Delete";
            colbtn.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(colbtn);
        }
    }
}
