using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Telerik.WinControls.UI;
using Utils;
using Taxi_BLL;
using DAL;


namespace Taxi_AppMain
{
    public partial class frmPlotTiming : UI.SetupBase
    {
    
        RadDropDownMenu contextMenu = null;
        ZoneBO objMaster = null;
        private void FormatGrid()
        {
           
            grdZones.AllowAddNewRow = false;
            grdZones.ShowGroupPanel = false;
           // grdZones.AutoCellFormatting = true;
            grdZones.ShowRowHeaderColumn = false;
           
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
            grdZones.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "IsUpdated";
            grdZones.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Plot #";
            col.Name = "PlotNo";
            col.ReadOnly = true;
            col.Width = 60;
            col.IsPinned = true;
            grdZones.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Plot Name";
            col.Name = "Plot";
            col.ReadOnly = true;
            col.Width = 230;
            col.IsPinned = true;
            grdZones.Columns.Add(col);


            var zoneList = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();

            GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot1";
            //colCombo.DataSource = General.GetGeneralList<Gen_SysPolicyDocumentsList>(null);
            colCombo.DataSource = zoneList;
            colCombo.HeaderText = "BackupPlot 1";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
          //  colCombo.NullValue = "Select";
            colCombo.Width = 205;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);




            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot2";
            colCombo.DataSource = zoneList.ToList();
         //   colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName =args.OrderNo + "."+ args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.HeaderText = "BackupPlot 2";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
      
