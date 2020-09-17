using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_BLL;
using DAL;
using Utils;
using Taxi_Model;
using Telerik.WinControls.Enumerations;
using System.Data.Linq;
using Telerik.WinControls;
using UI;
using System.Collections;

namespace Taxi_AppMain
{
    public partial class frmSpecialDayFaresDetail : UI.SetupBase
    {
        private bool _IsTariffDetailExist = false;

        public bool IsTariffDetailExist
        {
            get { return _IsTariffDetailExist; }
            set { _IsTariffDetailExist = value; }
        }
        bool IsLoaded = false;
        FareBO objMaster;

        //private int _CopyFareVehicleId;

        //public int CopyFareVehicleId
        //{
        //    get { return _CopyFareVehicleId; }
        //    set { _CopyFareVehicleId = value; }
        //}
        private int _VehicleId;

        public int VehicleId
        {
            get { return _VehicleId; }
            set { _VehicleId = value; }
        }

        //List<Fare> CopyFare;
        //List<Fare_PDAMeter> CopyFarePDAMeter;
        //List<Fare_OtherCharge> CopyFareOtherCharge;
        //List<Fare_ChargesDetail> CopyFareChargesDetail;
       // List<Gen_Company> globalcompanyidfilter = new List<Gen_Company>();
        public frmSpecialDayFaresDetail()
        {
            InitializeComponent();
            FormatListerGrid();
            grdLister.AllowAddNewRow = false;
            grdLister.ShowGroupPanel = false;
            //grdLister.TableElement.RowHeight = 30;
            grdLister.ReadOnly = true;
            grdLister.MasterTemplate.ShowRowHeaderColumn = false;
            FormatFaresOtherDetailGrid();
            FormatFaresPDAOtherDetailGrid();
            FormatFaresDetailGrid();
            ddlCompany.Enabled = false;
            objMaster = new FareBO();
            this.SetProperties((INavigation)objMaster);
            this.Load += new EventHandler(frmSpecialDayFares_Load);
            this.Shown += new EventHandler(frmSpecialDayFaresDetail_Shown);
            this.grdOtherCharges.CellDoubleClick += new GridViewCellEventHandler(grdOtherCharges_CellDoubleClick);
            this.grdOtherCharges.CellClick += new GridViewCellEventHandler(grdOtherCharges_CellClick);
            this.btnAdd_OtherCharges.Click += new EventHandler(btnAdd_OtherCharges_Click);
            this.btnClear_OtherChrges.Click += new EventHandler(btnClear_OtherChrges_Click);

            this.grdLister.CellClick += new GridViewCellEventHandler(grdVehicleTypes_CellClick);
            this.grdPDAOtherCharges.CellDoubleClick += new GridViewCellEventHandler(grdPDAOtherCharges_CellDoubleClick);
            this.btnPDAAdd_OtherCharges.Click += new EventHandler(btnPDAAdd_OtherCharges_Click);
            this.btnPDAClear_OtherChrges.Click += new EventHandler(btnPDAClear_OtherChrges_Click);
            this.chkSpecialDay.ToggleStateChanged += new StateChangedEventHandler(chkSpecialDay_ToggleStateChanged);

            this.KeyDown += new KeyEventHandler(frmSpecialDayFares_KeyDown);
            this.grdOtherCharges.CommandCellClick += new CommandCellClickEventHandler(grdOtherCharges_CommandCellClick);
            this.grdPDAOtherCharges.CommandCellClick += new CommandCellClickEventHandler(grdPDAOtherCharges_CommandCellClick);
            this.chkCompanyWise.ToggleStateChanged += new StateChangedEventHandler(chkCompanyWise_ToggleStateChanged);
            this.ddlSubCompanyId.SelectedValueChanged += new EventHandler(ddlSubCompanyId_SelectedValueChanged);
            this.ddlCompany.SelectedValueChanged += new EventHandler(ddlCompany_SelectedValueChanged);
          
            //Fixed Fares
            this.btnAdd.Click += new EventHandler(btnAdd_Click);
            this.btnClear.Click += new EventHandler(btnClear_Click);
            this.grdDetails.CellDoubleClick += new GridViewCellEventHandler(grdDetails_CellDoubleClick);
            this.btnAddFromLocation.Click += new EventHandler(btnAddFromLocation_Click);
            this.ddlFromLocType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlFromLocType_SelectedIndexChanged);
            this.ddlToLocType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlToLocType_SelectedIndexChanged);
            this.btnEditFromLoc.Click += new EventHandler(btnEditFromLoc_Click);
            this.btnEditToLoc.Click += new EventHandler(btnEditToLoc_Click);
            this.btnAddFromLoc.Click += new EventHandler(btnAddFromLoc_Click);
            this.btnAddToLoc.Click += new EventHandler(btnAddToLoc_Click);
            this.btnDeleteFromLst.Click += new EventHandler(btnDeleteFromLst_Click);
            this.btnDeleteToLst.Click += new EventHandler(btnDeleteToLst_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
        }

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "coldelete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Record ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    int VehicleTypeId = grdLister.CurrentRow.Cells["VehicleTypeId"].Value.ToInt();
                    int ? CompanyId = grdLister.CurrentRow.Cells["CompanyId"].Value.ToIntorNull();
                    int SubCompanyId= grdLister.CurrentRow.Cells["SubCompanyId"].Value.ToInt();
                   // var list = General.GetQueryable<Fare>(c => (c.VehicleTypeId == VehicleId) && (c.SubCompanyId == SubCompanyId) && ((c.CompanyId == null || c.CompanyId == CompanyId))).ToList();
                    try
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            //var students = db.student.Where(s => s.caurse_id == caurseID);

                            var list = db.Fares.Where(c => (c.VehicleTypeId == VehicleId) && (c.SubCompanyId == SubCompanyId) && ((c.CompanyId == null || c.CompanyId == CompanyId))).ToList();


