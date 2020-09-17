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
using UI;
using Utils;

namespace Taxi_AppMain
{
    public partial class frmFareMeterSetting : UI.SetupBase
    {
        SysPolicyBO objMaster;
        decimal MeterRoundedValue = 0;
        int FareMeterType = 0;
        public frmFareMeterSetting()
        {
            InitializeComponent();
            objMaster = new SysPolicyBO();
            this.SetProperties((INavigation)objMaster);
            MeterTypes();
            FormatGrid();
            this.Load += new EventHandler(frmFareMeterSetting_Load);
            this.ddlFareMeterType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlFareMeterType_SelectedIndexChanged);
        }

        void ddlFareMeterType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //string MeterType = ddlFareMeterType.Text.ToStr().Trim();
            //if (MeterType.StartsWith("Fare Meter 1"))
            //{
            //    MeterRoundedValue = 0;
            //    spnMeterRoundedValue.Visible = false;
            //    lblFareValue.Visible = false;
            //}
            //else if (MeterType.StartsWith("Fare Meter 2"))
            //{
            //    spnMeterRoundedValue.Visible = true;
            //    lblFareValue.Visible = true;
            //}
        }

        void frmFareMeterSetting_Load(object sender, EventArgs e)
        {
            try
            {
                
                var list = (from a in General.GetQueryable<Fleet_VehicleType>(null)
                            orderby a.OrderNo
                            select new
                            {
                                VehicleTypeId = a.Id,
                                VehicleType = a.VehicleType
                            }).ToList();


                grdFareMeterSetting.RowCount = list.Count;

                for (int i = 0; i < list.Count; i++)
                {

                    grdFareMeterSetting.Rows[i].Cells[COLS.VehicleTypeId].Value = list[i].VehicleTypeId.ToInt();
                    grdFareMeterSetting.Rows[i].Cells[COLS.VehicleType].Value = list[i].VehicleType.ToStr();
                    grdFareMeterSetting.Rows[i].Cells[COLS.HasMeter].Value = false;
                }

              
                Gen_SysPolicy obj = General.GetQueryable<Gen_SysPolicy>(null).FirstOrDefault();
                if (obj != null)
                {
                    objMaster.GetByPrimaryKey(obj.Id);
                    DisplayRecord();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }            
        }
        public void MeterTypes()
        {
            RadListDataItem item=new RadListDataItem();
            item.Text="Fare Meter 1 (BASIC)";
            item.Value=1;

            ddlFareMeterType.Items.Add(item);

            item = new RadListDataItem();
            item.Text = "Fare Meter 2 (ADVANCED)";
            item.Value = 2;
            ddlFareMeterType.Items.Add(item);
        }

        
        public override void DisplayRecord()
        {


            if (objMaster.Current == null) return;


            try
            {

                if (objMaster.Current.Gen_SysPolicy_Configurations.Count == 1)
                {

                    int PolicyId = objMaster.Current.Gen_SysPolicy_Configurations[0].FareMeterType.ToInt();
                    ddlFareMeterType.SelectedValue = PolicyId;

                    //if (PolicyId == 2)
                    //{
                       
                    //    ddlFareMeterType.Text ="Fare Meter 2(ADVANCED)";

                    //}
                    //else
                    //{
                    //   // ddlFareMeterType.Enabled = false;
                    //    ddlFareMeterType.SelectedIndex = 0;
                    //    spnMeterRoundedValue.Visible = false;
                    //}



                    chkEnablePeakOffPeakWiseFares.Checked = objMaster.Current.Gen_SysPolicy_Configurations[0].EnablePeakOffPeakFares.ToBool();
               


                    spnMeterRoundedValue.Value = objMaster.Current.Gen_SysPolicy_Configurations[0].FareMeterRoundedCalc.ToDecimal();

                    var list = (from a in General.GetQueryable<Gen_SysPolicy_FareMeterSetting>(c => c.SysPolicyId != null)
                                select new
                                {
                                    Id = a.Id,
                                    SysPolicyId = a.SysPolicyId,
                                    VehicleTypeId = a.VehicleTypeId,
                                    HasMeter = a.HasMeter,
                                    AutoStartWaiting=a.AutoStartWaiting,
                                    a.AutoStartWaitingBelowSpeed,
                                    a.AutoStartWaitingBelowSpeedSeconds,
                                    AutoStopWaitingSpeed=a.AutoStopWaitingOnSpeed,
                                    DrvWaitingCharges=a.DrvWaitingChargesPerMin,
                                    WaitingTime=a.AccWaitingChargesPerMin
                                }).ToList();


                    GridViewRowInfo row = null;
                    foreach (var item in list)
                    {
                        row = grdFareMeterSetting.Rows.FirstOrDefault(c => c.Cells[COLS.VehicleTypeId].Value.ToIntorNull() == item.VehicleTypeId);

                        if (row != null)
                        {
                            row.Cells[COLS.VehicleTypeId].Value = item.VehicleTypeId.ToInt();
                            row.Cells[COLS.Id].Value = item.Id.ToInt();
                            row.Cells[COLS.HasMeter].Value = item.HasMeter.ToBool();
                            row.Cells[COLS.SysPolicyId].Value = item.SysPolicyId.ToIntorNull();

                            row.Cells[COLS.AutoStartWaiting].Value = item.AutoStartWaiting.ToBool();
                            row.Cells[COLS.AutoStartWaitingSpeedLimit].Value = item.AutoStartWaitingBelowSpeed.ToDecimal();
                            row.Cells[COLS.AutoStartWaitingSecondsLimit].Value = item.AutoStartWaitingBelowSpeedSeconds.ToInt();


                            row.Cells[COLS.AutoStopWaitingOnSpeed].Value = item.AutoStopWaitingSpeed.ToDecimal();
                            row.Cells[COLS.DrvWaitingCharges].Value = item.DrvWaitingCharges.ToDecimal();
                            row.Cells[COLS.WaitingTime].Value = item.WaitingTime.ToDecimal();


                        }
                    }


                    //for (int i = 0; i < list.Count; i++)
                    //{
                    //    if (grdFareMeterSetting.Rows[i].Cells[COLS.VehicleTypeId].Value.ToInt() == list[i].VehicleTypeId.Value.ToInt())
                    //    {
                    //        grdFareMeterSetting.Rows[i].Cells[COLS.VehicleTypeId].Value = list[i].VehicleTypeId.ToInt();
                    //        grdFareMeterSetting.Rows[i].Cells[COLS.Id].Value = list[i].Id.ToInt();
                    //        grdFareMeterSetting.Rows[i].Cells[COLS.HasMeter].Value = list[i].HasMeter.ToBool();
                    //        grdFareMeterSetting.Rows[i].Cells[COLS.SysPolicyId].Value = list[i].SysPolicyId.ToInt();
                    //    }
                    //}

                }

            }
            catch (Exception ex)
            {


            }
        }
        public struct COLS
        {
            public static string Id = "Id";
            public static string SysPolicyId = "SysPolicyId";
            public static string VehicleTypeId = "VehicleTypeId";
            public static string VehicleType = "VehicleType";
            public static string HasMeter = "HasMeter";
            public static string AutoStartWaiting = "AutoStartWaiting";
            public static string AutoStartWaitingSpeedLimit = "AutoStartWaitingSpeedLimit";
            public static string AutoStartWaitingSecondsLimit = "AutoStartWaitingSecondsLimit";

            public static string AutoStopWaitingOnSpeed = "AutoStopWaitingOnSpeed";
            public static string DrvWaitingCharges = "DrvWaitingCharges";
            public static string AccountWaitingCharges = "AccountWaitingCharges";
            public static string WaitingTime = "WaitingTime";
        }
        public void FormatGrid()
        {
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdFareMeterSetting.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.SysPolicyId;
            col.IsVisible = false;
            grdFareMeterSetting.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.VehicleTypeId;
            col.IsVisible = false;
            grdFareMeterSetting.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.VehicleType;
            col.HeaderText = "Vehicle Type";
            col.ReadOnly = true;
            col.Width = 130;
            col.IsPinned = true;
            grdFareMeterSetting.Columns.Add(col);


            GridViewCheckBoxColumn ckcol = new GridViewCheckBoxColumn();
            ckcol.Name = COLS.HasMeter;
            ckcol.HeaderText = "Has Meter";
            ckcol.Width = 80;
            grdFareMeterSetting.Columns.Add(ckcol);


            ckcol = new GridViewCheckBoxColumn();
            ckcol.Name = COLS.AutoStartWaiting;
            ckcol.HeaderText = "AutoStart Waiting";
            ckcol.Width = 110;            
            grdFareMeterSetting.Columns.Add(ckcol);


            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Maximum = 50;
            colD.HeaderText = "AutoStart Waiting on Speed Limit(mph)";
            colD.Name = COLS.AutoStartWaitingSpeedLimit;
            colD.Minimum = 0;
            colD.Width = 210;
            grdFareMeterSetting.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Maximum = 600;
            colD.HeaderText = "AutoStart Waiting Seconds";
            colD.Name = COLS.AutoStartWaitingSecondsLimit;
            colD.Minimum = 0;
            colD.Width = 150;
            grdFareMeterSetting.Columns.Add(colD);



             colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Maximum = 50;
            colD.HeaderText = "AutoStop Waiting On Speed(mph)";
            colD.Name = COLS.AutoStopWaitingOnSpeed;
            colD.Minimum = 0;
            colD.Width = 180;
            grdFareMeterSetting.Columns.Add(colD);

            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Maximum = 500;
            colD.HeaderText = "Waiting Charges";
            colD.Name = COLS.DrvWaitingCharges;
            colD.Minimum = 0;
            colD.Width = 100;
            grdFareMeterSetting.Columns.Add(colD);


            colD = new  GridViewDecimalColumn();
            colD.DecimalPlaces = 0;
            colD.DataEditFormatString = "{0:F0}";
            colD.FormatString = "{0:F0}";
            colD.Maximum = 500;
            colD.HeaderText = "Waiting Time(secs)";
            colD.Name = COLS.WaitingTime;
            colD.Minimum = 0;
            colD.Width = 110;
            grdFareMeterSetting.Columns.Add(colD);


            //colD = new GridViewDecimalColumn();
            //colD.DecimalPlaces = 2;
            //colD.Maximum = 140;
            //colD.HeaderText = "A/C Waiting Charges/mins)";
            //colD.Name = COLS.AccountWaitingCharges;
            //colD.Minimum = 0;
            //colD.Width = 200;
            //grdFareMeterSetting.Columns.Add(colD);


            GridViewCommandColumn cmdCol = new GridViewCommandColumn();
            cmdCol.UseDefaultText = true;
            cmdCol.DefaultText = "Meter Settings";
            cmdCol.Width = 100;
            cmdCol.Name = "btnmeter";
            cmdCol.IsVisible = true;
            grdFareMeterSetting.Columns.Add(cmdCol);


            grdFareMeterSetting.CommandCellClick += new CommandCellClickEventHandler(grdFareMeterSetting_CommandCellClick);
           

        }

        void grdFareMeterSetting_CommandCellClick(object sender, EventArgs e)
        {


            try
            {
                if (grdFareMeterSetting.CurrentColumn != null)
                {
                    if (grdFareMeterSetting.CurrentColumn.Name == "btnmeter")
                    {

                        int vehicleTypeId = grdFareMeterSetting.CurrentRow.Cells[COLS.VehicleTypeId].Value.ToInt();

                      //  General.GetObject<Fare>(c => c.VehicleTypeId == vehicleTypeId);

                        AppVars.objPolicyConfiguration.EnablePeakOffPeakFares = chkEnablePeakOffPeakWiseFares.Checked;



                        if (AppVars.objPolicyConfiguration.FareMeterType.ToInt() == 2)
                        {
                            using (frmSpecialDayFares frm = new frmSpecialDayFares())
                            {
                                frm.ShowDialog();
                            }

                        }
                        else
                        {

                            using (frmPDAMeterFares frm = new frmPDAMeterFares(vehicleTypeId))
                            {
                                frm.ShowDialog();
                            }
                        }

                        GC.Collect();

                    }


                }

            }
            catch
            {


            }

        }


      

        public override void Save()
        {

            try
            {


                string MeterType = ddlFareMeterType.Text.ToStr().Trim();
                if (string.IsNullOrEmpty(MeterType))
                {
                    ENUtils.ShowMessage("Required : Fare Meter Type");
                    return;
                }
                if (MeterType =="Fare Meter 2(ADVANCED)")
                {
                    MeterRoundedValue = spnMeterRoundedValue.Value.ToDecimal();
                    FareMeterType = 2;
                }
                else
                {
                    FareMeterType = 1;
                    MeterRoundedValue = 0;
                }

               

                Gen_SysPolicy obj = General.GetQueryable<Gen_SysPolicy>(null).FirstOrDefault();
                if (obj != null)
                {
                    objMaster.GetByPrimaryKey(obj.Id);
                    objMaster.Edit();
                }


                objMaster.Current.Gen_SysPolicy_Configurations[0].FareMeterType = ddlFareMeterType.SelectedValue.ToInt();
                objMaster.Current.Gen_SysPolicy_Configurations[0].FareMeterRoundedCalc = MeterRoundedValue.ToDecimal();

                //"Gen_SysPolicy",
                string[] skipProperties = new string[] { "Gen_SysPolicy","Fleet_VehicleType" };
                List<Gen_SysPolicy_FareMeterSetting> listofFares = (from a in grdFareMeterSetting.Rows
                                                                select new Gen_SysPolicy_FareMeterSetting
                                                                {
                                                                    Id = a.Cells[COLS.Id].Value.ToInt(),
                                                                    SysPolicyId = objMaster.Current.Id,
                                                                    VehicleTypeId = a.Cells[COLS.VehicleTypeId].Value.ToIntorNull(),
                                                                    HasMeter = a.Cells[COLS.HasMeter].Value.ToBool(),
                                                                    AutoStartWaiting = a.Cells[COLS.AutoStartWaiting].Value.ToBool(),
                                                                     AutoStartWaitingBelowSpeed=a.Cells[COLS.AutoStartWaitingSpeedLimit].Value.ToDecimal(),
                                                                      AutoStartWaitingBelowSpeedSeconds=a.Cells[COLS.AutoStartWaitingSecondsLimit].Value.ToInt(),
                                                                     AutoStopWaitingOnSpeed=a.Cells[COLS.AutoStopWaitingOnSpeed].Value.ToDecimal(),
                                                                      DrvWaitingChargesPerMin=a.Cells[COLS.DrvWaitingCharges].Value.ToDecimal(),
                                                                      AccWaitingChargesPerMin=a.Cells[COLS.WaitingTime].Value.ToDecimal()
                                                                }).ToList();


                


                IList<Gen_SysPolicy_FareMeterSetting> savedListFares = objMaster.Current.Gen_SysPolicy_FareMeterSettings;
                Utils.General.SyncChildCollection(ref savedListFares, ref listofFares, "Id", skipProperties);






                bool IsUpdated = false;

                if (objMaster.Current.Gen_SysPolicy_Configurations[0].EnablePeakOffPeakFares.ToBool() != chkEnablePeakOffPeakWiseFares.Checked
                    || objMaster.Current.Gen_SysPolicy_Configurations[0].FareMeterType.ToInt() != ddlFareMeterType.SelectedValue.ToInt())
                {



                    objMaster.Current.Gen_SysPolicy_Configurations[0].EnablePeakOffPeakFares = chkEnablePeakOffPeakWiseFares.Checked;

                    IsUpdated = true;
                 

                }
                
                
                objMaster.Save();


                if (IsUpdated)
                {
                    General.LoadConfiguration(); 

                }

              
               
                Close();
            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }


        private void UpdateMeterSettings()
        {



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
