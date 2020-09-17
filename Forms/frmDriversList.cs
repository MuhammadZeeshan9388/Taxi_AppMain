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
using System.Collections;
using Telerik.WinControls.UI.Docking;
using UI;

namespace Taxi_AppMain
{
    public partial class frmDriversList : UI.SetupBase
    {
        DriverBO objMaster;
        private bool IsClosed = false;
        BackgroundWorker worker = null;


     

        RadDropDownMenu AddRentItems = null;

        bool ShowMot2 = false;

        public frmDriversList()
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
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;


            ShowMot2 = AppVars.objPolicyConfiguration.DefaultClientId.ToStr() != "pinkberrycars";

            FormatGrid();
            this.FormClosing += frmDriversList_FormClosing;



            //if (AppVars.listUserRights.Count(c => c.formName == "frmDriverRent") > 0)
            //{
            //    // for menus
            //    grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);
            //}
           
        }


        //void grdLister_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        //{
        //    try
        //    {
        //        if (AddRentItems == null)
        //        {
        //            AddRentItems = new RadDropDownMenu();
        //            AddRentItems.BackColor = Color.Orange;

        //            RadMenuItem AddRentItems1 = new RadMenuItem("Add Driver Rent");
        //            AddRentItems1.ForeColor = Color.DarkBlue;
        //            AddRentItems1.BackColor = Color.Orange;
        //            AddRentItems1.Font = new Font("Tahoma", 10, FontStyle.Bold);
        //            AddRentItems1.Click += new EventHandler(AddRentItems1_Click);
        //            AddRentItems.Items.Add(AddRentItems1);

        //            RadMenuItem AddRentItems2 = new RadMenuItem("Driver Rent List");
        //            AddRentItems2.ForeColor = Color.DarkBlue;
        //            AddRentItems2.BackColor = Color.Orange;
        //            AddRentItems2.Font = new Font("Tahoma", 10, FontStyle.Bold);
        //            AddRentItems2.Click += new EventHandler(AddRentItems2_Click);
        //            AddRentItems.Items.Add(AddRentItems2);
        //        }

        //        GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
        //        if (cell == null)
        //            return;

        //        else if (cell.GridControl.Name == "grdLister" && cell.RowInfo.Cells["DriverTypeId"].Value.ToInt()==1)
        //        {
        //            e.ContextMenu = AddRentItems;
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ENUtils.ShowMessage(ex.Message);

        //    }
        //}
        //void AddRentItems1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
        //        {
        //            int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
        //            frmDriverRent frm = new frmDriverRent(id);

        //            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverRent1");

        //            if (doc != null)
        //            {
        //                doc.Close();
        //            }

        //            MainMenuForm.MainMenuFrm.ShowForm(frm);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ENUtils.ShowMessage(ex.Message);
        //    }
        //}

        //void AddRentItems2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
        //        {
        //            int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
        //            frmRentList frm = new frmRentList(id);
        //            frm.Show();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ENUtils.ShowMessage(ex.Message);
        //    }
        //}


        private void CancelWorker()
        {
            if (worker != null && worker.WorkerSupportsCancellation)
            {
                lblLoading.Text = "Loading Data Please Wait";
                lblLoading.Image = Resources.Resource1.pleasewait2;
                lblLoading.TextImageRelation = TextImageRelation.TextBeforeImage;
                lblLoading.Visible = true;
                lblLoading.ForeColor = Color.Blue;
                btnCloseError.Visible = false;
                hasError = false;

            
                worker.CancelAsync();

              
            }

        }

        void frmDriversList_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsClosed = true;

