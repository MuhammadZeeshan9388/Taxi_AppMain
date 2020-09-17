using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;
using Taxi_BLL;
using DAL;
using System.Collections;
using System.Linq.Expressions;
using Taxi_Model;
using Taxi_AppMain;
using Utils;
using System.Windows.Forms;
using UI;
using System.Data;

namespace Taxi_AppMain
{
   public  class ComboFunctions
 
   
   {

        public static  DataTable GetCompanyList()
        {
            TaxiDataContext db = new TaxiDataContext();
            var dt=   db.GetTable<Gen_Company>().Where(c => c.IsClosed == false)
                .OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName }).ToList().ToDataTable();
            return dt;
        }
        public static void FillCompanyCombo(ComboBox dropDown)
        {



            using (TaxiDataContext db = new TaxiDataContext())
            {

                ComboFunctions.FillCombo
              (db.GetTable<Gen_Company>().Where(c => c.IsClosed == false).OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName ,args.BackgroundColor,args.TextColor }).ToList(), dropDown, "CompanyName", "Id");

            }


        }

        public static void FillCompanyCombo(RadDropDownList dropDown, int SubcompanyId)
        {
            using (TaxiDataContext db = new TaxiDataContext())
            {
                ComboFunctions.FillCombo
              (db.GetTable<Gen_Company>()
              .Where(c => c.IsClosed == false && (SubcompanyId==0 || c.SubCompanyId == SubcompanyId ))
              .OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName }).ToList(), dropDown, "CompanyName", "Id");
            }
        }


        public static void FillCompanyComboX(ComboBox dropDown)
        {

            try
            {
                SuggestComboBox s = (SuggestComboBox)dropDown;


                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var list = db.GetTable<Gen_Company>().Where(c => c.IsClosed == false).OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName, args.BackgroundColor, args.TextColor }).ToList();
                    ComboFunctions.FillCombo
                  (list, dropDown, "CompanyName", "Id");



                    // then you have to set the PropertySelector like this:
                    s.PropertySelector = collection => list.Select(p => p.CompanyName);

                    // filter rule can be customized: e.g. a StartsWith search:
                    s.FilterRule = (item, text) => item.ToLower().Contains(text.ToLower().Trim());

                    // ordering rule can also be customized: e.g. order by the surname:
                    s.SuggestListOrderRule = X => X.Split(' ')[1];

                    dropDown.AutoCompleteMode = AutoCompleteMode.None;

                }
            }
            catch
            {

            }

        }


        public static void FillCompanyComboX(ComboBox dropDown, int companyId)
        {
            


            try
            {
                SuggestComboBox s = (SuggestComboBox)dropDown;


                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var list = (from a in General.GetQueryable<Gen_Company>(c => c.IsClosed == false || c.Id == companyId)
                                select new
                                {
                                    Id = a.Id,
                                    CompanyName = a.CompanyName,
                                    a.BackgroundColor,
                                    a.TextColor
                                }).OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName, args.BackgroundColor, args.TextColor }).ToList();

                 
                    ComboFunctions.FillCombo(list, dropDown, "CompanyName", "Id");



                    // then you have to set the PropertySelector like this:
                    s.PropertySelector = collection => list.Select(p => p.CompanyName);

                    // filter rule can be customized: e.g. a StartsWith search:
                    s.FilterRule = (item, text) => item.ToLower().Contains(text.ToLower().Trim());

                    // ordering rule can also be customized: e.g. order by the surname:
                    s.SuggestListOrderRule = X => X.Split(' ')[1];

                    dropDown.AutoCompleteMode = AutoCompleteMode.None;
                }
            }
            catch
            {

            }

            
        }







        public static void FillEscortCombo(ComboBox dropDown, Expression<Func<Gen_Escort, bool>> _condition)
        {

            ComboFunctions.FillCombo<Gen_Escort>
             (General.GetQueryable<Gen_Escort>(null).OrderBy(c => c.EscortName).ToList()
                         , dropDown, "EscortName", "Id");


        }
        public static void FillCompanyDepartmentCombo(ComboBox dropDown, Expression<Func<Gen_Company_Department, bool>> _condition)
        {

            ComboFunctions.FillCombo<Gen_Company_Department>
             (General.GetQueryable<Gen_Company_Department>(_condition).OrderBy(c => c.DepartmentName).ToList()
                         , dropDown, "DepartmentName", "Id");


        }
        public static void FillDriverNoCombo(ComboBox dropDown)
        {


            var list = (from a in General.GetQueryable<Fleet_Driver>(c => c.IsActive == true)

                        orderby a.DriverNo
                        select new
                        {
                            Id = a.Id,
                            DriverName = a.DriverNo + " - " + a.DriverName

                        }).ToList();

            ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id");
        }
        public static void FillDriverNoQueueCombo(ComboBox dropDown, int? driverId, string driverNameNo)
        {
            var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                        where a.DriverId != null && a.Status == true && a.Fleet_Driver.IsActive == true
                        && (a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE || a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONBREAK)
                        // orderby a.QueueNo
                        orderby a.QueueDateTime
                        select new
                        {
                            Id = a.DriverId,
                            DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")

                        }).ToList();

            if (driverId != null && list.Count(c => c.Id == driverId) == 0)
            {

                list.Add(new { Id = driverId, DriverName = driverNameNo });

            }

            ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);

            if (driverId != null)
            {

                dropDown.SelectedValue = driverId;

            }

        }
        public static void FillCompanyCombo(ComboBox dropDown, int companyId)
        {



            var list = (from a in General.GetQueryable<Gen_Company>(c => c.IsClosed == false || c.Id == companyId)
                        select new
                        {
                            Id = a.Id,
                            CompanyName = a.CompanyName,
                            a.BackgroundColor,
                            a.TextColor

                        }).ToList();



            ComboFunctions.FillCombo(list, dropDown, "CompanyName", "Id");


        }


      



        public static void FillLocationTypeCombo(ComboBox dropDown)
        {


            using (TaxiDataContext db = new TaxiDataContext())
            {



                ComboFunctions.FillCombo<Gen_LocationType>
                 (db.Gen_LocationTypes.OrderBy(c => c.LocationType).ToList(), dropDown, "LocationType", "Id");
            }

        }

        //public static void FillHolidaysTypeCombo(RadDropDownList dropdown)
        //{
        //    ComboFunctions.FillCombo<Holiday>
        //        (General.GetQueryable<Holiday>(null).OrderBy(c => c.HolidayType).ToList(), dropdown, "HolidayType", "Id");
        //}


        public static void FillDriverCommissionCollectionHistoryCombo(RadDropDownList dropdown)
       {
           ComboFunctions.FillComboNotSorted<Fleet_DriverCommissionCollectionHistory>
               (General.GetQueryable<Fleet_DriverCommissionCollectionHistory>(null).OrderBy(c => c.Id).ToList(), dropdown, "DateRange", "Id");
       }
       public static void FillDriverWeeklyRentHistoryCombo(RadDropDownList dropdown)
       {
           ComboFunctions.FillComboNotSorted<Fleet_DriverWeeklyRentHistory>
               (General.GetQueryable<Fleet_DriverWeeklyRentHistory>(null).OrderBy(c => c.Id).ToList(), dropdown, "DateRange", "Id");
       }


       public static void FillBabySeats(RadDropDownList dropdown)
       {
           ComboFunctions.FillCombo<Gen_BabySeat>
               (General.GetQueryable<Gen_BabySeat>(null).OrderBy(c=>c.Id).ToList(), dropdown, "ZoneType", "Id");
       }

       public static void FillCommissionReasonCombo(RadDropDownList dropdown)
       {
           ComboFunctions.FillComboNotSorted< DriverCommissionPayReason>
               (General.GetQueryable<DriverCommissionPayReason>(null).OrderBy(c => c.Id).ToList(), dropdown, "CommissionReason", "Id");
       }

       public static void FillRentPayReasonCombo(RadDropDownList dropdown)
       {
           ComboFunctions.FillComboNotSorted<DriverRentPayReason>
               (General.GetQueryable<DriverRentPayReason>(null).OrderBy(c=>c.Id).ToList(), dropdown, "RentReason", "Id");
       }
       //Gen_CompanyGroups
       public static void FillCompanyGroupCombo(RadDropDownList dropdown)
       {
           ComboFunctions.FillComboNotSorted<Gen_CompanyGroup>
               (General.GetQueryable<Gen_CompanyGroup>(null).OrderBy(c => c.Id).ToList(), dropdown, "GroupName", "Id");
       }
       public static void FillComboNotSorted<T>(List<T> list, RadDropDownList dropdown, string DisplayMember, string ValueMember) where T : class
       {
           try
           {
               dropdown.DataSource = null;
               dropdown.Items.Clear();

               dropdown.DisplayMember = DisplayMember;
               dropdown.ValueMember = ValueMember;
               dropdown.DataSource = list;
               dropdown.NullText = "Select";
               dropdown.SelectedIndex = -1;

             //  dropdown.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;
               dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
               dropdown.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
           }
           catch (Exception ex)
           {
               ENUtils.ShowMessage("Drop Down = " + dropdown.Name + " " + ex.Message);

           }
       }

       public static void FillZoneTypes(RadDropDownList dropdown)
       {
           ComboFunctions.FillCombo<Gen_ZoneType>
               (General.GetQueryable<Gen_ZoneType>(null).OrderBy(c => c.ZoneType).ToList(), dropdown, "ZoneType", "Id");
       }

       public static void FillPlotWiseFare(RadDropDownList dropdown)
       {
           var list = (from a in General.GetQueryable<Gen_Zone>(c=>c.MaxLatitude!=null)
                       orderby a.OrderNo
                       select new
                       {
                           Id = a.Id,
                           ZoneName = a.OrderNo + "." +a.ZoneName
                           //ZoneName=a.ShortName+" - " +a.Gen_ZoneType.ZoneType
                       }).ToList();
           ComboFunctions.FillCombo(list, dropdown, "ZoneName", "Id",false);

       }

       public static void FillDriverNoComboSorted(RadDropDownList dropDown)
       {
          

                var list = (from a in AppVars.BLData.GetQueryable<Fleet_Driver>(c => c.IsActive == true).AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())


                            select new
                            {
                                Id = a.Id,
                                DriverName = a.DriverNo + " - " + a.DriverName

                            }).ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id");


       }



       public static void FillControllerCombo(ComboBox dropdown)
       {

           try
           {
               dropdown.DataSource = General.GetQueryable<UM_User>(c=>c.SecurityGroupId==2).OrderBy(c => c.UserName).ToList();
              // dropdown.Items.Clear();

               dropdown.DisplayMember = "UserName";
               dropdown.ValueMember = "Id";
              // dropdown.DataSource = list;
               // dropdown.NullText = "Select";
               dropdown.SelectedIndex = -1;

               //   dropdown.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;
               dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
               dropdown.DropDownStyle = ComboBoxStyle.DropDownList;
           }
           catch (Exception ex)
           {
               ENUtils.ShowMessage("Drop Down = " + dropdown.Name + " " + ex.Message);

           }

          
       }


       public static void FillUsers(RadDropDownList dropdown)
       {
           ComboFunctions.FillCombo<UM_User>
               (General.GetQueryable<UM_User>(null).OrderBy(c => c.UserName).ToList(), dropdown, "UserName", "Id", false);
       }
       public static void FillThirdPartyCompanyCombo(RadDropDownList dropdown)
       {
           ComboFunctions.FillCombo<Gen_Party>
               (General.GetQueryable<Gen_Party>(c=>c.IsSysGen==null || c.IsSysGen==false).OrderBy(c => c.CompanyName).ToList(), dropdown, "CompanyName", "Id", false);
       }
       public static void FillSMSTemplate(RadDropDownList dropdown)
       {
           ComboFunctions.FillCombo<SMSBunch>
               (General.GetQueryable<SMSBunch>(null).OrderBy(c => c.MessageTemplate).ToList(), dropdown, "MessageTemplate", "Id", false);

       }

       public static void FillSMSTagCombo2(RadDropDownList dropDown, Expression<Func<SMSTag, bool>> expression)
       {

           ComboFunctions.FillCombo<SMSTag>
               //     (General.GetQueryable<ta.GetAll<SMSTag>().ToList(), dropDown, "TagDisplayValue", "TagMemberValue");

            (AppVars.BLData.GetAll<SMSTag>(expression).ToList(), dropDown, "TagDisplayValue", "TagMemberValue");


       }

       //public static void FillCombo<T>(List<T> list, RadComboBox dropdown, string DisplayMember, string ValueMember) where T : class
       //{
       //    try
       //    {
       //        dropdown.DataSource = null;
       //        dropdown.Items.Clear();

       //        dropdown.DisplayMember = DisplayMember;
       //        dropdown.ValueMember = ValueMember;
       //        dropdown.DataSource = list;
       //        dropdown.NullText = "Select";
       //        dropdown.SelectedIndex = -1;

       //    //    dropdown.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;
       //        dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
       //        dropdown.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
       //    }
       //    catch (Exception ex)
       //    {
       //        ENUtils.ShowMessage("Drop Down = " + dropdown.Name + " " + ex.Message);

       //    }
       //}

       public static void FillSubCompanyCombo(RadComboBox dropDown)
       {

           ComboFunctions.FillCombo<Gen_SubCompany>
            (General.GetQueryable<Gen_SubCompany>(null).OrderBy(c => c.CompanyName).ToList(), dropDown, "CompanyName", "Id");


       }

       public static void FillSubCompanyNameCombo(RadComboBox dropDown)
       {

           ComboFunctions.FillCombo
            (General.GetQueryable<Gen_SubCompany>(null).Select(args=>new{args.Id,args.CompanyName}).OrderBy(c => c.CompanyName).ToList(), dropDown, "CompanyName", "Id");


       }




       public static void FillSubCompanyCombo(RadDropDownList dropDown)
       {
          
           ComboFunctions.FillCombo<Gen_SubCompany>
            (General.GetQueryable<Gen_SubCompany>(null).OrderBy(c=>c.CompanyName).ToList(), dropDown, "CompanyName", "Id", false);


       }


        public static void FillSubCompanyCombo(RadDropDownList dropDown,bool usePartialDetails)
        {

            ComboFunctions.FillCombo
             (General.GetQueryable<Gen_SubCompany>(null).Select(args=>new { args.Id, args.CompanyName }).OrderBy(c => c.CompanyName).ToList(), dropDown, "CompanyName", "Id", false);


        }

        public static void FillCommisionDriverDates(RadDropDownList dropDown, Expression<Func<Fleet_DriverCommision, bool>> _condition)
       {
           var list = (from a in AppVars.BLData.GetQueryable<Fleet_DriverCommision>(_condition).AsEnumerable().OrderByDescending(c => c.TransDate)

                       select new
                       {
                           Id = a.Id,
                           Dates = string.Format("{0:dd-MMM-yyyy}", a.FromDate) + " To " + string.Format("{0:dd-MMM-yyyy}", a.ToDate)
                       }).ToList();


           ComboFunctions.FillCombo(list, dropDown, "Dates", "Id");
       }

       public static void FillFuelTypeCombo(RadDropDownList dropDown)
       {
           var list = AppVars.BLData.GetAll<Fleet_FuelType>().ToList();

           ComboFunctions.FillCombo<Fleet_FuelType>
            (list, dropDown, "FuelType", "Id", false);


       }
       public static void FillDriverShiftsCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<Driver_Shift>
            (AppVars.BLData.GetAll<Driver_Shift>().OrderBy(c => c.ShiftName).ToList(), dropDown, "ShiftName", "Id");


       }

      

       public static void FillCombo<T>(List<T> list, RadComboBox dropdown, string DisplayMember, string ValueMember) where T : class
       {
           try
           {
               dropdown.DataSource = null;
               dropdown.Items.Clear();

             //  dropdown.Sorted = Telerik.WinControls.Enumerations.SortStyle.Ascending;
               dropdown.SelectedIndex = -1;
               dropdown.DisplayMember = DisplayMember;
               dropdown.ValueMember = ValueMember;
               dropdown.DataSource = list;
               dropdown.NullText = "Select";
               
               //      dropdown.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;
               dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
               dropdown.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;

           }
           catch (Exception ex)
           {
               ENUtils.ShowMessage("Drop Down = " + dropdown.Name + " " + ex.Message); 

           }
       }





       public static void FillCombo<T>(List<T> list, RadDropDownList dropdown,string DisplayMember,string ValueMember) where T:class
       {
           try
           {
               dropdown.DataSource = null;
               dropdown.Items.Clear();

           dropdown.DisplayMember = DisplayMember;
           dropdown.ValueMember = ValueMember;
           dropdown.DataSource = list;
           dropdown.NullText = "Select";
           dropdown.SelectedIndex = -1;

           dropdown.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;
           dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
           dropdown.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
             }
           catch (Exception ex)
           {
               ENUtils.ShowMessage("Drop Down = " + dropdown.Name + " " + ex.Message); 

           }
       }


       public static void FillCombo<T>(List<T> list, ComboBox dropdown, string DisplayMember, string ValueMember) where T : class
       {
           try
           {
               dropdown.DataSource = null;
               dropdown.Items.Clear();

               dropdown.DisplayMember = DisplayMember;
               dropdown.ValueMember = ValueMember;
               dropdown.DataSource = list;
              // dropdown.NullText = "Select";
               dropdown.SelectedIndex = -1;
              ///  dropdown.FormattingEnabled = true;
             //   dropdown.MaxDropDownItems = 30;

                //   dropdown.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;
             //   dropdown.AutoCompleteSource = AutoCompleteSource.CustomSource;
               dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                dropdown.AutoCompleteSource = AutoCompleteSource.ListItems;
               dropdown.DropDownStyle = ComboBoxStyle.DropDown;
           }
           catch (Exception ex)
           {
               ENUtils.ShowMessage("Drop Down = " + dropdown.Name + " " + ex.Message);

           }
       }


       public static void FillCombo(IList list, ComboBox dropdown, string DisplayMember, string ValueMember)
       {
           try
           {
           //    dropdown.DataSource = null;
           //    dropdown.Items.Clear();

               dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
               dropdown.DropDownStyle = ComboBoxStyle.DropDown;

               dropdown.DisplayMember = DisplayMember;
               dropdown.ValueMember = ValueMember;
               dropdown.DataSource = list;
           //    dropdown.NullText = "Select";
               dropdown.SelectedIndex = -1;

            //   dropdown.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;


           }
           catch (Exception ex)
           {
               ENUtils.ShowMessage("Drop Down = " + dropdown.Name + " " + ex.Message);

           }

       }



       public static void FillCombo<T>(List<T> list, RadDropDownList dropdown, string DisplayMember, string ValueMember,bool sorted) where T : class
       {
           try
           {
               dropdown.DataSource = null;
               dropdown.Items.Clear();

               dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
               dropdown.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;

               dropdown.DisplayMember = DisplayMember;
               dropdown.ValueMember = ValueMember;
               dropdown.DataSource = list;
          
               dropdown.NullText = "Select";
               dropdown.SelectedIndex = -1;

              

               if(sorted)
                  dropdown.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;
  
           
           }
           catch (Exception ex)
           {
               ENUtils.ShowMessage("Drop Down = " + dropdown.Name + " " + ex.Message);

           }
       }


       public static void FillCombo<T>(List<T> list, ComboBox dropdown, string DisplayMember, string ValueMember, bool sorted) where T : class
       {
           try
           {
               dropdown.DataSource = null;
               dropdown.Items.Clear();

               //dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
               //dropdown.DropDownStyle = ComboBoxStyle.DropDown;

               dropdown.DisplayMember = DisplayMember;
               dropdown.ValueMember = ValueMember;
               dropdown.DataSource = list;

                //      dropdown.NullText = "Select";

                dropdown.SelectedIndex = -1;


                dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                dropdown.DropDownStyle = ComboBoxStyle.DropDown;

                //   if (sorted)
                //         dropdown.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;


            }
           catch (Exception ex)
           {
               ENUtils.ShowMessage("Drop Down = " + dropdown.Name + " " + ex.Message);

           }
       }



       public static void FillCombo(IList list, RadDropDownList dropdown, string DisplayMember, string ValueMember) 
       {
           try
           {
               dropdown.DataSource = null;
               dropdown.Items.Clear();

               dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
               dropdown.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;

           dropdown.DisplayMember = DisplayMember;
           dropdown.ValueMember = ValueMember;
           dropdown.DataSource = list;
           dropdown.NullText = "Select";
           dropdown.SelectedIndex = -1;

           dropdown.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;

         
              }
           catch (Exception ex)
           {
               ENUtils.ShowMessage("Drop Down = " + dropdown.Name + " " + ex.Message); 

           }

       }


       public static void FillCombo(IList list, RadComboBox dropdown, string DisplayMember, string ValueMember)
       {
           try
           {
               dropdown.DataSource = null;
               dropdown.Items.Clear();

          //     dropdown.Sorted = Telerik.WinControls.Enumerations.SortStyle.Ascending;

               dropdown.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
               dropdown.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;

               dropdown.DisplayMember = DisplayMember;
               dropdown.ValueMember = ValueMember;
               dropdown.DataSource = list;
               dropdown.NullText = "Select";
               dropdown.SelectedIndex = -1;
           //    dropdown.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;

            
           }
           catch (Exception ex)
           {
               ENUtils.ShowMessage("Drop Down = " + dropdown.Name + " " + ex.Message);

           }

       }




       public static void FillSecGroupsCombo(RadDropDownList dropDown)
       {

               ComboFunctions.FillCombo<UM_SecurityGroup>
                (General.GetGeneralList<UM_SecurityGroup>(null), dropDown, "GroupName", "Id");


       }

       public static void FillSecGroupsComboExcRoot(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<UM_SecurityGroup>
            (General.GetGeneralList<UM_SecurityGroup>(c => c.GroupName != "Root User"), dropDown, "GroupName", "Id");


       }


       public static void FillLocationTypeCombo(RadDropDownList dropDown, Expression<Func<Gen_LocationType, bool>> _condition)
       {

           ComboFunctions.FillCombo<Gen_LocationType>
            (AppVars.BLData.GetAll<Gen_LocationType>(_condition).OrderBy(c => c.LocationType).ToList(), dropDown, "LocationType", "Id");
       

       }


       public static void FillLocationTypeCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<Gen_LocationType>
            (AppVars.BLData.GetAll<Gen_LocationType>().OrderBy(c=>c.LocationType).ToList(), dropDown, "LocationType", "Id");
       


       }

       public static void FillDriverTypeCombo(RadDropDownList dropDown)
       {
           dropDown.Items.Clear();

          
          RadListDataItem  d = new RadListDataItem();
           d.Text = "On Rent";
           d.Value = 1;
           dropDown.Items.Add(d);

           d = new RadListDataItem();
           d.Text = "On Commission";
           d.Value = 2;
           dropDown.Items.Add(d);
          
        


         //  ComboFunctions.FillCombo<Fleet_DriverType>
         //   (AppVars.BLData.GetAll<Fleet_DriverType>().ToList(), dropDown, "DriverType", "Id");


       }

       public static void FillLocationTypeCombo(RadComboBox dropDown)
       {
        


                ComboFunctions.FillCombo<Gen_LocationType>
                 (AppVars.BLData.GetAll<Gen_LocationType>().OrderBy(c => c.LocationType).ToList(), dropDown, "LocationType", "Id");
        
       }

   


       public static void FillAccountTypeCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<AccountType>
            (AppVars.BLData.GetAll<AccountType>().ToList(), dropDown, "AccountTypeName", "Id");


       }


       public static void FillSMSTagCombo(RadDropDownList dropDown,Expression<Func<SMSTag,bool>> expression)
       {

           ComboFunctions.FillCombo<SMSTag>
                     //     (General.GetQueryable<ta.GetAll<SMSTag>().ToList(), dropDown, "TagDisplayValue", "TagMemberValue");

            (AppVars.BLData.GetAll<SMSTag>(expression).ToList(), dropDown, "TagDisplayValue", "TagMemberValue");


       }



       public static void FillUsersCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<UM_User>
            (AppVars.BLData.GetAll<UM_User>().ToList(), dropDown, "UserName", "Id");


       }


       public static void FillAvailablePaymentGatewayCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo
            (General.GetQueryable<Gen_SysPolicy_PaymentDetail>(null).Select(args => new { Id = args.PaymentGateway.Id ,Name=args.PaymentGateway.Name}).ToList(), dropDown, "Name", "Id");


       }

        public static void FillDispatchAvailablePaymentGatewayCombo(RadDropDownList dropDown)
        {

            var list = (General.GetQueryable<Gen_SysPolicy_PaymentDetail>(null)
             .Select(args => new { Id = args.PaymentGateway.Id, Name = args.PaymentGateway.Name , args.EnableMobileIntegration })).ToList();



            if (list.Count(c => c.EnableMobileIntegration == null || c.EnableMobileIntegration == false) > 0)
                list = list.Where(c => c.EnableMobileIntegration == null || c.EnableMobileIntegration == false).ToList();
           

            //

            ComboFunctions.FillCombo(list, dropDown, "Name", "Id");


        }

        public static void FillPaymentGatewayCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<PaymentGateway>
            (AppVars.BLData.GetAll<PaymentGateway>().ToList(), dropDown, "Name", "Id");


       }


       public static void FillLocationsCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<Gen_Location>
            (AppVars.BLData.GetAll<Gen_Location>().OrderBy(c => c.LocationName).ToList(), dropDown, "LocationName", "Id");
       }

       public static void FillLocationsCombo(RadComboBox dropDown)
       {

           ComboFunctions.FillCombo<Gen_Location>
            (AppVars.BLData.GetAll<Gen_Location>().OrderBy(c => c.LocationName).ToList(), dropDown, "LocationName", "Id");
       }


       public static void FillLocationsCombo(RadDropDownList dropDown,Expression<Func<Gen_Location,bool>> _condition)
       {
          

           var list=AppVars.BLData.GetAll<Gen_Location>(_condition).OrderBy(c => c.LocationName)
            
                        .Select(args=>new
                                    {
                                        Id=args.Id,
                                        LocationName=args.PostCode!=string.Empty?args.LocationName + ", "+args.PostCode :args.LocationName

                                    })
            
                        .ToList();

           ComboFunctions.FillCombo(list, dropDown, "LocationName", "Id");
       }


       public static void FillPostCodeLocationsCombo(RadDropDownList dropDown, Expression<Func<Gen_Location, bool>> _condition)
       {
           var list = AppVars.BLData.GetAll<Gen_Location>(_condition).OrderBy(c => c.LocationName)

                        .Select(args => new
                        {
                            Id = args.Id,
                            LocationName = args.PostCode

                        })

                        .ToList();

           ComboFunctions.FillCombo(list, dropDown, "LocationName", "Id");
       }



       public static void FillLocationsCombo(RadComboBox dropDown, Expression<Func<Gen_Location, bool>> _condition)
       {


           var list = General.GetQueryable<Gen_Location>(_condition).OrderBy(c => c.LocationName)

                       .Select(args => new
                       {
                           Id = args.Id,
                           LocationName = args.PostCode != string.Empty ? args.LocationName + ", " + args.PostCode : args.LocationName,
                           PostCode=args.PostCode
                       })

                       .ToList();


           if (AppVars.zonesList!=null && AppVars.zonesList.Count() > 0 && AppVars.objPolicyConfiguration.DefaultClientId!="apexradio")
           {

               var zones = (from a in AppVars.zonesList
                            from b in list
                            where b.PostCode.Split(' ')[0].Equals(a)
                          //  orderby a
                            select new
                                {
                                    Id = b.Id,
                                    LocationName = b.LocationName,
                                    PostCode = b.PostCode

                                })
                              //  .OrderBy(item => item.PostCode, new NaturalSortComparer<string>())
                                .ToList();



               var finalList = zones.Union(list).ToList();





               ComboFunctions.FillCombo(finalList, dropDown, "LocationName", "Id");
           }
           else
           {

               ComboFunctions.FillCombo(list, dropDown, "LocationName", "Id");


           }
       }



       public static void FillZonesCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<Gen_Zone>
            (AppVars.BLData.GetAll<Gen_Zone>(c=>c.MaxLatitude!=null).OrderBy(c => c.ZoneName).ToList(), dropDown, "ZoneName", "Id");
       }

       public static void FillVehicleColorsCombo(RadDropDownList dropDown)
       {


         

               using (TaxiDataContext db = new TaxiDataContext())
               {
                   var colors = db.Fleet_VehicleColors.OrderBy(c => c.VehicleColor).ToList().ToDataTable();


                 //  Program.dtCombos.Tables.Add(colors);

                   ComboFunctions.FillCombo<Fleet_VehicleColor>
                    (db.Fleet_VehicleColors.OrderBy(c => c.VehicleColor).ToList(), dropDown, "VehicleColor", "Id");
               }

           
       }



       public static void FillZonesPlottedCombo(RadDropDownList dropDown)
       {
           var list = (from a in General.GetQueryable<Gen_Zone>(c=>c.ZoneName!="SIN BIN")
                       orderby a.OrderNo
                       select new
                           {
                               Id = a.Id,
                               ZoneName =a.OrderNo+". " + a.ZoneName + " (" + a.ShortName+")"

                           }).ToList();

           ComboFunctions.FillCombo(list, dropDown, "ZoneName", "Id");
       }

      

       public static void FillVehicleCombo(RadDropDownList dropDown)
       {

           var list = (from a in AppVars.BLData.GetQueryable<Fleet_Master>(null)
                       select new
                       {
                           Id = a.Id,
                           Vehicle = a.VehicleNo 

                       }).ToList();

           ComboFunctions.FillCombo(list, dropDown, "Vehicle", "Id");


       }


       public static void FillBookingStatusCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<BookingStatus>
            (AppVars.BLData.GetAll<BookingStatus>().OrderBy(c => c.StatusName).ToList(), dropDown, "StatusName", "Id");


       }



       public static void FillCreditCardCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<Gen_CreditCardType>
            (AppVars.BLData.GetAll<Gen_CreditCardType>(null).ToList(), dropDown, "Name", "Id");


       }


       public static void FillCompanyCombo(RadDropDownList dropDown,int companyId,string companyName)
       {

           var list = (from a in AppVars.BLData.GetAll<Gen_Company>(c => c.IsClosed == false)
                      select new
                          {
                              Id=a.Id,
                              CompanyName=a.CompanyName

                          }).ToList();

            if(companyId!=0)
            {
                list.Add(new { Id = companyId, CompanyName = companyName });
              //  list.Add(new {Id=companyId, CompanyName=companyName});
               
                    //list.Add(new { Id = driverId, DriverName = driverNameNo });

               
            }

           ComboFunctions.FillCombo(list, dropDown, "CompanyName", "Id");


       }


       public static void FillCompanyCombo(RadComboBox dropDown, int companyId)
       {

          

           var list = (from a in AppVars.BLData.GetAll<Gen_Company>(c => c.IsClosed == false || c.Id==companyId)
                       select new
                       {
                           Id = a.Id,
                           CompanyName = a.CompanyName

                       }).ToList();

         

           ComboFunctions.FillCombo(list, dropDown, "CompanyName", "Id");


       }


       public static void FillCompanyCombo(ComboBox dropDown, List<Gen_Company> list)
       {

           ComboFunctions.FillCombo<Gen_Company>(list, dropDown, "CompanyName", "Id");


       }

       public static void FillCompanyCombo(RadDropDownList dropDown,List<Gen_Company> list)
       {

           ComboFunctions.FillCombo<Gen_Company>(list, dropDown, "CompanyName", "Id");


       }

       public static void FillCompanyCombo(RadComboBox dropDown, List<Gen_Company> list)
       {

           ComboFunctions.FillCombo<Gen_Company>(list, dropDown, "CompanyName", "Id");
           dropDown.SelectedIndex = -1;


       }


       public static void FillCompanyForInvoiceCombo(RadDropDownList dropDown)
       {

           using (TaxiDataContext db = new TaxiDataContext())
           {

               ComboFunctions.FillCombo
             (db.GetTable<Gen_Company>().Where(c => c.IsClosed == false && 
                 (c.Gen_Company_PaymentTypes.Count==0 || c.Gen_Company_PaymentTypes.Any(a=>a.PaymentTypeId==Enums.PAYMENT_TYPES.BANK_ACCOUNT))).OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName }).ToList(), dropDown, "CompanyName", "Id");

           }


       }


       public static void FillCompanyForInvoiceCombo(RadDropDownList dropDown,int companyId)
       {

           using (TaxiDataContext db = new TaxiDataContext())
           {

               ComboFunctions.FillCombo
             (db.GetTable<Gen_Company>().Where(c => (c.IsClosed == false || c.Id==companyId) &&
                 (c.Gen_Company_PaymentTypes.Count == 0 || c.Gen_Company_PaymentTypes.Any(a => a.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT))).OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName }).ToList(), dropDown, "CompanyName", "Id");

           }


       }

       public static void FillCompanyCombo(RadDropDownList dropDown)
       {

       
          
           using (TaxiDataContext db=new TaxiDataContext())
           {

                ComboFunctions.FillCombo
              (db.GetTable<Gen_Company>().Where(c=>c.IsClosed==false).OrderBy(c => c.CompanyName ).Select(args=>new{args.Id,args.CompanyName}).ToList(), dropDown, "CompanyName", "Id");

           }
        
       
       }

       public static void FillCompany_AllCombo(RadDropDownList dropDown)
       {



           using (TaxiDataContext db = new TaxiDataContext())
           {

               ComboFunctions.FillCombo
             (db.GetTable<Gen_Company>().Where(c => c.IsClosed == false).OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName }).ToList(), dropDown, "CompanyName", "Id");

           }


       }

       public static void FillCompanyCombo(RadComboBox dropDown)
       {



           using (TaxiDataContext db = new TaxiDataContext())
           {

               ComboFunctions.FillCombo
             (db.GetTable<Gen_Company>().Where(c => c.IsClosed == false).OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName }).ToList(), dropDown, "CompanyName", "Id");

           }

           dropDown.SelectedIndex = -1;


       }

       public static void FillSubCompanyNameCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo
            (General.GetQueryable<Gen_SubCompany>(null).Select(args => new { args.Id, args.CompanyName }).OrderBy(c => c.CompanyName).ToList(), dropDown, "CompanyName", "Id", false);


       }


       public static void FillCompanyCombo(RadDropDownList dropDown,Expression<Func<Gen_Company,bool>> _condition)
       {

           ComboFunctions.FillCombo<Gen_Company>
            (AppVars.BLData.GetAll<Gen_Company>(_condition).OrderBy(c => c.CompanyName).ToList(), dropDown, "CompanyName", "Id");


       }


      

       public static void FillVehicleTypeCombo(RadDropDownList dropDown)
       {
           try
           {
               using (TaxiDataContext db = new TaxiDataContext())
               {

                   // var list=AppVars.BLData.GetAll<Fleet_VehicleType>().AsEnumerable().OrderBy(c => c.OrderNo.ToInt()).ToList();
                   var list = db.Fleet_VehicleTypes.Select(args => new { args.Id, args.VehicleType, args.OrderNo }).OrderBy(c => c.OrderNo).ToList();

                   ComboFunctions.FillCombo(list, dropDown, "VehicleType", "Id", false);

               }
           }
           catch
           {


           }

       }


       public static void FillSinBinDriversCombo(RadDropDownList dropDown)
       {
           //   DateTime now=DateTime.Now.ToDate(); 
           //(from a in General.GetQueryable<Fleet_Driver_Location>(c => c.ZoneId!=null && c.Gen_Zone.ZoneName=="SIN BIN")
           var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                       where a.DriverId != null && a.Status == true && a.Fleet_Driver.HasPDA == true
                       && (a.DriverWorkStatusId==Enums.Driver_WORKINGSTATUS.AVAILABLE || a.DriverWorkStatusId==Enums.Driver_WORKINGSTATUS.ONBREAK || a.DriverWorkStatusId==Enums.Driver_WORKINGSTATUS.SINBIN) 
                       // orderby a.QueueNo
                       orderby a.QueueDateTime

                       select new
                       {
                           Id = a.DriverId,
                           DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")
                           //DriverName = a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName

                       }).ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id");


       }

       public static void FillSinBinDriverCombo(RadDropDownList dropDown)
       {
           //   DateTime now=DateTime.Now.ToDate();
           var list = (from a in General.GetQueryable<Fleet_Driver_Location>(c => c.ZoneId!=null && c.Gen_Zone.ZoneName=="SIN BIN")

                       orderby a.Fleet_Driver.DriverNo
                       select new
                       {
                           Id = a.DriverId,
                           DriverName = a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName

                       }).ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id");


       }


       public static void FillAttributeCombo(RadDropDownList dropdown)
       {
           var list = (from a in General.GetQueryable<Gen_Attribute>(null)
                       orderby a.Name
                       select new
                       {
                           ShortName = a.ShortName,
                           Name = a.ShortName + " [" + a.Name + "]"
                       }).ToList();
           ComboFunctions.FillCombo(list, dropdown, "Name", "ShortName", false);

       }

       public static void FillDriverNoCombo(RadDropDownList dropDown)
       {


           var list = (from a in AppVars.BLData.GetQueryable<Fleet_Driver>(c => c.IsActive == true)

                       orderby a.DriverNo
                       select new
                           {
                               Id = a.Id,
                               DriverName = a.DriverNo + " - " + a.DriverName

                           }).ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id");
       }



       public static void FillFreezePlottedDriverNoCombo(RadDropDownList dropDown)
       {
           var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                       where a.DriverId != null && a.Status == true && (a.Fleet_Driver.SubcompanyId == AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId == 0) && (a.ZoneId == null || a.ZoneId != null && a.Gen_Zone.ZoneName != "SIN BIN")
                       && (a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.NOTAVAILABLE || a.DriverWorkStatusId==Enums.Driver_WORKINGSTATUS.SOONTOCLEAR)
                       orderby a.QueueDateTime
                       select new
                       {
                           Id = a.DriverId,
                           DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")

                       }).Distinct().ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);


       }


       public static void FillDriverNoCombo(RadDropDownList dropDown,Expression<Func<Fleet_Driver,bool>> _condition)
       {
           //   DateTime now=DateTime.Now.ToDate();
           var list = (from a in AppVars.BLData.GetQueryable<Fleet_Driver>(_condition)
                    //   where a.IsActive==true
                       orderby a.DriverNo
                       select new
                       {
                           Id = a.Id,
                           DriverName = a.DriverNo + " - " + a.DriverName

                       }).ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id");


       }


       public static void FillDriverNoCombo(RadComboBox dropDown, Expression<Func<Fleet_Driver, bool>> _condition)
       {
           //   DateTime now=DateTime.Now.ToDate();
           var list = (from a in AppVars.BLData.GetQueryable<Fleet_Driver>(_condition)
                       //   where a.IsActive==true
                       orderby a.DriverNo
                       select new
                       {
                           Id = a.Id,
                           DriverName = a.DriverNo + " - " + a.DriverName

                       }).ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id");


       }


       public static void FillDriverNoCombo(RadListControl listBox)
       {
           var list = (from a in AppVars.BLData.GetAll<Fleet_Driver>(c => c.IsActive == true)
                       orderby a.DriverNo
                       select new
                       {
                           Id = a.Id,
                           DriverName = a.DriverNo + " - " + a.DriverName

                       }).ToList();

           listBox.DataSource = list;
           listBox.DisplayMember = "DriverName";
           listBox.ValueMember = "Id";
           listBox.SortStyle= Telerik.WinControls.Enumerations.SortStyle.Ascending;

          // ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id");


       }

       public static void FillDriverNoQueueCombo(ComboBox dropDown)
       {

           if (AppVars.objPolicyConfiguration.DisableJobOfferToOnBreakDrv.ToBool())
           {
               var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                           where a.DriverId != null && a.Status == true && a.Fleet_Driver.IsActive == true && (a.Fleet_Driver.SubcompanyId == AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId == 0) && (a.ZoneId == null || a.ZoneId != null && a.Gen_Zone.ZoneName != "SIN BIN")
                           && (a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE)

                           orderby a.QueueDateTime
                           select new
                           {
                               Id = a.DriverId,
                               DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")

                           }).Distinct().ToList();

               ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);

           }
           else
           {

               var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                           where a.DriverId != null && a.Status == true && a.Fleet_Driver.IsActive == true && (a.Fleet_Driver.SubcompanyId == AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId == 0) && (a.ZoneId == null || a.ZoneId != null && a.Gen_Zone.ZoneName != "SIN BIN")
                           && (a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE || a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONBREAK)

                           orderby a.QueueDateTime
                           select new
                           {
                               Id = a.DriverId,
                               DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")

                           }).Distinct().ToList();

               ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);
           }
       }

       public static void FillDriverNoQueueCombo(RadDropDownList dropDown)
       {

           if (AppVars.objPolicyConfiguration.DisableJobOfferToOnBreakDrv.ToBool())
           {
               var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                           where a.DriverId != null && a.Status == true && a.Fleet_Driver.IsActive == true && (a.Fleet_Driver.SubcompanyId == AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId == 0) && (a.ZoneId == null || a.ZoneId != null && a.Gen_Zone.ZoneName != "SIN BIN")
                           && (a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE)

                           orderby a.QueueDateTime
                           select new
                           {
                               Id = a.DriverId,
                               DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")

                           }).Distinct().ToList();

               ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);

           }
           else
           {

               var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                           where a.DriverId != null && a.Status == true && a.Fleet_Driver.IsActive == true && (a.Fleet_Driver.SubcompanyId == AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId == 0) && (a.ZoneId == null || a.ZoneId != null && a.Gen_Zone.ZoneName != "SIN BIN")
                           && (a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE || a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONBREAK)

                           orderby a.QueueDateTime
                           select new
                           {
                               Id = a.DriverId,
                               DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")

                           }).Distinct().ToList();

               ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);
           }
       }





        public static void FillDriverNoQueueDespatchCombo(RadDropDownList dropDown)
        {

            if (AppVars.objPolicyConfiguration.DisableJobOfferToOnBreakDrv.ToBool())
            {
                var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                            where a.DriverId != null && a.Status == true && a.Fleet_Driver.IsActive == true && (a.Fleet_Driver.SubcompanyId == AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId == 0) && (a.ZoneId == null || a.ZoneId != null && a.Gen_Zone.ZoneName != "SIN BIN")
                            && (a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE)

                            orderby a.QueueDateTime
                            select new
                            {
                                Id = a.DriverId,
                                DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")
                                ,a.IsManualLogin
                            }).Distinct().ToList();

                ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);

            }
            else
            {

                var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                            where a.DriverId != null && a.Status == true && a.Fleet_Driver.IsActive == true && (a.Fleet_Driver.SubcompanyId == AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId == 0) && (a.ZoneId == null || a.ZoneId != null && a.Gen_Zone.ZoneName != "SIN BIN")
                            && (a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE || a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONBREAK)

                            orderby a.QueueDateTime
                            select new
                            {
                                Id = a.DriverId,
                                DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")
                                , a.IsManualLogin
                            }).Distinct().ToList();

                ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);
            }
        }

        public static void FillDriverNoFOJQueueCombo(RadDropDownList dropDown)
       {
           var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                       where a.DriverId != null && a.Status == true && a.Fleet_Driver.IsActive == true && (a.Fleet_Driver.SubcompanyId == AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId == 0) && (a.ZoneId == null || a.ZoneId != null && a.Gen_Zone.ZoneName != "SIN BIN")
                       && (a.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.AVAILABLE && a.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.ONBREAK)
                       // orderby a.QueueNo
                       orderby a.QueueDateTime
                       select new
                       {
                           Id = a.DriverId,
                           DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")

                       }).Distinct().ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);
       }


        public static void FillDriverNoFOJDespatchQueueCombo(RadDropDownList dropDown)
        {
            var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                        where a.DriverId != null && a.Status == true && a.Fleet_Driver.IsActive == true && (a.Fleet_Driver.SubcompanyId == AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId == 0) && (a.ZoneId == null || a.ZoneId != null && a.Gen_Zone.ZoneName != "SIN BIN")
                        && (a.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.AVAILABLE && a.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.ONBREAK) && (a.IsManualLogin==null || a.IsManualLogin==false)
                        // orderby a.QueueNo
                        orderby a.QueueDateTime
                        select new
                        {
                            Id = a.DriverId,
                            DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")

                        }).Distinct().ToList();

            ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);
        }


        public static void FillDriverDates(RadDropDownList dropDown, Expression<Func<DriverRent, bool>> _condition)
       {
           var list = (from a in AppVars.BLData.GetQueryable<DriverRent>(_condition).AsEnumerable().OrderByDescending(c => c.TransDate)

                       select new
                       {
                           Id = a.Id,
                           Dates = string.Format("{0:dd-MMM-yyyy}", a.FromDate) + " To " + string.Format("{0:dd-MMM-yyyy}", a.ToDate)
                       }).ToList();


           ComboFunctions.FillCombo(list, dropDown, "Dates", "Id");
       }


       public static void FillPDADLoginriverNoCombo(RadDropDownList dropDown)
       {
           var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                       where a.DriverId != null && a.Status == true && a.Fleet_Driver.HasPDA == true
                       // orderby a.QueueNo
                       orderby a.QueueDateTime
                       select new
                       {
                           Id = a.Fleet_Driver.DriverNo,
                           DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")
                           //    DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName ")
                       }).Distinct().ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);
       }


       public static void FillAllPDADLoginriverIdCombo(RadDropDownList dropDown)
       {
           var list = (from a in General.GetQueryable<Fleet_Driver>(c=> c.HasPDA==true && (c.IsActive!=null && c.IsActive==true))                    
                      
                       select new
                       {
                           Id = a.Id,
                           DriverNo=a.DriverNo,
                           DriverName = (a.DriverNo + " - " + a.DriverName + " [" + a.Fleet_VehicleType.VehicleType + "]")
                           //    DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName ")
                       }).AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>()).ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);
       }




       public static void FillPDADLoginriverIdCombo(RadDropDownList dropDown)
       {
           var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                       where a.DriverId != null && a.Status == true && a.Fleet_Driver.HasPDA == true
                      // orderby a.QueueNo
                      orderby a.QueueDateTime
                       select new
                       {
                           Id = a.DriverId,
                           DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")
                           //    DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName ")
                       }).Distinct().ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);
       }

       public static void FillNonAvailPDADLoginDriverCombo(RadDropDownList dropDown)
       {
           var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(c => c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONROUTE
                                                                       || c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ARRIVED
                                                                       )
                       where a.DriverId != null && a.Status == true && a.Fleet_Driver.HasPDA == true
                      // orderby a.QueueNo
                      orderby a.QueueDateTime
                       select new
                       {
                           Id = a.DriverId,
                           DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")
                           //    DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName ")
                       }).Distinct().ToList();

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);
       }

      


       public static void FillDriverNoQueueCombo(RadDropDownList dropDown, int? driverId, string driverNameNo)
       {
           var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                       where a.DriverId != null && a.Status == true && a.Fleet_Driver.IsActive==true
                       && (a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE || a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONBREAK)
                      // orderby a.QueueNo
                      orderby a.QueueDateTime
                       select new
                       {
                           Id = a.DriverId,
                           DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")

                       }).ToList();

           if (driverId != null && list.Count(c => c.Id == driverId) == 0)
           {

               list.Add(new { Id = driverId, DriverName = driverNameNo });

           }

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);

           if (driverId != null)
           {

               dropDown.SelectedValue = driverId;

           }

       }


       public static void FillDriverNoQueueCombo(MyDropDownList dropDown, int? driverId, string driverNameNo)
       {
           var list = (from a in General.GetQueryable<Fleet_DriverQueueList>(null)
                       where a.DriverId != null && a.Status == true && a.Fleet_Driver.IsActive == true
                       && (a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE || a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONBREAK)
                       // orderby a.QueueNo
                       orderby a.QueueDateTime
                       select new
                       {
                           Id = a.DriverId,
                           DriverName = (a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName + " [" + a.Fleet_Driver.Fleet_VehicleType.VehicleType + "]")

                       }).ToList();

           if (driverId != null && list.Count(c => c.Id == driverId) == 0)
           {

               list.Add(new { Id = driverId, DriverName = driverNameNo });

           }

           ComboFunctions.FillCombo(list, dropDown, "DriverName", "Id", false);

           if (driverId != null)
           {

               dropDown.SelectedValue = driverId;

           }

       }


       public static void FillDriverCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<Fleet_Driver>
            (AppVars.BLData.GetAll<Fleet_Driver>(c=>c.IsActive==true).OrderBy(c => c.DriverName).ToList(), dropDown, "DriverName", "Id");

       }





       public static void FillMultiColumnCustomerCombo(RadMultiColumnComboBox dropDown)
       {
           var list = General.GetQueryable<Customer>(null).Select(args => new
                                                            {
                                                                Id = args.Id,
                                                                Name = args.Name,
                                                                Address = args.Address1,
                                                                Telephone = args.TelephoneNo,
                                                                Mobile = args.MobileNo,
                                                                Email = args.Email
                                                            }).Distinct().ToList();

           dropDown.DataSource = list;
           dropDown.DisplayMember = "Name";
           dropDown.ValueMember = "Id";

           dropDown.NullText = "Select";
           dropDown.SelectedIndex = -1;

           dropDown.EditorControl.ShowRowHeaderColumn = false;
           dropDown.EditorControl.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;



       }



       public static void FillPaymentTypeCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<Gen_PaymentType>
            (AppVars.BLData.GetAll<Gen_PaymentType>(c=>c.IsVisible==true).OrderBy(c => c.PaymentType).ToList(), dropDown, "PaymentType", "Id");
       }


       public static void FillBookingTypeCombo(RadDropDownList dropDown)
       {

           ComboFunctions.FillCombo<BookingType>
               (General.GetQueryable<BookingType>(c=>c.Id!=Enums.BOOKING_TYPES.ONROAD)
           // (AppVars.BLData.GetAll<BookingType>(c=>c.Id!=Enums.BOOKING_TYPES.ONROAD)
                        .OrderBy(c => c.Id).ToList(), dropDown, "BookingTypeName", "Id");


       }
        public static void FillBookingTypeCombo(ComboBox dropDown)
        {

            ComboFunctions.FillCombo<BookingType>
                (General.GetQueryable<BookingType>(c => c.Id != Enums.BOOKING_TYPES.ONROAD)
                         // (AppVars.BLData.GetAll<BookingType>(c=>c.Id!=Enums.BOOKING_TYPES.ONROAD)
                         .OrderBy(c => c.Id).ToList(), dropDown, "BookingTypeName", "Id");


        }



        public static void FillCompanyDepartmentCombo(RadDropDownList dropDown,Expression<Func<Gen_Company_Department,bool>> _condition)
       {

           ComboFunctions.FillCombo<Gen_Company_Department>
            (General.GetQueryable<Gen_Company_Department>(_condition).OrderBy(c => c.DepartmentName).ToList()
                        , dropDown, "DepartmentName", "Id");


       }

       public static void FillEscortCombo(RadDropDownList dropDown, Expression<Func<Gen_Escort, bool>> _condition)
       {

           ComboFunctions.FillCombo<Gen_Escort>
            (General.GetQueryable<Gen_Escort>(null).OrderBy(c => c.EscortName).ToList()
                        , dropDown, "EscortName", "Id");


       }

       public static void FillCompanyCostCentersCombo(RadDropDownList dropDown, Expression<Func<Gen_Company_CostCenter, bool>> _condition)
       {

           ComboFunctions.FillCombo<Gen_Company_CostCenter>
            (General.GetQueryable<Gen_Company_CostCenter>(_condition).OrderBy(c => c.CostCenterName).ToList()
                        , dropDown, "CostCenterName", "Id");


       }






    }
}
