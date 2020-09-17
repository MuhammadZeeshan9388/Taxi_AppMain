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
using Taxi_AppMain.Classes;
using Telerik.WinControls.UI;
using System.Collections;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmAddDayWiseDriverShift : UI.SetupBase
    {
        DriverShiftsBO objDriverShifts;
        bool IsDriverShiftLoaded = false;
        public frmAddDayWiseDriverShift()
        {
            InitializeComponent();
            objDriverShifts = new DriverShiftsBO();
            this.SetProperties((INavigation)objDriverShifts);
            this.Load += new EventHandler(frmAddDayWiseDriverShift_Load);
            this.KeyDown += new KeyEventHandler(frmAddDayWiseDriverShift_KeyDown);
        }

        void frmAddDayWiseDriverShift_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        

        void frmAddDayWiseDriverShift_Load(object sender, EventArgs e)
        {
            FillCombo();
            IsDriverShiftLoaded = true;
            Days();
            PopulateData();
            DefaultTime();
        }
        public override void PopulateData()
        {
            try
            {
                var list = (from a in General.GetQueryable<Fleet_Driver>(c => c.IsActive == true)
                            select new
                            {
                                Id = a.Id,
                                DriverNo = a.DriverNo + "-" + a.DriverName,
                            }).ToList();

                foreach (var item in list)
                {
                    RadListDataItem objList=new RadListDataItem();
                    objList.Value=item.Id;
                    objList.Text=item.DriverNo;
                    lstAllDrivers.Items.Add(objList);
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void FillCombo()
        {
            ComboFunctions.FillDriverShiftsCombo(ddlShifts);
        }
        private void Days()
        {
            try
            {
                ddlDays.NullText = "Select";
                ddlDays.Items.Add("Monday");
                ddlDays.Items.Add("Tuesday");
                ddlDays.Items.Add("Wednesday");
                ddlDays.Items.Add("Thursday");
                ddlDays.Items.Add("Friday");
                ddlDays.Items.Add("Saturday");
                ddlDays.Items.Add("Sunday");
            }
            catch (Exception ex)
            { }
        }
        private void DefaultTime()
        {
            dtpStartTime.Value = DateTime.Now;
            dtpToTime.Value = DateTime.Now;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddSelectedToDayDriverWiseShift_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAllDrivers.SelectedItem!=null)
                {
                    string DriverNo = lstAllDrivers.SelectedItem.Text;
                    int Id = lstAllDrivers.SelectedValue.ToInt();
                    RadListDataItem objItem = new RadListDataItem();
                    objItem.Value = Id;
                    objItem.Text = DriverNo;
                    lstDayWiseDriver.Items.Add(objItem);
                    DeleteItemFromListBox(lstAllDrivers, lstAllDrivers.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void DeleteItemFromListBox(RadListControl lst, RadListDataItem item)
        {
            if (lst.Items.Contains(item))
                lst.Items.Remove(item);

        }
        private void btnAddSelectedToAllDriver_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDayWiseDriver.SelectedItem!=null)
                {
                    string DriverNo = lstDayWiseDriver.SelectedItem.Text;
                    int Id = lstDayWiseDriver.SelectedValue.ToInt();
                    RadListDataItem objItem = new RadListDataItem();
                    objItem.Text = DriverNo;
                    objItem.Value = Id;
                    DeleteItemFromListBox(lstDayWiseDriver, lstDayWiseDriver.SelectedItem);
                    lstAllDrivers.Items.Add(objItem);
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            Save();
        }
        public override void Save()
        {
            try
            {
                DateTime? From = dtpStartTime.Value.ToDateTimeorNull();
                DateTime? To = dtpToTime.Value.ToDateTimeorNull();
                string error = string.Empty;
                int? DriverShiftID = ddlShifts.SelectedValue.ToInt();
                int? DayNo = 0;
                if (ddlDays.SelectedItem != null)
                { 
                    DayNo = ddlDays.SelectedIndex.ToInt();
                }
                else
                {
                    error = "Required : Day";
                }
                if (lstDayWiseDriver.Items.Count == 0)
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        error = "Required : Drivers to Add Shift";
                    }
                    else
                    {
                        error += Environment.NewLine+ "Required : Drivers to Add Shift";
                    }
                }
                if (From == null)
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        error = "Required : From Time";
                    }
                    else
                    {
                        error +=Environment.NewLine+ "Required : From Time";
                    }
                }
                if (To == null)
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        error = "Required : To Time";
                    }
                    else
                    {
                        error +=Environment.NewLine+ "Required : To Time";
                    }
                }
                if (DriverShiftID == 0)
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        error = "Required : Driver Shift";
                    }
                    else
                    {
                        error +=Environment.NewLine+ "Required : Driver Shift";
                    }
                }




                //if (To != null && To < from)
                if (From.Value.Hour <= 12 && To.Value.Hour <= 12)
                {
                    if (To < From)
                    {
                        if (string.IsNullOrEmpty(error))
                        {
                            error = "Required : To Time must be greater than End Time";
                        }
                        else
                        {
                            error += Environment.NewLine + "Required : To Time must be greater than End Time";
                        }
                    }

                }
                else if (From.Value.Hour > 12 && To.Value.Hour > 12)
                {
                    if (To < From)
                    {
                        if (string.IsNullOrEmpty(error))
                        {
                            error = "Required : To Time must be greater than End Time";
                        }
                        else
                        {
                            error +=Environment.NewLine+ "Required : To Time must be greater than End Time";
                        }
                    }

                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }
                

                for (int i = 0; i < lstDayWiseDriver.Items.Count; i++)
                {
                    int DriverId = lstDayWiseDriver.Items[i].Value.ToInt();
       
                    //var query = General.GetObject<Fleet_Driver_Shift>(c => c.DriverId == DriverId && ((c.FromTime>=From.ToDateTimeorNull()) && (c.ToTime>=To.ToDateTimeorNull())));// && (c.Driver_Shift_ID==DriverShiftID)));// && (c.FromTime >=From.ToDate()) &&  (c.Driver_Shift_ID==DriverShiftID)); //|| (c.ToTime == To.ToDate())).Id;// || (c.Driver_Shift_ID == DriverShiftID));
                   // var query = General.GetObject<Fleet_Driver_Shift>(c => c.DriverId == DriverId && ((c.FromTime > From) ||(c.ToTime>From) || (c.ToTime >= To)));
                    
                    var query = General.GetObject<Fleet_Driver_Shift>(c => c.DriverId == DriverId && ((c.ToTime >= From) || (c.ToTime >= To)));
                    //if (query != null)
                    //{
                    //    if (query.FromTime > From && query.FromTime > To && query.ToTime > From && query.FromTime > To)
                    //    {
                    //        objDriverShifts.New();
                    //        objDriverShifts.Current.DriverId = DriverId;
                    //        objDriverShifts.Current.FromTime = From;
                    //        objDriverShifts.Current.ToTime = To;
                    //        objDriverShifts.Current.IsActive = true;
                    //        objDriverShifts.Current.DayNo = DayNo;
                    //        objDriverShifts.Current.Driver_Shift_ID = DriverShiftID;
                    //        objDriverShifts.Save();
                    //    }
                    //}
                    if (query ==null)
                    {
                        objDriverShifts.New();
                        objDriverShifts.Current.DriverId = DriverId;
                        objDriverShifts.Current.FromTime = From;
                        objDriverShifts.Current.DayNo = DayNo;
                        objDriverShifts.Current.ToTime = To;
                        objDriverShifts.Current.IsActive = true;
                        objDriverShifts.Current.Driver_Shift_ID = DriverShiftID;
                        objDriverShifts.Save();
                      //  objDriverShifts.Clear();
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void ddlShifts_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (IsDriverShiftLoaded == true)
            {
                int? ShiftId = ddlShifts.SelectedValue.ToInt();
                if (ShiftId == 7)
                {
                    dtpStartTime.Value = DateTime.Now.ToDateorNull();
                    dtpToTime.Value = DateTime.Now.ToDateorNull();
                    dtpStartTime.Enabled = false;
                    dtpToTime.Enabled = false;
                }
                else
                {
                    dtpStartTime.Enabled = true;
                    dtpToTime.Enabled = true;
                }
            }
        }

        private void btnAddAllToDayDriverWiseShift_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAllDrivers.Items.Count > 0)
                {
                    for (int i = 0; i < lstAllDrivers.Items.Count; i++)
                    {
                        RadListDataItem objItem = new RadListDataItem();
                        objItem.Value = lstAllDrivers.Items[i].Value.ToInt();
                        objItem.Text = lstAllDrivers.Items[i].Text.ToStr();
                        lstDayWiseDriver.Items.Add(objItem);
                      
                        //lstAllDrivers.Items.Remove(objItem);
                       // DeleteItemFromListBox(lstAllDrivers, lstAllDrivers.Items[i]);
                       //DeleteItemFromListBox(lstDayWiseDriver, lstDayWiseDriver.SelectedItem);
                    }
                    lstAllDrivers.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnAddAllToAllDriver_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstDayWiseDriver.Items.Count > 0)
                {
                    for (int i = 0; i < lstDayWiseDriver.Items.Count; i++)
                    {
                        RadListDataItem objItem = new RadListDataItem();
                        objItem.Value = lstDayWiseDriver.Items[i].Value.ToInt();
                        objItem.Text = lstDayWiseDriver.Items[i].Text.ToStr();
                        lstAllDrivers.Items.Add(objItem);
                       // DeleteItemFromListBox(lstDayWiseDriver,lstDayWiseDriver.Items[i].Value.ToInt());
                    }
                    lstDayWiseDriver.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
    }
}
