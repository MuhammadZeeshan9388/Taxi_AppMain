using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using Utils;
using Taxi_BLL;
using DAL;
using System.Data.SqlClient;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmAddFares : UI.SetupBase
    {
        int Id = 0;
        public frmAddFares(int CompanyId)
        {       
            InitializeComponent();
            FormatFaresDetailGrid();
            Id = CompanyId;

            this.Load += new EventHandler(frmAddFares_Load);
            FillCombo();
            ddlVehicleType.SelectedValue = Id;
            OnNew();
            this.grdDetails.CellDoubleClick += new GridViewCellEventHandler(grdDetails_CellDoubleClick);
            this.KeyDown += new KeyEventHandler(frmAddFares_KeyDown);
            this.grdDetails.CommandCellClick += new CommandCellClickEventHandler(grdDetails_CommandCellClick);
        }

        void grdDetails_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name == "Delete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a record? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        grdDetails.CurrentRow.Delete();
                    }

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void frmAddFares_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        void grdDetails_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdDetails.CurrentRow != null && grdDetails.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdDetails.CurrentRow;

                ddlFromLocType.SelectedValue = row.Cells[COLS_DETAILS.FROMLOCTYPEID].Value.ToInt();
                ddlToLocType.SelectedValue = row.Cells[COLS_DETAILS.TOLOCTYPEID].Value.ToInt();
                ddlFromLocation.SelectedValue = row.Cells[COLS_DETAILS.FROMLOCATIONID].Value.ToInt();
                ddlToLocation.SelectedValue = row.Cells[COLS_DETAILS.TOLOCATIONID].Value.ToInt();
                numRate_FareCharges.Value = row.Cells[COLS_DETAILS.FARE].Value.ToDecimal();


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
            col.Width = 260;
            col.ReadOnly = true;
            grdDetails.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "To Location";
            col.ReadOnly = true;
            col.Name = COLS_DETAILS.TOLOCATION;
            col.Width = 260;
            grdDetails.Columns.Add(col);



            GridViewDecimalColumn colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Fare (£)";
            colDec.Width = 130;
            colDec.ReadOnly = false;
            colDec.DecimalPlaces = 2;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS_DETAILS.FARE;
            grdDetails.Columns.Add(colDec);

            grdDetails.MasterTemplate.ShowRowHeaderColumn = false;


            //UI.GridFunctions.AddDeleteColumn(grdDetails);
            //grdDetails.Columns["ColDelete"].Width = 80;

            DetailGridButton();

        }
        public void DetailGridButton()
        {
            GridViewCommandColumn cmd = new GridViewCommandColumn();
            cmd.Name = "Delete";
            cmd.HeaderText = "";
            cmd.DefaultText = "Delete";
            cmd.UseDefaultText = true;
            cmd.TextImageRelation = TextImageRelation.ImageBeforeText;
            cmd.TextAlignment = ContentAlignment.MiddleCenter;
            cmd.Width = 80;
            grdDetails.Columns.Add(cmd);

        }
        private void ClearFromLocation()
        {
            ddlFromLocation.SelectedValue = null;

        }


        private void ClearToLocation()
        {
            ddlToLocation.SelectedValue = null;


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
        }
        void frmAddFares_Load(object sender, EventArgs e)
        {
            //FillCombo();
            //ddlVehicleType.SelectedValue = Id;

        }
        private void FillCombo()
        {
           // ComboFunctions.
            ComboFunctions.FillLocationTypeCombo(ddlFromLocType);
            ComboFunctions.FillLocationTypeCombo(ddlToLocType);
            ComboFunctions.FillVehicleTypeCombo(ddlVehicleType);
        //    ComboFunctions.FillCompanyCombo(ddlCompany);
            ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.POSTCODE;
            ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.POSTCODE;

        }
        private void FillFromLocations()
        {
            int locTypeId = ddlFromLocType.SelectedValue.ToInt();
            if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                ComboFunctions.FillPostCodeLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);


            }
            //else if (ddlFromLocType.Text.Trim().ToLower() == "zone")
            //{

            //    ComboFunctions.FillZonesPlottedCombo(ddlFromLocation);


            //}
            else
            {
                ComboFunctions.FillLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);
            }
        }

        private void FillToLocations()
        {
            int locTypeId = ddlToLocType.SelectedValue.ToInt();
            if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                ComboFunctions.FillPostCodeLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);


            }
            //else if (ddlToLocType.Text.Trim().ToLower() == "zone")
            //{

            //    ComboFunctions.FillZonesPlottedCombo(ddlToLocation);


            //}
            else
            {

                ComboFunctions.FillLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);
            }
        }
        private void CompanyWise(ToggleState args)
        {
            //if (args == Telerik.WinControls.Enumerations.ToggleState.On)
            //{
            //    ddlCompany.Enabled = true;

            //}
            //else
            //{
            //    ddlCompany.Enabled = false;
            //    ddlCompany.SelectedValue = null;
            //}

        }
        //private void chkCompanyWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        //{
        //    CompanyWise(args.ToggleState);
        //}
        private void AddDetail()
        {

            decimal fares = numRate_FareCharges.Value.ToDecimal();
            int? fromLocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
            int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            int? fromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
            int? toLocId = ddlToLocation.SelectedValue.ToIntorNull();


            string fromLocation = ddlFromLocation.Text.ToStr();
            string toLocation = ddlToLocation.Text.ToStr();
            string fromLocType = ddlFromLocType.Text.ToStr();
            string toLocType = ddlToLocType.Text.ToStr();

            string msg = string.Empty;


            string fromPostCode = "";
            string toPostCode = "";

            if (lstFromLocation.Items.Count == 0)
            {


                if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    if (fromLocId == null)
                    {
                        fromPostCode = General.GetPostCodeMatch(fromLocation);
                        if (!string.IsNullOrEmpty(fromPostCode) && fromPostCode.IsAlpha() == false)
                        {
                           // AddLocation(fromLocation, ref fromLocId, ref fromPostCode);
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
                           // AddLocation(toLocation, ref toLocId, ref toPostCode);
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
                    }
                }




            }

            ClearFareDetails();

        }

        private void chkCompanyWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            CompanyWise(args.ToggleState);
        }
        public override void OnNew()
        {
           // dtpEffectiveDate.Value = DateTime.Now;


            grdDetails.Rows.Clear();
       //     ddlCompany.SelectedValue = null;
         //   chkCompanyWise.Checked = false;
            CompanyWise(ToggleState.Off);
            ClearFareDetails();
            //ClearOtherChargesDetails();

            FillFromLocations();
            FillToLocations();


            //dtpEffectiveDate.Focus();
           // ddlVehicleType.SelectedValue = AppVars.objPolicyConfiguration.DefaultVehicleTypeId;
            ddlVehicleType.Focus();


        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlVehicleType.SelectedValue == null)
            {
                ENUtils.ShowMessage("Required : Vehicle");
                return;

            }
            AddDetail();
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

        private void btnAddFromLoc_Click(object sender, EventArgs e)
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

        private void btnAddToLoc_Click(object sender, EventArgs e)
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

        private void btnEditFromLoc_Click(object sender, EventArgs e)
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

        private void btnEditToLoc_Click(object sender, EventArgs e)
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
        private void DeleteItemFromListBox(RadListControl lst, RadListDataItem item)
        {
            if (lst.Items.Contains(item))
                lst.Items.Remove(item);

        }

        private void btnDeleteFromLst_Click(object sender, EventArgs e)
        {
            DeleteItemFromListBox(lstFromLocation, lstFromLocation.SelectedItem);
        }

        private void btnDeleteToLst_Click(object sender, EventArgs e)
        {
            DeleteItemFromListBox(lstToLocation, lstToLocation.SelectedItem);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFareDetails();
        }
        
        private void ddlFromLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillFromLocations();
        }

        private void ddlToLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillToLocations();
        }

        private void btnAddFromLocation_Click(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdDetails.Rows.Count ==0)
                {
                    ENUtils.ShowMessage("Required : Fares");
                    return;
                 }
                int FareId = ddlVehicleType.SelectedValue.ToInt();
                if (FareId == 0)
                {
                    ENUtils.ShowMessage("Required : Vehicle Type");
                    return;                
                }
                foreach (var item in grdDetails.Rows)
                {
                    //int FareId = ddlVehicleType.SelectedValue.ToInt();//item.Cells[""].Value.ToInt();
                    int originid = item.Cells["FROMLOCATIONID"].Value.ToInt();
                    int destinationid = item.Cells["TOLOCATIONID"].Value.ToInt();
                    decimal fare = item.Cells["FARE"].Value.ToDecimal();
                    int originlocationtypeid = item.Cells["FROMLOCTYPEID"].Value.ToInt();
                    int destinationlocationtypeid = item.Cells["TOLOCTYPEID"].Value.ToInt();
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_AddNewFare_ChargesDetails(FareId,originid,destinationid,fare,originlocationtypeid,destinationlocationtypeid,null,null,null,AppVars.LoginObj.LuserId.ToInt());
                    }
                }
                General.RefreshListWithoutSelected<frmFaresList>("frmFaresList1");
                this.Close();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage("Fare already exist of this Vehicle type");
            }
        }

    }
}
