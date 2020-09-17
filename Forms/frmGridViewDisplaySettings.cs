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
using UI;
using Taxi_BLL;
using Taxi_Model;
using Telerik.WinControls.UI;
using System.Collections;
using System.Web.UI.WebControls;
using Telerik.WinControls;   

namespace Taxi_AppMain    
{
    public partial class frmGridViewDisplaySettings : UI.SetupBase
    {
        UM_Form_UserDefinedSettingsBO objUserDefinedSettings;


        IList listofTodaysBooking = null;
        int BookingHours = 0;
        private int DaysInTodayBooking = 0;

        Font regularFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font oldFont = new Font("Tahoma", 10, FontStyle.Bold);
        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);
        Font bigFont = new Font("Tahoma", 12, FontStyle.Bold);
        Font oldBLFont = new Font("Tahoma", 10, FontStyle.Regular);
        private Color selectedRowBackColor;
        private Color selectedRowForeColor;
        bool lockCompletedBooking;
        bool lockCancelledBooking;
        bool lockNoFareBooking;

        RadDropDownMenu BookingDashboard = null;
        RadDropDownMenu BookingList = null;
       
        public frmGridViewDisplaySettings()
        {
            InitializeComponent();


            objUserDefinedSettings = new UM_Form_UserDefinedSettingsBO();
            this.SetProperties((INavigation)objUserDefinedSettings);
            FormatDashboardGrid();
            FormatBookingListGrid();
            //FormatSearchColumnsGrid();
            //FormatSearchIndexGrid();

            if (ThemeResolutionService.ApplicationThemeName == "ControlDefault")
            {
                this.selectedRowBackColor = Color.DeepSkyBlue;
                this.selectedRowForeColor = Color.White;
            }
            else
            {
                this.selectedRowBackColor = Color.Empty;
                this.selectedRowForeColor = Color.Black;
            }
            grdTodayBooking.AllowEditRow = false;
            grdTodayBooking.AllowDeleteRow = false;
            grdLister.AllowEditRow = false;
            grdTodayBooking.AllowRowReorder = false;
            grdLister.AllowRowReorder = false;
            grdLister.AllowDeleteRow = false;
            grdLister.TableElement.RowHeight = 24;
            grdTodayBooking.TableElement.RowHeight =  AppVars.objPolicyConfiguration.GridRowSize.ToInt();
            this.grdTodayBooking.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.grdDashboardColumns.ValueChanging += new ValueChangingEventHandler(grdDashboardColumns_ValueChanging);
            this.grdBookingColumns.ValueChanging += new ValueChangingEventHandler(grdBookingColumns_ValueChanging);
            grdTodayBooking.ViewRowFormatting += new RowFormattingEventHandler(grdTodayBooking_ViewRowFormatting);
         //   this.grdTodayBooking.ViewCellFormatting += new CellFormattingEventHandler(grdTodayBooking_ViewCellFormatting);
        //    this.grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);

            this.Shown += new EventHandler(frmGridViewDisplaySettings_Shown);

            this.grdSearchColumns.ValueChanging += new ValueChangingEventHandler(grdSearchColumns_ValueChanging);
           // this.grdSearch.ViewCellFormatting += new CellFormattingEventHandler(grdSearch_ViewCellFormatting);
            this.btnMoveDownSearch.Click += new EventHandler(btnMoveDownSearch_Click);
            this.btnMoveUpSearch.Click += new EventHandler(btnMoveUpSearch_Click);
            this.grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);
            this.grdTodayBooking.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdTodayBooking_ContextMenuOpening);
        }
         public void AddDummyData(RadGridView Grid)
        {
            try
            {
                GridViewRowInfo row = Grid.Rows.AddNew();
               // row = Grid.Rows.AddNew();
                row.Cells["RefNumber"].Value = "TX101";

                row.Cells["From"].Value = "Test";
                row.Cells["To"].Value = "Test";
                row.Cells["Fare"].Value = 0;
                row.Cells["Passenger"].Value = "Test";
                row.Cells["Driver"].Value = "Drv-101";
                row.Cells["Vehicle"].Value = "Saloon";

            }
            catch (Exception ex)
            { 
            
            }
        }

        void grdTodayBooking_ViewRowFormatting(object sender, RowFormattingEventArgs e)
        {

            if (e.RowElement is GridDataRowElement)
            {



                e.RowElement.ForeColor = Color.Black;




                if (e.RowElement.RowInfo.Cells["Account"].Value.ToStr() != string.Empty)
                {

                    e.RowElement.Font = oldFont;

                    if (AppVars.AppTheme == "ControlDefault")
                    {
                        if (e.RowElement.IsSelected == true)
                        {

                            e.RowElement.NumberOfColors = 1;
                            e.RowElement.BackColor = this.selectedRowBackColor;

                            e.RowElement.NumberOfColors = 1;
                            e.RowElement.BackColor = this.selectedRowBackColor;
                            e.RowElement.ForeColor = this.selectedRowForeColor;
                            e.RowElement.Font = newFont;
                            e.RowElement.DrawFill = true;
                        }


                        else
                        {
                            e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.TwoWayBindingLocal);
                            e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.TwoWayBindingLocal);

                            if (e.RowElement.BackColor == this.selectedRowBackColor)
                                e.RowElement.DrawFill = false;



                            e.RowElement.NumberOfColors = 1;





                            string Bgcolor = e.RowElement.RowInfo.Cells["BackgroundColor1"].Value.ToStr().Trim();
                            string textColor = e.RowElement.RowInfo.Cells["TextColor1"].Value.ToStr().Trim();

                            if (Bgcolor != string.Empty && textColor != string.Empty)
                            {



                                e.RowElement.BackColor = Color.FromArgb(Bgcolor.ToInt());
                                e.RowElement.ForeColor = Color.FromArgb(textColor.ToInt());

                            }
                            else
                            {
                                e.RowElement.ForeColor = Color.White;
                                e.RowElement.BackColor = Color.Crimson;


                            }



                            //     e.RowElement.DrawFill = true;
                        }
                    }





                    e.RowElement.DrawFill = true;



                }
                else
                {

                    e.RowElement.NumberOfColors = 1;

                    e.RowElement.Font = oldFont;

                    if (AppVars.AppTheme == "ControlDefault")
                    {
                        if (e.RowElement.IsSelected == true)
                        {

                            e.RowElement.DrawFill = true;

                            e.RowElement.NumberOfColors = 1;
                            e.RowElement.BackColor = this.selectedRowBackColor;
                            e.RowElement.ForeColor = this.selectedRowForeColor;
                            e.RowElement.Font = newFont;

                        }


                        else
                        {
                            //e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.TwoWayBindingLocal);

                            //if (e.RowElement.BackColor == this.selectedRowBackColor)
                            //    e.RowElement.DrawFill = false;


                            e.RowElement.BackColor = Color.White;
                            e.RowElement.ForeColor = Color.Black;
                            e.RowElement.DrawFill = true;
                        }
                    }



                    //if (e.Column.Name == "RefNumber" || e.Column.Name == "BookingDate")
                    //{

                    //  e.CellElement.BackColor = Color.White;
                    //   e.CellElement.NumberOfColors = 1;

                    if (e.RowElement.RowInfo.Cells["BookingBackgroundColor"].Value != null)
                    {

                        e.RowElement.RowInfo.Cells["PickupDate"].Style.BackColor = Color.FromArgb((e.RowElement.RowInfo.Cells["BookingBackgroundColor"].Value.ToInt()));
                        e.RowElement.RowInfo.Cells["PickupDate"].Style.CustomizeFill = true;
                    }


                    //  }


                }







                string Bggcolor = e.RowElement.RowInfo.Cells["BackgroundColor"].Value.ToStr().Trim();
                string texttColor = e.RowElement.RowInfo.Cells["TextColor"].Value.ToStr().Trim();

                if (Bggcolor != string.Empty && texttColor != string.Empty)
                {
                    e.RowElement.RowInfo.Cells["Vehicle"].Style.BackColor = Color.FromArgb(Bggcolor.ToInt());
                    e.RowElement.RowInfo.Cells["Vehicle"].Style.ForeColor = Color.FromArgb(texttColor.ToInt());
                    e.RowElement.RowInfo.Cells["Vehicle"].Style.CustomizeFill = true;


                }



                if (e.RowElement.RowInfo.Cells["FromLocTypeId"].Value.ToStr() != "" && e.RowElement.RowInfo.Cells["FromLocTypeId"].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                {
                    e.RowElement.RowInfo.Cells["From"].Style.BackColor = Color.GreenYellow;
                    e.RowElement.RowInfo.Cells["From"].Style.CustomizeFill = true;
                }

                if (e.RowElement.RowInfo.Cells["ToLocTypeId"].Value.ToStr() != "" && e.RowElement.RowInfo.Cells["ToLocTypeId"].Value.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                {
                    e.RowElement.RowInfo.Cells["To"].Style.BackColor = Color.GreenYellow;
                    e.RowElement.RowInfo.Cells["To"].Style.CustomizeFill = true;
                }

                if (e.RowElement.RowInfo.Cells["StatusColor"].Value.ToStr() != string.Empty)
                {

                    e.RowElement.RowInfo.Cells["Status"].Style.BackColor = Color.FromArgb(e.RowElement.RowInfo.Cells["StatusColor"].Value.ToInt());
                    e.RowElement.RowInfo.Cells["Status"].Style.CustomizeFill = true;
                }
            }

        }

        void grdTodayBooking_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                GridHeaderCellElement Headcell = e.ContextMenuProvider as GridHeaderCellElement;

                GridDataCellElement Rowcell = e.ContextMenuProvider as GridDataCellElement;
                
                if (Headcell != null && Headcell.GridControl.Name == "grdTodayBooking")
                {

                    if (BookingDashboard == null)
                    {
                        BookingDashboard = new RadDropDownMenu();
                        BookingDashboard.BackColor = Color.Orange;

                        RadMenuItem BDMoveLeft = new RadMenuItem("Move Left");
                        BDMoveLeft.ForeColor = Color.DarkBlue;
                        BDMoveLeft.BackColor = Color.Orange;
                        BDMoveLeft.Font = new Font("Tahoma", 10, FontStyle.Bold);

                        BDMoveLeft.Click += new EventHandler(BDMoveLeft_Click);
                        BookingDashboard.Items.Add(BDMoveLeft);


                        RadMenuItem BDMoveRight = new RadMenuItem("Move Right");
                        BDMoveRight.ForeColor = Color.DarkBlue;
                        BDMoveRight.BackColor = Color.Orange;
                        BDMoveRight.Font = new Font("Tahoma", 10, FontStyle.Bold);
                     
                        BDMoveRight.Click += new EventHandler(BDMoveRight_Click);
                        BookingDashboard.Items.Add(BDMoveRight);


                    }

                    e.ContextMenu = BookingDashboard;
                    return;
                }
                if (Rowcell==null)
                {
                    e.Cancel = true;
                    return;

                }
                else if (Rowcell.GridControl.Name == "grdTodayBooking")
                {

                    if (BookingDashboard == null)
                    {
                        BookingDashboard = new RadDropDownMenu();
                        BookingDashboard.BackColor = Color.Orange;

                        RadMenuItem BDMoveLeft = new RadMenuItem("Move Left");
                        BDMoveLeft.ForeColor = Color.DarkBlue;
                        BDMoveLeft.BackColor = Color.Orange;
                        BDMoveLeft.Font = new Font("Tahoma", 10, FontStyle.Bold);

                        BDMoveLeft.Click += new EventHandler(BDMoveLeft_Click);
                        BookingDashboard.Items.Add(BDMoveLeft);


                        RadMenuItem BDMoveRight = new RadMenuItem("Move Right");
                        BDMoveRight.ForeColor = Color.DarkBlue;
                        BDMoveRight.BackColor = Color.Orange;
                        BDMoveRight.Font = new Font("Tahoma", 10, FontStyle.Bold);
                        // MoveRight.Click 
                        BDMoveRight.Click += new EventHandler(BDMoveRight_Click);
                        BookingDashboard.Items.Add(BDMoveRight);


                    }

                    e.ContextMenu = BookingDashboard;
                    return;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void BDMoveLeft_Click(object sender, EventArgs e)
        {
            try
            {
                int Index = grdTodayBooking.CurrentColumn.Index;
                if (Index > 0)
                {
                    int NewIndex = (Index - 1);
                    this.grdTodayBooking.Columns.Move(Index, NewIndex);
                 
                }
                this.grdTodayBooking.CurrentColumn = null;
                if (grdTodayBooking.RowCount > 0)
                {
                    grdTodayBooking.CurrentRow = null;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void BDMoveRight_Click(object sender, EventArgs e)
        {
            try
            {
                int Index = grdTodayBooking.CurrentColumn.Index;
                int TotalVisibleColumn = grdTodayBooking.Columns.Count(c => c.IsVisible == true); ;
                int NewIndex = (Index + 1);
                if (TotalVisibleColumn == NewIndex)
                {
                    return;

                }

                this.grdTodayBooking.Columns.Move(Index, NewIndex);
               // this.grdTodayBooking.Columns.Move(NewIndex, Index);   
                this.grdTodayBooking.CurrentColumn = null;
                if (grdTodayBooking.RowCount > 0)
                {
                    grdTodayBooking.CurrentRow = null;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void grdLister_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                GridHeaderCellElement Headcell = e.ContextMenuProvider as GridHeaderCellElement;

                GridDataCellElement Rowcell = e.ContextMenuProvider as GridDataCellElement;
               
                if (Headcell != null && Headcell.GridControl.Name == "grdLister")
                {

                    if (BookingList == null)
                    {
                        BookingList = new RadDropDownMenu();
                        BookingList.BackColor = Color.Orange;

                        RadMenuItem BLMoveLeft = new RadMenuItem("Move Left");
                        BLMoveLeft.ForeColor = Color.DarkBlue;
                        BLMoveLeft.BackColor = Color.Orange;
                        BLMoveLeft.Font = new Font("Tahoma", 10, FontStyle.Bold);

                        BLMoveLeft.Click += new EventHandler(BLMoveLeft_Click);
                        BookingList.Items.Add(BLMoveLeft);


                        RadMenuItem BLMoveRight = new RadMenuItem("Move Right");
                        BLMoveRight.ForeColor = Color.DarkBlue;
                        BLMoveRight.BackColor = Color.Orange;
                        BLMoveRight.Font = new Font("Tahoma", 10, FontStyle.Bold);
                       // MoveRight.Click 
                        BLMoveRight.Click += new EventHandler(BLMoveRight_Click);
                        BookingList.Items.Add(BLMoveRight);


                    }

                    e.ContextMenu = BookingList;
                    return;
                }
                if (Rowcell == null)
                {
                    e.Cancel = true;
                    return;

                }
                if (Rowcell != null && Rowcell.GridControl.Name == "grdLister")
                {

                    if (BookingList == null)
                    {
                        BookingList = new RadDropDownMenu();
                        BookingList.BackColor = Color.Orange;

                        RadMenuItem BLMoveLeft = new RadMenuItem("Move Left");
                        BLMoveLeft.ForeColor = Color.DarkBlue;
                        BLMoveLeft.BackColor = Color.Orange;
                        BLMoveLeft.Font = new Font("Tahoma", 10, FontStyle.Bold);

                        BLMoveLeft.Click += new EventHandler(BLMoveLeft_Click);
                        BookingList.Items.Add(BLMoveLeft);


                        RadMenuItem BLMoveRight = new RadMenuItem("Move Right");
                        BLMoveRight.ForeColor = Color.DarkBlue;
                        BLMoveRight.BackColor = Color.Orange;
                        BLMoveRight.Font = new Font("Tahoma", 10, FontStyle.Bold);
                        // MoveRight.Click 
                        BLMoveRight.Click += new EventHandler(BLMoveRight_Click);
                        BookingList.Items.Add(BLMoveRight);


                    }

                    e.ContextMenu = BookingList;
                    return;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void BLMoveLeft_Click(object sender, EventArgs e)
        {
            try
            {
                int Index = grdLister.CurrentColumn.Index;
                if (Index > 0)
                {
                    int NewIndex = (Index - 1);
                    this.grdLister.Columns.Move(Index, NewIndex);
                }
                this.grdLister.CurrentColumn = null;
                if (grdLister.RowCount > 0)
                {
                    grdLister.CurrentRow = null;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void BLMoveRight_Click(object sender, EventArgs e)
        {
            try
            {
                int Index = grdLister.CurrentColumn.Index;
                int TotalVisibleColumn = grdLister.Columns.Count(c => c.IsVisible == true); ;
                
                    int NewIndex = (Index + 1);
                    if (TotalVisibleColumn == NewIndex)
                    {
                        return;
                    }
                    this.grdLister.Columns.Move(Index, NewIndex);


                    this.grdLister.CurrentColumn = null;
                    if (grdLister.RowCount > 0)
                    {
                        grdLister.CurrentRow = null;
                    }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

      

        void btnMoveUpSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdSearchColumnIndex.CurrentRow != null)
                {
                    string ColumnName = grdSearchColumnIndex.CurrentRow.Cells["ColumnName"].Value.ToStr();
                    int Index = grdSearchColumnIndex.CurrentRow.Index;
                    int TotalRows = (grdSearchColumnIndex.RowCount - 1);
                    if (Index > 0)
                    {
                        int NewIndex = (Index - 1);
                        this.grdSearchColumnIndex.Rows.Move(Index, NewIndex);
                        var grdObj = grdSearch.Columns.Where(c => (c.Name == ColumnName) && (c.IsVisible == true)).ToList();
                        int OldIndex = grdSearch.Columns[ColumnName].Index;

                        this.grdSearch.Columns.Move(OldIndex, NewIndex);
                        string OldColumn = "";// grdObj.Where(c => c.Index == NewIndex).FirstOrDefault().Name; //grdTodayBooking.Columns.Where(c => c.Index == OldIndex).FirstOrDefault().Name;
                        OldIndex--;
                        for (int i = 0; i < grdObj.Count; i++)
                        {
                            if (grdObj[i].Index == OldIndex)
                            {
                                OldColumn = grdObj[i].Name;
                                break;
                            }
                        }
                        if (!string.IsNullOrEmpty(OldColumn))
                        {
                            this.grdSearch.Columns.Move(grdSearch.Columns[OldColumn].Index, (OldIndex));
                        }
                    }
                }
              
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void btnMoveDownSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdSearchColumnIndex.CurrentRow != null)
                {
                    string ColumnName = grdSearchColumnIndex.CurrentRow.Cells["ColumnName"].Value.ToStr();
                    int Index = grdSearchColumnIndex.CurrentRow.Index;
                    int TotalRows = (grdSearchColumnIndex.RowCount - 1);
                    if (Index >= 0 && Index != TotalRows)
                    {
                        int NewIndex = (Index + 1);
                        this.grdSearchColumnIndex.Rows.Move(Index, NewIndex);
                        var grdObj = grdSearch.Columns.Where(c => (c.Name == ColumnName) && (c.IsVisible == true)).ToList();
                        int OldIndex = grdSearch.Columns[ColumnName].Index;

                        this.grdSearch.Columns.Move(OldIndex, NewIndex);
                        string OldColumn = ""; //grdTodayBooking.Columns.Where(c => c.Index == NewIndex).FirstOrDefault().Name;
                        OldIndex++;
                        for (int i = 0; i < grdObj.Count; i++)
                        {
                            if (grdObj[i].Index == OldIndex)
                            {
                                OldColumn = grdObj[i].Name;
                                break;
                            }
                        }
                        if (!string.IsNullOrEmpty(OldColumn))
                        {
                            this.grdSearch.Columns.Move(grdSearch.Columns[OldColumn].Index, (OldIndex));
                        }

                    }
                }
             
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
      
     
        void grdSearchColumns_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            try
            {
                if (grdSearchColumns.CurrentRow is GridViewRowInfo)
                {
                    int Id = this.grdSearchColumns.CurrentRow.Cells[COLS.Id].Value.ToInt();
                    int FormId = this.grdSearchColumns.CurrentRow.Cells[COLS.FormId].Value.ToInt();

                    //string HeaderText = grdBookingColumns.CurrentRow.Cells[COLSBOOKINGLIST.HeaderText].Value.ToStr();
                    int ColumnIndex = this.grdSearchColumns.CurrentRow.Cells[COLS.GridColMoveTo].Value.ToInt();

                    bool ColumnStatus = this.grdSearchColumns.CurrentRow.Cells[COLS.ShowColumn].Value.ToBool();

                    string columnName = this.grdSearchColumns.CurrentRow.Cells[COLS.ColumnName].Value.ToStr();

                    if (string.IsNullOrEmpty(PriviousSearchColumn) || PriviousSearchColumn != columnName)
                    {
                        if (ColumnStatus == true)
                        {
                            ColumnStatus = false;
                        }
                        else
                        {
                            ColumnStatus = true;
                        }
                    }
                    else if (PriviousSearchColumn == columnName)
                    {
                        RadCheckBoxEditor chEditor = this.grdSearchColumns.ActiveEditor as RadCheckBoxEditor; ;
                        if (chEditor != null)
                        {
                            bool isChecked = Convert.ToBoolean(chEditor.Value);
                            if (isChecked == true)
                            {
                                ColumnStatus = false;
                            }
                            else
                            {
                                ColumnStatus = true;
                            }
                        }
                    }
                    if (ColumnStatus == false)
                    {
                        //DeleteRowFromSearchColumnIndex(Id);
                    }
                    else
                    {
                       // AddRowToSearchColumnIndex(Id, FormId, columnName, columnName, ColumnStatus, ColumnIndex);
                    }

                    for (int i = 0; i < grdSearch.RowCount; i++)
                    {
                        if (this.grdSearch.Columns.Contains(columnName))
                        {
                            this.grdSearch.Columns[columnName].IsVisible = ColumnStatus;
                            int OldIndex = this.grdSearch.Columns[columnName].Index;
                            int NewIndex = this.grdSearch.Columns.Where(c => c.IsVisible == true).Count();
                            this.grdSearch.Columns.Move(OldIndex, NewIndex);
                            break;
                        }
                    }
                    PriviousSearchColumn = columnName;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        string PriviousSearchColumn = string.Empty;
        
       
      
        private void FormatSearchColumnsGrid()
        {
            grdSearchColumns.AllowRowReorder = false;
            grdSearchColumns.ShowGroupPanel = false;
            grdSearchColumns.AllowAddNewRow = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdSearchColumns.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.FormId;
            col.IsVisible = false;
            grdSearchColumns.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.ColumnName;
            col.HeaderText = "Column Name";
            col.ReadOnly = true;
            col.Width = 200;
            grdSearchColumns.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.GridColMoveTo;
            col.IsVisible = false;
            grdSearchColumns.Columns.Add(col);
            GridViewCheckBoxColumn ckcol = new GridViewCheckBoxColumn();
            ckcol.Width = 150;
            ckcol.Name = COLS.ShowColumn;
            ckcol.HeaderText = "Show Column";
            ckcol.ReadOnly = false;
            grdSearchColumns.Columns.Add(ckcol);
            GridViewDecimalColumn dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.Width;
            dcol.HeaderText = "Width";
            dcol.Width = 100;
            dcol.IsVisible = false;
            grdSearchColumns.Columns.Add(dcol);

        }

        public void FormatSearchIndexGrid()
        {
            grdSearchColumnIndex.AllowRowReorder = false;
            grdSearchColumnIndex.ShowGroupPanel = false;
            grdSearchColumnIndex.AllowAddNewRow = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdSearchColumnIndex.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.FormId;
            col.IsVisible = false;
            grdSearchColumnIndex.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.ColumnName;
            col.HeaderText = "Column Name";
            col.ReadOnly = true;
            col.Width = 150;
            grdSearchColumnIndex.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.HeaderText;
            col.HeaderText = "Header Text";
            col.ReadOnly = false;
            col.Width = 150;
            grdSearchColumnIndex.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.GridColMoveTo;
            col.IsVisible = false;
            col.ReadOnly = false;
            grdSearchColumnIndex.Columns.Add(col);
        }


        public struct COLS
        {
            public static string Id = "Id";
            public static string FormId = "FormId";
            public static string ShowColumn = "ShowColumn";
            public static string ColumnName = "ColumnName";
            public static string HeaderText = "HeaderText";
            public static string GridColMoveTo = "GridColMoveTo";
            public static string Width = "Width";

        }

        private void FormatDashboardGrid()
        {
            grdDashboardColumns.AllowDeleteRow = false;
            grdDashboardColumns.AllowRowReorder = false;
            grdDashboardColumns.ShowGroupPanel = false;
            grdDashboardColumns.AllowAddNewRow = false;
            GridViewCheckBoxColumn ckcol = new GridViewCheckBoxColumn();
            ckcol.Width = 80;
            ckcol.Name = COLS.ShowColumn;
            ckcol.HeaderText = "Show";
            grdDashboardColumns.Columns.Add(ckcol);
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdDashboardColumns.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.FormId;
            col.IsVisible = false;
            grdDashboardColumns.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.ColumnName;
            col.HeaderText = "Column Name";
            col.ReadOnly = true;
            col.Width = 200;
            grdDashboardColumns.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.GridColMoveTo;
            col.IsVisible = false;
            grdDashboardColumns.Columns.Add(col);
         
            GridViewDecimalColumn dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.Width;
            dcol.HeaderText = "Width";
            dcol.Width = 100;
            dcol.IsVisible = false;
            grdDashboardColumns.Columns.Add(dcol);

        }
        public void FormatBookingListGrid()
        {
            grdBookingColumns.AllowDeleteRow = false;
            grdBookingColumns.AllowRowReorder = false;
            grdBookingColumns.ShowGroupPanel = false;
            grdBookingColumns.AllowAddNewRow = false;
            GridViewCheckBoxColumn ckcol = new GridViewCheckBoxColumn();
            ckcol.Width = 80;
            ckcol.Name = COLS.ShowColumn;
            ckcol.HeaderText = "Show";
            grdBookingColumns.Columns.Add(ckcol);
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdBookingColumns.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.FormId;
            col.IsVisible = false;
            grdBookingColumns.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.ColumnName;
            col.HeaderText = "Column Name";
            col.ReadOnly = true;
            col.Width = 200;
            grdBookingColumns.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.GridColMoveTo;
            col.IsVisible = false;
            grdBookingColumns.Columns.Add(col);
           

        }
        

        void frmGridViewDisplaySettings_Shown(object sender, EventArgs e)
        {
            this.radPageViewPage3.Item.Visibility = ElementVisibility.Collapsed;
                //                pageAirportCommission.Item.Visibility = ElementVisibility.Visible;
            PopulateSettingColumnsGrid();
            PopulateData();
        }
        private void PopulateSettingColumnsGrid()
        {
            PopulateDashboardColumns();
            PopulateBookingListColumns();
            //PopulateSearchColumns();
        }
        




  
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



     

        void grdBookingColumns_ValueChanging(object sender, EventArgs e)
        {
            try
            {
                if (grdBookingColumns.CurrentRow is GridViewRowInfo)
                {
                    int Id = grdBookingColumns.CurrentRow.Cells[COLS.Id].Value.ToInt();
                    int FormId = grdBookingColumns.CurrentRow.Cells[COLS.FormId].Value.ToInt();

                    //string HeaderText = grdBookingColumns.CurrentRow.Cells[COLSBOOKINGLIST.HeaderText].Value.ToStr();
                    int ColumnIndex = grdBookingColumns.CurrentRow.Cells[COLS.GridColMoveTo].Value.ToInt();

                    bool ColumnStatus = grdBookingColumns.CurrentRow.Cells[COLS.ShowColumn].Value.ToBool();

                    string columnName = grdBookingColumns.CurrentRow.Cells[COLS.ColumnName].Value.ToStr();

                    if (string.IsNullOrEmpty(PriviousBookingColumn) || PriviousBookingColumn != columnName)
                    {
                        if (ColumnStatus == true)
                        {
                            ColumnStatus = false;
                        }
                        else
                        {
                            ColumnStatus = true;
                        }
                    }
                    else if (PriviousBookingColumn == columnName)
                    {
                        RadCheckBoxEditor chEditor = this.grdBookingColumns.ActiveEditor as RadCheckBoxEditor; ;
                        if (chEditor != null)
                        {
                            bool isChecked = Convert.ToBoolean(chEditor.Value);
                            if (isChecked == true)
                            {
                                ColumnStatus = false;
                            }
                            else
                            {
                                ColumnStatus = true;
                            }
                        }
                    }
                    if (ColumnStatus == false)
                    {
                        //DeleteRowFromBookingListColumnIndex(Id);
                    }
                    else
                    {
                        //AddRowToBookingListColumnIndex(Id, FormId, columnName, columnName, ColumnStatus, ColumnIndex);
                    }
                    for (int i = 0; i < grdLister.RowCount; i++)
                    {
                        if (grdLister.Columns.Contains(columnName))
                        {
                            grdLister.Columns[columnName].IsVisible = ColumnStatus;
                            int OldIndex = grdLister.Columns[columnName].Index;
                            int NewIndex = grdLister.Columns.Where(c => c.IsVisible == true).Count();
                            grdLister.Columns.Move(OldIndex, NewIndex);
                        }
                    }
                    PriviousBookingColumn = columnName;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        public string PriviousDashboardColumn = string.Empty;
        public string PriviousBookingColumn = string.Empty;
        void grdDashboardColumns_ValueChanging(object sender, EventArgs e)
        {
            try
            {
                if (grdDashboardColumns.CurrentRow is GridViewRowInfo)
                {
                    if (grdDashboardColumns.CurrentRow.Cells[COLS.ShowColumn].IsCurrent == true)
                    {
                        int Id = grdDashboardColumns.CurrentRow.Cells[COLS.Id].Value.ToInt();
                        bool ColumnStatus = grdDashboardColumns.CurrentRow.Cells[COLS.ShowColumn].Value.ToBool();
                        //int FormId = grdDashboardColumnIndex.CurrentRow.Cells[COLS.FormId].Value.ToInt();
                        int FormId = grdDashboardColumns.CurrentRow.Cells[COLS.FormId].Value.ToInt();
                        string columnName = grdDashboardColumns.CurrentRow.Cells[COLS.ColumnName].Value.ToStr();
                        string HeaderText = grdDashboardColumns.CurrentRow.Cells[COLS.ColumnName].Value.ToStr();
                        int ColumnIndex = grdDashboardColumns.CurrentRow.Cells[COLS.GridColMoveTo].Value.ToInt();
                        if (string.IsNullOrEmpty(PriviousDashboardColumn) || PriviousDashboardColumn != columnName)
                        {
                            if (ColumnStatus == true)
                            {
                                ColumnStatus = false;
                            }
                            else
                            {
                                ColumnStatus = true;
                            }
                        }
                        else if (PriviousDashboardColumn == columnName)
                        {
                            // int row = grdDashboardColumns.CurrentRow.Cells[COLS.Id].Value.ToInt();
                            RadCheckBoxEditor chEditor = this.grdDashboardColumns.ActiveEditor as RadCheckBoxEditor; ;
                            if (chEditor != null)
                            {
                                bool isChecked = Convert.ToBoolean(chEditor.Value);
                                if (isChecked == true)
                                {
                                    ColumnStatus = false;
                                }
                                else
                                {
                                    ColumnStatus = true;

                                }

                            }

                        }
                        if (ColumnStatus == false)
                        {
                           // DeleteRowFromDashboardColumnIndex(Id);
                        }
                        else
                        {
                          //  AddRowToDashboardColumnIndex(Id, FormId, columnName, HeaderText, ColumnStatus, ColumnIndex);
                        }
                        for (int i = 0; i < grdTodayBooking.RowCount; i++)
                        {
                            if (grdTodayBooking.Columns.Contains(columnName))
                            {
                                grdTodayBooking.Columns[columnName].HeaderText = columnName;
                                grdTodayBooking.Columns[columnName].IsVisible = ColumnStatus;
                                int OldIndex = grdTodayBooking.Columns[columnName].Index;
                                int NewIndex = grdTodayBooking.Columns.Where(c=>c.IsVisible==true).Count();
                                grdTodayBooking.Columns.Move(OldIndex,NewIndex);
                            }
                        }
                        PriviousDashboardColumn = columnName;
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        public override void PopulateData()
        {
            PopulateTodayBooking();
            PopulateBookingList();
            //PopulateSearchData();
        
        }
        public void PopulateSearchData()
        {
            try
            {
                grdSearch.AllowRowReorder = false;
                grdSearch.AllowDeleteRow = false;
                DateTime dtLastBooking=DateTime.Now.AddDays(-1);
                var data1 = AppVars.BLData.GetAll<Booking>(null).OrderByDescending(c => c.PickupDateTime).Where(c => c.BookingDate <= dtLastBooking).Take(3);

                var query = from a in data1
                            where
                            (a.SubcompanyId == AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId == 0)
                            
                            select new
                            {
                                Id = a.Id,
                                RefNumber = a.BookingNo,
                                BookingDate = a.BookingDate,
                                PickupDate = a.PickupDateTime,
                                Passenger = a.CustomerName,
                                Acc = a.CompanyId != null ? a.Gen_Company.CompanyName : "",
                                OrderNo = a.OrderNo,
                                From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                Fare = a.FareRate,
                                Driver = a.Fleet_Driver.DriverNo,
                                Vehicle = a.Fleet_VehicleType.VehicleType,
                                MobileNo = a.CustomerMobileNo,
                                Status = a.BookingStatus.StatusName,
                                StatusTextColor = a.BookingStatus.TextColor,
                                PaymentRef = a.PaymentComments
                            };
                grdSearch.DataSource = query;
                //NC 9/jan
                var SearchGridColumnList = General.GetQueryable<UM_Form_UserDefinedSetting>(c => (c.UM_Form.FormName == "frmBookingDashBoard") && (c.FormTab == "search") && (c.IsVisible == false || c.GridColMoveTo != null)).ToList();
                //var SearchGridColumnList = General.GetQueryable<UM_Form_UserDefinedSetting>(c => (c.UM_Form.FormName == "frmBookingDashBoard") && (c.FormTab == "search") && (c.IsVisible == false || c.GridColMoveTo != null)).ToList();
                for (int i = 0; i < SearchGridColumnList.Count; i++)
                {
                    string Name = SearchGridColumnList[i].GridColumnName;
                    grdSearch.Columns[SearchGridColumnList[i].GridColumnName].IsVisible = SearchGridColumnList[i].IsVisible.ToBool();
                    if (SearchGridColumnList[i].GridColMoveTo != null)
                    {
                        grdSearch.Columns.Move(grdSearch.Columns[SearchGridColumnList[i].GridColumnName].Index, SearchGridColumnList[i].GridColMoveTo.ToInt());
                    }
                }

                var ColumnOrderList = (SearchGridColumnList.Where(c => c.IsVisible == true).OrderBy(c => c.GridColMoveTo)).ToList();
                for (int j = 0; j < ColumnOrderList.Count; j++)
                {
                    string ColumnName = ColumnOrderList[j].GridColumnName.ToStr();
                    grdSearch.Columns[ColumnName].Width = ColumnOrderList[j].GridColWidth.ToInt();
                    grdSearch.Columns.Move(grdSearch.Columns[ColumnOrderList[j].GridColumnName].Index, ColumnOrderList[j].GridColMoveTo.ToInt());
                    grdSearch.Columns[ColumnName].HeaderText = ColumnOrderList[j].HeaderText.ToStr();
                }
                var listofColumns = (from a in grdSearchColumnIndex.Rows
                                     select new
                                         {
                                             ColumnName = a.Cells[COLS.ColumnName].Value.ToStr(),
                                             HeaderText = a.Cells[COLS.HeaderText].Value.ToStr()
                                         }).ToList();

                for (int j = 0; j < grdSearch.Columns.Where(c => c.IsVisible == true).Count(); j++)
                {
                    foreach (var item in listofColumns)
                    {
                        if (grdSearch.Columns[j].Name.ToLower() == item.ColumnName.ToLower() && string.IsNullOrEmpty(grdSearch.Columns[j].HeaderText))
                        {
                            grdSearch.Columns[j].HeaderText = grdSearch.Columns[j].Name;
                            break;
                        }
                    }
                }
                //
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public void PopulateTodayBooking()
        {
            try
            {
                grdTodayBooking.Rows.Clear();
                grdTodayBooking.Columns.Clear();
              
                DateTime? dt = DateTime.Now.ToDateorNull();
                DateTime recentDays = dt.Value.AddDays(-1);

                DateTime prebookingdays = dt.Value.AddDays(AppVars.objPolicyConfiguration.HourControllerReport.ToInt()).ToDate();

                // live
                var data1 = General.GetQueryable<Booking>(a => (a.PickupDateTime.Value.Date >= recentDays && a.PickupDateTime.Value.Date <= prebookingdays)
                    && (a.BookingStatusId != Enums.BOOKINGSTATUS.DISPATCHED && a.BookingStatusId != Enums.BOOKINGSTATUS.CANCELLED)
                    && a.IsQuotation == false && (a.SubcompanyId == AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId == 0));
                DateTime dtNow = DateTime.Now;

                var query = (from a in data1

                             select new
                             {
                                 Id = a.Id,
                                 Plot = a.Gen_Zone1.ShortName,

                                 PlotHour = (a.ZoneId != null && a.Gen_Zone1.FlashingHour != null)
                                 ? a.PickupDateTime.Value.AddMinutes(-(a.Gen_Zone1.FlashingHour.Value.Minute)).AddHours(-(a.Gen_Zone1.FlashingHour.Value.Hour))
                                 : a.PickupDateTime.Value.AddHours(-BookingHours),

                                 RefNumber = a.BookingNo,
                                 BookingDateTime = a.BookingDate,
                                 PickupDateTemp = a.PickupDateTime,
                                 PickUpDate = string.Format("{0:dd-MM}", a.PickupDateTime),
                                 Time = string.Format("{0:HH:mm}", a.PickupDateTime),

                                 Passenger = a.CustomerName,
                                 MobileNo = a.CustomerMobileNo != null && a.CustomerMobileNo != "" ? a.CustomerMobileNo : a.CustomerPhoneNo,

                                 From = a.FromDoorNo != string.Empty ? a.FromDoorNo + "-" + a.FromStreet + " " + a.FromAddress : a.FromAddress,
                                 Pickup = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromOther : a.FromOther,
                                 FromPostCode = a.FromPostCode,
                                 To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                 GoingTo = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToOther : a.ToOther,
                                 ToPostCode = a.ToPostCode,

                                 Fare = a.FareRate,
                                 Pax = a.NoofPassengers,
                                 PaymentMethod = a.Gen_PaymentType.PaymentType,
                                 FromLocTypeId = a.FromLocTypeId,
                                 ToLocTypeId = a.ToLocTypeId,

                                 BackgroundColor1 = a.Gen_Company.BackgroundColor,
                                 TextColor1 = a.Gen_Company.TextColor,

                                 BackgroundColor = a.Fleet_VehicleType.BackgroundColor,

                                 TextColor = a.Fleet_VehicleType.TextColor,

                                 Account = a.Gen_Company.CompanyName,
                                 PReference = (a.PaymentComments != null && a.PaymentComments != "") ? "YES" : "",

                                 Vehicle = a.Fleet_VehicleType.VehicleType,

                                 UpdateBy = a.AddLog,
                                 SpecialReq = a.SpecialRequirements,
                                 StatusId = a.BookingStatusId,
                                 Status = a.BookingStatus.StatusName,
                                 StatusColor = a.BookingStatus.BackgroundColor,
                                 Driver = a.DriverId != null ? a.Fleet_Driver.DriverNo + "-" + a.BookingStatus.StatusName : "",
                                 DriverId = a.DriverId,
                                 IsAutoDespatch = a.AutoDespatch,
                                 BookingTypeId = a.BookingTypeId,
                                 HasNotes = a.Booking_Notes.Count,
                                 HasNotesImg = "",
                                 SubCompanyBgColor = a.SubcompanyId != null ? a.Gen_SubCompany.BackgroundColor : -1,

                                 BookingBackgroundColor = a.BookingType.BackgroundColor,
                                 GroupId = a.JobCode,
                                 FromLocId = a.FromLocId,
                                 PrePickupDate = a.PickupDateTime.Value.Date,
                                 BabySeats = a.BabySeats,
                                 FromLocBgColor = a.FromLocId != null ? a.Gen_Location1.BackgroundColor : -1,
                                 ToLocBgColor = a.ToLocId != null ? a.Gen_Location2.BackgroundColor : -1,
                                 FromLocTextColor = a.FromLocId != null ? a.Gen_Location1.TextColor : -1,
                                 ToLocTextColor = a.ToLocId != null ? a.Gen_Location2.TextColor : -1,
                                 IsConfirmedDriver = a.IsConfirmedDriver,
                                 MilesFromBase = a.ExtraMile,
                                 IsBidding = a.IsBidding,
                                 DeadMileage = a.DeadMileage,
                                 a.DespatchDateTime,
                                 a.JourneyTypeId,
                                 Due = a.ZoneId != null ? a.Gen_Zone1.JobDueTime : null
                             }).ToList();

                DateTime prevDates = dt.Value.AddDays(-3);
                DateTime Hours = DateTime.Now.ToDateTime().AddHours(BookingHours);

                //  int rowIndex = grdPendingJobs.CurrentRow != null ? grdPendingJobs.CurrentRow.Index : -1;

                int val = grdTodayBooking.TableElement.VScrollBar.Value;

                if (BookingHours > 0)
                {

                    grdTodayBooking.DataSource = query.Where(a => a.PickupDateTemp >= prevDates && a.PlotHour <= dtNow
                    && (a.StatusId == Enums.BOOKINGSTATUS.WAITING || a.StatusId == Enums.BOOKINGSTATUS.PENDING
                       || a.StatusId == Enums.BOOKINGSTATUS.NOTACCEPTED || a.StatusId == Enums.BOOKINGSTATUS.REJECTED
                       || a.StatusId == Enums.BOOKINGSTATUS.ONHOLD || a.StatusId == Enums.BOOKINGSTATUS.BID
                       || a.StatusId == Enums.BOOKINGSTATUS.PENDING_START
                                       || a.StatusId == Enums.BOOKINGSTATUS.NOSHOW))
                                       .OrderBy(c => c.PickupDateTemp).ToList();

                }
                else
                {

                    if (AppVars.objPolicyConfiguration.EnableGhostJob.ToBool())
                    {
                        grdTodayBooking.DataSource = query.Where(a => (a.PickupDateTemp >= prevDates && a.PickupDateTemp.Value.Date <= dt.Value.AddDays(DaysInTodayBooking))
                                  && (a.StatusId == Enums.BOOKINGSTATUS.WAITING || a.StatusId == Enums.BOOKINGSTATUS.PENDING || a.StatusId == Enums.BOOKINGSTATUS.NOTACCEPTED || a.StatusId == Enums.BOOKINGSTATUS.REJECTED
                                     || a.StatusId == Enums.BOOKINGSTATUS.NOSHOW || a.StatusId == Enums.BOOKINGSTATUS.ONHOLD || a.StatusId == Enums.BOOKINGSTATUS.BID
                                      || a.StatusId == Enums.BOOKINGSTATUS.PENDING_START || a.StatusId == Enums.BOOKINGSTATUS.FOJ))
                                      .OrderBy(c => c.PickupDateTemp).ToList();
                    }
                    else
                    {

                        grdTodayBooking.DataSource = query.Where(a => (a.PickupDateTemp >= prevDates && a.PickupDateTemp.Value.Date <= dt.Value.AddDays(DaysInTodayBooking))
                                   && (a.StatusId == Enums.BOOKINGSTATUS.WAITING || a.StatusId == Enums.BOOKINGSTATUS.PENDING || a.StatusId == Enums.BOOKINGSTATUS.NOTACCEPTED || a.StatusId == Enums.BOOKINGSTATUS.REJECTED
                                      || a.StatusId == Enums.BOOKINGSTATUS.NOSHOW || a.StatusId == Enums.BOOKINGSTATUS.ONHOLD || a.StatusId == Enums.BOOKINGSTATUS.BID
                                       || a.StatusId == Enums.BOOKINGSTATUS.PENDING_START))
                                       .OrderBy(c => c.PickupDateTemp).ToList();

                        
                    }
                }


               

                //Dashborad Grid
                var DashboardHiddenColumnList = General.GetQueryable<UM_Form_UserDefinedSetting>(c => (c.UM_Form.FormName == "frmBookingDashBoard") && (c.FormTab == null)).ToList();
                

                if (grdTodayBooking.RowCount == 0)
                {
                    grdTodayBooking.DataSource = null;
                    foreach (var item in DashboardHiddenColumnList)
                    {
                        if(!grdTodayBooking.Columns.Contains(item.GridColumnName))
                        grdTodayBooking.Columns.Add(item.GridColumnName);
                    }
                    AddDummyData(grdTodayBooking);
                }

                for (int i = 0; i < DashboardHiddenColumnList.Count; i++)
                {
                    grdTodayBooking.Columns[DashboardHiddenColumnList[i].GridColumnName].IsVisible = DashboardHiddenColumnList[i].IsVisible.ToBool();

                    if (DashboardHiddenColumnList[i].GridColMoveTo != null)
                    {
                        grdTodayBooking.Columns.Move(grdTodayBooking.Columns[DashboardHiddenColumnList[i].GridColumnName].Index, DashboardHiddenColumnList[i].GridColMoveTo.ToInt());
                    }
                }
                var ColumnOrderList = (DashboardHiddenColumnList.Where(c => c.IsVisible == true).OrderBy(c => c.GridColMoveTo)).ToList();
                for (int j = 0; j < ColumnOrderList.Count; j++)
                {
                    int Index = ColumnOrderList[j].GridColMoveTo.ToInt(); ;
                    string ColumnName = ColumnOrderList[j].GridColumnName.ToStr();
                    grdTodayBooking.Columns[ColumnName].Width = ColumnOrderList[j].GridColWidth.ToInt();
                    grdTodayBooking.Columns.Move(Index, Index);
                }

                //var list2 = DashboardHiddenColumnList.OrderBy(c => c.GridColMoveTo).ToList();
                //grdTodayBooking.BeginUpdate();
                //var list3 = (from a in DashboardHiddenColumnList
                //             orderby a.GridColMoveTo
                //             select new
                //             {
                //                 GridColMoveTo = a.GridColMoveTo != null ? a.GridColMoveTo : 100,
                //                 GridColumnName = a.GridColumnName,
                //                 GridColWidth = a.GridColWidth,
                //                 IsVisible = a.IsVisible
                //             }).ToList();

                //var list2 = list3.OrderBy(c => c.GridColMoveTo).ToList();
                //grdTodayBooking.DataSource = listofTodaysBooking;

                //for (int i = 0; i < list2.Count; i++)
                //{
                //    //string Name = DashboardHiddenColumnList[i].GridColumnName;                    
                //    grdTodayBooking.Columns[list2[i].GridColumnName].IsVisible = list2[i].IsVisible.ToBool();

                //    if (list2[i].GridColMoveTo != 100)
                //    {
                //        grdTodayBooking.Columns[list2[i].GridColumnName].Width = list2[i].GridColWidth.ToInt();
                //        //grdTodayBooking.Columns.Move(Index, Index);
                //        grdTodayBooking.Columns.Move(grdTodayBooking.Columns[list2[i].GridColumnName].Index, list2[i].GridColMoveTo.ToInt());
                //        //grdTodayBooking.Columns.Move(grdTodayBooking.Columns[list2[i].GridColumnName].Index, list2[i].GridColMoveTo.ToInt());
                //        // grdTodayBooking.Columns[Name].HeaderText = DashboardHiddenColumnList[i].HeaderText.ToStr();
                //    }
                //}
                //grdTodayBooking.EndUpdate();
                //var ColumnOrderList = (list2.Where(c => c.IsVisible == true).OrderBy(c => c.GridColMoveTo)).ToList();
                //for (int j = 0; j < ColumnOrderList.Count; j++)
                //{
                //    int Index = ColumnOrderList[j].GridColMoveTo.ToInt(); ;
                //    string ColumnName = ColumnOrderList[j].GridColumnName.ToStr();
                //    grdTodayBooking.Columns[ColumnName].Width = ColumnOrderList[j].GridColWidth.ToInt();
                //    grdTodayBooking.Columns.Move(Index, Index);
                //    //   grdTodayBooking.Columns.Move(grdTodayBooking.Columns[ColumnOrderList[j].GridColumnName].Index, ColumnOrderList[j].GridColMoveTo.ToInt());
                //}

                //Old
               // grdTodayBooking.DataSource = listofTodaysBooking;
               
                //grdTodayBooking.BeginUpdate();
                //foreach (var item in ColumnOrderList)
                //{
                //    grdTodayBooking.Columns.Move(grdTodayBooking.Columns[item.GridColumnName].Index, item.GridColMoveTo.ToInt());
                //}
                //grdTodayBooking.EndUpdate();
                
                //

                //var ColumnOrderList = (BookingColumnList.Where(c => c.IsVisible == true).OrderBy(c => c.GridColMoveTo)).ToList();
                //for (int j = 0; j < ColumnOrderList.Count; j++)
                //{
                //    string ColumnName = ColumnOrderList[j].GridColumnName.ToStr();
                //    grdLister.Columns[ColumnName].Width = ColumnOrderList[j].GridColWidth.ToInt();
                //    grdLister.Columns.Move(grdLister.Columns[ColumnOrderList[j].GridColumnName].Index, ColumnOrderList[j].GridColMoveTo.ToInt());
                //    grdLister.Columns[ColumnName].HeaderText = ColumnOrderList[j].HeaderText.ToStr();
                //}

                //var listofColumns = (from a in grdDashboardColumns.Rows
                //                     select new
                //                     {

                //                         ColumnName = a.Cells[COLS.ColumnName].Value.ToStr(),
                //                         //HeaderText = a.Cells[COLSIND.HeaderText].Value.ToStr()//HeaderText;)
                //                     }).ToList();

                //for (int j = 0; j < grdTodayBooking.Columns.Where(c => c.IsVisible == true).Count(); j++)
                //{
                //    foreach (var item in listofColumns)
                //    {
                //        if (grdTodayBooking.Columns[j].Name.ToLower() == item.ColumnName.ToLower())// && string.IsNullOrEmpty(grdTodayBooking.Columns[j].HeaderText))
                //        {
                //            grdTodayBooking.Columns[j].HeaderText = grdTodayBooking.Columns[j].Name;
                //            break;
                //        }
                //    }
                //}

                grdTodayBooking.Columns["Due"].IsVisible = false;
            }
            catch (Exception ex)
            {
                //  ENUtils.ShowMessage(ex.Message);

            }

        }
        private void PopulateDashboardColumns()
        {
            try
            {

                //var hiddenColumnsList = General.GetQueryable<UM_Form_UserDefinedSetting>(c => c.UM_Form.FormName == "frmBookingDashBoard").OrderBy(c => c.GridColMoveTo).ToList();
                var list = (from a in General.GetQueryable<UM_Form_UserDefinedSetting>(c => (c.UM_Form.FormName == "frmBookingDashBoard") && (c.DisplaySettings == true) && (c.FormTab == null))
                            orderby a.GridColMoveTo
                            select new
                            {
                                Id = a.Id,
                                FormId = a.FormId,
                                GridColumnName = a.GridColumnName,
                                HeaderText = a.HeaderText,
                                IsVisible = a.IsVisible,
                                GridColMoveTo = a.GridColMoveTo
                            }).ToList();
                var ColumnList = list.OrderBy(c=>c.GridColumnName).ToList();

                grdDashboardColumns.RowCount = ColumnList.Count;
                for (int i = 0; i < ColumnList.Count; i++)
                {
                    grdDashboardColumns.Rows[i].Cells[COLS.Id].Value = ColumnList[i].Id;
                    grdDashboardColumns.Rows[i].Cells[COLS.FormId].Value = ColumnList[i].FormId;
                    grdDashboardColumns.Rows[i].Cells[COLS.ColumnName].Value = ColumnList[i].GridColumnName;
                    //grdDashboardColumns.Rows[i].Cells[COLS.HeaderText].Value = list[i].HeaderText;
                    grdDashboardColumns.Rows[i].Cells[COLS.ShowColumn].Value = ColumnList[i].IsVisible;
                    grdDashboardColumns.Rows[i].Cells[COLS.GridColMoveTo].Value = ColumnList[i].GridColMoveTo;
                }

                var list2 = list.Where(c => c.IsVisible == true).ToList();


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void PopulateSearchColumns()
        {
            try
            {

                //var hiddenColumnsList = General.GetQueryable<UM_Form_UserDefinedSetting>(c => c.UM_Form.FormName == "frmBookingDashBoard").OrderBy(c => c.GridColMoveTo).ToList();
                var list = (from a in General.GetQueryable<UM_Form_UserDefinedSetting>(c => (c.UM_Form.FormName == "frmBookingDashBoard") && (c.DisplaySettings == true) && (c.FormTab == "search"))
                            orderby a.GridColMoveTo
                            select new
                            {
                                Id = a.Id,
                                FormId = a.FormId,
                                GridColumnName = a.GridColumnName,
                                HeaderText = a.HeaderText,
                                IsVisible = a.IsVisible,
                                GridColMoveTo = a.GridColMoveTo
                            }).ToList();
                var ColumnList = list.OrderBy(c => c.GridColumnName).ToList();
                grdSearchColumns.RowCount = ColumnList.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    grdSearchColumns.Rows[i].Cells[COLS.Id].Value = ColumnList[i].Id;
                    grdSearchColumns.Rows[i].Cells[COLS.FormId].Value = ColumnList[i].FormId;
                    grdSearchColumns.Rows[i].Cells[COLS.ColumnName].Value = ColumnList[i].GridColumnName;
                    grdSearchColumns.Rows[i].Cells[COLS.ShowColumn].Value = ColumnList[i].IsVisible;
                    grdSearchColumns.Rows[i].Cells[COLS.GridColMoveTo].Value = ColumnList[i].GridColMoveTo;
                }

                var list2 = list.Where(c => c.IsVisible == true).ToList();

                // GridViewRowInfo row;

                grdSearchColumnIndex.RowCount = list2.Count;

                for (int i = 0; i < list2.Count; i++)
                {
                    //row = grdDashboardColumnIndex.Rows.AddNew();
                    grdSearchColumnIndex.Rows[i].Cells[COLS.Id].Value = list2[i].Id;
                    grdSearchColumnIndex.Rows[i].Cells[COLS.FormId].Value = list2[i].FormId;
                    grdSearchColumnIndex.Rows[i].Cells[COLS.ColumnName].Value = list2[i].GridColumnName;
                    grdSearchColumnIndex.Rows[i].Cells[COLS.HeaderText].Value = list2[i].HeaderText;
                    grdSearchColumnIndex.Rows[i].Cells[COLS.GridColMoveTo].Value = list2[i].GridColMoveTo;
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public void PopulateBookingList()
        {
            try
            {
            
                grdLister.Rows.Clear();
                grdLister.Columns.Clear();
                int bookingstatusId = 0;
          

                var data1 = General.GetQueryable<Booking>(c => c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING
              && c.BookingStatusId != Enums.BOOKINGSTATUS.ONHOLD && c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING_WEBBOOKING
              && c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING_WEBBOOKING && c.BookingStatusId != Enums.BOOKINGSTATUS.REJECTED_WEBBOOKING
                           && ((c.PickupDateTime.Value >= DateTime.Now.ToDate()) && (c.PickupDateTime.Value <= DateTime.Now.ToDate() + TimeSpan.Parse("23:59:59")))
              && (c.SubcompanyId == AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId == 0)
              && (bookingstatusId == 0 || c.BookingStatusId == bookingstatusId))
                         .OrderByDescending(c => c.PickupDateTime);
              //  var cc = data12.Count();

               
                var query = (from a in data1
                                 select new
                                 {
                                     Id = a.Id,
                                     Token=a.JobCode,
                                     RefNumber = a.BookingNo,
                                     BookingDate = a.BookingDate,
                                     PickupDate = a.PickupDateTime,
                                     Passenger = a.CustomerName,
                                     MobileNo = a.CustomerMobileNo != null && a.CustomerMobileNo != "" ? a.CustomerMobileNo : a.CustomerPhoneNo,
                                     From = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                     Pickup = a.FromDoorNo != string.Empty ? a.FromDoorNo + " - " + a.FromOther : a.FromOther,
                                     FromPostCode = a.FromPostCode,
                                     To = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                     GoingTo = a.ToDoorNo != string.Empty ? a.ToDoorNo + " - " + a.ToOther : a.ToOther,
                                     ToPostCode = a.ToPostCode,
                                     Fare = a.FareRate,
                                     PaymentMethod = a.Gen_PaymentType.PaymentType,
                                     AccountFare = a.CompanyPrice,
                                     CustomerFare = a.CustomerPrice,
                                     Account = a.OrderNo != null && a.OrderNo != string.Empty ? a.Gen_Company.CompanyName + " - " + a.OrderNo : a.Gen_Company.CompanyName,
                                     Driver = a.Fleet_Driver.DriverNo,
                                     DriverId = a.DriverId,
                                     Vehicle = a.Fleet_VehicleType.VehicleType,
                                     Status = a.BookingStatus.StatusName,
                                     StatusColor = a.BookingStatus.BackgroundColor,
                                     BookingTypeId = a.BookingTypeId,
                                     VehicleBgColor = a.Fleet_VehicleType.BackgroundColor,
                                     VehicleTextColor = a.Fleet_VehicleType.TextColor,
                                     BackgroundColor1 = a.Gen_Company.BackgroundColor,
                                     TextColor1 = a.Gen_Company.TextColor,
                                     FromLocTypeId = a.FromLocTypeId,
                                     ToLocTypeId = a.ToLocTypeId,
                                     SubCompanyBgColor = a.SubcompanyId != null ? a.Gen_SubCompany.BackgroundColor : -1,
                                     StatusId = a.BookingStatusId,
                                     BookingBackgroundColor = a.BookingType.BackgroundColor,
                                     FromLocBgColor = a.FromLocId != null ? a.Gen_Location1.BackgroundColor : -1,
                                     ToLocBgColor = a.ToLocId != null ? a.Gen_Location2.BackgroundColor : -1,
                                     FromLocTextColor = a.FromLocId != null ? a.Gen_Location1.TextColor : -1,
                                     ToLocTextColor = a.ToLocId != null ? a.Gen_Location2.TextColor : -1,

                                 }).ToList();
                grdLister.DataSource = query;
                


                var BookingColumnList = General.GetQueryable<UM_Form_UserDefinedSetting>(c => (c.UM_Form.FormName == "frmBookingsList")).ToList();// && (c.IsVisible == false || c.GridColMoveTo != null)).ToList();


                if (grdLister.RowCount == 0)
                {
                    grdLister.DataSource = null;
                    foreach (var item in BookingColumnList)
                    {
                        if (!grdLister.Columns.Contains(item.GridColumnName))
                            grdLister.Columns.Add(item.GridColumnName);
                    }
                    AddDummyData(grdLister);
                }

                for (int i = 0; i < BookingColumnList.Count; i++)
                {
                    string Name = BookingColumnList[i].GridColumnName;

                    grdLister.Columns[BookingColumnList[i].GridColumnName].IsVisible = BookingColumnList[i].IsVisible.ToBool();

                    if (BookingColumnList[i].GridColMoveTo != null)
                    {
                        grdLister.Columns.Move(grdLister.Columns[BookingColumnList[i].GridColumnName].Index, BookingColumnList[i].GridColMoveTo.ToInt());
                    }
                }

                var ColumnOrderList = (BookingColumnList.Where(c => c.IsVisible == true).OrderBy(c => c.GridColMoveTo)).ToList();
                for (int j = 0; j < ColumnOrderList.Count; j++)
                {
                    string ColumnName = ColumnOrderList[j].GridColumnName.ToStr();
                    grdLister.Columns[ColumnName].Width = ColumnOrderList[j].GridColWidth.ToInt();
                    grdLister.Columns.Move(grdLister.Columns[ColumnOrderList[j].GridColumnName].Index, ColumnOrderList[j].GridColMoveTo.ToInt());
                  
                }
               
              
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public void PopulateBookingListColumns()
        {
            try
            {
                var list = (from a in General.GetQueryable<UM_Form_UserDefinedSetting>(c => (c.UM_Form.FormName == "frmBookingsList") && (c.DisplaySettings == true))
                            orderby a.GridColMoveTo
                            select new
                            {
                                Id = a.Id,
                                FormId = a.FormId,
                                GridColumnName = a.GridColumnName,
                                HeaderText = a.HeaderText,
                                IsVisible = a.IsVisible,
                                GridColMoveTo = a.GridColMoveTo
                            }).ToList();
                var ColumnList = list.OrderBy(c => c.GridColumnName).ToList();
                grdBookingColumns.RowCount = ColumnList.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    grdBookingColumns.Rows[i].Cells[COLS.Id].Value = ColumnList[i].Id;
                    grdBookingColumns.Rows[i].Cells[COLS.FormId].Value = ColumnList[i].FormId;
                    grdBookingColumns.Rows[i].Cells[COLS.ColumnName].Value = ColumnList[i].GridColumnName;

                    grdBookingColumns.Rows[i].Cells[COLS.ShowColumn].Value = ColumnList[i].IsVisible;
                    grdBookingColumns.Rows[i].Cells[COLS.GridColMoveTo].Value = ColumnList[i].GridColMoveTo;
                }
                var list2 = list.Where(c => c.IsVisible == true).ToList();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void UpdateDashboard()
        {
            try
            {

                var list = (from a in grdTodayBooking.Columns
                            select new
                            {
                                Index = a.Index,
                                Name = a.Name,
                                IsVisible = a.IsVisible,
                                Width = a.Width
                            }).ToList();
                int Index = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    var objGrd = grdDashboardColumns.Rows.Where(c => c.Cells[COLS.ColumnName].Value.ToStr() == list[i].Name.ToStr()).FirstOrDefault();
                    if (objGrd != null)
                    {
                        int Id = objGrd.Cells[COLS.Id].Value.ToInt();//grdBookingColumns.Rows[i].Cells[COLSBOOKINGLIST.Id].Value
                        if (Id == 0)
                        {
                            continue;
                        }
                        objUserDefinedSettings.GetByPrimaryKey(Id);//grdBookingColumns.Rows[i].Cells[COLSBOOKINGLIST.Id].Value.ToIntorNull());
                        objUserDefinedSettings.Edit();

                        objUserDefinedSettings.Current.IsVisible = list[i].IsVisible;
                        if (list[i].IsVisible)
                        {
                            objUserDefinedSettings.Current.GridColWidth = list[i].Width;
                            objUserDefinedSettings.Current.GridColMoveTo = Index; //list[i].Index;

                            Index++;
                        }
                        else
                        {
                            objUserDefinedSettings.Current.GridColMoveTo = null;
                            objUserDefinedSettings.Current.GridColWidth = 0;
                        }
                        objUserDefinedSettings.Save();
                        objUserDefinedSettings.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                if (objUserDefinedSettings.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objUserDefinedSettings.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }
        private void UpdateBookingList()
        {
            try
            {
                string ColumnName = string.Empty;

                var list = (from a in grdLister.Columns
                            select new
                            {
                                Index = a.Index,
                                Name = a.Name,
                                IsVisible=a.IsVisible,
                                Width = a.Width
                            }).ToList();
                int Index=0;
                for (int i = 0; i < list.Count; i++)
                {
                    var objGrd = grdBookingColumns.Rows.Where(c => c.Cells[COLS.ColumnName].Value.ToStr() == list[i].Name.ToStr()).FirstOrDefault();
                    if (objGrd != null)
                    {
                        int Id = objGrd.Cells[COLS.Id].Value.ToInt();//grdBookingColumns.Rows[i].Cells[COLSBOOKINGLIST.Id].Value
                        if (Id == 0)
                        {
                            continue;
                        }
                        objUserDefinedSettings.GetByPrimaryKey(Id);//grdBookingColumns.Rows[i].Cells[COLSBOOKINGLIST.Id].Value.ToIntorNull());
                        objUserDefinedSettings.Edit();

                        objUserDefinedSettings.Current.IsVisible = list[i].IsVisible;
                        if (list[i].IsVisible)
                        {
                            objUserDefinedSettings.Current.GridColWidth = list[i].Width;
                            objUserDefinedSettings.Current.GridColMoveTo = Index; //list[i].Index;

                            Index++;
                        }
                        else
                        {
                            objUserDefinedSettings.Current.GridColMoveTo = null;
                            objUserDefinedSettings.Current.GridColWidth = 0;
                        }
                        objUserDefinedSettings.Save();
                        objUserDefinedSettings.Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                if (objUserDefinedSettings.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objUserDefinedSettings.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }
        private void UpdateSearch()
        {
            try
            {
                int ColumnWidth = 0;
                string ColumnName = string.Empty;
                for (int i = 0; i < grdSearchColumns.RowCount; i++)
                {
                    objUserDefinedSettings.GetByPrimaryKey(grdSearchColumns.Rows[i].Cells[COLS.Id].Value.ToIntorNull());
                    objUserDefinedSettings.Edit();
                    objUserDefinedSettings.Current.IsVisible = grdSearchColumns.Rows[i].Cells[COLS.ShowColumn].Value.ToBool();
                    ColumnName = grdSearchColumns.Rows[i].Cells[COLS.ColumnName].Value.ToStr();
                    if (grdSearch.Columns.Contains(ColumnName) == true && grdSearch.Columns[ColumnName].IsVisible == true)
                    {
                        ColumnWidth = grdSearch.Columns[ColumnName].Width;
                        var list = (from a in grdSearchColumnIndex.Rows
                                    where a.Cells[COLS.ColumnName].Value.ToStr() == ColumnName
                                    select new
                                    {
                                        Index=a.Index,
                                        HeaderText = a.Cells[COLS.HeaderText].Value.ToStr()
                                    }).ToList();
                            
                        foreach (var item in list)
                        {
                            int Index = item.Index;
                            string HeaderText = item.HeaderText;
                            objUserDefinedSettings.Current.GridColMoveTo = Index;
                            objUserDefinedSettings.Current.HeaderText = HeaderText;
                        }
                    }
                    else
                    {
                        ColumnWidth = 0;
                    }

                    objUserDefinedSettings.Current.GridColWidth = ColumnWidth;
                    objUserDefinedSettings.Save();
                    objUserDefinedSettings.Clear();
                }
            }
            catch (Exception ex)
            {
                if (objUserDefinedSettings.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objUserDefinedSettings.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }

        public override void Save()
        {
            string Error = string.Empty;
            if (grdTodayBooking.Columns.Where(c => c.IsVisible == true).Count() < 6)
            {
                Error = "Minimum 6 columns should be visible for Dashboard Grid";
            }
            if (grdLister.Columns.Where(c => c.IsVisible == true).Count() < 6)
            {
                if (string.IsNullOrEmpty(Error))
                {
                    Error = "Minimum 6 columns should be visible for BookingList Grid";
                }
                else
                {
                    Error += Environment.NewLine + "Minimum 6 columns should be visible for BookingList Grid";
                }
            }
            //if (grdSearch.Columns.Where(c => c.IsVisible == true).Count() < 6)
            //{
            //    if (string.IsNullOrEmpty(Error))
            //    {
            //        Error = "Minimum 6 columns should be visible for Search Grid";
            //    }
            //    else
            //    {
            //        Error += Environment.NewLine + "Minimum 6 columns should be visible for Search Grid";
            //    }
            //}
            if (!string.IsNullOrEmpty(Error))
            {
                ENUtils.ShowMessage(Error);
                return;
            }

            UpdateDashboard();
            UpdateBookingList();
           // UpdateSearch();
            //PopulateData();

            //PopulateTodayAndPreBookingData();
            //PopulateBookingList();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
