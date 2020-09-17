using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using Telerik.WinControls.UI;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Taxi_AppMain.Classes;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using System.Collections;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmAllocateDriver : Form
    {
        BookingBO ObjMaster;
        int IsOpenFrom = 0;
        long allocatedJobId = 0;
     
        public frmAllocateDriver(long ID,int openFrom)
        {
            InitializeComponent();
            ObjMaster = new BookingBO();
            ObjMaster.GetByPrimaryKey(ID);


            ComboFunctions.FillDriverNoCombo(ddl_Driver);
          //  ComboFunctions.FillDriverNoQueueCombo(ddl_Driver);
            DisplayRecord();
            this.Shown += new EventHandler(frmAllocateDriver_Shown);
            this.KeyDown += new KeyEventHandler(frmAllocateDriver_KeyDown);
            this.IsOpenFrom = openFrom;
           
          
        }

        void frmAllocateDriver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();

            }
            else if(e.KeyCode==Keys.Enter)
            {
                Save();

            }
        }

      
        void frmAllocateDriver_Shown(object sender, EventArgs e)
        {
            ddl_Driver.Focus();
            if(!string.IsNullOrEmpty(ddl_Driver.Text))
            {
                    ddl_Driver.DropDownListElement.TextBox.TextBoxItem.Select(0,2);
            }
        }

       
        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private string allocateDrvNo;

        private void Save()
        {
            try
            {
                int defaultAllocationLimit = AppVars.objPolicyConfiguration.AllocateDrvPreExistJobLimit.ToInt();
                int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
                int? oldDriverId = null;
                string oldDriverNo = string.Empty;
                long jobId = 0;
                bool cancelJob = false;
                DateTime? pickupDateAndTime=ObjMaster.Current.PickupDateTime.ToDateTimeorNull();


                //if (driverId != null && defaultAllocationLimit>0 &&
                //    General.GetQueryable<Booking>(null).Count(c => c.DriverId == driverId && c.Id!=ObjMaster.Current.Id
                //         && (c.BookingStatusId==Enums.BOOKINGSTATUS.WAITING || c.BookingStatusId==Enums.BOOKINGSTATUS.PENDING)
                //         &&
                         
                //         (
                //         (c.PickupDateTime.Value<=pickupDateAndTime
                //         && (c.PickupDateTime.Value >= pickupDateAndTime.Value.AddMinutes(-defaultAllocationLimit)) )

                //              || (c.PickupDateTime.Value >= pickupDateAndTime
                //         && (c.PickupDateTime.Value <= pickupDateAndTime.Value.AddMinutes(defaultAllocationLimit)))
                //         )

                       
                //         ) > 0)
                //{

                //    if (DialogResult.No == MessageBox.Show("This driver already have a Job Allocated at this time "+
                //         Environment.NewLine + "Do you still want to Allocate it ? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                //    {
                //        return;

                //    }

                //}


                if(driverId!=null)
                {

                    var ObjDriver = General.GetObject<Fleet_Driver>(c => c.Id == driverId);

                    if (ObjDriver != null)
                    {
                        allocateDrvNo = ObjDriver.DriverNo.ToStr().Trim();
                        if (ObjDriver.VehicleTypeId != null)
                        {
                            if (ObjMaster.Current.AttributeValues.ToStr().Trim().Length > 0)
                            {

                                string[] bookingAttrs = ObjMaster.Current.AttributeValues.ToStr().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                string drvAttributes = ObjDriver.AttributeValues.ToStr() + "," + ObjDriver.Fleet_VehicleType.AttributeValues;

                                int totalAttr = bookingAttrs.Count();
                                int matchCnt = 0;
                                string unmatchedAttrValue = string.Empty;
                                string[] drvAttrsArr = drvAttributes.ToStr().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                                foreach (var item in bookingAttrs)
                                {


                                    if (drvAttrsArr.Count(c => c.ToLower() == item.ToLower()) > 0)
                                    {
                                        matchCnt++;

                                    }
                                    else
                                    {

                                        unmatchedAttrValue += item + ",";
                                    }
                                }

                                if (matchCnt != totalAttr)
                                {

                                    if (unmatchedAttrValue.EndsWith(","))
                                    {
                                        unmatchedAttrValue = unmatchedAttrValue.Substring(0, unmatchedAttrValue.LastIndexOf(","));

                                    }

                                    MessageBox.Show(("Driver : " + ObjDriver.DriverNo + " doesn't have attributes (" + unmatchedAttrValue + ")"), "Warning");
                                    return;
                                }
                            }

                            if (AppVars.listUserRights.Count(c => c.functionId == "RESTRICT ON DESPATCH JOB TO INVALID VEHICLE DRIVER") > 0)
                            {
                                string vehAttributes = ObjMaster.Current.Fleet_VehicleType.DefaultIfEmpty().AttributeValues.ToStr().Trim();

                                if (vehAttributes.Length > 0)
                                {

                                    bool MatchedAttr = false;
                                    foreach (var item in vehAttributes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                    {
                                        if (ObjDriver.VehicleTypeId.ToInt() == item.ToInt())
                                        {
                                            MatchedAttr = true;
                                            break;

                                        }

                                    }



                                    if (MatchedAttr == false)
                                    {

                                        MessageBox.Show("This Job is for " + ObjMaster.Current.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                                                                                "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + ".");

                                        return;

                                    }

                                }
                                else
                                {

                                    if (ObjDriver.Fleet_VehicleType.NoofPassengers.ToInt() < ObjMaster.Current.Fleet_VehicleType.NoofPassengers.ToInt())
                                    {
                                        MessageBox.Show("This Job is for " + ObjMaster.Current.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                                                                                  "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + ".");


                                        return;
                                    }
                                }

                            }
                            else
                            {


                                if (ObjDriver.Fleet_VehicleType.NoofPassengers.ToInt() < ObjMaster.Current.Fleet_VehicleType.NoofPassengers.ToInt())
                                {
                                    if (DialogResult.No == MessageBox.Show("This Job is for " + ObjMaster.Current.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                                                                              "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + "." + Environment.NewLine
                                                                          + "Do you still want to Allocate this Job to that Driver " + ObjDriver.DriverNo + " ?", "Despatch", MessageBoxButtons.YesNo))
                                    {
                                        return;

                                    }



                                }

                            }

                        }                        
                    }

                    try
                    {
                        if ((driverId != null && ObjMaster.Current.DriverId == null) || (driverId != null && ObjMaster.Current.DriverId != null && driverId != ObjMaster.Current.DriverId))
                        {

                            if (IsDriverDocumentExpired(driverId.ToInt(), ObjDriver))
                                return;

                        }
                    }
                    catch
                    {

                    }
                }

            



               
                    if (ObjMaster.Current != null)
                    {
                        if (driverId != null || (ObjMaster.Current.DriverId != null && ObjMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.WAITING))
                        {


                            //if ((driverId != null && ObjMaster.Current.DriverId == null) || (driverId!=null && ObjMaster.Current.DriverId!=null && driverId!=ObjMaster.Current.DriverId))
                            //{

                            //   if  (IsDriverDocumentExpired(driverId.ToInt()))
                            //       return;

                            //}


                            if (driverId == null && ObjMaster.Current.DriverId != null)
                                oldDriverNo = ObjMaster.Current.Fleet_Driver.DriverNo.ToStr();


                            if (ObjMaster.Current.DriverId != null)
                                oldDriverId = ObjMaster.Current.DriverId;


                            ObjMaster.CheckDataValidation = false;

                            ObjMaster.Edit();

                            ObjMaster.Current.DriverId = driverId;

                            ObjMaster.Current.IsConfirmedDriver=driverId!=null ? chkConfirmed.Checked:false;





                            if (ObjMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.NOTACCEPTED)
                                ObjMaster.Current.BookingStatusId = Enums.BOOKINGSTATUS.WAITING;



                            if (driverId == null || (oldDriverId != null && oldDriverId != driverId && ObjMaster.Current.BookingStatusId != Enums.BOOKINGSTATUS.WAITING))
                            {
                                ObjMaster.Current.BookingStatusId = Enums.BOOKINGSTATUS.WAITING;
                                cancelJob = true;

                            }


                            jobId = ObjMaster.Current.Id;
                        allocatedJobId = jobId;

                            ObjMaster.CheckCustomerValidation = false;
                            ObjMaster.DisableUpdateReturnJob = true;

                            ObjMaster.Save();


                            if (ObjMaster.Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.THIRDPARTY && ObjMaster.Current.OnlineBookingId!=null)
                            {
                                General.UpdateThirdPartyJobStatus(null, jobId, "allocated");
                            }

                            this.Close();


                            if (IsOpenFrom == 1)
                            {

                                RefreshTodayBookingsDashboard();

                            }
                            else
                            {
                                RefreshTodayAndPreBookingsDashboard();
                              //  AppVars.frmMDI.RefreshTodayAnPreDashboard();

                            }

                            string Msg = string.Empty;


                            if (driverId != null)
                            {
                                if(chkConfirmed.Checked)
                                    Msg = "Job is Allocated and confirmed to Driver (" + ObjMaster.Current.Fleet_Driver.DriverNo.ToStr() + ")";
                                else
                                    Msg = "Job is Allocated  to Driver (" + ObjMaster.Current.Fleet_Driver.DriverNo.ToStr() + ")";


                            }
                            else if (driverId == null && !string.IsNullOrEmpty(oldDriverNo))
                                Msg = "Job is De-Allocated from Driver (" + oldDriverNo + ")";

                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                db.stp_BookingLog(ObjMaster.Current.Id, AppVars.LoginObj.UserName.ToStr(), Msg);


                                if (cancelJob)
                                {

                                    if (AppVars.objPolicyConfiguration.DespatchOfflineJobs.ToBool())
                                    {
                                        db.stp_DeleteDrvOfflineJob(ObjMaster.Current.Id, oldDriverId);
                                    }
                                }

                            }




                            if (cancelJob)
                            {

                                //For TCP Connection
                                if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                                {
                                        new Thread(delegate()
                                        {
                                             General.SendMessageToPDA("request pda=" + oldDriverId + "=" + jobId + "=Cancelled Pre Job>>" + jobId + "=2");
                                     }).Start();  
                                
                                }

                            }

                           
                          


                        }
                        else
                        {

                             ENUtils.ShowMessage("Required: Driver");
                        }
                       


                    }
             
            }
            catch (Exception ex)
            {


            }
        }

        private void RefreshTodayAndPreBookingsDashboard()
        {
            try
            {
                new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_TODAY_AND_PREBOOKING_DASHBOARD);

                // ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).RefreshTodayBookingData();
                // dashBoard.LoadDriverWaitingGrid();
            }
            catch (Exception ex)
            {


            }

        }

        private void RefreshTodayBookingsDashboard()
        {
            try
            {
                new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_ALLOCATEDRIVER+">>today>>"+allocatedJobId+">>"+ allocateDrvNo+">>"+ddl_Driver.SelectedValue.ToInt()+">>"+chkConfirmed.Checked.ToStr());

            
            }
            catch (Exception ex)
            {


            }

        }

        public  void DisplayRecord()
        {
            try
            {
                if (ObjMaster.Current == null) return;


               

                ddl_Driver.SelectedValue = ObjMaster.Current.DriverId;

                chkConfirmed.Checked = ObjMaster.Current.IsConfirmedDriver.ToBool();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }

        }



        private bool IsDriverDocumentExpired(int driverId,Fleet_Driver ObjDriver)
        {
            bool rtn = false;

            try
            {
               if(ObjDriver==null)
                  ObjDriver = General.GetObject<Fleet_Driver>(c => c.Id == driverId);

                if (ObjDriver != null)
                {


                    string msg = string.Empty;


                    DateTime nowDate = DateTime.Now.ToDate();
                    DateTime? pickupDateTime=ObjMaster.Current.PickupDateTime;



                    if (ObjDriver.MOTExpiryDate != null && (ObjDriver.MOTExpiryDate.ToDate() < nowDate || ObjDriver.MOTExpiryDate.ToDate()< pickupDateTime.ToDate()))
                        {
                           
                          if (ObjDriver.MOTExpiryDate.ToDate() < nowDate)
                            {

                                msg += "Driver MOT Expired :       " + string.Format("{0:dd/MM/yyyy}", ObjDriver.MOTExpiryDate) + Environment.NewLine;
                            }
                          else
                          {
                              msg += "Driver MOT is Expiring at :       " + string.Format("{0:dd/MM/yyyy}", ObjDriver.MOTExpiryDate) + Environment.NewLine;


                          }


                        }
                    if (ObjDriver.MOT2ExpiryDate != null && ObjDriver.MOT2ExpiryDate.ToDate() < nowDate)
                        {

                           // if (ObjDriver.MOT2ExpiryDate.ToDate() < nowDate)
                              msg += "Driver MOT 2 Expired :    " + string.Format("{0:dd/MM/yyyy}", ObjDriver.MOT2ExpiryDate) + Environment.NewLine;
                            //else
                            //    msg += "Driver MOT 2 is Expiring :    " + string.Format("{0:dd/MM/yyyy}", ObjDriver.MOT2ExpiryDate) + Environment.NewLine;

                        }
                     if (ObjDriver.InsuranceExpiryDate != null && (ObjDriver.InsuranceExpiryDate < nowDate || ObjDriver.InsuranceExpiryDate<pickupDateTime))
                        {
                            if (ObjDriver.InsuranceExpiryDate < nowDate)
                             msg += "Insurance Expired :          " + string.Format("{0:dd/MM/yyyy HH:mm}", ObjDriver.InsuranceExpiryDate) + Environment.NewLine;
                            else
                                msg += "Insurance is Expiring at :          " + string.Format("{0:dd/MM/yyyy HH:mm}", ObjDriver.InsuranceExpiryDate) + Environment.NewLine;


                        }

                     if (ObjDriver.PCODriverExpiryDate != null && (ObjDriver.PCODriverExpiryDate.ToDate() < nowDate || ObjDriver.PCODriverExpiryDate.ToDate()< pickupDateTime.ToDate()))
                        {
                            if (ObjDriver.PCODriverExpiryDate.ToDate() < nowDate)
                                 msg += "PCO Driver Expired :       " + string.Format("{0:dd/MM/yyyy}", ObjDriver.PCODriverExpiryDate) + Environment.NewLine;
                            else
                                msg += "PCO Driver is Expiring at :        " + string.Format("{0:dd/MM/yyyy}", ObjDriver.PCODriverExpiryDate) + Environment.NewLine;


                        }

                     if (ObjDriver.PCOVehicleExpiryDate != null &&  (ObjDriver.PCOVehicleExpiryDate.ToDate() < nowDate || ObjDriver.PCOVehicleExpiryDate.ToDate() < pickupDateTime.ToDate()))
                        {
                           if (ObjDriver.PCOVehicleExpiryDate.ToDate() < nowDate)
                               msg += "PCO Vehicle Expired :     " + string.Format("{0:dd/MM/yyyy}", ObjDriver.PCOVehicleExpiryDate) + Environment.NewLine;
                            else
                               msg += "PCO Vehicle is Expiring at :      " + string.Format("{0:dd/MM/yyyy}", ObjDriver.PCOVehicleExpiryDate) + Environment.NewLine;

                        }

                     if (ObjDriver.DrivingLicenseExpiryDate != null && (ObjDriver.DrivingLicenseExpiryDate.ToDate() < nowDate || ObjDriver.DrivingLicenseExpiryDate.ToDate() < pickupDateTime.ToDate()))
                        {
                            if (ObjDriver.DrivingLicenseExpiryDate.ToDate() < nowDate)
                              msg += "Driving License Expired :  " + string.Format("{0:dd/MM/yyyy}", ObjDriver.DrivingLicenseExpiryDate) + Environment.NewLine;
                            else
                                msg += "Driving License is Expiring at :  " + string.Format("{0:dd/MM/yyyy}", ObjDriver.DrivingLicenseExpiryDate) + Environment.NewLine;

                        }

                     if (ObjDriver.RoadTaxiExpiryDate != null && (ObjDriver.RoadTaxiExpiryDate.ToDate() < nowDate || ObjDriver.RoadTaxiExpiryDate.ToDate() < pickupDateTime.ToDate()))
                        {
                            if (ObjDriver.RoadTaxiExpiryDate.ToDate() < nowDate)
                                msg += "Road Tax Expired :            " + string.Format("{0:dd/MM/yyyy}", ObjDriver.RoadTaxiExpiryDate);
                            else
                                msg += "Road Tax is Expiring at :            " + string.Format("{0:dd/MM/yyyy}", ObjDriver.RoadTaxiExpiryDate);

                        }


                        if (!string.IsNullOrEmpty(msg))
                        {
                            msg = "Cannot Allocate Driver" + Environment.NewLine + msg;
                            rtn = true;

                            ENUtils.ShowMessage(msg);
                        }

                      
                   
                }

            }
            catch (Exception ex)
            {


            }

            return rtn;

        }


       

       
    }
}
