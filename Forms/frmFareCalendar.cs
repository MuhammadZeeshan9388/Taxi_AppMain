using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Telerik.WinControls.UI.Scheduler.Dialogs;
using Telerik.WinControls.UI;
using Taxi_BLL;
using Utils;
using DAL;
using System.Linq;
using Taxi_Model;
namespace Taxi_AppMain
{
    public partial class frmFareCalendar : RadSchedulerDialog, IEditAppointmentDialog
    {
        public IEvent appointment = null;
        private ISchedulerData schedulerData = null;
 
        FareCalendarBO objFareCalendar;
        public frmFareCalendar()
        {
            InitializeComponent();
            CalenderInsetday.SelectionChanged += new EventHandler(CalenderInsetday_SelectionChanged);
            CalenderInsetday.SelectedDate = DateTime.Now.ToDate();
            objFareCalendar = new FareCalendarBO();
            FillCombo();
            this.ddlHolidays.SelectedValueChanged += new EventHandler(ddlHolidays_SelectedValueChanged);
        }

        void ddlHolidays_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlHolidays.SelectedValue.ToInt() > 0)
            {
                //LeadAppointment ap = null;
               // radScheduler1.Appointments.Clear();
                CalenderInsetday.SelectedDates.Clear();
                var query=General.GetObject<FareCalendar>(c=>c.HolidayTypeId==ddlHolidays.SelectedValue.ToInt());
                if (query != null)
                {
                    int Id = query.Id;

                    var list = (from a in General.GetQueryable<FareCalender_Detail>(c => c.CalendarId == Id)
                                select new
                                {
                                    Id = a.Id,
                                    HolidayDate = a.HolidayDate
                                }).ToList();

                    foreach (var item in list)
                    {
                        CalenderInsetday.SelectedDate = item.HolidayDate.ToDate();
                    }
                    //foreach (var item in General.GetQueryable<Gen_InsetDay>(c => c.ContractId == null || c.ContractId == ddlContractnumberwithschool.SelectedValue.ToInt()))
                    //{


                    //    ap = new LeadAppointment();
                    //    ap.StatusId = item.Id;
                    //    ap.Summary = item.Reason;
                    //    ap.Start = item.InsetDays.ToDate();
                    //    ap.End = item.InsetDays.ToDate();

                    //    ap.BackgroundId = item.BackGroundColor.ToInt();


                    //    IEvent ev = (IEvent)ap;

                    //    radScheduler1.Appointments.Add(ev);

                    //}
                }                
            }
        }
        private void FillCombo()
        {
            ComboFunctions.FillHolidaysTypeCombo(ddlHolidays);
        }

        //public class CustomLeadAppointmentFactory : IAppointmentFactory
        //{


        //    public IEvent CreateNewAppointment()
        //    {
        //        return new LeadAppointment();
        //    }

        //}


        //public class LeadAppointment : Appointment
        //{
        //    public LeadAppointment()
        //        : base()
        //    {

        //    }


            //private Gen_InsetDay _objLead = new Gen_InsetDay();

            //public Gen_InsetDay ObjInsetDay
            //{

            //    get { return _objLead; }
            //    set { _objLead = value; }
            //}


            //private Gen_Department_Contract _objDeptContract = new Gen_Department_Contract();

            //public Gen_Department_Contract ObjDeptContract
            //{

            //    get { return _objDeptContract; }
            //    set { _objDeptContract = value; }
            //}
        //}
        public void SaveFareCalendar()
        {
            try
            {
                int HolyDayId = ddlHolidays.SelectedValue.ToInt();
                string Error = string.Empty;
                if (HolyDayId == 0)
                {
                    Error = "Required : Holyday Type";
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }
                var Query = General.GetObject<FareCalendar>(c => c.HolidayTypeId == HolyDayId);
                if (Query != null)
                {
                    objFareCalendar.GetByPrimaryKey(Query.Id);
                    objFareCalendar.Edit();


                }
                else
                {
                    objFareCalendar.New();
                }
                objFareCalendar.Current.HolidayTypeId = HolyDayId;
                //var dates = CalenderInsetday.SelectedDates;
                //var startDate = dtpInsetDate.Value.ToDate();
                //var endDate = dtpEndDate.Value.ToDate();
                //int days = (endDate - startDate).Days;
                //List<DateTime> range = Enumerable.Range(0, days)
                //     .Select(i => startDate.AddDays(i))
                //     .ToList();
                List<DateTime> dtHolydays = new List<DateTime>();
                foreach (var item in CalenderInsetday.SelectedDates.OrderBy(c => c.ToDateorNull()))
                {
                    if (dtHolydays.Contains(item.ToDate()) == false)
                    {
                        dtHolydays.Add(item);
                    }
                }

                List<FareCalender_Detail> ListDetail = (from a in dtHolydays
                                                        select new FareCalender_Detail
                                                     {
                                                         //Id = a.Id,
                                                         CalendarId = objFareCalendar.Current.Id,
                                                         HolidayDate = a.ToDateorNull(),
                                                     }).ToList();


                string[] skipProperties = { "FareCalendar" };
                IList<FareCalender_Detail> savedList = objFareCalendar.Current.FareCalender_Details;
                Utils.General.SyncChildCollection(ref savedList, ref ListDetail, "Id", skipProperties);
                objFareCalendar.Save();
                this.Close();
            }
            catch (Exception ex)
            {
                if (objFareCalendar.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objFareCalendar.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }

        }
        void CalenderInsetday_SelectionChanged(object sender, EventArgs e)
        {
            //  DateTime selectedate = CalenderInsetday.SelectedDate.Date;          
         //   ListselectedDays.Add(CalenderInsetday.SelectedDate.Date);
            CalenderInsetday.AllowMultipleSelect = true;
            CalenderInsetday.SelectedDates.Add(CalenderInsetday.SelectedDate);
        }
      
        #region IEditAppointmentDialog Members

        public bool EditAppointment(Telerik.WinControls.UI.IEvent appointment, Telerik.WinControls.UI.ISchedulerData schedulerData)
        {
            this.appointment = appointment;
            this.schedulerData = schedulerData;


            return true;
        }

        public void ShowRecurrenceDialog()
        {

        }

        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFareCalendar();
           // General.RefreshListWithoutSelected<frmInsetDaysCalendar>("frmInsetDaysCalendar1");

        }
    


        private void CalendarSelected_Click(object sender, EventArgs e)
        {
           // ListselectedDays.Remove(CalendarSelected.SelectedDate.Date);
        }
     
    }
}
