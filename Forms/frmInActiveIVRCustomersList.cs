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
using Taxi_BLL;
using DAL;
using Utils;
using Telerik.WinControls;
using Telerik.Data;
namespace Taxi_AppMain
{
    public partial class frmInActiveIVRCustomersList : UI.SetupBase
    {
         CustomerBO objMaster;
         int pageSize = 0;



         public frmInActiveIVRCustomersList()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmCustomersList_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
             grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new CustomerBO();
           
            this.SetProperties((INavigation)objMaster);
            grdLister.CellFormatting+=new CellFormattingEventHandler(grdLister_CellFormatting);


         
            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCustomersList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);

            grdLister.ShowGroupPanel = false;
            pageSize = AppVars.objPolicyConfiguration.ListingPagingSize.ToInt();

        }
        
         void frmCustomersList_Shown(object sender, EventArgs e)
         {
             this.InitializeForm("frmCustomer");

             grdLister.AllowAutoSizeColumns = true;
             grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

             ddlColumns.Text = "Name";

             PopulateData();
             AddCreateBookingColumn(grdLister);

             AddEditColumn(grdLister);

             if (this.CanDelete)
             {
                 AddDeleteColumn(grdLister);
             }


             grdLister.Columns["Id"].IsVisible = false;
             grdLister.Columns["Doorno"].IsVisible = false;

             grdLister.Columns["Address"].Width = 320;
             grdLister.Columns["Name"].Width = 70;           
             grdLister.Columns["Phone"].Width = 60;
             grdLister.Columns["MobileNo"].Width = 60;

             ddlColumns.Items.AddRange(grdLister.Columns.Where(c => c.Name != "Id" && c.Name != "ColEdit" && c.Name != "ColDelete" && c.Name != "btnCreateBooking")
                                    .Select(c => c.Name));
             ddlColumns.SelectedIndex = 0;

             UI.GridFunctions.SetFilter(grdLister);
             txtSearch.Focus();

        
         }


         private void AddCreateBookingColumn(RadGridView grid)
         {
             GridViewCommandColumn col = new GridViewCommandColumn();
             col.Width = 80;

             col.Name = "btnCreateBooking";
             col.UseDefaultText = true;
             col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
             col.DefaultText = "Create Booking";
             col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

             grid.Columns.Add(col);

         }

         private void BindProperties()
         {
            
         }

         public static void AddEditColumn(RadGridView grid)
         {
             GridViewCommandColumn col = new GridViewCommandColumn();
             col.BestFit();
         
             col.Name = "ColEdit";
             col.UseDefaultText = true;
             col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
             col.DefaultText = "Edit";
             col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

             grid.Columns.Add(col);

                    
         }

         private void AddDeleteColumn(RadGridView grid)
         {
             GridViewCommandColumn col = new GridViewCommandColumn();
             col.BestFit();

             col.Name = "ColDelete";
             col.UseDefaultText = true;
             col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
             col.DefaultText = "Delete";
             col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

             grid.Columns.Add(col);
        
         }



         private void grid_CommandCellClick(object sender, EventArgs e)
         {
             GridCommandCellElement gridCell = (GridCommandCellElement)sender;
             RadGridView grid = gridCell.GridControl;
             if (gridCell.ColumnInfo.Name == "ColDelete")
             {


                 if (gridCell.RowInfo != null)
                 {

                     if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Customer ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                     {
                         gridCell.RowInfo.Delete();
                  //       grid.CurrentRow.Delete();
                     }
                 }
             }
             else if (gridCell.ColumnInfo.Name == "ColEdit")
             {
                 ViewDetailForm();
             }
             else if (gridCell.ColumnInfo.Name.ToLower() == "btncreatebooking")
             {
                 GridViewRowInfo row = grid.CurrentRow;
                 if (row != null && row is GridViewRowInfo)
                 {
                     
                     if (gridCell.ColumnInfo.Name.ToLower() == "btncreatebooking")
                     {
                         string phone = row.Cells["Phone"].Value.ToStr().Trim(); 
                         string mobileNo= row.Cells["MobileNo"].Value.ToStr().Trim();
                         string email = row.Cells["Email"].Value.ToStr().Trim();

                         General.ShowBookingForm(0, false, row.Cells["Name"].Value.ToStr(), phone,mobileNo,
                                                          row.Cells["DoorNo"].Value.ToStr(), row.Cells["Address"].Value.ToStr(), email);
                     }
                 }

             }
         }


         void frmCustomersList_Load(object sender, EventArgs e)
         {
             
         }

         void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
         {
             ViewDetailForm();
         }

         private void ViewDetailForm()
         {

             if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
             {
                 ShowCustomerForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
             }
             else
             {
                 ENUtils.ShowMessage("Please select a record");
             }
         }


         private void ShowCustomerForm(int id)
         {


            frmCustomer frm = new frmCustomer();
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

                
                     objMaster = new CustomerBO();

                     try
                     {

                         objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                         objMaster.Delete(objMaster.Current);


                         PopulateData();
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

        
        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);
        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);



       

        public override void RefreshData()
        {
            PopulateData();
        }

       

        public override void PopulateData()
        {
            try
            {
                string searchTxt = txtSearch.Text.ToLower().Trim();
                string col = ddlColumns.Text.Trim().ToLower();

              


                bool col_name = false;
                bool col_phone = false;
                bool col_mobileno = false;
                bool col_address = false;
                bool col_doorno = false;

                if (col == "name")
                {
                    col_name = true;
                }
               
                else if (col == "phone")
                {
                    col_phone = true;
                }

                else if (col == "mobileno")
                {
                    col_mobileno = true;
                }

                else if (col == "address")
                {
                    col_address = true;
                }
                else if (col == "doorno")
                {
                    col_doorno = true;
                }


                if (searchTxt.Length < 3 && col != "doorno")
                    searchTxt = string.Empty;



                var data1 = General.GetQueryable<Customer>(c=>c.AccountNo!=null && c.AccountNo=="1").OrderBy(c => c.Name);

                int cnt = data1.Count();
                if (skip + pageSize > cnt && cnt - pageSize > 0)
                    skip = cnt - pageSize;
                else if (cnt <= pageSize)
                    skip = 0;

                var query = from a in data1.AsEnumerable()

                            where
                                (col_name && (a.Name.ToStr().ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                || (col_phone && (a.TelephoneNo.ToStr().ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                || (col_mobileno && (a.MobileNo.ToStr().ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                || (col_address && (a.Address1.ToStr().ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                || (col_doorno && (a.DoorNo.ToStr().ToLower().Contains(searchTxt) || searchTxt == string.Empty))


                            select new
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Phone = a.TelephoneNo,
                                MobileNo = a.MobileNo,
                                Address = a.DoorNo.ToStr() != string.Empty ? a.DoorNo + " - " + a.Address1 : a.Address1,
                                Doorno = a.DoorNo,
                                Email=a.Email

                            };


                this.grdLister.TableElement.BeginUpdate();

                if (excludeSkip == false)
                    grdLister.DataSource = query.Skip(skip).Take(pageSize).ToList();
                else
                    grdLister.DataSource = query.ToList();

                this.grdLister.TableElement.EndUpdate();
              
                lblTotal.Text = "Total Customers : " + cnt.ToStr();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


      

        private void btnFind_Click(object sender, EventArgs e)
        {
            excludeSkip = true;
            skip = 0;
            PopulateData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                excludeSkip = true;
                PopulateData();

            }
        }

        private void btnShowAllCustomers_Click(object sender, EventArgs e)
        {
          
            skip = 0;
            txtSearch.Text = string.Empty;
            PopulateData();
        }


        int skip = 0;
        bool excludeSkip = false;
        private void btnFirstRecords_Click(object sender, EventArgs e)
        {
            excludeSkip = false;
            skip = 0;
            PopulateData();
        }

        private void btnPreviousRecords_Click(object sender, EventArgs e)
        {
            excludeSkip = false;
            if (skip - pageSize < 0)
                skip = 0;
            else
                skip = skip - pageSize;

            PopulateData();
        }

        private void btnNextRecord_Click(object sender, EventArgs e)
        {
            excludeSkip = false;
            skip = skip + pageSize;
            PopulateData();
        }

        private void btnLastRecords_Click(object sender, EventArgs e)
        {
            excludeSkip = false;
            int cnt = General.GetQueryable<Customer>(null).Count();

            if(cnt<=pageSize)
            {
                skip=0;
            }
            else if (cnt > pageSize)
            {
                
                    skip =cnt- pageSize;
            
            }
            
            PopulateData();
        }

        RadGridViewExcelExporter exporter=null;
        private void btnExport_Click(object sender, EventArgs e)
        {

            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                var data1 = General.GetQueryable<Customer>(c=>c.AccountNo!=null && c.AccountNo=="1").OrderBy(c => c.Name);

             

                var query = (from a in data1

                            
                            select new
                            {
                           
                                Name = a.Name,
                                Phone = a.TelephoneNo,
                                MobileNo = a.MobileNo,
                                Address =  a.Address1,
                           

                            }).ToList();
                radGridView1.DataSource = query;


                exporter = new RadGridViewExcelExporter();
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                worker.RunWorkerAsync(saveFileDialog1.FileName);
                exporter.Progress += new ProgressHandler(exportProgress);

                this.btnExport.Enabled = false;
              
            }
        }

        void  worker_DoWork(object sender, DoWorkEventArgs e)
        {
            exporter.Export(this.radGridView1, (String)e.Argument, "Customer List");  
        }

        
    
    
 
    
    //Update the progress bar with the export progress    
    private void exportProgress(object sender, ProgressEventArgs e) 
    {     
        // Call InvokeRequired to check if thread needs marshalling, to access properly the UI thread.
        if (this.InvokeRequired)
        {
            this.Invoke(new EventHandler(
            delegate
            {
                if (e.ProgressValue <= 100)
                {
                    radProgressBar1.Value1 = e.ProgressValue;
                }
                else
                {
                    radProgressBar1.Value1 = 100;
                }
            }));
        }
        else
        {
            if (e.ProgressValue <= 100)
            {
                radProgressBar1.Value1 = e.ProgressValue;
            }
            else
            {
                radProgressBar1.Value1 = 100;
            }
        }     
   }     
    // when the worker finishes the export, we can do some post processing   
    private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) 
    {          
            this.btnExport.Enabled = true;    
            this.radProgressBar1.Value1 = 0;     
            //   RadMessageBox.SetThemeName(this.grdLister.ThemeName);   
            //RadMessageBox.Show("The data in the grid was exported successfully.", "Export to Excel");  
            ENUtils.ShowMessage("Export successfully.");  
    }

    private void grdLister_MouseDown(object sender, MouseEventArgs e)
    {
        try
        {
            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
            {
                Clipboard.Clear();
                if (this.grdLister.CurrentCell != null && this.grdLister.CurrentCell.Value.ToStr().Trim().Length > 0)
                {

                    Clipboard.SetText(this.grdLister.CurrentCell.Value.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }




    }
}

