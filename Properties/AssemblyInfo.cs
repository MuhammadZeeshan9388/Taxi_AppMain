using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Cab Treasure System")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("CabTreasure")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("0e426622-4090-4010-b9f5-d4ae5d4f434f")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("4.11.638")]
[assembly: AssemblyFileVersion("4.11.638.0")]
// multibooking editing => return via swap fixed
// 4.68.620.0 
// i) surcharge on plot wise by date & time criteria only for mileage fares

// 4.69.620.0 
// i) surcharge on plot wise by date & time criteria on both fixed and mileage fares,
//(OAKWOOD STATION CARS REQUESTED)


// 4.75.620
// Fixed : when you open different driver commission/Rent list , its opening multiple windows.




// 4.79.620 till 4.82.620
// BOOKING FORM 2 CHANGES :
// 1) asap pickup time
// 2) account dropdown autosuggess
// 3) exclude driver

//4.86.620
/*
 * Mole Valley Taxis Changes.
1. Enable FAre Increment  (show apply on fixed fares and plot to plot
2. Enable Surcharge on Plot on all type of fares
3. Show Account Password field on booking if its enabled from account info.
*/


//4.29.621
/*
 
1. FIXED : When you make a multibooking with W/R journeytype its make a return journey multibooking. (ESCALATED BY TANVEER FOR CAMBRIDGE TAXIS)
2. NEW : ADLANTE CARD PAYMENT  => 4.26.621
3. FIXED : When you login a dispatch sometimes price plot grid comes up and driver plots grid not showing up.
*/


// NEED TO UPDATE ON BLANK DB
// UPDATE STP_UPDATEJOBANDROUTE FROM ALPHA EAGLE CARS TO SET  AUTOPLOT ZONE ON CLEAR JOB STATUS
// ivr new scripts for driver abop



// 4.30.621
// invoice template 10 changes for molevalley



// 4.45.621
// On Rent Pay and Comm Pay show only Active Drivers


// 4.1.628
// Account Invoice Payment Report


//  4.10.628
// escort work in booking template 2


//  4.23.628.0
// woking taxis 5 points


//  4.1.630.0
// OXFORD TRAVEL => ESCORT , ESCORT PRICE IN BOOKING
// DEPARTMENT WISE PICKUP AND DROPOFF


//  4.2.630.0
// agent commission

//  4.5.630.0
// NEW REPORTS => DRIVER COMMISSION DETAIL REPORT =>FALCON CARS


//4.9.630.0
/*1.	Driver Earning Report  => (Changes) – (See attachment1.png)
They want us to remove Hrs, Break, Decline, N/S, Earning, Avg/Job, Avg/Day & Avg/Hrs columns and add four new columns with following details.

1.	After Commission they need another “Owed” column which contains the result of Account jobs – Driver Commission.
2.	Driver Expenditure - Contains parking charges or congestion charges.
3.	Driver PDA Rent.
4.	Net Due – which is Owed + Driver Expenditure.

2)In driver earning the drivers waiting time is not shown.  (ON F8 DRIVER EARNING => Change : THEY WANT Waiting & Parking Column
  E.g if in a job, fare is 65 and waiting  is 14.in driver earning  it will show only 65 . not (65+14)= 79.  


    3)  FULL POSTCODE FIXED FARES SUBCOMPANY WISE – CHANGES : INCLUDE FIXED FARES FOR FULL POSTCODE WITH PARTICULAR SUBCOMPANY. 
              [ THEY DON’T WANT TO CREATE PLOTS FURTHER, SO FULL POSTCODE TO FULL POSTCODE FARE BY EACH SUBCOMPANY SHOULD WORK]
add post code to post code fare starting with KT22, client added many fares of that Railway station post code in system eg KT22 to KT22,  KT22 0UT to  KT22 8QX, system is not calculating fares of Post code (KT22) second part, as mentioned Post code starting digits


 * 
 * 
 * */


//4.10.630.0
//ADD PICKUPDATE COLUMN ON CALLERID POPUP.