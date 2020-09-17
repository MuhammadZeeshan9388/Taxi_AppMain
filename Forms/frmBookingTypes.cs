using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using System.IO;
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;
using DAL;
namespace Taxi_AppMain
{
    public partial class frmBookingTypes : UI.SetupBase
    {
        public struct COLS
        {
            public static string Id = "Id";
            public static string BookingType = "BookingType";
            public static string BackgroundColor = "BackgroundColor";
            public static string BackgroundColorValue = "BackgroundColorValue";
            public static string BackgroundColorOldValue = "BackgroundColorOldValue";

        }


        BookingTypeBO objMaster;
        public frmBookingTypes()
        {
            InitializeComponent();

            objMaster = new BookingTypeBO();
            this.SetProperties((INavigation) objMaster);

            FormatGrid();
            this.Load += new EventHandler(frmBookingTypes_Load);
            this.FormClosed += new FormClosedEventHandler(frmBookingTypes_FormClosed);
        }

        void frmBookingTypes_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        void frmBookingTypes_Load(object sender, EventArgs e)
        {
            PopulateData();
        }
        public void FormatGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
            grdBookingType.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.BookingType;
            col.ReadOnly = true;
            col.Width = 150;
            col.HeaderText = "Booking Type";
            grdBookingType.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.BackgroundColor;
            col.ReadOnly = true;
            col.HeaderText = " ";
            col.Width = 100;
            grdBookingType.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = COLS.BackgroundColorValue;
            col.ReadOnly = false;
            col.HeaderText = " ";
            col.IsVisible = false;
            grdBookingType.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = COLS.BackgroundColorOldValue;
            col.ReadOnly = false;
            col.HeaderText = " ";
            col.IsVisible = false;
            grdBookingType.Columns.Add(col);



         

            GridViewCommandColumn colPick=new GridViewCommandColumn();
            colPick.UseDefaultText=true;
            colPick.HeaderText=" ";
            colPick.Name="Pick";
            colPick.DefaultText = "Pick";
            grdBookingType.Columns.Add(colPick);

             
           
           GridViewCommandColumn colClear=new GridViewCommandColumn();
            colClear.UseDefaultText=true;
            colClear.HeaderText=" ";
            colClear.Name="Clear";
            colClear.DefaultText = "Clear";
            grdBookingType.Columns.Add(colClear);
                           

            //grdLocationTypes.Columns["LocationType"].HeaderText = "Location Type";
            //grdLocationTypes.Columns["ShortCutKey"].HeaderText = "Short Cut Key";
            //grdLocationTypes.Columns["LocationType"].Width = 200;

            //grdLocationTypes.Columns["ShortCutKey"].Width = 157;
            //grdLocationTypes.Columns["ShortKey"].IsVisible = false;
            //grdLocationTypes.Columns["Id"].IsVisible = false;

             grdBookingType.CommandCellClick+=new CommandCellClickEventHandler(grdBookingType_CommandCellClick);
             grdBookingType.ViewCellFormatting += new CellFormattingEventHandler(grdBookingType_ViewCellFormatting);
             grdBookingType.TableElement.RowHeight = 40;
        }

        void grdBookingType_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                if (e.CellElement is GridDataCellElement)
                {


                    //  e.CellElement.DrawFill = false;


                      

                    e.CellElement.DrawFill = false;


                 
                 

                    if (e.Column.Name == "BackgroundColor")
                    {

                        e.CellElement.NumberOfColors = 1;


                        int bgColor = e.Row.Cells["BackgroundColorValue"].Value.ToInt();

                        if (bgColor != 0)
                        {

                            e.CellElement.BackColor = Color.FromArgb(bgColor);
                            e.CellElement.DrawFill = true;
                        }
                        else
                        {
                            e.CellElement.BackColor = Color.White;
                            e.CellElement.DrawFill = true;

                        }
                    }





                }
            }
            catch
            {

            }
        }

        void grdBookingType_CommandCellClick(object sender, EventArgs e)
        {
           
             GridCommandCellElement gridCell = (GridCommandCellElement)sender;
             string name = gridCell.ColumnInfo.Name.ToLower();

             GridViewRowInfo row = gridCell.RowElement.RowInfo;
    
          
             if (name == "pick")
             {
                 SetColor(row);
             }
            
          
             else if (name == "clear")
             {

                 ClearColor(row);
             }

           
         }



        private void SetColor(GridViewRowInfo txt)
        {
            if (DialogResult.OK == colorDialog1.ShowDialog())
            {

             //   if (txt.Cells[1].Style == null)
             //       txt.Cells[1].Style = new GridViewCellStyle();

                txt.Cells["BackgroundColorValue"].Value = colorDialog1.Color.ToArgb();
            
            }

        }


        private void ClearColor(GridViewRowInfo txt)
        {
          //  if (txt.Cells[1].Style == null)
         //       txt.Cells[1].Style = new GridViewCellStyle();

         
            txt.Cells["BackgroundColorValue"].Value = null;
           

        }

       

      


        public override void PopulateData()
        {
            try
            {
                var data1 = General.GetQueryable<BookingType>(c => c.BookingTypeName != "Local").ToList();

                grdBookingType.RowCount = data1.Count;
                for (int i = 0; i < data1.Count; i++)
                {
                    grdBookingType.Rows[i].Cells[COLS.Id].Value = data1[i].Id;
                    grdBookingType.Rows[i].Cells[COLS.BookingType].Value = data1[i].BookingTypeName;
               //    grdBookingType.Rows[i].Cells[COLS.BackgroundColor].Value = data1[i].BackgroundColor;
                    grdBookingType.Rows[i].Cells[COLS.BackgroundColorValue].Value = data1[i].BackgroundColor;
                    grdBookingType.Rows[i].Cells[COLS.BackgroundColorOldValue].Value = data1[i].BackgroundColor;
                }

            }
            catch (Exception ex)
            {


            }
        }


        public override void Save()
        {
            try
            {


                foreach (var item in grdBookingType.Rows)
                {

                    if (item.Cells[COLS.BackgroundColorValue].Value.ToInt() != item.Cells[COLS.BackgroundColorOldValue].Value.ToInt())
                    {
                        objMaster = new BookingTypeBO();
                        objMaster.GetByPrimaryKey(item.Cells[COLS.Id].Value.ToInt());

                        if (objMaster.Current != null)
                        {
                            objMaster.Current.BackgroundColor = item.Cells[COLS.BackgroundColorValue].Value.ToIntorNull();

                            objMaster.Save();

                        }

                    }


                    

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }


        }

        
        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            SaveAndClose();
        }

       


        

       
    }
}
