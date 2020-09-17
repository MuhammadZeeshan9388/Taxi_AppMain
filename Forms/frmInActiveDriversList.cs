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
using Telerik.WinControls.UI;
using Taxi_Model;
using Utils;
using Telerik.WinControls;
using Taxi_AppMain.Forms;

namespace Taxi_AppMain
{
    public partial class frmInActiveDriversList : UI.SetupBase
    {
        DriverBO objMaster;
        
        public frmInActiveDriversList()
        {
            InitializeComponent();
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new DriverBO();
            
           
            this.SetProperties((INavigation)objMaster);


            grdLister.ViewCellFormatting+=new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmDriversList_Shown);
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);

        
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

        RadButtonElement button = null;
        string cellValue = string.Empty;
        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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

            else if (e.CellElement is GridFilterCellElement)
            {
                e.CellElement.Font = oldFont;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.BackColor = Color.White;
                e.CellElement.RowElement.BackColor = Color.White;
                e.CellElement.RowElement.NumberOfColors = 1;

                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

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
                        button.Image =Resources.Resource1.edit2;
                    }
                    if (button.Text == "Delete")
                    {

                        button.Image = Resources.Resource1.delete;
                    
                    }
                }


                e.CellElement.ToolTipText = e.CellElement.Text;
                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;

                 
                if (e.CellElement.RowElement.IsSelected == true)
                {

                    if (e.Column.Name != "MOTExpiry" && e.Column.Name!="MOT2Expiry" && e.Column.Name != "PCOVehicleExpiry" && e.Column.Name!="LicenseExpiry"
                        && e.Column.Name != "PCODriverExpiry" && e.Column.Name != "InsuranceExpiry")
                    {


                        e.CellElement.RowElement.NumberOfColors = 1;
                        e.CellElement.RowElement.BackColor = Color.DeepSkyBlue;

                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.BackColor = Color.DeepSkyBlue;
                        e.CellElement.ForeColor = Color.White;
                        e.CellElement.Font = newFont;

                    }
                    else
                    {

                       // e.CellElement.DrawFill = false;

                        e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.TwoWayBindingLocal);
                    }


               

                }


                else
                {
                 //   e.CellElement.DrawFill = false;


                    e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.TwoWayBindingLocal);
                }



                if (e.CellElement.RowElement.RowInfo.Cells["EndDate"].Value!=null)
                {
                    if (e.Column.Name == "MOTExpiry" || e.Column.Name == "PCODriverExpiry" || e.Column.Name=="MOT2Expiry" || e.Column.Name=="LicenseExpiry"
                        || e.Column.Name == "InsuranceExpiry" || e.Column.Name == "PCOVehicleExpiry")
                    {
                        e.CellElement.BackColor = Color.White;
                    }
                }

            }
        }

        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {

                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Driver ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    RadGridView grid = gridCell.GridControl;
                    grid.CurrentRow.Delete();
                }
            }
            else if (gridCell.ColumnInfo.Name.ToLower() == "btnedit")
            {
                ViewDetailForm();
            }
        }



        void frmDriversList_Shown(object sender, EventArgs e)
        {
            this.InitializeForm("frmDriver");


            try
            {

                grdLister.AllowAutoSizeColumns = true;
                grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                grdLister.EnableHotTracking = false;
                grdLister.ShowGroupPanel = false;
                grdLister.AllowAddNewRow = false;
                grdLister.AllowEditRow = false;

                PopulateData();

                UI.GridFunctions.SetFilter(grdLister);

                grdLister.AddEditColumn();

                if (this.CanDelete)
                {
                    grdLister.AddDeleteColumn();

                    grdLister.Columns["btnDelete"].Width = 70;
                }

                grdLister.Columns["btnEdit"].Width = 70;


                grdLister.Columns["Id"].IsVisible = false;
                grdLister.Columns["Name"].Width = 150;
                grdLister.Columns["MobileNo"].Width = 100;


                grdLister.Columns["EndDate"].IsVisible = false;

                grdLister.Columns["No"].Width = 70;
                grdLister.Columns["VehicleNo"].Width = 90;
                grdLister.Columns["VehicleNo"].HeaderText = "Vehicle No";
                grdLister.Columns["VehicleType"].HeaderText = "Vehicle";
                grdLister.Columns["VehicleType"].Width = 60;
                grdLister.Columns["MOTExpiry"].HeaderText = "MOT Expiry";


                grdLister.Columns["MOT2Expiry"].HeaderText = "MOT 2 Expiry";
                grdLister.Columns["LicenseExpiry"].HeaderText = "License Expiry";


                grdLister.Columns["PCOVehicleExpiry"].HeaderText = "PHC Vehicle Expiry";
                grdLister.Columns["PCODriverExpiry"].HeaderText = "PHC Driver Expiry";

                grdLister.Columns["InsuranceExpiry"].HeaderText = "Insurance Expiry";
                grdLister.Columns["InsuranceExpiry"].Width = 130;

                (grdLister.Columns["InsuranceExpiry"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";
                (grdLister.Columns["InsuranceExpiry"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";

                (grdLister.Columns["PCODriverExpiry"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
                (grdLister.Columns["PCODriverExpiry"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";


                (grdLister.Columns["PCOVehicleExpiry"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
                (grdLister.Columns["PCOVehicleExpiry"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";

                (grdLister.Columns["MOTExpiry"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
                (grdLister.Columns["MOTExpiry"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";

                (grdLister.Columns["MOT2Expiry"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
                (grdLister.Columns["MOT2Expiry"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";




                foreach (var doc in General.GetQueryable<Gen_Syspolicy_DriverDocumentList>(c => c.IsVisible == false).ToList())
                {
                    if (doc.DocumentName.ToStr().ToLower() == "mot 2")
                        grdLister.Columns["MOT2Expiry"].IsVisible = false;

                    //else if (doc.DocumentName.ToStr().ToLower() == "road tax")
                    //    grdLister.Columns["RoadTaxExpiry"].IsVisible = false;

                }

            }
            catch (Exception ex)
            {


            }

        

        }

    



     


        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ViewDetailForm();
        }

        private void ViewDetailForm()
        {

            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {
                ShowDriverForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }


        private void ShowDriverForm(int id)
        {


            frmDriver frm = new frmDriver();
            frm.OpenedFromInActiveList = true;
            frm.OnDisplayRecord(id);

            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();

        }


        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                    objMaster = new DriverBO();

                    try
                    {

                        objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                        objMaster.Delete(objMaster.Current);


                    }
                    catch (Exception ex)
                    {
                        if (objMaster.Errors.Count > 0)
                            ENUtils.ShowMessage(objMaster.ShowErrors());
                        else
                        {
                            ENUtils.ShowMessage(ex.Message);

                        }
                        e.Cancel = true;

                    }
                

            }
        }



        Font f = new Font("Tahoma", 10, FontStyle.Bold);
        Color clr = Color.Yellow;
        private void grdLister_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column != null && e.Row != null && e.Row.Cells["Id"].Value != null)
            {
                if (e.Column.Name == "Name")
                {
                    e.CellElement.Font = f;

                }

          


            }
        }

        
       


        public override void RefreshData()
        {
            PopulateData();
        }

        private int FilterType = 1;

        public override void PopulateData()
        {
            try
            {

                if (FilterType == 1)
                {

                    var query = (from a in General.GetQueryable<Fleet_Driver>(null).AsEnumerable()
                                 where a.IsActive == false || a.Fleet_Driver_Availabilities.LastOrDefault().DefaultIfEmpty().EndingDate != null

                                 select new
                                {
                                    Id = a.Id,
                                    No = a.DriverNo,
                                    Name = a.DriverName,
                                    VehicleNo = a.VehicleNo,
                                    VehicleType = a.Fleet_VehicleType.VehicleType,
                                    MOTExpiry = a.MOTExpiryDate,
                                    MOT2Expiry = a.MOT2ExpiryDate,
                                    PCOVehicleExpiry = a.PCOVehicleExpiryDate,
                                    InsuranceExpiry = a.InsuranceExpiryDate,
                                    PCODriverExpiry = a.PCODriverExpiryDate,
                                    LicenseExpiry = a.DrivingLicenseExpiryDate,
                                    MobileNo = a.MobileNo,
                                    EndDate = a.Fleet_Driver_Availabilities.LastOrDefault().DefaultIfEmpty().EndingDate

                                })

                                .OrderBy(item => item.No, new NaturalSortComparer<string>());



                    grdLister.DataSource = query.ToList();
                }
                else if (FilterType == 2)
                {

                    var query = (from a in General.GetQueryable<Fleet_Driver>(b => b.IsActive == false).AsEnumerable()
                                 where a.Fleet_Driver_Availabilities.LastOrDefault().DefaultIfEmpty().EndingDate == null

                                 select new
                                 {
                                     Id = a.Id,
                                     No = a.DriverNo,
                                     Name = a.DriverName,
                                     VehicleNo = a.VehicleNo,
                                     VehicleType = a.Fleet_VehicleType.VehicleType,
                                     MOTExpiry = a.MOTExpiryDate,
                                     MOT2Expiry = a.MOT2ExpiryDate,
                                     PCOVehicleExpiry = a.PCOVehicleExpiryDate,
                                     InsuranceExpiry = a.InsuranceExpiryDate,
                                     PCODriverExpiry = a.PCODriverExpiryDate,
                                     LicenseExpiry = a.DrivingLicenseExpiryDate,
                                     MobileNo = a.MobileNo,
                                     EndDate = a.Fleet_Driver_Availabilities.LastOrDefault().DefaultIfEmpty().EndingDate

                                 })

                                .OrderBy(item => item.No, new NaturalSortComparer<string>());



                    grdLister.DataSource = query.ToList();
                }
                else if (FilterType == 3)
                {

                    var query = (from a in General.GetQueryable<Fleet_Driver>(null).AsEnumerable()
                                 where a.Fleet_Driver_Availabilities.LastOrDefault().DefaultIfEmpty().EndingDate != null

                                 select new
                                 {
                                     Id = a.Id,
                                     No = a.DriverNo,
                                     Name = a.DriverName,
                                     VehicleNo = a.VehicleNo,
                                     VehicleType = a.Fleet_VehicleType.VehicleType,
                                     MOTExpiry = a.MOTExpiryDate,
                                     MOT2Expiry = a.MOT2ExpiryDate,
                                     PCOVehicleExpiry = a.PCOVehicleExpiryDate,
                                     InsuranceExpiry = a.InsuranceExpiryDate,
                                     PCODriverExpiry = a.PCODriverExpiryDate,
                                     LicenseExpiry = a.DrivingLicenseExpiryDate,
                                     MobileNo = a.MobileNo,
                                     EndDate = a.Fleet_Driver_Availabilities.LastOrDefault().DefaultIfEmpty().EndingDate

                                 })

                                .OrderBy(item => item.No, new NaturalSortComparer<string>());



                    grdLister.DataSource = query.ToList();
                }

            }
            catch (Exception ex)
            {


            }
            
        }

        private void chkAllDrivers_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkAllDrivers.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                FilterType = 1;
            else if (chkLeftDrivers.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                FilterType = 2;
            else if (chkHolidayDrivers.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                FilterType = 3;

            PopulateData();



        }

      

       

    }
}

