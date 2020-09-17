using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI;
using Utils;
using Taxi_BLL;
using Taxi_Model;
using DotNetCoords;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmPlotMovements : SetupBase
    {
        private string BaseArea = string.Empty;

        public frmPlotMovements()
        {
            InitializeComponent();





            radPageView1.SelectedPageChanged+=new EventHandler(radPageView1_SelectedPageChanged); 
          
            

        }

        void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {
            RefreshRecords();
        }


        List<Gen_Location> listofLocations = null;

       

       

        private List<Gen_Location> GetLocationsList()
        {

            string[] arrPriority = AppVars.objPolicyConfiguration.PriorityPostCodes.Split(new char[] { ',' });

            char[] arr = new char[] { ' ' };

            return General.GetQueryable<Gen_Location>(c=>c.LocationTypeId!=Enums.LOCATION_TYPES.POSTCODE)
                .AsEnumerable().Where(c => arrPriority.Count(a => a == c.PostCode.Split(arr)[0]) > 0).ToList();

            
        }


        private void FormatGrid()
        {

            grdAddresses.Font = new Font("Tahoma", 10);

            grdAddresses.AllowAddNewRow = false;
            grdAddresses.ShowGroupPanel = false;
            // grdZones.AutoCellFormatting = true;
            grdAddresses.ShowRowHeaderColumn = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
            grdAddresses.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "FullAddress";
            col.HeaderText = "FullAddress";
            col.IsVisible = false;
            grdAddresses.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Width = 450;
            col.HeaderText = "Street";
            col.Name = "Street";
            col.ReadOnly = false;
            grdAddresses.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.Name = "PostCode";
            col.ReadOnly = false;
            col.HeaderText = "Post Code";
            grdAddresses.Columns.Add(col);


            GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "Zone";
            //colCombo.DataSource = General.GetGeneralList<Gen_SysPolicyDocumentsList>(null);
            colCombo.DataSource = listofZones;
            colCombo.HeaderText = "Plot";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            //  colCombo.NullValue = "Select";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdAddresses.Columns.Add(colCombo);



            col = new GridViewTextBoxColumn();
           
            col.Name = "OldZoneId";
            col.IsVisible = false;
            grdAddresses.Columns.Add(col);


            GridFunctions.SetFilter(grdAddresses);


            grdAddresses.Columns["Zone"].ReadOnly = false;
            grdAddresses.AllowEditRow = true;



            AddEditColumn(grdAddresses);
            AddDeleteColumn(grdAddresses);

            grdAddresses.CommandCellClick += new CommandCellClickEventHandler(grdAddresses_CommandCellClick);

        }

        void grdAddresses_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
               
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                string name = gridCell.ColumnInfo.Name.ToLower();

                GridViewRowInfo row = gridCell.RowElement.RowInfo;
                string fullAddress=row.Cells["FullAddress"].Value.ToStr().Trim().ToUpper();


                string street = row.Cells["Street"].Value.ToStr().Trim().ToUpper();
                string postCode = row.Cells["PostCode"].Value.ToStr().Trim().ToUpper();

                int zoneId = row.Cells["Zone"].Value.ToInt();

                if (zoneId==0)
                {
                    ENUtils.ShowMessage("Plot cannot be empty");
                    return;

                }

                if (string.IsNullOrEmpty(street))
                {

                    ENUtils.ShowMessage("Street cannot be empty");
                    return;
                }



                if (string.IsNullOrEmpty(postCode))
                {

                    ENUtils.ShowMessage("PostCode cannot be empty");
                    return;
                }

                string addressQuery = string.Empty;




                if (name == "delete")
                {

                    if (DialogResult.Yes == RadMessageBox.Show("are you sure ?", "Delete", MessageBoxButtons.YesNo))
                    {

                        addressQuery = "delete from GEN_ADDRESSES where addressline1='" + fullAddress + "';" + Environment.NewLine;


                        row.Delete();
                    }
                    else
                        return;
                }
                else if (name == "edit")
                {

                        street = street.Replace("'A", "A").ToStr().Trim().Replace("'B", "B").ToStr().Trim().Replace("'C", "C").Replace("'C", "C").ToStr().Trim()
                  .Replace("'D", "D").ToStr().Trim().Replace("'E", "E").ToStr().Trim().Replace("'F", "F").ToStr().Trim().Replace("'G", "G").ToStr().Trim().Replace("'H", "H").ToStr().Trim()
                  .Replace("'I", "I").ToStr().Trim().Replace("'J", "J").ToStr().Trim().Replace("'K", "K").ToStr().Trim().Replace("'L", "L").ToStr().Trim()
                  .Replace("'M", "M").ToStr().Trim().Replace("'N", "N").ToStr().Trim().Replace("'O", "O").ToStr().Trim().Replace("'P", "P").ToStr().Trim()
                  .Replace("'Q", "Q").ToStr().Trim().Replace("'R", "R").ToStr().Trim().Replace("'S", "S").ToStr().Trim().Replace("'T", "T").ToStr().Trim().Replace("'U", "U").ToStr().Trim()
                  .Replace("'V", "V").ToStr().Trim().Replace("'W", "W").ToStr().Trim().Replace("'X", "X").ToStr().Trim().Replace("'Y", "Y").ToStr().Trim()
                  .Replace("'Z", "Z").ToStr().Trim();

                    street = street.Replace("\r\n", "").ToStr().Trim();
                    street = street.Replace(",", "").ToStr().Trim();


                    string OLDaDDRESS = row.Cells["FullAddress"].Value.ToStr();

                        OLDaDDRESS = OLDaDDRESS.Replace("'A", "''A").ToStr().Trim().Replace("'B", "''B").ToStr().Trim().Replace("'C", "''C")
                    .Replace("'D", "''D").ToStr().Trim().Replace("'E", "''E").ToStr().Trim().Replace("'F", "''F").ToStr().Trim().Replace("'G", "''G").ToStr().Trim().Replace("'H", "''H").ToStr().Trim()
                    .Replace("'I", "''I").ToStr().Trim().Replace("'J", "''J").ToStr().Trim().Replace("'K", "''K").ToStr().Trim().Replace("'L", "''L").ToStr().Trim()
                    .Replace("'M", "''M").ToStr().Trim().Replace("'N", "''N").ToStr().Trim().Replace("'O", "''O").ToStr().Trim().Replace("'P", "''P").ToStr().Trim()
                    .Replace("'Q", "''Q").ToStr().Trim().Replace("'R", "''R").ToStr().Trim().Replace("'S", "''S").ToStr().Trim().Replace("'T", "''T").ToStr().Trim().Replace("'U", "''U").ToStr().Trim()
                    .Replace("'V", "''V").ToStr().Trim().Replace("'W", "''W").ToStr().Trim().Replace("'X", "''X").ToStr().Trim().Replace("'Y", "''Y").ToStr().Trim()
                    .Replace("'Z", "''Z").ToStr().Trim();



                    addressQuery = "update GEN_ADDRESSES SET addressline1='" + street + " " + postCode + "',PostalCode='"+postCode+"',ZoneId=" + zoneId + "  where addressline1='" + OLDaDDRESS + "';" + Environment.NewLine;

                }

                

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    if (addressQuery.ToStr().Trim() != string.Empty)
                    {
                        db.stp_RunProcedure(addressQuery);
                    }                   
                }

                if (addressQuery.ToStr().Length > 0)
                {

                    if (name == "delete")
                    {
                        AppVars.listOfAddress.RemoveAll(c => c.AddressLine1 == fullAddress);

                        addresses.RemoveAll(c => c.AddressLine1 == fullAddress);
                    }
                    else if (name == "edit")
                    {
                        var obj=  AppVars.listOfAddress.FirstOrDefault(c => c.AddressLine1 == fullAddress);
                        if (obj != null)
                        {
                            obj.AddressLine1 = street + " " + postCode;
                            obj.PostalCode = postCode;
                           

                             var obj1= addresses.FirstOrDefault(c => c.AddressLine1 == fullAddress);
                             if (obj1 !=null)
                             {
                                 obj1.AddressLine1 = street + " " + postCode;
                                 obj1.PostalCode = postCode;

                                 if (obj1.ZoneId != zoneId)
                                 {
                                     obj.ZoneId = zoneId;

                                     obj1.ZoneId = zoneId;

                                     row.Delete();
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


        private void AddEditColumn(RadGridView grid)
        {

            GridViewCommandColumn col = new GridViewCommandColumn();

            col.AutoSizeMode = BestFitColumnMode.DisplayedDataCells;
            col.Name = "edit";
            col.UseDefaultText = true;
            //  col.ImageLayout = System.Windows.Forms.ImageLayout.Center;

            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            col.DefaultText = "Update";
            col.Width = 60;         
            grid.Columns.Add(col);

        }


        private void AddDeleteColumn(RadGridView grid)
        {

            GridViewCommandColumn col = new GridViewCommandColumn();

            col.AutoSizeMode = BestFitColumnMode.DisplayedDataCells;
            col.Name = "delete";
            col.UseDefaultText = true;
            //  col.ImageLayout = System.Windows.Forms.ImageLayout.Center;

            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            col.DefaultText = "Delete";
            col.Width = 60;
            grid.Columns.Add(col);

        }



        private void FormatLocationsGrid()
        {
            grdLocations.Font = new Font("Tahoma", 10);
            grdLocations.AllowAddNewRow = false;
            grdLocations.ShowGroupPanel = false;
            // grdZones.AutoCellFormatting = true;
            grdLocations.ShowRowHeaderColumn = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
            grdLocations.Columns.Add(col);


            //col = new GridViewTextBoxColumn();
            //col.Width = 100;
            //col.ReadOnly = true;
            //col.Name = "Type";
            //col.HeaderText = "Type";
            //grdLocations.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Width = 450;
            col.ReadOnly = false;
            col.HeaderText = "Location Name";
            col.Name = "Street";
            grdLocations.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Width = 100;
            col.ReadOnly = false;
            col.Name = "PostCode";
            col.HeaderText = "Post Code";
            grdLocations.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.IsVisible = false;
            col.Name = "OldStreet";
            grdLocations.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.Name = "OldPostCode";
            col.IsVisible = false;
            grdLocations.Columns.Add(col);

            //col = new GridViewTextBoxColumn();
            //col.Width = 250;
            //col.ReadOnly = true;
            //col.Name = "Address";
            //col.HeaderText = "Address";
            //grdLocations.Columns.Add(col);


            GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "Zone";
            //colCombo.DataSource = General.GetGeneralList<Gen_SysPolicyDocumentsList>(null);
            colCombo.DataSource = listofZones;
            colCombo.HeaderText = "Plot";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            //  colCombo.NullValue = "Select";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdLocations.Columns.Add(colCombo);

            col = new GridViewTextBoxColumn();
            col.Name = "OldZoneId";
            col.IsVisible = false;
            grdLocations.Columns.Add(col);

            GridFunctions.SetFilter(grdLocations);
            grdLocations.Columns["Zone"].ReadOnly = false;
            grdLocations.AllowEditRow = true;


            AddEditColumn(grdLocations);
            AddDeleteColumn(grdLocations);


            grdLocations.CommandCellClick += new CommandCellClickEventHandler(grdLocations_CommandCellClick);

        }

        void grdLocations_CommandCellClick(object sender, EventArgs e)
        {
            try
            {

                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                string name = gridCell.ColumnInfo.Name.ToLower();

                GridViewRowInfo row = gridCell.RowElement.RowInfo;

                string street = row.Cells["Street"].Value.ToStr().Trim();
                string postCode = row.Cells["PostCode"].Value.ToStr().Trim();
                int zoneId = row.Cells["Zone"].Value.ToInt();

                if (zoneId == 0)
                {
                    ENUtils.ShowMessage("Plot cannot be empty");
                    return;

                }

                if (string.IsNullOrEmpty(street))
                {

                    ENUtils.ShowMessage("Street cannot be empty");
                    return;
                }



                if (string.IsNullOrEmpty(postCode))
                {

                    ENUtils.ShowMessage("PostCode cannot be empty");
                    return;
                }



                string addressQuery = string.Empty;

                if (name == "delete")
                {

                    if (DialogResult.Yes == RadMessageBox.Show("are you sure ?", "Delete", MessageBoxButtons.YesNo))
                    {

                        addressQuery = "delete from Gen_locations  where LOCATIONNAME='" + row.Cells["OldStreet"].Value.ToStr() + "' AND POSTCODE='" + row.Cells["OldPostCode"].Value.ToStr() + "';" + Environment.NewLine;


                        row.Delete();
                    }
                    else
                        return;
                }
                else if (name == "edit")
                {
                        street = street.Replace("'A", "A").ToStr().Trim().Replace("'B", "B").ToStr().Trim().Replace("'C", "C").Replace("'C", "C").ToStr().Trim()
                    .Replace("'D", "D").ToStr().Trim().Replace("'E", "E").ToStr().Trim().Replace("'F", "F").ToStr().Trim().Replace("'G", "G").ToStr().Trim().Replace("'H", "H").ToStr().Trim()
                    .Replace("'I", "I").ToStr().Trim().Replace("'J", "J").ToStr().Trim().Replace("'K", "K").ToStr().Trim().Replace("'L", "L").ToStr().Trim()
                    .Replace("'M", "M").ToStr().Trim().Replace("'N", "N").ToStr().Trim().Replace("'O", "O").ToStr().Trim().Replace("'P", "P").ToStr().Trim()
                    .Replace("'Q", "Q").ToStr().Trim().Replace("'R", "R").ToStr().Trim().Replace("'S", "S").ToStr().Trim().Replace("'T", "T").ToStr().Trim().Replace("'U", "U").ToStr().Trim()
                    .Replace("'V", "V").ToStr().Trim().Replace("'W", "W").ToStr().Trim().Replace("'X", "X").ToStr().Trim().Replace("'Y", "Y").ToStr().Trim()
                    .Replace("'Z", "Z").ToStr().Trim();

                        street = street.Replace("\r\n", "").ToStr().Trim();
                        street = street.Replace(",", "").ToStr().Trim();

                        string OLDStreet = row.Cells["OldStreet"].Value.ToStr();
                        OLDStreet = OLDStreet.Replace("'A", "''A").ToStr().Trim().Replace("'B", "''B").ToStr().Trim().Replace("'C", "''C")
                       .Replace("'D", "''D").ToStr().Trim().Replace("'E", "''E").ToStr().Trim().Replace("'F", "''F").ToStr().Trim().Replace("'G", "''G").ToStr().Trim().Replace("'H", "''H").ToStr().Trim()
                       .Replace("'I", "''I").ToStr().Trim().Replace("'J", "''J").ToStr().Trim().Replace("'K", "''K").ToStr().Trim().Replace("'L", "''L").ToStr().Trim()
                       .Replace("'M", "''M").ToStr().Trim().Replace("'N", "''N").ToStr().Trim().Replace("'O", "''O").ToStr().Trim().Replace("'P", "''P").ToStr().Trim()
                       .Replace("'Q", "''Q").ToStr().Trim().Replace("'R", "''R").ToStr().Trim().Replace("'S", "''S").ToStr().Trim().Replace("'T", "''T").ToStr().Trim().Replace("'U", "''U").ToStr().Trim()
                       .Replace("'V", "''V").ToStr().Trim().Replace("'W", "''W").ToStr().Trim().Replace("'X", "''X").ToStr().Trim().Replace("'Y", "''Y").ToStr().Trim()
                       .Replace("'Z", "''Z").ToStr().Trim();

                        addressQuery += "UPDATE GEN_LOCATIONS SET locationname='" + street + "',PostCode='" + postCode + "',ZoneId=" + zoneId + " where LOCATIONNAME='" + OLDStreet + "' AND POSTCODE='" + row.Cells["OldPostCode"].Value.ToStr() + "';" + Environment.NewLine;

                }

               

               // addressQuery = addressQuery.Replace("SET locationname=", "SET locationname='").ToStr().Trim();

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    if (addressQuery.ToStr().Trim() != string.Empty)
                    {
                        db.stp_RunProcedure(addressQuery);
                    }
                }

                if (addressQuery.ToStr().Length > 0)
                {
                    RefreshRecords();

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }

        }








        private void frmPlotMovements_Load(object sender, EventArgs e)
        {
            InitializeData();
            treeView1.ExpandAll();
        }


        private void InitializeData()
        {
            try
            {

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    listOfCoordinates = db.Gen_Coordinates.ToList();

                    listofZones = db.Gen_Zones.Where(c => c.MaxLatitude != null).OrderBy(c => c.OrderNo).ToList();

                    listofPolyVertices = db.Gen_Zone_PolyVertices.ToList();
                }


                string val = AppVars.objPolicyConfiguration.BaseAddress.ToStr();

                if (val.Contains(" "))
                {
                    val = General.GetPostCodeMatch(val);

                    val = val.Split(new char[] { ' ' })[0];

                    for (int i = 0; i < val.Length; i++)
                    {
                        if (char.IsLetter(val[i]))
                        {
                            BaseArea += val[i];
                        }
                        else
                            break;
                    }


                }


                char[] splitArr = new char[] { ' ' };

                string[] arrPriority = AppVars.objPolicyConfiguration.PriorityPostCodes.Split(new char[] { ',' });


                foreach (var item in arrPriority)
                {
                    treeView1.Nodes[0].Nodes.Add(item);
                }


                foreach (var item in listofZones)
                {
                    treeView1.Nodes[1].Nodes.Add(new TreeNode { Text =item.OrderNo.ToStr()+ ". "+ item.ZoneName, Tag = item.Id });
                }


                treeView1.Nodes[2].Tag = -1;

                addresses = AppVars.listOfAddress
                   .Where(c => c.PostalCode.StartsWith(this.BaseArea)).ToList();

                addresses = addresses.Where(c => arrPriority.Count(a => a == c.PostalCode.Split(splitArr)[0]) > 0).ToList();

                FormatGrid();

                FormatLocationsGrid();
                
                treeView1.ShowPlusMinus = true;
                                
            }
            catch (Exception ex)
            {


            }

        }

        List<Gen_Coordinate> listOfCoordinates = null;
        List<Gen_Zone> listofZones = null;
        List<Gen_Zone_PolyVertice> listofPolyVertices = null;
        List<stp_GetFullAddressesResult> addresses = null;


        private void PopulateUnPlottedAddresses(string postcode)
        {

            char[] splitArr = new char[] { ' ' };

            string[] arrPriority = AppVars.objPolicyConfiguration.PriorityPostCodes.Split(new char[] { ',' });



           


            var priorityAddresses = addresses.Where(c => c.PostalCode.Split(splitArr)[0] == postcode && c.ZoneId==null).OrderBy(c=>c.PostalCode).ToList();

           

            List<stp_GetFullAddressesResult> unPlottedAddresses = new List<stp_GetFullAddressesResult>();

         


            int? zoneId = null;
            int cnter = 0;
            for (int i = 0; i < priorityAddresses.Count; i++)
            {

                try
                {

                    cnter++;

                    zoneId = null;

                    string postCode = General.GetPostCode(priorityAddresses[i].AddressLine1.ToUpper());

                    Gen_Coordinate objCoord = listOfCoordinates.FirstOrDefault(c => c.PostCode == postCode);


                    if (objCoord != null)
                    {

                        double latitude = 0, longitude = 0;

                        latitude = Convert.ToDouble(objCoord.Latitude);
                        longitude = Convert.ToDouble(objCoord.Longitude);



                        var plot = (from a in listofZones.Where(c => c.MinLatitude != null && (latitude >= c.MinLatitude && latitude <= c.MaxLatitude)
                                                           && (longitude <= c.MaxLongitude && longitude >= c.MinLongitude))
                                    orderby a.PlotKind

                                    select a.Id).ToArray<int>();


                        if (plot.Count() > 0)
                        {
                            var list = (from p in plot
                                        join a in listofPolyVertices on p equals a.ZoneId
                                        select a).ToList();


                            foreach (int plotId in plot)
                            {
                                if (FindPoint(latitude, longitude, list.Where(c => c.ZoneId == plotId).ToList()))
                                {
                                    zoneId = plotId;
                                    break;

                                }
                            }
                        }
                 

                    }


                    // zoneId= General.GetZoneId(addresses[i].AddressLine1.ToStr().ToUpper());

                    if (zoneId == null)
                    {
                        unPlottedAddresses.Add(priorityAddresses[i]);
                    }
                    
                }
                catch
                {


                }
            }



            int cnt=unPlottedAddresses.Count;

            grdAddresses.RowCount = cnt;


            for (int i = 0; i < cnt; i++)
            {
                grdAddresses.Rows[i].Cells["FullAddress"].Value = unPlottedAddresses[i].AddressLine1;
                grdAddresses.Rows[i].Cells["Street"].Value = unPlottedAddresses[i].AddressLine1.Replace(unPlottedAddresses[i].PostalCode,"").Trim();
                grdAddresses.Rows[i].Cells["PostCode"].Value = unPlottedAddresses[i].PostalCode;
                grdAddresses.Rows[i].Cells["Zone"].Value = unPlottedAddresses[i].ZoneId;
                grdAddresses.Rows[i].Cells["OldZoneId"].Value = unPlottedAddresses[i].ZoneId;
            }


          
         
           


        }




        private int? GetPointZoneId(List<Gen_Zone_PolyVertice> listofVertices, string postCode,int? selectedZoneId)
        {

           

            Gen_Coordinate objCoord = listOfCoordinates.FirstOrDefault(c => c.PostCode == postCode);

            if (objCoord != null)
            {
                if (FindPoint(Convert.ToDouble(objCoord.Latitude), Convert.ToDouble(objCoord.Longitude), listofVertices))
                    return selectedZoneId;
                else
                    return null;
            }
            else
                return null;

           
        }
     

        private void PopulatePlottedAddresses( int? SelectedZoneId)
        {

            char[] splitArr = new char[] { ' ' };

            string[] arrPriority = AppVars.objPolicyConfiguration.PriorityPostCodes.Split(new char[] { ',' });

            List<stp_GetFullAddressesResult> priorityAddresses = null;


            if (SelectedZoneId == -1)
            {
                priorityAddresses =new TaxiDataContext().stp_GetFullAddresses("","",-1).ToList();

            }
            else
            {
                priorityAddresses = addresses.Where(c => c.ZoneId == SelectedZoneId || c.ZoneId == null).OrderBy(c => c.PostalCode).ToList();

            }
            


            var listofVertices=listofPolyVertices.Where(c => c.ZoneId == SelectedZoneId).ToList();

            List<stp_GetFullAddressesResult> PlottedAddresses = (from a in priorityAddresses

                                                                 select new stp_GetFullAddressesResult
                                                                 {
                                                                     AddressLine1 = a.AddressLine1,
                                                                     PostalCode = a.PostalCode,
                                                                     ZoneId = a.ZoneId != null ? a.ZoneId : GetPointZoneId(listofVertices, a.PostalCode, SelectedZoneId)


                                                                 }).ToList();




            PlottedAddresses.RemoveAll(c => c.ZoneId == null);

           
            //int cnter = 0;
            //for (int i = 0; i < priorityAddresses.Count; i++)
            //{

            //    try
            //    {

            //        cnter++;


            //        if (priorityAddresses[i].ZoneId.ToInt() == 0)
            //        {


            //            Gen_Coordinate objCoord = listOfCoordinates.FirstOrDefault(c => c.PostCode == priorityAddresses[i].PostalCode);

            //            if (objCoord != null)
            //            {
            //                if (FindPoint(Convert.ToDouble(objCoord.Latitude), Convert.ToDouble(objCoord.Longitude), listofVertices))
            //                {
            //                    PlottedAddresses.Add(new stp_GetFullAddressesResult { AddressLine1 = priorityAddresses[i].AddressLine1, PostalCode = priorityAddresses[i].PostalCode, ZoneId = SelectedZoneId });
            //                }

            //            }
            //        }
            //        else
            //        {
            //             PlottedAddresses.Add(new stp_GetFullAddressesResult { AddressLine1 = priorityAddresses[i].AddressLine1, PostalCode = priorityAddresses[i].PostalCode, ZoneId = priorityAddresses[i].ZoneId });
            //        }

                

                 

            //    }
            //    catch
            //    {


            //    }
            //}



            int cnt = PlottedAddresses.Count;

            grdAddresses.RowCount = cnt;


            for (int i = 0; i < cnt; i++)
            {

                grdAddresses.Rows[i].Cells["FullAddress"].Value = PlottedAddresses[i].AddressLine1;
                grdAddresses.Rows[i].Cells["Street"].Value = PlottedAddresses[i].AddressLine1.Replace(PlottedAddresses[i].PostalCode, "").Trim();
                grdAddresses.Rows[i].Cells["PostCode"].Value = PlottedAddresses[i].PostalCode;
                grdAddresses.Rows[i].Cells["Zone"].Value = PlottedAddresses[i].ZoneId;
                grdAddresses.Rows[i].Cells["OldZoneId"].Value = PlottedAddresses[i].ZoneId;
            }

        }

        private void PopulateNodes()
        {




        }


        private void PopulateUnPlottedLocation(string postcode)
        {

            char[] splitArr = new char[] { ' ' };



            var priorityAddresses = listofLocations.Where(c => c.PostCode.Split(splitArr)[0] == postcode && c.ZoneId == null).OrderBy(c => c.PostCode).ToList();


            List<stp_GetFullAddressesResult> unPlottedAddresses = new List<stp_GetFullAddressesResult>();



       


            int? zoneId = null;
            int cnter = 0;
            for (int i = 0; i < priorityAddresses.Count; i++)
            {

                try
                {

                    cnter++;

                    zoneId = null;

                  //  string postCode = General.GetPostCode(priorityAddresses[i]..ToUpper());

                    Gen_Coordinate objCoord = listOfCoordinates.FirstOrDefault(c => c.PostCode == priorityAddresses[i].PostCode);


                    if (objCoord != null)
                    {

                        double latitude = 0, longitude = 0;

                        latitude = Convert.ToDouble(objCoord.Latitude);
                        longitude = Convert.ToDouble(objCoord.Longitude);



                        var plot = (from a in listofZones.Where(c => c.MinLatitude != null && (latitude >= c.MinLatitude && latitude <= c.MaxLatitude)
                                                           && (longitude <= c.MaxLongitude && longitude >= c.MinLongitude))
                                    orderby a.PlotKind

                                    select a.Id).ToArray<int>();


                        if (plot.Count() > 0)
                        {
                            var list = (from p in plot
                                        join a in listofPolyVertices on p equals a.ZoneId
                                        select a).ToList();


                            foreach (int plotId in plot)
                            {
                                if (FindPoint(latitude, longitude, list.Where(c => c.ZoneId == plotId).ToList()))
                                {
                                    zoneId = plotId;
                                    break;

                                }
                            }
                        }


                    }


                    // zoneId= General.GetZoneId(addresses[i].AddressLine1.ToStr().ToUpper());

                    if (zoneId == null)
                    {
                        unPlottedAddresses.Add(new stp_GetFullAddressesResult {  AddressLine1 = priorityAddresses[i].LocationName, PostalCode = priorityAddresses[i].PostCode, ZoneId = priorityAddresses[i].ZoneId });
                    }

                }
                catch
                {


                }
            }



            int cnt = unPlottedAddresses.Count;

            grdLocations.RowCount = cnt;


            for (int i = 0; i < cnt; i++)
            {

                grdLocations.Rows[i].Cells["Street"].Value = unPlottedAddresses[i].AddressLine1.Replace(unPlottedAddresses[i].PostalCode, "").Trim();
                grdLocations.Rows[i].Cells["PostCode"].Value = unPlottedAddresses[i].PostalCode;
                grdLocations.Rows[i].Cells["Zone"].Value = unPlottedAddresses[i].ZoneId;
                grdLocations.Rows[i].Cells["OldZoneId"].Value = unPlottedAddresses[i].ZoneId;
            }







        }




        private void PopulatePlottedLocations(int? SelectedZoneId)
        {

            char[] splitArr = new char[] { ' ' };

            string[] arrPriority = AppVars.objPolicyConfiguration.PriorityPostCodes.Split(new char[] { ',' });


            List<Gen_Location> priorityAddresses = null;

            if (SelectedZoneId == -1)
            {
                priorityAddresses = General.GetQueryable<Gen_Location>(C=>C.ZoneId!=null && C.LocationTypeId!=Enums.LOCATION_TYPES.POSTCODE).OrderBy(C=>C.PostCode).ToList();


            }
            else
            {

                priorityAddresses = listofLocations.Where(c => c.ZoneId == SelectedZoneId || c.ZoneId == null).OrderBy(c => c.PostCode).ToList();
            }

            List<stp_GetFullAddressesResult> PlottedAddresses = new List<stp_GetFullAddressesResult>();
            var listofVertices = listofPolyVertices.Where(c => c.ZoneId == SelectedZoneId).ToList();


            int cnter = 0;
            for (int i = 0; i < priorityAddresses.Count; i++)
            {

                try
                {

                    cnter++;

                    if (priorityAddresses[i].ZoneId.ToInt() == 0)
                    {

                        Gen_Coordinate objCoord = listOfCoordinates.FirstOrDefault(c => c.PostCode == priorityAddresses[i].PostCode);

                        if (objCoord != null)
                        {
                            if (FindPoint(Convert.ToDouble(objCoord.Latitude), Convert.ToDouble(objCoord.Longitude), listofVertices))
                            {
                                PlottedAddresses.Add(new stp_GetFullAddressesResult { AddressLine1 = priorityAddresses[i].LocationName, PostalCode = priorityAddresses[i].PostCode, ZoneId = SelectedZoneId });

                            }
                        }
                    }
                    else
                    {
                        PlottedAddresses.Add(new stp_GetFullAddressesResult { AddressLine1 = priorityAddresses[i].LocationName, PostalCode = priorityAddresses[i].PostCode, ZoneId = priorityAddresses[i].ZoneId });


                    }

                }
                catch
                {


                }
            }



            int cnt = PlottedAddresses.Count;

            grdLocations.RowCount = cnt;


            for (int i = 0; i < cnt; i++)
            {

                grdLocations.Rows[i].Cells["Street"].Value = PlottedAddresses[i].AddressLine1.Replace(PlottedAddresses[i].PostalCode, "").Trim();
                grdLocations.Rows[i].Cells["PostCode"].Value = PlottedAddresses[i].PostalCode;
                grdLocations.Rows[i].Cells["Zone"].Value = PlottedAddresses[i].ZoneId;
                grdLocations.Rows[i].Cells["OldZoneId"].Value = PlottedAddresses[i].ZoneId;
            }

        }


        public static bool FindPoint(double pointLat, double pointLng, List<Gen_Zone_PolyVertice> PontosPolig)
        {//                             X               y               
            int sides = PontosPolig.Count();
            int j = sides - 1;
            bool pointStatus = false;

            for (int i = 0; i < sides; i++)
            {
                if (PontosPolig[i].Longitude < pointLng && PontosPolig[j].Longitude >= pointLng ||
                    PontosPolig[j].Longitude < pointLng && PontosPolig[i].Longitude >= pointLng)
                {
                    if (PontosPolig[i].Latitude + (pointLng - PontosPolig[i].Longitude) /
                        (PontosPolig[j].Longitude - PontosPolig[i].Longitude) * (PontosPolig[j].Latitude - PontosPolig[i].Latitude) < pointLat)
                    {
                        pointStatus = !pointStatus;
                    }
                }
                j = i;
            }
            return pointStatus;
        }


        public static int? GetZoneId(string address)
        {
            if (address != "AS DIRECTED" && string.IsNullOrEmpty(General.GetPostCodeMatch(address)))
                return null;

            if (address.Contains(", UK"))
                address = address.Remove(address.LastIndexOf(", UK"));



            int? zoneId = null;

            try
            {
                if (address == "AS DIRECTED")
                {

                    zoneId = General.GetObject<Gen_Zone>(c => c.ZoneName == address).DefaultIfEmpty().Id;

                    if (zoneId == 0)
                        zoneId = null;
                }
                else
                {

                    zoneId = AppVars.listOfAddress.FirstOrDefault(c => c.AddressLine1.Contains(address.ToStr().ToUpper())).DefaultIfEmpty().ZoneId;
                    if (zoneId == null)
                    {

                        string postCode = General.GetPostCode(address);

                        Gen_Coordinate objCoord = General.GetObject<Gen_Coordinate>(c => c.PostCode == postCode);


                        if (objCoord != null)
                        {

                            double latitude = 0, longitude = 0;

                            latitude = Convert.ToDouble(objCoord.Latitude);
                            longitude = Convert.ToDouble(objCoord.Longitude);



                            var plot = (from a in General.GetQueryable<Gen_Zone>(c => c.MinLatitude != null && (latitude >= c.MinLatitude && latitude <= c.MaxLatitude)
                                                               && (longitude <= c.MaxLongitude && longitude >= c.MinLongitude))
                                        orderby a.PlotKind

                                        select a.Id).ToArray<int>();


                            if (plot.Count() > 0)
                            {
                                var list = (from p in plot
                                            join a in General.GetQueryable<Gen_Zone_PolyVertice>(null) on p equals a.ZoneId
                                            select a).ToList();




                                foreach (int plotId in plot)
                                {
                                    if (FindPoint(latitude, longitude, list.Where(c => c.ZoneId == plotId).ToList()))
                                    {
                                        zoneId = plotId;
                                        break;

                                    }
                                }
                            }
                            else
                            {

                                if (AppVars.objPolicyConfiguration.PriorityPostCodes.ToStr().Length > 0)
                                {
                                    string[] arr = AppVars.objPolicyConfiguration.PriorityPostCodes.Split(new char[] { ',' });



                                    if (objCoord.PostCode.ToStr().Contains(" ") && arr.Contains(objCoord.PostCode.Split(new char[] { ' ' })[0]))
                                    {


                                        var zone = (from a in General.GetQueryable<Gen_Zone_PolyVertice>(null).AsEnumerable()


                                                    select new
                                                    {

                                                        a.Gen_Zone.Id,
                                                        a.Gen_Zone.ZoneName,
                                                        DistanceMin = new LatLng(Convert.ToDouble(a.Latitude), Convert.ToDouble(a.Longitude)).DistanceMiles(new LatLng(Convert.ToDouble(objCoord.Latitude), Convert.ToDouble(objCoord.Longitude))),


                                                    }).OrderBy(c => c.DistanceMin).FirstOrDefault();



                                        if (zone != null)
                                            zoneId = zone.Id;
                                    }


                                }


                            }

                        }
                    }


                }


            }
            catch (Exception ex)
            {


            }

            return zoneId;

        }

        private void ShowLoading(bool show)
        {
          //  lblLoading.Visible = show;

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode node = e.Node;

                if (node != null)
                {
                    txtPlotName.Text = node.Text.ToStr().Trim();

                    int tag = node.Tag.ToInt();

                  

                    if (radPageView1.SelectedPage == pg_addresses)
                    {

                        if (tag == 0)
                        {
                            PopulateUnPlottedAddresses(node.Text.ToStr().ToUpper());
                        }

                        else
                        {

                            PopulatePlottedAddresses(tag);


                        }
                    }

                    else if (radPageView1.SelectedPage == pg_locations)
                    {



                        if (tag == 0)
                        {
                            PopulateUnPlottedLocation(node.Text.ToStr().ToUpper());
                        }
                        else
                        {
                            PopulatePlottedLocations(tag);

                        }

                    }

                  

                }
            }
            catch (Exception ex)
            {
            //    lblLoading.Visible = false;

            }


        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }


        private void SaveChanges()
        {
            try
            {
                string addressQuery = string.Empty;
                string locationQuery = string.Empty;

                foreach (var item in grdAddresses.Rows.Where(c => c.Cells["Zone"].Value != null && c.Cells["Zone"].Value.ToInt() != c.Cells["OldZoneId"].Value.ToInt()))
                {
                    addressQuery += "UPDATE GEN_ADDRESSES SET ZONEID=" + item.Cells["Zone"].Value.ToInt() + " where addressline1='" + item.Cells["FullAddress"].Value.ToStr()+ "';"+Environment.NewLine;


                    AppVars.listOfAddress.FirstOrDefault(c => c.AddressLine1 == item.Cells["FullAddress"].Value.ToStr() 
                                           ).DefaultIfEmpty().ZoneId = item.Cells["Zone"].Value.ToInt();

                }


                foreach (var item in grdLocations.Rows.Where(c => c.Cells["Zone"].Value != null && c.Cells["Zone"].Value.ToInt() != c.Cells["OldZoneId"].Value.ToInt()))
                {
                    locationQuery += "UPDATE GEN_LOCATIONS SET ZONEID=" + item.Cells["Zone"].Value.ToInt() + " where LOCATIONNAME='" + item.Cells["Street"].Value.ToStr() + "' AND POSTCODE='" + item.Cells["PostCode"].Value.ToStr() + "';"+Environment.NewLine;


                       
                }


                using (TaxiDataContext db = new TaxiDataContext())
                {

                    if (addressQuery.ToStr().Trim() != string.Empty)
                    {

                        db.stp_RunProcedure(addressQuery);
                    }

                    if (locationQuery.ToStr().Trim() != string.Empty)
                    {
                        db.stp_RunProcedure(locationQuery);
                    }
                }

                if (addressQuery.ToStr().Length > 0 || locationQuery.ToStr().Length > 0)
                {

                    RefreshRecords();
                }

            }
            catch(Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);


            }

        }

        private void RefreshRecords()
        {

            TreeNode node = treeView1.SelectedNode;

            if (node == null)
                return;


            int? tag = node.Tag.ToInt();

            ShowLoading(true);
            if (radPageView1.SelectedPage == pg_addresses)
            {
                if (tag == 0)
                {
                    PopulateUnPlottedAddresses(node.Text.ToStr().ToUpper());
                }
                else
                {
                    PopulatePlottedAddresses(tag);

                }

            }
            else if (radPageView1.SelectedPage == pg_locations)
            {


                this.listofLocations = GetLocationsList();



                if (tag == 0)
                {
                    PopulateUnPlottedLocation(node.Text.ToStr().ToUpper());
                }
                else
                {
                    PopulatePlottedLocations(tag);

                }

            }

            ShowLoading(false);
        }

    }
}