            colCombo.Width = 205;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);




            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot3";
          //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 3";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";

            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);




            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot4";
          //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 4";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);

            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot5";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 5";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);

            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot6";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 6";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);


            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot7";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 7";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);

            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot8";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 8";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);

            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot9";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 9";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);



            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot10";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 10";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);


            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot11";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 11";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);


            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot12";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 12";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);


            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot13";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 13";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);


            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot14";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 14";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);

            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot15";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 15";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);

            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot16";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 16";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);

            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot17";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 17";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);

            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot18";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 18";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);

            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot19";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 19";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);

            colCombo = new GridViewComboBoxColumn();
            colCombo.Name = "BackupPlot20";
            //  colCombo.DataSource = AppVars.BLData.GetAll<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, ZoneName = args.OrderNo + "." + args.ZoneName + " (" + args.ShortName + ")" }).ToList();
            colCombo.DataSource = zoneList.ToList();
            colCombo.HeaderText = "BackupPlot 20";
            colCombo.DisplayMember = "ZoneName";
            colCombo.ValueMember = "Id";
            colCombo.Width = 200;
            colCombo.ReadOnly = false;
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdZones.Columns.Add(colCombo);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Entrance Message";
            col.Name = "PlotMessage";
            col.ReadOnly = false;
            col.Width = 150;
            grdZones.Columns.Add(col);


            GridViewDecimalColumn colLimit = new GridViewDecimalColumn();
            colLimit.HeaderText = "Limit";
            colLimit.Name = "Limit";
            colLimit.ReadOnly = false;
            colLimit.Width = 70;
            colLimit.Maximum = 1000;
            colLimit.Minimum = -1;
            colLimit.DecimalPlaces = 0;
            grdZones.Columns.Add(colLimit);







            col = new GridViewTextBoxColumn();
            col.HeaderText = "OverLimit Message";
            col.Name = "OverLimitMessage";
            col.ReadOnly = false;
            col.Width = 150;
            grdZones.Columns.Add(col);
            

            GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "Flashing Time";
            colDate.Name = "FlashingHour";
            colDate.Width = 130;
            colDate.ReadOnly = false;
            colDate.CustomFormat = "HH:mm";
            colDate.FormatString = "{0:HH:mm}";
            grdZones.Columns.Add(colDate);



             colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "Job Due";
            colDate.Name = "JobDue";
            colDate.Width = 100;
            colDate.ReadOnly = false;
            colDate.CustomFormat = "HH:mm";
            colDate.FormatString = "{0:HH:mm}";
            grdZones.Columns.Add(colDate);



            GridViewCheckBoxColumn colChk1 = new GridViewCheckBoxColumn();
            colChk1.HeaderText = "Out of Town";
            colChk1.Name = "outoftown";
            colChk1.ReadOnly = false;
            colChk1.Width = 120;
            grdZones.Columns.Add(colChk1);



            GridViewCheckBoxColumn colChk = new GridViewCheckBoxColumn();
            colChk.HeaderText = "AutoDespatch";
            colChk.Name = "AutoDespatch";
            colChk.ReadOnly = false;
            colChk.Width = 120;
            grdZones.Columns.Add(colChk);

            colChk = new GridViewCheckBoxColumn();
            colChk.HeaderText = "Bidding";
            colChk.Name = "Bidding";
            colChk.ReadOnly = false;
            colChk.Width = 80;
            grdZones.Columns.Add(colChk);


            GridViewDecimalColumn colRadius = new GridViewDecimalColumn();
            colRadius.HeaderText = "Radius";
            colRadius.Name = "Radius";
            colRadius.ReadOnly = false;
            colRadius.Width = 80;
            colRadius.Maximum = 1000;
            colRadius.Minimum = 0;
            colRadius.DecimalPlaces = 0;
            grdZones.Columns.Add(colRadius);

            grdZones.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
        
          
          
        }




        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {

                e.CellElement.DrawFill = false;

                if (e.CellElement is GridDataCellElement)
                {

                    if (e.Column.Name == "BackupPlot1")
                    {
                        if (e.Row.Cells["BackupPlot1"].Tag.ToBool())
                        {

                            e.CellElement.NumberOfColors = 1;
                            e.CellElement.BackColor = Color.LightGray;                          
                        }
                        else
                        {
                            e.CellElement.NumberOfColors = 1;
                            e.CellElement.BackColor = Color.White;
                          
                        }



                        e.CellElement.DrawFill = true;
                    }
                    else if (e.Column.Name == "BackupPlot2")
                    {
                        if (e.Row.Cells["BackupPlot2"].Tag.ToBool())
                        {

                            e.CellElement.NumberOfColors = 1;
                            e.CellElement.BackColor = Color.LightGray;
                        }
                        else
                        {
                            e.CellElement.NumberOfColors = 1;
                            e.CellElement.BackColor = Color.White;

                        }



                        e.CellElement.DrawFill = true;
                    }



                }
            }
            catch { }
        }


        public frmPlotTiming()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmPlotTiming_Load);
            grdZones.CellEndEdit += new GridViewCellEventHandler(grdZones_CellEndEdit);
            objMaster = new ZoneBO();


            this.SetProperties((INavigation)objMaster);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmPlotTiming_KeyDown);
            this.FormClosing += new FormClosingEventHandler(frmPlotTiming_FormClosing);
            this.grdZones.EnableSorting = false;
        
        }

        void frmPlotTiming_FormClosing(object sender, FormClosingEventArgs e)
        {

            this.Dispose(true);

        }

        private void frmPlotTiming_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CloseForm();
            }
            else if (e.KeyCode == Keys.Home)
            {
                Save();
                CloseForm();

            }

        }

        void grdZones_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            e.Row.Cells["IsUpdated"].Value = "1";

          


        }

        void frmPlotTiming_Load(object sender, EventArgs e)
        {
            try
            {

                FormatGrid();


                var list = (from a in General.GetQueryable<Gen_Zone>(c => c.MinLatitude != null)
                            orderby a.OrderNo
                            select new
                            {
                                a.Id,
                                a.OrderNo,
                                a.ZoneName,
                                a.ShortName, 
                                a.FlashingHour,
                                a.EnableAutoDespatch,
                                a.EnableBidding,
                                a.Gen_Zone_Backups,
                                a.BiddingRadius,
                                a.PlotLimit,
                                a.PlotEntranceMessage,
                                a.PlotLimitExceedMessage,
                                a.JobDueTime,
                                a.BlockDropOff
                            }).ToList();


                grdZones.RowCount = list.Count;

                for (int i = 0; i < list.Count; i++)
                {

                    grdZones.Rows[i].Cells["Id"].Value = list[i].Id;
                    grdZones.Rows[i].Cells["PlotNo"].Value = list[i].OrderNo.ToStr();
                    grdZones.Rows[i].Cells["Plot"].Value = list[i].ZoneName + " ("+list[i].ShortName.ToStr()+")";
                    grdZones.Rows[i].Cells["FlashingHour"].Value = list[i].FlashingHour.ToDateTime();
                    grdZones.Rows[i].Cells["JobDue"].Value = list[i].JobDueTime.ToDateTime();
                    grdZones.Rows[i].Cells["AutoDespatch"].Value = list[i].EnableAutoDespatch.ToBool();
                    grdZones.Rows[i].Cells["Bidding"].Value = list[i].EnableBidding.ToBool();
                    grdZones.Rows[i].Cells["outoftown"].Value = list[i].BlockDropOff.ToBool();

                    grdZones.Rows[i].Cells["Radius"].Value = list[i].BiddingRadius.ToInt();


                    if (list[i].Gen_Zone_Backups != null)
                    {

                        grdZones.Rows[i].Cells["BackupPlot1"].Value = list[i].Gen_Zone_Backups.BackupZone1Id;

                        if (list[i].Gen_Zone_Backups.BackupZone1Priority.ToBool())
                        {
                            if (list[i].Gen_Zone_Backups.BackupZone1Id != null)
                                grdZones.Rows[i].Cells["BackupPlot1"].Tag = true;

                            grdZones.Rows[i].Cells["BackupPlot2"].Tag = false;

                        }
                        else
                        {
                            grdZones.Rows[i].Cells["BackupPlot1"].Tag = false;

                            if (list[i].Gen_Zone_Backups.BackupZone1Id != null)
                                grdZones.Rows[i].Cells["BackupPlot2"].Tag = true;

                        }


                        grdZones.Rows[i].Cells["BackupPlot1"].Tag = list[i].Gen_Zone_Backups.BackupZone1Priority.ToBool();

                        grdZones.Rows[i].Cells["BackupPlot2"].Value = list[i].Gen_Zone_Backups.BackupZone2Id;

                        grdZones.Rows[i].Cells["BackupPlot3"].Value = list[i].Gen_Zone_Backups.BackupZone3Id;

                        grdZones.Rows[i].Cells["BackupPlot4"].Value = list[i].Gen_Zone_Backups.BackupZone4Id;

                        grdZones.Rows[i].Cells["BackupPlot5"].Value = list[i].Gen_Zone_Backups.BackupZone5Id;

                        grdZones.Rows[i].Cells["BackupPlot6"].Value = list[i].Gen_Zone_Backups.BackupZone6Id;

                        grdZones.Rows[i].Cells["BackupPlot7"].Value = list[i].Gen_Zone_Backups.BackupZone7Id;

                        grdZones.Rows[i].Cells["BackupPlot8"].Value = list[i].Gen_Zone_Backups.BackupZone8Id;

                        grdZones.Rows[i].Cells["BackupPlot9"].Value = list[i].Gen_Zone_Backups.BackupZone9Id;

                        grdZones.Rows[i].Cells["BackupPlot10"].Value = list[i].Gen_Zone_Backups.BackupZone10Id;

                        grdZones.Rows[i].Cells["BackupPlot11"].Value = list[i].Gen_Zone_Backups.BackupZone11Id;

                        grdZones.Rows[i].Cells["BackupPlot12"].Value = list[i].Gen_Zone_Backups.BackupZone12Id;

                        grdZones.Rows[i].Cells["BackupPlot13"].Value = list[i].Gen_Zone_Backups.BackupZone13Id;

                        grdZones.Rows[i].Cells["BackupPlot14"].Value = list[i].Gen_Zone_Backups.BackupZone14Id;

                        grdZones.Rows[i].Cells["BackupPlot15"].Value = list[i].Gen_Zone_Backups.BackupZone15Id;

                        grdZones.Rows[i].Cells["BackupPlot16"].Value = list[i].Gen_Zone_Backups.BackupZone16Id;

                        grdZones.Rows[i].Cells["BackupPlot17"].Value = list[i].Gen_Zone_Backups.BackupZone17Id;

                        grdZones.Rows[i].Cells["BackupPlot18"].Value = list[i].Gen_Zone_Backups.BackupZone18Id;

                        grdZones.Rows[i].Cells["BackupPlot19"].Value = list[i].Gen_Zone_Backups.BackupZone19Id;

                        grdZones.Rows[i].Cells["BackupPlot20"].Value = list[i].Gen_Zone_Backups.BackupZone20Id;
                    }

                    grdZones.Rows[i].Cells["Limit"].Value = list[i].PlotLimit.ToInt();
                    grdZones.Rows[i].Cells["PlotMessage"].Value = list[i].PlotEntranceMessage.ToStr();
                    grdZones.Rows[i].Cells["OverLimitMessage"].Value = list[i].PlotLimitExceedMessage.ToStr();


                }


                grdZones.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);

                if (grdZones.Rows.Count > 0)
                {
                    grdZones.CurrentRow = grdZones.Rows[0];

                }
            }
            catch (Exception ex)
            {


            }


        }



        void grdLister_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
                if (cell == null)
                    return;

                else if (cell.ColumnInfo.Name == "BackupPlot1" || cell.ColumnInfo.Name == "BackupPlot2")
                {

                    if (contextMenu == null)
                    {
                        contextMenu = new RadDropDownMenu();
                        contextMenu.BackColor = Color.Orange;

                        RadMenuItem item1 = new RadMenuItem("High Priority");
                        item1.ForeColor = Color.DarkBlue;
                        item1.BackColor = Color.Orange;
                        item1.Font = new Font("Tahoma", 10, FontStyle.Bold);

                        item1.Click += new EventHandler(item1_Click);
                        contextMenu.Items.Add(item1);


                        RadMenuItem item2 = new RadMenuItem("Mark All Bidding");
                        item2.ForeColor = Color.DarkBlue;
                        item2.BackColor = Color.Orange;
                        item2.Font = new Font("Tahoma", 10, FontStyle.Bold);

                        item2.Click += new EventHandler(item2_Click);
                        contextMenu.Items.Add(item2);


                        RadMenuItem item3 = new RadMenuItem("UnMark All Bidding");
                        item3.ForeColor = Color.DarkBlue;
                        item3.BackColor = Color.Orange;
                        item3.Font = new Font("Tahoma", 10, FontStyle.Bold);
                        item3.Click += new EventHandler(item3_Click);
                        contextMenu.Items.Add(item3);



                        RadMenuItem item4 = new RadMenuItem("Mark All AutoDespatch");
                        item4.ForeColor = Color.DarkBlue;
                        item4.BackColor = Color.Orange;
                        item4.Font = new Font("Tahoma", 10, FontStyle.Bold);

                        item4.Click += new EventHandler(item4_Click);
                        contextMenu.Items.Add(item4);


                        RadMenuItem item5 = new RadMenuItem("UnMark All AutoDespatch");
                        item5.ForeColor = Color.DarkBlue;
                        item5.BackColor = Color.Orange;
                        item5.Font = new Font("Tahoma", 10, FontStyle.Bold);
                        item5.Click += new EventHandler(item5_Click);
                        contextMenu.Items.Add(item5);
                    }

                    e.ContextMenu = contextMenu;
                    return;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }


        }


        void item1_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdZones.CurrentRow != null && grdZones.CurrentRow is GridViewDataRowInfo)
                {
                    grdZones.CurrentRow.Cells["BackupPlot1"].Tag = false;
                    grdZones.CurrentRow.Cells["BackupPlot2"].Tag = false;
                    if(grdZones.CurrentCell.ColumnInfo.Name=="BackupPlot1")
                        grdZones.CurrentRow.Cells["BackupPlot1"].Tag = true;  
                    else
                        grdZones.CurrentRow.Cells["BackupPlot2"].Tag = true;

                    grdZones.CurrentRow.Cells["IsUpdated"].Value = "1";


                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        void item2_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdZones.CurrentColumn != null)
                {

                    grdZones.Rows.ToList().ForEach(c => c.Cells["Bidding"].Value = true);
                    grdZones.Rows.ToList().ForEach(c => c.Cells["IsUpdated"].Value = "1");



                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }



        void item3_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdZones.CurrentColumn != null)
                {

                    grdZones.Rows.ToList().ForEach(c => c.Cells["Bidding"].Value = false);
                    grdZones.Rows.ToList().ForEach(c => c.Cells["IsUpdated"].Value = "1");



                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        void item4_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdZones.CurrentColumn != null)
                {

                    grdZones.Rows.ToList().ForEach(c => c.Cells["AutoDespatch"].Value = true);
                    grdZones.Rows.ToList().ForEach(c => c.Cells["IsUpdated"].Value = "1");



                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }



        void item5_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdZones.CurrentColumn != null)
                {

                    grdZones.Rows.ToList().ForEach(c => c.Cells["AutoDespatch"].Value = false);
                    grdZones.Rows.ToList().ForEach(c => c.Cells["IsUpdated"].Value = "1");



                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {

            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            try
            {
                //if(AppVars.objPolicyConfiguration.EnableAutoDespatch.ToBool())
                //{

                //    int nearestAutoDespatchRadius=AppVars.objPolicyConfiguration.AutoDespatchElapsedTime.ToInt();

                //    if (grdZones.Rows.Where(c => c.Cells["IsUpdated"].Value.ToStr() == "1"  && c.Cells["Radius"].Value.ToInt()>0)
                //               .Any(c => c.Cells["Radius"].Value.ToInt() < nearestAutoDespatchRadius))
                //    {

                //        ENUtils.ShowMessage("Bidding Radius must be greater than AutoDespatch Nearest Distance");
                //        return;
                //    }

                
                //}

                DateTime? dt = null;
             
                foreach (var item in grdZones.Rows.Where(c => c.Cells["IsUpdated"].Value.ToStr() == "1"))
                {
                    objMaster.GetByPrimaryKey(item.Cells["Id"].Value.ToInt());

                    if (objMaster.Current != null)
                    {
                        dt = item.Cells["FlashingHour"].Value.ToDateTimeorNull();

                        if(dt!=null && (dt.Value.Minute ==0  && dt.Value.Hour==0))
                            dt=null;

                        objMaster.Current.FlashingHour = dt;


                        dt = item.Cells["JobDue"].Value.ToDateTimeorNull();

                        if (dt != null && (dt.Value.Minute == 0 && dt.Value.Hour == 0))
                            dt = null;

                        objMaster.Current.JobDueTime = dt;

                        objMaster.Current.EnableAutoDespatch = item.Cells["AutoDespatch"].Value.ToBool();
                        objMaster.Current.EnableBidding = item.Cells["Bidding"].Value.ToBool();



                        objMaster.Current.BiddingRadius = item.Cells["Radius"].Value.ToInt();

                        if (item.Cells["BackupPlot1"].Value.ToIntorNull() != null && item.Cells["BackupPlot2"].Value.ToIntorNull() != null)
                        {
                            if (objMaster.Current.Gen_Zone_Backups == null)
                                objMaster.Current.Gen_Zone_Backups = new Gen_Zone_Backup();

                            objMaster.Current.Gen_Zone_Backups.BackupZone1Id = item.Cells["BackupPlot1"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone2Id = item.Cells["BackupPlot2"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone3Id = item.Cells["BackupPlot3"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone4Id = item.Cells["BackupPlot4"].Value.ToIntorNull();

                            objMaster.Current.Gen_Zone_Backups.BackupZone5Id = item.Cells["BackupPlot5"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone6Id = item.Cells["BackupPlot6"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone7Id = item.Cells["BackupPlot7"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone8Id = item.Cells["BackupPlot8"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone9Id = item.Cells["BackupPlot9"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone10Id = item.Cells["BackupPlot10"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone11Id = item.Cells["BackupPlot11"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone12Id = item.Cells["BackupPlot12"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone13Id = item.Cells["BackupPlot13"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone14Id = item.Cells["BackupPlot14"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone15Id = item.Cells["BackupPlot15"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone16Id = item.Cells["BackupPlot16"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone17Id = item.Cells["BackupPlot17"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone18Id = item.Cells["BackupPlot18"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone19Id = item.Cells["BackupPlot19"].Value.ToIntorNull();
                            objMaster.Current.Gen_Zone_Backups.BackupZone20Id = item.Cells["BackupPlot20"].Value.ToIntorNull();


                            if (item.Cells["BackupPlot1"].Tag == null && item.Cells["BackupPlot2"].Tag == null)
                                objMaster.Current.Gen_Zone_Backups.BackupZone1Priority = true;
                            else if (item.Cells["BackupPlot2"].Tag == null)
                                objMaster.Current.Gen_Zone_Backups.BackupZone1Priority = true;

                            else
                                objMaster.Current.Gen_Zone_Backups.BackupZone1Priority = item.Cells["BackupPlot1"].Tag.ToBool();

                        }
                        else
                        {
                            if (item.Cells["BackupPlot1"].Value.ToIntorNull() == null)
                            {

                                if (objMaster.Current.Gen_Zone_Backups == null)
                                    objMaster.Current.Gen_Zone_Backups = new Gen_Zone_Backup();

                                objMaster.Current.Gen_Zone_Backups.BackupZone1Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone2Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone3Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone4Id = null;

                                objMaster.Current.Gen_Zone_Backups.BackupZone5Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone6Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone7Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone8Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone9Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone10Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone11Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone12Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone13Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone14Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone15Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone16Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone17Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone18Id = null;

                                objMaster.Current.Gen_Zone_Backups.BackupZone19Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone20Id = null;

                            }
                            else
                            {

                                if (objMaster.Current.Gen_Zone_Backups == null)
                                    objMaster.Current.Gen_Zone_Backups = new Gen_Zone_Backup();

                                objMaster.Current.Gen_Zone_Backups.BackupZone1Id = item.Cells["BackupPlot1"].Value.ToIntorNull();
                                objMaster.Current.Gen_Zone_Backups.BackupZone1Priority = true;
                            }


                            if (item.Cells["BackupPlot2"].Value.ToIntorNull() == null)
                            {

                                if (objMaster.Current.Gen_Zone_Backups == null)
                                    objMaster.Current.Gen_Zone_Backups = new Gen_Zone_Backup();

                                objMaster.Current.Gen_Zone_Backups.BackupZone2Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone3Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone4Id = null;

                                objMaster.Current.Gen_Zone_Backups.BackupZone5Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone6Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone7Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone8Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone9Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone10Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone11Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone12Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone13Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone14Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone15Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone16Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone17Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone18Id = null;

                                objMaster.Current.Gen_Zone_Backups.BackupZone19Id = null;
                                objMaster.Current.Gen_Zone_Backups.BackupZone20Id = null;


                            }


                            //if (item.Cells["BackupPlot1"].Value.ToIntorNull() != null && item.Cells["BackupPlot2"].Value.ToIntorNull() == null)
                            //{
                            //    objMaster.Current.Gen_Zone_Backups.BackupZone2Id = null;

                            //}

                        }


                        // 9 dec 14

                        objMaster.Current.PlotEntranceMessage = item.Cells["PlotMessage"].Value.ToStr().Trim();
                        objMaster.Current.PlotLimit = item.Cells["Limit"].Value.ToInt();
                        objMaster.Current.PlotLimitExceedMessage = item.Cells["OverLimitMessage"].Value.ToStr();


                        objMaster.Current.BlockDropOff = item.Cells["outoftown"].Value.ToBool();
                        //

                        objMaster.CheckDataValidation = false;
                        objMaster.Save();
                    }
                }

                CloseForm();
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
           
        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
    }
}