                            db.Fares.DeleteAllOnSubmit(list);
                            db.SubmitChanges();
                            grdLister.CurrentRow.Delete();
                            if (grdLister.Rows.Count > 0 && grdLister.CurrentRow != null)
                            {
                                VehicleTypeId = grdLister.CurrentRow.Cells["VehicleTypeId"].Value.ToInt();
                                CompanyId = grdLister.CurrentRow.Cells["CompanyId"].Value.ToIntorNull();
                                SubCompanyId = grdLister.CurrentRow.Cells["SubCompanyId"].Value.ToInt();
                                ddlCompany.SelectedValue = CompanyId;
                                ddlSubCompanyId.SelectedValue = SubCompanyId;

                                DisplayFares(VehicleTypeId, SubCompanyId, CompanyId);
                            }
                            else
                            {
                                grdDetails.Rows.Clear();
                                grdOtherCharges.Rows.Clear();
                                grdPDAOtherCharges.Rows.Clear();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ENUtils.ShowMessage(ex.Message);
                    }
                    
                }
            }
        }

        void frmSpecialDayFaresDetail_Shown(object sender, EventArgs e)
        {
            if (grdLister.RowCount > 0 && grdLister.CurrentRow != null)
            {
                int VehicleTypeId = grdLister.CurrentRow.Cells["VehicleTypeId"].Value.ToInt();
                int? CompanyId = grdLister.CurrentRow.Cells["CompanyId"].Value.ToIntorNull();
                int SubCompanyId = grdLister.CurrentRow.Cells["SubCompanyId"].Value.ToInt();
                ddlCompany.SelectedValue = CompanyId;
                ddlSubCompanyId.SelectedValue = SubCompanyId;
                ddlSubCompanyId.Enabled = false;
                ddlCompany.Enabled = false;
                chkCompanyWise.Enabled = false;
                DisplayFares(VehicleTypeId, SubCompanyId, CompanyId);
            }
        }

        

        void btnSave_Click(object sender, EventArgs e)
        {
            if (grdLister.RowCount == 0 || grdLister.CurrentRow == null)
            {
                ENUtils.ShowMessage("Please select Vehicle to save Fixed Fares");
                return;
            }
            if (grdDetails.RowCount == 0)
            {
                ENUtils.ShowMessage("Required : Fixed Fares");
                return;
            }
            Save();
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnDeleteToLst_Click(object sender, EventArgs e)
        {
            DeleteItemFromListBox(lstToLocation, lstToLocation.SelectedItem);
        }

        void btnDeleteFromLst_Click(object sender, EventArgs e)
        {
            DeleteItemFromListBox(lstFromLocation, lstFromLocation.SelectedItem);
        }

        void ddlToLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillToLocations();
        }

        void ddlFromLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillFromLocations();
        }

        void btnAddToLoc_Click(object sender, EventArgs e)
        {
            int? LocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            int? LocId = ddlToLocation.SelectedValue.ToIntorNull();

            string msg = string.Empty;
            string locName = ddlToLocation.Text.ToStr();
            string PostCode = "";




            if (LocTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                if (LocId == null)
                {
                    PostCode = General.GetPostCodeMatch(locName);
                    if (!string.IsNullOrEmpty(PostCode) && PostCode.IsAlpha() == false)
                    {
                        AddLocation(locName, ref LocId, ref PostCode);
                        FillToLocations();



                    }
                }
            }





            if (LocId == null)
            {
                msg += "Required : To Location." + Environment.NewLine;

            }

            if (!string.IsNullOrEmpty(msg))
            {
                ENUtils.ShowMessage(msg);
                return;

            }


            AddLocationToListBox(lstToLocation, LocId, locName, LocTypeId, ddlToLocation.Tag.ToIntorNull());

            ClearToLocation();
        }

        void btnAddFromLoc_Click(object sender, EventArgs e)
        {
            int? fromLocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
            int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            int? fromLocId = ddlFromLocation.SelectedValue.ToIntorNull();

            string msg = string.Empty;
            string fromLocation = ddlFromLocation.Text.ToStr();
            string fromPostCode = "";


            if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                if (fromLocId == null)
                {
                    fromPostCode = General.GetPostCodeMatch(fromLocation);
                    if (!string.IsNullOrEmpty(fromPostCode) && fromPostCode.IsAlpha() == false)
                    {
                        AddLocation(fromLocation, ref fromLocId, ref fromPostCode);
                        FillFromLocations();



                    }
                }
            }




            if (fromLocId == null)
            {
                msg += "Required : From Location." + Environment.NewLine;

            }

            if (!string.IsNullOrEmpty(msg))
            {
                ENUtils.ShowMessage(msg);
                return;

            }

            AddLocationToListBox(lstFromLocation, fromLocId, fromLocation, fromLocTypeId, ddlFromLocation.Tag.ToIntorNull());

            ClearFromLocation();
        }


        void btnEditToLoc_Click(object sender, EventArgs e)
        {
            if (lstToLocation.SelectedItem == null)
            {
                ENUtils.ShowMessage("Please select a value to Edit");
                return;
            }

            RadListDataItem item = lstToLocation.SelectedItem;


            ddlToLocType.SelectedValue = item.Value.ToStr().Substring(item.Value.ToStr().IndexOf(',') + 1).ToIntorNull();
            FillToLocations();
            ddlToLocation.SelectedValue = item.Value.ToStr().Remove(item.Value.ToStr().IndexOf(',')).ToInt();


            ddlToLocation.Tag = lstToLocation.SelectedIndex;
        }

        void btnEditFromLoc_Click(object sender, EventArgs e)
        {
            if (lstFromLocation.SelectedItem == null)
            {
                ENUtils.ShowMessage("Please select a value to Edit");
                return;
            }

            RadListDataItem item = lstFromLocation.SelectedItem;


            ddlFromLocType.SelectedValue = item.Value.ToStr().Substring(item.Value.ToStr().IndexOf(',') + 1).ToIntorNull();
            FillFromLocations();
            ddlFromLocation.SelectedValue = item.Value.ToStr().Remove(item.Value.ToStr().IndexOf(',')).ToInt();

            ddlFromLocation.Tag = lstFromLocation.SelectedIndex;
        }


        void btnAddFromLocation_Click(object sender, EventArgs e)
        {
            RadButton ddl = (RadButton)sender;

            int? locTypeId = null;
            string name = ddl.Name;
            if (name == "btnAddFromLocation")
            {
                locTypeId = ddlFromLocType.SelectedValue.ToIntorNull();

            }
            else if (name == "btnAddToLocation")
            {
                locTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            }


            int? locId = General.ShowLocationForm(locTypeId);


            if (name == "btnAddFromLocation")
            {
                FillFromLocations();
                ddlFromLocation.SelectedValue = locId;

            }
            else if (name == "btnAddToLocation")
            {
                FillToLocations();
                ddlToLocation.SelectedValue = locId;

            }
        }

        void ddlCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            //int Id = 0;
            //if (grdLister.RowCount > 0 && grdLister.CurrentRow != null)
            //{
            //    Id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
            //}
            //DisplayFares(Id, ddlSubCompanyId.SelectedValue.ToInt(), ddlCompany.SelectedValue.ToIntorNull());
        }

        void ddlSubCompanyId_SelectedValueChanged(object sender, EventArgs e)
        {
            //int Id = 0;
            //if (grdLister.RowCount > 0 && grdLister.CurrentRow != null)
            //{
            //    Id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
            //}
            //DisplayFares(Id, ddlSubCompanyId.SelectedValue.ToInt(), ddlCompany.SelectedValue.ToIntorNull());
        }

        void chkCompanyWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            CompanyWise(args.ToggleState);
        }
       
        void grdPDAOtherCharges_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "coldelete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Record ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    try
                    {
                        int Id = grdPDAOtherCharges.CurrentRow.Cells["Id"].Value.ToInt();
                        if (Id > 0)
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                var tbl = db.Fare_PDAMeters.FirstOrDefault(c => c.Id == Id);
                                db.Fare_PDAMeters.DeleteOnSubmit(tbl);
                                db.SubmitChanges();
                                grdPDAOtherCharges.CurrentRow.Delete();
                                IsTariffDetailExist = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ENUtils.ShowMessage(ex.Message);
                    }
                }
            }
            else if (gridCell.ColumnInfo.Name.ToLower() == "coledit")
            {
                GridViewRowInfo row = grdPDAOtherCharges.CurrentRow;

                numpdafrommile.Value = row.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal();
                numpdatomile.Value = row.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal();

                numPDARates_OtherCharges.Value = row.Cells[COLS_OTHERDETAILS.FARE].Value.ToDecimal();
            }

        }

        void grdOtherCharges_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "coldelete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Record ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    try
                    {
                        int Id = grdOtherCharges.CurrentRow.Cells["Id"].Value.ToInt();
                        var query = General.GetObject<Fare_ChargesDetail>(c => c.FareId == Id);
                        if (query != null)
                        {
                            ENUtils.ShowMessage("Fixed Fares exist so it can't delete...!");
                            return;
                        }
                        if (Id > 0)
                        {
                            objMaster.GetByPrimaryKey(Id);
                            objMaster.Delete(objMaster.Current);

                            grdOtherCharges.CurrentRow.Delete();
                            //ClearFaresDetail();
                            if (grdPDAOtherCharges.RowCount > 0 && grdPDAOtherCharges.CurrentRow != null)
                            {
                                grdPDAOtherCharges.CurrentRow.Delete();
                            }
                            if (grdPDAOtherCharges.RowCount > 0)
                            {
                                grdPDAOtherCharges.Rows[0].IsCurrent = true;
                                grdPDAOtherCharges.CurrentRow.Delete();
                            }
                            if (grdOtherCharges.RowCount > 0)
                            {
                                int FareId = grdOtherCharges.Rows[0].Cells["Id"].Value.ToInt();
                                if (FareId > 0)
                                {
                                    objMaster.GetByPrimaryKey(FareId);
                                    Fare obj = objMaster.Current;
                                    DisplayFarePDAOtherDetails(obj);
                                    if (grdPDAOtherCharges.RowCount > 0)
                                    {
                                        grdPDAOtherCharges.Rows[0].IsCurrent = true;
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
            }
            else if (gridCell.ColumnInfo.Name.ToLower() == "coledit")
            {
                EditFaresDetail();


            }
        }



        void frmSpecialDayFares_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        void chkSpecialDay_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                txtSpecialDayName.Visible = true;
                lblFromSpecialDateTime.Visible = true;
                lblTillSpecialDateTime.Visible = true;
                //dtpFromSpecialDate.Visible = true;
                //dtpTillSpecialDate.Visible = true;
                lblFromSpecialTime.Visible = true;
                lblTillSpecialTime.Visible = true;
                dtpFromSpecialDate.Visible = true;
                dtpTillSpecialDate.Visible = true;
                dtpFromSpecialTime.Visible = true;
                dtpTillSpecialTime.Visible = true;
                DateTime? fromStartTime = DateTime.Now.ToDateorNull();
                dtpFromSpecialDate.Value = null;
                dtpTillSpecialDate.Value = null;
                dtpFromSpecialTime.Value = fromStartTime;
                dtpTillSpecialTime.Value = fromStartTime;


                ddlTillDay.Text = "";
                ddlFromDay.Text = "";

                dtpFromStartTime.Value = null;
                dtpTillStartTime.Value = null;

                ddlTillDay.Enabled = false;
                ddlFromDay.Enabled = false;
                dtpFromStartTime.Enabled = false;
                dtpTillStartTime.Enabled = false;
            }
            else
            {
                txtSpecialDayName.Visible = false;
                lblFromSpecialDateTime.Visible = false;
                lblTillSpecialDateTime.Visible = false;
                lblFromSpecialTime.Visible = false;
                lblTillSpecialTime.Visible = false;
                dtpFromSpecialDate.Visible = false;
                dtpTillSpecialDate.Visible = false;
                dtpFromSpecialTime.Visible = false;
                dtpTillSpecialTime.Visible = false;
                ddlTillDay.Enabled = true;
                ddlFromDay.Enabled = true;
                dtpFromStartTime.Enabled = true;
                dtpTillStartTime.Enabled = true;
            }
        }

        void btnPDAClear_OtherChrges_Click(object sender, EventArgs e)
        {
            ClearPDAOtherChargesDetails();
        }

        void btnPDAAdd_OtherCharges_Click(object sender, EventArgs e)
        {
            AddPDAOtherChargesDetail();
        }

        void grdOtherCharges_CellClick(object sender, GridViewCellEventArgs e)
        {
            //if (!IsTariffDetailExist)
           // if (!IsTariffDetailExist && grdOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value != null)
            if (!IsTariffDetailExist && grdOtherCharges.Rows.Count > 0 && grdOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value != null)
            {
                string Tariff = "";
                if (grdPDAOtherCharges.RowCount == 0 || grdPDAOtherCharges.CurrentRow == null)
                {
                    Tariff = lblTariff.Text.ToStr();
                    //int FareId = grdPDAOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.FAREID].Value.ToInt();
                    string CurrentTariff = Tariff.Replace("Mileage", "").Trim();

                    //
                    for (int i = 0; i < grdOtherCharges.RowCount; i++)
                    {
                        if (grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TARIFF].Value.ToStr() == CurrentTariff)
                        {
                            grdOtherCharges.Rows[i].IsCurrent = true;
                        }
                    }
                }
                Tariff = Tariff.Replace("Mileage", "PDA Mileage");
                ENUtils.ShowMessage("Required : " + Tariff);
                return;
            }

            if (e.Row != null && e.Row is GridViewRowInfo && !string.IsNullOrEmpty(e.Row.Cells["Id"].Value.ToStr()))
            {
                EditFaresDetail();
                int Id = e.Row.Cells["Id"].Value.ToInt();
                if (Id > 0)
                {
                    objMaster.GetByPrimaryKey(Id);
                    IsTariffDetailExist = false;
                    Fare obj = objMaster.Current;
                    DisplayFarePDAOtherDetails(obj);
                    if (grdPDAOtherCharges.RowCount > 0)
                    {
                        grdPDAOtherCharges.Rows[0].IsCurrent = true;

                    }
                }
            }
        }


        void grdPDAOtherCharges_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdPDAOtherCharges.CurrentRow != null && grdPDAOtherCharges.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdPDAOtherCharges.CurrentRow;

                numpdafrommile.Value = row.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal();
                numpdatomile.Value = row.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal();

                numPDARates_OtherCharges.Value = row.Cells[COLS_OTHERDETAILS.FARE].Value.ToDecimal();
            }
        }

        void grdVehicleTypes_CellClick(object sender, GridViewCellEventArgs e)
        {
            int VehicleTypeId = grdLister.CurrentRow.Cells["VehicleTypeId"].Value.ToInt();
            int? CompanyId = grdLister.CurrentRow.Cells["CompanyId"].Value.ToIntorNull();
            int SubCompanyId = grdLister.CurrentRow.Cells["SubCompanyId"].Value.ToInt();

            if (!IsTariffDetailExist && grdOtherCharges.Rows.Count > 0 && grdOtherCharges.CurrentRow!=null && grdOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value != null)
            {

                if (grdOtherCharges.CurrentRow != null)
                {
                    //int VehicleTypeId = grdOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.VehicleTypeId].Value.ToInt();
                    for (int i = 0; i < grdLister.RowCount; i++)
                    {
                        if (grdLister.Rows[i].Cells["VehicleTypeId"].Value.ToInt() == VehicleTypeId)
                        {
                            grdLister.Rows[i].IsCurrent = true;
                        }
                    }
                    string Tariff = lblTariff.Text;
                    Tariff = Tariff.Replace("Mileage", "PDA Mileage");
                    ENUtils.ShowMessage("Required : " + Tariff);
                    return;
                }
            }

            ddlCompany.SelectedValue = CompanyId;
            ddlSubCompanyId.SelectedValue = SubCompanyId;
            //ddlSubCompanyId.Enabled = false;
            //ddlCompany.Enabled = false;
            //chkCompanyWise.Enabled = false;
            DisplayFares(VehicleTypeId, SubCompanyId, CompanyId);
        }

        void grdOtherCharges_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            EditFaresDetail();
        }

        void btnClear_OtherChrges_Click(object sender, EventArgs e)
        {

            //if (!IsTariffDetailExist)
            //if (!IsTariffDetailExist && grdOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value != null)
            if (!IsTariffDetailExist && grdOtherCharges.Rows.Count > 0 && grdOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value != null)
            {
                string Tariff = lblTariff.Text;
                Tariff = Tariff.Replace("Mileage", "PDA Mileage");
                ENUtils.ShowMessage("Required : " + Tariff);
                return;
            }
            ClearFaresDetail();
            grdOtherCharges.CurrentRow = null;
            grdPDAOtherCharges.CurrentRow = null;
        }
        private void EditFaresDetail()
        {
            try
            {
                if (grdOtherCharges.CurrentRow != null && grdOtherCharges.CurrentRow is GridViewDataRowInfo)
                {
                    GridViewRowInfo row = grdOtherCharges.CurrentRow;
                    ddlFromDay.Text = row.Cells[COLS_OTHERDETAILS.FromDay].Value.ToStr();
                    ddlTillDay.Text = row.Cells[COLS_OTHERDETAILS.TillDay].Value.ToStr();
                    numStartRate.Value = row.Cells[COLS_OTHERDETAILS.StartRate].Value.ToDecimal();
                    numStartRateMile.Value = row.Cells[COLS_OTHERDETAILS.StartRateMile].Value.ToDecimal();
                    dtpFromStartTime.Value = row.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value.ToDateTime();
                    dtpTillStartTime.Value = row.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value.ToDateTime();

                    chkSpecialDay.Checked = row.Cells[COLS_OTHERDETAILS.IsSpecialDay].Value.ToBool();
                    txtSpecialDayName.Text = row.Cells[COLS_OTHERDETAILS.SpecialDayName].Value.ToStr();
                    dtpFromSpecialDate.Value = row.Cells[COLS_OTHERDETAILS.FromSpecialDate].Value.ToDateorNull();
                    dtpTillSpecialDate.Value = row.Cells[COLS_OTHERDETAILS.TillSpecialDate].Value.ToDateorNull();

                    dtpFromSpecialTime.Value = row.Cells[COLS_OTHERDETAILS.FromSpecialDate].Value.ToDateTimeorNull();
                    dtpTillSpecialTime.Value = row.Cells[COLS_OTHERDETAILS.TillSpecialDate].Value.ToDateTimeorNull();

                    chkCompanyWise.Checked = row.Cells[COLS_OTHERDETAILS.IsCompanyWise].Value.ToBool();
                    ddlCompany.SelectedValue = row.Cells[COLS_OTHERDETAILS.CompanyId].Value.ToInt();
                    ddlSubCompanyId.SelectedValue = row.Cells[COLS_OTHERDETAILS.SubCompanyId].Value.ToInt();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        void btnAdd_OtherCharges_Click(object sender, EventArgs e)
        {
            AddFaresDetail();
        }

        private void FillCombos()
        {
            ComboFunctions.FillCompanyCombo(ddlCompany);
            ComboFunctions.FillSubCompanyCombo(ddlSubCompanyId);

            if (ddlSubCompanyId.Items.Count == 1)
                ddlSubCompanyId.SelectedIndex = 0;
        }

        private void FillDaysCombo()
        {
            try
            {
                ddlFromDay.NullText = "Select";
                ddlFromDay.Items.Add("Monday");
                ddlFromDay.Items.Add("Tuesday");
                ddlFromDay.Items.Add("Wednesday");
                ddlFromDay.Items.Add("Thursday");
                ddlFromDay.Items.Add("Friday");
                ddlFromDay.Items.Add("Saturday");
                ddlFromDay.Items.Add("Sunday");

                ddlTillDay.NullText = "Select";
                ddlTillDay.Items.Add("Monday");
                ddlTillDay.Items.Add("Tuesday");
                ddlTillDay.Items.Add("Wednesday");
                ddlTillDay.Items.Add("Thursday");
                ddlTillDay.Items.Add("Friday");
                ddlTillDay.Items.Add("Saturday");
                ddlTillDay.Items.Add("Sunday");


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void frmSpecialDayFares_Load(object sender, EventArgs e)
        {
            FillCombos();
            FillDaysCombo();
            FillLocationTypesCombo();
            // FillFromLocations();
            // FillToLocations();
            Populate();
            IsLoaded = true;
            grdOtherCharges.CurrentRow = null;
            if (grdLister.RowCount > 0)
            {
                grdLister.Rows[0].IsCurrent = true;
                int Id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();

                DisplayFares(Id, ddlSubCompanyId.SelectedValue.ToInt(), ddlCompany.SelectedValue.ToIntorNull());
                if (grdOtherCharges.RowCount > 0)
                {
                    grdOtherCharges.Rows[0].IsCurrent = true;
                    EditFaresDetail();
                    grdOtherCharges.CurrentRow = null;
                }

            }

            //W


        }
        private void AddPDAOtherChargesDetail()
        {
            try
            {

                decimal fromMile = numpdafrommile.Value.ToDecimal();
                decimal toMile = numpdatomile.Value.ToDecimal();
                decimal rate = numPDARates_OtherCharges.Value.ToDecimal();
                string msg = string.Empty;


                TimeSpan FromStartTime = TimeSpan.Zero;
                TimeSpan TillStartTime = TimeSpan.Zero;

                if (toMile == 0)
                {
                    msg += "Required : To Mile." + Environment.NewLine;
                }

                if (rate == 0)
                {
                    msg += "Required : Fare rate." + Environment.NewLine;
                }
                if (grdOtherCharges.CurrentRow == null)
                {
                    msg += "Please select Fare Rate Type." + Environment.NewLine;
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    ENUtils.ShowMessage(msg);
                    return;

                }


                GridViewRowInfo row;
                if (grdPDAOtherCharges.CurrentRow == null &&
                    grdPDAOtherCharges.Rows.Count(c =>
                                            c.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal() == fromMile
                                        && c.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal() == toMile
                                         ) > 0)
                {
                    ENUtils.ShowMessage("From Mile and To Mile already exist");
                    // numFromMile.Focus();
                    return;
                }

                if (grdPDAOtherCharges.RowCount == 0 && grdPDAOtherCharges.CurrentRow != null)
                {
                    grdPDAOtherCharges.CurrentRow = null;
                }
                if (grdPDAOtherCharges.CurrentRow != null)
                    row = grdPDAOtherCharges.CurrentRow;
                else
                    row = grdPDAOtherCharges.Rows.AddNew();


                row.Cells[COLS_OTHERDETAILS.FROMMILE].Value = fromMile;
                row.Cells[COLS_OTHERDETAILS.TOMILE].Value = toMile;

                row.Cells[COLS_OTHERDETAILS.FARE].Value = rate;



                try
                {

                    int Id = grdOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.ID].Value.ToInt();

                    if (Id > 0)
                    {
                        objMaster.GetByPrimaryKey(Id);
                        objMaster.Edit();
                    }

                    objMaster.SaveWithoutValidatingVehicleType = true;
                    string[] skipProperties = { "Gen_Location", "Gen_Location1","Gen_LocationType",
                                            "Gen_LocationType1","Fare","Gen_Zone1","Gen_Zone","Fare_ZoneWisePricing1","Fare_ZoneWisePricing"};

                    IList<Fare_PDAMeter> savedList3 = objMaster.Current.Fare_PDAMeters;
                    List<Fare_PDAMeter> listofpdaOtherDetail = (from r in grdPDAOtherCharges.Rows
                                                                select new Fare_PDAMeter
                                                                {
                                                                    Id = r.Cells[COLS_OTHERDETAILS.ID].Value.ToLong(),
                                                                    FareId = r.Cells[COLS_OTHERDETAILS.FAREID].Value.ToInt(),
                                                                    FromMile = r.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal(),
                                                                    ToMile = r.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal(),
                                                                    Rate = r.Cells[COLS_OTHERDETAILS.FARE].Value.ToDecimal(),

                                                                }).ToList();


                    Utils.General.SyncChildCollection(ref savedList3, ref listofpdaOtherDetail, "Id", skipProperties);
                    //

                    IList<Fare_OtherCharge> savedList4 = objMaster.Current.Fare_OtherCharges;
                    List<Fare_OtherCharge> listofFareothercharge = (from r in grdPDAOtherCharges.Rows
                                                                    select new Fare_OtherCharge
                                                                {
                                                                    Id = r.Cells[COLS_OTHERDETAILS.ID].Value.ToLong(),
                                                                    FareId = r.Cells[COLS_OTHERDETAILS.FAREID].Value.ToInt(),
                                                                    FromMile = r.Cells[COLS_OTHERDETAILS.FROMMILE].Value.ToDecimal(),
                                                                    ToMile = r.Cells[COLS_OTHERDETAILS.TOMILE].Value.ToDecimal(),
                                                                    Rate = r.Cells[COLS_OTHERDETAILS.FARE].Value.ToDecimal(),

                                                                }).ToList();


                    Utils.General.SyncChildCollection(ref savedList4, ref listofFareothercharge, "Id", skipProperties);
                    //
                    objMaster.Save();
                    IsTariffDetailExist = true;
                }
                catch (Exception exc)
                {
                    if (objMaster.Errors.Count > 0)
                        ENUtils.ShowMessage(objMaster.ShowErrors());
                    else
                    {
                        ENUtils.ShowMessage(exc.Message);
                    }
                }

                ClearPDAOtherChargesDetails();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        private void Populate()
        {
            try
            {
                List<stp_GetFareListByVehicleResult> data1 = null;
                List<stp_GetFareListByVehicleResult> data2 = new List<stp_GetFareListByVehicleResult>();

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    data1 = db.stp_GetFareListByVehicle(VehicleId).ToList();
                }
                foreach (var item in data1)
                {
                    if(data2.Count(c=>(c.VehicleTypeId==item.VehicleTypeId) && (c.CompanyId==item.CompanyId) && (c.SubCompanyId==item.SubCompanyId))==0)
                    {
                        data2.Add(new stp_GetFareListByVehicleResult { Id = item.Id, VehicleTypeId = item.VehicleTypeId, VehicleType = item.VehicleType, CompanyId = item.CompanyId, Company = item.Company, SubCompanyId = item.SubCompanyId, SubCompanyName = item.SubCompanyName });
                    }
                }
                data2= data2.OrderBy(c => c.SubCompanyName).ToList();
                grdLister.BeginUpdate();
                grdLister.RowCount = data2.Count;
                for (int i = 0; i < data2.Count; i++)
                {
                    grdLister.Rows[i].Cells["Id"].Value = data2[i].Id;
                    grdLister.Rows[i].Cells["CompanyId"].Value = data2[i].CompanyId;
                    grdLister.Rows[i].Cells["Company"].Value = data2[i].Company;
                    grdLister.Rows[i].Cells["VehicleType"].Value = data2[i].VehicleType;
                    grdLister.Rows[i].Cells["VehicleTypeId"].Value = data2[i].VehicleTypeId;
                    grdLister.Rows[i].Cells["SubCompanyId"].Value = data2[i].SubCompanyId;
                    grdLister.Rows[i].Cells["SubCompanyName"].Value = data2[i].SubCompanyName;
                }
                grdLister.EndUpdate();

               // var list=data1.Where(c=>c.CompanyId.)
                //grdLister.DataSource = data2.OrderBy(c=>c.SubCompanyName);


                //if (grdLister.Columns["ColDelete"] == null)
                //{
                //    if (this.CanDelete)
                //    {
                //        UI.GridFunctions.AddDeleteColumn(grdLister);
                //    }


                //}

                //if (this.CanDelete && grdLister.Columns["ColDelete"] != null)
                //    grdLister.Columns["ColDelete"].Width = 70;

                ////  grdLister.Columns["ColDelete"].Width = 70;
                //grdLister.Columns["VehicleType"].HeaderText = "Vehicle Type";
                //grdLister.Columns["VehicleType"].Width = 110;
                //grdLister.Columns["Company"].Width = 110;
                //grdLister.Columns["SubCompanyName"].Width = 120;
                //grdLister.Columns["VehicleTypeId"].IsVisible = false;
                //grdLister.Columns["CompanyId"].IsVisible = false;
                //grdLister.Columns["SubCompanyId"].IsVisible = false;
                //grdLister.Columns["Id"].IsVisible = false;
                //grdLister.Columns["SubCompanyName"].HeaderText = "Sub Company";

                //if (grdLister.Columns.Contains("btnDelete") == false)
                //{
                //    GridDeleteButton(grdLister);
                //}


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void FormatFaresPDAOtherDetailGrid()
        {
            try
            {
                grdPDAOtherCharges.AllowAddNewRow = false;
                // grdOtherCharges.AllowEditRow = false;
                // grdPDAOtherCharges.AutoCellFormatting = true;
                grdPDAOtherCharges.ShowGroupPanel = false;

                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.Name = COLS_OTHERDETAILS.ID;
                col.IsVisible = false;
                grdPDAOtherCharges.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.Name = COLS_OTHERDETAILS.FAREID;
                col.IsVisible = false;
                grdPDAOtherCharges.Columns.Add(col);



                GridViewDecimalColumn colDec = null;


                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "From Mile";
                colDec.Name = COLS_OTHERDETAILS.FROMMILE;
                colDec.Width = 240;
                colDec.DecimalPlaces = 2;
                colDec.ReadOnly = true;
                grdPDAOtherCharges.Columns.Add(colDec);

                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "To Mile";
                colDec.Name = COLS_OTHERDETAILS.TOMILE;
                colDec.Width = 240;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                grdPDAOtherCharges.Columns.Add(colDec);



                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Rate (£)";
                colDec.Width = 110;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                colDec.ThousandsSeparator = true;
                colDec.Name = COLS_OTHERDETAILS.FARE;
                grdPDAOtherCharges.Columns.Add(colDec);

                grdPDAOtherCharges.MasterTemplate.ShowRowHeaderColumn = false;


                GridViewCommandColumn cmdcol = new GridViewCommandColumn();

                cmdcol.Width = 80;
                cmdcol.Name = "ColEdit";
                cmdcol.UseDefaultText = true;
                cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
                cmdcol.DefaultText = "Edit";
                cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                grdPDAOtherCharges.Columns.Add(cmdcol);
                cmdcol = new GridViewCommandColumn();
                cmdcol.Width = 80;
                cmdcol.Name = "ColDelete";
                cmdcol.UseDefaultText = true;
                cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
                cmdcol.DefaultText = "Delete";
                cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                grdPDAOtherCharges.Columns.Add(cmdcol);

                //UI.GridFunctions.AddDeleteColumn(grdPDAOtherCharges);
                //grdPDAOtherCharges.Columns["ColDelete"].Width = 80;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void GridDeleteButton(RadGridView Grid)
        {
            GridViewCommandColumn cmdcol = new GridViewCommandColumn();
            cmdcol.Width = 60;
            cmdcol.Name = "ColDelete";
            cmdcol.UseDefaultText = true;
            cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdcol.DefaultText = "Delete";
            cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            Grid.Columns.Add(cmdcol);
        }
        public struct COLS_OTHERDETAILS
        {
            public static string ID = "ID";
            public static string FAREID = "FAREID";
            public static string VehicleTypeId = "VehicleTypeId";

            public static string FROMMILE = "FromMile";
            public static string TOMILE = "ToMile";


            public static string PEAKTIME = "Peak";
            public static string PEAKRATE = "PeakRate";

            public static string OFFPEAKTIME = "OffPeak";
            public static string OFFPEAKRATE = "OffPeakRate";

            public static string FROMENDTIME = "FromEndTime";
            public static string TILLENDTIME = "TillEndTime";
            public static string FARE = "Fare";

            public static string FROMSTARTTIME = "FromStartTime";
            public static string TILLSTARTTIME = "TillStartTime";

            //tariff
            public static string TARIFF = "TARIFF";



            public static string FromDay = "FromDay";
            public static string TillDay = "TillDay";
            public static string StartRate = "StartRate";
            public static string StartRateMile = "StartRateMile";
            public static string IsSpecialDay = "IsSpecialDay";
            public static string SpecialDayName = "SpecialDayName";
            public static string FromSpecialDate = "FromSpecialDate";
            public static string TillSpecialDate = "TillSpecialDate";
            public static string SubCompanyId = "SubCompanyId";
            public static string CompanyId = "CompanyId";
            public static string IsCompanyWise = "IsCompanyWise";

        }
        private void FormatFaresOtherDetailGrid()
        {
            try
            {
                grdOtherCharges.AllowAddNewRow = false;
                // grdOtherCharges.AllowEditRow = false;
                // grdOtherCharges.AutoCellFormatting = true;
                grdOtherCharges.ShowGroupPanel = false;
                grdOtherCharges.MasterTemplate.ShowRowHeaderColumn = false;

                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.Name = COLS_OTHERDETAILS.ID;
                col.IsVisible = false;
                grdOtherCharges.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.Name = COLS_OTHERDETAILS.CompanyId;
                col.IsVisible = false;
                grdOtherCharges.Columns.Add(col);
                col = new GridViewTextBoxColumn();
                col.Name = COLS_OTHERDETAILS.SubCompanyId;
                col.IsVisible = false;
                grdOtherCharges.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.Name = COLS_OTHERDETAILS.VehicleTypeId;
                col.IsVisible = false;
                grdOtherCharges.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.Name = COLS_OTHERDETAILS.TARIFF;
                col.HeaderText = "Tariff";
                col.Width = 80;
                col.ReadOnly = true;
                grdOtherCharges.Columns.Add(col);

                GridViewDecimalColumn colDec = null;



                GridViewDateTimeColumn colDtp = new GridViewDateTimeColumn();
                colDtp.Name = COLS_OTHERDETAILS.FROMSTARTTIME;
                colDtp.HeaderText = "From Time";
                colDtp.ReadOnly = true;
                //colDtp.IsVisible = false;
                colDtp.Width = 90;
                colDtp.CustomFormat = "HH:mm";
                colDtp.FormatString = "{0:HH:mm}";
                grdOtherCharges.Columns.Add(colDtp);


                colDtp = new GridViewDateTimeColumn();
                colDtp.Name = COLS_OTHERDETAILS.TILLSTARTTIME;
                //colDtp.IsVisible = false;
                colDtp.Width = 90;
                colDtp.ReadOnly = true;
                colDtp.HeaderText = "Till Time";
                colDtp.CustomFormat = "HH:mm";
                colDtp.FormatString = "{0:HH:mm}";
                grdOtherCharges.Columns.Add(colDtp);



                col = new GridViewTextBoxColumn();
                col.Name = COLS_OTHERDETAILS.FromDay;
                col.IsVisible = false;
                grdOtherCharges.Columns.Add(col);
                col = new GridViewTextBoxColumn();
                col.Name = COLS_OTHERDETAILS.TillDay;
                col.IsVisible = false;
                grdOtherCharges.Columns.Add(col);

                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Start Rate Mile";
                colDec.Width = 120;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                colDec.ThousandsSeparator = true;
                colDec.Name = COLS_OTHERDETAILS.StartRateMile;
                grdOtherCharges.Columns.Add(colDec);

                colDec = new GridViewDecimalColumn();
                colDec.HeaderText = "Start Rate(£)";
                colDec.Width = 100;
                colDec.ReadOnly = true;
                colDec.DecimalPlaces = 2;
                colDec.ThousandsSeparator = true;
                colDec.Name = COLS_OTHERDETAILS.StartRate;
                grdOtherCharges.Columns.Add(colDec);

                col = new GridViewTextBoxColumn();
                col.Name = COLS_OTHERDETAILS.SpecialDayName;
                col.HeaderText = "Special Day";
                col.ReadOnly = true;
                col.Width = 120;

                grdOtherCharges.Columns.Add(col);



                colDtp = new GridViewDateTimeColumn();

                colDtp.Name = COLS_OTHERDETAILS.FromSpecialDate;
                colDtp.IsVisible = false;
                colDtp.HeaderText = "From Special Date";
                colDtp.Width = 110;
                colDtp.ReadOnly = true;
                colDtp.CustomFormat = "dd/MM/yyyy HH:mm";
                colDtp.FormatString = "{0:dd/MM/yyyy HH:mm}";
                grdOtherCharges.Columns.Add(colDtp);

                colDtp = new GridViewDateTimeColumn();
                colDtp.Name = COLS_OTHERDETAILS.TillSpecialDate;
                colDtp.IsVisible = false;
                //dd/MM/yyyy HH:mm tt
                colDtp.HeaderText = "Till Special Date";
                colDtp.Width = 110;
                colDtp.ReadOnly = true;
                colDtp.CustomFormat = "dd/MM/yyyy HH:mm";
                colDtp.FormatString = "{0:dd/MM/yyyy HH:mm}";
                grdOtherCharges.Columns.Add(colDtp);

                GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
                cbcol.Name = COLS_OTHERDETAILS.IsSpecialDay;
                cbcol.IsVisible = false;
                grdOtherCharges.Columns.Add(cbcol);
                cbcol = new GridViewCheckBoxColumn();
                cbcol.Name = COLS_OTHERDETAILS.IsCompanyWise;
                cbcol.IsVisible = false;
                grdOtherCharges.Columns.Add(cbcol);





                GridViewCommandColumn cmdcol = new GridViewCommandColumn();
                cmdcol.Width = 80;
                cmdcol.Name = "ColEdit";
                cmdcol.UseDefaultText = true;
                cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
                cmdcol.DefaultText = "Edit";
                cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                grdOtherCharges.Columns.Add(cmdcol);
                cmdcol = new GridViewCommandColumn();
                cmdcol.Width = 80;
                cmdcol.Name = "ColDelete";
                cmdcol.UseDefaultText = true;
                cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
                cmdcol.DefaultText = "Delete";
                cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                grdOtherCharges.Columns.Add(cmdcol);


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        public struct COLS_VEH
        {

            public static string Id = "Id";
            public static string VehicleType = "VehicleType";

        }
        private void FormatListerGrid()
        {
            grdLister.AllowAddNewRow = false;
            // grdOtherCharges.AllowEditRow = false;
            //grdVehicleTypes.AutoCellFormatting = true;
            grdLister.ShowGroupPanel = false;
            grdLister.TableElement.RowHeight = 30;
            grdLister.MasterTemplate.ShowRowHeaderColumn = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = "Id";
            col.IsVisible = false;
            grdLister.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = "VehicleTypeId";
            col.IsVisible = false;
            grdLister.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = "CompanyId";
            col.IsVisible = false;
            grdLister.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = "SubCompanyId";
            col.IsVisible = false;
            grdLister.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = "VehicleType";
            col.HeaderText = "Vehicle Type";
            col.Width = 110;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "Company";
            col.HeaderText = "Company";
            col.Width = 110;
            col.IsVisible = true;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = "SubCompanyName";
            col.HeaderText = "Sub Company";
            col.Width = 140;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);

            GridDeleteButton(grdLister);

            //grdLister.Columns["VehicleType"].HeaderText = "Vehicle Type";
            //grdLister.Columns["VehicleType"].Width = 110;
            //grdLister.Columns["Company"].Width = 110;
            //grdLister.Columns["SubCompanyName"].Width = 120;
            //grdLister.Columns["VehicleTypeId"].IsVisible = false;
            //grdLister.Columns["CompanyId"].IsVisible = false;
            //grdLister.Columns["SubCompanyId"].IsVisible = false;
            //grdLister.Columns["Id"].IsVisible = false;
            //grdLister.Columns["SubCompanyName"].HeaderText = "Sub Company";


        }
        private void DisplayFareDetails(List<Fare> obj)
        {
            if (obj.Count == 0) return;
            grdOtherCharges.Rows.Clear();
            grdOtherCharges.RowCount = obj.Count;

            for (int i = 0; i < grdOtherCharges.RowCount; i++)
            {
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.ID].Value = obj[i].Id;
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.VehicleTypeId].Value = obj[i].VehicleTypeId;

                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.StartRate].Value = obj[i].StartRate.ToDecimal();
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.StartRateMile].Value = obj[i].StartRateValidMiles.ToDecimal();

                DateTime? fromStartTime = obj[i].FromDateTime;
                DateTime? tillStartTime = obj[i].TillDateTime;

                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FromDay].Value = obj[i].FromDayName;
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TillDay].Value = obj[i].TillDayName;

                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value = fromStartTime;
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value = tillStartTime;

                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.IsSpecialDay].Value = obj[i].IsDayWise;
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.SpecialDayName].Value = obj[i].SpecialDayName;
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FromSpecialDate].Value = obj[i].FromSpecialDate;
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TillSpecialDate].Value = obj[i].TillSpecialDate;
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TARIFF].Value = "Tariff " + (i + 1);
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.SubCompanyId].Value = obj[i].SubCompanyId;
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.CompanyId].Value = obj[i].CompanyId;
                grdOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.IsCompanyWise].Value = obj[i].IsCompanyWise;
            }
        }

        private void DisplayFarePDAOtherDetails(Fare obj)
        {
            if (obj == null) return;

            grdPDAOtherCharges.Rows.Clear();
            grdPDAOtherCharges.RowCount = obj.Fare_PDAMeters.Count;

            for (int i = 0; i < grdPDAOtherCharges.RowCount; i++)
            {
                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.ID].Value = obj.Fare_PDAMeters[i].Id;
                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FAREID].Value = obj.Fare_PDAMeters[i].FareId;

                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FROMMILE].Value = obj.Fare_PDAMeters[i].FromMile.ToDecimal();
                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.TOMILE].Value = obj.Fare_PDAMeters[i].ToMile.ToDecimal();

                grdPDAOtherCharges.Rows[i].Cells[COLS_OTHERDETAILS.FARE].Value = obj.Fare_PDAMeters[i].Rate;

            }
            if (grdOtherCharges.CurrentRow != null)
            {
                lblTariff.Text = grdOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.TARIFF].Value.ToStr() + " Mileage";
            }
            if (grdPDAOtherCharges.RowCount > 0)
            {
                IsTariffDetailExist = true;
            }
            else
            {
                IsTariffDetailExist = false;
            }
            ClearPDAOtherChargesDetails();
        }
        private void ClearPDAOtherChargesDetails()
        {
            numpdafrommile.Value = numpdatomile.Value;
            numpdatomile.Value = 0;
            numPDARates_OtherCharges.Value = 0;
            grdPDAOtherCharges.CurrentRow = null;
            numpdafrommile.Focus();
        }

        private void AddFaresDetail()
        {
            try
            {
                decimal StartRate = numStartRate.Value.ToDecimal();
                decimal StartRateMile = numStartRateMile.Value.ToDecimal();
                string msg = string.Empty;
                string DayValue = string.Empty;

                TimeSpan FromStartTime = TimeSpan.Zero;
                TimeSpan TillStartTime = TimeSpan.Zero;
                TimeSpan FromEndTime = TimeSpan.Zero;
                TimeSpan TillEndTime = TimeSpan.Zero;

                string FromDay = ddlFromDay.Text.Trim();
                string TillDay = ddlTillDay.Text.Trim();
                DateTime? fromStartTime = dtpFromStartTime.Value;
                DateTime? tillStartTime = dtpTillStartTime.Value;

                DateTime? FromSpecialTime = dtpFromStartTime.Value;
                DateTime? TillSpecialTime = dtpTillStartTime.Value;

                string SpecialDayName = txtSpecialDayName.Text.Trim();
                DateTime? FromSpecialDate = dtpFromSpecialDate.Value.ToDateorNull();
                DateTime? TillSpecialDate = dtpTillSpecialDate.Value.ToDateorNull();
                FromSpecialTime = dtpFromSpecialTime.Value;
                TillSpecialTime = dtpTillSpecialTime.Value;
                int SubCompanyId = ddlSubCompanyId.SelectedValue.ToInt();

                int VehicleTypeId = 0;

                if (grdOtherCharges.RowCount > 0 && grdOtherCharges.CurrentRow != null && grdOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.VehicleTypeId].Value.ToInt() > 0)
                {
                    VehicleTypeId = grdOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.VehicleTypeId].Value.ToInt();
                }
                else
                {
                    objMaster.Clear();
                    if (grdOtherCharges.CurrentRow != null)
                    {
                        grdOtherCharges.CurrentRow = null;
                    }
                    if (grdLister.CurrentRow != null)
                    {
                        VehicleTypeId = grdLister.CurrentRow.Cells[COLS_VEH.Id].Value.ToInt();
                    }
                    else
                    {
                        msg += "Required : Vehicle Type" + Environment.NewLine;
                    }
                }


                bool IsSpecialDay = chkSpecialDay.Checked;
                if (IsSpecialDay == false)
                {

                    if (string.IsNullOrEmpty(FromDay))
                    {
                        msg += "Required : From Day" + Environment.NewLine;
                    }
                    if (string.IsNullOrEmpty(TillDay))
                    {
                        msg += "Required : Till Day" + Environment.NewLine;
                    }
                    if (fromStartTime == null)
                    {
                        msg += "Required : From Start Time" + Environment.NewLine;
                    }

                    if (tillStartTime == null)
                    {
                        msg += "Required : Till Start Time" + Environment.NewLine;
                    }
                    if (StartRate == 0)
                    {
                        msg += "Required : Start Rate." + Environment.NewLine;
                    }
                    if (StartRateMile == 0)
                    {
                        msg += "Required : Start Rate Mile." + Environment.NewLine;
                    }
                    SpecialDayName = "";
                    FromSpecialDate = null;
                    TillSpecialDate = null;

                    int FromDayId = 0;
                    int TillDayId = 0;
                    if (FromDay == "Monday")
                    {
                        FromDayId = 2;
                    }
                    else if (FromDay == "Tuesday")
                    {
                        FromDayId = 3;
                    }
                    else if (FromDay == "Wednesday")
                    {
                        FromDayId = 4;
                    }
                    else if (FromDay == "Thursday")
                    {
                        FromDayId = 5;
                    }
                    else if (FromDay == "Friday")
                    {
                        FromDayId = 6;
                    }
                    else if (FromDay == "Saturday")
                    {
                        FromDayId = 7;
                    }
                    else if (FromDay == "Sunday")
                    {
                        FromDayId = 1;
                    }

                    if (TillDay == "Monday")
                    {
                        TillDayId = 2;
                    }
                    else if (TillDay == "Tuesday")
                    {
                        TillDayId = 3;
                    }
                    else if (TillDay == "Wednesday")
                    {
                        TillDayId = 4;
                    }
                    else if (TillDay == "Thursday")
                    {
                        TillDayId = 5;
                    }
                    else if (TillDay == "Friday")
                    {
                        TillDayId = 6;
                    }
                    else if (TillDay == "Saturday")
                    {
                        TillDayId = 7;
                    }
                    else if (TillDay == "Sunday")
                    {
                        TillDayId = 1;
                    }

                    if (FromDayId > TillDayId)
                    {
                        for (int i = FromDayId; i >= TillDayId; i++)
                        {
                            if (string.IsNullOrEmpty(DayValue))
                            {
                                DayValue = i.ToStr();
                            }
                            else
                            {
                                DayValue += "," + i.ToStr();

                            }
                            if (i == 7)
                            {
                                i = 0;
                                for (int j = 1; j <= TillDayId; j++)
                                {
                                    if (string.IsNullOrEmpty(DayValue))
                                    {
                                        DayValue = j.ToStr();
                                    }
                                    else
                                    {
                                        DayValue += "," + j.ToStr();

                                    }
                                }
                                break;
                            }

                        }
                    }
                    else if (FromDayId == TillDayId)
                    {
                        DayValue = FromDayId.ToStr();
                    }
                    else
                    {
                        for (int i = FromDayId; i <= TillDayId; i++)
                        {
                            if (string.IsNullOrEmpty(DayValue))
                            {
                                DayValue = i.ToStr();
                            }
                            else
                            {
                                DayValue += "," + i.ToStr();
                            }
                        }
                    }

                }
                else
                {
                    if (string.IsNullOrEmpty(SpecialDayName))
                    {
                        msg += "Required : Special Day" + Environment.NewLine;
                    }
                    if (FromSpecialDate == null)
                    {
                        msg += "Required : From Date" + Environment.NewLine;
                    }

                    if (TillSpecialDate == null)
                    {
                        msg += "Required : Till Date" + Environment.NewLine;
                    }
                    if (StartRate == 0)
                    {
                        msg += "Required : Start Rate." + Environment.NewLine;
                    }
                    if (StartRateMile == 0)
                    {
                        msg += "Required : Start Rate Mile." + Environment.NewLine;
                    }
                    FromStartTime = TimeSpan.Zero;
                    TillStartTime = TimeSpan.Zero;
                    FromDay = "";
                    TillDay = "";
                    DayValue = string.Empty;
                }
                if (SubCompanyId == 0)
                {
                    msg += "Required : Sub Company.";
                }
                if (!string.IsNullOrEmpty(msg))
                {
                    ENUtils.ShowMessage(msg);
                    return;
                }


                int Id = 0;
                try
                {

                    if (grdOtherCharges.CurrentRow != null)
                    {
                        Id = grdOtherCharges.CurrentRow.Cells[COLS_OTHERDETAILS.ID].Value.ToInt();
                    }
                    if (Id > 0)
                    {
                        objMaster.GetByPrimaryKey(Id);
                    }
                    if (objMaster.PrimaryKeyValue == null)
                    {
                        objMaster.New();
                        objMaster.Current.AddBy = AppVars.LoginObj.LuserId.ToInt();
                        objMaster.Current.AddOn = DateTime.Now;
                    }
                    else
                    {
                        objMaster.Edit();
                    }
                    objMaster.SaveWithoutValidatingVehicleType = true;
                    objMaster.Current.VehicleTypeId = VehicleTypeId;
                    DateTime dateValue = new DateTime(1900, 1, 1, 0, 0, 0);

                    objMaster.Current.FromDayName = FromDay;
                    objMaster.Current.TillDayName = TillDay;

                    objMaster.Current.FromDateTime = fromStartTime != null ? dateValue.ToDate() + fromStartTime.Value.TimeOfDay : dateValue;
                    objMaster.Current.TillDateTime = tillStartTime != null ? dateValue.ToDate() + tillStartTime.Value.TimeOfDay : dateValue;

                    objMaster.Current.StartRate = StartRate;
                    objMaster.Current.StartRateValidMiles = StartRateMile;

                    objMaster.Current.IsCompanyWise = chkCompanyWise.Checked;
                    objMaster.Current.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
                    objMaster.Current.SubCompanyId = ddlSubCompanyId.SelectedValue.ToIntorNull();
                    objMaster.Current.DayValue = DayValue;
                    objMaster.Current.SpecialDayName = SpecialDayName;

                    objMaster.Current.IsDayWise = IsSpecialDay;//item.Cells[COLS_OTHERDETAILS.IsSpecialDay].Value.ToBool();
                    objMaster.Current.FromSpecialDate = dtpFromSpecialTime.Value != null ? FromSpecialDate.ToDateorNull() + dtpFromSpecialTime.Value.ToDateTimeorNull().Value.TimeOfDay : FromSpecialDate.ToDateorNull();
                    objMaster.Current.TillSpecialDate = dtpTillSpecialTime.Value != null ? TillSpecialDate.ToDateorNull() + dtpTillSpecialTime.Value.ToDateTimeorNull().Value.TimeOfDay : TillSpecialDate.ToDateorNull();
                    objMaster.Save();
                    objMaster.Clear();


                }
                catch (Exception exc)
                {
                    if (objMaster.Errors.Count > 0)
                        ENUtils.ShowMessage(objMaster.ShowErrors());
                    else
                    {
                        ENUtils.ShowMessage(exc.Message);
                    }
                }

                GridViewRowInfo row;
                if (grdOtherCharges.CurrentRow != null)
                    row = grdOtherCharges.CurrentRow;
                else
                    row = grdOtherCharges.Rows.AddNew();


                row.Cells[COLS_OTHERDETAILS.ID].Value = objMaster.Current.Id;
                row.Cells[COLS_OTHERDETAILS.VehicleTypeId].Value = VehicleTypeId;
                row.Cells[COLS_OTHERDETAILS.StartRate].Value = StartRate;
                row.Cells[COLS_OTHERDETAILS.StartRateMile].Value = StartRateMile;

                row.Cells[COLS_OTHERDETAILS.FromDay].Value = FromDay;
                row.Cells[COLS_OTHERDETAILS.TillDay].Value = TillDay;

                //
                row.Cells[COLS_OTHERDETAILS.CompanyId].Value = ddlCompany.SelectedValue.ToIntorNull();
                row.Cells[COLS_OTHERDETAILS.SubCompanyId].Value = ddlSubCompanyId.SelectedValue.ToIntorNull();
                row.Cells[COLS_OTHERDETAILS.IsCompanyWise].Value = chkCompanyWise.Checked;

                row.Cells[COLS_OTHERDETAILS.FROMSTARTTIME].Value = fromStartTime;
                row.Cells[COLS_OTHERDETAILS.TILLSTARTTIME].Value = tillStartTime;
                row.Cells[COLS_OTHERDETAILS.IsSpecialDay].Value = IsSpecialDay;
                row.Cells[COLS_OTHERDETAILS.SpecialDayName].Value = SpecialDayName;
                row.Cells[COLS_OTHERDETAILS.FromSpecialDate].Value = dtpFromSpecialTime.Value != null ? FromSpecialDate.ToDateorNull() + dtpFromSpecialTime.Value.ToDateTimeorNull().Value.TimeOfDay : FromSpecialDate.ToDateorNull();
                row.Cells[COLS_OTHERDETAILS.TillSpecialDate].Value = dtpTillSpecialTime.Value != null ? TillSpecialDate.ToDateorNull() + dtpTillSpecialTime.Value.ToDateTimeorNull().Value.TimeOfDay : TillSpecialDate.ToDateorNull();
                row.IsCurrent = true;

                int Index = row.Index;
                string Tariff = "Tariff " + (Index + 1);
                row.Cells[COLS_OTHERDETAILS.TARIFF].Value = Tariff;
                lblTariff.Text = Tariff + " Mileage";
                if (grdPDAOtherCharges.RowCount > 0 && grdPDAOtherCharges.CurrentRow != null)
                {
                    IsTariffDetailExist = true;
                }
                else
                {
                    IsTariffDetailExist = false;
                }
                Fare obj = objMaster.Current;
                DisplayFarePDAOtherDetails(obj);
                ddlFromDay.Focus();


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        private void ClearFaresDetail()
        {

            numStartRate.Value = 0;
            numStartRateMile.Value = 0;


            ddlFromDay.Text = "";
            ddlTillDay.Text = "";

            ddlFromDay.NullText = "Select";
            ddlTillDay.NullText = "Select";
            DateTime? fromStartTime = DateTime.Now.ToDateorNull();
            dtpFromStartTime.Value = fromStartTime;
            dtpTillStartTime.Value = fromStartTime;
            txtSpecialDayName.Text = "";
            dtpTillSpecialDate.Value = null;
            dtpFromSpecialDate.Value = null;

            dtpTillSpecialTime.Value = null;
            dtpFromSpecialTime.Value = null;
            numStartRate.Focus();
            objMaster.Clear();
            chkSpecialDay.Checked = false;
            txtSpecialDayName.Visible = false;
            lblFromSpecialDateTime.Visible = false;
            lblTillSpecialDateTime.Visible = false;
            lblFromSpecialTime.Visible = false;
            lblTillSpecialTime.Visible = false;
            dtpFromSpecialDate.Visible = false;
            dtpTillSpecialDate.Visible = false;
            dtpFromSpecialTime.Visible = false;
            dtpTillSpecialTime.Visible = false;
            ddlFromDay.Focus();
        }


        public void DisplayFares(int VehicleId, int SubCompanyId, int? CompanyId)
        {
            try
            {
                if (IsLoaded == false)
                {
                    return;
                }
                var list = General.GetQueryable<Fare>(c => (c.VehicleTypeId == VehicleId) && (c.SubCompanyId == SubCompanyId) && ((CompanyId == null ||  c.CompanyId == CompanyId))).ToList();
                if (CompanyId == null)
                {
                    list = list.Where(c => c.CompanyId == null).ToList();
                }
                
                int FareId = 0;

                if (list.Count > 0)
                {
                    FareId = list.FirstOrDefault().Id;

                    DisplayFareDetails(list);
                    if (grdOtherCharges.RowCount > 0)
                    {
                        objMaster.GetByPrimaryKey(FareId);
                        Fare obj = objMaster.Current;
                        DisplayFarePDAOtherDetails(obj);
                        DisplayFareDetails(obj);
                        objMaster.Clear();

                    }
                }
                else
                {
                    objMaster.Clear();
                    grdOtherCharges.Rows.Clear();
                    ClearFaresDetail();
                    grdPDAOtherCharges.Rows.Clear();
                    grdDetails.Rows.Clear();
                    lblTariff.Text = "Tariff Mileage";
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        //Fixed Fares Code
        private void ApplyChangesOnFixedFares()
        {
            try
            {
                decimal percent = numPercent.Value.ToDecimal();

                if (grdDetails.RowCount > 0)
                { //COLS_DETAILS



                    if (chkApplyToAll.Checked.ToBool())
                    {

                        if (rbtnAdd.IsChecked)
                        {
                            for (int i = 0; i < grdDetails.RowCount; i++)
                            {

                                decimal Amount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() + Amount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grdDetails.RowCount; i++)
                            {
                                decimal Amount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() - Amount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                            }
                        }
                    }
                    else
                    {
                        if (rbtnAdd.IsChecked)
                        {
                            for (int i = 0; i < grdDetails.RowCount; i++)
                            {
                                if ((grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT
                                    || grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)
                                    ||
                                    (grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT
                                    || grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.UNDERGROUNDSTATION))
                                {

                                    continue;
                                }
                                else
                                {

                                    decimal Amount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                    grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() + Amount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grdDetails.RowCount; i++)
                            {
                                if ((grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT
                                    || grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.UNDERGROUNDSTATION)
                                ||
                                    (grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT
                                    || grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == Enums.LOCATION_TYPES.UNDERGROUNDSTATION))
                                {

                                    continue;
                                }
                                else
                                {
                                    decimal Amount = ((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                    grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value = (grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() - Amount); //((grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value.ToDecimal() * percent.ToDecimal()) / 100);
                                }
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
        public struct COLS_DETAILS
        {
            public static string ID = "ID";
            public static string FAREID = "FAREID";
            public static string FROMLOCTYPEID = "FROMLOCTYPEID";
            public static string TOLOCTYPEID = "TOLOCTYPEID";

            public static string FROMLOCATIONID = "FROMLOCATIONID";
            public static string TOLOCATIONID = "TOLOCATIONID";

            public static string FROMLOCATION = "FromLocation";
            public static string TOLOCATION = "ToLocation";
            public static string FARE = "Fare";
            public static string COMPANYFARE = "CompanyFare";

        }
        private void FormatFaresDetailGrid()
        {
            grdDetails.AllowAddNewRow = false;
            //   grdDetails.AllowEditRow = false;
            grdDetails.AutoCellFormatting = true;
            grdDetails.ShowGroupPanel = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.ID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.FAREID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.FROMLOCTYPEID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.TOLOCTYPEID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.FROMLOCATIONID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS_DETAILS.TOLOCATIONID;
            col.IsVisible = false;
            grdDetails.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "From Location";
            col.Name = COLS_DETAILS.FROMLOCATION;
            col.Width = 250;
            col.ReadOnly = true;
            grdDetails.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "To Location";
            col.ReadOnly = true;
            col.Name = COLS_DETAILS.TOLOCATION;
            col.Width = 250;
            grdDetails.Columns.Add(col);



            GridViewDecimalColumn colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Fare (£)";
            colDec.Width = 110;
            colDec.ReadOnly = false;
            colDec.DecimalPlaces = 2;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS_DETAILS.FARE;
            grdDetails.Columns.Add(colDec);


            colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "A/C Fare (£)";
            colDec.Width = 110;
            colDec.ReadOnly = false;
            colDec.DecimalPlaces = 2;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS_DETAILS.COMPANYFARE;
            grdDetails.Columns.Add(colDec);

            grdDetails.MasterTemplate.ShowRowHeaderColumn = false;


            UI.GridFunctions.AddDeleteColumn(grdDetails);
            grdDetails.Columns["ColDelete"].Width = 80;
            UI.GridFunctions.SetFilter(grdDetails);

            colDec.ReadOnly = false;
            grdDetails.AllowEditRow = true;
        }

        private void ClearFromLocation()
        {
            ddlFromLocation.SelectedValue = null;

        }


        private void ClearToLocation()
        {
            ddlToLocation.SelectedValue = null;


        }
        private void FillLocationTypesCombo()
        {
            ComboFunctions.FillLocationTypeCombo(ddlFromLocType);
            ComboFunctions.FillLocationTypeCombo(ddlToLocType);
        }

        private void ClearFareDetails()
        {
            ClearFromLocation();
            ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.POSTCODE;

            ClearToLocation();
            ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.POSTCODE;
            numRate_FareCharges.Value = 0;
            ddlFromLocation.Focus();
            grdDetails.CurrentRow = null;



            ddlFromLocation.Tag = null;
            ddlToLocation.Tag = null;

            lstFromLocation.Items.Clear();
            lstToLocation.Items.Clear();

            txtFromAddress.Text = string.Empty;
            txtToAddress.Text = string.Empty;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if (ddlVehicleType.SelectedValue == null)
            if (grdLister.CurrentRow == null)
            {
                ENUtils.ShowMessage("Required : Vehicle");
                return;

            }
            AddDetail();
        }

        private void AddDetail()
        {

            decimal fares = numRate_FareCharges.Value.ToDecimal();
            decimal companyfares = numCompanyRate_FareCharges.Value.ToDecimal();
            int? fromLocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
            int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            int? fromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
            int? toLocId = ddlToLocation.SelectedValue.ToIntorNull();


            string fromLocation = ddlFromLocation.Text.ToStr();
            string toLocation = ddlToLocation.Text.ToStr();
            string fromLocType = ddlFromLocType.Text.ToStr();
            string toLocType = ddlToLocType.Text.ToStr();
            string fromAddress = txtFromAddress.Text.ToStr().Trim().ToUpper();
            string toAddress = txtToAddress.Text.ToStr().Trim().ToUpper();


            if (fromLocTypeId == 100 && toLocTypeId == 100)
            {
                ENUtils.ShowMessage("Please enter Plot to NonPlot or NonPlot to Plot Fares");
                return;

            }

            string msg = string.Empty;


            string fromPostCode = "";
            string toPostCode = "";


            if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
            {
                GridViewRowInfo row;

                if (grdDetails.CurrentRow != null)
                    row = grdDetails.CurrentRow;
                else
                    row = grdDetails.Rows.AddNew();

                if (fromLocTypeId != Enums.LOCATION_TYPES.ADDRESS)
                    fromAddress = fromLocation;

                if (toLocTypeId != Enums.LOCATION_TYPES.ADDRESS)
                    toAddress = toLocation;

                row.Cells[COLS_DETAILS.FROMLOCATION].Value = fromAddress;
                row.Cells[COLS_DETAILS.TOLOCATION].Value = toAddress;
                row.Cells[COLS_DETAILS.FROMLOCATIONID].Value = fromLocId;
                row.Cells[COLS_DETAILS.TOLOCATIONID].Value = toLocId;

                row.Cells[COLS_DETAILS.FROMLOCTYPEID].Value = fromLocTypeId;
                row.Cells[COLS_DETAILS.TOLOCTYPEID].Value = toLocTypeId;
                row.Cells[COLS_DETAILS.FARE].Value = fares;
                row.Cells[COLS_DETAILS.COMPANYFARE].Value = companyfares;
            }

            else
            {




                if (lstFromLocation.Items.Count == 0)
                {


                    if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        if (fromLocId == null)
                        {
                            fromPostCode = General.GetPostCodeMatch(fromLocation);
                            if (!string.IsNullOrEmpty(fromPostCode) && fromPostCode.IsAlpha() == false)
                            {
                                AddLocation(fromLocation, ref fromLocId, ref fromPostCode);
                                FillFromLocations();
                                ddlFromLocation.SelectedValue = fromLocId;
                            }
                        }
                    }

                    if (fromLocId == null)
                    {
                        msg += "Required : From Location." + Environment.NewLine;

                    }


                    if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        if (toLocId == null)
                        {
                            toPostCode = General.GetPostCodeMatch(toLocation);
                            if (!string.IsNullOrEmpty(toPostCode) && toPostCode.IsAlpha() == false)
                            {
                                AddLocation(toLocation, ref toLocId, ref toPostCode);
                                FillToLocations();
                                ddlToLocation.SelectedValue = toLocId;
                            }
                        }
                    }

                    if (toLocId == null)
                    {
                        msg += "Required : To Location." + Environment.NewLine;
                    }

                    if (fares == 0)
                    {
                        msg += "Required : Fare rate.";
                    }

                    if (!string.IsNullOrEmpty(msg))
                    {
                        ENUtils.ShowMessage(msg);
                        return;

                    }


                    GridViewRowInfo row;
                    if (grdDetails.CurrentRow == null &&
                        grdDetails.Rows.Count(c =>
                                                c.Cells[COLS_DETAILS.FROMLOCATIONID].Value.ToInt() == fromLocId
                                            && c.Cells[COLS_DETAILS.TOLOCATIONID].Value.ToInt() == toLocId
                                             ) > 0)
                    {
                        ENUtils.ShowMessage("From Location and To Location already exist");
                        ddlFromLocation.Focus();
                        return;

                    }


                    if (grdDetails.CurrentRow != null)
                        row = grdDetails.CurrentRow;
                    else
                        row = grdDetails.Rows.AddNew();

                    row.Cells[COLS_DETAILS.FROMLOCATION].Value = fromLocation;
                    row.Cells[COLS_DETAILS.TOLOCATION].Value = toLocation;
                    row.Cells[COLS_DETAILS.FROMLOCATIONID].Value = fromLocId;
                    row.Cells[COLS_DETAILS.TOLOCATIONID].Value = toLocId;

                    row.Cells[COLS_DETAILS.FROMLOCTYPEID].Value = fromLocTypeId;
                    row.Cells[COLS_DETAILS.TOLOCTYPEID].Value = toLocTypeId;
                    row.Cells[COLS_DETAILS.FARE].Value = fares;
                    row.Cells[COLS_DETAILS.COMPANYFARE].Value = companyfares;

                }
                else
                {

                    if (lstToLocation.Items.Count == 0)
                    {
                        ENUtils.ShowMessage("Required : To Location" + Environment.NewLine + "To Location Box cannot be Empty");
                        return;
                    }


                    if (fares == 0)
                    {
                        ENUtils.ShowMessage("Required : Fare Rate");
                        return;
                    }




                    GridViewRowInfo detailRow = null;
                    foreach (RadListDataItem locFrom in lstFromLocation.Items)
                    {

                        fromLocTypeId = locFrom.Value.ToStr().Substring(locFrom.Value.ToStr().IndexOf(',') + 1).ToIntorNull();
                        fromLocId = locFrom.Value.ToStr().Remove(locFrom.Value.ToStr().IndexOf(',')).ToIntorNull();
                        fromLocation = locFrom.Text.ToStr();

                        foreach (RadListDataItem locTo in lstToLocation.Items)
                        {
                            detailRow = grdDetails.Rows.AddNew();

                            detailRow.Cells[COLS_DETAILS.FROMLOCATION].Value = fromLocation;
                            detailRow.Cells[COLS_DETAILS.FROMLOCATIONID].Value = fromLocId;
                            detailRow.Cells[COLS_DETAILS.FROMLOCTYPEID].Value = fromLocTypeId;


                            detailRow.Cells[COLS_DETAILS.TOLOCATION].Value = locTo.Text.ToStr();
                            detailRow.Cells[COLS_DETAILS.TOLOCATIONID].Value = locTo.Value.ToStr().Remove(locTo.Value.ToStr().IndexOf(',')).ToIntorNull();
                            detailRow.Cells[COLS_DETAILS.TOLOCTYPEID].Value = locTo.Value.ToStr().Substring(locTo.Value.ToStr().IndexOf(',') + 1).ToIntorNull();

                            detailRow.Cells[COLS_DETAILS.FARE].Value = fares;
                            detailRow.Cells[COLS_DETAILS.COMPANYFARE].Value = companyfares;
                        }
                    }




                }
            }

            ClearFareDetails();

        }


        private void AddLocation(string postCode, ref int? locId, ref string postCodeValue)
        {
            LocationBO loc = new LocationBO();
            try
            {

                loc.New();
                loc.Current.LocationName = postCode;
                loc.Current.PostCode = postCode;
                //    loc.Current.LocationName = "";
                loc.Current.LocationTypeId = Enums.LOCATION_TYPES.POSTCODE;
                loc.Save();



                locId = loc.Current.Id.ToIntorNull();
                postCodeValue = loc.Current.PostCode;

            }
            catch (Exception ex)
            {
                if (loc.Errors.Count > 0)
                    ENUtils.ShowMessage(loc.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFareDetails();
        }

        private void grdDetails_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdDetails.CurrentRow != null && grdDetails.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdDetails.CurrentRow;

                ddlFromLocType.SelectedValue = row.Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt();
                ddlToLocType.SelectedValue = row.Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt();
                ddlFromLocation.SelectedValue = row.Cells[COLS_DETAILS.FROMLOCATIONID].Value.ToInt();
                ddlToLocation.SelectedValue = row.Cells[COLS_DETAILS.TOLOCATIONID].Value.ToInt();




                this.txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = row.Cells[COLS_DETAILS.FROMLOCATION].Value.ToStr();
                this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                this.txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = row.Cells[COLS_DETAILS.TOLOCATION].Value.ToStr();
                this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                numRate_FareCharges.Value = row.Cells[COLS_DETAILS.FARE].Value.ToDecimal();
                numCompanyRate_FareCharges.Value = row.Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal();

            }
        }
        AutoCompleteTextBox aTxt = null;
        string[] res = null;
        string searchTxt = "";

        void TextBoxElement_TextChanged(object sender, EventArgs e)
        {


            try
            {

                // IsKeyword = false;

                InitializeTimer();
                timer1.Stop();

                aTxt = (AutoCompleteTextBox)sender;
                aTxt.ResetListBox();

                //if (aTxt.Name == "txtFromAddress")
                //    txtToAddress.SendToBack();

                //else if (aTxt.Name == "txtToAddress")
                //    txtToAddress.BringToFront();



                if (AppVars.objPolicyConfiguration.EnablePOI.ToBool())
                {

                    InitializeSearchPOIWorker();

                    if (POIWorker.IsBusy)
                    {
                        POIWorker.CancelAsync();

                        POIWorker.Dispose();
                        POIWorker = null;
                        GC.Collect();
                        InitializeSearchPOIWorker();

                    }


                    AddressTextChangePOI();
                }
                else
                {

                    AddressTextChangeWOPOI();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void FillFromLocations()
        {
            int locTypeId = ddlFromLocType.SelectedValue.ToInt();
            if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                ComboFunctions.FillPostCodeLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);

                txtFromAddress.Visible = false;
                ddlFromLocation.Visible = true;
                lstFromLocation.Visible = true;
                btnAddFromLoc.Visible = true;
                btnEditFromLoc.Visible = true;
                btnDeleteFromLst.Visible = true;
            }
            else if (locTypeId == Enums.LOCATION_TYPES.ADDRESS)
            {
                txtFromAddress.Visible = true;
                ddlFromLocation.Visible = false;
                lstFromLocation.Visible = false;
                btnAddFromLoc.Visible = false;
                btnEditFromLoc.Visible = false;
                btnDeleteFromLst.Visible = false;


            }
            //else if (ddlFromLocType.Text.Trim().ToLower() == "zone")
            //{

            //    ComboFunctions.FillZonesPlottedCombo(ddlFromLocation);


            //}
            else
            {
                txtFromAddress.Visible = false;
                ddlFromLocation.Visible = true;
                lstFromLocation.Visible = true;
                btnAddFromLoc.Visible = true;
                btnEditFromLoc.Visible = true;

                btnDeleteFromLst.Visible = true;

                if (locTypeId != 100)
                {
                    ComboFunctions.FillLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);
                }
                else
                {
                    ComboFunctions.FillZonesCombo(ddlFromLocation);

                }
            }
        }

        private void FillToLocations()
        {
            int locTypeId = ddlToLocType.SelectedValue.ToInt();
            if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                ComboFunctions.FillPostCodeLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);
                txtToAddress.Visible = false;
                ddlToLocation.Visible = true;
                lstToLocation.Visible = true;
                btnAddToLoc.Visible = true;
                btnEditToLoc.Visible = true;
                btnDeleteToLst.Visible = true;
                btnAddToLocation.Visible = true;
            }
            else if (locTypeId == Enums.LOCATION_TYPES.ADDRESS)
            {
                txtToAddress.Visible = true;
                ddlToLocation.Visible = false;
                lstToLocation.Visible = false;
                btnAddToLoc.Visible = false;
                btnEditToLoc.Visible = false;
                btnDeleteToLst.Visible = false;
                btnAddToLocation.Visible = false;

            }
            //else if (ddlToLocType.Text.Trim().ToLower() == "zone")
            //{

            //    ComboFunctions.FillZonesPlottedCombo(ddlToLocation);


            //}
            else
            {
                txtToAddress.Visible = false;
                ddlToLocation.Visible = true;
                lstToLocation.Visible = true;
                btnAddToLoc.Visible = true;
                btnEditToLoc.Visible = true;
                btnDeleteToLst.Visible = true;
                btnAddToLocation.Visible = true;


                if (locTypeId != 100)
                {
                    ComboFunctions.FillLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);
                }
                else
                {
                    ComboFunctions.FillZonesCombo(ddlToLocation);

                }


            }
        }
        private void CompanyWise(ToggleState args)
        {
            if (args == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlCompany.Enabled = true;
                //  pnlPercentage.Visible = false;
                chkApplyToAll.Checked = false;
                chkApplyToAll.Visible = false;



                //if (AppVars.objPolicyConfiguration.EnableZoneWiseFares.ToBool() == false)
                //{
                pnlStartRates.Visible = true;
                //  }
            }
            else
            {
                ddlCompany.Enabled = false;
                ddlCompany.SelectedValue = null;

                pnlPercentage.Visible = true;

                pnlStartRates.Visible = false;
                numStartRate.Value = 0.00m;
                numStartRateValidMiles.Value = 0.00m;
            }

        }
        #region TextChanged

        //void TextBoxElement_TextChanged(object sender, EventArgs e)
        //{


        //    try
        //    {

        //        // IsKeyword = false;

        //        InitializeTimer();
        //        timer1.Stop();

        //        aTxt = (AutoCompleteTextBox)sender;
        //        aTxt.ResetListBox();

        //        //if (aTxt.Name == "txtFromAddress")
        //        //    txtToAddress.SendToBack();

        //        //else if (aTxt.Name == "txtToAddress")
        //        //    txtToAddress.BringToFront();



        //        if (AppVars.objPolicyConfiguration.EnablePOI.ToBool())
        //        {

        //            InitializeSearchPOIWorker();

        //            if (POIWorker.IsBusy)
        //            {
        //                POIWorker.CancelAsync();

        //                POIWorker.Dispose();
        //                POIWorker = null;
        //                GC.Collect();
        //                InitializeSearchPOIWorker();

        //            }


        //            AddressTextChangePOI();
        //        }
        //        else
        //        {

        //            AddressTextChangeWOPOI();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        BackgroundWorker POIWorker = null;
        private void InitializeSearchPOIWorker()
        {
            if (POIWorker == null)
            {
                POIWorker = new BackgroundWorker();
                POIWorker.WorkerSupportsCancellation = true;
                POIWorker.DoWork += new DoWorkEventHandler(POIWorker_DoWork);
                POIWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(POIWorker_RunWorkerCompleted);
            }



        }

        void POIWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null || e.Cancelled || (sender as BackgroundWorker) == null)
                return;

            try
            {


                ShowAddressesPOI((string[])e.Result);

            }
            catch
            {


            }
        }

        void POIWorker_DoWork(object sender, DoWorkEventArgs e)
        {


            string searchValue = e.Argument.ToStr();
            try
            {
                if (POIWorker == null)
                {
                    e.Cancel = true;
                    return;


                }

                //   Console.WriteLine("Start work : " + searchValue);

                string postCode = General.GetPostCodeMatchOpt(searchValue);

                string doorNo = string.Empty;
                string place = string.Empty;

                if (!string.IsNullOrEmpty(postCode) && postCode.IsAlpha() == true)
                    postCode = string.Empty;

                string street = searchValue;

                if (postCode.Length > 0)
                {
                    street = street.Replace(postCode, "").Trim();
                }


                if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(postCode) && street.IsAlpha() == false && street.Length < 4 && searchValue.IndexOf(postCode) < searchValue.IndexOf(street))
                {
                    street = "";
                    postCode = searchTxt;
                }


                if (street.Length > 0)
                {

                    if (char.IsNumber(street[0]))
                    {

                        for (int i = 0; i <= 3; i++)
                        {
                            if (char.IsNumber(street[i]) || (doorNo.Length > 0 && doorNo.Length == i && char.IsLetter(street[i])))
                                doorNo += street[i];
                            else
                                break;
                        }
                    }
                }


                if (street.EndsWith("#"))
                {
                    street = street.Replace("#", "").Trim();
                    place = "p=";
                }

                if (doorNo.Length > 0 && place.Length == 0)
                {
                    street = street.Replace(doorNo, "").Trim();


                }


                if (postCode.Length == 0 && street.Length < 3)
                {
                    e.Cancel = true;
                    return;

                }


                if (street.Length > 1 || postCode.Length > 0)
                {
                    if (postCode.Length > 0)
                    {
                        if (doorNo.Length > 0 && postCode == General.GetPostCodeMatch(postCode))
                        {
                            doorNo = string.Empty;
                        }

                    }

                    if (postCode.Length >= 5 && postCode.Contains(" ") == false)
                    {
                        string resultPostCode = AppVars.listOfAddress.FirstOrDefault(a => a.PostalCode.Strip(' ') == postCode).DefaultIfEmpty().PostalCode.ToStr().Trim();


                        if (resultPostCode.Length >= 5 && resultPostCode.Contains(" "))
                        {
                            postCode = resultPostCode;

                        }

                    }


                    if (POIWorker == null || POIWorker.CancellationPending || ((sender as BackgroundWorker) == null || (sender as BackgroundWorker).CancellationPending))
                    {
                        e.Cancel = true;
                        return;
                    }



                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        e.Result = db.stp_GetByRoadLevelData(postCode, doorNo, street, place).Select(c => c.AddressLine1).ToArray<string>();

                    }

                    if (POIWorker == null || POIWorker.CancellationPending || ((sender as BackgroundWorker) == null || (sender as BackgroundWorker).CancellationPending))
                    {
                        e.Cancel = true;
                        return;
                    }




                    //   Console.WriteLine("end work : " + searchValue);

                }
            }
            catch
            {
                //     Console.WriteLine("Start work catch: " + searchValue);

            }
        }



        private void AddressTextChangeWOPOI()
        {
            string text = aTxt.Text;
            string doorNo = string.Empty;

            if (AppVars.objPolicyConfiguration.StripDoorNoOnAddress.ToBool())
            {
                if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToStr().ToLower() == aTxt.Text.ToLower())
                {
                    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                    aTxt.Text = aTxt.ListBoxElement.SelectedItem.ToStr().Trim().ToUpper().Trim();


                    if (aTxt.Text.Contains(".") && aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".") - 1]))
                    {

                        aTxt.Text = aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
                    }

                    aTxt.SelectedItem = aTxt.Text.Trim();
                    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                    //    }               

                }

            }

            if (text.Length > 2 && text.EndsWith(".") == false && text.EndsWith(",") == false)
            {

                if (aTxt.SelectedItem == null || (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() != aTxt.Text.ToLower()))
                {


                    for (int i = 0; i <= 2; i++)
                    {
                        if (char.IsNumber(text[i]))
                            doorNo += text[i];
                        else
                            break;

                    }
                    text = text.Remove(text.IndexOf(doorNo), doorNo.Length).TrimStart(new char[] { ' ' });
                }
            }



            if (text.Length > 1 && text != "BASX")
            {
                if (text.EndsWith("   "))
                {
                    //if (aTxt.Name == "txtFromAddress")
                    //{
                    //    FocusOnPickupPlot();
                    //}
                    //else if (aTxt.Name == "txtToAddress")
                    //{
                    //    FocusOnDropOffPlot();
                    //}

                    return;

                }

                else if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() == aTxt.Text.ToLower())
                {
                    aTxt.ListBoxElement.Items.Clear();

                    aTxt.ResetListBox();

                    string locName = aTxt.SelectedItem.ToLower();
                    int commaIndex = aTxt.SelectedItem.LastIndexOf(',');
                    if (commaIndex != -1)
                    {
                        locName = locName.Remove(commaIndex);
                    }


                    string formerValue = aTxt.FormerValue.ToLower().Trim();

                    int? loctypeId = 0;
                    Gen_Location loc = null;
                    if (AppVars.keyLocations.Contains(formerValue) || aTxt.FormerValue.EndsWith("  ")
                    || (aTxt.FormerValue.Length < 13 && aTxt.FormerValue.WordCount() == 2 && aTxt.FormerValue.Remove(aTxt.FormerValue.IndexOf(' ')).Trim().Length <= 3 && aTxt.FormerValue.Strip(' ').IsAlpha()))
                    {


                        if (aTxt.FormerValue.EndsWith("  ") || (aTxt.FormerValue.Length < 13 && aTxt.FormerValue.WordCount() == 2 && aTxt.FormerValue.Remove(aTxt.FormerValue.IndexOf(' ')).Trim().Length <= 2 && aTxt.FormerValue.Strip(' ').IsAlpha()))
                        {
                            loc = General.GetObject<Gen_Location>(c => c.LocationName.ToLower() == locName);
                        }
                        else
                            loc = General.GetObject<Gen_Location>(c => c.ShortCutKey == formerValue && c.LocationName.ToLower() == locName);

                        if (loc != null)
                        {
                            loctypeId = loc.LocationTypeId;
                        }
                    }

                    if (loctypeId != 0)
                    {

                        if (aTxt.Name == "txtFromAddress")
                        {



                            if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                            {
                                aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                aTxt.Text = aTxt.Text.Trim();
                                aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                                //if (aTxt.Name == "txtFromAddress")
                                //{
                                //    SetPickupZone(aTxt.Text);

                                //    UpdateAutoCalculateFares();
                                //}


                            }


                        }
                        else if (aTxt.Name == "txtToAddress")
                        {


                            if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                            {
                                aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                aTxt.Text = aTxt.Text.Trim();
                                aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                                //  SetDropOffZone(aTxt.Text);
                                //  UpdateAutoCalculateFares();


                            }




                        }
                    }
                    else if (aTxt.Text.Contains('.'))
                    {

                        //   RemoveNumbering(doorNo);


                    }
                    else if (!string.IsNullOrEmpty(doorNo))
                    {
                        aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                        aTxt.Text = doorNo + " " + aTxt.Text;
                        aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                    }
                    //else
                    //{
                    //    if (aTxt.Name == "txtFromAddress")
                    //    {


                    //        SetPickupZone(aTxt.SelectedItem);


                    //    }

                    //    else if (aTxt.Name == "txtToAddress")
                    //    {
                    //        SetDropOffZone(aTxt.SelectedItem);


                    //    }

                    //    if (aTxt.SelectedItem.ToStr().Trim() != string.Empty)
                    //    {
                    //        UpdateAutoCalculateFares();

                    //    }


                    //}

                    aTxt.FormerValue = string.Empty;


                    return;
                }



                text = text.ToLower();





                StartAddressTimer(text);

            }

            else
            {

                aTxt.ResetListBox();
                //  aTxt.ListBoxElement.Visible = false;
                aTxt.ListBoxElement.Items.Clear();

                //   CancelWebClientAsync();
                //  wc.CancelAsync();
                aTxt.Values = null;

            }



        }

        void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (aTxt == null)
                {

                    timer1.Stop();
                    return;
                }

                timer1.Stop();

                searchTxt = searchTxt.ToUpper();


                if (AppVars.objPolicyConfiguration.EnablePOI.ToBool())
                {

                    if (POIWorker.IsBusy)
                        POIWorker.CancelAsync();



                    POIWorker.RunWorkerAsync(searchTxt);
                }
                else
                {

                    PerformAddressChangeTimerWOPOI();
                }


            }
            catch (Exception ex)
            {


            }

        }


        private void StartAddressTimer(string text)
        {
            text = text.ToLower();
            searchTxt = text;
            InitializeTimer();
            timer1.Start();
        }

        private void InitializeTimer()
        {
            if (this.timer1 == null)
            {
                this.timer1 = new System.Windows.Forms.Timer();
                this.timer1.Tick += timer1_Tick;
                this.timer1.Interval = 500;
            }

        }








        private void AddressTextChangePOI()
        {
            string text = aTxt.Text;
            string doorNo = string.Empty;

            if (aTxt.SelectedItem != null && aTxt.ListBoxElement.SelectedItem != null && aTxt.SelectedItem.ToStr().ToLower() == aTxt.Text.ToLower()
               && aTxt.Text.Length > 0)
            {
                aTxt.TextChanged -= TextBoxElement_TextChanged;
                //  aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                aTxt.Text = aTxt.ListBoxElement.SelectedItem.ToStr().Trim().ToUpper().Trim();

                if (aTxt.Text.Contains(".") && aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".") - 1]))
                {
                    aTxt.Text = aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
                }

                aTxt.SelectedItem = aTxt.Text.Trim();
                aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            }




            for (int i = 0; i <= 2; i++)
            {
                if (char.IsNumber(text[i]))
                    doorNo += text[i];
                else
                    break;
            }





            if (text.Length > 1 && text != "BASX")
            {
                if (text.EndsWith("   "))
                {
                    //if (aTxt.Name == "txtFromAddress")
                    //{
                    //    FocusOnPickupPlot();
                    //}
                    //else if (aTxt.Name == "txtToAddress")
                    //{
                    //    FocusOnDropOffPlot();
                    //}
                    //return;
                }


                else if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() == aTxt.Text.ToLower())
                {
                    aTxt.ListBoxElement.Items.Clear();
                    aTxt.ResetListBox();

                    string locName = aTxt.SelectedItem.ToLower();
                    int commaIndex = aTxt.SelectedItem.LastIndexOf(',');
                    if (commaIndex != -1)
                    {
                        locName = locName.Remove(commaIndex);
                    }


                    string formerValue = aTxt.FormerValue.ToLower().Trim();





                    //else if (!string.IsNullOrEmpty(doorNo))
                    //{
                    //    aTxt.TextChanged -= TextBoxElement_TextChanged;
                    //    aTxt.Text = aTxt.Text;
                    //    aTxt.TextChanged += TextBoxElement_TextChanged;
                    //}
                    //else
                    //{
                    //    if (aTxt.Name == "txtFromAddress")
                    //    {
                    //        SetPickupZone(aTxt.SelectedItem);
                    //    }

                    //    else if (aTxt.Name == "txtToAddress")
                    //    {
                    //        SetDropOffZone(aTxt.SelectedItem);

                    //    }

                    //    if (aTxt.SelectedItem.ToStr().Trim() != string.Empty)
                    //    {
                    //        UpdateAutoCalculateFares();
                    //    }
                    //}

                    aTxt.FormerValue = string.Empty;
                    return;
                }


                text = text.ToLower();

                StartAddressTimer(text);

            }

            else
            {
                //if (MapType == Enums.MAP_TYPE.NONE)
                //    return;
                aTxt.ResetListBox();
                aTxt.ListBoxElement.Items.Clear();
                aTxt.Values = null;

            }



        }






        private void ShowAddressesPOI(string[] resValue)
        {
            int sno = 1;

            // var finalList = resValue;



            var finalList = (from a in AppVars.zonesList
                             from b in resValue
                             where b.Contains(a) && (b.Substring(b.IndexOf(a), a.Length) == a && (b.IndexOf(a) - 1) >= 0 && b[b.IndexOf(a) - 1] == ' ' && GeneralBLL.GetHalfPostCodeMatch(b) == a)

                             select b).ToArray<string>();


            if (finalList.Count() > 0)
            {
                finalList = finalList.Union(resValue).ToArray<string>();

            }
            else
                finalList = resValue;



            if (finalList.Count() < 10)
            {
                finalList = finalList.Select(args => (sno++) + ". " + args).ToArray();
            }


            aTxt.ListBoxElement.Items.Clear();
            aTxt.ListBoxElement.Items.AddRange(finalList);


            if (aTxt.ListBoxElement.Items.Count == 0)
                aTxt.ResetListBox();
            else
            {


                aTxt.ShowListBox();


            }

            if (searchTxt != aTxt.FormerValue.ToLower())
            {
                aTxt.FormerValue = aTxt.Text;

            }
        }




        private void PerformAddressChangeTimerWOPOI()
        {

            string postCode = General.GetPostCodeMatch(searchTxt);
            string fullPostCode = postCode;


            if (!string.IsNullOrEmpty(postCode) && postCode.IsAlpha() == true)
                postCode = string.Empty;


            string street = searchTxt;



            int IsAsc = 0;
            if (!string.IsNullOrEmpty(postCode))
            {
                street = street.Replace(postCode, "").Trim();

                if (postCode.Contains(' ') == false)
                {
                    if (postCode.Length == 3 && Char.IsNumber(postCode[2]))
                    {

                        IsAsc = 1;
                    }
                    else if (postCode.Length == 2 && Char.IsNumber(postCode[1]))
                    {

                        IsAsc = 1;
                    }
                    else if (postCode.Length > 3 && Char.IsNumber(postCode[3]))
                    {

                        IsAsc = 2;
                    }


                }

            }


            if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(postCode) && street.IsAlpha() == false && street.Length < 4 && searchTxt.IndexOf(postCode) < searchTxt.IndexOf(street))
            {
                street = "";
                postCode = searchTxt;
            }


            if (IsAsc == 1)
            {



                if (!string.IsNullOrEmpty(street))
                {


                    res = (from a in AppVars.listOfAddress

                           where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))

                           orderby a.PostalCode

                           select a.AddressLine1

                                   ).Take(1000).ToArray<string>();

                }
                else
                {

                    res = (from a in AppVars.listOfAddress

                           where a.PostalCode.StartsWith(postCode)

                           orderby a.PostalCode

                           select a.AddressLine1

                         ).Take(600).ToArray<string>();
                }

            }
            else if (IsAsc == 2)
            {


                res = (from a in AppVars.listOfAddress

                       where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))

                       orderby a.PostalCode descending

                       select a.AddressLine1

                               ).Take(500).ToArray<string>();


                if (street.Contains(' ') && res.Count() == 0)
                {

                    string[] vals = street.Split(' ');
                    int valCnt = vals.Count();

                    res = (from a in AppVars.listOfAddress

                           where (vals.Count(c => a.AddressLine1.Contains(c)) == valCnt)

                           select a.AddressLine1

                         ).Take(30).ToArray<string>();


                }


            }
            else
            {

                if (postCode.Contains(' '))
                {

                    res = null;

                    if (AppVars.objPolicyConfiguration.StripDoorNoOnAddress.ToBool()
                        && AppVars.zonesList.Count() > 0
                        && fullPostCode.Length > 0)
                    {

                        fullPostCode = General.GetPostCodeMatch(fullPostCode);

                        if (fullPostCode.Length > 0 && searchTxt.Trim() == fullPostCode)
                        {


                            string[] res1 = (from a in AppVars.listOfAddress

                                             where a.PostalCode == postCode

                                             select a.AddressLine1

                                       ).Take(1).ToArray<string>();





                            res = (from a in new TaxiDataContext().stp_GetRoadLevelData(fullPostCode)
                                   select a.AddressLine1).ToArray<string>();


                            res = res1.Union(res).Distinct().ToArray<string>();


                        }


                        if (res.Count() == 0)
                        {
                            res = (from a in AppVars.listOfAddress

                                   where a.PostalCode.StartsWith(postCode)

                                   orderby a.PostalCode

                                   select a.AddressLine1

                                  ).Take(100).ToArray<string>();


                        }




                    }
                    else
                    {

                        res = (from a in AppVars.listOfAddress

                               where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode))))

                               select a.AddressLine1

                                  ).Take(500).ToArray<string>();
                    }
                }
                else
                {


                    if (street.Length == 3 && street.IsAlpha() && !string.IsNullOrEmpty(AppVars.objPolicyConfiguration.CountyString))
                    {


                        string[] areas = AppVars.objPolicyConfiguration.CountyString.Split(',');

                        string last = street[2].ToStr();
                        street = street.Remove(2);

                        res = (from b in AppVars.listOfAddress.Where(a => areas.Any(c => a.AddressLine1.Contains(c)) && a.AddressLine1.Split(' ').Count() > 5)
                               //  let x = (areas.Any(c => b.Address.Contains(c)) ? b.Address.Split(' ') : null)
                               let x = b.AddressLine1.Split(' ')
                               where

                                  (

                               (x.ElementAt(0).StartsWith(street) && x.ElementAt(1).StartsWith(last))
                            || (x.ElementAt(0).StartsWith(street) && areas.Contains(x.ElementAt(2)) == false && x.ElementAt(2).StartsWith(last))
                                )

                               select b.AddressLine1

                                  ).Take(200).ToArray<string>();



                    }
                    else
                    {


                        if (street.WordCount() == 1 && street.ContainsNoSpaces())
                        {
                            //  street = street + " ";




                            if (AppVars.zonesList.Count() == 0)
                            {
                                res = (from a in AppVars.listOfAddress

                                       where (a.AddressLine1.StartsWith(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))
                                       select a.AddressLine1

                                ).Take(500).ToArray<string>();
                            }
                            else
                            {
                                res = (from a in AppVars.listOfAddress

                                       where (a.AddressLine1.StartsWith(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))
                                       select a.AddressLine1

                               ).Take(100).ToArray<string>();

                            }


                            if (AppVars.zonesList.Count() > 0)
                            {

                                string[] res2 = (from a in AppVars.listOfAddress

                                                 where (a.AddressLine1.StartsWith(street))
                                                 && AppVars.zonesList.Count(c => a.PostalCode.StartsWith(c)) > 0
                                                 select a.AddressLine1

                                    ).Take(200).ToArray<string>();

                                res = res2.Union(res).Distinct().ToArray<string>();


                            }










                        }
                        else
                        {



                            if (AppVars.zonesList.Count() > 0)
                            {




                                if (postCode.Length == 0)
                                {

                                    res = (from a in AppVars.listOfAddress


                                           where

                                           (a.AddressLine1.StartsWith(street))
                                           select a.AddressLine1

                                       ).Take(500).ToArray<string>();
                                }
                                else
                                {
                                    res = (from a in AppVars.listOfAddress


                                           where

                                           ((a.AddressLine1.StartsWith(street))
                                        && ((a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))

                                           select a.AddressLine1

                                      ).Take(500).ToArray<string>();


                                }


                                res = res.Union((from a in AppVars.listOfAddress


                                                 where

                                                 (a.AddressLine1.Contains(street)
                                                 && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))




                                                 select a.AddressLine1

                                   ).Take(2000)
                                     ).Distinct().ToArray<string>();




                            }
                            else
                            {

                                res = (from a in AppVars.listOfAddress

                                       where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))



                                       select a.AddressLine1

                                    ).Take(1000).ToArray<string>();
                            }

                        }






                    }



                    if (street.Contains(' ') && res.Count() == 0)
                    {



                        string[] vals = street.Split(' ');
                        int valCnt = vals.Count();


                        res = (from a in AppVars.listOfAddress

                               where (vals.Count(c => a.AddressLine1.Contains(c)) == valCnt)



                               select a.AddressLine1

                             ).Take(30).ToArray<string>();


                    }



                }



            }

            ShowAddresses();

        }

        private void ShowAddresses()
        {
            int sno = 1;



            var finalList = (from a in AppVars.zonesList
                             from b in res
                             where b.Contains(a) && (b.Substring(b.IndexOf(a), a.Length) == a && b[b.IndexOf(a) - 1] == ' ' && GeneralBLL.GetHalfPostCodeMatch(b) == a)




                             select b).ToArray<string>();


            if (finalList.Count() > 0)
            {
                finalList = finalList.Union(res).ToArray<string>();

                //  finalList = finalList.OrderBy(c=>AppVars.zonesList. c== AppVars.zonesList) c =.Union(res).ToArray<string>();

            }
            else
                finalList = res;


            if (finalList.Count() < 10)
            {

                finalList = finalList.Select(args => (sno++) + ". " + args).ToArray();

            }



            aTxt.ListBoxElement.Items.Clear();
            aTxt.ListBoxElement.Items.AddRange(finalList);


            if (aTxt.ListBoxElement.Items.Count == 0)
                aTxt.ResetListBox();
            else
            {

                //  aTxt.ListBoxElement.Visible = true;

                aTxt.ShowListBox();


            }

            if (searchTxt != aTxt.FormerValue.ToLower())
            {
                aTxt.FormerValue = aTxt.Text;

            }
        }



        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == '1' || e.KeyChar == '2' || e.KeyChar == '3' || e.KeyChar == '4'
                    || e.KeyChar == '5' || e.KeyChar == '6' || e.KeyChar == '7'
                    || e.KeyChar == '8' || e.KeyChar == '9')
                {




                    AutoCompleteTextBox txtData = (AutoCompleteTextBox)sender;
                    if (txtData.Text.StartsWith("W1"))
                        return;



                    if (txtData.Text.Length > 4 && txtData.ListBoxElement.Visible == true && txtData.ListBoxElement.Items.Count < 10)
                    {
                        string idx = e.KeyChar.ToStr();

                        txtData.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                        string item = txtData.ListBoxElement.Items[idx.ToInt() - 1].ToStr();

                        string doorNo = string.Empty;
                        for (int i = 0; i <= 2; i++)
                        {
                            if (char.IsNumber(txtData.FormerValue[i]))
                                doorNo += txtData.FormerValue[i];
                            else
                                break;

                        }


                        if (AppVars.objPolicyConfiguration.StripDoorNoOnAddress.ToBool())
                        {
                            txtData.Text = (item.Remove(0, item.IndexOf('.') + 1).Trim()).Trim();
                        }
                        else
                        {

                            txtData.Text = (doorNo + " " + item.Remove(0, item.IndexOf('.') + 1).Trim()).Trim();
                        }


                        //if (txtData.Name == "txtFromAddress")
                        //{
                        //    SetPickupZone(txtData.Text);
                        //    FocusOnFromDoor();
                        //}
                        //else if (txtData.Name == "txtToAddress")
                        //{
                        //    SetDropOffZone(txtData.Text);
                        //    FocusOnToDoor();
                        //}
                        //else if (txtData.Name == "txtViaAddress")
                        //{
                        //    txtData.ResetListBox();
                        //    AddViaPoint();

                        //}
                        txtData.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                        e.Handled = true;

                        aTxt.ResetListBox();
                        aTxt.ListBoxElement.Items.Clear();


                        //   UpdateAutoCalculateFares();
                        //   txtViaAddress.Focus();
                    }



                }
            }
            catch (Exception ex)
            {


            }
        }

        private void AddLocationToListBox(RadListControl lst, int? LocId, string locName, int? locTypeId, int? index)
        {


            string value = LocId.ToStr() + "," + locTypeId.ToStr();

            RadListDataItem item = null;
            if (index == null || index == -1)
            {

                item = new RadListDataItem();
                lst.Items.Add(item);
            }
            else
            {
                item = lst.SelectedItem;

            }


            item.Text = locName;
            item.Value = value;



        }
        private void DeleteItemFromListBox(RadListControl lst, RadListDataItem item)
        {
            if (lst.Items.Contains(item))
                lst.Items.Remove(item);

        }
        public override void Save()
        {
            try
            {
                int VehicleTypeId = grdLister.CurrentRow.Cells["VehicleTypeId"].Value.ToInt();
                int ? CompanyId= ddlCompany.SelectedValue.ToIntorNull();
                int SubCompanyId= ddlSubCompanyId.SelectedValue.ToInt();
                int FareId = 0;
                if (grdDetails.Rows.Count > 0 && grdDetails.Rows.Where(c => c.Cells["FAREID"].Value != null).Any())
                {
                    FareId = grdDetails.Rows.Where(c => c.Cells["FAREID"].Value != null).FirstOrDefault().Cells["FAREID"].Value.ToInt();
                }
                if (FareId == 0)
                {
                    var query = General.GetObject<Fare>(c => (c.VehicleTypeId == VehicleTypeId) && (c.SubCompanyId == SubCompanyId) && (c.CompanyId == null || c.CompanyId == CompanyId)).DefaultIfEmpty().Id;
                    if (query>0)
                    {
                        FareId = query;
                    }
                }
                if (FareId > 0)
                {
                    objMaster.GetByPrimaryKey(FareId);
                    objMaster.Edit();
                }
                else
                {
                    objMaster.New();
                }

                objMaster.SaveWithoutValidatingVehicleType = true;
                objMaster.Current.VehicleTypeId = VehicleTypeId;
                objMaster.Current.IsCompanyWise = chkCompanyWise.Checked;
                objMaster.Current.CompanyId = ddlCompany.SelectedValue.ToIntorNull();


                objMaster.Current.SubCompanyId = ddlSubCompanyId.SelectedValue.ToIntorNull();

                objMaster.Current.StartRate = numFixStartRate.Value;
                objMaster.Current.StartRateValidMiles = numStartRateValidMiles.Value;

                string[] skipProperties = { "Gen_Location", "Gen_Location1","Gen_LocationType",
                                            "Gen_LocationType1","Fare","Gen_Zone1","Gen_Zone","Fare_ZoneWisePricing1","Fare_ZoneWisePricing"};

                IList<Fare_ChargesDetail> savedList = objMaster.Current.Fare_ChargesDetails;
                List<Fare_ChargesDetail> listofDetail = (from r in grdDetails.Rows
                                                         select new Fare_ChargesDetail
                                                         {
                                                             Id = r.Cells[COLS_DETAILS.ID].Value.ToLong(),
                                                             FareId = r.Cells[COLS_DETAILS.FAREID].Value.ToInt(),
                                                             OriginLocationTypeId = r.Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToIntorNull(),
                                                             DestinationLocationTypeId = r.Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToIntorNull(),
                                                             OriginId = r.Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() != 100 ? r.Cells[COLS_DETAILS.FROMLOCATIONID].Value.ToIntorNull() : null,
                                                             DestinationId = r.Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() != 100 ? r.Cells[COLS_DETAILS.TOLOCATIONID].Value.ToIntorNull() : null,
                                                             FromZoneId = r.Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt() == 100 ? r.Cells[COLS_DETAILS.FROMLOCATIONID].Value.ToIntorNull() : null,
                                                             ToZoneId = r.Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt() == 100 ? r.Cells[COLS_DETAILS.TOLOCATIONID].Value.ToIntorNull() : null,
                                                             //  FromZoneId=null,
                                                             //  ToZoneId=null,
                                                             FromAddress = r.Cells[COLS_DETAILS.FROMLOCATION].Value.ToStr().Trim(),
                                                             ToAddress = r.Cells[COLS_DETAILS.TOLOCATION].Value.ToStr().Trim(),
                                                             Rate = r.Cells[COLS_DETAILS.FARE].Value.ToDecimal(),
                                                             CompanyRate = r.Cells[COLS_DETAILS.COMPANYFARE].Value.ToDecimal()
                                                         }).ToList();


                Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);


                objMaster.Save();

                //General.RefreshListWithoutSelected<frmFaresList>("frmFaresList1");
            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }


        }
        private void DisplayFareDetails(Fare obj)
        {
            if (obj == null) return;



            grdDetails.RowCount = obj.Fare_ChargesDetails.Count;


            grdDetails.BeginUpdate();
            for (int i = 0; i < grdDetails.RowCount; i++)
            {
                grdDetails.Rows[i].Cells[COLS_DETAILS.ID].Value = obj.Fare_ChargesDetails[i].Id;
                grdDetails.Rows[i].Cells[COLS_DETAILS.FAREID].Value = obj.Fare_ChargesDetails[i].FareId;
                grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCTYPEID].Value = obj.Fare_ChargesDetails[i].OriginLocationTypeId;
                grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCTYPEID].Value = obj.Fare_ChargesDetails[i].DestinationLocationTypeId;

                grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATIONID].Value = obj.Fare_ChargesDetails[i].OriginId;

                grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATIONID].Value = obj.Fare_ChargesDetails[i].DestinationId;



                if (obj.Fare_ChargesDetails[i].OriginLocationTypeId.ToInt() == 100)
                {
                    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATIONID].Value = obj.Fare_ChargesDetails[i].FromZoneId;

                    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATION].Value = obj.Fare_ChargesDetails[i].Gen_Zone.DefaultIfEmpty().ZoneName;
                }
                else
                {

                    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATION].Value = obj.Fare_ChargesDetails[i].FromAddress.ToStr();

                    //Loc = obj.Fare_ChargesDetails[i].Gen_Location1.DefaultIfEmpty();
                    //if (Loc.LocationTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATION].Value = Loc.PostCode;
                    //}
                    //else if (obj.Fare_ChargesDetails[i].OriginLocationTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS)
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATION].Value = obj.Fare_ChargesDetails[i].FromAddress.ToStr();


                    //}


                    //else
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.FROMLOCATION].Value = Loc.PostCode == string.Empty ? Loc.LocationName :
                    //                                                                 Loc.LocationName + " - " + Loc.PostCode;
                    //}
                }


                if (obj.Fare_ChargesDetails[i].DestinationLocationTypeId.ToInt() == 100)
                {
                    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATIONID].Value = obj.Fare_ChargesDetails[i].ToZoneId;


                    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATION].Value = obj.Fare_ChargesDetails[i].Gen_Zone1.DefaultIfEmpty().ZoneName;

                }
                else
                {
                    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATION].Value = obj.Fare_ChargesDetails[i].ToAddress.ToStr();

                    //Loc = obj.Fare_ChargesDetails[i].Gen_Location.DefaultIfEmpty();
                    //if (Loc.LocationTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATION].Value = Loc.PostCode;


                    //}
                    //else if (obj.Fare_ChargesDetails[i].DestinationLocationTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS)
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATION].Value = obj.Fare_ChargesDetails[i].ToAddress.ToStr();


                    //}
                    //else
                    //{
                    //    grdDetails.Rows[i].Cells[COLS_DETAILS.TOLOCATION].Value = Loc.PostCode == string.Empty ? Loc.LocationName :
                    //                                                                 Loc.LocationName + " - " + Loc.PostCode;
                    //}
                }


                grdDetails.Rows[i].Cells[COLS_DETAILS.FARE].Value = obj.Fare_ChargesDetails[i].Rate;
                grdDetails.Rows[i].Cells[COLS_DETAILS.COMPANYFARE].Value = obj.Fare_ChargesDetails[i].CompanyRate;
            }

            grdDetails.EndUpdate();

            ClearFareDetails();
        }
        #endregion

        


    }
}