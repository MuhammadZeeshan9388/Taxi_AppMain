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


namespace Taxi_AppMain
{
    public partial class frmLocalization : UI.SetupBase
    {
        public struct COLS
        {
            public static string Id = "Id";
            public static string MasterId = "MasterId";
            public static string Area = "Area";

            public static string PostCode = "PostCode";



        }


        public frmLocalization()
        {
            InitializeComponent();

        
      
           this.Shown += new EventHandler(frmZones_Shown);
           this.btnSaveClose.Click += new EventHandler(btnSaveClose_Click);
           this.btnExit1.Click += new EventHandler(btnExit1_Click);

           FormatGrid();
        }

        void btnSaveClose_Click(object sender, EventArgs e)
        {
            Save();
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void frmZones_Shown(object sender, EventArgs e)
        {
            DisplayRecord();
            txtPostCode.Focus();
        }

   
     

        #region Overridden Methods


      

        public override void DisplayRecord()
        {
            grdPostCodes.Rows.Clear();
            GridViewRowInfo row=null;
            
            using (TaxiDataContext db = new TaxiDataContext())
            {
                var list = db.stp_GetLocalizationDetails().Where(c=>c.DetailId!=null).ToList();
                foreach (var item in list)
                {
                    row = grdPostCodes.Rows.AddNew();
                    row.Cells[COLS.Id].Value = item.MasterId;
                    row.Cells[COLS.MasterId].Value = item.DetailId;
                    row.Cells[COLS.PostCode].Value = item.PostCode;
                }
            }
            
            grdPostCodes.CurrentRow = null;
        }




        public override void Save()
        {
            try
            {
                if (grdPostCodes.RowCount == 0)
                {
                    ENUtils.ShowMessage("Required : Post Code");
                    return;
                }

                StringBuilder sb = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                //sb.Append("delete from StreetsData.dbo.Localization;");
                //sb.Append("delete from StreetsData.dbo.LocalizationDetails;");
                List<string> objList = new List<string>();
                string PostCode = string.Join(",", grdPostCodes.Rows.Select(c => c.Cells[COLS.PostCode].Value.ToStr()).ToArray<string>());
                string NewPostCode = string.Empty;

                foreach (var item in PostCode.Split(','))
                {
                    string Word = string.Empty;

                    for (int i = 0; i < item.Length; i++)
                    {
                        if (item[i].ToStr().IsNumeric() == true)
                        {
                            objList.Add(Word);
                            break;
                        }
                        else
                        {
                            Word += item[i].ToStr();
                        }
                    }
                }

                //var list = objList.Distinct().ToList();
                //foreach (var item in list)
                //{
                //    sb.Append("insert into PAFDb.dbo.Localization (PostCode) values ('" + item.ToStr() + "%" + "');"); 
                //}


                var list2 = grdPostCodes.Rows.Select(c => c.Cells[COLS.PostCode].Value).ToList();

                foreach (var item in list2)
                {
                    sb2.Append("insert into PAFDb.dbo.LocalizationDetail (PostCode) values ('" + item.ToStr() + "');");
                }
             
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.stp_SaveLocalaization(sb.ToStr(),sb2.ToStr());
                }


                General.LoadZoneList();

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
            txtPostCode.Focus();
        }

        #endregion

   

        private void FormatGrid()
        {
            grdPostCodes.AllowAutoSizeColumns = true;
            grdPostCodes.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdPostCodes.AllowAddNewRow = false;
            grdPostCodes.ShowGroupPanel = false;
            grdPostCodes.AutoCellFormatting = true;
            grdPostCodes.ShowRowHeaderColumn = false;

            //grdDocuments.CommandCellClick += new CommandCellClickEventHandler(grdDocuments_CommandCellClick);

            grdPostCodes.CommandCellClick += new CommandCellClickEventHandler(grdPostCodes_CommandCellClick);

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.Id;
            grdPostCodes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.MasterId;
            grdPostCodes.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Post Code";
            col.Name = COLS.PostCode;
            col.Width = 100;
            col.ReadOnly = true;

            grdPostCodes.Columns.Add(col);

           // UI.GridFunctions.AddDeleteColumn(grdPostCodes);
            GridViewCommandColumn cmdcol = new GridViewCommandColumn();
            cmdcol.Width = 70;

            cmdcol.Name = "coldelete";
            cmdcol.UseDefaultText = true;
            cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdcol.DefaultText = "Delete";
            cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
          
            grdPostCodes.Columns.Add(cmdcol);



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

        private void frmZones_FormClosed(object sender, FormClosedEventArgs e)
        {
            General.DisposeForm(this);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearPostCode();
        }

        private void ClearPostCode()
        {

            grdPostCodes.CurrentRow = null;
            txtPostCode.Text = string.Empty;
            
            txtPostCode.Focus();

        }

        private void btnAddPostCode_Click(object sender, EventArgs e)
        {
            AddPostCode();
        }

        private void AddPostCode()
        {
            string postCode = txtPostCode.Text.ToStr().Trim();
          
            string error = string.Empty;

         

            if (string.IsNullOrEmpty(postCode))
            {
                error += "Required : Post Code";
            }       

            if (grdPostCodes.CurrentRow != null && grdPostCodes.CurrentRow is GridViewNewRowInfo)
                grdPostCodes.CurrentRow = null;


            int rowIndex = grdPostCodes.CurrentRow != null ? grdPostCodes.CurrentRow.Index : -1;


            //if (grdPostCodes.Rows.Count(c => c.Cells[COLS.PostCode].Value.ToStr() == postCode && c.Index != rowIndex) > 0)
            if (grdPostCodes.Rows.Count(c => c.Cells[COLS.PostCode].Value.ToStr() == postCode) > 0)
            {
                error += "Post Code already exist..";

            }
            
            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;
            }


            GridViewRowInfo row = null;
            if (grdPostCodes.CurrentRow != null)
            {
                row = grdPostCodes.CurrentRow;
             
            }

            else
            {
                row = grdPostCodes.Rows.AddNew();

            }
            row.Cells[COLS.PostCode].Value = postCode;



            ClearPostCode();




        }

        private void grdPostCodes_DoubleClick(object sender, EventArgs e)
        {
            if (grdPostCodes.CurrentRow != null && grdPostCodes.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdPostCodes.CurrentRow;
              
                txtPostCode.Text = row.Cells[COLS.PostCode].Value.ToStr();


            }
        }


        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MoveRow(true);
        }


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

        private void btnMoveDownZone_Click(object sender, EventArgs e)
        {
            MoveRow(false);
        }
    }
}
