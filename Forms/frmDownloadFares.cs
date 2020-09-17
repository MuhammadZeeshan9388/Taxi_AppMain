using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using SpreadsheetGear.Windows.Forms;
using Utils;
using Taxi_Model;
using Taxi_BLL;

namespace Taxi_AppMain
{
    public partial class frmDownloadFares : UI.SetupBase
    {
        WorkbookView objWBView = null;

        public struct COLS
        {
            public static string ID = "ID";
            public static string VehicleId = "VehicleId";

            public static string VehicleName = "Vehicle";

            public static string From = "From";

            public static string To = "To";
            public static string Fare = "Fare";

            public static string Status = "Status";
        

        }
        public frmDownloadFares()
        {
            InitializeComponent();

            objWBView = new WorkbookView();

            FormatGrid();
        }


        private void FormatGrid()
        {

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.ID;
            col.IsVisible = false;
            grdFixFares.Columns.Add(col);
     


            col = new GridViewTextBoxColumn();
            col.Name = COLS.VehicleName;
            col.HeaderText = COLS.VehicleName;
            col.FieldName = COLS.VehicleName;
            col.WrapText = true;
            col.Width = 100;
            grdFixFares.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = COLS.From;
            col.HeaderText = COLS.From;
            col.FieldName = COLS.From;
            col.WrapText = true;
            col.Width = 150;
            grdFixFares.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.To;
            col.HeaderText = COLS.To;
            col.Width = 150;
            col.WrapText = true;
            col.FieldName = COLS.To;
            grdFixFares.Columns.Add(col);


           


            GridViewDecimalColumn colDec = new GridViewDecimalColumn();
            colDec.Name = COLS.Fare;
            colDec.FieldName = COLS.Fare;
            colDec.DecimalPlaces = 2;
            colDec.Minimum = 0;
            colDec.Maximum = 999999;
            colDec.HeaderText = "Fare";
            colDec.Width = 80;
            grdFixFares.Columns.Add(colDec);



        }


        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void PasteFixFares()
        {
            int testi = 0;

            try
            {

                this.objWBView.Paste();

                objWBView.GetLock();


                int rowsCnt = objWBView.ActiveWorksheet.UsedRange.RowCount;
                int cellCnt = objWBView.ActiveWorksheet.UsedRange.CellCount.ToInt();
                if (cellCnt < 4) return;

                int lastRow = 0;

                lastRow = grdFixFares.RowCount;

                grdFixFares.RowCount += rowsCnt;

                for (int i = lastRow; i < grdFixFares.RowCount; i++)
                {
                    testi = i;
                    string vehicle = objWBView.ActiveWorksheet.Cells[i, 0].Value.ToStr().Trim().ToLower();

                    if (vehicle == "vehicle")
                    {
                        continue;
                    }

                    string from = objWBView.ActiveWorksheet.Cells[i, 1].Value.ToStr().Trim();
                    string to = objWBView.ActiveWorksheet.Cells[i, 2].Value.ToStr().Trim();

                    decimal fares = 0.00m;

                    if (objWBView.ActiveWorksheet.Cells[i, 3].Value.ToStr().StartsWith("£"))
                    {
                        fares = objWBView.ActiveWorksheet.Cells[i, 3].Value.ToStr().Substring(1).ToDecimal();
                    }
                    else
                    {

                         fares = objWBView.ActiveWorksheet.Cells[i, 3].Value.ToDecimal();
                    }

                    grdFixFares.Rows[i].Cells[COLS.VehicleName].Value = vehicle;
                    grdFixFares.Rows[i].Cells[COLS.From].Value = from;
                    grdFixFares.Rows[i].Cells[COLS.To].Value = to;
                    grdFixFares.Rows[i].Cells[COLS.Fare].Value = fares;

                }


                objWBView.ActiveWorksheet.Cells.Delete();

                objWBView.ReleaseLock();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message +" " + testi);

            }

        }

        private void PasteMilesFares()
        {
            this.objWBView.Paste();



        }

        private void ClearAll()
        {
            grdFixFares.Rows.Clear();
           

        }

        private void CloseForm()
        {
            this.Close();
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            Save();

            CloseForm();
        }

