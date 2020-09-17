using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Utils;
using Telerik.WinControls.UI;
using Taxi_AppMain.Classes;
using System.Data.Linq.SqlClient;


namespace Taxi_AppMain
{
    public partial class frmManagePostCodes : UI.SetupBase
    {

        public struct COLS
        {
            public static string Id = "Id";
            public static string Check = "Check";
            public static string AreaName = "AreaName";
            public static string PostCode = "PostCode";



        }
        RadDropDownMenu Update = null;

        public frmManagePostCodes()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmManagePostCodes_Load);
           
           this.btnSaveClose.Click += new EventHandler(btnSaveClose_Click);
           this.btnExit1.Click += new EventHandler(btnExit1_Click);
          
           this.grdPostCodes.CellEndEdit += new GridViewCellEventHandler(grdPostCodes_CellEndEdit);
          // this.FormClosing+=new FormClosingEventHandler(frmManagePostCodes_FormClosing);
           FormatGrid();
           FormatSelectedPostCodesGrid();
         //  grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);
           
           grdSelectedPostCodes.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdSelectedPostCodes_ContextMenuOpening);
        }

      
        void grdSelectedPostCodes_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
                if (cell == null)
                {
                    e.Cancel = true;
                    return;

                }
                else if (cell.GridControl.Name == "grdSelectedPostCodes")
                {

                    if (Update == null)
                    {
                        Update = new RadDropDownMenu();
                        Update.BackColor = Color.Orange;

                        RadMenuItem UpdateCurrent = new RadMenuItem("Update Coordinates");
                        UpdateCurrent.ForeColor = Color.DarkBlue;
                        UpdateCurrent.BackColor = Color.Orange;
                        UpdateCurrent.Font = new Font("Tahoma", 10, FontStyle.Bold);

                        UpdateCurrent.Click += new EventHandler(UpdateCurrent_Click);
                        Update.Items.Add(UpdateCurrent);


                        RadMenuItem UpdateUp = new RadMenuItem("Move Up");
                        UpdateUp.ForeColor = Color.DarkBlue;
                        UpdateUp.BackColor = Color.Orange;
                        UpdateUp.Font = new Font("Tahoma", 10, FontStyle.Bold);
                        UpdateUp.Click += new EventHandler(UpdateUp_Click);
                        Update.Items.Add(UpdateUp);


                        RadMenuItem UpdateDown = new RadMenuItem("Move Down");
                        UpdateDown.ForeColor = Color.DarkBlue;
                        UpdateDown.BackColor = Color.Orange;
                        UpdateDown.Font = new Font("Tahoma", 10, FontStyle.Bold);
                        UpdateDown.Click += new EventHandler(UpdateDown_Click);
                        Update.Items.Add(UpdateDown);


                       
                    }

                    e.ContextMenu = Update;
                    return;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void UpdateDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdSelectedPostCodes.CurrentRow != null)
                {
                    GridViewRowInfo row = grdSelectedPostCodes.CurrentRow;
                   // int Id = row.Cells[COLS.Id].Value.ToInt();
                    //string ColumnName = grdSelectedPostCodes.CurrentRow.Cells["ColumnName"].Value.ToStr();
                    int Index = grdSelectedPostCodes.CurrentRow.Index;
                    int TotalRows = (grdSelectedPostCodes.RowCount - 1);
                    if (Index >= 0 && Index != TotalRows)
                    {
                        int NewIndex = (Index + 1);
                        this.grdSelectedPostCodes.Rows.Move(Index, NewIndex);
                       
                    }
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void UpdateCurrent_Click(object sender, EventArgs e)
        {
            try
            {

                if (DialogResult.Yes == MessageBox.Show("Do you  want to update coordinates ? " + Environment.NewLine + "This will take a few minutes to update", "Update Coordinates", MessageBoxButtons.YesNo))
                {

                    InitializeCurrentUpdateWorker();

                    btnUpdateCoordinates.Enabled = false;
                    grdSelectedPostCodes.Enabled = false;
                    btnSaveClose.Enabled = false;
                    btnExit1.Enabled = false;

                    lblUpdate.Text = string.Empty;
                    UpdateCurrentworker.RunWorkerAsync();


                }




            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }
        }

        void UpdateUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdSelectedPostCodes.CurrentRow != null)
                {
                    //string ColumnName = grdSearchColumnIndex.CurrentRow.Cells["ColumnName"].Value.ToStr();
                    int Index = grdSelectedPostCodes.CurrentRow.Index;
                    int TotalRows = (grdSelectedPostCodes.RowCount - 1);
                    if (Index > 0)
                    {
                        int NewIndex = (Index - 1);
                        this.grdSelectedPostCodes.Rows.Move(Index, NewIndex);
                      
                    }
                }
              
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

      
        void grdPostCodes_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            
            //SelectedPostCodes();
           // PopulateSelectedPostCodes();

            var row = e.Row;

            if (row != null && row is GridViewDataRowInfo)
            {
                bool ischecked= e.Value.ToBool();


                if (ischecked)
                {
                    if (grdSelectedPostCodes.Rows.Count(c => c.Cells[COLS.PostCode].Value.ToStr().Trim() == row.Cells[COLS.PostCode].Value.ToStr().Trim()) == 0)
                    {
                        GridViewRowInfo rowInfo = grdSelectedPostCodes.Rows.AddNew();


                        rowInfo.Cells[COLS.Id].Value = row.Cells[COLS.Id].Value;
                        rowInfo.Cells[COLS.PostCode].Value = row.Cells[COLS.PostCode].Value.ToStr();
                        rowInfo.Cells[COLS.AreaName].Value = row.Cells[COLS.AreaName].Value.ToStr();

                    }

                }
                else
                {
                    if (grdSelectedPostCodes.Rows.Count(c => c.Cells[COLS.PostCode].Value.ToStr().Trim() == row.Cells[COLS.PostCode].Value.ToStr().Trim()) > 0)
                    {
                        grdSelectedPostCodes.Rows.Remove(grdSelectedPostCodes.Rows.FirstOrDefault(c => c.Cells[COLS.PostCode].Value.ToStr().Trim()==row.Cells[COLS.PostCode].Value.ToStr().Trim()));

                    }

                }

                lblSelectedPostCodes.Text = "Selected PostCodes " + (grdSelectedPostCodes.Rows.Count);



            }
        }

    

        void frmManagePostCodes_Load(object sender, EventArgs e)
        {
            DisplayRecord();   
        }

        void btnSaveClose_Click(object sender, EventArgs e)
        {
            Save();
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectedPostCodes()
        {
            try
            {
                lblCount.Text = "Total PostCodes " + grdPostCodes.Rows.Count();// +" out of " + grdPostCodes.Rows.Count() + "";
            }
            catch (Exception ex)
            { }
        }
     

        #region Overridden Methods


      

        public override void DisplayRecord()
        {
            try
            {
                grdPostCodes.Rows.Clear();
                
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_DisplayPostCodes().ToList();
                    grdPostCodes.RowCount = list.Count;
                  

                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].PostCodeId != null)
                        {
                            grdPostCodes.Rows[i].Cells[COLS.Check].Value = true;

                          
                        }

                        grdPostCodes.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                        grdPostCodes.Rows[i].Cells[COLS.PostCode].Value = list[i].Postcode;
                        grdPostCodes.Rows[i].Cells[COLS.AreaName].Value = list[i].AreaName;     


                    }


                    var selectedlist=list.Where(c => c.PostCodeId != null).ToList();
                    grdSelectedPostCodes.RowCount = selectedlist.Count;

                    for (int i = 0; i <selectedlist.Count; i++)
                    {
                       

                          grdSelectedPostCodes.Rows[i].Cells[COLS.Id].Value = selectedlist[i].Id;
                          grdSelectedPostCodes.Rows[i].Cells[COLS.PostCode].Value = selectedlist[i].Postcode;
                          grdSelectedPostCodes.Rows[i].Cells[COLS.AreaName].Value = selectedlist[i].AreaName;                                            


                    }

                }

                SelectedPostCodes();
               
                grdPostCodes.CurrentRow = null;
                lblUpdate.Text = string.Empty;

                lblSelectedPostCodes.Text = "Selected PostCodes " + (grdSelectedPostCodes.Rows.Count);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }




        public override void Save()
        {
            try
            {
                if (grdPostCodes.Rows.Where(c=>c.Cells[COLS.Check].Value.ToBool()==true).Count() == 0)
                {
                    ENUtils.ShowMessage("Please select PostCodes");
                    return;
                }

                StringBuilder sb = new StringBuilder();
                                var listMain = (from a in grdSelectedPostCodes.Rows.OrderBy(C=>C.Index)
                                select new
                                {
                                    Id = a.Cells[COLS.Id].Value,
                                    PostCode = a.Cells[COLS.PostCode].Value
                                }).ToList();
                foreach (var item in listMain)
                {
                    sb.Append("insert into PAFDb.dbo.Localization (PostCode,PostCodeId) values ('" + item.PostCode.ToStr() + "',"+item.Id+");"); 
                }
             
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.stp_SavePostCodes(sb.ToStr());
                }
                this.Close();
              
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
          
        }


    
        public override void  AddNew()
        {
         	 OnNew();
        }

        public override void  OnNew()
        {
 	        grdPostCodes.Rows.Clear();
        
        }

        #endregion

        private void FormatSelectedPostCodesGrid()
        {
            grdSelectedPostCodes.AllowAutoSizeColumns = true;
            grdSelectedPostCodes.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdSelectedPostCodes.AllowAddNewRow = false;
            grdSelectedPostCodes.AllowRowReorder = true;
            grdSelectedPostCodes.ShowGroupPanel = false;
            grdSelectedPostCodes.AutoCellFormatting = true;
            grdSelectedPostCodes.ShowRowHeaderColumn = false;
            grdSelectedPostCodes.EnableFiltering = true;


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.Id;
            grdSelectedPostCodes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.HeaderText = "PostCode";
            col.Name = COLS.PostCode;
            col.Width = 70;
            col.ReadOnly = true;
            grdSelectedPostCodes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.HeaderText = "Area Name";
            col.Name = COLS.AreaName;
            col.Width = 90;
            col.ReadOnly = true;
            grdSelectedPostCodes.Columns.Add(col);
        }   

        private void FormatGrid()
        {
            grdPostCodes.AllowAutoSizeColumns = true;
            grdPostCodes.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdPostCodes.AllowAddNewRow = false;
            grdPostCodes.ShowGroupPanel = false;
            grdPostCodes.AutoCellFormatting = true;
            grdPostCodes.ShowRowHeaderColumn = false;
            grdPostCodes.EnableFiltering = true;



            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.Id;
            grdPostCodes.Columns.Add(col);


            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS.Check;
            cbcol.Width = 50;
            grdPostCodes.Columns.Add(cbcol);
            col = new GridViewTextBoxColumn();
            col.HeaderText = "PostCode";
            col.Name = COLS.PostCode;
            col.Width = 80;
            col.ReadOnly = true;

            grdPostCodes.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.HeaderText = "Area Name";
            col.Name = COLS.AreaName;
            col.Width = 110;
            col.ReadOnly = true;
            grdPostCodes.Columns.Add(col);

            grdPostCodes.CommandCellClick += new CommandCellClickEventHandler(grdPostCodes_CommandCellClick);
           // UI.GridFunctions.AddDeleteColumn(grdPostCodes);
            //GridViewCommandColumn cmdcol = new GridViewCommandColumn();
            //cmdcol.Width = 70;

            //cmdcol.Name = "coldelete";
            //cmdcol.UseDefaultText = true;
            //cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            //cmdcol.DefaultText = "Delete";
            //cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
          
            //grdPostCodes.Columns.Add(cmdcol);



        }

        void grdPostCodes_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "coldelete")
            {
                RadGridView grid = gridCell.GridControl;
                grid.CurrentRow.Delete();
            }   
        }

        private bool IsFormClosed = false;

        void frmManagePostCodes_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
             try
            {
                if(worker!=null)
                {
                    if (worker.IsBusy)
                    {
                        if (DialogResult.No == MessageBox.Show("Coordinates are not fully updated! " + Environment.NewLine + "Do you still want to close this form ?", "Update Coordinates", MessageBoxButtons.YesNo))
                        {
                            e.Cancel = true;
                            return;
                        }

                    }

                    IsFormClosed = true;
                        worker.CancelAsync();
                        worker.Dispose();
                        worker = null;

                }

                if (UpdateCurrentworker != null)
                {
                    if (UpdateCurrentworker.IsBusy)
                    {
                        if (DialogResult.No == MessageBox.Show("Coordinates are not fully updated! " + Environment.NewLine + "Do you still want to close this form ?", "Update Coordinates", MessageBoxButtons.YesNo))
                        {
                            e.Cancel = true;
                            return;
                        }

                    }

                    IsFormClosed = true;
                    UpdateCurrentworker.CancelAsync();
                    UpdateCurrentworker.Dispose();
                    UpdateCurrentworker = null;

                }
               
             
            }
            catch
            {


            }
        }

        private void frmZones_FormClosed(object sender, FormClosedEventArgs e)
        {

          
              General.DisposeForm(this);
           
        }





        BackgroundWorker worker = null;

        BackgroundWorker UpdateCurrentworker = null;

        private void MoveRow(bool moveUp) 
        {
            GridViewRowInfo currentRow = this.grdPostCodes.CurrentRow;
            if (currentRow == null)
            {
                return;
            }
            int index = moveUp ? currentRow.Index - 1 : currentRow.Index + 1;
            if (index < 0 || index >= this.grdPostCodes.RowCount) 
            {
                return; 
            }
            this.grdPostCodes.Rows.Move(index, currentRow.Index);
            this.grdPostCodes.CurrentRow = this.grdPostCodes.Rows[index]; 
        }

        private void InitializeWorker()
        {
            if (worker == null)
            {
                worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = true;
                worker.WorkerReportsProgress = true;
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
              
            }

          

        }
        private void InitializeCurrentUpdateWorker()
        {
            
            if (UpdateCurrentworker == null)
            {
                UpdateCurrentworker = new BackgroundWorker();
                UpdateCurrentworker.WorkerSupportsCancellation = true;
                UpdateCurrentworker.WorkerReportsProgress = true;
                UpdateCurrentworker.DoWork += new DoWorkEventHandler(UpdateCurrentworker_DoWork);
                UpdateCurrentworker.ProgressChanged += new ProgressChangedEventHandler(UpdateCurrentworker_ProgressChanged);
                UpdateCurrentworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(UpdateCurrentworker_RunWorkerCompleted);

            }

        }
        void UpdateCurrentworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string result = (string)e.Result;

            lblUpdate.Text = result;

            btnUpdateCoordinates.Enabled = true;
            btnSaveClose.Enabled = true;
            btnExit1.Enabled = true;
            grdSelectedPostCodes.Enabled = true;
        }

        void UpdateCurrentworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentRow cr = e.UserState as CurrentRow;
            if (cr != null)
            {
                lblUpdate.Text = "Updating (" + cr.UpdateValue + ") " + (cr.index) + " out of " + cr.Total + "";
            }
        }

        void UpdateCurrentworker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {


                //var listMain = (from a in grdPostCodes.Rows.Where(c => c.Cells[COLS.Check].Value.ToBool() == true)
                //                select new
                //                {
                //                    Id = a.Cells[COLS.Id].Value,
                //                    PostCode = a.Cells[COLS.PostCode].Value.ToStr()
                //                }).ToList();

                GridViewRowInfo row = grdSelectedPostCodes.CurrentRow;
                if (row.Cells[COLS.Id].Value.ToInt() == 0)
                {
                    //e.Result
                    return;
                }
                bool allowUpdating = false;
                //foreach (var item in listMain)
                //{

                    using (TaxiDataContext db = new TaxiDataContext())
                    {


                        allowUpdating = false;

                        var listofCoordinates = db.stp_GetCoordinatesByRoadLevelData(row.Cells[COLS.Id].Value.ToInt(), string.Empty).ToList();

                        //row.Cells[COLS.PostCode].Value
                        char PostCodeShort = row.Cells[COLS.PostCode].Value.ToStr()[0];

                        //if (item.PostCode.Length == 1 && listofCoordinates.Count != db.Gen_Coordinates.Count(c => c.PostCode[0] == item.PostCode[0] && SqlMethods.Like(c.PostCode, "[A-Z][0-9]%")))
                        if (PostCodeShort.ToStr().Length == 1 && listofCoordinates.Count != db.Gen_Coordinates.Count(c => c.PostCode[0] == PostCodeShort && SqlMethods.Like(c.PostCode, "[A-Z][0-9]%")))
                        {
                            allowUpdating = true;

                        }
                        else if (row.Cells[COLS.PostCode].Value.ToStr().Length > 1 && listofCoordinates.Count != db.Gen_Coordinates.Count(c => c.PostCode.StartsWith(row.Cells[COLS.PostCode].Value.ToStr())))
                        {
                            allowUpdating = true;

                        }

                        if (allowUpdating)
                        {

                            CurrentRow cr = new CurrentRow();
                            cr.Total = listofCoordinates.Count;
                            //cr.UpdateValue = item.PostCode;
                            cr.UpdateValue = row.Cells[COLS.PostCode].Value.ToStr();

                            int cnter = 0;
                        // int updatecounter = 0;
                        foreach (var item2 in listofCoordinates)
                        {
                            try
                            {

                                if (IsFormClosed)
                                    break;

                                db.Gen_Coordinates.InsertOnSubmit(new Gen_Coordinate { PostCode = item2.PostCode, Latitude = item2.Latitude, Longitude = item2.Longitude });
                                db.SubmitChanges();


                            }
                            catch
                            {


                            }

                            cnter++;
                            cr.index = cnter;
                            UpdateCurrentworker.ReportProgress(cnter, cr);

                        }
                        //List<Gen_Coordinate> cc = new List<Gen_Coordinate>();
                        //foreach (var item2 in listofCoordinates)
                        //{
                        //    try
                        //    {

                        //        if (IsFormClosed)
                        //            break;

                        //        if (cc.Count < 500)
                        //        {
                        //            cc.Add(new Gen_Coordinate { PostCode = item2.PostCode, Latitude = item2.Latitude, Longitude = item2.Longitude });
                        //        }
                        //        else
                        //        {
                        //            db.Gen_Coordinates.InsertAllOnSubmit(cc);
                        //            //db.Gen_Coordinates.InsertOnSubmit(new Gen_Coordinate { PostCode = item2.PostCode, Latitude = item2.Latitude, Longitude = item2.Longitude });
                        //            db.SubmitChanges();

                        //            if (listofCoordinates.Count + 500 > listofCoordinates.Count)
                        //            {
                        //                cnter = listofCoordinates.Count;
                        //            }
                        //            else
                        //            {
                        //                cnter += 500;
                        //            }
                        //            cc.Clear();


                        //            cr.index = cnter;
                        //            UpdateCurrentworker.ReportProgress(cnter, cr);
                        //        }

                        //    }
                        //    catch
                        //    {


                        //    }



                        //  //  updatecounter++;
                        //    //cnter++;
                        //    //cr.index = cnter;
                        //    //UpdateCurrentworker.ReportProgress(cnter, cr);

                        //}
                    }
                    }
                    //if (IsFormClosed)
                    //    break;
                //}
                e.Result = "Completed";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;

            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentRow cr = e.UserState as CurrentRow;
            if (cr != null)
            {
                lblUpdate.Text = "Updating ("+cr.UpdateValue+") " + (cr.index) + " out of " + cr.Total + "";
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string result = (string)e.Result;

            lblUpdate.Text = result;

            btnUpdateCoordinates.Enabled = true;
            btnSaveClose.Enabled = true;
            btnExit1.Enabled = true;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {


            var listMain = (from a in grdPostCodes.Rows.Where(c => c.Cells[COLS.Check].Value.ToBool() == true)
                            select new
                            {
                                Id = a.Cells[COLS.Id].Value,
                                PostCode = a.Cells[COLS.PostCode].Value.ToStr()
                            }).ToList();


            bool allowUpdating = false;
            foreach (var item in listMain)
            {

                using (TaxiDataContext db = new TaxiDataContext())
                {


                    allowUpdating = false;

                    var listofCoordinates = db.stp_GetCoordinatesByRoadLevelData(item.Id.ToInt(), string.Empty).ToList();


                    if (item.PostCode.Length == 1 && listofCoordinates.Count != db.Gen_Coordinates.Count(c => c.PostCode[0] == item.PostCode[0] && SqlMethods.Like(c.PostCode, "[A-Z][0-9]%")))
                    {
                        allowUpdating = true;

                    }
                    else if (item.PostCode.Length > 1 && listofCoordinates.Count != db.Gen_Coordinates.Count(c => c.PostCode.StartsWith(item.PostCode)))
                    {
                        allowUpdating = true;

                    }

                    if (allowUpdating)
                    {

                        CurrentRow cr = new CurrentRow();
                        cr.Total = listofCoordinates.Count;
                        cr.UpdateValue = item.PostCode;

                        int cnter = 0;
                            foreach (var item2 in listofCoordinates)
                            {
                                try
                                {

                                    if (IsFormClosed)
                                        break;

                                    db.Gen_Coordinates.InsertOnSubmit(new Gen_Coordinate { PostCode = item2.PostCode, Latitude = item2.Latitude, Longitude = item2.Longitude });
                                    db.SubmitChanges();


                                }
                                catch
                                {


                                }

                                cnter++;
                                cr.index = cnter;
                                worker.ReportProgress(cnter, cr);

                            }
                            //    List<Gen_Coordinate> cc = new List<Gen_Coordinate>();
                            //foreach (var item2 in listofCoordinates)
                            //{
                            //    try
                            //    {

                            //        if (IsFormClosed)
                            //            break;

                            //      //  db.Gen_Coordinates.InsertOnSubmit(new Gen_Coordinate { PostCode = item2.PostCode, Latitude = item2.Latitude, Longitude = item2.Longitude });
                            //      //  db.SubmitChanges();


                            //            if (cc.Count < 500)
                            //            {
                            //                cc.Add(new Gen_Coordinate { PostCode = item2.PostCode, Latitude = item2.Latitude, Longitude = item2.Longitude });
                            //            }
                            //            else
                            //            {
                            //                db.Gen_Coordinates.InsertAllOnSubmit(cc);
                            //                //db.Gen_Coordinates.InsertOnSubmit(new Gen_Coordinate { PostCode = item2.PostCode, Latitude = item2.Latitude, Longitude = item2.Longitude });
                            //                db.SubmitChanges();

                            //                if (listofCoordinates.Count + 500 > listofCoordinates.Count)
                            //                {
                            //                    cnter = listofCoordinates.Count;
                            //                }
                            //                else
                            //                {
                            //                    cnter += 500;
                            //                }
                            //                cc.Clear();


                            //                cr.index = cnter;
                            //                worker.ReportProgress(cnter, cr);
                            //            }

                            //        }
                            //    catch
                            //    {


                            //    }



                            //}
                        } 


                }

                if (IsFormClosed)
                    break;


            }


                e.Result="Completed";
            }
            catch(Exception ex)
            {
                  e.Result=ex.Message;

            }
        }

        class CurrentRow
        {
            public int index;
            public string UpdateValue;
            public int Total;
        }

        private void btnUpdateCoordinates_Click(object sender, EventArgs e)
        {
            try
            {

                if (DialogResult.Yes == MessageBox.Show("Do you  want to update coordinates ? " + Environment.NewLine + "This will take a few minutes to update", "Update Coordinates", MessageBoxButtons.YesNo))
                {

                    InitializeWorker();

                    btnUpdateCoordinates.Enabled = false;
                    btnSaveClose.Enabled = false;
                    btnExit1.Enabled = false;

                    lblUpdate.Text = string.Empty;
                    worker.RunWorkerAsync();


                }




            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }
        }

    }
}
