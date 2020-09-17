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
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmPricePlots : UI.SetupBase
    {
        public struct COLS_PricePlot
        {
            public static string ID = "ID";

            public static string NAME = "NAME";

            public static string FROMPRICE = "FROMPRICE";

            public static string TILLPRICE = "TILLPRICE";

        }


        PricePlotsBO objMaster;
        public frmPricePlots()
        {
            InitializeComponent();

            objMaster = new PricePlotsBO();
            this.SetProperties((INavigation)objMaster);
            FormatGrid();
            PopulateData();
        }

       

   
     

        #region Overridden Methods




        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;
            numfromprice.Value = objMaster.Current.FromPrice.ToInt();
            numTillprice.Value = objMaster.Current.TillPrice.ToInt();
        }

        public override void PopulateData()
        {
            try
            {
                var list = General.GetQueryable<Gen_PricePlot>(c => c.Id > 0).ToList();

                grdPriceplot.RowCount = list.Count;
                for (int i = 0; i < grdPriceplot.Rows.Count; i++)
                {
                    grdPriceplot.Rows[i].Cells[COLS_PricePlot.ID].Value = list[i].Id;
                    grdPriceplot.Rows[i].Cells[COLS_PricePlot.NAME].Value = list[i].PlotName;
                    if (list[i].FromPrice.ToInt() > 0)
                    {
                        grdPriceplot.Rows[i].Cells[COLS_PricePlot.FROMPRICE].Value = list[i].FromPrice.ToInt();
                    }
                    else
                    {
                        grdPriceplot.Rows[i].Cells[COLS_PricePlot.FROMPRICE].Value = null;
                    }
                    if (list[i].TillPrice.ToInt() > 0)
                    {
                    grdPriceplot.Rows[i].Cells[COLS_PricePlot.TILLPRICE].Value = list[i].TillPrice.ToInt();
                    }
                    else
                    {
                        grdPriceplot.Rows[i].Cells[COLS_PricePlot.TILLPRICE].Value = null;
                    }
                }

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
                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    objMaster.Edit();
                }

                string Plotnames = string.Empty;
                if (numTillprice.Value > 0)
                {
                    Plotnames = numfromprice.Value + " tO " + numTillprice.Value;
                    objMaster.Current.TillPrice = numTillprice.Value.ToInt();
                }

                else
                {
                    Plotnames = numfromprice.Value + "+";
                    objMaster.Current.TillPrice = null;
                }
                objMaster.Current.PlotName = Plotnames.ToString();
                objMaster.Current.ShortName = Plotnames.ToStr();
                objMaster.Current.FromPrice = numfromprice.Value.ToInt();
            
                objMaster.Save();
                PopulateData();
                ClearPricePlot();

            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }
        }


        public override void  AddNew()
        {
         	 OnNew();
        }

        public override void  OnNew()
        {
 	      
        }

        #endregion

   

        private void FormatGrid()
        {
            grdPriceplot.AllowAutoSizeColumns = true;
            grdPriceplot.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdPriceplot.AllowAddNewRow = false;
            grdPriceplot.ShowGroupPanel = false;
            grdPriceplot.AutoCellFormatting = true;
            grdPriceplot.ShowRowHeaderColumn = false;

            //grdDocuments.CommandCellClick += new CommandCellClickEventHandler(grdDocuments_CommandCellClick);


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS_PricePlot.ID;
            grdPriceplot.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Name";
            col.Name = COLS_PricePlot.NAME;
            col.Width = 100;
            col.ReadOnly = true;
            grdPriceplot.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "From";
            col.Name = COLS_PricePlot.FROMPRICE;
            col.ReadOnly = true;
            col.Width = 60;
            grdPriceplot.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Till";
            col.Name = COLS_PricePlot.TILLPRICE;
            col.ReadOnly = true;
            col.Width = 60;
            grdPriceplot.Columns.Add(col);

            //UI.GridFunctions.AddDeleteColumn(grdPriceplot);

            grdPriceplot.AddEditColumn();
            grdPriceplot.Columns["btnEdit"].Width = 60;

            grdPriceplot.AddDeleteColumn();
            grdPriceplot.Columns["btnDelete"].Width = 80;


            //UI.GridFunctions.AddDeleteColumn(grdPriceplot);

           

            grdPriceplot.EnableFiltering = true;
          //  UI.GridFunctions.SetFilter(grdPriceplot);

          

        }

        private void frmZones_FormClosed(object sender, FormClosedEventArgs e)
        {
         

            General.DisposeForm(this);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearPricePlot();
        }

        private void ClearPricePlot()
        {

            grdPriceplot.CurrentRow = null;

            numfromprice.Value = 0;
            numTillprice.Value = 0;
            numfromprice.Focus();
            objMaster.Clear();

        }
   
        private void grdPostCodes_DoubleClick(object sender, EventArgs e)
        {
            if (grdPriceplot.CurrentRow != null && grdPriceplot.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdPriceplot.CurrentRow;

            }
        }

        private void btnAddPriceplot_Click(object sender, EventArgs e)
        {
            Save();

        }

        private void grdPriceplot_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            RadGridView grid = gridCell.GridControl;
            if (gridCell.ColumnInfo.Name == "btnDelete")
            {

                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Price Plot ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    int Id = grdPriceplot.CurrentRow.Cells[COLS_PricePlot.ID].Value.ToInt();
                    objMaster.GetByPrimaryKey(Id);
                    objMaster.Delete(objMaster.Current);
                    grid.CurrentRow.Delete();
                }
            }
            else if (gridCell.ColumnInfo.Name == "btnEdit")
            {
                int Id = grdPriceplot.CurrentRow.Cells[COLS_PricePlot.ID].Value.ToInt();
                objMaster.GetByPrimaryKey(Id);
                DisplayRecord();


            }


        }

        private void numfromprice_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    string Error = "";
            //    var list = General.GetQueryable<Gen_PricePlot>(c => c.Id > 0).ToList();
            //    grdPriceplot.RowCount = list.Count;
            //    foreach (var item in grdPriceplot.Rows)
            //    {
            //        if (numfromprice.Value.ToInt() == item.Cells[COLS_PricePlot.FROMPRICE].Value.ToInt() || (numfromprice.Value.ToInt() >= item.Cells[COLS_PricePlot.FROMPRICE].Value.ToInt() && numfromprice.Value.ToInt() <= item.Cells[COLS_PricePlot.TILLPRICE].Value.ToInt()))
            //        {
            //            Error = "Already Exist";
            //            ENUtils.ShowMessage(Error);
            //            numfromprice.Value = 0;
            //            return;
            //        }

            //    }
               
            //}
            //catch (Exception ex)
            //{
            //    ENUtils.ShowMessage(ex.Message);
            //}

        }

        
    }



}