            CancelWorker();
            worker.Dispose();
            
        }

        List<Gen_Syspolicy_DriverDocumentList> listofDocs = new List<Gen_Syspolicy_DriverDocumentList>();

        void frmDriversList_Shown(object sender, EventArgs e)
        {
            try
            {


                grdLister.AllowAutoSizeColumns = true;
                grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
                grdLister.EnableHotTracking = false;
                grdLister.ShowGroupPanel = false;
                grdLister.AllowAddNewRow = false;
                grdLister.AllowEditRow = false;


                listofDocs = General.GetQueryable<Gen_Syspolicy_DriverDocumentList>(null).ToList();

                try
                {

                    this.InitializeForm("frmDriver");
                }
                catch
                {


                }

                worker.RunWorkerAsync(listOfData);


              
            }
            catch
            {


            }

          

        }

        IList listOfData = null;


    

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            PopulateData();
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
                        e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.TwoWayBindingLocal);
                    }               

                }


                else
                {

                    if (e.Column.Name != "MOTExpiry" && e.Column.Name != "MOT2Expiry" && e.Column.Name != "PCOVehicleExpiry" && e.Column.Name != "LicenseExpiry"
                        && e.Column.Name != "PCODriverExpiry" && e.Column.Name != "InsuranceExpiry")
                    {

                        e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.TwoWayBindingLocal);
                    }
                }



                //if (e.CellElement.RowElement.RowInfo.Cells["EndDate"].Value!=null)
                //{
                //    if (e.Column.Name == "MOTExpiry" || e.Column.Name == "PCODriverExpiry" || e.Column.Name=="MOT2Expiry" || e.Column.Name=="LicenseExpiry"
                //        || e.Column.Name == "InsuranceExpiry" || e.Column.Name == "PCOVehicleExpiry")
                //    {
                //        e.CellElement.BackColor = Color.White;
                     
                //    }             

                //}


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


        private void AddCommandColumn(RadGridView grid, string colName, string caption)
        {

            if (grid.Columns.Count(c => c.Name == colName) == 0)
            {

                GridViewCommandColumn col = new GridViewCommandColumn();
                col.Width = 60;

                col.Name = colName;
                col.UseDefaultText = true;
                col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
                col.DefaultText = caption;
                col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

                grid.Columns.Add(col);
            }

        }

      
       
    

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.Columns.Count == 0) return;


                DateTime dtNow=DateTime.Now.ToDate();

                if ((AppVars.frmMDI.ActiveControl != null && AppVars.frmMDI.ActiveControl.Name.Equals("frmDriversList1") == true))
                {

                    foreach (var item in grdLister.Rows)
                    {

                        if (item.Cells["MOTExpiry"].Value.ToDate() < dtNow)
                        {
                            item.Cells["MOTExpiry"].Style.BackColor = Color.Pink;
                            item.Cells["MOTExpiry"].Style.CustomizeFill = true;

                        }
                        else  if (item.Cells["MOTExpiry"].Value.ToDate() < dtNow.AddDays(MOTDays))
                        {

                            if (item.Cells["MOTExpiry"].Style.BackColor == Color.White)
                            {

                                item.Cells["MOTExpiry"].Style.BackColor = Color.Yellow;
                            }
                            else
                            {

                                item.Cells["MOTExpiry"].Style.BackColor = Color.White;
                            }

                            item.Cells["MOTExpiry"].Style.CustomizeFill = true;

                        }




                        if ( item.Cells["MOT2Expiry"].Value.ToDate() < dtNow)
                        {
                            item.Cells["MOT2Expiry"].Style.BackColor = Color.Pink;
                            item.Cells["MOT2Expiry"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["MOT2Expiry"].Value.ToDate() < dtNow.AddDays(MOT2Days))
                        {

                            if (item.Cells["MOT2Expiry"].Style.BackColor == Color.White)
                            {

                                item.Cells["MOT2Expiry"].Style.BackColor = Color.Lavender;
                            }
                            else
                            {

                                item.Cells["MOT2Expiry"].Style.BackColor = Color.White;
                            }

                            item.Cells["MOT2Expiry"].Style.CustomizeFill = true;

                        }




                        if ( item.Cells["RoadTaxExpiry"].Value.ToDate() < dtNow)
                        {
                            item.Cells["RoadTaxExpiry"].Style.BackColor = Color.Pink;
                            item.Cells["RoadTaxExpiry"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["RoadTaxExpiry"].Value.ToDate() < dtNow.AddDays(RoadTaxDays))
                        {

                            if (item.Cells["RoadTaxExpiry"].Style.BackColor == Color.White)
                            {

                                item.Cells["RoadTaxExpiry"].Style.BackColor = Color.Lavender;
                            }
                            else
                            {

                                item.Cells["RoadTaxExpiry"].Style.BackColor = Color.White;
                            }

                            item.Cells["RoadTaxExpiry"].Style.CustomizeFill = true;

                        }



                        if (item.Cells["LicenseExpiry"].Value.ToDate() < dtNow)
                        {
                            item.Cells["LicenseExpiry"].Style.BackColor = Color.Pink;
                            item.Cells["LicenseExpiry"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["LicenseExpiry"].Value.ToDate() < dtNow.AddDays(LicenseDays))
                        {

                            if (item.Cells["LicenseExpiry"].Style.BackColor == Color.White)
                            {

                                item.Cells["LicenseExpiry"].Style.BackColor = Color.PaleGoldenrod;
                            }
                            else
                            {

                                item.Cells["LicenseExpiry"].Style.BackColor = Color.White;
                            }

                            item.Cells["LicenseExpiry"].Style.CustomizeFill = true;

                        }



                        if (item.Cells["PCOVehicleExpiry"].Value.ToDate() < dtNow)
                        {
                            item.Cells["PCOVehicleExpiry"].Style.BackColor = Color.Pink;
                            item.Cells["PCOVehicleExpiry"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["PCOVehicleExpiry"].Value.ToDate() < dtNow.AddDays(PHCVehicleDays))
                        {

                            if (item.Cells["PCOVehicleExpiry"].Style.BackColor == Color.White)
                            {

                                item.Cells["PCOVehicleExpiry"].Style.BackColor = Color.Orange;
                            }
                            else
                            {

                                item.Cells["PCOVehicleExpiry"].Style.BackColor = Color.White;
                            }

                            item.Cells["PCOVehicleExpiry"].Style.CustomizeFill = true;

                        }



                        if ( item.Cells["PCODriverExpiry"].Value.ToDate() < dtNow)
                        {
                            item.Cells["PCODriverExpiry"].Style.BackColor = Color.Pink;
                            item.Cells["PCODriverExpiry"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["PCODriverExpiry"].Value.ToDate() < dtNow.AddDays(PHCDriverDays))
                        {

                            if (item.Cells["PCODriverExpiry"].Style.BackColor == Color.White)
                            {

                                item.Cells["PCODriverExpiry"].Style.BackColor = Color.Gainsboro;
                            }
                            else
                            {

                                item.Cells["PCODriverExpiry"].Style.BackColor = Color.White;
                            }

                            item.Cells["PCODriverExpiry"].Style.CustomizeFill = true;

                        }



                        if (item.Cells["InsuranceExpiry"].Value.ToDateTime() < DateTime.Now)
                        {
                            item.Cells["InsuranceExpiry"].Style.BackColor = Color.Pink;
                            item.Cells["InsuranceExpiry"].Style.CustomizeFill = true;

                        }
                        else if (item.Cells["InsuranceExpiry"].Value.ToDateTime() < DateTime.Now.AddDays(InsuranceDays))
                        {

                            if (item.Cells["InsuranceExpiry"].Style.BackColor == Color.White)
                            {

                                item.Cells["InsuranceExpiry"].Style.BackColor = Color.LightBlue;
                            }
                            else
                            {

                                item.Cells["InsuranceExpiry"].Style.BackColor = Color.White;
                            }

                            item.Cells["InsuranceExpiry"].Style.CustomizeFill = true;

                        }

                        


                        
                    }



                   
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


        private void StopTimer()
        {
            timer1.Stop();

        }

        private void StartTimer()
        {
            timer1.Start();

        }

        private void ShowDriverForm(int id)
        {

            try
            {

                StopTimer();

                frmDriver frm = new frmDriver();
                frm.OnDisplayRecord(id);

                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm.MaximizeBox = false;
                frm.ShowDialog();




                StartTimer();
            }
            catch (Exception ex)
            {


            }
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



       
      
        private void grdLister_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column != null && e.Row != null && e.Row.Cells["Id"].Value != null)
            {
                if (e.Column.Name == "Name")
                {
                    e.CellElement.Font = newFont;

                }        

            }
        }

        
       


        public override void RefreshData()
        {

            CancelWorker();


            if (worker.IsBusy == false)
            {

                worker.RunWorkerAsync();
            }
        }


        private bool hasError = false;

        private void PopulateUI(IList list)
        {
            try
            {
                if (IsClosed)
                    return;

                grdLister.DataSource = list;

                lblTotal.Text = "Total Driver(s) : " + list.Count.ToStr();
            }
            catch (Exception ex)
            {
                hasError = true;

                //if (this.InvokeRequired)
                //{
                    
                //    UIMessage del = new UIMessage(ShowUI);
                //    this.BeginInvoke(del, true, "There is a Problem on fetching data from server. Please try again or refresh it..");

                //}
                //else
                //{

                //    ShowUI(true, "There is a Problem on fetching data from server. Please try again or refresh it..");

                //}
            }
        }

        private void FormatGrid()
        {
            
         

                         
          GridViewTextBoxColumn  col = new GridViewTextBoxColumn();
            col.HeaderText = "No";
            col.Name = "No";
            col.Width = 70;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Name";
            col.Name = "Name";
            col.Width = 150;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Vehicle No";
            col.Name = "VehicleNo";
            col.Width = 90;
            grdLister.Columns.Add(col);


           

              col = new GridViewTextBoxColumn();
            col.HeaderText = "Vehicle Type";
            col.Name = "VehicleType";
            col.Width = 60;
            grdLister.Columns.Add(col);

               col = new GridViewTextBoxColumn();
            col.HeaderText = "MOT Expiry";
            col.Name = "MOTExpiry";
            col.Width = 60;
            grdLister.Columns.Add(col);


               col = new GridViewTextBoxColumn();
            col.HeaderText = "MOT2 Expiry";
            col.Name = "MOT2Expiry";
            col.Width = 60;
            col.IsVisible = false;
            grdLister.Columns.Add(col);


               col = new GridViewTextBoxColumn();
            col.HeaderText = "PHC Vehicle Expiry";
            col.Name = "PCOVehicleExpiry";
            col.Width = 60;
            grdLister.Columns.Add(col);

               col = new GridViewTextBoxColumn();
            col.HeaderText = "Insurance Expiry";
            col.Name = "InsuranceExpiry";
            col.Width = 130;
            grdLister.Columns.Add(col);


               col = new GridViewTextBoxColumn();
            col.HeaderText = "PHC Driver Expiry";
            col.Name = "PCODriverExpiry";
            col.Width = 60;
            grdLister.Columns.Add(col);


               col = new GridViewTextBoxColumn();
            col.HeaderText = "License Expiry";
            col.Name = "License Expiry";
            col.Width = 60;
            grdLister.Columns.Add(col);


             col = new GridViewTextBoxColumn();
            col.HeaderText = "Mobile No";
            col.Name = "MobileNo";
            col.Width = 100;
            grdLister.Columns.Add(col);


             col = new GridViewTextBoxColumn();
            col.HeaderText = "EndDate";
            col.Name = "EndDate";
            col.Width = 60;
            grdLister.Columns.Add(col);


           

        }


       
        private void ShowUI(bool show, string message)
        {

            if (IsClosed)
                return;


           

            if (show == false && hasError==false)
            {

                lblLoading.Visible = show;
            }

         
            if (hasError)
            {
                    lblLoading.TextImageRelation = TextImageRelation.ImageBeforeText;
                    lblLoading.Image = Resources.Resource1.remove;
                    lblLoading.ForeColor = Color.Red;
                    lblLoading.Visible = true;
                    lblLoading.Text = "There is a Problem on fetching data from server. Please try again or refresh it..";
                    btnCloseError.Visible = true;
            
            }
             else
             {


                    lblLoading.Image = null;
                    lblLoading.ForeColor = Color.Blue;


                    UI.GridFunctions.SetFilter(grdLister);


                    AddCommandColumn(grdLister, "btnEdit", "Edit");
                    if (this.CanDelete)
                    {
                        AddCommandColumn(grdLister, "btnDelete", "Delete");



                        grdLister.Columns["btnDelete"].Width = 70;
                    }

                    grdLister.Columns["btnEdit"].Width = 70;


                    grdLister.Columns["Id"].IsVisible = false;

                    grdLister.Columns["Name"].Width = 150;
                    grdLister.Columns["MobileNo"].Width = 100;

                    grdLister.Columns["EndDate"].IsVisible = false;
                    grdLister.Columns["DriverTypeId"].IsVisible = false;


                    grdLister.Columns["MOTExpiry"].IsVisible = listofDocs.FirstOrDefault(c => c.DocumentName == "MOT").IsVisible.ToBool();
                    grdLister.Columns["MOT2Expiry"].IsVisible = listofDocs.FirstOrDefault(c => c.DocumentName == "MOT 2").IsVisible.ToBool();
                    grdLister.Columns["RoadTaxExpiry"].IsVisible = listofDocs.FirstOrDefault(c => c.DocumentName == "Road Tax").IsVisible.ToBool();


                    if (grdLister.Columns["MOT2Expiry"].IsVisible)
                    {
                        label9.Text = "MOT 2 Expiry";
                    }


                    //if (grdLister.Columns["MOTExpiry"].IsVisible == false)
                    //{
                    //    lblMotExpiry.Visible = false;
                    //    lblMotExpiryBox.Visible = false;

                    //}

               //     if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "pinkberrycars")
               //     {
                        grdLister.Columns["MOT2Expiry"].IsVisible = false;

                      

                 //   }
                  //  else
                 //   {

                       


                        //grdLister.Columns["RoadTaxExpiry"].IsVisible = false;
                        (grdLister.Columns["MOT2Expiry"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
                        (grdLister.Columns["MOT2Expiry"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
              ///      }
              ///      

                        grdLister.Columns["RoadTaxExpiry"].HeaderText = "Road Tax Expiry";
                        grdLister.Columns["RoadTaxExpiry"].Width = 140;

                        (grdLister.Columns["RoadTaxExpiry"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
                        (grdLister.Columns["RoadTaxExpiry"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";

                

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


                    grdLister.Columns["DrvBadge"].HeaderText = "Drv Badge";
                    grdLister.Columns["VehBadge"].HeaderText = "Veh Badge";



                     PHCVehicleDays = listofDocs.FirstOrDefault(c => c.Id == 1).DefaultIfEmpty().ExpiryDays.ToInt();
                     PHCDriverDays = listofDocs.FirstOrDefault(c => c.Id == 2).DefaultIfEmpty().ExpiryDays.ToInt();
                     MOTDays = listofDocs.FirstOrDefault(c => c.Id == 3).DefaultIfEmpty().ExpiryDays.ToInt();
                     InsuranceDays = listofDocs.FirstOrDefault(c => c.Id == 4).DefaultIfEmpty().ExpiryDays.ToInt();
                     MOT2Days = listofDocs.FirstOrDefault(c => c.Id == 5).DefaultIfEmpty().ExpiryDays.ToInt();
                     LicenseDays = listofDocs.FirstOrDefault(c => c.Id == 6).DefaultIfEmpty().ExpiryDays.ToInt();
                     RoadTaxDays = listofDocs.FirstOrDefault(c => c.Id == 7).DefaultIfEmpty().ExpiryDays.ToInt();
                   int days = AppVars.objPolicyConfiguration.DriverExpiryNoticeInDays.ToInt();


                    //DateTime dtNow = DateTime.Now.ToDate();

                    //// MOT
                    //objYellow.CellBackColor = Color.Yellow;
                    //objYellow.TValue1 = string.Format("{0:dd/MM/yyyy}", dtNow.AddDays(days));
                    //objYellow.ConditionType = ConditionTypes.LessOrEqual;
                    //objYellow.TValue2 = string.Empty;

                    //// MOT 2
                    //objLavendar.CellBackColor = Color.Lavender;
                    //objLavendar.TValue1 = string.Format("{0:dd/MM/yyyy}", dtNow.AddDays(days));
                    //objLavendar.ConditionType = ConditionTypes.LessOrEqual;
                    //objLavendar.TValue2 = string.Empty;




                    //// License Expiry
                    //objPaleGoldenrod.CellBackColor = Color.PaleGoldenrod;
                    //objPaleGoldenrod.TValue1 = string.Format("{0:dd/MM/yyyy}", dtNow.AddDays(days));
                    //objPaleGoldenrod.ConditionType = ConditionTypes.LessOrEqual;
                    //objPaleGoldenrod.TValue2 = string.Empty;


                    //// INSURANCE
                    //objLightBlue.CellBackColor = Color.LightBlue;
                    //objLightBlue.TValue1 = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now.AddDays(days));
                    //objLightBlue.ConditionType = ConditionTypes.LessOrEqual;
                    //objLightBlue.TValue2 = string.Empty;


                    //// PCO DRIVER
                    //objGainboro.CellBackColor = Color.Gainsboro;
                    ////objGainboro.ApplyToRow = true;
                    ////objGainboro.RowBackColor = Color.Gainsboro;
                    //objGainboro.TValue1 = string.Format("{0:dd/MM/yyyy}", dtNow.AddDays(days));
                    //objGainboro.ConditionType = ConditionTypes.LessOrEqual;
                    //objGainboro.TValue2 = string.Empty;


                    //// PCO VEHICLE
                    //objOrange.CellBackColor = Color.Orange;
                    ////objOrange.ApplyToRow = true;
                    ////objOrange.RowBackColor = Color.Orange;
                    //objOrange.TValue1 = string.Format("{0:dd/MM/yyyy}", DateTime.Now.ToDate().AddDays(days));
                    //objOrange.ConditionType = ConditionTypes.LessOrEqual;
                    //objOrange.TValue2 = string.Empty;


                    //// BLANK
                    //objWhite.ApplyToRow = true;
                    //objWhite.RowBackColor = Color.White;
                    //objWhite.TValue1 = string.Format("{0:dd/MM/yyyy}", DateTime.Now.ToDate().AddDays(days));
                    //objWhite.ConditionType = ConditionTypes.LessOrEqual;
                    //objWhite.TValue2 = string.Empty;


                    //grdLister.Columns["LicenseExpiry"].ConditionalFormattingObjectList.Add(objWhite);
                
                    //grdLister.Columns["RoadTaxExpiry"].ConditionalFormattingObjectList.Add(objWhite);
                    //grdLister.Columns["MOTExpiry"].ConditionalFormattingObjectList.Add(objWhite);
                    //grdLister.Columns["PCOVehicleExpiry"].ConditionalFormattingObjectList.Add(objWhite);
                    //grdLister.Columns["PCODriverExpiry"].ConditionalFormattingObjectList.Add(objWhite);
                    //grdLister.Columns["InsuranceExpiry"].ConditionalFormattingObjectList.Add(objWhite);


                    //objAfterExpire.CellBackColor = Color.Pink;
                    //objAfterExpire.ConditionType = ConditionTypes.Less;
                    //objAfterExpire.TValue1 = string.Format("{0:dd/MM/yyyy}", DateTime.Now.ToDate());

                    //grdLister.Columns["PCODriverExpiry"].ConditionalFormattingObjectList.Add(objAfterExpire);
                    //grdLister.Columns["MOTExpiry"].ConditionalFormattingObjectList.Add(objAfterExpire);
                
                    //grdLister.Columns["RoadTaxExpiry"].ConditionalFormattingObjectList.Add(objAfterExpire);


                    //grdLister.Columns["LicenseExpiry"].ConditionalFormattingObjectList.Add(objAfterExpire);
                    //grdLister.Columns["PCOVehicleExpiry"].ConditionalFormattingObjectList.Add(objAfterExpire);


                    //objAfterExpireInsurance.CellBackColor = Color.Pink;
                    //objAfterExpireInsurance.ConditionType = ConditionTypes.Less;
                    //objAfterExpireInsurance.TValue1 = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now.ToDateTime());

                    //grdLister.Columns["InsuranceExpiry"].ConditionalFormattingObjectList.Add(objAfterExpireInsurance);



                    timer1.Start();
                }         


         
        }


          int PHCVehicleDays =0 ;
          int PHCDriverDays = 0;
          int MOTDays = 0;
          int InsuranceDays =0; 
          int MOT2Days = 0;
          int LicenseDays =0;
          int RoadTaxDays = 0;
             

        delegate void UIMessage(bool show,string message);
        delegate void UIDelegate(IList list);

        private bool IsFirstLoaded = false;


        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (IsClosed)
                return;

          

            if (this.InvokeRequired)
            {
                UIDelegate del = new UIDelegate(PopulateUI);
                this.BeginInvoke(del, listOfData);

                UIMessage del2 = new UIMessage(ShowUI);
                this.BeginInvoke(del2, false, "");

            }
            else
            {
                PopulateUI(listOfData);
                ShowUI(false, "");
            }          

        }

        public override void PopulateData()
        {
            try
            {

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    listOfData = db.stp_GetDriversList(AppVars.DefaultDriverSubCompanyId).OrderBy(item => item.No, new NaturalSortComparer<string>()).ToList();



                }



               //listOfData=  (from a in General.GetQueryable<Fleet_Driver>(c => c.IsActive == true && (c.SubcompanyId == AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId == 0))
               //                  .AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())

               //  select new
               //  {
               //      Id = a.Id,
               //      No = a.DriverNo,
               //      Name = a.DriverName,
               //      VehicleNo = a.VehicleNo,
               //      VehicleType = a.Fleet_VehicleType.VehicleType,
               //      MOTExpiry = a.MOTExpiryDate,
               //      MOT2Expiry = a.MOT2ExpiryDate,
               //      PCOVehicleExpiry = a.PCOVehicleExpiryDate,
               //      InsuranceExpiry = a.InsuranceExpiryDate,
               //      PCODriverExpiry = a.PCODriverExpiryDate,
               //      LicenseExpiry = a.DrivingLicenseExpiryDate,
               //      RoadTaxExpiry=a.RoadTaxiExpiryDate,
               //      MobileNo = a.MobileNo,
              
               //      EndDate=a.LastEndDate,
               //      DriverTypeId=a.DriverTypeId
               //  }).ToList();


               if (IsFirstLoaded == false)
               {
                   grdLister.Columns.Clear();
                   IsFirstLoaded = true;

               }


               if (IsClosed)
                   return;


               

                //if (this.InvokeRequired)
                //{
                //    UIDelegate del = new UIDelegate(PopulateUI);
                //    this.BeginInvoke(del,list);

                //}
                //else
                //{
                //    PopulateUI(list);

                //}


              
           
            }
            catch (Exception ex)
            {
                hasError = true;
                //if (this.InvokeRequired)
                //{
                //    hasError = true;


                //    UIMessage del = new UIMessage(ShowUI);
                //    this.BeginInvoke(del, true, "There is a Problem on fetching data from server. Please try again or refresh it..");

                //}
                //else
                //{
                //    has
                //    ShowUI(true, "There is a problem on fetching data from server. Please try again or refresh it..");

                //}

            }
            
        }

      

     

        private void btnExpiredDrivers_Click(object sender, EventArgs e)
        {
            frmExpiredDrivers frm = new frmExpiredDrivers();
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btnCloseError_Click(object sender, EventArgs e)
        {
            lblLoading.Visible = false;
        }

    }
}