        public override void Save()
        {
            FareBO objFareMaster = null;

            try
            {
                int vehicleId = 0;


                string oldFrom = string.Empty;
                string vehicle = string.Empty;
                string from = string.Empty;
                string to = string.Empty;
                decimal fares = 0.00m;

                int? fromLocId = null;
                int? toLocId = null;
                int? fromLocTypeId = null;
                int? toLocTypeId = null;

                List<Fleet_VehicleType> listOfVehicle = AppVars.BLData.GetQueryable<Fleet_VehicleType>(null).ToList();
                List<Gen_Location> listOfLocations =  General.GetQueryable<Gen_Location>(null).ToList();


                LocationBO objLocationMaster = null;

                int recordNo = 0;
                foreach (var row in grdFixFares.Rows)
                {
                    if (!string.IsNullOrEmpty(row.Cells[COLS.VehicleName].Value.ToStr()))
                    {


                        vehicle = row.Cells[COLS.VehicleName].Value.ToStr();

                    }

                        from = row.Cells[COLS.From].Value.ToStr().ToUpper();
                    oldFrom = from;

                    if (!string.IsNullOrEmpty(row.Cells[COLS.To].Value.ToStr().ToUpper()))
                    {

                        to = row.Cells[COLS.To].Value.ToStr().ToUpper();

                    }

                    if (row.Cells[COLS.Fare].Value.ToStr().StartsWith("£"))
                    {
                        fares = row.Cells[COLS.Fare].Value.ToStr().Substring(1).ToDecimal();


                    }
                    else
                        fares = row.Cells[COLS.Fare].Value.ToDecimal();



                   

                    if (string.IsNullOrEmpty(from))
                    {

                        continue;
                    }

                    if (string.IsNullOrEmpty(to))
                    {


                    }

                    vehicleId = listOfVehicle.FirstOrDefault(c => c.VehicleType.ToLower().Trim() == vehicle).DefaultIfEmpty().Id;

                    if (vehicleId == 0)
                    {
                       // row.Cells[COLS.Status].Value = "Required : Vehicle";
                        continue;
                    }



                    Gen_Location objLoc = listOfLocations.FirstOrDefault(c =>
                                                ((c.LocationName.ToUpper().Trim() + " " + c.PostCode.ToUpper()) == from)
                                               || ( c.PostCode == from));


                    if (objLoc == null)
                    {
                        objLocationMaster = new LocationBO();

                        try
                        {
                            objLocationMaster.New();

                            from = General.GetPostCodeMatch(from);



                            if (string.IsNullOrEmpty(from))
                            {


                            }

                            objLocationMaster.Current.LocationName = from;
                            objLocationMaster.Current.PostCode = from;
                            objLocationMaster.Current.LocationTypeId = Enums.LOCATION_TYPES.POSTCODE;

                            objLocationMaster.Save();
                         }                         
                        catch (Exception ex)
                        {
                            if (objLocationMaster.Errors.Count > 0)
                            {
                                ENUtils.ShowMessage(objLocationMaster.ShowErrors());
                            }
                            else
                            {
                                ENUtils.ShowMessage(ex.Message);

                            }
                            break;

                        }

                        objLoc = objLocationMaster.Current;

                        listOfLocations.Add(objLoc);
                    }

                    fromLocId = objLoc.Id;
                    fromLocTypeId = objLoc.LocationTypeId;



                    objLoc = listOfLocations.FirstOrDefault(c =>
                                               ((c.LocationName.ToUpper().Trim() + " " + c.PostCode.ToUpper()) == to)
                                              || (c.PostCode == to));

                    if (objLoc == null)
                    {
                        objLocationMaster = new LocationBO();

                        try
                        {

                            objLocationMaster.New();


                            to = General.GetPostCodeMatch(to);

                           // to = General.GetPostCodeMatch(to);


                            if (to.Contains(' ')==false)
                            {
                              

                            }

                            objLocationMaster.Current.LocationName = to;
                            objLocationMaster.Current.PostCode = to;
                            objLocationMaster.Current.LocationTypeId = Enums.LOCATION_TYPES.POSTCODE;

                            objLocationMaster.Save();
                        }                         
                        catch (Exception ex)
                        {
                            if (objLocationMaster.Errors.Count > 0)
                            {
                                ENUtils.ShowMessage(objLocationMaster.ShowErrors());
                            }
                            else
                            {
                                ENUtils.ShowMessage(ex.Message);

                            }
                            break;

                        }
                        objLoc = objLocationMaster.Current;
                        listOfLocations.Add(objLoc);

                    }

                    toLocId = objLoc.Id;
                    toLocTypeId = objLoc.LocationTypeId;


                    if (recordNo == 0)
                    {


                        objFareMaster = new FareBO();


                        int fareId = General.GetObject<Fare>(c => c.VehicleTypeId == vehicleId).DefaultIfEmpty().Id;

                        if(fareId>0)
                        {
                            objFareMaster.GetByPrimaryKey(fareId);


                        }
                        else
                        {

                        objFareMaster.New();
                        }
                       
                        
                        objFareMaster.Current.VehicleTypeId = vehicleId;
                        objFareMaster.Current.IsCompanyWise = false;
                    }

                    objFareMaster.Current.Fare_ChargesDetails.Add(new Fare_ChargesDetail
                                                                                {
                                                                                    OriginId = fromLocId,
                                                                                    DestinationId = toLocId,
                                                                                    OriginLocationTypeId = fromLocTypeId,
                                                                                    DestinationLocationTypeId = toLocTypeId,
                                                                                    Rate = fares
                                                                                });

                    recordNo++;

                }

                objFareMaster.Save();
            }
            catch (Exception ex)
            {
                if (objFareMaster.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objFareMaster.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnPasteFixFares_Click(object sender, EventArgs e)
        {
            PasteFixFares();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            Save();
        }

        private void btnClearAll_Click_1(object sender, EventArgs e)
        {

        }

        private void btnClearPlotFares_Click(object sender, EventArgs e)
        {
            grdPlotFares.Rows.Clear();
        }

        private void btnExitPlotFares_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnSavePlotFares_Click(object sender, EventArgs e)
        {
            SavePlotFares();
        }

        private void SavePlotFares()
        {


        }


        private void PastePlotFares()
        {



        }

        private void btnPastePlotFares_Click(object sender, EventArgs e)
        {
            PastePlotFares();
        }


        






        

       
    }
}
