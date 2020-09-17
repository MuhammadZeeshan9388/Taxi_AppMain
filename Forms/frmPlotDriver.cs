using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Taxi_Model;
using Utils;
using Telerik.WinControls.UI;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmPlotDriver : Forms.BaseForm
    {
        long DriverId = 0;

        private bool IsPlotting;

        public frmPlotDriver(bool IsPlot)
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmPlotDriver_Shown);

            this.IsPlotting = IsPlot;

            if (IsPlotting == false)
            {
                btnPlot.Text = "Un-Plot";
                lblHeading.Text = "Un-Plot Driver";
            }

        }

        public frmPlotDriver(long Id, bool IsPlot)
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmPlotDriver_Shown);

            this.IsPlotting = IsPlot;

            if (IsPlotting == false)
            {
                btnPlot.Text = "Un-Plot";

                lblHeading.Text = "Un-Plot Driver";

            }
            this.DriverId = Id;

            ddl_Driver.Enabled = false;
        }


        void frmPlotDriver_Shown(object sender, EventArgs e)
        {
            if (IsPlotting)
            {
                if (AppVars.objPolicyConfiguration.EnablePDA.ToBool())
                {
                    ComboFunctions.FillFreezePlottedDriverNoCombo(ddl_Driver);

                }
                else
                   ComboFunctions.FillDriverNoCombo(ddl_Driver);

                ComboFunctions.FillZonesPlottedCombo(ddlZone);
                ComboFunctions.FillVehicleCombo(ddlVehicle);

            }
            else
            {
                Pg_Ordering.Item.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                Pg_Main.Text = "Un-Plot";

                ddlZone.Enabled = false;
                ddlVehicle.Enabled = false;
                lblZone.Enabled = false;

                var list1 = General.GetQueryable<Fleet_DriverQueueList>(a => a.DriverId != null && a.Status == true);

                var query = from a in list1

                            orderby a.QueueDateTime
                            select new
                            {
                                Id = a.Id,
                                DriverNo = a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName,
                            };

                ComboFunctions.FillCombo(query.ToList(), ddl_Driver, "DriverNo", "Id");


            }

            ddl_Driver.RootElement.Focus();
            ddl_Driver.RootElement.Children[0].Focus();

            IsFormLoaded = true;


            if (DriverId != 0)
            {
                DriverQueueBO objMaster = new DriverQueueBO();
                objMaster.GetByPrimaryKey(DriverId);

                ddl_Driver.SelectedValue = objMaster.Current.DriverId.ToInt();
                ddlZone.SelectedValue = objMaster.Current.ZoneId.ToInt();
                ddlVehicle.SelectedValue = objMaster.Current.FleetMasterId.ToInt();
            }
        }

        private void btnPlot_Click(object sender, EventArgs e)
        {
            int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
            int? zoneId = ddlZone.SelectedValue.ToIntorNull();
            int? FleetId = ddlVehicle.SelectedValue.ToIntorNull();

            string error = string.Empty;

            if (driverId == null)
            {
                error+="Please Select a Driver"+Environment.NewLine;        
        
            }


            if (IsPlotting)
            {
                if (zoneId == null)
                {
                    error += "Please Select a Zone";

                    // ENUtils.ShowMessage("Please Select a Driver");
               
                }
            }
                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;

                }
            

       

       

            if (IsPlotting)
            {
                PlotDriver(driverId,zoneId,FleetId);
            }
            else
            {
                UnPlotDriver(driverId.ToLong());

            }
        }


        public static void Logout(string driverNo)
        {
            Fleet_DriverQueueList obj = General.GetObject<Fleet_DriverQueueList>(c => c.DriverId != null && c.Fleet_Driver.DriverNo == driverNo && c.Status == true);
            if (obj != null)
            {
                General.LogoutDriver(obj.Id);

            }
        }   

        private void UnPlotDriver(long Id)
        {

            General.LogoutDriver(Id.ToLong());
            this.Close();
        }    


        private bool _plotted;

        public bool Plotted
        {
            get { return _plotted; }
            set { _plotted = value; }
        }
        private void PlotDriver(int? driverId, int? zoneId, int? FleetId)
        {
            DriverQueueBO objMaster = null;
            try
            {
                Fleet_DriverQueueList objPlottedDriver = General.GetObject<Fleet_DriverQueueList>(c => c.Status == true && c.DriverId == driverId);

                objMaster = new DriverQueueBO();
                if (objPlottedDriver == null)
                {
                    objMaster.New();
                    objMaster.Current.LoginDateTime = DateTime.Now;
                }
                else
                {
                    objMaster.GetByPrimaryKey(objPlottedDriver.Id);
                }              

                objMaster.Current.FleetMasterId = FleetId;
                objMaster.Current.ZoneId = zoneId;
                objMaster.Current.DriverId = driverId;
                objMaster.Current.Status = true;


                if(objMaster.Current.Fleet_Driver==null || objMaster.Current.Fleet_Driver.HasPDA.ToBool()==false)
                   objMaster.Current.QueueDateTime = DateTime.Now;
              
                objMaster.Save();

                if (objMaster.Current.Fleet_Driver.HasPDA.ToBool())
                {
                    if (zoneId != null)
                    {
                        string zoneName = ddlZone.Text.ToUpper().Trim();
                       

                        if (objMaster.Current.CurrentJobId != null)
                        {

                            string msg = string.Empty;
                            string pickUpPlot = string.Empty;
                            string dropOffPlot = string.Empty;

                             if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "AbbeyCarsleeds")
                             {
                                pickUpPlot = objMaster.Current.ZoneId != null ? "<<<" + objMaster.Current.Booking.Gen_Zone1.ZoneName.ToStr() : "";

                                if (zoneName.Contains("("))
                                    zoneName = zoneName.Remove(zoneName.IndexOf("("));

                                if (zoneName.Contains("."))
                                {
                                    zoneName = zoneName.Substring(zoneName.IndexOf(".") + 1);
                                }

                                dropOffPlot = objMaster.Current.Booking.DropOffZoneId != null ? "<<<" + zoneName.Trim() : "";
                             }

                            string mobNo = objMaster.Current.Booking.CustomerMobileNo;
                            if (string.IsNullOrEmpty(mobNo))
                                mobNo = " ";


                            msg = (!string.IsNullOrEmpty(objMaster.Current.Booking.FromDoorNo) ? objMaster.Current.Booking.FromDoorNo + "-" + objMaster.Current.Booking.FromAddress + pickUpPlot : objMaster.Current.Booking.FromAddress + pickUpPlot) +
                                ">>" +
                                (!string.IsNullOrEmpty(objMaster.Current.Booking.ToDoorNo) ? objMaster.Current.Booking.ToDoorNo + "-" + objMaster.Current.Booking.ToAddress + dropOffPlot : objMaster.Current.Booking.ToAddress + dropOffPlot) +
                                ">>" +
                                string.Format("{0:dd/MM/yyyy   HH:mm}", objMaster.Current.Booking.PickupDateTime) +
                                 ">>" +
                                 objMaster.Current.Booking.CustomerName +
                                 ">>" +
                                 mobNo;


                              // For TCP Connection
                              if (AppVars.objPolicyConfiguration.IsListenAll.ToBool() && !string.IsNullOrEmpty(AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim()))
                              {
                                  new Thread(delegate()
                                  {
                                      General.SendMessageToPDA("request pda=" + driverId + "=" + objMaster.Current.CurrentJobId + "=" + "Update Job>>" + driverId + ">>" + objMaster.Current.CurrentJobId + ">>" + msg + "=8");
                                  }).Start();  
                              }

                              new TaxiDataContext().stp_UpdateJobDropOffPlot(objMaster.Current.CurrentJobId, driverId, zoneId);

                              new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD_DRIVER);

                        }
                    }
                }
                else
                {

                    General.RefreshListWithoutSelected<frmDriverLoginList>("frmDriverLoginList1");
                  //  General.RefreshWaitingDrivers();

                }

        
                this.PlottedDriverId = driverId;
                this.Plotted = true;

                this.Close();
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


 



        private int? plottedDriverId;

        public int? PlottedDriverId
        {
            get { return plottedDriverId; }
            set { plottedDriverId = value; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }


        private void CloseForm()
        {
            this.Close();

        }

        bool IsFormLoaded = false;
        private void ddl_Driver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (IsFormLoaded == false) return;
        
            int? driverId = ddl_Driver.SelectedValue.ToIntorNull();

            if (driverId != null)
            {
                Fleet_DriverQueueList objPlottedDriver = General.GetObject<Fleet_DriverQueueList>(c => c.Status == true && c.DriverId == driverId);


                if (objPlottedDriver != null)
                {

                 
                    ddlVehicle.SelectedValue = objPlottedDriver.FleetMasterId;

                      ddlZone.SelectedValue =General.GetObject<Fleet_Driver_Location>(c=>c.DriverId==driverId).DefaultIfEmpty().ZoneId;

                }
            }

        }

        



        private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {
            try
            {
                if (radPageView1.SelectedPage == Pg_Ordering && lstOrderedDrvs.Items.Count==0)
                {
                

                    var list1 = General.GetQueryable<Fleet_DriverQueueList>(a => a.DriverId != null && a.Status == true &&
                                    (a.DriverWorkStatusId == null || a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE
                                                || a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONBREAK
                                                || a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ARRIVED
                                                 || a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONROUTE
                                                ));

                    var query = (from a in list1

                                 orderby a.QueueDateTime
                                 select new
                                 {
                                     Id = a.Id,
                                     DriverNo = a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName,
                                 }).ToList();


                    lstOrderedDrvs.Items.Clear();

                    Font font = new Font("Tahoma", 9, FontStyle.Regular);
                    RadListDataItem listItem = null;
                    foreach (var item in query)
                    {

                        listItem = new RadListDataItem();
                        listItem.Font = font;
                        listItem.Text = item.DriverNo;
                        listItem.Value = item.Id;

                        lstOrderedDrvs.Items.Add(listItem);

                    }


                    if (lstOrderedDrvs.Items.Count > 0)
                        lstOrderedDrvs.Items[0].Selected = true;


                }
            }
            catch (Exception ex)
            {



            }
        }

        private void MoveDown(RadListControl listBox)
        {
            if (listBox.Items.Count < 2)
            {
                return;
            }
            if (listBox.SelectedItem == null)
            {
                return;
            }
            if (listBox.SelectedIndex == listBox.Items.Count - 1)
            {
                return;
            }
            RadListDataItem item = listBox.SelectedItem;
            int index = listBox.SelectedIndex;
            listBox.Items.Remove(item);
            listBox.Items.Insert(index + 1, item);
            listBox.SelectedItem = item;
        }


        private void MoveUp(RadListControl listBox)
        {
            if (listBox.Items.Count < 2)
            {
                return;
            }
            if (listBox.SelectedItem == null)
            {
                return;
            }
            if (listBox.SelectedIndex == 0)
            {
                return;
            }
            RadListDataItem item = listBox.SelectedItem;
            int index = listBox.SelectedIndex;
            listBox.Items.Remove(item);
            listBox.Items.Insert(index - 1, item);
            listBox.SelectedItem = item;
        }


        private void btnSaveOrder_Click(object sender, EventArgs e)
        {

            DriverQueueBO objBO = new DriverQueueBO();
            objBO.CheckDataValidation = false;
            try
            {
                int cnt = 0;
                foreach (RadListDataItem item in lstOrderedDrvs.Items)
                {
                  
                    objBO.GetByPrimaryKey(item.Value);

                    if (objBO.Current != null)
                    {
                        cnt += 10;

                        objBO.Current.QueueDateTime = DateTime.Now.AddMilliseconds(cnt);

                        objBO.Save();
                    }
                }

              //  General.RefreshWaitingDrivers();
            
                CloseForm();
             
            }
            catch (Exception ex)
            {
                if (objBO.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objBO.ShowErrors());

                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }




            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MoveUp(lstOrderedDrvs);
        }

        private void btnMoveDownZone_Click(object sender, EventArgs e)
        {
            MoveDown(lstOrderedDrvs);
        }
    }
}
