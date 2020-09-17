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
    
    public partial class frmSinBinSettings : UI.SetupBase
    {
        public struct COLS
        {
            public static string Id = "Id";
            public static string SinBinTypeId = "SinBinTypeId";
            public static string SinBinType = "SinBinType";
            public static string Minutes = "Minutes";
           

        }


        SysPolicyBO objMaster;
        public frmSinBinSettings()
        {
            InitializeComponent();

            objMaster = new SysPolicyBO();
            this.SetProperties((INavigation) objMaster);
            objMaster.GetByPrimaryKey(1);
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
            grdSinBin.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "SinBinTypeId";
            grdSinBin.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.SinBinType;
            col.ReadOnly = true;
            col.Width = 150;
            col.HeaderText = "Sin Bin Type";
            
            grdSinBin.Columns.Add(col);

            GridViewDecimalColumn col1 = new GridViewDecimalColumn();
            col1.Name = COLS.Minutes;
           // col1.ReadOnly = true;
            col1.HeaderText = "Minutes";
            col1.DecimalPlaces = 0;
            col1.Width = 80;
            grdSinBin.Columns.Add(col1);


                                

             grdSinBin.CommandCellClick+=new CommandCellClickEventHandler(grdBookingType_CommandCellClick);
             grdSinBin.ViewCellFormatting += new CellFormattingEventHandler(grdBookingType_ViewCellFormatting);
             grdSinBin.TableElement.RowHeight = 30;
        }

        Font f = new Font("Tahoma", 9, FontStyle.Bold);
        void grdBookingType_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                if (e.CellElement is GridHeaderCellElement)
                {

                    e.CellElement.Font = f;
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
                var data1 = General.GetQueryable<SinBinType>(c => c.Isvisible == true).ToList();

                grdSinBin.RowCount = data1.Count;
                for (int i = 0; i < data1.Count; i++)
                {
                    grdSinBin.Rows[i].Cells[COLS.SinBinTypeId].Value = data1[i].Id;
                    grdSinBin.Rows[i].Cells[COLS.SinBinType].Value = data1[i].SinBinTypeName;
                   
              
                }
                DisplayRecord();

            }
            catch (Exception ex)
            {


            }
        }

        public override void DisplayRecord()
        {
            try
            {
                var objSinBin = General.GetQueryable<Gen_SysPolicy_SinBinSetting>(c => c.SysPolicyId == 1).ToList();
                if (objSinBin.Count > 0)
                {

                    for (int i = 0; i < objSinBin.Count; i++)
                    {
                      
                        grdSinBin.Rows[i].Cells[COLS.Id].Value = objSinBin[i].Id;
                        grdSinBin.Rows[i].Cells[COLS.SinBinTypeId].Value = objSinBin[i].SinBinTypeId;
                        grdSinBin.Rows[i].Cells[COLS.Minutes].Value = objSinBin[i].SinBinMinutes;
                       
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




                string[] skipProperties = { "Gen_SysPolicy", "SinBinTypes" };
                IList<Gen_SysPolicy_SinBinSetting> savedList = objMaster.Current.Gen_SysPolicy_SinBinSettings;
                List<Gen_SysPolicy_SinBinSetting> listofDetail = (from r in grdSinBin.Rows

                                                                  select new Gen_SysPolicy_SinBinSetting
                                                            {
                                                                Id = r.Cells[COLS.Id].Value.ToInt(),
                                                                SysPolicyId = objMaster.Current.Id,
                                                                SinBinTypeId = r.Cells[COLS.SinBinTypeId].Value.ToInt(),
                                                                SinBinMinutes =  r.Cells[COLS.Minutes].Value.ToInt(),
                                                               

                                                            }).ToList();


                Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);

                objMaster.Save();

                //objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);

                CreateSINBINZone();

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

        private void CreateSINBINZone()
        {
            try
            {
                if (General.GetQueryable<Gen_Zone>(c => c.ZoneName == "SIN BIN").Count() == 0)
                {
                    ZoneBO objZone = new ZoneBO();
                    objZone.New();
                    objZone.Current.ZoneName = "SIN BIN";
                    objZone.Current.ShortName = "SIN BIN";
                    objZone.Current.AddOn = DateTime.Now;
                    objZone.Current.AddBy = AppVars.LoginObj.LuserId.ToInt();
                    objZone.Current.OrderNo = 1000;
                    objZone.Current.ZoneTypeId = 1;
                    objZone.Current.PlotKind = 1;
                    objZone.CheckDataValidation = false;
                    objZone.Save();


                }
            }
            catch
            {


            }


        }

        
        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            SaveAndClose();
        }

        private void pnlHeaderTitle_Paint(object sender, PaintEventArgs e)
        {

        }

       


        

       
    }
}
