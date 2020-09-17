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
using System.Collections;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmAddFaresToWeb : UI.SetupBase
    {
        string Connection1 = General.DecryptConnectionString(Program.objLic.OnlineDataString.ToString());
        BackgroundWorker worker = null;
        StringBuilder sb = new StringBuilder();

        bool IsExporting = false;
        bool IsFareLoading = false;
        bool IsExportinError = false;
        int TotalAddress = 0;
        List<Fleet_VehicleType> VehicleList = new List<Fleet_VehicleType>();
        List<Fare_ChargesDetail> FareList = new List<Fare_ChargesDetail>();
        List<Fleet_VehicleType> listadd = new List<Fleet_VehicleType>();
        List<DataRow> listfinal = new List<DataRow>();
        DataTable dt = null;
        SqlConnection conn = null;
        SqlCommand cmd = null;
        Thread thread = null;
        // List<Gen_Location> ListOfPostCodes = null;
        public frmAddFaresToWeb()
        {
            InitializeComponent();
            FormatFaresGrid();
            grdFares.CellFormatting += new CellFormattingEventHandler(grdFares_CellFormatting);
            this.Load += new EventHandler(frmAddFaresToWeb_Load);

        }

        void grdFares_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Row.Cells[COLS.RowNo].Value.ToInt() == 1)
            {
                e.CellElement.NumberOfColors = 1;
                e.CellElement.BackColor = Color.LightGreen;
                e.CellElement.DrawFill = true;
            }
        }


        void frmAddFaresToWeb_Load(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(GetClientName));
            thread.Start();
            //    GetClientName();
            worker = new BackgroundWorker();


            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

        }

        int Counter = 0;
        private string GetPostCodeForFare(string Address)
        {
            string getpostcodefares = string.Empty;

            var getPostCode = General.GetObject<Gen_Location>(c => c.LocationName == Address);
            if (getPostCode != null)
            {
                getpostcodefares = getPostCode.PostCode;
            }
            else
            {
                getpostcodefares = General.GetPostCode(Address);
            }
            return getpostcodefares;


        }
        private void GetFares()
        {
            try
            {
                if (IsExporting == true)
                {
                    // GeClientName();
                    TotalAddress = grdFares.RowCount;
                    int RowNo = 0;


                    for (int i = 0; i < grdFares.RowCount; i++)
                    {
                        string FromPostCode = string.Empty;
                        string ToPostCode = string.Empty;
                        string FromAddress = string.Empty;
                        string ToAddress = string.Empty;


                        var rowsdata = (from row in dt.AsEnumerable()
                                        where row.Field<string>("Name").ToLower() == grdFares.Rows[i].Cells[COLS.VehicleType].Value.ToString().ToLower() && row.Field<long>("ClientID") == ClientID
                                        select row).ToList();
                        if (rowsdata.Any())
                        {

                            if (IsExportinError == true)
                            {
                                ExportFaresToWeb();
                            }
                            else
                            {
                                if (grdFares.Rows[i].Cells[COLS.RowNo].Value.ToInt() == 0)
                                {
                                    grdFares.Rows[i].Cells[COLS.RowNo].Value = 1;
                                    RowNo++;
                                    if (grdFares.Rows[i].Cells[COLS.OrignTypeId].Value.ToInt() == 8 || grdFares.Rows[i].Cells[COLS.OrignTypeId].Value.ToInt() == 1)
                                    {
                                        FromPostCode = GetPostCodeForFare(grdFares.Rows[i].Cells[COLS.FROMLOCATION].Value.ToString());

                                        FromAddress = grdFares.Rows[i].Cells[COLS.FROMLOCATION].Value.ToStr();
                                    }
                                    else if (grdFares.Rows[i].Cells[COLS.OrignTypeId].Value.ToInt() != 8 && grdFares.Rows[i].Cells[COLS.OrignTypeId].Value.ToInt() != 1)
                                    {
                                        FromPostCode = GetPostCodeForFare(grdFares.Rows[i].Cells[COLS.FROMLOCATION].Value.ToString());
                                        FromAddress = FromPostCode;
                                    }
                                    if (grdFares.Rows[i].Cells[COLS.DestinationTypeId].Value.ToInt() == 8 || grdFares.Rows[i].Cells[COLS.DestinationTypeId].Value.ToInt() == 1)
                                    {
                                        ToPostCode = GetPostCodeForFare(grdFares.Rows[i].Cells[COLS.TOLOCATION].Value.ToStr());

                                        ToAddress = grdFares.Rows[i].Cells[COLS.TOLOCATION].Value.ToStr();
                                    }
                                    else if (grdFares.Rows[i].Cells[COLS.DestinationTypeId].Value.ToInt() != 8 && grdFares.Rows[i].Cells[COLS.DestinationTypeId].Value.ToInt() != 1)
                                    {
                                        ToPostCode = GetPostCodeForFare(grdFares.Rows[i].Cells[COLS.TOLOCATION].Value.ToStr());

                                        ToAddress = ToPostCode;

                                    }
                                    if (FromAddress == "")
                                    {
                                        FromPostCode = GetPostCodeForFare(grdFares.Rows[i].Cells[COLS.FROMLOCATION].Value.ToStr());
                                        FromAddress = FromPostCode;


                                    }
                                    if (ToAddress == "")
                                    {
                                        ToPostCode = GetPostCodeForFare(grdFares.Rows[i].Cells[COLS.TOLOCATION].Value.ToStr());
                                        ToAddress = ToPostCode;

                                    }

                                    sb.Append("insert into FixFare ([From],[To],Fare,FromPostCode,ToPostCode,ClientId) values(" + "'" + FromAddress + "'" + "," + "'" + ToAddress + "'" + "," + grdFares.Rows[i].Cells[COLS.FARE].Value.ToDecimal() + "," + "'" + FromPostCode + "'" + "," + "'" + ToPostCode + "'" + "," + ClientID + "" + ")Select SCOPE_IDENTITY()" +
                                  "INSERT INTO VehicleFixeFare  ([ClientId],[FixeFareId],[VehicleID],[FARE],IsActive)values(" + ClientID + ",@@IDENTITY," + rowsdata[0].ItemArray[0] + "," + grdFares.Rows[i].Cells[COLS.FARE].Value.ToDecimal() + ",1)");
                                    //sb.Append("insert into FixFare ([From],[To],Fare,FromPostCode,ToPostCode,ClientId) values(" + "'" + FromAddress + "'" + "," + "'" + ToAddress + "'" + "," + grdFares.Rows[i].Cells[COLS.FARE].Value.ToDecimal() + "," + "'" + FromPostCode + "'" + "," + "'" + ToPostCode + "'" + "," + ClientID + "" + ")Select SCOPE_IDENTITY()" +
                                    //"INSERT INTO VehicleFixeFare  ([ClientId],[FixeFareId],[VehicleID],[FARE],IsActive)values(" + ClientID + ",@@IDENTITY," + listfinal[0].ItemArray[0] + "," + grdFares.Rows[i].Cells[COLS.FARE].Value.ToDecimal() + ",1)");                          
                                    this.Invoke(new MethodInvoker(delegate
                                    {
                                        lblExportingStatus.Text = "Exporting Fare " + (1 + i) + " out of " + TotalAddress + "";
                                    }));
                                    Counter++;

                                    if (grdFares.RowCount == Counter)
                                    {
                                        ExportFaresToWeb();
                                    }


                                }
                            }
                        }
                    }
                }
            }
            catch
            {

                if (IsExportinError == true)
                {
                    worker.CancelAsync();
                    worker.RunWorkerAsync();
                }
            }
        }
        int ClientID = 0;
        void GetClientName()
        {
            try
            {
                string Client = AppVars.objPolicyConfiguration.DefaultClientId.ToString();
                string ClientName = Client;
                conn = new SqlConnection();
                conn.ConnectionString = Connection1;
                conn.Open();
                dt = new DataTable();

                // For Client ID
                string ID = "select ID from Clients  where Name = '" + ClientName + "'";
                SqlDataAdapter dr = new SqlDataAdapter(ID, conn);
                dr.Fill(dt);
                ClientID = dt.Rows[0]["ID"].ToInt();

                conn.Close();
                GetVehicalIdFromWeb();

            }
            catch (Exception ex)
            {
                conn.Close();
                ENUtils.ShowErrorMessage(ex.Message);

            }

        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (IsClosed)
                return;

            if (this.InvokeRequired)
            {
                UIDelegate del = new UIDelegate(PopulateUI);
                this.BeginInvoke(del, FareList);
            }
            else
            {
                PopulateUI(FareList, VehicleList);
            }
        }
        delegate void UIDelegate(List<Fare_ChargesDetail> list, List<Fleet_VehicleType> Vehicle);
      //  bool hasError = false;
        bool IsClosed = false;
        private void PopulateUI(List<Fare_ChargesDetail> list, List<Fleet_VehicleType> Vehicle)
        {
            try
            {
                if (IsClosed)
                    return;
                if (IsFareLoading == true)
                {
                    int Count = list.Count;
                    grdFares.RowCount = Count;
                    grdFares.BeginUpdate();
                    for (int i = 0; i < grdFares.RowCount; i++)
                    {
                        grdFares.Rows[i].Cells[COLS.Id].Value = FareList[i].Id;
                        grdFares.Rows[i].Cells[COLS.FROMLOCATION].Value = list[i].FromAddress;
                        grdFares.Rows[i].Cells[COLS.TOLOCATION].Value = list[i].ToAddress;
                        grdFares.Rows[i].Cells[COLS.FARE].Value = list[i].Rate;
                        grdFares.Rows[i].Cells[COLS.RowNo].Value = 0;
                        grdFares.Rows[i].Cells[COLS.OrignTypeId].Value = list[i].OriginLocationTypeId;
                        grdFares.Rows[i].Cells[COLS.DestinationTypeId].Value = list[i].DestinationLocationTypeId;
                        grdFares.Rows[i].Cells[COLS.VehicleType].Value = VehicleList[i].VehicleType;
                        grdFares.Rows[i].Cells[COLS.VehicleId].Value = VehicleList[i].Id;




                    }
                    grdFares.EndUpdate();
                }


            }
            catch 
            {
             //   hasError = true;
            }
        }
        private void ExecuteQuery()
        {
            try
            {
                if (grdFares.RowCount > TotalAddress)
                    worker.RunWorkerAsync();
            }
            catch 
            { }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            VehicleFares();
        }
        public struct COLS
        {
            public static string Id = "Id";
            public static string FROMLOCATION = "FromLocation";
            public static string TOLOCATION = "ToLocation";
            public static string FARE = "Fare";
            public static string FromPostCode = "FromPostCode";
            public static string ToPostCode = "ToPostCode";
            public static string RowNo = "RowNo";
            public static string OrignTypeId = "OrignTypeId";
            public static string DestinationTypeId = "DestinationTypeId";
            public static string VehicleType = "VehicleType";
            public static string VehicleId = "VehicleId";
        }


        private void FormatFaresGrid()
        {
            grdFares.AllowAddNewRow = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdFares.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "VehicleId";
            col.Name = COLS.VehicleId;
            col.Width = 150;
            col.IsVisible = false;
            grdFares.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "VehicleType";
            col.Name = COLS.VehicleType;
            col.Width = 150;
            grdFares.Columns.Add(col);
            col.IsVisible = true;
            col = new GridViewTextBoxColumn();
            col.HeaderText = "From Location";
            col.Name = COLS.FROMLOCATION;
            col.Width = 350;
            grdFares.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.HeaderText = "To Location";
            col.Name = COLS.TOLOCATION;
            col.Width = 350;
            grdFares.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.RowNo;
            col.IsVisible = false;
            grdFares.Columns.Add(col);
            GridViewDecimalColumn colDec = new GridViewDecimalColumn();
            colDec.HeaderText = "Fare £";
            colDec.Width = 120;
            colDec.DecimalPlaces = 2;
            colDec.ThousandsSeparator = true;
            colDec.Name = COLS.FARE;
            grdFares.Columns.Add(colDec);
            grdFares.MasterTemplate.ShowRowHeaderColumn = false;

            col = new GridViewTextBoxColumn();
            col.HeaderText = "OrignTypeId";
            col.Name = COLS.OrignTypeId;
            col.Width = 350;
            col.IsVisible = false;
            grdFares.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "DestinationTypeId";
            col.Name = COLS.DestinationTypeId;
            col.Width = 350;
            grdFares.Columns.Add(col);
            col.IsVisible = false;
            UI.GridFunctions.SetFilter(grdFares);


        }

        private void VehicleFares()
        {
            try
            {
                FareList.Clear();
                foreach (var item in listadd)
                {
                    var Query = General.GetObject<Fare>(c => c.VehicleTypeId == item.Id && c.CompanyId == null);
                    if (Query != null)
                    {
                        var list = (from a in General.GetQueryable<Fare_ChargesDetail>(c => c.FareId == Query.Id)
                                    select new
                                    {
                                        Id = a.Id,
                                        FromLocation = a.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS ? a.FromAddress : a.Gen_Location1.LocationName,
                                        ToLocation = a.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS ? a.ToAddress : a.Gen_Location.LocationName,
                                        FARE = a.Rate,
                                        DestinationTypeID = a.DestinationLocationTypeId,
                                        OrigintypeId = a.OriginLocationTypeId,

                                    }).ToList();


                        foreach (var items in list)
                        {
                            FareList.Add(new Fare_ChargesDetail { Id = items.Id, FromAddress = items.FromLocation, ToAddress = items.ToLocation, Rate = items.FARE, OriginLocationTypeId = items.OrigintypeId, DestinationLocationTypeId = items.DestinationTypeID });
                            VehicleList.Add(new Fleet_VehicleType { Id = item.Id, VehicleType = item.VehicleType });
                        }

                    }

                }
                IsFareLoading = true;

                if (IsExporting == true)
                {
                    GetFares();
                }
              //  hasError = false;

            }
            catch 
            {
               // hasError = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            worker.Dispose();
        }

        private void btnVehicleFare_Click(object sender, EventArgs e)
        {
            lblExportingStatus.Text = string.Empty;
            IsExporting = false;
            IsFareLoading = true;
            worker.RunWorkerAsync();
        }

        private void btnExportToWeb_Click(object sender, EventArgs e)
        {
            try
            {
                IsFareLoading = false;
                IsExporting = true;
                worker.RunWorkerAsync();
            }
            catch 
            { }
        }

        private void ExportFaresToWeb()
        {
            try
            {
                using (conn = new SqlConnection(Connection1))
                {
                    conn.Open();
                    cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_InsertvehiclefixefareFromDispatch";
                    cmd.Parameters.Add("@ClientID", SqlDbType.Int).Value = ClientID;
                    cmd.Parameters.Add("@FareData", SqlDbType.VarChar).Value = sb.ToStr();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Counter = 0;
                    sb.Length = 0;
                    this.Invoke(new MethodInvoker(delegate
                    {
                        lblExportingStatus.Text = "Fares Uploaded Successfully";
                    }));

                  //  hasError = false;
                    IsExportinError = false;

                }
            }
            catch 
            {
              //  hasError = true;
                IsExportinError = true;
                conn.Close();
            }
        }

        private void GetVehicalIdFromWeb()
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    lblshowexport.Text += "These Vehicles are not exist in Web DataBase = ";
                }));

                conn = new SqlConnection(Connection1);
                conn.Open();
                //  var LIST=(from a in General.GetQueryable<Ve>)
                string Query = "Select ID,Name,ClientID from Vehicle ";
                SqlDataAdapter ad = new SqlDataAdapter(Query, conn);
                dt = new DataTable();

                //    List<DataRow> listrows = new List<DataRow>();

                ad.Fill(dt);
                var list = General.GetQueryable<Fleet_VehicleType>(c => c.VehicleType != null);
                foreach (var item in list)
                {

                    var rowsdata = (from row in dt.AsEnumerable()
                                    where row.Field<string>("Name").ToLower() == item.VehicleType.ToLower() && row.Field<long>("ClientID") == ClientID
                                    select row).ToList();
                    if (rowsdata.Any())
                    {
                        // listrows = rowsdata.AsEnumerable().ToList();
                        listfinal.Add(rowsdata[0]);
                        listadd.Add(item);
                    }

                    else
                    {
                        var Vehiclenotinlist = (from row in dt.AsEnumerable()
                                                where row.Field<string>("Name").ToLower() != item.VehicleType.ToLower() && row.Field<long>("ClientID") == ClientID
                                                select row).ToList();
                        int clientid = Vehiclenotinlist[0].ItemArray[2].ToInt();
                        if (Vehiclenotinlist.Any() && clientid == ClientID)
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                lblshowexport.Text += item.VehicleType + ", ";
                            }));

                        }

                    }

                }
                this.Invoke(new MethodInvoker(delegate
                {
                    worker.RunWorkerAsync();
                }));


            }
            catch 
            {

            }
        }



    }
}
