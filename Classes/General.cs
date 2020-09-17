using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using DAL;
using Taxi_Model;
using System.Linq.Expressions;
using Utils;
using System.Windows.Forms;
using Telerik.WinControls.UI.Docking;
using UI;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using System.Data.Linq;
using System.Text.RegularExpressions;
using Taxi_BLL;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Microsoft.Reporting.WinForms;
using Taxi_AppMain.Classes;
using System.Xml;
using System.Data;
using System.Net;
using Microsoft.Win32;
using System.Net.Sockets;
using DotNetCoords;
using WebApi;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;

namespace Taxi_AppMain
{
    public class General
    {


       public static int? CallerIdType = null;

        public static string DownloadRecordingFile(string FolderPath, string BaseURL, string ClientUserName, string UniqueID, string CallerId,DateTime bookingDate)
        {





            try
            {

                if(CallerIdType==null)
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        CallerIdType= db.CallerIdType_Configurations.Select(c => c.VOIPCLIType).FirstOrDefault().DefaultIfEmpty().ToInt();
                    }
                }


                if (CallerIdType == 2)  // YESTECH
                {
                    TCS.Call.MakeCall c = new TCS.Call.MakeCall();
                   return  c.YESTECH_GetRecordingFile(FolderPath, BaseURL, "", UniqueID, bookingDate, AppVars.objPolicyConfiguration.CallRecordingToken);

                }

                else
                {

                    string fileName = UniqueID + "_" + CallerId + ".wav";
                    string FileUrl = BaseURL.Trim().TrimEnd('/') + "/" + ClientUserName + "/" + fileName;
                    //string FloderPath = System.IO.Directory.GetCurrentDirectory() + "\\" + "Recordings";

                    if (!System.IO.Directory.Exists(FolderPath))
                    {
                        System.IO.Directory.CreateDirectory(FolderPath);
                    }

                    using (WebClient wc = new WebClient())
                    {
                        //wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                        wc.DownloadFile(
                            // Param1 = Link of file
                            new System.Uri(FileUrl),
                            // Param2 = Path to save
                            FolderPath + "\\" + fileName
                        );
                    }



                    return FolderPath + "\\" + fileName;

                }
            }
            catch (Exception exe)
            {
            }

            return string.Empty;
        }


        public static string DecryptSysCon(string dataString)
        {
            return Cryptography.Decrypt(dataString, "Y2FidHJlYXN1cmU6Y2FidHJlYXN1cmU5ODcwIUAj", true);

        }

        public static string EncryptSysCon(string dataString)
        {
            return Cryptography.Encrypt(dataString, "Y2FidHJlYXN1cmU6Y2FidHJlYXN1cmU5ODcwIUAj", true);

        }


        public static bool VerifyLicense(string defaultClientId)
        {
            bool verify = false;
            

            try
            {
                try
                {
                    string Urls = "http://eurlic.co.uk/license/api/Cab/VerifyLicense";
                    var baseAddress = new Uri(Urls);
                    var json = string.Empty;
                    json = "{\"DefaultClientID\":" + "\"" + defaultClientId + "\"" + "}";


                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(Urls);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Headers.Add("Authorization", "Basic " + "Y2FidHJlYXN1cmU6Y2FidHJlYXN1cmU5ODcwIUAj");
                    //   string usernamePassword = Base64Encode("cabtreasure:cabtreasure9870!@#");
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {


                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        Program.objLic = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<ClsLic>(result);
                    }

                }
                catch (Exception ex)
                {
                    Program.objLic.IsValid = false;
                    Program.objLic.Reason = ex.Message;


                }


                if (Program.objLic.IsValid)
                {
                    verify = true;

                    if (Program.objLic.ExpiryDateTime.ToStr().Trim().Length > 0)
                    AppVars.LicenseExpiryDate = "License will Expire on " + string.Format("{0:dd/MMM/yyyy HH:mm}", Program.objLic.ExpiryDateTime.ToDateTimeorNull());

                    string serialized = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(Program.objLic);
                    try
                    {

                        File.WriteAllText(Application.StartupPath + "\\SysCon.dat", General.EncryptSysCon(serialized));
                    }
                    catch
                    {


                    }
                }
                else
                {

                    if (Program.objLic.Reason.ToStr().Trim().Length > 0)
                    {


                        if(Program.objLic.ExpiryDateTime.ToStr().Trim().Length > 0)
                             AppVars.LicenseExpiryDate = "License will Expire on " + string.Format("{0:dd/MMM/yyyy HH:mm}", Program.objLic.ExpiryDateTime.ToDateTimeorNull());


                        if (File.Exists(Application.StartupPath + "\\SysCon.dat"))
                        {

                            string data = File.ReadAllText(Application.StartupPath + "\\SysCon.dat");

                            Program.objLic = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<ClsLic>(General.DecryptSysCon(data));

                            using (LicDataContextDataContext db = new LicDataContextDataContext())
                            {
                                stp_SysPolicyAuthResult objClient = db.stp_SysPolicyAuth(defaultClientId).FirstOrDefault();

                                if (objClient != null)
                                {
                                    Program.objLic.ExpiryDateTime =string.Format("{0:dd/MM/yyyy HH:mm}", objClient.LastCondition.ToDateTimeorNull());

                                    if (objClient.ScriptType.ToStr().ToLower() == "valid")
                                    {
                                        verify = true;
                                        Program.objLic.IsValid = true;

                                        string serialized = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(Program.objLic);
                                        try
                                        {
                                            File.WriteAllText(Application.StartupPath + "\\SysCon.dat", General.EncryptSysCon(serialized));


                                        }
                                        catch
                                        {


                                        }
                                    }


                                    AppVars.LicenseExpiryDate = "License will Expire on " + string.Format("{0:dd/MMM/yyyy HH:mm}", Program.objLic.ExpiryDateTime.ToDateTimeorNull());

                                }
                                else
                                {

                                    Program.objLic.IsValid = false;
                                    Program.objLic.Reason = "Authentication Failed...";
                                  //  MessageBox.Show("Authentication Failed...");
                                }


                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Program.objLic.IsValid = false;
                Program.objLic.Reason = ex.Message;


            }


            return verify;

        }

        public static decimal GetFixFareRate(int companyId, int vehicleTypeId, int tempFromLocId, int tempToLocId, string tempFromPostCode
    , string tempToPostCode, ref string errorMsg, ref List<decimal> milesList, bool IsVia, bool CanCheckZoneWise, DateTime? pickupTime, ref decimal deadMileage, int fromLocTypeId, int toLocTypeId, ref bool companyFareExist, ref string estimatedTime, int? fromZoneId, int? toZoneId, ref bool IsMoreFareWise, ref int farecalculateby, int subCompanyId)
        {

            decimal rtnFare = 0.00m;
            string fromVal = tempFromPostCode;
            string toVal = tempToPostCode;


            tempFromPostCode = GetPostCodeMatch(tempFromPostCode);
            tempToPostCode = GetPostCodeMatch(tempToPostCode);

            //bool surchargeRateFromAmountWise = false;
            //bool surchargeRateToAmountWise = false;

            //decimal surchargeRateFrom = 0.00m;
            //decimal surchargeRateTo = 0.00m;

            // bool IsMoreFareWise = false;
            int actualVehicleTypeId = vehicleTypeId;
            try
            {

                //if ((tempFromPostCode.Length > 0 || fromZoneId.ToInt() > 0) && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN))
                //{
                //    tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                //  //  surchargeRateFrom = GetSurchargeRate(tempFromPostCode, fromZoneId, pickupTime.ToDateTime(), ref surchargeRateFromAmountWise);
                //}

                //if ((tempToPostCode.Length > 0 || toZoneId.ToInt() > 0) && (toLocTypeId != Enums.LOCATION_TYPES.TOWN))
                //{
                //    tempToPostCode = General.GetPostCodeMatch(tempToPostCode);
                //  //  surchargeRateTo = GetSurchargeRate(tempToPostCode, toZoneId, pickupTime.ToDateTime(), ref surchargeRateToAmountWise);
                //}

                //if (tempFromPostCode.Length > 0 && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN))
                //{
                //    tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                //    surchargeRateFrom = GetSurchargeRate(tempFromPostCode, ref surchargeRateFromAmountWise);
                //}

                //if (tempToPostCode.Length > 0 && (toLocTypeId != Enums.LOCATION_TYPES.TOWN))
                //{
                //    tempToPostCode = General.GetPostCodeMatch(tempToPostCode);
                //    surchargeRateTo = GetSurchargeRate(tempToPostCode, ref surchargeRateToAmountWise);
                //}


                string fromSingleHalfPostCode = string.Empty;
                string fromHalfPostCode = string.Empty;
                string startFromPostCode = "";
                //if (tempFromLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempFromPostCode) && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    string[] fromArr = tempFromPostCode.Split(new char[] { ' ' });
                    startFromPostCode = fromArr[0];

                    fromHalfPostCode = startFromPostCode;

                    startFromPostCode = General.CheckIfSpecialPostCode(startFromPostCode);

                    fromSingleHalfPostCode = fromArr[0] + " " + fromArr[1][0];

                }

                //   }


                string ToSingleHalfPostCode = string.Empty;
                string toHalfPostCode = string.Empty;
                string startToPostCode = "";
                //if (tempToLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempToPostCode) && (toLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    string[] toArr = tempToPostCode.Split(new char[] { ' ' });

                    startToPostCode = toArr[0];
                    toHalfPostCode = startToPostCode;
                    startToPostCode = General.CheckIfSpecialPostCode(startToPostCode);

                    ToSingleHalfPostCode = toArr[0] + " " + toArr[1][0];
                }
                //}

                //int defaultVehicleId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();

                //if (vehicleTypeId != defaultVehicleId)
                //{

                //    if (AppVars.objPolicyConfiguration.ApplyPercentageWiseFareOn.ToBool())
                //    {





                //        if (IsMoreFareWise == false)
                //        {
                //            if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                //                                            ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.FromZoneId == fromZoneId || c.OriginId == tempFromLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))))
                //                                                  && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.ToZoneId == toZoneId || c.DestinationId == tempToLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))))))

                //                                                  || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (c.OriginId == tempToLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode)))))
                //                                                  && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (c.DestinationId == tempFromLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))))))
                //                                                     )
                //                                               && c.Fare.VehicleTypeId == defaultVehicleId
                //                                                && (c.Fare.CompanyId == companyId || companyId == 0)).Count() > 0)

                //                                          )
                //            {

                //                vehicleTypeId = defaultVehicleId;
                //                IsMoreFareWise = true;
                //            }
                //        }


                //    }
                //    else
                //    {

                //        if (AppVars.objPolicyConfiguration.EnableZoneWiseFares.ToBool() == false)
                //        {


                //            if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                //                                                      ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                //                                                            && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                //                                                            || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                //                                                            && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                //                                                               )
                //                                                         && c.Fare.VehicleTypeId == vehicleTypeId
                //                                                          && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0)

                //                && (General.GetQueryable<Fare_OtherCharge>(c => c.Fare.VehicleTypeId == vehicleTypeId
                //                                                              && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0))
                //            {

                //                vehicleTypeId = defaultVehicleId;
                //                IsMoreFareWise = true;
                //            }
                //        }
                //    }


                //}





                List<Fare_ChargesDetail> list = null;

                fromVal = fromVal.Replace("  ", " ");
                toVal = toVal.Replace("  ", " ");


                  if ((list == null || (list != null && list.Count() == 0)) && tempFromPostCode.Length > 0 && tempToPostCode.Length >0)
                    {

                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()) || (c.FromAddress.ToUpper().EndsWith(tempFromPostCode)) )) || c.OriginId == tempFromLocId)
                                                                       && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()     ) || ( c.ToAddress.ToUpper().EndsWith(tempToPostCode)  )     )) || c.DestinationId == tempToLocId))

                                                                          )

                                                                     && c.Fare.VehicleTypeId == vehicleTypeId

                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                            //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                      ).ToList();

                    }

                    if ((list == null || (list != null && list.Count() == 0)) && tempFromPostCode.Length > 0 && tempToPostCode.Length > 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) ||(tempFromLocId==0 && (c.FromAddress.ToUpper().EndsWith(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                    && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (tempToLocId == 0 && (c.ToAddress.ToUpper().EndsWith(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                    || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (tempToLocId == 0 && (c.FromAddress.ToUpper().EndsWith(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                    && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (tempFromLocId == 0 && (c.ToAddress.ToUpper().EndsWith(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                  && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();


                        if (list != null && list.Count > 0)
                        {
                            errorMsg = "Reverse found";

                        }

                    }

                    if ((tempFromLocId != 0 || tempToLocId != 0) && (list == null || (list != null && list.Count() == 0)))
                    {
                        if (tempFromLocId > 0)
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                   && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                   || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                   && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                      )

                                                                 && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                  ).ToList();

                        }

                        if ((list == null || list.Count == 0) && tempToLocId > 0)
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                    || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                    && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();

                        }



                        if ((list == null || list.Count == 0))
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                    || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();

                        }

                        if (list != null && list.Count > 0)
                        {
                            errorMsg = "Reverse found";

                        }


                    }

                

                //if ((tempFromLocId == 0 && string.IsNullOrEmpty(startFromPostCode)) || (tempToLocId == 0 && string.IsNullOrEmpty(startToPostCode)))
                //    obj = null;


                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 2)
                {
                    tempFromPostCode = fromVal;
                    tempToPostCode = toVal;
                }



                Fare_ChargesDetail obj = null;

                if (list != null)
                {
                    if (companyId != 0)
                    {
                        if (list.Count(c => c.Fare.CompanyId == companyId) > 0)
                        {
                            obj = list.FirstOrDefault(c => c.Fare.CompanyId == companyId);

                            companyFareExist = true;
                        }
                        else
                        {

                            if (General.GetQueryable<Taxi_Model.Fare>(c => c.CompanyId == companyId).Count() == 0)
                            {
                                obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                                companyFareExist = true;

                            }


                        }
                    }
                    else
                    {
                        obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                    }

                }


                if (obj != null)
                {

                    rtnFare = obj.Rate.ToDecimal();
                    deadMileage = 0;

                    farecalculateby = 4;

                }




                //if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == false)
                //{

                //    decimal totalSurchargePercentage = surchargeRateFrom + surchargeRateTo;

                //    decimal fareSurchargePercent = (rtnFare * totalSurchargePercentage) / 100;
                //    rtnFare = rtnFare + fareSurchargePercent;

                //}
                //else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == true)
                //{

                //    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                //}
                //else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == false)
                //{
                //    surchargeRateTo = (rtnFare * surchargeRateTo) / 100;

                //    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                //}
                //else if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == true)
                //{
                //    surchargeRateFrom = (rtnFare * surchargeRateFrom) / 100;

                //    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                //}



            }
            catch
            {


                //   MessageBox.Show(ex.Message);
            }
            return rtnFare;
        }

      

        public static void ShowEmailForm(ReportViewer viewer, string fileTitle, string email, Gen_SubCompany objSubCompany,bool showDialog)
        {
            frmEmail frm = new frmEmail(viewer, fileTitle, email, objSubCompany);
            frm.StartPosition = FormStartPosition.CenterScreen;

            if (showDialog)
                frm.ShowDialog();
            else
                frm.Show();
        }


        public static void LoadConfiguration()
        {

            AppVars.objPolicyConfiguration = GetObject<Gen_SysPolicy_Configuration>(c => c.SysPolicyId == 1);


        }

        public static void AddUserLog(string message,int logType)
        {

            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {

                    db.stp_AddUserLogs(AppVars.LoginObj.LuserId.ToInt(), message, logType);

                }
            }
            catch
            {


            }

        }

        public static bool UpdateThirdPartyJob(Booking objBooking, long jobId, string status)
        {

            bool rtn = true;


            Fleet_Driver objDriver = null;
            Fleet_Driver_Location objDrvLocation = null;
            try
            {


                if (objBooking == null)
                    objBooking = General.GetObject<Booking>(c => c.Id == jobId);


                int sysgenId=objBooking.CompanyId!=null ? objBooking.Gen_Company.SysGenId.ToInt():0;

                if (sysgenId == Enums.SYSGEN_COMPANY.KARHOO)
                {


                    WebApi.BookingInformation obj = new WebApi.BookingInformation();
                    obj.booking_id = objBooking.Id.ToStr();
                    obj.karhoo_ref = objBooking.OnlineBookingId.ToStr();

                    obj.status = status.ToLower();

                    obj.vehicle = new WebApi.Vehicle();
                    obj.vehicle.status = status.ToLower();
                    objDriver = objBooking.Fleet_Driver.DefaultIfEmpty();

                    if (objDriver != null)
                    {

                        objDrvLocation = objDriver.Fleet_Driver_Locations.FirstOrDefault();


                        obj.vehicle.color = objDriver.VehicleColor.ToStr();


                        string driverName = objDriver.DriverName;

                        string[] arr = driverName.Split(new char[] { ' ' });

                        obj.vehicle.driver_first_name = arr[0].ToStr();

                        if (arr.Length > 1)
                        {
                            obj.vehicle.driver_last_name = arr[1].ToStr();
                        }

                        obj.vehicle.driver_phone = objDriver.MobileNo.ToStr();
                        obj.vehicle.eta_minutes = objDrvLocation.EstimatedTimeLeft.ToStr();
                        obj.vehicle.latitude = objDrvLocation.Latitude;
                        obj.vehicle.longitude = objDrvLocation.Longitude;
                        obj.vehicle.make = objDriver.VehicleMake.ToStr();
                        obj.vehicle.model = objDriver.VehicleModel.ToStr();
                        obj.vehicle.vehicle_plate = objDriver.VehicleNo.ToStr();
                        obj.vehicle.vehicle_id = objDriver.VehicleTypeId.ToStr();
                        obj.vehicle.vehicle_type = objDriver.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr();
                        obj.vehicle.driver_id = objDriver.DriverNo.ToStr();
                    }


                    //if (status != "declined")
                    //{
                    //    obj.vehicle.to_location = new ToLocation();
                    //    obj.vehicle.to_location.address = GetWebApiAddress(objBooking.ToDoorNo.ToStr(), objBooking.ToAddress.ToStr().Trim(), objBooking.ToPostCode.ToStr().Trim());
                    //}


                    WebAPI.Karho.UpdateTripStatus(obj, "cabtreasure", "awKcEGZPt6NA7Mg9VTJbZPSZ8zTQRaDK");



                    if (status.ToLower() == "completed")
                    {

                        WebApi.GetFinalPriceProperties objPricebooking = new WebApi.GetFinalPriceProperties();

                        objPricebooking.karhoo_ref = objBooking.OnlineBookingId.ToStr();
                        objPricebooking.supplier_company = "cabtreasure";
                        objPricebooking.booking_id = objBooking.Id.ToStr();
                        objPricebooking.notes = objBooking.NotesString;
                        objPricebooking.status = objBooking.BookingStatus.StatusName;
                        objPricebooking.vehicle.vehicle_type = objDriver.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr();
                        objPricebooking.vehicle.vehicle_id = objBooking.Fleet_VehicleType.Id.ToStr();
                        objPricebooking.vehicle.vehicle_plate = objDriver.VehicleNo.ToStr();
                        double lat = (double)objDrvLocation.Longitude;
                        objPricebooking.vehicle.latitude = lat;
                        objPricebooking.vehicle.longitude = objDrvLocation.Latitude;
                        objPricebooking.vehicle.eta_minutes = objDrvLocation.EstimatedTimeLeft.ToStr();
                        objPricebooking.vehicle.make = objDriver.VehicleMake.ToStr();
                        objPricebooking.vehicle.model = objDriver.VehicleModel.ToStr();
                        objPricebooking.vehicle.color = objDriver.VehicleColor.ToStr();
                        objPricebooking.vehicle.driver_id = objDriver.DriverNo.ToStr();
                        string driverName = objDriver.DriverName;

                        string[] arr = driverName.Split(new char[] { ' ' });
                        objPricebooking.vehicle.driver_first_name = arr[0].ToStr();
                        if (arr.Length > 1)
                        {
                            objPricebooking.vehicle.driver_last_name = arr[1].ToStr();
                        }



                        objPricebooking.vehicle.driver_phone = objDriver.MobileNo.ToStr();
                        double faretotal = (double)objBooking.FareRate;


                        objPricebooking.total = faretotal;
                        objPricebooking.currency = "GBP";
                        objPricebooking.price_components = new List<PriceComponent>()
                        {
                            new PriceComponent {component_name = "base rate", value = (double)objBooking.FareRate, description = "Base Rate"},
                            new PriceComponent {component_name = "parking", value = 0.0, description = "Parking"},
                            new PriceComponent {component_name = "tax", value = 0.0, description = "Tax"}
                        };

                        WebAPI.Karho.UpdateTripFinalPrice(objPricebooking, "cabtreasure", "awKcEGZPt6NA7Mg9VTJbZPSZ8zTQRaDK");

                    }
                }
                else if (sysgenId == Enums.SYSGEN_COMPANY.KABBEE)
                {
                    WebApi.KabbeeProperties obj = new KabbeeProperties();
                    obj.FleetBookingId = objBooking.Id.ToStr();
                    
                      status = status.ToLower();
                      string response = string.Empty;

                    if (status == "declined")
                    {
                    
                        obj.Status = WebApi.KabbeeStatus.CANCELLED;               

                    }
                    else
                    {
                    

                        if (status == "allocated")
                        {
                            obj.Status = WebApi.KabbeeStatus.ALLOCATED;
                        }
                        else if (status == "onroute")
                        {

                            obj.Status = WebApi.KabbeeStatus.ALLOCATED;
                        }
                        else if (status == "arrived")
                        {

                            obj.Status = WebApi.KabbeeStatus.ARRIVED;
                        }
                        else if (status == "pob")
                        {

                            obj.Status = WebApi.KabbeeStatus.POB;
                        }
                        else if (status == "completed")
                        {

                            obj.Status = WebApi.KabbeeStatus.CLEAR;
                        }


                         objDriver=objBooking.Fleet_Driver;
                         if (objDriver != null)
                         {

                             // Driver Details
                             obj.Driver = new KabbeeDriver();
                             obj.Driver.CallNo = objDriver.DriverNo.ToStr();
                             obj.Driver.FullName = objDriver.DriverName.ToStr();
                             obj.Driver.PcoNo = objDriver.Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.PCODriver).DefaultIfEmpty().BadgeNumber.ToStr();


                             // Driver Vehicle Details
                             obj.Vehicle = new KabbeeVehicle();
                             obj.Vehicle.Colour = objDriver.VehicleColor.ToStr();
                             obj.Vehicle.Make = objDriver.VehicleMake.ToStr();
                             obj.Vehicle.Model = objDriver.VehicleModel.ToStr();
                             obj.Vehicle.RegistrationNo = objDriver.VehicleNo.ToStr();


                             if (status != "allocated")
                             {
                                  objDrvLocation = objDriver.Fleet_Driver_Locations.FirstOrDefault();
                                  if (objDrvLocation != null && objDrvLocation.Latitude != 0)
                                  {

                                      obj.Vehicle.Gps = new KabbeeGPS();
                                      obj.Vehicle.Gps.Latitude = objDrvLocation.Latitude.ToStr();
                                      obj.Vehicle.Gps.Longitude = objDrvLocation.Longitude.ToStr();
                                  }
                             }
                         }
                    }

                    response = WebApi.Kabbee.BookingStatusUpdate(obj, "Y2FidHJlYXN1cmVAY2Fia2FiYmVlLmNvbTpjYWJ0cmVhc3VyZQ==");

                }

            }
            catch
            {

                rtn = false;
            }

            return rtn;

        }


        public static bool UpdateKarhoTripData(Booking objBooking, long jobId, string status)
        {

            bool rtn = true;


            Fleet_Driver objDriver = null;
            Fleet_Driver_Location objDrvLocation = null;
            try
            {


                if (objBooking == null)
                    objBooking = General.GetObject<Booking>(c => c.Id == jobId);

                UpdatStatusFinaleProperties obj = new UpdatStatusFinaleProperties();

                obj.booking_id = objBooking.Id.ToStr();
                obj.karhoo_ref = objBooking.OnlineBookingId.ToStr();

                obj.status = status.ToLower();

                obj.vehicle = new WebApi.Vehicle();
                obj.vehicle.status = status.ToLower();
                objDriver = objBooking.Fleet_Driver;


                if (objDriver != null)
                {

                    objDrvLocation = objDriver.Fleet_Driver_Locations.FirstOrDefault();


                    obj.vehicle.color = objDriver.VehicleColor.ToStr();


                    string driverName = objDriver.DriverName;

                    string[] arr = driverName.Split(new char[] { ' ' });

                    obj.vehicle.driver_first_name = arr[0].ToStr();

                    if (arr.Length > 1)
                    {
                        obj.vehicle.driver_last_name = arr[1].ToStr();
                    }

                    obj.vehicle.driver_phone = objDriver.MobileNo.ToStr();
                    obj.vehicle.eta_minutes = objDrvLocation.EstimatedTimeLeft.ToStr();
                    obj.vehicle.latitude = objDrvLocation.Latitude;
                    obj.vehicle.longitude = objDrvLocation.Longitude;
                    obj.vehicle.make = objDriver.VehicleMake.ToStr();
                    obj.vehicle.model = objDriver.VehicleModel.ToStr();
                    obj.vehicle.vehicle_plate = objDriver.VehicleNo.ToStr();
                    obj.vehicle.vehicle_id = objDriver.VehicleTypeId.ToStr();
                    obj.vehicle.vehicle_type = objDriver.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr();
                    obj.vehicle.driver_id = objDriver.DriverNo.ToStr();
                }




                obj.from_location = new FromLocation();
                obj.from_location.address = GetWebApiAddress(objBooking.FromDoorNo.ToStr(), objBooking.FromAddress.ToStr().Trim(), objBooking.FromPostCode.ToStr().Trim());



                if (obj.from_location.address.postal_code.ToStr().Contains(" "))
                {

                    var objCoord = General.GetObject<Gen_Coordinate>(c => c.PostCode == obj.from_location.address.postal_code.ToUpper());


                    if (objCoord != null)
                    {

                        obj.from_location.latitude = Convert.ToDouble(objCoord.Latitude);
                        obj.from_location.longitude = Convert.ToDouble(objCoord.Longitude);

                    }
                    else
                    {

                        var objDCoord = GetDistance.PostCodeToLongLat(obj.from_location.address.postal_code.ToUpper(), "GB");

                        if (objDCoord != null)
                        {

                            obj.from_location.latitude = Convert.ToDouble(objDCoord.Value.Latitude);
                            obj.from_location.longitude = Convert.ToDouble(objDCoord.Value.Longitude);

                        }

                    }
                }


                obj.to_location = new ToLocation();
                obj.to_location.address = GetWebApiAddress(objBooking.ToDoorNo.ToStr(), objBooking.ToAddress.ToStr().Trim(), objBooking.ToPostCode.ToStr().Trim());


                if (obj.to_location.address.postal_code.ToStr().Contains(" "))
                {

                    var objCoord = General.GetObject<Gen_Coordinate>(c => c.PostCode == obj.to_location.address.postal_code.ToUpper());


                    if (objCoord != null)
                    {

                        obj.to_location.latitude = Convert.ToDouble(objCoord.Latitude);
                        obj.to_location.longitude = Convert.ToDouble(objCoord.Longitude);

                    }
                    else
                    {

                        var objDCoord = GetDistance.PostCodeToLongLat(obj.to_location.address.postal_code.ToUpper(), "GB");

                        if (objDCoord != null)
                        {

                            obj.to_location.latitude = Convert.ToDouble(objDCoord.Value.Latitude);
                            obj.to_location.longitude = Convert.ToDouble(objDCoord.Value.Longitude);

                        }

                    }
                }


                WebAPI.Karho.UpdateStatusUpdated(obj, "cabtreasure", "awKcEGZPt6NA7Mg9VTJbZPSZ8zTQRaDK");

                if (status.ToLower() == "completed")
                {

                    WebApi.GetFinalPriceProperties objPricebooking = new WebApi.GetFinalPriceProperties();

                    objPricebooking.karhoo_ref = objBooking.OnlineBookingId.ToStr();
                    objPricebooking.supplier_company = "cabtreasure";
                    objPricebooking.booking_id = objBooking.Id.ToStr();
                    objPricebooking.notes = objBooking.NotesString;
                    objPricebooking.status = objBooking.BookingStatus.StatusName;
                    objPricebooking.vehicle.vehicle_type = objDriver.Fleet_VehicleType.DefaultIfEmpty().VehicleType.ToStr();
                    objPricebooking.vehicle.vehicle_id = objBooking.Fleet_VehicleType.Id.ToStr();
                    objPricebooking.vehicle.vehicle_plate = objDriver.VehicleNo.ToStr();
                    double lat = (double)objDrvLocation.Longitude;
                    objPricebooking.vehicle.latitude = lat;
                    objPricebooking.vehicle.longitude = objDrvLocation.Latitude;
                    objPricebooking.vehicle.eta_minutes = objDrvLocation.EstimatedTimeLeft.ToStr();
                    objPricebooking.vehicle.make = objDriver.VehicleMake.ToStr();
                    objPricebooking.vehicle.model = objDriver.VehicleModel.ToStr();
                    objPricebooking.vehicle.color = objDriver.VehicleColor.ToStr();
                    objPricebooking.vehicle.driver_id = objDriver.DriverNo.ToStr();
                    string driverName = objDriver.DriverName;

                    string[] arr = driverName.Split(new char[] { ' ' });
                    objPricebooking.vehicle.driver_first_name = arr[0].ToStr();
                    if (arr.Length > 1)
                    {
                        objPricebooking.vehicle.driver_last_name = arr[1].ToStr();
                    }



                    objPricebooking.vehicle.driver_phone = objDriver.MobileNo.ToStr();
                    double faretotal = (double)objBooking.FareRate;


                    objPricebooking.total = faretotal;
                    objPricebooking.currency = "GBP";
                    objPricebooking.price_components = new List<PriceComponent>()
                        {
                            new PriceComponent {component_name = "base rate", value = (double)objBooking.FareRate, description = "Base Rate"},
                            new PriceComponent {component_name = "parking", value = 0.0, description = "Parking"},
                            new PriceComponent {component_name = "tax", value = 0.0, description = "Tax"}
                        };

                    WebAPI.Karho.UpdateTripFinalPrice(objPricebooking, "cabtreasure", "awKcEGZPt6NA7Mg9VTJbZPSZ8zTQRaDK");

                }

            }
            catch
            {

                rtn = false;
            }

            return rtn;

        }


        public static WebApi.Address GetWebApiAddress(string doorNo, string fullAddress, string postcode)
        {
            WebApi.Address address = new WebApi.Address();




            var addressArr = fullAddress.Split(new char[] { ',' });



            if (addressArr.Count() == 5)
            {
                address.street_name = addressArr[0].ToStr();
                address.postal_code = postcode;
                address.display_address = fullAddress;
                address.building_number = doorNo;
                address.region = addressArr[1].ToStr();
                address.city = addressArr[3].ToStr();
                address.country = addressArr[4].ToStr();


                if (address.postal_code.ToStr().Length == 0)
                    address.postal_code = addressArr[2].ToStr().ToUpper().Trim();


            }
            if (addressArr.Count() == 4)
            {
                address.street_name = addressArr[0].ToStr();
                address.postal_code = postcode;
                address.display_address = fullAddress;
                address.building_number = doorNo;
                address.region = addressArr[1].ToStr();
                address.city = addressArr[1].ToStr();
                address.country = addressArr[3].ToStr();


                if (address.postal_code.ToStr().Length == 0)
                    address.postal_code = addressArr[2].ToStr().ToUpper().Trim();

                //     Heathrow Airport, London, TW6 1EW, GB              

            }
            else
            {
                //  address.street_name = addressArr[0].ToStr();
                address.postal_code = postcode;
                address.display_address = fullAddress;
                address.building_number = doorNo;
                //      address.region = addressArr[1].ToStr();
                //    address.city = addressArr[1].ToStr();
                address.country = "GB";
                address.city = "LONDON";
                address.region = "GREATER LONDON";


                // if (address.postal_code.ToStr().Length == 0)
                //      address.postal_code = addressArr[2].ToStr().ToUpper().Trim();
            }



            return address;





            // Gastein Road, London, W6 8LU, london, GB



        }





        public static void UpdateThirdPartyJobStatus(Booking objBooking, long jobId, string status)
        {

            new Thread(delegate()
            {

                UpdateThirdPartyJob(objBooking, jobId, status);
            }).Start();

           // return rtn;
        }


        public static void UpdateKarhoData(Booking objBooking, long jobId, string status)
        {

            new Thread(delegate()
            {

                UpdateKarhoTripData(objBooking, jobId, status);
            }).Start();

            // return rtn;
        }

        public static void AddBookingLog(long jobId,string logMessage)
        {
            if (jobId == 0)
                return;

            using (TaxiDataContext db = new TaxiDataContext())
            {
                db.stp_BookingLog(jobId, AppVars.LoginObj.UserName.ToStr(), logMessage);


            }


        }

        public static void SaveSentEmail(string msg, string subject, string sentTo)
        {
            try
            {


                using (TaxiDataContext db = new TaxiDataContext())
                {

                    SentEmail obj = new SentEmail();
                    obj.SentOn = DateTime.Now;
                    obj.SentTo = sentTo.ToStr().Trim();
                    obj.EmailBody = msg;
                    obj.Subject = subject.ToStr();
                    obj.SentBy = AppVars.LoginObj.UserName.ToStr();

                    db.SentEmails.InsertOnSubmit(obj);
                    db.SubmitChanges();

                }
            }
            catch (Exception ex)
            {


            }


        }

        public static void SaveSentSMS(string msg, string toNumbers)
        {
            try
            {


                using (TaxiDataContext db = new TaxiDataContext())
                {
                   

                    SentSM objSms = new SentSM();
                    objSms.SentOn = DateTime.Now;
                    objSms.SentTo = toNumbers.ToStr().Trim();
                    objSms.SMSBody = msg;
                    objSms.SentBy = AppVars.LoginObj.UserName.ToStr();

                    db.SentSMs.InsertOnSubmit(objSms);




                    db.SubmitChanges();

                }
            }
            catch (Exception ex)
            {


            }

        }


        public static void SaveTemplate(string msg, string toNumbers)
        {
            try
            {

                if (msg.ToStr().Trim().Length > 6 || msg.ToStr().Trim().WordCount() > 1)
                {

                    using (Taxi_Model.TaxiDataContext context = new TaxiDataContext())
                    {
                        int c = context.Fleet_DriverTemplets.Where(w => w.Templets == msg).ToList().Count();

                        if (c == 0)
                        {
                            Fleet_DriverTemplet d = new Fleet_DriverTemplet();
                            d.Templets = msg;
                            d.SysPolicyId = 1;
                            context.Fleet_DriverTemplets.InsertOnSubmit(d);
                            context.SubmitChanges();


                        }



                    }
                }


                //using (TaxiDataContext db = new TaxiDataContext())
                //{


                //    SentSM objSms = new SentSM();
                //    objSms.SentOn = DateTime.Now;
                //    objSms.SentTo = toNumbers.ToStr().Trim();
                //    objSms.SMSBody = msg;
                //    objSms.SentBy = AppVars.LoginObj.UserName.ToStr();

                //    db.SentSMs.InsertOnSubmit(objSms);




                //    db.SubmitChanges();

                //}
            }
            catch (Exception ex)
            {


            }

        }


        public static bool ReCallPreBooking(long jobId, int driverId)
        {

            try
            {
                bool rtn = true;

                (new TaxiDataContext()).stp_RecoverPreJob(jobId, Enums.BOOKINGSTATUS.WAITING, driverId, "", AppVars.LoginObj.UserName.ToStr());




                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {
                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Pre Job>>" + jobId + "=2").Result.ToBool();
                    }

                }
                else
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Pre Job>>" + jobId + "=2").Result.ToBool();
                    }


                }

                return rtn;

            }
            catch (Exception ex)
            {


                //  ENUtils.ShowMessage(ex.Message);
                return false;

            }




        }



        public static string DecryptConnectionString(string connString)
        {
            return Cryptography.Decrypt(connString, "softeuroconnskey", true);

        }

        public static List<object[]> ShowCustomerBunch(IList list, string pkColumn)
        {

            Taxi_AppMain.frmCustomerBunch frm = new Taxi_AppMain.frmCustomerBunch(list, pkColumn, true);

            frm.ShowDialog();

            return frm.ListofData;

        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn == null) return null;
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null) return null;
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }






        public static string GetCLIFirstExtensions()
        {
            try
            {

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Service.xml"))
                {

                    string machineName = System.Environment.MachineName.ToStr().ToLower();

                    XmlDocument doc = new XmlDocument();
                    doc.Load(System.Windows.Forms.Application.StartupPath + "\\Service.xml");


                    return doc.GetElementsByTagName("extensions").OfType<XmlNode>().FirstOrDefault()
                                .ChildNodes.OfType<XmlNode>().Where(c => c.FirstChild.InnerText.ToStr().ToLower().Trim() == machineName)
                                .Select(c => c.LastChild.InnerText).FirstOrDefault<string>();


                }
                else
                    return null;

            }
            catch (Exception ex)
            {

                return null;

            }


        }




        public static void AddLog(string logMessage, string logFrom)
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.CommandTimeout = 5;
                    db.stp_AddLog(logMessage, AppVars.LoginObj.LoginName, logFrom);
                }
            }
            catch (Exception ex)
            {


            }
        }

        //public static TU CopyPropertiesTo<T, TU>( T source, TU dest)
        //{
        //    var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
        //    var destProps = typeof(TU).GetProperties()
        //            .Where(x => x.CanWrite)
        //            .ToList();

        //    foreach (var sourceProp in sourceProps)
        //    {
        //        if (destProps.Any(x => x.Name == sourceProp.Name))
        //        {
        //            var p = destProps.First(x => x.Name == sourceProp.Name);
        //            if (p.CanWrite && (p.PropertyType==typeof(object))==false)
        //            { // check if the property can be set or no.
        //                try
        //                {
        //                    p.SetValue(dest, sourceProp.GetValue(source, null), null);
        //                }
        //                catch
        //                {

        //                }
        //            }
        //        }

        //    }

        //    return dest;

        //}

        

        public static void ShowEmailForm(ReportViewer viewer, string fileTitle)
        {
            frmEmail frm = new frmEmail(viewer, fileTitle);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }
        public static void ShowEmailForm(ReportViewer viewer, string fileTitle, string email)
        {
            frmEmail frm = new frmEmail(viewer, fileTitle, email);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        public static void ShowEmailForm(ReportViewer viewer, string fileTitle, string email,bool showDialog)
        {
            frmEmail frm = new frmEmail(viewer, fileTitle, email);
            frm.StartPosition = FormStartPosition.CenterScreen;
            if (showDialog)
                frm.ShowDialog();
            else
                frm.Show();
        }





        public static bool SendPDATestMessage(string msg)
        {


            bool rtn = false;



            byte[] data = Encoding.UTF8.GetBytes(msg);
            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    try
                    {

                        tcpClient.SendTimeout = 3000;
                        tcpClient.ReceiveTimeout = 2000;


                        IPAddress ip = null;

                        if (IPAddress.TryParse(AppVars.objPolicyConfiguration.ListenerIP.ToStr(), out ip))
                            tcpClient.Connect(new IPEndPoint(ip, 1101));
                        else
                            tcpClient.Connect(AppVars.objPolicyConfiguration.ListenerIP.ToStr(), 1101);


                        tcpClient.GetStream().Write(data, 0, data.Length);

                        Byte[] inputBuffer = new Byte[200];
                        int bytes = tcpClient.GetStream().Read(inputBuffer, 0, inputBuffer.Length);
                        string dataValue = Encoding.UTF8.GetString(inputBuffer, 0, bytes);
                        tcpClient.Close();


                        if (dataValue.ToStr() == "ok" || dataValue.ToStr().StartsWith("ok"))
                        {
                            rtn = true;
                        }
                        else
                            rtn = false;
                    }
                    catch (Exception ex)
                    {

                       

                       

                    }

                }

              //  GC.Collect();
            }
            catch (Exception eex)
            {
                rtn = false;
            }

            return rtn;


        }

        public static void ClickACallDriver(int driverId,string numberj)
        {
            if (driverId > 0)
            {



                try
                {
                    if (numberj.ToStr().Trim().Length == 0)
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.CommandTimeout = 5;
                            db.DeferredLoadingEnabled = false;
                            numberj = db.Fleet_Drivers.Where(c => c.Id == driverId).Select(c => c.MobileNo).FirstOrDefault().ToStr();



                        }
                    }

                    ClickACall(numberj, "", "");
                }
                catch
                {


                }
            }       
        }

        public static void ClickACallCustomer(long jobId)
        {

            if (jobId > 0)
            {
                try
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.CommandTimeout = 5;
                        db.DeferredLoadingEnabled = false;
                        var obj = db.Bookings.Where(c => c.Id == jobId).Select(c => new { c.CustomerMobileNo, c.CustomerPhoneNo, c.SubcompanyId }).FirstOrDefault();

                        if (obj != null && (obj.CustomerMobileNo.ToStr().Trim().Length > 0 || obj.CustomerPhoneNo.ToStr().Trim().Length > 0))
                        {
                            string calleridToShow = "";
                            if (obj.SubcompanyId != null)
                            {
                                calleridToShow = db.Gen_SubCompanies.Where(c => c.Id == obj.SubcompanyId).Select(c => c.TelephoneNo).FirstOrDefault().ToStr();

                                ClickACall(obj.CustomerMobileNo, obj.CustomerPhoneNo, calleridToShow);


                                string msg = obj.CustomerMobileNo.ToStr().Trim();

                                if(string.IsNullOrEmpty(msg))
                                {
                                    msg = obj.CustomerPhoneNo.ToStr().Trim();
                                }

                                if(msg.ToStr().Trim().Length > 0)
                                   db.stp_BookingLog(jobId, AppVars.LoginObj.UserName.ToStr(), "Call To Customer - " + msg);
                            }
                        }

                    }

                }
                catch
                {

                }

            }

        }

        public static void ClickACall(string number, string number2, string calleridToShow)
        {

            (((frmMainMenu)(Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmMainMenu")))).ClickACall(number, number2, calleridToShow);

        }



        public static DateTime? LastKilledOn = null;

        public async static Task<bool> SendMessageToPDA(string msg)
        {
            frmMainMenu ObjfrmMainMenu = ((frmMainMenu)(Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmMainMenu")));
            HubConnection Connection = ObjfrmMainMenu.Connection;
            IHubProxy HubProxy = ObjfrmMainMenu.HubProxy;

            bool result;
            bool rtn = true;

            try
            {
                if (Connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
                {
                    Connection.Start();

                    Thread.Sleep(5000);
                }
                else if (Connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connecting
                            || Connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Reconnecting)
                {
                    Thread.Sleep(5000);
                    //     Connection.Stop();
                    //Connection.Start();
                }
                HubProxy.Invoke("MessageToPDA", msg);
                //  result = HubProxy.Invoke<bool>("MessageToPDA", msg).Result;

                //if (frmMainMenu.Acknowledgement == "ok")
                //    rtn = true;

                //   GC.Collect();
            }
            catch (Exception ex)
            {
                File.AppendAllText("tcpcatchlog.txt", DateTime.Now.ToStr() + ": Exception: " + ex.Message + " Inner Exception: " + ex.InnerException + Environment.NewLine);
                rtn = false;
            }

            return rtn;
        }

        public async static Task<bool> SendMessageToPDA(string msg, string driverId)
        {
            frmMainMenu ObjfrmMainMenu = ((frmMainMenu)(Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmMainMenu")));
            HubConnection Connection = ObjfrmMainMenu.Connection;
            IHubProxy HubProxy = ObjfrmMainMenu.HubProxy;

            bool result;
            bool rtn = true;

            try
            {
                if (Connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
                {
                    Connection.Start();

                    Thread.Sleep(5000);
                }
                else if (Connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Connecting
                            || Connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Reconnecting)
                {
                    Thread.Sleep(5000);
                    // Connection.Stop();
                    //  await Connection.Start();
                }

                result = HubProxy.Invoke<bool>("MessageToPDAByDriverID", msg, driverId).Result;

             //   if (frmMainMenu.Acknowledgement == "ok")
                 //   rtn = true;

             //   GC.Collect();
            }
            catch (Exception ex)
            {
                File.AppendAllText("tcpcatchlog.txt", DateTime.Now.ToStr() + ": Exception: " + ex.Message + " Inner Exception: " + ex.InnerException + Environment.NewLine);
                rtn = false;
            }

            return rtn;
        }

        public static bool SendPDAMessage(string msg)
        {


            bool rtn = false;     

          
           
            byte[] data = Encoding.UTF8.GetBytes(msg);
            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    try
                    {

                        tcpClient.SendTimeout = 3000;
                        tcpClient.ReceiveTimeout = 2000;


                        IPAddress ip = null;

                        if (IPAddress.TryParse(AppVars.objPolicyConfiguration.ListenerIP.ToStr(), out ip))
                            tcpClient.Connect(new IPEndPoint(ip, 1101));
                        else
                            tcpClient.Connect(AppVars.objPolicyConfiguration.ListenerIP.ToStr(), 1101);


                        tcpClient.GetStream().Write(data, 0, data.Length);

                        Byte[] inputBuffer = new Byte[200];
                        int bytes = tcpClient.GetStream().Read(inputBuffer, 0, inputBuffer.Length);
                        string dataValue = Encoding.UTF8.GetString(inputBuffer, 0, bytes);
                        tcpClient.Close();
                     

                        if (dataValue.ToStr() == "ok" || dataValue.ToStr().StartsWith("ok"))
                        {
                            rtn = true;
                            LastKilledOn = DateTime.Now;
                        }
                        else
                            rtn = false;




                    }
                    catch (Exception ex)
                    {

                      

                        try
                        {
                           

                            if (ex.Message.ToStr().ToLower().Contains("no connection") || ex.Message.ToStr().ToLower().Contains("did not properly respond"))
                            {
                                File.AppendAllText("excep_pda.txt", DateTime.Now.ToStr() + ":" + ex.Message + Environment.NewLine);

                              
                                if (System.Configuration.ConfigurationManager.AppSettings["cankilltcp"]!=null && System.Configuration.ConfigurationManager.AppSettings["cankilltcp"].ToStr() == "true")
                                {

                                    if (LastKilledOn == null || DateTime.Now.Subtract(LastKilledOn.Value).TotalSeconds>=10)
                                    {

                                        if (AppVars.enableSMSService.ToBool())
                                        {
                                            try
                                            {
                                                bool isKilled = false;

                                                Process tcplistener = Process.GetProcesses().FirstOrDefault(c => c.ProcessName.ToLower().Contains("tcs_tcplistener"));
                                                if (tcplistener != null)
                                                {
                                                  //  tcplistener.Close();
                                                    tcplistener.Kill();
                                                 
                                                    isKilled = true;
                                                }
                                                else
                                                {
                                                    isKilled = true;

                                                }

                                                if (isKilled)
                                                {

                                                    string path = System.Windows.Forms.Application.StartupPath.Replace("Treasure Cab System", "MapAppSetup") + "\\TCS_TcpListener.exe";

                                                    if (File.Exists(path))
                                                    {
                                                        Thread.Sleep(1000);
                                                        Process.Start(path);

                                                        File.AppendAllText("kill_tcplistener.txt", DateTime.Now.ToStr() + ":" + Environment.NewLine);
                                                        LastKilledOn = DateTime.Now;
                                                        Thread.Sleep(2000);
                                                    }
                                                }
                                            }
                                            catch (Exception ex2)
                                            {


                                            }
                                        }
                                        else
                                        {
                                            new BroadcasterData().BroadCastToAll("**internalmessage>>tcprestart>>true>>111");
                                            LastKilledOn = DateTime.Now;
                                            Thread.Sleep(2000);

                                        }
                                    }
                                }
                            //    }
                            }

                        }
                        catch
                        {


                        }

                        rtn = false;
                        using (TcpClient tcpClient2 = new TcpClient())
                        {
                            try
                            {

                                tcpClient2.SendTimeout = 3000;
                                tcpClient2.ReceiveTimeout = 2000;


                                IPAddress ip = null;

                                if (IPAddress.TryParse(AppVars.objPolicyConfiguration.ListenerIP.ToStr(), out ip))
                                    tcpClient2.Connect(new IPEndPoint(ip, 1101));
                                else
                                    tcpClient2.Connect(AppVars.objPolicyConfiguration.ListenerIP.ToStr(), 1101);




                                tcpClient2.GetStream().Write(data, 0, data.Length);

                                Byte[] inputBuffer = new Byte[200];
                                int bytes = tcpClient2.GetStream().Read(inputBuffer, 0, inputBuffer.Length);
                                string dataValue = Encoding.UTF8.GetString(inputBuffer, 0, bytes);
                                tcpClient2.Close();


                                //   GC.Collect();

                                if (dataValue.ToStr() == "ok" || dataValue.ToStr().StartsWith("ok"))
                                {
                                    rtn = true;
                                }
                                else
                                    rtn = false;


                                try
                                {

                                    File.AppendAllText("tcpcatchlog.txt", DateTime.Now.ToStr() + ":" + ex.Message + Environment.NewLine);


                              

                                }
                                catch
                                {


                                }
                            }
                            catch (Exception ee)
                            {

                                rtn = false;

                            }

                        }


                        new BroadcasterData().BroadCastToLocalIPPort("**" + msg, 3529);

                    }

                }

                GC.Collect();
            }
            catch (Exception eex)
            {
                rtn = false;
            }

            return rtn;

         
        }


        public static bool SendSockMessage(string msg,string ipVal,int port)
        {


            bool rtn = false;





            byte[] data = Encoding.UTF8.GetBytes(msg);
            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    try
                    {

                        tcpClient.SendTimeout = 3000;
                        tcpClient.ReceiveTimeout = 2000;


                        IPAddress ip = null;

                        if (IPAddress.TryParse(ipVal, out ip))
                            tcpClient.Connect(new IPEndPoint(ip, port));
                        else
                            tcpClient.Connect(ipVal, port);


                        tcpClient.GetStream().Write(data, 0, data.Length);

                        Byte[] inputBuffer = new Byte[200];
                        int bytes = tcpClient.GetStream().Read(inputBuffer, 0, inputBuffer.Length);
                        string dataValue = Encoding.UTF8.GetString(inputBuffer, 0, bytes);
                        tcpClient.Close();


                        if (dataValue.ToStr() == "ok" || dataValue.ToStr().StartsWith("ok"))
                        {
                            rtn = true;
                        }
                        else
                            rtn = false;





                    }
                    catch (Exception ex)
                    {



                     

                     

                        rtn = false;
                        using (TcpClient tcpClient2 = new TcpClient())
                        {
                            try
                            {

                                tcpClient2.SendTimeout = 3000;
                                tcpClient2.ReceiveTimeout = 2000;


                                IPAddress ip = null;

                                if (IPAddress.TryParse(ipVal, out ip))
                                    tcpClient2.Connect(new IPEndPoint(ip, port));
                                else
                                    tcpClient2.Connect(ipVal, port);




                                tcpClient2.GetStream().Write(data, 0, data.Length);

                                Byte[] inputBuffer = new Byte[200];
                                int bytes = tcpClient2.GetStream().Read(inputBuffer, 0, inputBuffer.Length);
                                string dataValue = Encoding.UTF8.GetString(inputBuffer, 0, bytes);
                                tcpClient2.Close();


                                //   GC.Collect();

                                if (dataValue.ToStr() == "ok" || dataValue.ToStr().StartsWith("ok"))
                                {
                                    rtn = true;
                                }
                                else
                                    rtn = false;


                              
                            }
                            catch (Exception ee)
                            {

                                rtn = false;

                            }

                        }


                      //  new BroadcasterData().BroadCastToLocalIPPort("**" + msg, 3529);

                    }

                }

                GC.Collect();
            }
            catch (Exception eex)
            {
                rtn = false;
            }

            return rtn;


        }



        private void ForceRestartTcp()
        {




        }




        public static string GetSharedDocumentsPath()
        {
            string fullPath = GetSharedNetworkPath() + "\\Taxi\\";

            return fullPath;

        }


        public static void SendEmail(string subject, string Emailmessage, string FromEmail, string ToEmail)
        {

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            ///    NetworkCredential mailAuthentication = new NetworkCredential("eurosofttech@hotmail.com", "123123123A");
            NetworkCredential mailAuthentication = new NetworkCredential("danish@eurosofttech.co.uk", "Admin1234");

            message.To.Add(new MailAddress(ToEmail));
            message.From = new MailAddress(FromEmail);
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = Emailmessage;
            smtp.UseDefaultCredentials = false;
            //       smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //   smtp.Host = "smtp.live.com";
            smtp.Host = "smtp.eurosofttech.co.uk";
            smtp.Port = 587;
            smtp.Credentials = mailAuthentication;
            smtp.Send(message);


        }



        public static decimal GetSurchargeRate(string postCode, ref bool IsAmountWise)
        {
            decimal value = 0.00m;
            string[] splitPostCode = postCode.Split(new char[] { ' ' });
            if (splitPostCode.Count() > 0)
            {
                string postcode = CheckIfSpecialPostCode(splitPostCode[0].Trim().ToUpper());

                Gen_SysPolicy_SurchargeRate obj = General.GetObject<Gen_SysPolicy_SurchargeRate>(c => c.SysPolicyId != null && c.PostCode.Trim().ToLower() == postcode.ToLower());

                if (obj != null)
                {

                    IsAmountWise = obj.IsAmountWise.ToBool();


                    if (IsAmountWise)
                    {
                        value = obj.Amount.ToDecimal();
                    }
                    else
                    {
                        value = obj.Percentage.ToDecimal();
                    }

                }
            }

            return value;

        }


        public static decimal GetSurchargeRate(string postCode,int? ZoneId,DateTime dt, ref bool IsAmountWise)
        {
            decimal value = 0.00m;

            try
            {
                string[] splitPostCode = postCode.Split(new char[] { ' ' });
                if (splitPostCode.Count() > 0)
                {
                    string postcode = CheckIfSpecialPostCode(splitPostCode[0].Trim().ToUpper());

                    Gen_SysPolicy_SurchargeRate obj = null;

                    if (ZoneId.ToInt() == 0)
                    {
                        obj = General.GetObject<Gen_SysPolicy_SurchargeRate>(c => c.SysPolicyId != null && c.PostCode.Trim().ToLower() == postcode.ToLower());

                    }
                    else
                    {
                        obj = General.GetObject<Gen_SysPolicy_SurchargeRate>(c => c.SysPolicyId != null && (c.PostCode.Trim().ToLower() == postcode.ToLower() || c.zoneid == ZoneId));


                    }
                    if (obj != null)
                    {

                        if (obj.EnableSurcharge.ToBool())
                        {
                            //if(dt==null)
                            //   dt = DateTime.Now;


                            bool applySurg = false;
                            if (obj.CriteriaBy.ToInt() == 1)
                            {
                                if (dt >= obj.ApplicableFromDateTime && dt <= obj.ApplicableToDateTime)
                                    applySurg = true;

                            }
                            else if (obj.CriteriaBy.ToInt() == 2)
                            {
                                if (dt.ToDate() >= obj.ApplicableFromDateTime.ToDate() && dt.ToDate() <= obj.ApplicableToDateTime.ToDate())
                                    applySurg = true;

                            }
                            else if (obj.CriteriaBy.ToInt() == 3)
                            {
                                int day = dt.Date.DayOfWeek.ToInt();


                                if ((day >= obj.ApplicableFromDay.ToInt() && day <= obj.ApplicableToDay.ToInt())
                                    &&
                                       (dt.TimeOfDay >= obj.ApplicableFromDateTime.ToDateTime().TimeOfDay && dt.TimeOfDay <= obj.ApplicableToDateTime.ToDateTime().TimeOfDay))
                                    applySurg = true;

                            }


                            if (applySurg)
                            {
                                IsAmountWise = obj.IsAmountWise.ToBool();


                                if (IsAmountWise)
                                {
                                    value = obj.Amount.ToDecimal();
                                }
                                else
                                {
                                    value = obj.Percentage.ToDecimal();
                                }
                            }
                        }

                    }
                }


            }
            catch
            {

            }

            return value;

        }










        public static decimal GetFareRate(int subCompanyId, int companyId, int vehicleTypeId, int tempFromLocId, int tempToLocId, string tempFromPostCode
                   , string tempToPostCode, ref string errorMsg, ref List<decimal> milesList, bool IsVia, bool CanCheckZoneWise, DateTime? pickupTime, ref decimal deadMileage, int fromLocTypeId, int toLocTypeId, ref bool companyFareExist, ref string estimatedTime)
        {
            string miles = "";
            decimal rtnFare = 0.00m;
            string fromVal = tempFromPostCode;
            string toVal = tempToPostCode;


            bool surchargeRateFromAmountWise = false;
            bool surchargeRateToAmountWise = false;

            decimal surchargeRateFrom = 0.00m;
            decimal surchargeRateTo = 0.00m;

            bool IsMoreFareWise = false;
            int actualVehicleTypeId = vehicleTypeId;
            try
            {

                if (tempFromPostCode.Length > 0 && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                    surchargeRateFrom = GetSurchargeRate(tempFromPostCode, ref surchargeRateFromAmountWise);
                }

                if (tempToPostCode.Length > 0 && (toLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    tempToPostCode = General.GetPostCodeMatch(tempToPostCode);
                    surchargeRateTo = GetSurchargeRate(tempToPostCode, ref surchargeRateToAmountWise);
                }


                string fromSingleHalfPostCode = string.Empty;
                string fromHalfPostCode = string.Empty;
                string startFromPostCode = "";
                //if (tempFromLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempFromPostCode) && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN))
                    {
                        string[] fromArr = tempFromPostCode.Split(new char[] { ' ' });
                        startFromPostCode = fromArr[0];

                        fromHalfPostCode = startFromPostCode;

                        startFromPostCode = General.CheckIfSpecialPostCode(startFromPostCode);

                        fromSingleHalfPostCode = fromArr[0] + " "+ fromArr[1][0];

                    }
                 
             //   }


                string ToSingleHalfPostCode = string.Empty;
                string toHalfPostCode = string.Empty;
                string startToPostCode = "";
                //if (tempToLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempToPostCode) && (toLocTypeId != Enums.LOCATION_TYPES.TOWN))
                    {
                        string[] toArr = tempToPostCode.Split(new char[] { ' ' });

                        startToPostCode = toArr[0];
                        toHalfPostCode = startToPostCode;
                        startToPostCode = General.CheckIfSpecialPostCode(startToPostCode);

                        if(toArr.Count()>1)
                        ToSingleHalfPostCode = toArr[0] + " " + toArr[1][0];
                    }
                //}

                int defaultVehicleId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();
                List<Fare_ChargesDetail> list = null;
                if ((IsVia==false && milesList.Count==0) || (IsVia && AppVars.objPolicyConfiguration.ViaPointFareCalculationType.ToBool()==false))
                {


                    if (vehicleTypeId != defaultVehicleId)
                    {

                        if (AppVars.objPolicyConfiguration.ApplyPercentageWiseFareOn.ToBool())
                        {

                            if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                                                            ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginId == tempFromLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))))
                                                                  && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationId == tempToLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))))))

                                                                  || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (c.OriginId == tempToLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode)))))
                                                                  && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (c.DestinationId == tempFromLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))))))
                                                                     )
                                                               && c.Fare.VehicleTypeId == defaultVehicleId
                                                                && (c.Fare.CompanyId == companyId || companyId == 0)).Count() > 0)

                                                          )
                            {

                                vehicleTypeId = defaultVehicleId;
                                IsMoreFareWise = true;
                            }


                        }
                        else
                        {


                            if (
                                //(General.GetQueryable<Fare_ChargesDetail>(c =>


                                //                                      ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                //                                            && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                //                                            || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                //                                            && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                //                                               )
                                //                                         && c.Fare.VehicleTypeId == vehicleTypeId
                                //                                          && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0)

                                (General.GetQueryable<Fare_ChargesDetail>(c =>


                                                            ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginId == tempFromLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))))
                                                                  && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationId == tempToLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))))))

                                                                  || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (c.OriginId == tempToLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode)))))
                                                                  && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (c.DestinationId == tempFromLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))))))
                                                                     )
                                                               && c.Fare.VehicleTypeId == vehicleTypeId
                                                                && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0)

                                && (General.GetQueryable<Fare_OtherCharge>(c => c.Fare.VehicleTypeId == vehicleTypeId
                                                                              && (c.Fare.CompanyId == companyId || companyId == 0 || c.Fare.CompanyId == null)).Count() == 0))
                            {

                                vehicleTypeId = defaultVehicleId;
                                IsMoreFareWise = true;
                            }
                        }


                    }





                   



                    if (list == null || (list != null && list.Count() == 0))
                    {

                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()))) || c.OriginId == tempFromLocId)
                                                                       && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()) || (c.ToAddress.ToUpper().EndsWith(tempToPostCode))   )) || c.DestinationId == tempToLocId))

                                                                          )

                                                                     && c.Fare.VehicleTypeId == vehicleTypeId

                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                            //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                      ).ToList();

                    }

                    if (list == null || (list != null && list.Count() == 0))
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                    && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                    || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                    && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                  && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();


                        if (list != null && list.Count > 0)
                        {
                            errorMsg = "Reverse found";

                        }

                    }

                    if ((tempFromLocId != 0 || tempToLocId != 0) && (list == null || (list != null && list.Count() == 0)))
                    {
                        if (tempFromLocId > 0)
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                   && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                   || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                   && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                      )

                                                                 && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                  ).ToList();

                        }

                        if ((list == null || list.Count == 0) && tempToLocId > 0)
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                    || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                    && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();

                        }



                        if ((list == null || list.Count == 0))
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                    || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();

                        }

                        if (list != null && list.Count > 0)
                        {
                            errorMsg = "Reverse found";

                        }


                    }

                }
                
                //if ((tempFromLocId == 0 && string.IsNullOrEmpty(startFromPostCode)) || (tempToLocId == 0 && string.IsNullOrEmpty(startToPostCode)))
                //    obj = null;


                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 2)
                {
                    tempFromPostCode = fromVal;
                    tempToPostCode = toVal;
                }


                stp_getCoordinatesByAddressResult pickupCoord = null;
                stp_getCoordinatesByAddressResult destCoord = null;

                try
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        pickupCoord = db.stp_getCoordinatesByAddress(fromVal, tempFromPostCode).FirstOrDefault();
                        destCoord = db.stp_getCoordinatesByAddress(toVal, tempToPostCode).FirstOrDefault();
                    }
                }
                catch
                {


                }


                string originString = string.Empty;
                string destString = string.Empty;
                if (pickupCoord != null && pickupCoord.Latitude != null && pickupCoord.Latitude != 0)
                {
                    originString = pickupCoord.Latitude + "," + pickupCoord.Longtiude;
                }

                if (destCoord != null && destCoord.Latitude != null && destCoord.Latitude != 0)
                {
                    destString = destCoord.Latitude + "," + destCoord.Longtiude;
                }


                //if (IsVia || milesList.Count > 0)
                //    miles = CalculateDistance(tempFromPostCode, tempToPostCode, ref estimatedTime, true).ToStr();
                //else
                //    miles = CalculateDistance(tempFromPostCode, tempToPostCode, ref estimatedTime).ToStr();


                if (IsVia || milesList.Count > 0)
                    miles = CalculateDistance(originString, destString, ref estimatedTime, true).ToStr();
                else
                    miles = CalculateDistance(originString, destString, ref estimatedTime).ToStr();




                milesList.Add(miles.ToDecimal());


                Fare_ChargesDetail obj = null;

                if (list != null)
                {
                    if (companyId != 0)
                    {
                        if (list.Count(c => c.Fare.CompanyId == companyId) > 0)
                        {
                            obj = list.FirstOrDefault(c => c.Fare.CompanyId == companyId);

                            companyFareExist = true;
                        }
                        else
                        {

                            if (General.GetQueryable<Taxi_Model.Fare>(c => c.CompanyId == companyId).Count() == 0)
                            {
                                obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                                companyFareExist = true;

                            }


                        }
                    }
                    else
                    {
                        obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                    }               

                }


                if (obj != null && AppVars.objPolicyConfiguration.PreferredMileageFares.ToBool()==false)
                {

                    rtnFare = obj.Rate.ToDecimal();
                    deadMileage = 0;
                }
                else
                {

                    if ((string.IsNullOrEmpty(tempFromPostCode) && string.IsNullOrEmpty(originString)) || (string.IsNullOrEmpty(tempToPostCode) && string.IsNullOrEmpty(destString)))
                    {
                        errorMsg = "Error";
                        return 0;
                    }
                  


                    if (IsVia)
                        return rtnFare;

                    if (deadMileage > 0)
                    {

                        string fromBasePostCode = General.GetPostCodeMatch(AppVars.objPolicyConfiguration.BaseAddress.ToStr().ToUpper().Trim());
                        decimal deadMileageDistance = General.CalculateDistance(fromBasePostCode, tempFromPostCode);

                        deadMileage = deadMileageDistance - deadMileage;

                        if (deadMileage < 0)
                            deadMileage = 0;


                        milesList.Add(deadMileage.ToDecimal());

                    }

                    // Calculate Fare Mileage Wise                
                    //  ISingleResult<ClsFares> objFare = General.SP_CalculateFares(vehicleTypeId.ToIntorNull(), companyId.ToIntorNull(), milesList.Sum().ToStr(), pickupTime);
                    decimal totalMiles = milesList.Sum();


                  

                   // var objFare = new TaxiDataContext().stp_CalculateGeneralFares(vehicleTypeId, companyId, totalMiles, pickupTime);
                    if (AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool() && AppVars.objPolicyConfiguration.FareMeterType.ToInt() != 2)
                    {
                        pickupTime = string.Format("{0:dd/MM/yyyy HH:mm}", new DateTime(1900, 1, 1, 0, 0, 0).ToDate() + pickupTime.Value.TimeOfDay).ToDateTime();

                    }


                  ISingleResult<stp_CalculateGeneralFaresBySubCompanyResult> objFare   = new TaxiDataContext().stp_CalculateGeneralFaresBySubCompany(vehicleTypeId, companyId, totalMiles, pickupTime, subCompanyId);
                   

                    if (objFare != null)
                    {
                        var f = objFare.FirstOrDefault();

                        if ((f.Result==null || f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                        {


                            if (AppVars.objPolicyConfiguration.PreferredMileageFares.ToBool() == false || obj == null ||
                                (f.totalFares.ToDecimal() > obj.Rate.ToDecimal() && (fromLocTypeId!=Enums.LOCATION_TYPES.AIRPORT && toLocTypeId!=Enums.LOCATION_TYPES.AIRPORT) ))
                            {
                                rtnFare = f.totalFares.ToDecimal();

                                companyFareExist = f.CompanyFareExist.ToBool();
                            }
                            else
                            {

                                if (obj != null)
                                    rtnFare = obj.Rate.ToDecimal();
                                else
                                {
                                    rtnFare = f.totalFares.ToDecimal();
                                    companyFareExist = f.CompanyFareExist.ToBool();
                                }

                            }                    
                            
                        }
                        else
                            errorMsg = "Error";



                    }
                    else
                        errorMsg = "Error";




                    

                    if (deadMileage > 0)
                    {
                        deadMileage = 0;

                        if (milesList.Count > 1)
                            milesList.RemoveAt(1);

                    }

                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                    {

                        decimal startRateTillMiles = General.GetObject<Fleet_VehicleType>(c => c.Id == vehicleTypeId).DefaultIfEmpty().StartRateValidMiles.ToDecimal();
                        if (startRateTillMiles > 0 && totalMiles > startRateTillMiles)
                        {

                            //  rtnFare = Math.Ceiling((rtnFare);
                            rtnFare = Math.Ceiling(rtnFare);
                        }
                    }
                    else
                    {

                        decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                        if (roundUp > 0)
                        {
                            // fareVal = (decimal)Math.Ceiling(fareVal / roundUp) * roundUp;

                            rtnFare = (decimal)Math.Ceiling(rtnFare / roundUp) * roundUp;

                        }   


                       // rtnFare = (decimal)Math.Ceiling(rtnFare / 0.5m) * 0.5m;


                    }

                }


              



                if (IsMoreFareWise)
                {

                    decimal AddedAmount = 0.00m;
                    string op = string.Empty;

                    Gen_SysPolicy_FaresSetting objFare = null;

                    //if (companyId != null)
                    //{
                    //    objFare = General.GetObject<Gen_SysPolicy_FaresSetting>(c => c.SysPolicyId != null && c.VehicleTypeId == actualVehicleTypeId && (c.IsCompanyWise!=null && c.IsCompanyWise==true));
                    //}
                    //else
                    //{

                 //   if(objFare==null)
                        objFare = General.GetObject<Gen_SysPolicy_FaresSetting>(c => c.SysPolicyId != null && c.VehicleTypeId == actualVehicleTypeId );
                  //  }
                    if (objFare != null)
                    {
                        op = objFare.Operator.ToStr();


                        if (objFare.IsAmountWise == false)
                        {
                            AddedAmount = (rtnFare * objFare.Percentage.ToDecimal()) / 100;
                        }
                        else
                        {
                            AddedAmount = objFare.Amount.ToDecimal();

                        }

                    }


                    switch (op)
                    {
                        case "+":
                            rtnFare = rtnFare + AddedAmount;
                            break;

                        case "-":
                            rtnFare = rtnFare - AddedAmount;
                            break;

                        default:
                            rtnFare = rtnFare + AddedAmount;
                            break;
                    }



                }


                if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == false)
                {

                    decimal totalSurchargePercentage = surchargeRateFrom + surchargeRateTo;

                    decimal fareSurchargePercent = (rtnFare * totalSurchargePercentage) / 100;
                    rtnFare = rtnFare + fareSurchargePercent;

                }
                else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == true)
                {

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }
                else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == false)
                {
                    surchargeRateTo = (rtnFare * surchargeRateTo) / 100;

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }
                else if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == true)
                {
                    surchargeRateFrom = (rtnFare * surchargeRateFrom) / 100;

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }



            }
            catch (Exception ex)
            {


             //   MessageBox.Show(ex.Message);
            }
            return rtnFare;
        }

        public static decimal GetFareRate(int subCompanyId, int companyId, int vehicleTypeId, int tempFromLocId, int tempToLocId, string tempFromPostCode
                      , string tempToPostCode, ref string errorMsg, ref List<decimal> milesList, bool IsVia, bool CanCheckZoneWise, DateTime? pickupTime, ref decimal deadMileage, int fromLocTypeId, int toLocTypeId, ref bool companyFareExist, ref string estimatedTime,ref decimal companyPrice,int? fromZoneId,int? toZoneId)
        {
            string miles = "";
            decimal rtnFare = 0.00m;
            string fromVal = tempFromPostCode;
            string toVal = tempToPostCode;


            bool surchargeRateFromAmountWise = false;
            bool surchargeRateToAmountWise = false;

            decimal surchargeRateFrom = 0.00m;
            decimal surchargeRateTo = 0.00m;

            bool IsMoreFareWise = false;
            int actualVehicleTypeId = vehicleTypeId;
            try
            {

                if ((tempFromPostCode.Length > 0 || fromZoneId.ToInt()>0) && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                    surchargeRateFrom = GetSurchargeRate(tempFromPostCode,fromZoneId,pickupTime.ToDateTime(), ref surchargeRateFromAmountWise);
                }

                if ((tempToPostCode.Length > 0 || toZoneId.ToInt()>0) && (toLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    tempToPostCode = General.GetPostCodeMatch(tempToPostCode);
                    surchargeRateTo = GetSurchargeRate(tempToPostCode,toZoneId, pickupTime.ToDateTime(), ref surchargeRateToAmountWise);
                }


                string fromSingleHalfPostCode = string.Empty;
                string fromHalfPostCode = string.Empty;
                string startFromPostCode = "";
                //if (tempFromLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempFromPostCode) && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    string[] fromArr = tempFromPostCode.Split(new char[] { ' ' });
                    startFromPostCode = fromArr[0];

                    fromHalfPostCode = startFromPostCode;

                    startFromPostCode = General.CheckIfSpecialPostCode(startFromPostCode);

                    if (fromArr.Count() > 1)
                    {
                        fromSingleHalfPostCode = fromArr[0] + " " + fromArr[1][0];
                    }
                   
                }

                //   }


                string ToSingleHalfPostCode = string.Empty;
                string toHalfPostCode = string.Empty;
                string startToPostCode = "";
                //if (tempToLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempToPostCode) && (toLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    string[] toArr = tempToPostCode.Split(new char[] { ' ' });

                    startToPostCode = toArr[0];
                    toHalfPostCode = startToPostCode;
                    startToPostCode = General.CheckIfSpecialPostCode(startToPostCode);

                    if (toArr.Count() > 1)
                        ToSingleHalfPostCode = toArr[0] + " " + toArr[1][0];
                }
                //}

                int defaultVehicleId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();
                List<Fare_ChargesDetail> list = null;
                if ((IsVia == false && milesList.Count == 0) || (IsVia && AppVars.objPolicyConfiguration.ViaPointFareCalculationType.ToBool() == false))
                {


                    if (vehicleTypeId != defaultVehicleId)
                    {

                        if (AppVars.objPolicyConfiguration.ApplyPercentageWiseFareOn.ToBool())
                        {

                            if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                                                            ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginId == tempFromLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))))
                                                                  && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationId == tempToLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))))))

                                                                  || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (c.OriginId == tempToLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode)))))
                                                                  && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (c.DestinationId == tempFromLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))))))
                                                                     )
                                                               && c.Fare.VehicleTypeId == defaultVehicleId
                                                                && (c.Fare.CompanyId == companyId || companyId == 0)).Count() > 0)

                                                          )
                            {

                                vehicleTypeId = defaultVehicleId;
                                IsMoreFareWise = true;
                            }


                        }
                        else
                        {


                            using (TaxiDataContext db = new TaxiDataContext())
                            {

                                if (tempFromPostCode.Length > 0 && tempToPostCode.Length > 0 &&

                                       (db.Fare_ChargesDetails.Where(c =>


                                                                
                                                                    c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0)

                                    && 
                                    (db.Fare_OtherCharges.Where(c => c.Fare.VehicleTypeId == vehicleTypeId
                                                                                  && (c.Fare.CompanyId == companyId || companyId == 0 || c.Fare.CompanyId == null)).Count() == 0))


                                //(General.GetQueryable<Fare_ChargesDetail>(c =>


                                //                            ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginId == tempFromLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))))
                                //                                  && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationId == tempToLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))))))

                                //                                  || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (c.OriginId == tempToLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode)))))
                                //                                  && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (c.DestinationId == tempFromLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))))))
                                //                                     )
                                //                               && c.Fare.VehicleTypeId == vehicleTypeId
                                //                                && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0)

                                //&& (General.GetQueryable<Fare_OtherCharge>(c => c.Fare.VehicleTypeId == vehicleTypeId
                                //                                              && (c.Fare.CompanyId == companyId || companyId == 0 || c.Fare.CompanyId == null)).Count() == 0))
                                {

                                    vehicleTypeId = defaultVehicleId;
                                    IsMoreFareWise = true;
                                }
                                else if (tempFromPostCode.Length == 0 || tempToPostCode.Length == 0)
                                {
                                    if ((db.Fare_OtherCharges.Where(c => c.Fare.VehicleTypeId == vehicleTypeId
                                                                                  && (c.Fare.CompanyId == companyId || companyId == 0 || c.Fare.CompanyId == null)).Count() == 0))
                                    {
                                        vehicleTypeId = defaultVehicleId;
                                        IsMoreFareWise = true;

                                    }

                                }
                            }
                        }


                    }









                    if ((list == null || (list != null && list.Count() == 0)) && tempFromPostCode.Length > 0 && tempToPostCode.Length > 0)
                    {

                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()) || (c.FromAddress.ToUpper().EndsWith(tempFromPostCode)))) || c.OriginId == tempFromLocId)
                                                                       && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()) || (c.ToAddress.ToUpper().EndsWith(tempToPostCode)))) || c.DestinationId == tempToLocId))

                                                                          )

                                                                     && c.Fare.VehicleTypeId == vehicleTypeId

                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                            //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                      ).ToList();

                    }

                    if ((list == null || (list != null && list.Count() == 0)) && tempFromPostCode.Length > 0 && tempToPostCode.Length > 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (tempFromLocId == 0 && (c.FromAddress.ToUpper().EndsWith(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                    && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (tempToLocId == 0 && (c.ToAddress.ToUpper().EndsWith(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                    || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (tempToLocId == 0 && (c.FromAddress.ToUpper().EndsWith(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                    && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (tempFromLocId == 0 && (c.ToAddress.ToUpper().EndsWith(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                  && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();


                        if (list != null && list.Count > 0)
                        {
                            errorMsg = "Reverse found";

                        }

                    }

                    if ((tempFromLocId != 0 || tempToLocId != 0) && (list == null || (list != null && list.Count() == 0)))
                    {
                        if (tempFromLocId > 0)
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                   && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                   || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                   && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                      )

                                                                 && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                  ).ToList();

                        }

                        if ((list == null || list.Count == 0) && tempToLocId > 0)
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                    || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                    && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();

                        }



                        if ((list == null || list.Count == 0))
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                    || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();

                        }

                        if (list != null && list.Count > 0)
                        {
                            errorMsg = "Reverse found";

                        }


                    }

                }
                else
                {

                    if((IsVia == false && milesList.Count > 0 && AppVars.objPolicyConfiguration.ViaPointFareCalculationType.ToBool() && vehicleTypeId!=defaultVehicleId))
                    {
                        vehicleTypeId = defaultVehicleId;
                        IsMoreFareWise = true;

                    }


                }

                //if ((tempFromLocId == 0 && string.IsNullOrEmpty(startFromPostCode)) || (tempToLocId == 0 && string.IsNullOrEmpty(startToPostCode)))
                //    obj = null;


                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 2)
                {
                    tempFromPostCode = fromVal;
                    tempToPostCode = toVal;
                }

               


                stp_getCoordinatesByAddressResult pickupCoord = null;
                stp_getCoordinatesByAddressResult destCoord = null;

                try
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        pickupCoord = db.stp_getCoordinatesByAddress(fromVal,General.GetPostCodeMatch( tempFromPostCode)).FirstOrDefault();
                        destCoord = db.stp_getCoordinatesByAddress(toVal,General.GetPostCodeMatch(tempToPostCode)).FirstOrDefault();
                    }
                }
                catch
                {


                }
              


                string originString = string.Empty;
                string destString = string.Empty;
                if (pickupCoord != null && pickupCoord.Latitude != null && pickupCoord.Latitude != 0)
                {
                    originString = pickupCoord.Latitude + "," +pickupCoord.Longtiude;
                }

                if (destCoord != null && destCoord.Latitude != null && destCoord.Latitude != 0)
                {
                    destString = destCoord.Latitude + "," + destCoord.Longtiude;
                }


                //if (IsVia || milesList.Count > 0)
                //    miles = CalculateDistance(tempFromPostCode, tempToPostCode, ref estimatedTime, true).ToStr();
                //else
                //    miles = CalculateDistance(tempFromPostCode, tempToPostCode, ref estimatedTime).ToStr();


                if (IsVia || milesList.Count > 0)
                    miles = CalculateDistance(originString, destString, ref estimatedTime, true).ToStr();
                else
                    miles = CalculateDistance(originString, destString, ref estimatedTime).ToStr();




                milesList.Add(miles.ToDecimal());


                Fare_ChargesDetail obj = null;

                if (list != null)
                {
                    if (companyId != 0)
                    {
                        if (list.Count(c => c.Fare.CompanyId == companyId) > 0)
                        {

                            obj = list.FirstOrDefault(c => c.Fare.CompanyId == companyId && c.Fare.SubCompanyId==subCompanyId);

                            if(obj==null)
                               obj = list.FirstOrDefault(c => c.Fare.CompanyId == companyId);

                            companyFareExist = true;
                        }
                        else
                        {

                            if (General.GetQueryable<Taxi_Model.Fare>(c => c.CompanyId == companyId).Count() == 0)
                            {

                                obj = list.FirstOrDefault(c => c.Fare.SubCompanyId == subCompanyId && c.Fare.CompanyId == null);

                                if (obj == null)
                                    obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);


                                companyFareExist = true;

                            }


                        }
                    }
                    else
                    {

                        obj = list.FirstOrDefault(c => c.Fare.SubCompanyId == subCompanyId && c.Fare.CompanyId == null);

                        if(obj==null)
                           obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                    }

                }


                if (obj != null && AppVars.objPolicyConfiguration.PreferredMileageFares.ToBool() == false)
                {

                    rtnFare = obj.Rate.ToDecimal();
                    companyPrice = obj.CompanyRate.ToDecimal();
                    deadMileage = 0;
                    errorMsg = "fixed";
                }
                else
                {

                    if ((string.IsNullOrEmpty(tempFromPostCode) && string.IsNullOrEmpty(originString)) || (string.IsNullOrEmpty(tempToPostCode) && string.IsNullOrEmpty(destString)))
                    {
                        errorMsg = "Error";
                        return 0;
                    }



                    if (IsVia)
                        return rtnFare;

                    if (deadMileage > 0)
                    {

                        string fromBasePostCode = General.GetPostCodeMatch(AppVars.objPolicyConfiguration.BaseAddress.ToStr().ToUpper().Trim());
                        decimal deadMileageDistance = General.CalculateDistance(fromBasePostCode, tempFromPostCode);

                        deadMileage = deadMileageDistance - deadMileage;

                        if (deadMileage < 0)
                            deadMileage = 0;


                        milesList.Add(deadMileage.ToDecimal());

                    }

                    // Calculate Fare Mileage Wise                
                    //  ISingleResult<ClsFares> objFare = General.SP_CalculateFares(vehicleTypeId.ToIntorNull(), companyId.ToIntorNull(), milesList.Sum().ToStr(), pickupTime);
                    decimal totalMiles = milesList.Sum();


                    if (AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal() > 0)
                    {

                        totalMiles = Math.Ceiling(totalMiles / AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal()) * AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal();

                    }



                    // var objFare = new TaxiDataContext().stp_CalculateGeneralFares(vehicleTypeId, companyId, totalMiles, pickupTime);
                    if (AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool() && AppVars.objPolicyConfiguration.FareMeterType.ToInt() != 2)
                    {
                        pickupTime = string.Format("{0:dd/MM/yyyy HH:mm}", new DateTime(1900, 1, 1, 0, 0, 0).ToDate() + pickupTime.Value.TimeOfDay).ToDateTime();

                    }




                    ISingleResult<stp_CalculateGeneralFaresBySubCompanyResult> objFare = new TaxiDataContext().stp_CalculateGeneralFaresBySubCompany(vehicleTypeId, companyId, totalMiles, pickupTime, subCompanyId);


                    if (objFare != null)
                    {
                        var f = objFare.FirstOrDefault();

                        if ((f.Result == null || f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                        {


                            if (AppVars.objPolicyConfiguration.PreferredMileageFares.ToBool() == false || obj == null ||
                                (f.totalFares.ToDecimal() > obj.Rate.ToDecimal() && (fromLocTypeId != Enums.LOCATION_TYPES.AIRPORT && toLocTypeId != Enums.LOCATION_TYPES.AIRPORT)))
                            {
                                rtnFare = f.totalFares.ToDecimal();
                                companyPrice = f.totalFares.ToDecimal();
                                companyFareExist = f.CompanyFareExist.ToBool();
                            }
                            else
                            {

                                if (obj != null)
                                {
                                    rtnFare = obj.Rate.ToDecimal();
                                    companyPrice = obj.CompanyRate.ToDecimal();
                                }
                                else
                                {
                                    rtnFare = f.totalFares.ToDecimal();
                                    companyFareExist = f.CompanyFareExist.ToBool();
                                }

                            }

                        }
                        else
                            errorMsg = "Error";



                    }
                    else
                        errorMsg = "Error";






                    if (deadMileage > 0)
                    {
                        deadMileage = 0;

                        if (milesList.Count > 1)
                            milesList.RemoveAt(1);

                    }

                    if (AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt() == actualVehicleTypeId 
                        || (IsMoreFareWise==false && rtnFare>0) )
                    {
                        if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                        {

                            decimal startRateTillMiles = General.GetObject<Fleet_VehicleType>(c => c.Id == vehicleTypeId).DefaultIfEmpty().StartRateValidMiles.ToDecimal();
                            if (startRateTillMiles > 0 && totalMiles > startRateTillMiles)
                            {

                                if (companyPrice > 0)
                                {
                                    companyPrice = Math.Ceiling(companyPrice);

                                }
                                //  rtnFare = Math.Ceiling((rtnFare);
                                rtnFare = Math.Ceiling(rtnFare);

                               
                            }
                        }
                        else
                        {



                            decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                            if (roundUp > 0)
                            {
                                // fareVal = (decimal)Math.Ceiling(fareVal / roundUp) * roundUp;

                                rtnFare = (decimal)Math.Ceiling(rtnFare / roundUp) * roundUp;


                                 if (companyPrice > 0)
                                {
                                    companyPrice =(decimal)Math.Ceiling(companyPrice / roundUp) * roundUp;

                                }
                            }


                            if (AppVars.objPolicyConfiguration.AutoShowBookingNearestDrv.ToBool())
                            {

                                rtnFare = (decimal)Math.Round(rtnFare, 0);

                                if (companyPrice > 0)
                                {
                                    companyPrice = (decimal)Math.Round(companyPrice, 0);

                                }
                            }



                            // rtnFare = (decimal)Math.Ceiling(rtnFare / 0.5m) * 0.5m;


                        }

                    }

                }






                if (IsMoreFareWise)
                {

                    decimal AddedAmount = 0.00m;
                    string op = string.Empty;

                    Gen_SysPolicy_FaresSetting objFare = null;

                    //if (companyId != null)
                    //{
                    //    objFare = General.GetObject<Gen_SysPolicy_FaresSetting>(c => c.SysPolicyId != null && c.VehicleTypeId == actualVehicleTypeId && (c.IsCompanyWise!=null && c.IsCompanyWise==true));
                    //}
                    //else
                    //{

                    //   if(objFare==null)
                    objFare = General.GetObject<Gen_SysPolicy_FaresSetting>(c => c.SysPolicyId != null && c.VehicleTypeId == actualVehicleTypeId);
                    //  }
                    if (objFare != null)
                    {
                        op = objFare.Operator.ToStr();


                        if (objFare.IsAmountWise == false)
                        {
                            AddedAmount = (rtnFare * objFare.Percentage.ToDecimal()) / 100;
                        }
                        else
                        {
                            AddedAmount = objFare.Amount.ToDecimal();

                        }

                    }


                    switch (op)
                    {
                        case "+":
                            rtnFare = rtnFare + AddedAmount;
                            if (companyPrice > 0)
                            {
                                companyPrice = companyPrice + AddedAmount;

                            }
                            break;

                        case "-":
                            rtnFare = rtnFare - AddedAmount;
                            if (companyPrice > 0)
                            {
                                companyPrice = companyPrice - AddedAmount;

                            }
                            break;

                        default:
                            rtnFare = rtnFare + AddedAmount;
                            if (companyPrice > 0)
                            {
                                companyPrice = companyPrice + AddedAmount;

                            }
                            break;
                    }




                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                    {

                            
                            rtnFare = Math.Ceiling(rtnFare);

                            if (companyPrice > 0)
                            {
                                companyPrice = Math.Ceiling(companyPrice);

                            }
                      
                    }
                    else
                    {

                        decimal roundUp2 = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                        if (roundUp2 > 0)
                        {
                            rtnFare = (decimal)Math.Ceiling(rtnFare / roundUp2) * roundUp2;


                            if (companyPrice > 0)
                            {
                                companyPrice = (decimal)Math.Ceiling(companyPrice / roundUp2) * roundUp2;

                            }


                        }

                        if (AppVars.objPolicyConfiguration.AutoShowBookingNearestDrv.ToBool())
                        {

                            rtnFare = (decimal)Math.Round(rtnFare, 0);

                            if (companyPrice > 0)
                            {
                                companyPrice = (decimal)Math.Round(companyPrice, 0);

                            }
                        }
                    }

                }



                //if (obj == null) // surcharge should add only on mileage fares
                //{

                    if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == false)
                    {

                        decimal totalSurchargePercentage = surchargeRateFrom + surchargeRateTo;

                        decimal fareSurchargePercent = (rtnFare * totalSurchargePercentage) / 100;
                        rtnFare = rtnFare + fareSurchargePercent;

                    }
                    else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == true)
                    {

                        rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                    }
                    else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == false)
                    {
                        surchargeRateTo = (rtnFare * surchargeRateTo) / 100;

                        rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                    }
                    else if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == true)
                    {
                        surchargeRateFrom = (rtnFare * surchargeRateFrom) / 100;

                        rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                    }


              //  }

             



            }
            catch (Exception ex)
            {


                //   MessageBox.Show(ex.Message);
            }
            return rtnFare;
        }




        public static decimal GetFareRate(int subCompanyId, int companyId, int vehicleTypeId, int tempFromLocId, int tempToLocId, string tempFromPostCode
                  , string tempToPostCode, ref string errorMsg, ref List<decimal> milesList, bool IsVia, bool CanCheckZoneWise, DateTime? pickupTime, ref decimal deadMileage, int fromLocTypeId, int toLocTypeId, ref bool companyFareExist, ref string estimatedTime, ref decimal companyPrice)
        {
            string miles = "";
            decimal rtnFare = 0.00m;
            string fromVal = tempFromPostCode;
            string toVal = tempToPostCode;


            bool surchargeRateFromAmountWise = false;
            bool surchargeRateToAmountWise = false;

            decimal surchargeRateFrom = 0.00m;
            decimal surchargeRateTo = 0.00m;

            bool IsMoreFareWise = false;
            int actualVehicleTypeId = vehicleTypeId;
            try
            {

                if (tempFromPostCode.Length > 0 && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                    surchargeRateFrom = GetSurchargeRate(tempFromPostCode, ref surchargeRateFromAmountWise);
                }

                if (tempToPostCode.Length > 0 && (toLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    tempToPostCode = General.GetPostCodeMatch(tempToPostCode);
                    surchargeRateTo = GetSurchargeRate(tempToPostCode, ref surchargeRateToAmountWise);
                }


                string fromSingleHalfPostCode = string.Empty;
                string fromHalfPostCode = string.Empty;
                string startFromPostCode = "";
                //if (tempFromLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempFromPostCode) && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    string[] fromArr = tempFromPostCode.Split(new char[] { ' ' });
                    startFromPostCode = fromArr[0];

                    fromHalfPostCode = startFromPostCode;

                    startFromPostCode = General.CheckIfSpecialPostCode(startFromPostCode);

                    if (fromArr.Count() > 1)
                    {
                        fromSingleHalfPostCode = fromArr[0] + " " + fromArr[1][0];
                    }

                }

                //   }


                string ToSingleHalfPostCode = string.Empty;
                string toHalfPostCode = string.Empty;
                string startToPostCode = "";
                //if (tempToLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempToPostCode) && (toLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    string[] toArr = tempToPostCode.Split(new char[] { ' ' });

                    startToPostCode = toArr[0];
                    toHalfPostCode = startToPostCode;
                    startToPostCode = General.CheckIfSpecialPostCode(startToPostCode);

                    if (toArr.Count() > 1)
                        ToSingleHalfPostCode = toArr[0] + " " + toArr[1][0];
                }
                //}

                int defaultVehicleId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();
                List<Fare_ChargesDetail> list = null;
                if ((IsVia == false && milesList.Count == 0) || (IsVia && AppVars.objPolicyConfiguration.ViaPointFareCalculationType.ToBool() == false))
                {


                    if (vehicleTypeId != defaultVehicleId)
                    {

                        if (AppVars.objPolicyConfiguration.ApplyPercentageWiseFareOn.ToBool())
                        {

                            if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                                                            ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginId == tempFromLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))))
                                                                  && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationId == tempToLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))))))

                                                                  || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (c.OriginId == tempToLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode)))))
                                                                  && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (c.DestinationId == tempFromLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))))))
                                                                     )
                                                               && c.Fare.VehicleTypeId == defaultVehicleId
                                                                && (c.Fare.CompanyId == companyId || companyId == 0)).Count() > 0)

                                                          )
                            {

                                vehicleTypeId = defaultVehicleId;
                                IsMoreFareWise = true;
                            }


                        }
                        else
                        {


                            if (tempFromPostCode.Length > 0 && tempToPostCode.Length > 0 &&
                                //(General.GetQueryable<Fare_ChargesDetail>(c =>


                                //                                      ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                //                                            && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                //                                            || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                //                                            && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                //                                               )
                                //                                         && c.Fare.VehicleTypeId == vehicleTypeId
                                //                                          && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0)

                                (General.GetQueryable<Fare_ChargesDetail>(c =>


                                                            ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginId == tempFromLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))))
                                                                  && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationId == tempToLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))))))

                                                                  || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (c.OriginId == tempToLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode)))))
                                                                  && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (c.DestinationId == tempFromLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))))))
                                                                     )
                                                               && c.Fare.VehicleTypeId == vehicleTypeId
                                                                && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0)

                                && (General.GetQueryable<Fare_OtherCharge>(c => c.Fare.VehicleTypeId == vehicleTypeId
                                                                              && (c.Fare.CompanyId == companyId || companyId == 0 || c.Fare.CompanyId == null)).Count() == 0))
                            {

                                vehicleTypeId = defaultVehicleId;
                                IsMoreFareWise = true;
                            }
                            else if (tempFromPostCode.Length == 0 || tempToPostCode.Length == 0)
                            {
                                if ((General.GetQueryable<Fare_OtherCharge>(c => c.Fare.VehicleTypeId == vehicleTypeId
                                                                              && (c.Fare.CompanyId == companyId || companyId == 0 || c.Fare.CompanyId == null)).Count() == 0))
                                {
                                    vehicleTypeId = defaultVehicleId;
                                    IsMoreFareWise = true;

                                }

                            }
                        }


                    }









                    if ((list == null || (list != null && list.Count() == 0)) && tempFromPostCode.Length > 0 && tempToPostCode.Length > 0)
                    {

                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()) || (c.FromAddress.ToUpper().EndsWith(tempFromPostCode)))) || c.OriginId == tempFromLocId)
                                                                       && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()) || (c.ToAddress.ToUpper().EndsWith(tempToPostCode)))) || c.DestinationId == tempToLocId))

                                                                          )

                                                                     && c.Fare.VehicleTypeId == vehicleTypeId

                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                                                                                                                   //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                      ).ToList();

                    }

                    if ((list == null || (list != null && list.Count() == 0)) && tempFromPostCode.Length > 0 && tempToPostCode.Length > 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (tempFromLocId == 0 && (c.FromAddress.ToUpper().EndsWith(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                    && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (tempToLocId == 0 && (c.ToAddress.ToUpper().EndsWith(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                    || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (tempToLocId == 0 && (c.FromAddress.ToUpper().EndsWith(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                    && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (tempFromLocId == 0 && (c.ToAddress.ToUpper().EndsWith(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                  && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                                                                                                                 // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();


                        if (list != null && list.Count > 0)
                        {
                            errorMsg = "Reverse found";

                        }

                    }

                    if ((tempFromLocId != 0 || tempToLocId != 0) && (list == null || (list != null && list.Count() == 0)))
                    {
                        if (tempFromLocId > 0)
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                   && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                   || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                   && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                      )

                                                                 && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                                                                                                                   // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                  ).ToList();

                        }

                        if ((list == null || list.Count == 0) && tempToLocId > 0)
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                    || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                    && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                                                                                                                   // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();

                        }



                        if ((list == null || list.Count == 0))
                        {
                            list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                    || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                    && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                       )

                                                                  && c.Fare.VehicleTypeId == vehicleTypeId
                                                                    && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                                                                                                                   // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                                   ).ToList();

                        }

                        if (list != null && list.Count > 0)
                        {
                            errorMsg = "Reverse found";

                        }


                    }

                }
                else
                {

                    if ((IsVia == false && milesList.Count > 0 && AppVars.objPolicyConfiguration.ViaPointFareCalculationType.ToBool() && vehicleTypeId != defaultVehicleId))
                    {
                        vehicleTypeId = defaultVehicleId;
                        IsMoreFareWise = true;

                    }


                }

                //if ((tempFromLocId == 0 && string.IsNullOrEmpty(startFromPostCode)) || (tempToLocId == 0 && string.IsNullOrEmpty(startToPostCode)))
                //    obj = null;


                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 2)
                {
                    tempFromPostCode = fromVal;
                    tempToPostCode = toVal;
                }




                stp_getCoordinatesByAddressResult pickupCoord = null;
                stp_getCoordinatesByAddressResult destCoord = null;

                try
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        pickupCoord = db.stp_getCoordinatesByAddress(fromVal, General.GetPostCodeMatch(tempFromPostCode)).FirstOrDefault();
                        destCoord = db.stp_getCoordinatesByAddress(toVal, General.GetPostCodeMatch(tempToPostCode)).FirstOrDefault();
                    }
                }
                catch
                {


                }



                string originString = string.Empty;
                string destString = string.Empty;
                if (pickupCoord != null && pickupCoord.Latitude != null && pickupCoord.Latitude != 0)
                {
                    originString = pickupCoord.Latitude + "," + pickupCoord.Longtiude;
                }

                if (destCoord != null && destCoord.Latitude != null && destCoord.Latitude != 0)
                {
                    destString = destCoord.Latitude + "," + destCoord.Longtiude;
                }


                //if (IsVia || milesList.Count > 0)
                //    miles = CalculateDistance(tempFromPostCode, tempToPostCode, ref estimatedTime, true).ToStr();
                //else
                //    miles = CalculateDistance(tempFromPostCode, tempToPostCode, ref estimatedTime).ToStr();


                if (IsVia || milesList.Count > 0)
                    miles = CalculateDistance(originString, destString, ref estimatedTime, true).ToStr();
                else
                    miles = CalculateDistance(originString, destString, ref estimatedTime).ToStr();




                milesList.Add(miles.ToDecimal());


                Fare_ChargesDetail obj = null;

                if (list != null)
                {
                    if (companyId != 0)
                    {
                        if (list.Count(c => c.Fare.CompanyId == companyId) > 0)
                        {
                            obj = list.FirstOrDefault(c => c.Fare.CompanyId == companyId);

                            companyFareExist = true;
                        }
                        else
                        {

                            if (General.GetQueryable<Taxi_Model.Fare>(c => c.CompanyId == companyId).Count() == 0)
                            {
                                obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                                companyFareExist = true;

                            }


                        }
                    }
                    else
                    {
                        obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                    }

                }


                if (obj != null && AppVars.objPolicyConfiguration.PreferredMileageFares.ToBool() == false)
                {

                    rtnFare = obj.Rate.ToDecimal();
                    companyPrice = obj.CompanyRate.ToDecimal();
                    deadMileage = 0;
                    errorMsg = "fixed";
                }
                else
                {

                    if ((string.IsNullOrEmpty(tempFromPostCode) && string.IsNullOrEmpty(originString)) || (string.IsNullOrEmpty(tempToPostCode) && string.IsNullOrEmpty(destString)))
                    {
                        errorMsg = "Error";
                        return 0;
                    }



                    if (IsVia)
                        return rtnFare;

                    if (deadMileage > 0)
                    {

                        string fromBasePostCode = General.GetPostCodeMatch(AppVars.objPolicyConfiguration.BaseAddress.ToStr().ToUpper().Trim());
                        decimal deadMileageDistance = General.CalculateDistance(fromBasePostCode, tempFromPostCode);

                        deadMileage = deadMileageDistance - deadMileage;

                        if (deadMileage < 0)
                            deadMileage = 0;


                        milesList.Add(deadMileage.ToDecimal());

                    }

                    // Calculate Fare Mileage Wise                
                    //  ISingleResult<ClsFares> objFare = General.SP_CalculateFares(vehicleTypeId.ToIntorNull(), companyId.ToIntorNull(), milesList.Sum().ToStr(), pickupTime);
                    decimal totalMiles = milesList.Sum();


                    if (AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal() > 0)
                    {

                        totalMiles = Math.Ceiling(totalMiles / AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal()) * AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal();

                    }



                    // var objFare = new TaxiDataContext().stp_CalculateGeneralFares(vehicleTypeId, companyId, totalMiles, pickupTime);
                    if (AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool() && AppVars.objPolicyConfiguration.FareMeterType.ToInt() != 2)
                    {
                        pickupTime = string.Format("{0:dd/MM/yyyy HH:mm}", new DateTime(1900, 1, 1, 0, 0, 0).ToDate() + pickupTime.Value.TimeOfDay).ToDateTime();

                    }




                    ISingleResult<stp_CalculateGeneralFaresBySubCompanyResult> objFare = new TaxiDataContext().stp_CalculateGeneralFaresBySubCompany(vehicleTypeId, companyId, totalMiles, pickupTime, subCompanyId);


                    if (objFare != null)
                    {
                        var f = objFare.FirstOrDefault();

                        if ((f.Result == null || f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                        {


                            if (AppVars.objPolicyConfiguration.PreferredMileageFares.ToBool() == false || obj == null ||
                                (f.totalFares.ToDecimal() > obj.Rate.ToDecimal() && (fromLocTypeId != Enums.LOCATION_TYPES.AIRPORT && toLocTypeId != Enums.LOCATION_TYPES.AIRPORT)))
                            {
                                rtnFare = f.totalFares.ToDecimal();
                                companyPrice = f.totalFares.ToDecimal();
                                companyFareExist = f.CompanyFareExist.ToBool();
                            }
                            else
                            {

                                if (obj != null)
                                {
                                    rtnFare = obj.Rate.ToDecimal();
                                    companyPrice = obj.CompanyRate.ToDecimal();
                                }
                                else
                                {
                                    rtnFare = f.totalFares.ToDecimal();
                                    companyFareExist = f.CompanyFareExist.ToBool();
                                }

                            }

                        }
                        else
                            errorMsg = "Error";



                    }
                    else
                        errorMsg = "Error";






                    if (deadMileage > 0)
                    {
                        deadMileage = 0;

                        if (milesList.Count > 1)
                            milesList.RemoveAt(1);

                    }

                    if (AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt() == actualVehicleTypeId
                        || (IsMoreFareWise == false && rtnFare > 0))
                    {
                        if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                        {

                            decimal startRateTillMiles = General.GetObject<Fleet_VehicleType>(c => c.Id == vehicleTypeId).DefaultIfEmpty().StartRateValidMiles.ToDecimal();
                            if (startRateTillMiles > 0 && totalMiles > startRateTillMiles)
                            {

                                if (companyPrice > 0)
                                {
                                    companyPrice = Math.Ceiling(companyPrice);

                                }
                                //  rtnFare = Math.Ceiling((rtnFare);
                                rtnFare = Math.Ceiling(rtnFare);


                            }
                        }
                        else
                        {



                            decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                            if (roundUp > 0)
                            {
                                // fareVal = (decimal)Math.Ceiling(fareVal / roundUp) * roundUp;

                                rtnFare = (decimal)Math.Ceiling(rtnFare / roundUp) * roundUp;


                                if (companyPrice > 0)
                                {
                                    companyPrice = (decimal)Math.Ceiling(companyPrice / roundUp) * roundUp;

                                }
                            }


                            if (AppVars.objPolicyConfiguration.AutoShowBookingNearestDrv.ToBool())
                            {

                                rtnFare = (decimal)Math.Round(rtnFare, 0);

                                if (companyPrice > 0)
                                {
                                    companyPrice = (decimal)Math.Round(companyPrice, 0);

                                }
                            }



                            // rtnFare = (decimal)Math.Ceiling(rtnFare / 0.5m) * 0.5m;


                        }

                    }

                }






                if (IsMoreFareWise)
                {

                    decimal AddedAmount = 0.00m;
                    string op = string.Empty;

                    Gen_SysPolicy_FaresSetting objFare = null;

                    //if (companyId != null)
                    //{
                    //    objFare = General.GetObject<Gen_SysPolicy_FaresSetting>(c => c.SysPolicyId != null && c.VehicleTypeId == actualVehicleTypeId && (c.IsCompanyWise!=null && c.IsCompanyWise==true));
                    //}
                    //else
                    //{

                    //   if(objFare==null)
                    objFare = General.GetObject<Gen_SysPolicy_FaresSetting>(c => c.SysPolicyId != null && c.VehicleTypeId == actualVehicleTypeId);
                    //  }
                    if (objFare != null)
                    {
                        op = objFare.Operator.ToStr();


                        if (objFare.IsAmountWise == false)
                        {
                            AddedAmount = (rtnFare * objFare.Percentage.ToDecimal()) / 100;
                        }
                        else
                        {
                            AddedAmount = objFare.Amount.ToDecimal();

                        }

                    }


                    switch (op)
                    {
                        case "+":
                            rtnFare = rtnFare + AddedAmount;
                            if (companyPrice > 0)
                            {
                                companyPrice = companyPrice - AddedAmount;

                            }
                            break;

                        case "-":
                            rtnFare = rtnFare - AddedAmount;
                            if (companyPrice > 0)
                            {
                                companyPrice = companyPrice - AddedAmount;

                            }
                            break;

                        default:
                            rtnFare = rtnFare + AddedAmount;
                            if (companyPrice > 0)
                            {
                                companyPrice = companyPrice - AddedAmount;

                            }
                            break;
                    }




                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                    {


                        rtnFare = Math.Ceiling(rtnFare);

                        if (companyPrice > 0)
                        {
                            companyPrice = Math.Ceiling(companyPrice);

                        }

                    }
                    else
                    {

                        decimal roundUp2 = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                        if (roundUp2 > 0)
                        {
                            rtnFare = (decimal)Math.Ceiling(rtnFare / roundUp2) * roundUp2;


                            if (companyPrice > 0)
                            {
                                companyPrice = (decimal)Math.Ceiling(companyPrice / roundUp2) * roundUp2;

                            }


                        }

                        if (AppVars.objPolicyConfiguration.AutoShowBookingNearestDrv.ToBool())
                        {

                            rtnFare = (decimal)Math.Round(rtnFare, 0);

                            if (companyPrice > 0)
                            {
                                companyPrice = (decimal)Math.Round(companyPrice, 0);

                            }
                        }
                    }

                }



                if (obj == null)
                {

                    if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == false)
                    {

                        decimal totalSurchargePercentage = surchargeRateFrom + surchargeRateTo;

                        decimal fareSurchargePercent = (rtnFare * totalSurchargePercentage) / 100;
                        rtnFare = rtnFare + fareSurchargePercent;

                    }
                    else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == true)
                    {

                        rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                    }
                    else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == false)
                    {
                        surchargeRateTo = (rtnFare * surchargeRateTo) / 100;

                        rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                    }
                    else if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == true)
                    {
                        surchargeRateFrom = (rtnFare * surchargeRateFrom) / 100;

                        rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                    }


                }





            }
            catch (Exception ex)
            {


                //   MessageBox.Show(ex.Message);
            }
            return rtnFare;
        }


        public static decimal GetSimpleFareRateWithRoundTrip(int companyId, int vehicleTypeId, int tempFromLocId, int tempToLocId, string tempFromPostCode
             , string tempToPostCode, ref string errorMsg, ref List<decimal> milesList, bool IsVia, bool CanCheckZoneWise, DateTime? pickupTime, ref decimal deadMileage, int fromLocTypeId, int toLocTypeId, ref bool companyFareExist, ref string estimatedTime, int? fromZoneId, int? toZoneId, ref bool IsMoreFareWise,ref int farecalculateby)
        {
        
            decimal rtnFare = 0.00m;
            string fromVal = tempFromPostCode;
            string toVal = tempToPostCode;


            bool surchargeRateFromAmountWise = false;
            bool surchargeRateToAmountWise = false;

            decimal surchargeRateFrom = 0.00m;
            decimal surchargeRateTo = 0.00m;

            // bool IsMoreFareWise = false;
            int actualVehicleTypeId = vehicleTypeId;
            try
            {

                if (tempFromPostCode.Length > 0)
                {
                    tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                    surchargeRateFrom = GetSurchargeRate(tempFromPostCode, ref surchargeRateFromAmountWise);
                }

                if (tempToPostCode.Length > 0)
                {
                    tempToPostCode = General.GetPostCodeMatch(tempToPostCode);
                    surchargeRateTo = GetSurchargeRate(tempToPostCode, ref surchargeRateToAmountWise);
                }


                string fromSingleHalfPostCode = string.Empty;
                string fromHalfPostCode = string.Empty;
                string startFromPostCode = "";
                //if (tempFromLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempFromPostCode))
                {
                    string[] fromArr = tempFromPostCode.Split(new char[] { ' ' });
                    startFromPostCode = fromArr[0];

                    fromHalfPostCode = startFromPostCode;

                    startFromPostCode = General.CheckIfSpecialPostCode(startFromPostCode);

                    fromSingleHalfPostCode = fromArr[0] + " " + fromArr[1][0];

                }

                //   }


                string ToSingleHalfPostCode = string.Empty;
                string toHalfPostCode = string.Empty;
                string startToPostCode = "";
                //if (tempToLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempToPostCode))
                {
                    string[] toArr = tempToPostCode.Split(new char[] { ' ' });

                    startToPostCode = toArr[0];
                    toHalfPostCode = startToPostCode;
                    startToPostCode = General.CheckIfSpecialPostCode(startToPostCode);

                    ToSingleHalfPostCode = toArr[0] + " " + toArr[1][0];
                }
                //}

                int defaultVehicleId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();

                if (vehicleTypeId != defaultVehicleId)
                {

                    if (AppVars.objPolicyConfiguration.ApplyPercentageWiseFareOn.ToBool())
                    {





                        if (IsMoreFareWise == false)
                        {
                            if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                                                            ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.FromZoneId == fromZoneId || c.OriginId == tempFromLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))))
                                                                  && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.ToZoneId == toZoneId || c.DestinationId == tempToLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))))))

                                                                  || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (c.OriginId == tempToLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode)))))
                                                                  && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (c.DestinationId == tempFromLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))))))
                                                                     )
                                                               && c.Fare.VehicleTypeId == defaultVehicleId
                                                                && (c.Fare.CompanyId == companyId || companyId == 0)).Count() > 0)

                                                          )
                            {

                                vehicleTypeId = defaultVehicleId;
                                IsMoreFareWise = true;
                            }
                        }


                    }
                    else
                    {


                        if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                                                                  ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                        && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                        || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                        && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                           )
                                                                     && c.Fare.VehicleTypeId == vehicleTypeId
                                                                      && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0)

                            && (General.GetQueryable<Fare_OtherCharge>(c => c.Fare.VehicleTypeId == vehicleTypeId
                                                                          && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0))
                        {

                            vehicleTypeId = defaultVehicleId;
                            IsMoreFareWise = true;
                        }
                    }


                }





                List<Fare_ChargesDetail> list = null;


                if (list == null || (list != null && list.Count() == 0))
                {
                    //int? zoneId = fromZoneId;
                    if (fromZoneId != 0)
                    {

                        list = General.GetQueryable<Fare_ChargesDetail>(c => (((c.FromZoneId == fromZoneId)
                                                                       && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()))) || c.DestinationId == tempToLocId))

                                                                          )

                                                                     && c.Fare.VehicleTypeId == vehicleTypeId
                            //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                      ).ToList();
                    }
                    else if (toZoneId != 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()))) || c.OriginId == tempFromLocId)
                                                               && c.ToZoneId == toZoneId)

                                                                  )

                                                             && c.Fare.VehicleTypeId == vehicleTypeId
                            //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                              ).ToList();

                    }
                }


                if (list == null || (list != null && list.Count() == 0))
                {

                    list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()))) || c.OriginId == tempFromLocId)
                                                                   && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()))) || c.DestinationId == tempToLocId))

                                                                      )

                                                                 && c.Fare.VehicleTypeId == vehicleTypeId
                        //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                  ).ToList();

                }

                if (list == null || (list != null && list.Count() == 0))
                {
                    list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                        // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();


                    if (list != null && list.Count > 0)
                    {
                        errorMsg = "Reverse found";

                    }

                }

                if ((tempFromLocId != 0 || tempToLocId != 0) && (list == null || (list != null && list.Count() == 0)))
                {
                    if (tempFromLocId > 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                               && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                               || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                               && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                  )

                                                             && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                              ).ToList();

                    }

                    if ((list == null || list.Count == 0) && tempToLocId > 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();

                    }



                    if ((list == null || list.Count == 0))
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();

                    }

                    if (list != null && list.Count > 0)
                    {
                        errorMsg = "Reverse found";

                    }


                }



                //if ((tempFromLocId == 0 && string.IsNullOrEmpty(startFromPostCode)) || (tempToLocId == 0 && string.IsNullOrEmpty(startToPostCode)))
                //    obj = null;


                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 2)
                {
                    tempFromPostCode = fromVal;
                    tempToPostCode = toVal;
                }



                Fare_ChargesDetail obj = null;

                if (list != null)
                {
                    if (companyId != 0)
                    {
                        if (list.Count(c => c.Fare.CompanyId == companyId) > 0)
                        {
                            obj = list.FirstOrDefault(c => c.Fare.CompanyId == companyId);

                            companyFareExist = true;
                        }
                        else
                        {

                            if (General.GetQueryable<Taxi_Model.Fare>(c => c.CompanyId == companyId).Count() == 0)
                            {
                                obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                                companyFareExist = true;

                            }


                        }
                    }
                    else
                    {
                        obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                    }

                }


                if (obj != null)
                {

                    rtnFare = obj.Rate.ToDecimal();
                    deadMileage = 0;
                }
                else
                {

                    if (string.IsNullOrEmpty(tempFromPostCode) || string.IsNullOrEmpty(tempToPostCode))
                    {
                        errorMsg = "Error";
                        return 0;
                    }


                    if (AppVars.objPolicyConfiguration.DeadMileage.ToDecimal() > 0)
                    {
                        //   string basePostCode = GetPostCodeMatch(AppVars.objPolicyConfiguration.BaseAddress.ToStr().ToUpper());

                        string basePostCode = AppVars.objPolicyConfiguration.DefaultCounty.ToStr();


                        decimal towntoPickup = General.CalculateDistance(basePostCode, tempFromPostCode);
                        decimal destToTown = (General.CalculateDistance(tempToPostCode, basePostCode));

                        decimal journeyMilage = General.CalculateDistance(tempFromPostCode, tempToPostCode);


                        if (towntoPickup > AppVars.objPolicyConfiguration.DeadMileage.ToDecimal()
                            && destToTown > AppVars.objPolicyConfiguration.DeadMileage.ToDecimal())
                        {

                            journeyMilage = (towntoPickup + journeyMilage + destToTown) / 2;
                            farecalculateby = 3;
                        }
                        else
                            farecalculateby = 2;


                        journeyMilage = Math.Round(journeyMilage, 1);
                        milesList.Add(journeyMilage);


                        //if ((towntoPickup + destToTown) > journeyMilage)
                        //{


                        //    miles = (towntoPickup + journeyMilage + destToTown) / 2;
                        //}
                        //else
                        //{
                        //    //     miles = journeyMilage;
                        //    miles = journeyMilage + ((destToTown) / 2);
                        //    // miles = journeyMilage + ((destToTown) / 2);
                        //}                     


                    }
                    else
                    {
                        decimal journeyMilage = General.CalculateDistance(tempFromPostCode, tempToPostCode);
                        journeyMilage = Math.Round(journeyMilage, 1);
                        milesList.Add(journeyMilage);
                    }


                    // Calculate Fare Mileage Wise                
                    //  ISingleResult<ClsFares> objFare = General.SP_CalculateFares(vehicleTypeId.ToIntorNull(), companyId.ToIntorNull(), milesList.Sum().ToStr(), pickupTime);
                    decimal totalMiles = milesList.Sum();



                    if (AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
                    {
                        var objFare = new TaxiDataContext().stp_CalculateGeneralFaresBySubCompany(vehicleTypeId, companyId, totalMiles, pickupTime,null);




                        if (objFare != null)
                        {
                            var f = objFare.FirstOrDefault();

                            if ((f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                            {
                                rtnFare = f.totalFares.ToDecimal();

                                companyFareExist = f.CompanyFareExist.ToBool();
                            }
                            else
                                errorMsg = "Error";
                        }
                        else
                            errorMsg = "Error";
                    }
                    else
                    {

                        var objFare = new TaxiDataContext().stp_CalculateGeneralFares(vehicleTypeId, companyId, totalMiles, pickupTime);

                        if (objFare != null)
                        {
                            var f = objFare.FirstOrDefault();

                            if ((f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                            {
                                rtnFare = f.totalFares.ToDecimal();

                                companyFareExist = f.CompanyFareExist.ToBool();
                            }
                            else
                                errorMsg = "Error";
                        }
                        else
                            errorMsg = "Error";

                    }

                    if (deadMileage > 0)
                    {
                        deadMileage = 0;

                        if (milesList.Count > 1)
                            milesList.RemoveAt(1);

                    }

                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                    {

                        decimal startRateTillMiles = General.GetObject<Fleet_VehicleType>(c => c.Id == vehicleTypeId).DefaultIfEmpty().StartRateValidMiles.ToDecimal();
                        if (startRateTillMiles > 0 && totalMiles > startRateTillMiles)
                        {

                            //  rtnFare = Math.Ceiling((rtnFare);
                            rtnFare = Math.Ceiling(rtnFare);
                        }
                    }
                    else
                    {
                        decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                        if (roundUp > 0)
                        {

                            rtnFare = (decimal)Math.Ceiling(rtnFare / roundUp) * roundUp;
                        }

                    }

                }

                if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == false)
                {

                    decimal totalSurchargePercentage = surchargeRateFrom + surchargeRateTo;

                    decimal fareSurchargePercent = (rtnFare * totalSurchargePercentage) / 100;
                    rtnFare = rtnFare + fareSurchargePercent;

                }
                else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == true)
                {

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }
                else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == false)
                {
                    surchargeRateTo = (rtnFare * surchargeRateTo) / 100;

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }
                else if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == true)
                {
                    surchargeRateFrom = (rtnFare * surchargeRateFrom) / 100;

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }



            }
            catch
            {


                //   MessageBox.Show(ex.Message);
            }
            return rtnFare;
        }




        public static decimal GetSimpleFareRateWithRoundTrip(int companyId, int vehicleTypeId, int tempFromLocId, int tempToLocId, string tempFromPostCode
          , string tempToPostCode, ref string errorMsg, ref List<decimal> milesList, bool IsVia, bool CanCheckZoneWise, DateTime? pickupTime, ref decimal deadMileage, int fromLocTypeId, int toLocTypeId, ref bool companyFareExist, ref string estimatedTime, int? fromZoneId, int? toZoneId, ref bool IsMoreFareWise, ref int farecalculateby,int subCompanyId)
        {

            decimal rtnFare = 0.00m;
            string fromVal = tempFromPostCode;
            string toVal = tempToPostCode;


            //bool surchargeRateFromAmountWise = false;
            //bool surchargeRateToAmountWise = false;

            //decimal surchargeRateFrom = 0.00m;
            //decimal surchargeRateTo = 0.00m;

            // bool IsMoreFareWise = false;
            int actualVehicleTypeId = vehicleTypeId;
           

            try
            {

                //if ((tempFromPostCode.Length > 0) && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN))
                //{
                //    tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                //    surchargeRateFrom = GetSurchargeRate(tempFromPostCode, ref surchargeRateFromAmountWise);
                //}

                //if ((tempToPostCode.Length > 0 ) && (toLocTypeId != Enums.LOCATION_TYPES.TOWN))
                //{
                //    tempToPostCode = General.GetPostCodeMatch(tempToPostCode);
                //    surchargeRateTo = GetSurchargeRate(tempToPostCode, ref surchargeRateToAmountWise);
                //}

                if ((tempFromPostCode.Length > 0 || fromZoneId.ToInt() > 0) && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                  //  surchargeRateFrom = GetSurchargeRate(tempFromPostCode, fromZoneId, pickupTime.ToDateTime(), ref surchargeRateFromAmountWise);
                }

                if ((tempToPostCode.Length > 0 || toZoneId.ToInt() > 0) && (toLocTypeId != Enums.LOCATION_TYPES.TOWN))
                {
                    tempToPostCode = General.GetPostCodeMatch(tempToPostCode);
                 //   surchargeRateTo = GetSurchargeRate(tempToPostCode, toZoneId, pickupTime.ToDateTime(), ref surchargeRateToAmountWise);
                }


                string fromSingleHalfPostCode = string.Empty;
                string fromHalfPostCode = string.Empty;
                string startFromPostCode = "";
                //if (tempFromLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempFromPostCode) && (fromLocTypeId != Enums.LOCATION_TYPES.TOWN ))
                {
                    string[] fromArr = tempFromPostCode.Split(new char[] { ' ' });
                    startFromPostCode = fromArr[0];

                    fromHalfPostCode = startFromPostCode;

                    startFromPostCode = General.CheckIfSpecialPostCode(startFromPostCode);

                    fromSingleHalfPostCode = fromArr[0] + " " + fromArr[1][0];

                }

                //   }


                string ToSingleHalfPostCode = string.Empty;
                string toHalfPostCode = string.Empty;
                string startToPostCode = "";
                //if (tempToLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempToPostCode) && (toLocTypeId != Enums.LOCATION_TYPES.TOWN ))
                {
                    string[] toArr = tempToPostCode.Split(new char[] { ' ' });

                    startToPostCode = toArr[0];
                    toHalfPostCode = startToPostCode;
                    startToPostCode = General.CheckIfSpecialPostCode(startToPostCode);

                    ToSingleHalfPostCode = toArr[0] + " " + toArr[1][0];
                }
                //}

                int defaultVehicleId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();

                if (vehicleTypeId != defaultVehicleId)
                {

                    if (AppVars.objPolicyConfiguration.ApplyPercentageWiseFareOn.ToBool())
                    {





                        if (IsMoreFareWise == false)
                        {
                            if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                                                            ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.FromZoneId == fromZoneId || c.OriginId == tempFromLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))))
                                                                  && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.ToZoneId == toZoneId || c.DestinationId == tempToLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))))))

                                                                  || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (c.OriginId == tempToLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode)))))
                                                                  && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (c.DestinationId == tempFromLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))))))
                                                                     )
                                                               && c.Fare.VehicleTypeId == defaultVehicleId
                                                                && (c.Fare.CompanyId == companyId || companyId == 0)).Count() > 0)

                                                          )
                            {

                                vehicleTypeId = defaultVehicleId;
                                IsMoreFareWise = true;
                            }
                        }


                    }
                    else
                    {


                        if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                                                                  ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                        && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                        || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                        && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                           )
                                                                     && c.Fare.VehicleTypeId == vehicleTypeId
                                                                      && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0)

                            && (General.GetQueryable<Fare_OtherCharge>(c => c.Fare.VehicleTypeId == vehicleTypeId
                                                                          && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0))
                        {

                            vehicleTypeId = defaultVehicleId;
                            IsMoreFareWise = true;
                        }
                    }


                }





                List<Fare_ChargesDetail> list = null;


                //if (list == null || (list != null && list.Count() == 0))
                //{
                //    //int? zoneId = fromZoneId;
                //    if (fromZoneId != 0)
                //    {

                //        list = General.GetQueryable<Fare_ChargesDetail>(c => (((c.FromZoneId == fromZoneId)
                //                                                       && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()))) || c.DestinationId == tempToLocId))

                //                                                          )

                //                                                     && c.Fare.VehicleTypeId == vehicleTypeId
                //            //&& (c.Fare.CompanyId == companyId || companyId == 0)

                //                                                      ).ToList();
                //    }
                //    else if (toZoneId != 0)
                //    {
                //        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()))) || c.OriginId == tempFromLocId)
                //                                               && c.ToZoneId == toZoneId)

                //                                                  )

                //                                             && c.Fare.VehicleTypeId == vehicleTypeId
                //            //&& (c.Fare.CompanyId == companyId || companyId == 0)

                //                                              ).ToList();

                //    }
                //}


                //if ((list == null || (list != null && list.Count() == 0)) && tempFromPostCode.ToStr().Length > 0 && tempToPostCode.ToStr().Length>0)
                //{

                //    list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()))) || c.OriginId == tempFromLocId)
                //                                                   && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()))) || c.DestinationId == tempToLocId))

                //                                                      )

                //                                                 && c.Fare.VehicleTypeId == vehicleTypeId
                //        //&& (c.Fare.CompanyId == companyId || companyId == 0)

                //                                                  ).ToList();

                //}

                //if ((list == null || (list != null && list.Count() == 0)) && tempFromPostCode.ToStr().Length > 0 && tempToPostCode.ToStr().Length > 0)
                //{
                //    list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                //                                                && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                //                                                || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                //                                                && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                //                                                   )

                //                                              && c.Fare.VehicleTypeId == vehicleTypeId
                //        // && (c.Fare.CompanyId == companyId || companyId == 0)

                //                                               ).ToList();


                //    if (list != null && list.Count > 0)
                //    {
                //        errorMsg = "Reverse found";

                //    }

                //}


                if ((list == null || (list != null && list.Count() == 0)) && tempFromPostCode.Length > 0 && tempToPostCode.Length > 0)
                {

                    list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()) || (c.FromAddress.ToUpper().EndsWith(tempFromPostCode)))) || c.OriginId == tempFromLocId)
                                                                   && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()) || (c.ToAddress.ToUpper().EndsWith(tempToPostCode)))) || c.DestinationId == tempToLocId))

                                                                      )

                                                                 && c.Fare.VehicleTypeId == vehicleTypeId

                                                                && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                                                                                                               //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                  ).ToList();

                }

                if ((list == null || (list != null && list.Count() == 0)) && tempFromPostCode.Length > 0 && tempToPostCode.Length > 0)
                {
                    list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (tempFromLocId == 0 && (c.FromAddress.ToUpper().EndsWith(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (tempToLocId == 0 && (c.ToAddress.ToUpper().EndsWith(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (tempToLocId == 0 && (c.FromAddress.ToUpper().EndsWith(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (tempFromLocId == 0 && (c.ToAddress.ToUpper().EndsWith(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                                                              && (c.Fare.CompanyId == companyId || c.Fare.CompanyId == null) // need to comment later (this is not for all clients)- make a check on settings
                                                                                                                             // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();


                    if (list != null && list.Count > 0)
                    {
                        errorMsg = "Reverse found";

                    }

                }

                if ((tempFromLocId != 0 || tempToLocId != 0) && (list == null || (list != null && list.Count() == 0)))
                {
                    if (tempFromLocId > 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                               && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                               || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                               && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                  )

                                                             && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                              ).ToList();

                    }

                    if ((list == null || list.Count == 0) && tempToLocId > 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();

                    }



                    if ((list == null || list.Count == 0))
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();

                    }

                    if (list != null && list.Count > 0)
                    {
                        errorMsg = "Reverse found";

                    }


                }



                //if ((tempFromLocId == 0 && string.IsNullOrEmpty(startFromPostCode)) || (tempToLocId == 0 && string.IsNullOrEmpty(startToPostCode)))
                //    obj = null;


                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 2)
                {
                    tempFromPostCode = fromVal;
                    tempToPostCode = toVal;
                }



                Fare_ChargesDetail obj = null;

                if (list != null)
                {
                    if (companyId != 0)
                    {
                        if (list.Count(c => c.Fare.CompanyId == companyId) > 0)
                        {
                            obj = list.FirstOrDefault(c => c.Fare.CompanyId == companyId);

                            companyFareExist = true;
                        }
                        else
                        {

                            if (General.GetQueryable<Taxi_Model.Fare>(c => c.CompanyId == companyId).Count() == 0)
                            {
                                obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                                companyFareExist = true;

                            }


                        }
                    }
                    else
                    {
                        obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                    }

                }


                if (obj != null)
                {

                    rtnFare = obj.Rate.ToDecimal();
                    deadMileage = 0;
                    farecalculateby = 4;
                }
                else
                {
                    if (tempFromPostCode.ToStr().Length == 0)
                        tempFromPostCode = fromVal ;


                    if (tempToPostCode.ToStr().Length == 0)
                        tempToPostCode =toVal;

                    if (string.IsNullOrEmpty(tempFromPostCode) || string.IsNullOrEmpty(tempToPostCode))
                    {
                        errorMsg = "Error";
                        return 0;
                    }



                    if (AppVars.objPolicyConfiguration.DeadMileage.ToDecimal() > 0)
                    {
                        //   string basePostCode = GetPostCodeMatch(AppVars.objPolicyConfiguration.BaseAddress.ToStr().ToUpper());

                        string basePostCode = AppVars.objPolicyConfiguration.DefaultCounty.ToStr();


                        decimal towntoPickup = General.CalculateDistance(basePostCode, tempFromPostCode);
                        decimal destToTown = (General.CalculateDistance(tempToPostCode, basePostCode));

                        decimal journeyMilage = General.CalculateDistance(tempFromPostCode, tempToPostCode);


                        if (towntoPickup > AppVars.objPolicyConfiguration.DeadMileage.ToDecimal()
                            && destToTown > AppVars.objPolicyConfiguration.DeadMileage.ToDecimal())
                        {

                            journeyMilage = (towntoPickup + journeyMilage + destToTown) / 2;
                            farecalculateby = 3;
                        }
                        else
                            farecalculateby = 2;


                        journeyMilage = Math.Round(journeyMilage, 1);
                        milesList.Add(journeyMilage);


                        //if ((towntoPickup + destToTown) > journeyMilage)
                        //{


                        //    miles = (towntoPickup + journeyMilage + destToTown) / 2;
                        //}
                        //else
                        //{
                        //    //     miles = journeyMilage;
                        //    miles = journeyMilage + ((destToTown) / 2);
                        //    // miles = journeyMilage + ((destToTown) / 2);
                        //}                     


                    }
                    else
                    {
                        decimal journeyMilage = General.CalculateDistance(tempFromPostCode, tempToPostCode);
                        journeyMilage = Math.Round(journeyMilage, 1);
                        milesList.Add(journeyMilage);
                    }


                    // Calculate Fare Mileage Wise                
                    //  ISingleResult<ClsFares> objFare = General.SP_CalculateFares(vehicleTypeId.ToIntorNull(), companyId.ToIntorNull(), milesList.Sum().ToStr(), pickupTime);
                    decimal totalMiles = milesList.Sum();



                    if (AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
                    {
                        var objFare = new TaxiDataContext().stp_CalculateGeneralFaresBySubCompany(vehicleTypeId, companyId, totalMiles, pickupTime, subCompanyId);




                        if (objFare != null)
                        {
                            var f = objFare.FirstOrDefault();

                            if ((f.Result==null || f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                            {
                                rtnFare = f.totalFares.ToDecimal();

                                companyFareExist = f.CompanyFareExist.ToBool();
                            }
                            else
                                errorMsg = "Error";
                        }
                        else
                            errorMsg = "Error";
                    }
                    else
                    {

                        var objFare = new TaxiDataContext().stp_CalculateGeneralFares(vehicleTypeId, companyId, totalMiles, pickupTime);

                        if (objFare != null)
                        {
                            var f = objFare.FirstOrDefault();

                            if ((f.Result==null || f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                            {
                                rtnFare = f.totalFares.ToDecimal();

                                companyFareExist = f.CompanyFareExist.ToBool();
                            }
                            else
                                errorMsg = "Error";
                        }
                        else
                            errorMsg = "Error";

                    }

                    if (deadMileage > 0)
                    {
                        deadMileage = 0;

                        if (milesList.Count > 1)
                            milesList.RemoveAt(1);

                    }

                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                    {

                        decimal startRateTillMiles = General.GetObject<Fleet_VehicleType>(c => c.Id == vehicleTypeId).DefaultIfEmpty().StartRateValidMiles.ToDecimal();
                        if (startRateTillMiles > 0 && totalMiles > startRateTillMiles)
                        {

                            //  rtnFare = Math.Ceiling((rtnFare);
                            rtnFare = Math.Ceiling(rtnFare);
                        }
                    }
                    else
                    {
                        decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                        if (roundUp > 0)
                        {

                            rtnFare = (decimal)Math.Ceiling(rtnFare / roundUp) * roundUp;
                        }

                    }

                }


                //if (obj == null)
                //{

                    //if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == false)
                    //{

                    //    decimal totalSurchargePercentage = surchargeRateFrom + surchargeRateTo;

                    //    decimal fareSurchargePercent = (rtnFare * totalSurchargePercentage) / 100;
                    //    rtnFare = rtnFare + fareSurchargePercent;

                    //}
                    //else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == true)
                    //{

                    //    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                    //}
                    //else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == false)
                    //{
                    //    surchargeRateTo = (rtnFare * surchargeRateTo) / 100;

                    //    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                    //}
                    //else if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == true)
                    //{
                    //    surchargeRateFrom = (rtnFare * surchargeRateFrom) / 100;

                    //    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                    //}


                //}



            }
            catch
            {


                //   MessageBox.Show(ex.Message);
            }
            return rtnFare;
        }


        public static decimal GetSimpleFareRate(int companyId, int vehicleTypeId, int tempFromLocId, int tempToLocId, string tempFromPostCode
                  , string tempToPostCode, ref string errorMsg, ref List<decimal> milesList, bool IsVia, bool CanCheckZoneWise, DateTime? pickupTime, ref decimal deadMileage, int fromLocTypeId, int toLocTypeId, ref bool companyFareExist, ref string estimatedTime,int? fromZoneId,int? toZoneId,ref bool IsMoreFareWise)
        {
            string miles = "";
            decimal rtnFare = 0.00m;
            string fromVal = tempFromPostCode;
            string toVal = tempToPostCode;


            bool surchargeRateFromAmountWise = false;
            bool surchargeRateToAmountWise = false;

            decimal surchargeRateFrom = 0.00m;
            decimal surchargeRateTo = 0.00m;

           // bool IsMoreFareWise = false;
            int actualVehicleTypeId = vehicleTypeId;
            try
            {

                if (tempFromPostCode.Length > 0)
                {
                    tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                    surchargeRateFrom = GetSurchargeRate(tempFromPostCode, ref surchargeRateFromAmountWise);
                }

                if (tempToPostCode.Length > 0)
                {
                    tempToPostCode = General.GetPostCodeMatch(tempToPostCode);
                    surchargeRateTo = GetSurchargeRate(tempToPostCode, ref surchargeRateToAmountWise);
                }


                string fromSingleHalfPostCode = string.Empty;
                string fromHalfPostCode = string.Empty;
                string startFromPostCode = "";
                //if (tempFromLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempFromPostCode))
                {
                    string[] fromArr = tempFromPostCode.Split(new char[] { ' ' });
                    startFromPostCode = fromArr[0];

                    fromHalfPostCode = startFromPostCode;

                    startFromPostCode = General.CheckIfSpecialPostCode(startFromPostCode);

                    fromSingleHalfPostCode = fromArr[0] + " " + fromArr[1][0];

                }

                //   }


                string ToSingleHalfPostCode = string.Empty;
                string toHalfPostCode = string.Empty;
                string startToPostCode = "";
                //if (tempToLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempToPostCode))
                {
                    string[] toArr = tempToPostCode.Split(new char[] { ' ' });

                    startToPostCode = toArr[0];
                    toHalfPostCode = startToPostCode;
                    startToPostCode = General.CheckIfSpecialPostCode(startToPostCode);

                    ToSingleHalfPostCode = toArr[0] + " " + toArr[1][0];
                }
                //}

                int defaultVehicleId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();

                if (vehicleTypeId != defaultVehicleId)
                {

                    if (AppVars.objPolicyConfiguration.ApplyPercentageWiseFareOn.ToBool())
                    {


                     


                        if (IsMoreFareWise == false)
                        {
                            if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                                                            ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))  || (c.FromZoneId==fromZoneId || c.OriginId == tempFromLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))))
                                                                  && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.ToZoneId==toZoneId || c.DestinationId == tempToLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))))))

                                                                  || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || ( c.OriginId == tempToLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode)))))
                                                                  && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || ( c.DestinationId == tempFromLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))))))
                                                                     )
                                                               && c.Fare.VehicleTypeId == defaultVehicleId
                                                                && (c.Fare.CompanyId == companyId || companyId == 0)).Count() > 0)

                                                          )
                            {

                                vehicleTypeId = defaultVehicleId;
                                IsMoreFareWise = true;
                            }
                        }


                    }
                    else
                    {


                        if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                                                                  ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                        && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                        || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                        && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                           )
                                                                     && c.Fare.VehicleTypeId == vehicleTypeId
                                                                      && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0)

                            && (General.GetQueryable<Fare_OtherCharge>(c => c.Fare.VehicleTypeId == vehicleTypeId
                                                                          && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0))
                        {

                            vehicleTypeId = defaultVehicleId;
                            IsMoreFareWise = true;
                        }
                    }


                }





                List<Fare_ChargesDetail> list = null;


                if (list == null || (list != null && list.Count() == 0))
                {
                    //int? zoneId = fromZoneId;
                    if (fromZoneId != 0)
                    {

                        list = General.GetQueryable<Fare_ChargesDetail>(c => (((c.FromZoneId == fromZoneId)
                                                                       && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()))) || c.DestinationId == tempToLocId))

                                                                          )

                                                                     && c.Fare.VehicleTypeId == vehicleTypeId
                            //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                      ).ToList();
                    }
                    else if (toZoneId != 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()))) || c.OriginId == tempFromLocId)
                                                               &&  c.ToZoneId==toZoneId)

                                                                  )

                                                             && c.Fare.VehicleTypeId == vehicleTypeId
                            //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                              ).ToList();

                    }
                }


                if (list == null || (list != null && list.Count() == 0))
                {

                    list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()))) || c.OriginId == tempFromLocId)
                                                                   && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()))) || c.DestinationId == tempToLocId))

                                                                      )

                                                                 && c.Fare.VehicleTypeId == vehicleTypeId
                        //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                  ).ToList();

                }

                if (list == null || (list != null && list.Count() == 0))
                {
                    list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                        // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();


                    if (list != null && list.Count > 0)
                    {
                        errorMsg = "Reverse found";

                    }

                }

                if ((tempFromLocId != 0 || tempToLocId != 0) && (list == null || (list != null && list.Count() == 0)))
                {
                    if (tempFromLocId > 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                               && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                               || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                               && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                  )

                                                             && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                              ).ToList();

                    }

                    if ((list == null || list.Count == 0) && tempToLocId > 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();

                    }



                    if ((list == null || list.Count == 0))
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();

                    }

                    if (list != null && list.Count > 0)
                    {
                        errorMsg = "Reverse found";

                    }


                }



                //if ((tempFromLocId == 0 && string.IsNullOrEmpty(startFromPostCode)) || (tempToLocId == 0 && string.IsNullOrEmpty(startToPostCode)))
                //    obj = null;


                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 2)
                {
                    tempFromPostCode = fromVal;
                    tempToPostCode = toVal;
                }


                miles = CalculateDistance(tempFromPostCode, tempToPostCode, ref estimatedTime).ToStr();



                milesList.Add(miles.ToDecimal());


                Fare_ChargesDetail obj = null;

                if (list != null)
                {
                    if (companyId != 0)
                    {
                        if (list.Count(c => c.Fare.CompanyId == companyId) > 0)
                        {
                            obj = list.FirstOrDefault(c => c.Fare.CompanyId == companyId);

                            companyFareExist = true;
                        }
                        else
                        {

                            if (General.GetQueryable<Taxi_Model.Fare>(c => c.CompanyId == companyId).Count() == 0)
                            {
                                obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                                companyFareExist = true;

                            }


                        }
                    }
                    else
                    {
                        obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                    }

                }


                if (obj != null)
                {

                    rtnFare = obj.Rate.ToDecimal();
                    deadMileage = 0;
                }
                else
                {

                    if (string.IsNullOrEmpty(tempFromPostCode) || string.IsNullOrEmpty(tempToPostCode))
                    {
                        errorMsg = "Error";
                        return 0;
                    }


                   

                  

                    // Calculate Fare Mileage Wise                
                    //  ISingleResult<ClsFares> objFare = General.SP_CalculateFares(vehicleTypeId.ToIntorNull(), companyId.ToIntorNull(), milesList.Sum().ToStr(), pickupTime);
                    decimal totalMiles = milesList.Sum();

                    var objFare = new TaxiDataContext().stp_CalculateGeneralFares(vehicleTypeId, companyId, totalMiles, pickupTime);

                    if (objFare != null)
                    {
                        var f = objFare.FirstOrDefault();

                        if ((f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                        {
                            rtnFare = f.totalFares.ToDecimal();

                            companyFareExist = f.CompanyFareExist.ToBool();
                        }
                        else
                            errorMsg = "Error";
                    }
                    else
                        errorMsg = "Error";



                    if (deadMileage > 0)
                    {
                        deadMileage = 0;

                        if (milesList.Count > 1)
                            milesList.RemoveAt(1);

                    }

                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                    {

                        decimal startRateTillMiles = General.GetObject<Fleet_VehicleType>(c => c.Id == vehicleTypeId).DefaultIfEmpty().StartRateValidMiles.ToDecimal();
                        if (startRateTillMiles > 0 && totalMiles > startRateTillMiles)
                        {

                            //  rtnFare = Math.Ceiling((rtnFare);
                            rtnFare = Math.Ceiling(rtnFare);
                        }
                    }
                    else
                    {
                        decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                        if (roundUp > 0)
                        {

                            rtnFare = (decimal)Math.Ceiling(rtnFare / roundUp) * roundUp;
                        }

                    }

                }

                if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == false)
                {

                    decimal totalSurchargePercentage = surchargeRateFrom + surchargeRateTo;

                    decimal fareSurchargePercent = (rtnFare * totalSurchargePercentage) / 100;
                    rtnFare = rtnFare + fareSurchargePercent;

                }
                else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == true)
                {

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }
                else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == false)
                {
                    surchargeRateTo = (rtnFare * surchargeRateTo) / 100;

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }
                else if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == true)
                {
                    surchargeRateFrom = (rtnFare * surchargeRateFrom) / 100;

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }



            }
            catch 
            {


                //   MessageBox.Show(ex.Message);
            }
            return rtnFare;
        }



        public static decimal GetSimpleFareRateBySubCompany(int companyId, int vehicleTypeId, int tempFromLocId, int tempToLocId, string tempFromPostCode
             , string tempToPostCode, ref string errorMsg, ref List<decimal> milesList, bool IsVia, bool CanCheckZoneWise, DateTime? pickupTime, ref decimal deadMileage, int fromLocTypeId, int toLocTypeId, ref bool companyFareExist, ref string estimatedTime, int? fromZoneId, int? toZoneId, ref bool IsMoreFareWise,int? subcompanyId)
        {
            string miles = "";
            decimal rtnFare = 0.00m;
            string fromVal = tempFromPostCode;
            string toVal = tempToPostCode;


            bool surchargeRateFromAmountWise = false;
            bool surchargeRateToAmountWise = false;

            decimal surchargeRateFrom = 0.00m;
            decimal surchargeRateTo = 0.00m;

            // bool IsMoreFareWise = false;
            int actualVehicleTypeId = vehicleTypeId;
            try
            {

                if (tempFromPostCode.Length > 0)
                {
                    tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                    surchargeRateFrom = GetSurchargeRate(tempFromPostCode, ref surchargeRateFromAmountWise);
                }

                if (tempToPostCode.Length > 0)
                {
                    tempToPostCode = General.GetPostCodeMatch(tempToPostCode);
                    surchargeRateTo = GetSurchargeRate(tempToPostCode, ref surchargeRateToAmountWise);
                }


                string fromSingleHalfPostCode = string.Empty;
                string fromHalfPostCode = string.Empty;
                string startFromPostCode = "";
                //if (tempFromLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempFromPostCode))
                {
                    string[] fromArr = tempFromPostCode.Split(new char[] { ' ' });
                    startFromPostCode = fromArr[0];

                    fromHalfPostCode = startFromPostCode;

                    startFromPostCode = General.CheckIfSpecialPostCode(startFromPostCode);

                    fromSingleHalfPostCode = fromArr[0] + " " + fromArr[1][0];

                }

                //   }


                string ToSingleHalfPostCode = string.Empty;
                string toHalfPostCode = string.Empty;
                string startToPostCode = "";
                //if (tempToLocId == 0)
                //{


                if (!string.IsNullOrEmpty(tempToPostCode))
                {
                    string[] toArr = tempToPostCode.Split(new char[] { ' ' });

                    startToPostCode = toArr[0];
                    toHalfPostCode = startToPostCode;
                    startToPostCode = General.CheckIfSpecialPostCode(startToPostCode);

                    ToSingleHalfPostCode = toArr[0] + " " + toArr[1][0];
                }
                //}

                int defaultVehicleId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();

                if (vehicleTypeId != defaultVehicleId)
                {

                    if (AppVars.objPolicyConfiguration.ApplyPercentageWiseFareOn.ToBool())
                    {





                        if (IsMoreFareWise == false)
                        {
                            if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                                                            ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.FromZoneId == fromZoneId || c.OriginId == tempFromLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode)))))
                                                                  && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.ToZoneId == toZoneId || c.DestinationId == tempToLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))))))

                                                                  || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || (c.OriginId == tempToLocId || (c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode)))))
                                                                  && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (c.DestinationId == tempFromLocId || (c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))))))
                                                                     )
                                                               && c.Fare.VehicleTypeId == defaultVehicleId
                                                                && (c.Fare.CompanyId == companyId || companyId == 0)).Count() > 0)

                                                          )
                            {

                                vehicleTypeId = defaultVehicleId;
                                IsMoreFareWise = true;
                            }
                        }


                    }
                    else
                    {


                        if ((General.GetQueryable<Fare_ChargesDetail>(c =>


                                                                  ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                        && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                        || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                        && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                           )
                                                                     && c.Fare.VehicleTypeId == vehicleTypeId
                                                                      && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0)

                            && (General.GetQueryable<Fare_OtherCharge>(c => c.Fare.VehicleTypeId == vehicleTypeId
                                                                          && (c.Fare.CompanyId == companyId || companyId == 0)).Count() == 0))
                        {

                            vehicleTypeId = defaultVehicleId;
                            IsMoreFareWise = true;
                        }
                    }


                }





                List<Fare_ChargesDetail> list = null;


                if (list == null || (list != null && list.Count() == 0))
                {
                    //int? zoneId = fromZoneId;
                    if (fromZoneId != 0)
                    {

                        list = General.GetQueryable<Fare_ChargesDetail>(c => (((c.FromZoneId == fromZoneId)
                                                                       && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()))) || c.DestinationId == tempToLocId))

                                                                          )

                                                                     && c.Fare.VehicleTypeId == vehicleTypeId
                            //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                      ).ToList();
                    }
                    else if (toZoneId != 0)
                    {

                        list = General.GetQueryable<Fare_ChargesDetail>(c => (((c.FromZoneId == toZoneId)
                                                                 && ((tempFromLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == fromVal.ToLower()))) || c.DestinationId == tempFromLocId))

                                                                    )

                                                               && c.Fare.VehicleTypeId == vehicleTypeId
                            //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                ).ToList();

                        //list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()))) || c.OriginId == tempFromLocId)
                        //                                       && c.ToZoneId == toZoneId)

                        //                                          )

                        //                                     && c.Fare.VehicleTypeId == vehicleTypeId
                       

                        //                                      ).ToList();

                    }
                }


                if (list == null || (list != null && list.Count() == 0))
                {

                    list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && ((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || (c.OriginLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.FromAddress.ToLower() == fromVal.ToLower()))) || c.OriginId == tempFromLocId)
                                                                   && ((tempToLocId == 0 && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || (c.DestinationLocationTypeId == Enums.LOCATION_TYPES.ADDRESS && c.ToAddress.ToLower() == toVal.ToLower()))) || c.DestinationId == tempToLocId))

                                                                      )

                                                                 && c.Fare.VehicleTypeId == vehicleTypeId
                        //&& (c.Fare.CompanyId == companyId || companyId == 0)

                                                                  ).ToList();

                }

                if (list == null || (list != null && list.Count() == 0))
                {
                    list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                                || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                                && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                        // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();


                    if (list != null && list.Count > 0)
                    {
                        errorMsg = "Reverse found";

                    }

                }

                if ((tempFromLocId != 0 || tempToLocId != 0) && (list == null || (list != null && list.Count() == 0)))
                {
                    if (tempFromLocId > 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                               && ((tempToLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode))) || c.DestinationId == tempToLocId))

                                                               || (((tempToLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))) || c.OriginId == tempToLocId)
                                                               && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                  )

                                                             && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                              ).ToList();

                    }

                    if ((list == null || list.Count == 0) && tempToLocId > 0)
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((tempFromLocId == 0 && c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))) || c.OriginId == tempFromLocId)
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                && ((tempFromLocId == 0 && c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode))) || c.DestinationId == tempFromLocId))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();

                    }



                    if ((list == null || list.Count == 0))
                    {
                        list = General.GetQueryable<Fare_ChargesDetail>(c => ((((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(fromHalfPostCode) || c.Gen_Location1.PostCode.Equals(startFromPostCode) || c.Gen_Location1.PostCode.Equals(tempFromPostCode))))
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(toHalfPostCode) || c.Gen_Location.PostCode.Equals(startToPostCode) || c.Gen_Location.PostCode.Equals(tempToPostCode)))))

                                                                || (((c.Gen_Location1.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location1.PostCode.Equals(ToSingleHalfPostCode) || c.Gen_Location1.PostCode.Equals(toHalfPostCode) || c.Gen_Location1.PostCode.Equals(startToPostCode) || c.Gen_Location1.PostCode.Equals(tempToPostCode))))
                                                                && ((c.Gen_Location.LocationTypeId == Enums.LOCATION_TYPES.POSTCODE && (c.Gen_Location.PostCode.Equals(fromSingleHalfPostCode) || c.Gen_Location.PostCode.Equals(fromHalfPostCode) || c.Gen_Location.PostCode.Equals(startFromPostCode) || c.Gen_Location.PostCode.Equals(tempFromPostCode)))))
                                                                   )

                                                              && c.Fare.VehicleTypeId == vehicleTypeId
                            // && (c.Fare.CompanyId == companyId || companyId == 0)

                                                               ).ToList();

                    }

                    if (list != null && list.Count > 0)
                    {
                        errorMsg = "Reverse found";

                    }


                }



                //if ((tempFromLocId == 0 && string.IsNullOrEmpty(startFromPostCode)) || (tempToLocId == 0 && string.IsNullOrEmpty(startToPostCode)))
                //    obj = null;


                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 2)
                {
                    tempFromPostCode = fromVal;
                    tempToPostCode = toVal;
                }


                miles = CalculateDistance(tempFromPostCode, tempToPostCode, ref estimatedTime).ToStr();



                milesList.Add(miles.ToDecimal());


                Fare_ChargesDetail obj = null;

                if (list != null)
                {
                    if (companyId != 0)
                    {
                        if (list.Count(c => c.Fare.CompanyId == companyId) > 0)
                        {
                            obj = list.FirstOrDefault(c => c.Fare.CompanyId == companyId);

                            companyFareExist = true;
                        }
                        else
                        {

                            if (General.GetQueryable<Taxi_Model.Fare>(c => c.CompanyId == companyId).Count() == 0)
                            {
                                obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                                companyFareExist = true;

                            }


                        }
                    }
                    else
                    {
                        obj = list.FirstOrDefault(c => c.Fare.CompanyId == null);
                    }

                }


                if (obj != null)
                {

                    rtnFare = obj.Rate.ToDecimal();
                    deadMileage = 0;
                }
                else
                {

                    if (string.IsNullOrEmpty(tempFromPostCode) || string.IsNullOrEmpty(tempToPostCode))
                    {
                        errorMsg = "Error";
                        return 0;
                    }






                    // Calculate Fare Mileage Wise                
                    //  ISingleResult<ClsFares> objFare = General.SP_CalculateFares(vehicleTypeId.ToIntorNull(), companyId.ToIntorNull(), milesList.Sum().ToStr(), pickupTime);
                    decimal totalMiles = milesList.Sum();

                    var objFare = new TaxiDataContext().stp_CalculateGeneralFaresBySubCompany(vehicleTypeId, companyId, totalMiles, pickupTime,subcompanyId);

                    if (objFare != null)
                    {
                        var f = objFare.FirstOrDefault();

                        if ((f.Result == "Success" || f.Result.ToStr().IsNumeric()))
                        {
                            rtnFare = f.totalFares.ToDecimal();

                            companyFareExist = f.CompanyFareExist.ToBool();
                        }
                        else
                            errorMsg = "Error";
                    }
                    else
                        errorMsg = "Error";



                    if (deadMileage > 0)
                    {
                        deadMileage = 0;

                        if (milesList.Count > 1)
                            milesList.RemoveAt(1);

                    }

                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                    {

                        decimal startRateTillMiles = General.GetObject<Fleet_VehicleType>(c => c.Id == vehicleTypeId).DefaultIfEmpty().StartRateValidMiles.ToDecimal();
                        if (startRateTillMiles > 0 && totalMiles > startRateTillMiles)
                        {

                            //  rtnFare = Math.Ceiling((rtnFare);
                            rtnFare = Math.Ceiling(rtnFare);
                        }
                    }
                    else
                    {
                        decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                        if (roundUp > 0)
                        {

                            rtnFare = (decimal)Math.Ceiling(rtnFare / roundUp) * roundUp;
                        }

                    }

                }

                if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == false)
                {

                    decimal totalSurchargePercentage = surchargeRateFrom + surchargeRateTo;

                    decimal fareSurchargePercent = (rtnFare * totalSurchargePercentage) / 100;
                    rtnFare = rtnFare + fareSurchargePercent;

                }
                else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == true)
                {

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }
                else if (surchargeRateFromAmountWise == true && surchargeRateToAmountWise == false)
                {
                    surchargeRateTo = (rtnFare * surchargeRateTo) / 100;

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }
                else if (surchargeRateFromAmountWise == false && surchargeRateToAmountWise == true)
                {
                    surchargeRateFrom = (rtnFare * surchargeRateFrom) / 100;

                    rtnFare = rtnFare + surchargeRateFrom + surchargeRateTo;
                }



            }
            catch
            {


                //   MessageBox.Show(ex.Message);
            }
            return rtnFare;
        }

        public static void GetZoneName(ref string zoneName, string postCode)
        {

            if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() == false)
                return;

            //   string postCode = General.GetPostCode(postCode);

            Gen_Coordinate objCoord = General.GetObject<Gen_Coordinate>(c => c.PostCode == postCode);

            if (objCoord != null)
            {

                double latitude = 0, longitude = 0;

                latitude = Convert.ToDouble(objCoord.Latitude);
                longitude = Convert.ToDouble(objCoord.Longitude);



                var plot = (from a in General.GetQueryable<Gen_Zone>(c => (c.ShapeType!=null && c.ShapeType=="circle")
                             ||  (c.MinLatitude != null && (latitude >= c.MinLatitude && latitude <= c.MaxLatitude)
                                                   && (longitude <= c.MaxLongitude && longitude >= c.MinLongitude)))

                            select a.Id).ToArray<int>();




                if (plot.Count() > 0)
                {
                    var list = (from p in plot
                                join a in General.GetQueryable<Gen_Zone_PolyVertice>(null) on p equals a.ZoneId
                                select a).ToList();




                    foreach (int plotId in plot)
                    {
                        if (FindPoint(latitude, longitude, list.Where(c => c.ZoneId == plotId).ToList()))
                        {
                            zoneName = list.Where(c => c.ZoneId == plotId).FirstOrDefault().DefaultIfEmpty().Gen_Zone.ZoneName.ToStr();
                            break;

                        }
                    }
                }



            }

        }


        public static bool is_in_circle(double circle_x, double circle_y, double r, double x, double y)
        {

            double d = new LatLng(Convert.ToDouble(circle_x), Convert.ToDouble(circle_y)).DistanceMiles(new LatLng(Convert.ToDouble(x), Convert.ToDouble(y)));

            //double d = Math.Sqrt(((circle_x - x) * (circle_x - x)) + ((circle_y - y) * (circle_y - y)));
            return d <= r;
        }

        public static bool FindPoint(double pointLat, double pointLng, List<Gen_Zone_PolyVertice> PontosPolig)
        {//                             X               y               
            int sides = PontosPolig.Count();
            int j = sides - 1;
            bool pointStatus = false;


            if (sides == 1)
            {

                double radius = Convert.ToDouble(PontosPolig[0].Diameter) / 2;
                double lat = Convert.ToDouble(PontosPolig[0].Latitude);
                double lng = Convert.ToDouble(PontosPolig[0].Longitude);
               
                pointStatus = is_in_circle(pointLat, pointLng, radius, lat, lng);
               
            }
            else
            {

                for (int i = 0; i < sides; i++)
                {
                    if (PontosPolig[i].Longitude < pointLng && PontosPolig[j].Longitude >= pointLng ||
                        PontosPolig[j].Longitude < pointLng && PontosPolig[i].Longitude >= pointLng)
                    {
                        if (PontosPolig[i].Latitude + (pointLng - PontosPolig[i].Longitude) /
                            (PontosPolig[j].Longitude - PontosPolig[i].Longitude) * (PontosPolig[j].Latitude - PontosPolig[i].Latitude) < pointLat)
                        {
                            pointStatus = !pointStatus;
                        }
                    }
                    j = i;
                }
            }
            return pointStatus;
        }



        public static string directionKey = string.Empty;

        public static decimal CalculateDistance(string origin, string destination)
        {

            if (origin.ToStr().Trim().Length == 0 || destination.ToStr().Trim().Length == 0)
                return 0.00m;

            decimal miles = 0.00m;


            if (origin.Contains("&"))
                origin = origin.Replace("&", "AND").Trim();


            if (destination.Contains("&"))
                destination = destination.Replace("&", "AND").Trim();



            if ((LastCalcDestination.Length > 0 && LastCalcDestination.Length > 0
              && origin == LastCalcOrigin && destination == LastCalcDestination) && LastCalcMileage > 0)
            {


                miles = LastCalcMileage;

                return miles;

            }


            try
            {

                stp_getCoordinatesByAddressResult pickupCoord = null;
                stp_getCoordinatesByAddressResult destCoord = null;

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    pickupCoord = db.stp_getCoordinatesByAddress(origin, GetPostCodeMatch(origin)).FirstOrDefault();
                    destCoord = db.stp_getCoordinatesByAddress(destination, GetPostCodeMatch(destination)).FirstOrDefault();

                }


                string originString = string.Empty;
                string destString = string.Empty;
                if (pickupCoord != null && pickupCoord.Latitude != null && pickupCoord.Latitude != 0)
                {
                    originString = pickupCoord.Latitude + "," + pickupCoord.Longtiude;
                }

                if (destCoord != null && destCoord.Latitude != null && destCoord.Latitude != 0)
                {
                    destString = destCoord.Latitude + "," + destCoord.Longtiude;


                }




                bool exist = false;



           

                // offlinedistance
                if (AppVars.objPolicyConfiguration.EnableOfflineDistance.ToBool() && exist == false)
                {
                    string time = string.Empty;
                    miles = AppVars.frmMDI.GetDistanceAndTime(origin, destination, ref time);
                    exist = true;
                    LastCalcOrigin = origin;
                    LastCalcDestination = destination;
                    LastCalcMileage = miles;
                    LastCalEstTime = time;

                }
              
                if (exist == false)
                {
                   

                    if (origin.Contains(".") && origin.Contains(",") && origin.Split(new char[] { ',' }).Count() == 2)
                    {

                    }
                    else if (originString.Length > 0)
                    {
                        origin = originString;

                    }
                    else
                    {
                        origin += ", UK";
                    }


                    if (destination.Contains(".") && destination.Contains(",") && destination.Split(new char[] { ',' }).Count() == 2)
                    {


                    }
                    else if (destString.Length > 0)
                    {

                        destination = destString;
                    }
                    else
                    {
                        destination += ", UK";
                    }


                    if (string.IsNullOrEmpty(directionKey))
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {

                            directionKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='google'").FirstOrDefault().ToStr().Trim();


                            if (directionKey.Length == 0)
                                directionKey = " ";
                            else
                                directionKey = "&key=" + directionKey;
                        }

                    }

                    if (AppVars.objPolicyConfiguration.PreferredShortestDistance.ToBool())
                    {


                        string URL = "";
                        //if (via == null || via.Length == 0)
                        //{

                        URL = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&alternatives=true&units=imperial" + directionKey + "&sensor=false";
                        URL = string.Format(URL, origin, destination);  
                        
                 

                        try
                        {
                            WebRequest request = HttpWebRequest.Create(URL);

                            request.Headers.Add("Authorization", "");
                            System.Net.WebRequest.DefaultWebProxy = null;
                            request.Proxy = System.Net.WebRequest.DefaultWebProxy;



                            WebResponse response = request.GetResponse();
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer parser = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string responseStringData = reader.ReadToEnd();
                                RootObject responseData = parser.Deserialize<RootObject>(responseStringData);
                                if (responseData != null && responseData.routes != null && responseData.routes.Count > 0)
                                {

                                    var objShortest = responseData.routes.OrderBy(x => x.legs[0].distance.value).FirstOrDefault();

                                    if (objShortest.legs[0].distance.text.ToStr().EndsWith(" mi"))
                                    {
                                        miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.text.Replace(" mi", "").Trim())))), 1);

                                    }
                                    else
                                    {
                                        miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.value)) / 1609.344)), 1);
                                    }



                                    //if (AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal() > 0)
                                    //{

                                    //    miles = Math.Ceiling(miles / AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal()) * AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal();

                                    //}


                                    //  miles = objShortest.legs[0].distance.value;
                                    LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                    LastCalcDestination = destination.Replace(", UK", "").Trim();
                                    LastCalcMileage = miles;
                                    LastCalEstTime = string.Empty;


                                }
                            }
                        }
                        catch
                        {



                        }

                        if (miles == 0 && AppVars.objPolicyConfiguration.DeadMileage.ToDecimal() > 0)
                        {

                            try
                            {

                                URL = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&alternatives=true&units=imperial" + directionKey + "&sensor=false";
                                      URL = string.Format(URL, origin, destination);
                                
                                
                                WebRequest request = HttpWebRequest.Create(URL);

                                WebResponse response = request.GetResponse();
                                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                                {
                                    System.Web.Script.Serialization.JavaScriptSerializer parser = new System.Web.Script.Serialization.JavaScriptSerializer();
                                    string responseStringData = reader.ReadToEnd();
                                    RootObject responseData = parser.Deserialize<RootObject>(responseStringData);
                                    if (responseData != null && responseData.routes != null && responseData.routes.Count > 0)
                                    {
                                        var objShortest = responseData.routes.OrderBy(x => x.legs[0].distance.value).FirstOrDefault();

                                        miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.value)) / 1609.344)), 1);

                                        //  miles = objShortest.legs[0].distance.value;
                                        LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                        LastCalcDestination = destination.Replace(", UK", "").Trim();
                                        LastCalcMileage = miles;
                                        LastCalEstTime = string.Empty;

                                       if(miles>0)
                                       {
                                             File.AppendAllText("directionoverlimit","DISTANCE WITH NEW KEY : " +DateTime.Now.ToStr()+miles);
                                       }
                                    }
                                }
                            }
                            catch
                            {


                            }
                        }                    
                    }
                    else
                    {


                        string applyShortesDistance = AppVars.objPolicyConfiguration.PreferredShortestDistance.ToBool() ? "&avoid=highways" : "";
                        string url2 = string.Empty;

                        if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr().Trim() == "AnzHawaiifive3Ztzx")
                        {
                            url2 = "https://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + applyShortesDistance + "&sensor=false&key=AIzaSyB5bNY_Yn3lZNy0FB7WxgAz7dgbE29jr50";
                        }
                        else
                        {
                            url2 = "https://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + applyShortesDistance + directionKey+ "&sensor=false";

                        }

                        using (XmlTextReader reader = new XmlTextReader(url2))
                        {
                            reader.WhitespaceHandling = WhitespaceHandling.Significant;


                            using (System.Data.DataSet ds = new System.Data.DataSet())
                            {
                                ds.ReadXml(reader);
                                DataTable dt = ds.Tables["distance"];
                                if (dt != null)
                                {



                                    decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
                                    decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

                                    decimal milKM = 0.621m;
                                    decimal milMeter = 0.00062137119m;

                                    miles = (milKM * distanceKm) + (milMeter * distanceMeter);

                                    dt.Dispose();
                                    dt = null;
                                    exist = true;
                                    LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                    LastCalcDestination = destination.Replace(", UK", "").Trim();
                                    LastCalcMileage = miles;
                                    LastCalEstTime = string.Empty;




                                }
                            }

                            reader.Close();

                        }


                    }


                    //if (exist == false)
                    //{
                    //    if (string.IsNullOrEmpty(BingKey))
                    //    {
                    //        using (TaxiDataContext db = new TaxiDataContext())
                    //        {

                    //            BingKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='bing'").FirstOrDefault().ToStr().Trim();


                    //            if (BingKey.Length == 0)
                    //                BingKey = " ";
                    //        }
                    //    }

                    //    if (BingKey.Trim().Length > 0)
                    //    {


                    //        using (XmlTextReader reader2 = new XmlTextReader("http://dev.virtualearth.net/REST/V1/Routes/Driving?o=xml&wp.0=" + origin + "&wp.1=" + destination + "&DistanceUnit=Mile&key=" + BingKey))
                    //        {
                    //            reader2.WhitespaceHandling = WhitespaceHandling.Significant;


                    //            using (System.Data.DataSet ds = new System.Data.DataSet())
                    //            {




                    //                ds.ReadXml(reader2);
                    //                DataTable dt = ds.Tables["RouteLeg"];


                    //                if (dt != null)
                    //                {
                    //                    DataRow dRow = dt.Rows.OfType<DataRow>().FirstOrDefault();

                    //                    if (dRow != null)
                    //                    {
                    //                        miles = dRow["TravelDistance"].ToDecimal();

                    //                        LastCalcOrigin = origin.Replace(", UK", "").Trim();
                    //                        LastCalcDestination = destination.Replace(", UK", "").Trim();
                    //                        LastCalcMileage = miles;
                    //                        LastCalEstTime = string.Empty;
                    //                    }

                    //                    dt.Dispose();
                    //                    dt = null;
                    //                }


                    //            }


                    //            reader2.Close();
                    //        }
                    //    }
                    //}

                    //if (miles > 500)
                    //{
                    //    miles = GetDistance.BetweenTwoPostCodes(origin, destination, "GB", GetDistance.Units.Miles).ToDecimal();
                    //}
                }

                // }

            }
            catch
            {



            }

            return miles;
        }



        public static decimal CalculateDistance(string origin, string destination, ref string estimatedTime, bool isVia)
        {

            if (origin.ToStr().Trim().Length == 0 || destination.ToStr().Trim().Length == 0)
                return 0.00m;



            decimal miles = 0.00m;

            if (origin.Contains("&"))
                origin = origin.Replace("&", "AND").Trim();


            if (destination.Contains("&"))
                destination = destination.Replace("&", "AND").Trim();


            if ((LastCalcDestination.Length > 0 && LastCalcDestination.Length > 0
                && origin == LastCalcOrigin && destination == LastCalcDestination) && LastCalcMileage > 0)
            {


                miles = LastCalcMileage;

                estimatedTime = LastCalEstTime;

                return miles;

            }


            try
            {
                bool exist = false;



                if (File.Exists(@Application.StartupPath + "\\PostCodeDistance\\" + origin + "_" + destination + ".csv"))
                {
                    try
                    {
                        string val = File.ReadAllText(@Application.StartupPath + "\\PostCodeDistance\\" + origin + "_" + destination + ".csv").ToStr();

                        if (val.Contains(","))
                        {
                            string[] arr = val.Split(',');

                            if (arr.Count() == 1)
                            {
                                miles = arr[0].ToDecimal();

                            }
                            else if (arr.Count() > 1)
                            {
                                miles = arr[0].ToDecimal();
                                estimatedTime += arr[1].ToStr() + " mins";
                            }

                            exist = true;
                        }
                    }
                    catch
                    {


                    }


                }

                // offlinedistance
                if (AppVars.objPolicyConfiguration.EnableOfflineDistance.ToBool() && exist == false)
                {

                    miles = AppVars.frmMDI.GetDistanceAndTime(origin, destination, ref estimatedTime);
                    exist = true;
                    LastCalcOrigin = origin;
                    LastCalcDestination = destination;
                    LastCalcMileage = miles;
                    LastCalEstTime = estimatedTime;
                }





                if (exist == false)
                {


                    if (origin.Contains(".") && origin.Contains(",") && origin.Split(new char[] { ',' }).Count() == 2)
                    {

                    }
                    else
                    {
                        origin += ", UK";
                    }


                    if (destination.Contains(".") && destination.Contains(",") && destination.Split(new char[] { ',' }).Count() == 2)
                    {


                    }
                    else
                    {
                        destination += ", UK";
                    }

                    //origin += ", UK";
                    //destination += ", UK";

                    if (AppVars.objPolicyConfiguration.PreferredShortestDistance.ToBool())
                    {

                        if (string.IsNullOrEmpty(directionKey))
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {

                                directionKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='google'").FirstOrDefault().ToStr().Trim();


                                if (directionKey.Length == 0)
                                    directionKey = " ";
                                else
                                    directionKey = "&key=" + directionKey;
                            }
                        }

                        string useAlternatives = isVia ? "" : "alternatives=true";


                        string URL = "";

                        URL = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}" + useAlternatives + "&units=imperial" + directionKey + "&sensor=false";
                        URL = string.Format(URL, origin, destination);

                        WebRequest request = HttpWebRequest.Create(URL);

                        request.Headers.Add("Authorization", "");
                        System.Net.WebRequest.DefaultWebProxy = null;
                        request.Proxy = System.Net.WebRequest.DefaultWebProxy;



                        WebResponse response = request.GetResponse();
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            System.Web.Script.Serialization.JavaScriptSerializer parser = new System.Web.Script.Serialization.JavaScriptSerializer();
                            RootObject responseData = parser.Deserialize<RootObject>(reader.ReadToEnd());
                            if (responseData != null && responseData.routes != null && responseData.routes.Count > 0)
                            {

                                var objShortest = responseData.routes.OrderBy(x => x.legs[0].distance.value).FirstOrDefault();

                                if (objShortest.legs[0].distance.text.ToStr().EndsWith(" mi"))
                                {
                                    miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.text.Replace(" mi", "").Trim())))), 1);

                                }
                                else
                                {
                                    miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.value)) / 1609.344)), 1);
                                }

                                //if (AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal() > 0)
                                //{

                                //    miles = Math.Ceiling(miles / AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal()) * AppVars.objPolicyConfiguration.RoundJourneyMiles.ToDecimal();

                                //}

                                estimatedTime += (objShortest.legs[0].duration.value / 60) + " mins";

                                //  miles = objShortest.legs[0].distance.value;
                                LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                LastCalcDestination = destination.Replace(", UK", "").Trim();
                                LastCalcMileage = miles;
                                LastCalEstTime = string.Empty;

                                exist = true;
                            }
                        }


                        if (exist == false && miles == 0)
                        {
                            Thread.Sleep(500);
                            response = HttpWebRequest.Create(URL).GetResponse();
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer parser = new System.Web.Script.Serialization.JavaScriptSerializer();
                                RootObject responseData = parser.Deserialize<RootObject>(reader.ReadToEnd());
                                if (responseData != null && responseData.routes != null && responseData.routes.Count > 0)
                                {

                                    var objShortest = responseData.routes.OrderBy(x => x.legs[0].distance.value).FirstOrDefault();

                                    miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.value)) / 1609.344)), 1);
                                    estimatedTime += (objShortest.legs[0].duration.value / 60) + " mins";

                                    //  miles = objShortest.legs[0].distance.value;
                                    LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                    LastCalcDestination = destination.Replace(", UK", "").Trim();
                                    LastCalcMileage = miles;
                                    LastCalEstTime = string.Empty;

                                    exist = true;
                                }
                            }

                        }

                    }
                    else
                    {
                        string applyShortesDistance = AppVars.objPolicyConfiguration.PreferredShortestDistance.ToBool() ? "&avoid=highways" : "";

                        string url2 = "https://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + applyShortesDistance +directionKey+ "&sensor=false";

                        using (XmlTextReader reader = new XmlTextReader(url2))
                        {
                            reader.WhitespaceHandling = WhitespaceHandling.Significant;

                            using (System.Data.DataSet ds = new System.Data.DataSet())
                            {
                                ds.ReadXml(reader);
                                DataTable dt = ds.Tables["distance"];
                                if (dt != null)
                                {

                                    decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
                                    decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

                                    decimal milKM = 0.621m;
                                    decimal milMeter = 0.00062137119m;

                                    miles = (milKM * distanceKm) + (milMeter * distanceMeter);

                                    dt.Dispose();
                                    dt = null;
                                    exist = true;
                                    LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                    LastCalcDestination = destination.Replace(", UK", "").Trim();
                                    LastCalcMileage = miles;
                                    LastCalEstTime = string.Empty;
                                }

                                if (estimatedTime != " ")
                                {
                                    DataTable dtTime = ds.Tables["duration"];
                                    if (dtTime != null)
                                    {
                                        var rows = ds.Tables["duration"].Rows.OfType<DataRow>().Where(c => c[2].ToStr() == string.Empty);
                                        estimatedTime = (Math.Round((rows.Sum(c => Convert.ToDouble(c[0])) / 60), 0)).ToStr();
                                        estimatedTime += " mins";
                                    }
                                }
                            }

                            reader.Close();
                        }
                    }


                    //string url2 = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&mode=driving&units=imperial";

                    //using (XmlTextReader reader = new XmlTextReader(url2))
                    //{
                    //    reader.WhitespaceHandling = WhitespaceHandling.Significant;

                    //    using (System.Data.DataSet ds = new System.Data.DataSet())
                    //    {
                    //        ds.ReadXml(reader);
                    //        DataTable dt = ds.Tables["distance"];
                    //        if (dt != null)
                    //        {
                    //            miles = dt.Rows[0][1].ToStr().Replace("mi", "").Trim().ToDecimal();

                    //            dt.Dispose();
                    //            dt = null;
                    //            exist = true;
                    //            LastCalcOrigin = origin.Replace(", UK", "").Trim();
                    //            LastCalcDestination = destination.Replace(", UK", "").Trim();
                    //            LastCalcMileage = miles;
                    //            LastCalEstTime = string.Empty;
                    //        }


                    //        if (estimatedTime != " ")
                    //        {

                    //            DataTable dtTime = ds.Tables["duration"];
                    //            if (dtTime != null)
                    //            {


                    //                estimatedTime = dtTime.Rows[0][1].ToStr();

                    //            }
                    //        }
                    //    }



                    //    reader.Close();





                    //}

                }



                //if (exist == false)
                //{

                //    if (string.IsNullOrEmpty(BingKey))
                //    {
                //        using (TaxiDataContext db = new TaxiDataContext())
                //        {

                //            BingKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='bing'").FirstOrDefault().ToStr().Trim();


                //            if (BingKey.Length == 0)
                //                BingKey = " ";
                //        }

                //    }

                //    if (BingKey.Trim().Length > 0)
                //    {

                //        using (XmlTextReader reader2 = new XmlTextReader("http://dev.virtualearth.net/REST/V1/Routes/Driving?o=xml&wp.0=" + origin + "&wp.1=" + destination + "&DistanceUnit=Mile&key=" + BingKey))
                //        {
                //            reader2.WhitespaceHandling = WhitespaceHandling.Significant;


                //            using (System.Data.DataSet ds = new System.Data.DataSet())
                //            {




                //                ds.ReadXml(reader2);
                //                DataTable dt = ds.Tables["RouteLeg"];


                //                if (dt != null)
                //                {
                //                    DataRow dRow = dt.Rows.OfType<DataRow>().FirstOrDefault();

                //                    if (dRow != null)
                //                    {
                //                        miles = dRow["TravelDistance"].ToDecimal();
                //                        LastCalcOrigin = origin.Replace(", UK", "").Trim();
                //                        LastCalcDestination = destination.Replace(", UK", "").Trim();
                //                        LastCalcMileage = miles;
                //                        LastCalEstTime = string.Empty;
                //                    }

                //                    dt.Dispose();
                //                    dt = null;
                //                }


                //            }


                //            reader2.Close();
                //        }
                //    }
                //}

                if (miles > 500)
                {
                    miles = GetDistance.BetweenTwoPostCodes(origin, destination, "GB", GetDistance.Units.Miles).ToDecimal();
                }
            }
            catch
            {



            }

            return miles;
        }

      


        public static decimal CalculateDistance(string origin, string destination, ref string estimatedTime)
        {

            if (origin.ToStr().Trim().Length == 0 || destination.ToStr().Trim().Length == 0)
                return 0.00m;



            decimal miles = 0.00m;

            if (origin.Contains("&"))
                origin = origin.Replace("&", "AND").Trim();


            if (destination.Contains("&"))
                destination = destination.Replace("&", "AND").Trim();


            if ((LastCalcDestination.Length > 0 && LastCalcDestination.Length > 0
                && origin == LastCalcOrigin && destination == LastCalcDestination) && LastCalcMileage > 0)
            {


                miles = LastCalcMileage;

                estimatedTime = LastCalEstTime;

                return miles;

            }


            try
            {
                bool exist = false;



                if (File.Exists(@Application.StartupPath + "\\PostCodeDistance\\" + origin + "_" + destination + ".csv"))
                {
                    try
                    {
                        string val = File.ReadAllText(@Application.StartupPath + "\\PostCodeDistance\\" + origin + "_" + destination + ".csv").ToStr();

                        if (val.Contains(","))
                        {
                            string[] arr = val.Split(',');

                            if (arr.Count() == 1)
                            {
                                miles = arr[0].ToDecimal();

                            }
                            else if (arr.Count() > 1)
                            {
                                miles = arr[0].ToDecimal();
                                estimatedTime += arr[1].ToStr() + " mins";
                            }

                            exist = true;
                        }
                    }
                    catch
                    {


                    }


                }

                // offlinedistance
                if (AppVars.objPolicyConfiguration.EnableOfflineDistance.ToBool() && exist == false)
                {

                    miles = AppVars.frmMDI.GetDistanceAndTime(origin, destination, ref estimatedTime);
                    exist = true;
                    LastCalcOrigin = origin;
                    LastCalcDestination = destination;
                    LastCalcMileage = miles;
                    LastCalEstTime = estimatedTime;
                }
               

                if (exist == false)
                {
                    if (origin.Contains(".") && origin.Contains(",") && origin.Split(new char[] { ',' }).Count() == 2)
                    {

                    }
                    else
                    {
                        origin += ", UK";
                    }


                    if (destination.Contains(".") && destination.Contains(",") && destination.Split(new char[] { ',' }).Count() == 2)
                    {


                    }
                    else
                    {
                        destination += ", UK";
                    }
                    


                        if (string.IsNullOrEmpty(directionKey))
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {

                                directionKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='google'").FirstOrDefault().ToStr().Trim();


                                if (directionKey.Length == 0)
                                    directionKey = " ";
                                else
                                    directionKey = "&key=" + directionKey;
                            }

                        }

                    if (AppVars.objPolicyConfiguration.PreferredShortestDistance.ToBool())
                    {


                        string URL = "";

                        URL = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&alternatives=true&units=imperial" + directionKey + "&sensor=false";
                        URL = string.Format(URL, origin, destination);



                        WebRequest request = HttpWebRequest.Create(URL);

                        request.Headers.Add("Authorization", "");
                        System.Net.WebRequest.DefaultWebProxy = null;
                        request.Proxy = System.Net.WebRequest.DefaultWebProxy;



                        WebResponse response = request.GetResponse();
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            System.Web.Script.Serialization.JavaScriptSerializer parser = new System.Web.Script.Serialization.JavaScriptSerializer();
                            RootObject responseData = parser.Deserialize<RootObject>(reader.ReadToEnd());
                            if (responseData != null && responseData.routes != null && responseData.routes.Count > 0)
                            {

                                var objShortest = responseData.routes.OrderBy(x => x.legs[0].distance.value).FirstOrDefault();

                                if (objShortest.legs[0].distance.text.ToStr().EndsWith(" mi"))
                                {
                                    miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.text.Replace(" mi","").Trim())))), 1);

                                }
                                else
                                {
                                    miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.value)) / 1609.344)), 1);
                                }


                             
                                
                                estimatedTime += (objShortest.legs[0].duration.value / 60) + " mins";

                                //  miles = objShortest.legs[0].distance.value;
                                LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                LastCalcDestination = destination.Replace(", UK", "").Trim();
                                LastCalcMileage = miles;
                                LastCalEstTime = string.Empty;

                                exist = true;
                            }
                        }


                        if (exist == false && miles == 0)
                        {
                            Thread.Sleep(500);
                             response = HttpWebRequest.Create(URL).GetResponse();
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer parser = new System.Web.Script.Serialization.JavaScriptSerializer();
                                RootObject responseData = parser.Deserialize<RootObject>(reader.ReadToEnd());
                                if (responseData != null && responseData.routes != null && responseData.routes.Count > 0)
                                {

                                    var objShortest = responseData.routes.OrderBy(x => x.legs[0].distance.value).FirstOrDefault();

                                    miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.value)) / 1609.344)), 1);
                                    estimatedTime += (objShortest.legs[0].duration.value / 60) + " mins";

                                    //  miles = objShortest.legs[0].distance.value;
                                    LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                    LastCalcDestination = destination.Replace(", UK", "").Trim();
                                    LastCalcMileage = miles;
                                    LastCalEstTime = string.Empty;

                                    exist = true;
                                }
                            }

                        }

                    }
                    else
                    {
                        string applyShortesDistance = AppVars.objPolicyConfiguration.PreferredShortestDistance.ToBool() ? "&avoid=highways" : "";

                        string url2 = "https://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + applyShortesDistance + directionKey + "&sensor=false";

                        using (XmlTextReader reader = new XmlTextReader(url2))
                        {
                            reader.WhitespaceHandling = WhitespaceHandling.Significant;

                            using (System.Data.DataSet ds = new System.Data.DataSet())
                            {
                                ds.ReadXml(reader);
                                DataTable dt = ds.Tables["distance"];
                                if (dt != null)
                                {

                                    decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
                                    decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

                                    decimal milKM = 0.621m;
                                    decimal milMeter = 0.00062137119m;

                                    miles = (milKM * distanceKm) + (milMeter * distanceMeter);

                                    dt.Dispose();
                                    dt = null;
                                    exist = true;
                                    LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                    LastCalcDestination = destination.Replace(", UK", "").Trim();
                                    LastCalcMileage = miles;
                                    LastCalEstTime = string.Empty;
                                }

                                if (estimatedTime != " ")
                                {
                                    DataTable dtTime = ds.Tables["duration"];
                                    if (dtTime != null)
                                    {
                                        var rows = ds.Tables["duration"].Rows.OfType<DataRow>().Where(c => c[2].ToStr() == string.Empty);
                                        estimatedTime = (Math.Round((rows.Sum(c => Convert.ToDouble(c[0])) / 60), 0)).ToStr();
                                        estimatedTime += " mins";
                                    }
                                }
                            }

                            reader.Close();
                        }
                    }


                    //string url2 = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&mode=driving&units=imperial";

                    //using (XmlTextReader reader = new XmlTextReader(url2))
                    //{
                    //    reader.WhitespaceHandling = WhitespaceHandling.Significant;

                    //    using (System.Data.DataSet ds = new System.Data.DataSet())
                    //    {
                    //        ds.ReadXml(reader);
                    //        DataTable dt = ds.Tables["distance"];
                    //        if (dt != null)
                    //        {
                    //            miles = dt.Rows[0][1].ToStr().Replace("mi", "").Trim().ToDecimal();

                    //            dt.Dispose();
                    //            dt = null;
                    //            exist = true;
                    //            LastCalcOrigin = origin.Replace(", UK", "").Trim();
                    //            LastCalcDestination = destination.Replace(", UK", "").Trim();
                    //            LastCalcMileage = miles;
                    //            LastCalEstTime = string.Empty;
                    //        }


                    //        if (estimatedTime != " ")
                    //        {

                    //            DataTable dtTime = ds.Tables["duration"];
                    //            if (dtTime != null)
                    //            {


                    //                estimatedTime = dtTime.Rows[0][1].ToStr();
                                  
                    //            }
                    //        }
                    //    }



                    //    reader.Close();





                    //}

                }



                //if (exist == false)
                //{

                //    if (string.IsNullOrEmpty(BingKey))
                //    {
                //        using (TaxiDataContext db = new TaxiDataContext())
                //        {

                //            BingKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='bing'").FirstOrDefault().ToStr().Trim();


                //            if (BingKey.Length == 0)
                //                BingKey = " ";
                //        }

                //    }

                //    if (BingKey.Trim().Length > 0)
                //    {

                //        using (XmlTextReader reader2 = new XmlTextReader("http://dev.virtualearth.net/REST/V1/Routes/Driving?o=xml&wp.0=" + origin + "&wp.1=" + destination + "&DistanceUnit=Mile&key=" + BingKey))
                //        {
                //            reader2.WhitespaceHandling = WhitespaceHandling.Significant;


                //            using (System.Data.DataSet ds = new System.Data.DataSet())
                //            {




                //                ds.ReadXml(reader2);
                //                DataTable dt = ds.Tables["RouteLeg"];


                //                if (dt != null)
                //                {
                //                    DataRow dRow = dt.Rows.OfType<DataRow>().FirstOrDefault();

                //                    if (dRow != null)
                //                    {
                //                        miles = dRow["TravelDistance"].ToDecimal();
                //                        LastCalcOrigin = origin.Replace(", UK", "").Trim();
                //                        LastCalcDestination = destination.Replace(", UK", "").Trim();
                //                        LastCalcMileage = miles;
                //                        LastCalEstTime = string.Empty;
                //                    }

                //                    dt.Dispose();
                //                    dt = null;
                //                }


                //            }


                //            reader2.Close();
                //        }
                //    }
                //}

                //if (miles > 500)
                //{
                //    miles = GetDistance.BetweenTwoPostCodes(origin, destination, "GB", GetDistance.Units.Miles).ToDecimal();
                //}
            }
            catch
            {



            }

            return miles;
        }



        public static string BingKey = string.Empty;
        public static string LastCalcOrigin = string.Empty;
        public static string LastCalcDestination = string.Empty;
        public static decimal LastCalcMileage = 0.00m;
        public static string LastCalEstTime = string.Empty;




        public static string CalculateEstimatedTime(string origin, string destination, string via)
        {
            if (origin.Contains("&"))
                origin = origin.Replace("&", "AND").Trim();


            if (destination.Contains("&"))
                destination = destination.Replace("&", "AND").Trim();

            string time = string.Empty;

            string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + ", UK&destination=" + destination + ", UK" + via + "&sensor=false";

          //  string url2 = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&mode=driving&units=imperial";


            using (System.Data.DataSet ds = new System.Data.DataSet())
            {
                using (XmlTextReader reader = new XmlTextReader(url2))
                {

                    ds.ReadXml(reader);
                }
                if (ds.Tables[0].Rows[0]["status"].ToString() == "ZERO_RESULTS" || ds.Tables[0].Rows[0]["status"].ToString() == "INVALID_REQUEST" || ds.Tables[0].Rows[0]["status"].ToString() == "NOT_FOUND")
                {

                    return string.Empty;
                }
                else
                {


                    if (string.IsNullOrEmpty(via))
                    {
                        DataTable dt = ds.Tables["duration"];
                        DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
                        time = row.ItemArray[1].ToString();
                        dt.Dispose();
                        dt = null;
                    }
                    //else
                    //{
                    //    var rows = ds.Tables["duration"].Rows.OfType<DataRow>().Where(c => c[2].ToStr() == string.Empty);

                    //    time = (Math.Round((rows.Sum(c => Convert.ToDouble(c[0])) / 60), 0)).ToStr();
                    //    time += " mins";
                    //}
                }
            }


            return time;
        }


      

        public static IList GetStationsList()
        {

            var list = (from a in General.GetQueryable<Gen_Location>(null)
                        where a.LocationTypeId == Enums.LOCATION_TYPES.UNDERGROUNDSTATION
                        orderby a.LocationName
                        select new
                        {
                            Id = a.Id,
                            Location = a.LocationName

                        }).ToList();

            return list;

        }

        public static IList GetAirportsList()
        {

            var list = (from a in General.GetQueryable<Gen_Location>(null)
                        where a.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT
                        orderby a.LocationName
                        select new
                        {
                            Id = a.Id,
                            Location = a.LocationName

                        }).ToList();

            return list;



        }


        public static List<Gen_ServiceCharge> GetServiceChargesList()
        {

           return General.GetQueryable<Gen_ServiceCharge>(null).ToList();
                      

          



        }



        public static void AddCheckBoxColumn(string name, RadGridView grid)
        {
            GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
            col.Width = 40;
            col.HeaderText = "";
            col.Name = name;
            grid.Columns.Add(col);

        }



        public static string CheckIfSpecialPostCode(string postcode)
        {
            try
            {

                if (((postcode.StartsWith("EC") || postcode.StartsWith("WC") || postcode.StartsWith("SE1") ||
                          postcode.StartsWith("SW1")) && postcode.Length == 4) || postcode.StartsWith("W1") && postcode.Length == 3)
                {

                    if (char.IsLetter(postcode[postcode.Length - 1]))
                    {
                        postcode = postcode.Remove(postcode.Length - 1);
                    }
                }
            }
            catch
            {


            }

            return postcode;

        }



        public static bool ReCallBookingFromOnBoardGrid(long jobId, int driverId)
        {

            try
            {

                bool rtn = true;

                (new TaxiDataContext()).stp_UpdateJob(jobId, driverId, Enums.BOOKINGSTATUS.WAITING, Enums.Driver_WORKINGSTATUS.AVAILABLE, AppVars.objPolicyConfiguration.SinBinTimer.ToInt());


                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Job>>" + jobId + "=2").Result.ToBool();
                    }


                }
                else
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Job>>" + jobId + "=2").Result.ToBool();
                    }

                }


                if (AppVars.objPolicyConfiguration.DespatchOfflineJobs.ToBool())
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_SaveOfflineMessage(jobId, driverId, "", AppVars.LoginObj.LoginName.ToStr(), "Cancelled Job>>" + jobId + "=2");
                    }

                }

                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).UpdateTodaysBookingWithStatus(jobId,new Booking { BookingStatusId=Enums.BOOKINGSTATUS.NOSHOW });
                return rtn;
            }
            catch (Exception ex)
            {

                return false;



            }

        }














        public static bool ReCallBooking(long jobId, int driverId)
        {

            try
            {

                bool rtn = true;

                (new TaxiDataContext()).stp_UpdateJob(jobId, driverId, Enums.BOOKINGSTATUS.WAITING, Enums.Driver_WORKINGSTATUS.AVAILABLE, AppVars.objPolicyConfiguration.SinBinTimer.ToInt());


                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Job>>" + jobId + "=2").Result.ToBool();
                    }


                }
                else
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Job>>" + jobId + "=2").Result.ToBool();
                    }

                }


                if (AppVars.objPolicyConfiguration.DespatchOfflineJobs.ToBool())
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_SaveOfflineMessage(jobId, driverId, "", AppVars.LoginObj.LoginName.ToStr(), "Cancelled Job>>" + jobId + "=2");
                    }

                }

                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshData();
                return rtn;
            }
            catch (Exception ex)
            {

                return false;
               


            }

        }


        public static bool ReCallBookingWithStatus(long jobId, int driverId,int? bookingStatusId,int? driverStatusId)
        {

            try
            {

                bool rtn = true;

                (new TaxiDataContext()).stp_UpdateJob(jobId, driverId, bookingStatusId, driverStatusId, AppVars.objPolicyConfiguration.SinBinTimer.ToInt());


                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Job>>" + jobId + "=2").Result.ToBool();
                    }


                }
                else
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Job>>" + jobId + "=2").Result.ToBool();
                    }

                }


                if (AppVars.objPolicyConfiguration.DespatchOfflineJobs.ToBool())
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_SaveOfflineMessage(jobId, driverId, "", AppVars.LoginObj.LoginName.ToStr(), "Cancelled Job>>" + jobId + "=2");
                    }

                }

                return rtn;
            }
            catch (Exception ex)
            {

                return false;
                //   ENUtils.ShowMessage(ex.Message);


            }

        }



        public static bool ReCallDespatchBooking(long jobId, int driverId)
        {

            try
            {

                bool rtn = true;

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    db.stp_RunProcedure("update fleet_driverqueuelist set currentjobid=null,driverworkstatusid=1 where driverid=" + driverId);


                }
                

                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Job>>" + jobId + "=2").Result.ToBool();
                    }


                }
                else
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Job>>" + jobId + "=2").Result.ToBool();
                    }

                }


               

                return rtn;
            }
            catch (Exception ex)
            {

                return false;
                //   ENUtils.ShowMessage(ex.Message);


            }

        }


        public static void EmailReport(ReportViewer reportViewer1, string fileTitle, string toEmail, string subject, string messageBody)
        {
            try
            {

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                string reportType = "pdf";

                byte[] bytes = reportViewer1.LocalReport.Render(
                 reportType, null, out mimeType, out encoding, out extension, out streamids, out warnings);



                string path = System.Windows.Forms.Application.StartupPath + "\\";

                path += fileTitle + ".pdf";


                FileInfo file = new FileInfo(path);



                using (FileStream fs = file.Create())
                {

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();


                    List<Attachment> myAttach = new List<Attachment>();
                    myAttach.Add(new System.Net.Mail.Attachment(file.FullName));

                    Taxi_AppMain.Email.Send(subject, messageBody, AppVars.LoginObj.Email, toEmail, myAttach);

                    ENUtils.ShowMessage("Email has been Sent Successfully");

                    myAttach[0].Dispose();

                    File.Delete(path);



                }



            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        public static void ShowCompanyInvoiceForm(long id)
        {


            frmInvoice frm = new frmInvoice();
            frm.OnDisplayRecord(id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmInvoice1");

            if (doc != null)
            {
                doc.Close();
            }

            MainMenuForm.MainMenuFrm.ShowForm(frm);

        }


        public static void ShowEscortInvoiceForm(long id)
        {


            frmEscortInvoice frm = new frmEscortInvoice();
            frm.OnDisplayRecord(id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmEscortInvoice1");

            if (doc != null)
            {
                doc.Close();
            }

            MainMenuForm.MainMenuFrm.ShowForm(frm);

        }


        public static void ShowCompanyCourierInvoiceForm(long id)
        {


            frmInvoiceCourier frm = new frmInvoiceCourier();
            frm.OnDisplayRecord(id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmInvoiceCourier1");

            if (doc != null)
            {
                doc.Close();
            }

            MainMenuForm.MainMenuFrm.ShowForm(frm);

        }


        public static void ShowPreCompanyInvoiceForm(long id)
        {


            frmPreAccInvoice frm = new frmPreAccInvoice();
            frm.OnDisplayRecord(id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmPreAccInvoice1");

            if (doc != null)
            {
                doc.Close();
            }

            MainMenuForm.MainMenuFrm.ShowForm(frm);

        }

        public static void ShowCustomerInvoiceForm(long id)
        {


            frmCustomerInvoice frm = new frmCustomerInvoice();
            frm.OnDisplayRecord(id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmCustomerInvoice1");

            if (doc != null)
            {
                doc.Close();
            }

            MainMenuForm.MainMenuFrm.ShowForm(frm);

        }




        public static void ShowPreCustomerInvoiceForm(long id)
        {


            frmPreCustomerInvoice frm = new frmPreCustomerInvoice();
            frm.OnDisplayRecord(id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmPreCustomerInvoice1");

            if (doc != null)
            {
                doc.Close();
            }

            MainMenuForm.MainMenuFrm.ShowForm(frm);

        }




        public static void DisposeForm(Form frm)
        {

            frm.Dispose();

        }


        public static bool SendAdvanceBookingSMS(string toNumber, ref string returnMsg, string advanceText,int smsType)
        {
            bool rtn = true;

            try
            {
                if (string.IsNullOrEmpty(toNumber)) return false;


                string companyName = AppVars.objSubCompany.CompanyName.ToStr().Trim();
                string companyTelNo = AppVars.objSubCompany.TelephoneNo.ToStr().Trim();

                string text = advanceText.ToStr();


                if (string.IsNullOrEmpty(text))
                {
                    returnMsg = "* Advance Booking Text not exist in System Policy.";

                }
                else
                {
                    int idx = -1;
                    if (toNumber.StartsWith("044") == true)
                    {
                        idx = toNumber.IndexOf("044");
                        toNumber = toNumber.Substring(idx + 3);
                        toNumber = toNumber.Insert(0, "+44");


                    }

                    if (toNumber.StartsWith("07"))
                    {
                        toNumber = toNumber.Substring(1);
                    }

                    if (toNumber.StartsWith("0440") == false || toNumber.StartsWith("+440") == false)
                        toNumber = toNumber.Insert(0, "+44");



                    string message = string.Empty;

                    // Send To Customer
                    Thread thread = new Thread(delegate()
                    {
                        rtn = SendAdvanceSMS(toNumber, text,smsType);
                    });

                    thread.Priority = ThreadPriority.Lowest;
                    thread.Start();




                }


                if (AppVars.objPolicyConfiguration.DisablePopupNotifications.ToBool() == false)
                {

                    RadDesktopAlert advanceBookingText = new RadDesktopAlert();


                    if (rtn == false)
                    {
                        advanceBookingText.CaptionText = "Advance Booking Confirmation Text Send Failed...";

                        advanceBookingText.ContentText = returnMsg.Insert(0, "* ");
                    }
                    else
                    {
                        advanceBookingText.CaptionText = "Advance Booking Confirmation Text.";
                        advanceBookingText.ContentText = "Confirmation Text has been Sent Successfully ";
                    }

                    advanceBookingText.Show();
                }
                return rtn;

            }
            catch (Exception ex)
            {
                return false;

            }
        }



        //public static bool SendAdvanceBookingSMS(string toNumber, ref string returnMsg, string advanceText)
        //{
        //    bool rtn = true;

        //    try
        //    {
        //        if (string.IsNullOrEmpty(toNumber)) return false;


        //        string companyName = AppVars.objSubCompany.CompanyName.ToStr().Trim();
        //        string companyTelNo = AppVars.objSubCompany.TelephoneNo.ToStr().Trim();

        //        string text = advanceText.ToStr();


        //        if (string.IsNullOrEmpty(text))
        //        {
        //            returnMsg = "* Advance Booking Text not exist in System Policy.";

        //        }
        //        else
        //        {
        //            int idx = -1;
        //            if (toNumber.StartsWith("044") == true)
        //            {
        //                idx = toNumber.IndexOf("044");
        //                toNumber = toNumber.Substring(idx + 3);
        //                toNumber = toNumber.Insert(0, "+44");


        //            }

        //            if (toNumber.StartsWith("07"))
        //            {
        //                toNumber = toNumber.Substring(1);
        //            }

        //            if (toNumber.StartsWith("0440") == false || toNumber.StartsWith("+440") == false)
        //                toNumber = toNumber.Insert(0, "+44");



        //            string message = string.Empty;

        //            // Send To Customer
        //            Thread thread = new Thread(delegate()
        //            {
        //                rtn = SendAdvanceSMS(toNumber, text);
        //            });

        //            thread.Priority = ThreadPriority.Lowest;
        //            thread.Start();




        //        }


        //        RadDesktopAlert advanceBookingText = new RadDesktopAlert();


        //        if (rtn == false)
        //        {
        //            advanceBookingText.CaptionText = "Advance Booking Confirmation Text Send Failed...";

        //            advanceBookingText.ContentText = returnMsg.Insert(0, "* ");
        //        }
        //        else
        //        {
        //            advanceBookingText.CaptionText = "Advance Booking Confirmation Text.";
        //            advanceBookingText.ContentText = "Confirmation Text has been Sent Successfully ";
        //        }

        //        advanceBookingText.Show();

        //        return rtn;

        //    }
        //    catch (Exception ex)
        //    {
        //        return false;

        //    }
        //}


        public static bool SendAdvanceSMS(string mobileNo, string message,int smsType)
        {
            System.Threading.Thread.Sleep(1000);
            EuroSMS objSMS = new EuroSMS();
            string returnMsg = "";

            objSMS.BookingSMSAccountType = smsType;
            objSMS.ToNumber = mobileNo;
            objSMS.Message = message;
            return objSMS.Send(ref returnMsg);
        }

        //public static bool CancelBooking(long bookingId)
        //{




        //    bool rtn = false;
        //    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to cancel this Booking ?", "Cancel Booking", MessageBoxButtons.YesNo))
        //    {

        //        frmCancelReason frm = new frmCancelReason(bookingId);
        //        frm.ShowDialog();
        //        frm.Dispose();             

        //        General.RefreshListWithoutSelected<frmBookingsList>("frmBookingsList1");
        //        General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");

        //        rtn = true;

        //    }
        //    return rtn;
        //}


        public static bool ConfirmQuotation(long bookingId)
        {
            bool rtn = false;
            if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to confirm this Quotation ?", "Confirm Quotation", MessageBoxButtons.YesNo))
            {
                BookingBO objMaster = new BookingBO();

                objMaster.GetByPrimaryKey(bookingId);
                objMaster.Edit();
                objMaster.Current.IsQuotation = false;


                if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.CANCELLED)
                {
                    objMaster.Current.BookingStatusId = 1;

                    objMaster.Current.CancelReason="";
                    objMaster.Current.JobCancelledBy = "";
                    objMaster.Current.JobCancelledOn = null;

                }
                objMaster.Save();

                General.AddBookingLog(objMaster.Current.Id, "Quotation " + objMaster.Current.BookingNo + " Confirmed by " + AppVars.LoginObj.UserName);


                if (AppVars.objPolicyConfiguration.SendDirectBookingConfirmationEmail.ToBool())
                {
                    try
                    {
                        if (objMaster.PrimaryKeyValue != null)
                        {
                            new Thread(delegate()
                            {

                                JATEmail.SendDirectBookingConfirmationEmail(objMaster.Current);
                            }).Start();

                        }
                    }
                    catch
                    {


                    }
                }

                rtn = true;
            }
            return rtn;
        }





        public static void ClearDriverCurrentJob(long Id)
        {

            try
            {
                DriverQueueBO objDriverQueueBO = new DriverQueueBO();
                objDriverQueueBO.GetByPrimaryKey(Id);

                if (objDriverQueueBO.Current != null)
                {
                    long? jobId = objDriverQueueBO.Current.CurrentJobId;

                    objDriverQueueBO.Current.QueueDateTime = DateTime.Now;
                    objDriverQueueBO.Current.CurrentJobId = null;
                    objDriverQueueBO.Current.CurrentDestinationPostCode = string.Empty;
                    objDriverQueueBO.Current.DriverWorkStatusId = Enums.Driver_WORKINGSTATUS.AVAILABLE;
                    objDriverQueueBO.Current.WaitSinceOn = DateTime.Now;

                    objDriverQueueBO.Save();


                    if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                    {
                        //For TCP Connection
                        if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                        {
                            if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() && objDriverQueueBO.Current.Fleet_Driver.DefaultIfEmpty().HasPDA.ToBool())
                            {
                                if (jobId != null)
                                {
                                    string msg = "request pda=" + objDriverQueueBO.Current.DriverId + "=" + jobId + "=<<Cleared Job>>" + jobId + "=3";
                                    General.SendMessageToPDA(msg);
                                }
                            }
                        }

                    }
                    else
                    {
                        //For TCP Connection
                        if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                        {
                            if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() && objDriverQueueBO.Current.Fleet_Driver.DefaultIfEmpty().HasPDA.ToBool())
                            {
                                if (jobId != null)
                                {
                                    string msg = "request pda=" + objDriverQueueBO.Current.DriverId + "=" + jobId + "=<<Cleared Job>>" + jobId + "=3";
                                    General.SendMessageToPDA(msg);
                                }
                            }
                        }

                    }


                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_BookingLog(jobId, AppVars.LoginObj.UserName.ToStr(), "Job is Manually cleared by Controller (" + AppVars.LoginObj.UserName.ToStr() + ")");

                        db.stp_UpdateJobStatus(jobId, Enums.BOOKINGSTATUS.DISPATCHED);
                    }




                }
            }
            catch
            {


            }
        }


        public static void LogoutDriver(long id)
        {
            try
            {

                DriverQueueBO objMaster = new DriverQueueBO();
                objMaster.GetByPrimaryKey(id);

                if (objMaster.Current != null)
                {
                    objMaster.Current.Status = false;
                    objMaster.Current.DriverWorkStatusId = Enums.Driver_WORKINGSTATUS.AVAILABLE;
                    objMaster.Current.LogoutDateTime = DateTime.Now;
                    objMaster.Save();

                    //     AppVars.frmDashBoard.LoadDriverWaitingGrid();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);


            }
        }

        public static void BroadCastRefreshWaitingDrivers()
        {

            new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_WAITING_AND_DASBOARD);

        }

        public static void BroadCastRefresh(string refreshType)
        {

            new BroadcasterData().BroadCastToAll(refreshType);

        }


        //public static void RefreshWaitingDrivers()
        //{
        //    ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).RefreshWaitingDrivers();
         

        //}

        //public static void RefreshDriversGrids()
        //{
        //    ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).RefreshWaitingAndOnBoardDrivers();
         

        //}




        public static void UpdateDriverQueue(long Id, long? jobId, string destinationPostCode)
        {

            DriverQueueBO objDriverQue = new DriverQueueBO();

            objDriverQue.GetByPrimaryKey(Id);
            if (objDriverQue.Current != null)
            {
                objDriverQue.Current.CurrentJobId = jobId;
                objDriverQue.Current.CurrentDestinationPostCode = destinationPostCode;

                objDriverQue.Current.QueueDateTime = DateTime.Now;

                objDriverQue.Save();

            }

        }


        public static void OnDespatchUpdateDriverQueue(long Id, long? jobId, string destinationPostCode)
        {



            DriverQueueBO objDriverQue = new DriverQueueBO();

            //  int MaxQueueNo = General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true).Max(s => s.QueueNo).DefaultIfEmpty().ToInt();

            objDriverQue.GetByPrimaryKey(Id);
            if (objDriverQue.Current != null)
            {
                objDriverQue.Current.CurrentJobId = jobId;
                objDriverQue.Current.CurrentDestinationPostCode = destinationPostCode;

                objDriverQue.Current.QueueDateTime = DateTime.Now;
                //   objDriverQue.Current.QueueNo = MaxQueueNo + 1;
                objDriverQue.Current.DriverWorkStatusId = Enums.Driver_WORKINGSTATUS.NOTAVAILABLE;
                objDriverQue.Save();

            }

        }


        public static void UpdateDriverQueue(int? driverId)
        {

            DriverQueueBO objDriverQue = new DriverQueueBO();



            Fleet_DriverQueueList objthisDriver = General.GetObject<Fleet_DriverQueueList>(c => c.Status == true && c.DriverId == driverId);

            if (objthisDriver != null)
            {
                objDriverQue.GetByPrimaryKey(objthisDriver.Id);
                if (objDriverQue.Current != null)
                {
                    objDriverQue.Current.QueueDateTime = DateTime.Now;
                    //    objDriverQue.Current.QueueNo = MaxQueueNo + 1;
                    objDriverQue.Save();

                }
            }

        }

        public static IList GetHospitalsList()
        {

            var list = (from a in General.GetQueryable<Gen_Location>(null)
                        where a.LocationTypeId == Enums.LOCATION_TYPES.HOSPITAL
                        orderby a.LocationName
                        select new
                        {
                            Id = a.Id,
                            Location = a.LocationName

                        }).ToList();
            return list;

        }

        public static IList GetHotelsList()
        {

            var list = (from a in General.GetQueryable<Gen_Location>(null)
                        where a.LocationTypeId == Enums.LOCATION_TYPES.HOTELS
                        orderby a.LocationName
                        select new
                        {
                            Id = a.Id,
                            Location = a.LocationName

                        }).ToList();
            return list;

        }


        public static IList GetTownsList()
        {

            var list = (from a in General.GetQueryable<Gen_Location>(null)
                        where a.LocationTypeId == Enums.LOCATION_TYPES.TOWN
                        orderby a.LocationName
                        select new
                        {
                            Id = a.Id,
                            Location = a.LocationName

                        }).ToList();
            return list;

        }


        public static bool ShowDespatchFOJForm(Booking obj)
        {
            bool rtn = false;

            frmDespatchJob frm = new frmDespatchJob(obj, true);

            frm.ShowDialog();


            if (frm.SmsThread != null)
                frm.SmsThread.Start();

            rtn = frm.SuccessDespatched;

            frm.Dispose();

            return rtn;

        }




        public static bool ShowReDespatchForm(Booking obj)
        {
            bool rtn = false;

            frmDespatchJob frm = new frmDespatchJob(obj);
            frm.ReDespatchJob = true;
            frm.ShowDialog();



            if (frm.SmsThread != null)
                frm.SmsThread.Start();

            rtn = frm.SuccessDespatched;

            frm.Dispose();

            return rtn;

        }


        public static bool ShowDespatchForm(Booking obj)
        {
            bool rtn = false;

            frmDespatchJob frm = new frmDespatchJob(obj);

            frm.ShowDialog();



            if (frm.SmsThread != null)
                frm.SmsThread.Start();

            rtn = frm.SuccessDespatched;

            frm.Dispose();

            return rtn;

        }


        public static bool ShowDespatchForm(long jobId, int? driverId)
        {

            bool rtn = false;

            frmDespatchJob frm = new frmDespatchJob(jobId, AppVars.BLData.Get<Booking>(c => c.Id == jobId), driverId
                                        , AppVars.BLData.Get<Fleet_Driver>(c => c.Id == driverId), false);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            if (frm.SmsThread != null)
                frm.SmsThread.Start();

            rtn = frm.SuccessDespatched;

            frm.Dispose();


            return rtn;
        }



        public static int? ShowLocationForm(int? locTypeId)
        {


            frmLocations frm = new frmLocations(locTypeId);
            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.ShowDialog();

            return frm.LocationId;

        }

        public static List<object[]> ShowCustomerInvoiceBookingMultiLister(Expression<Func<Booking, bool>> _condition, Expression<Func<Invoice_Charge, bool>> _invoiceCondition, string[] hiddenColumns, DateTime? fromDate, DateTime? tillDate)
        {
            var list1 = General.GetGeneralList<Booking>(_condition).Where(b => b.BookingDate.ToDate() >= fromDate && b.BookingDate.ToDate() <= tillDate);
            var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);

            var list = (from b in list1
                        join c in list2 on b.Id equals c.BookingId into table2
                        from c in table2.DefaultIfEmpty()
                        where (c == null)
                        select new
                        {
                            Id = b.Id,


                            BookingDate = b.BookingDate,
                            PickupDate = b.PickupDateTime,

                            RefNo = b.BookingNo,
                            Vehicle = b.Fleet_VehicleType.VehicleType,
                            OrderNo = b.OrderNo,
                            PupilNo = b.PupilNo,
                            Passenger = b.CustomerName,
                            PickupPoint = b.FromAddress,
                            Destination = b.ToAddress,
                            Charges = b.CustomerPrice.ToDecimal(),

                            CompanyId = b.CompanyId,
                            CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                            Parking = b.ParkingCharges.ToDecimal(),
                            Waiting = b.WaitingCharges.ToDecimal(),
                            ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                            MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                            Congtion = b.CongtionCharges.ToDecimal(),
                            Total = b.TotalCharges.ToDecimal(),
                            PaymentTypeId = b.PaymentTypeId


                        }).ToList();


            Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


            frm.HiddenColumns = hiddenColumns;
            frm.ShowDialog();

            return frm.ListofData;

        }

        public static List<object[]> ShowBookingMultiLister(Expression<Func<Booking, bool>> _condition, Expression<Func<Invoice_Charge, bool>> _invoiceCondition, string[] hiddenColumns, DateTime? fromDate, DateTime? tillDate)
        {
            var list1 = General.GetGeneralList<Booking>(_condition).Where(b => b.BookingDate.ToDate() >= fromDate && b.BookingDate.ToDate() <= tillDate);
            var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);

            var list = (from b in list1
                        join c in list2 on b.Id equals c.BookingId into table2
                        from c in table2.DefaultIfEmpty()
                        where (c == null)
                        select new
                        {
                            Id = b.Id,


                            BookingDate = b.BookingDate,
                            PickupDate = b.PickupDateTime,

                            RefNo = b.BookingNo,
                            Vehicle = b.Fleet_VehicleType.VehicleType,
                            OrderNo = b.OrderNo,
                            PupilNo = b.PupilNo,
                            Passenger = b.CustomerName,
                            PickupPoint = b.FromAddress,
                            Destination = b.ToAddress,
                            Charges = b.FareRate.ToDecimal(),

                            CompanyId = b.CompanyId,
                            CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                            Parking = b.ParkingCharges.ToDecimal(),
                            Waiting = b.WaitingCharges.ToDecimal(),
                            ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                            MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                            Congtion = b.CongtionCharges.ToDecimal(),
                            Total = b.TotalCharges.ToDecimal()


                        }).ToList();


            Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


            frm.HiddenColumns = hiddenColumns;
            frm.ShowDialog();

            return frm.ListofData;

        }


        public static List<object[]> ShowEscortInvoiceBookingMultiLister(Expression<Func<Booking, bool>> _condition, Expression<Func<Invoice_Charge, bool>> _invoiceCondition, string[] hiddenColumns, Func<Booking, bool> _condition2)
        {
            var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);
            var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);

            var list = (from b in list1
                        join c in list2 on b.Id equals c.BookingId into table2
                        from c in table2.DefaultIfEmpty()
                        where (c == null)
                        select new
                        {
                            Id = b.Id,


                            BookingDate = b.BookingDate,
                            PickupDate = b.PickupDateTime,

                            RefNo = b.BookingNo,
                            Vehicle = b.Fleet_VehicleType.VehicleType,
                            OrderNo = b.OrderNo,
                            PupilNo = b.PupilNo,
                            Passenger = b.CustomerName,
                            PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                            Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                            //  Charges = b.FareRate.ToDecimal(),
                            Charges = b.EscortPrice.ToDecimal(),
                            CompanyId = b.CompanyId,
                            CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                            Parking = b.ParkingCharges.ToDecimal(),
                            Waiting = b.WaitingCharges.ToDecimal(),
                            ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                            MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                            Congtion = b.CongtionCharges.ToDecimal(),
                            Description = "",
                            Total = b.EscortPrice.ToDecimal(),
                            //  BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                            BookedBy = b.BookedBy.ToStr(),
                            Fare = b.FareRate.ToDecimal(),

                            AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                            PaymentTypeId = b.PaymentTypeId
                        }).ToList();


            Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


            frm.HiddenColumns = hiddenColumns;
            frm.ShowDialog();

            return frm.ListofData;

        }





        public static List<object[]> ShowBookingMultiLister(Expression<Func<Booking, bool>> _condition, Expression<Func<Invoice_Charge, bool>> _invoiceCondition, string[] hiddenColumns, Func<Booking, bool> _condition2)
        {
            var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);
            var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);

            var list = (from b in list1
                        join c in list2 on b.Id equals c.BookingId into table2
                        from c in table2.DefaultIfEmpty()
                        where (c == null)
                        select new
                        {
                            Id = b.Id,


                            BookingDate = b.BookingDate,
                            PickupDate = b.PickupDateTime,

                            RefNo = b.BookingNo,
                            Vehicle = b.Fleet_VehicleType.VehicleType,
                            OrderNo = b.OrderNo,
                            PupilNo = b.PupilNo,
                            Passenger = b.CustomerName,
                            PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                            Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                            //  Charges = b.FareRate.ToDecimal(),
                            Charges = b.CompanyPrice.ToDecimal(),
                            CompanyId = b.CompanyId,
                            CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                            Parking = b.ParkingCharges.ToDecimal(),
                            Waiting = b.WaitingCharges.ToDecimal(),
                            ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                            MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                            Congtion = b.CongtionCharges.ToDecimal(),
                            Description = "",
                            Total = b.TotalCharges.ToDecimal(),
                            //  BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                            BookedBy = b.BookedBy.ToStr(),
                            Fare = b.FareRate.ToDecimal(),

                            AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                            PaymentTypeId = b.PaymentTypeId
                        }).ToList();


            Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


            frm.HiddenColumns = hiddenColumns;
            frm.ShowDialog();

            return frm.ListofData;

        }


        public static List<object[]> ShowBookingMultiLister(Expression<Func<Booking, bool>> _condition, Expression<Func<Invoice_Charge, bool>> _invoiceCondition, string[] hiddenColumns, Func<Booking, bool> _condition2, string templateName)
        {

            Taxi_AppMain.frmLister frm = null;
            if (templateName == "Template13" || templateName == "Template24")
            {
                var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);
                var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);

                var list = (from b in list1
                            join c in list2 on b.Id equals c.BookingId into table2
                            from c in table2.DefaultIfEmpty()
                            where (c == null)
                            select new
                            {
                                Id = b.Id,


                                BookingDate = b.BookingDate,
                                PickupDate = b.PickupDateTime,

                                RefNo = b.BookingNo,
                                Vehicle = b.Fleet_VehicleType.VehicleType,
                                OrderNo = b.OrderNo,
                                PupilNo = b.PupilNo,
                                Passenger = b.CustomerName,
                                PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                                Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                                //  Charges = b.FareRate.ToDecimal(),
                                Charges = b.CompanyPrice.ToDecimal(),
                                CompanyId = b.CompanyId,
                                CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                                Parking = b.ParkingCharges.ToDecimal(),
                                Waiting = b.WaitingCharges.ToDecimal(),
                                ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                                MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                                Congtion = b.CongtionCharges.ToDecimal(),
                                Description = "",
                                Total = (b.CompanyPrice.ToDecimal() + b.WaitingCharges.ToDecimal()),
                                //  BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                                BookedBy = b.BookedBy.ToStr(),
                                Fare = b.FareRate.ToDecimal(),

                                AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                                PaymentTypeId = b.PaymentTypeId
                            }).ToList();


                frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


                frm.HiddenColumns = hiddenColumns;
                frm.ShowDialog();


            }
            else if (templateName == "Template14")
            {
                var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);
                var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);

                var list = (from b in list1
                            join c in list2 on b.Id equals c.BookingId into table2
                            from c in table2.DefaultIfEmpty()
                            where (c == null)
                            select new
                            {
                                Id = b.Id,


                                BookingDate = b.BookingDate,
                                PickupDate = b.PickupDateTime,

                                RefNo = b.BookingNo,
                                Vehicle = b.Fleet_VehicleType.VehicleType,
                                OrderNo = b.OrderNo,
                                PupilNo = b.PupilNo,
                                Passenger = b.CustomerName,
                                PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                                Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                                //  Charges = b.FareRate.ToDecimal(),
                                Charges = b.CompanyPrice.ToDecimal(),
                                CompanyId = b.CompanyId,
                                CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                                Parking = b.ParkingCharges.ToDecimal(),
                                Waiting = b.WaitingCharges.ToDecimal(),
                                ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                                MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                                Congtion = b.CongtionCharges.ToDecimal(),
                                Description = "",
                                Total = (b.CompanyPrice.ToDecimal() + b.WaitingCharges.ToDecimal() + b.ParkingCharges.ToDecimal() + b.ExtraDropCharges.ToDecimal()),
                                //  BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                                BookedBy = b.BookedBy.ToStr(),
                                Fare = b.FareRate.ToDecimal(),

                                AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                                PaymentTypeId = b.PaymentTypeId
                            }).ToList();


                frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


                frm.HiddenColumns = hiddenColumns;
                frm.ShowDialog();


            }
            else
            {
                var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);
                var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);

                var list = (from b in list1
                            join c in list2 on b.Id equals c.BookingId into table2
                            from c in table2.DefaultIfEmpty()
                            where (c == null)
                            select new
                            {
                                Id = b.Id,


                                BookingDate = b.BookingDate,
                                PickupDate = b.PickupDateTime,

                                RefNo = b.BookingNo,
                                Vehicle = b.Fleet_VehicleType.VehicleType,
                                OrderNo = b.OrderNo,
                                PupilNo = b.PupilNo,
                                Passenger = b.CustomerName,
                                PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                                Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                                //  Charges = b.FareRate.ToDecimal(),
                                Charges = b.CompanyPrice.ToDecimal(),
                                CompanyId = b.CompanyId,
                                CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                                Parking = b.ParkingCharges.ToDecimal(),
                                Waiting = b.WaitingCharges.ToDecimal(),
                                ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                                MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                                Congtion = b.CongtionCharges.ToDecimal(),
                                Description = "",
                                Total = b.TotalCharges.ToDecimal(),
                                //  BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                                BookedBy = b.BookedBy.ToStr(),
                                Fare = b.FareRate.ToDecimal(),

                                AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                                PaymentTypeId = b.PaymentTypeId
                            }).ToList();


                frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


                frm.HiddenColumns = hiddenColumns;
                frm.ShowDialog();


            }



            return (frm != null ? frm.ListofData : null);

        }



        public static List<object[]> ShowCourierBookingMultiLister(Expression<Func<Booking, bool>> _condition, Expression<Func<Invoice_Charge, bool>> _invoiceCondition, string[] hiddenColumns, Func<Booking, bool> _condition2)
        {
            var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);
            var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);


            var list = (from b in list1
                        join c in list2 on b.Id equals c.BookingId into table2
                        from c in table2.DefaultIfEmpty()
                        where (c == null)
                        select new
                        {
                            Id = b.Id,


                            BookingDate = b.BookingDate,
                            PickupDate = b.PickupDateTime,

                            RefNo = b.BookingNo,
                            Vehicle = b.Fleet_VehicleType.VehicleType,
                            OrderNo = b.OrderNo,
                            PupilNo = b.PupilNo,
                            Passenger = b.CustomerName,
                            PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                            Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                            //  Charges = b.FareRate.ToDecimal(),
                            Charges = 0,
                            CompanyId = b.CompanyId,
                            CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                            Parking = b.ParkingCharges.ToDecimal(),
                            Waiting = b.WaitingCharges.ToDecimal(),
                            ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                            MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                            Congtion = b.CongtionCharges.ToDecimal(),
                            Description = "",
                            Total = b.TotalCharges.ToDecimal(),
                            BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                            Fare = b.FareRate.ToDecimal(),

                            AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                            PaymentTypeId = b.PaymentTypeId,
                            
                        }).ToList();


            Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


            frm.HiddenColumns = hiddenColumns;
            frm.ShowDialog();

            return frm.ListofData;

        }



        public static List<object[]> ShowDriverTransactionBookingMultiLister(Expression<Func<Booking, bool>> _condition, string[] hiddenColumns, Func<Booking, bool> _condition2)
        {
            var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);


            var list = (from b in list1

                        select new
                        {
                            Id = b.Id,
                            BookingDate = b.BookingDate,
                            PickupDate = b.PickupDateTime,

                            RefNo = b.BookingNo,
                            Vehicle = b.Fleet_VehicleType.VehicleType,
                            OrderNo = b.OrderNo,
                            PupilNo = b.PupilNo,
                            Passenger = b.CustomerName,
                            PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                            Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                            //  Charges = b.FareRate.ToDecimal(),
                            Charges = b.CompanyPrice.ToDecimal(),
                            CompanyId = b.CompanyId,
                            Account = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                            Parking = b.CongtionCharges.ToDecimal(),
                            Waiting = b.MeetAndGreetCharges.ToDecimal(),
                            ExtraDrop = b.ParkingCharges.ToDecimal(),
                            MeetAndGreet = b.WaitingCharges.ToDecimal(),
                            Congtion = b.CongtionCharges.ToDecimal(),
                            Description = "",
                            Total = b.FareRate.ToDecimal() + b.CongtionCharges.ToDecimal() + b.MeetAndGreetCharges.ToDecimal(),
                            BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                            Fare = b.FareRate.ToDecimal(),

                            AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                            b.IsCommissionWise,
                            b.DriverCommissionType,
                            b.DriverCommission,
                            b.AgentCommission

                        }).ToList();


            Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


            frm.HiddenColumns = hiddenColumns;
            frm.ShowDialog();

            return frm.ListofData;

        }


        public static List<object[]> ShowDriverRentTransactionExpensesBookingMultiLister(Expression<Func<Booking, bool>> _condition, string[] hiddenColumns, Func<Booking, bool> _condition2)
        {

            TaxiDataContext db = new TaxiDataContext();

            var list1 = db.GetTable<Booking>().Where(_condition).Where(_condition2);
            var rentList =db.GetTable<DriverRent_Charge>().Where(c => c.BookingId != null && c.TransId != null);
      
            var list = (from b in list1
                        join b1 in rentList on b.Id equals b1.BookingId into table2
                        from b1 in table2.DefaultIfEmpty()
                     
                        where (b1 == null)
                        select new
                        {
                            Id = b.Id,
                            BookingDate = b.BookingDate,
                            PickupDate = b.PickupDateTime,

                            RefNo = b.BookingNo,
                            Vehicle = b.Fleet_VehicleType.VehicleType,
                            OrderNo = b.OrderNo,
                            PupilNo = b.PupilNo,
                            Passenger = b.CustomerName,
                            PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                            Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                            //  Charges = b.FareRate.ToDecimal(),
                            Charges = b.CompanyPrice.ToDecimal(),
                            CompanyId = b.CompanyId,
                            Account = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                            Parking = b.CongtionCharges,
                            Waiting = b.MeetAndGreetCharges,
                            ExtraDrop = b.ParkingCharges,
                            MeetAndGreet = b.MeetAndGreetCharges,
                            Congtion = b.CongtionCharges,
                            Description = "",
                            Total = b.FareRate + b.CongtionCharges + b.MeetAndGreetCharges,
                            BookedBy = "",
                            Fare = b.FareRate,

                            AccountType =0,
                            b.IsCommissionWise,
                            b.DriverCommissionType,
                            b.DriverCommission,
                            b.AgentCommission,
                            DropOffCharge = 0.00m,
                            PickupCharge = 0.00m,
                            PaymentTypeId = b.PaymentTypeId

                        }).ToList();


            Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


            frm.HiddenColumns = hiddenColumns;
            frm.ShowDialog();

            frm.Dispose();
            db.Dispose();

            return frm.ListofData;

        }


        public static List<object[]> ShowDriverRentTransactionExpensesWithWaitingBookingMultiLister(Expression<Func<Booking, bool>> _condition, string[] hiddenColumns, Func<Booking, bool> _condition2)
        {

            Taxi_AppMain.frmLister frm = null;
            using (TaxiDataContext db = new TaxiDataContext())
            {

                var list1 = db.GetTable<Booking>().Where(_condition);
                var rentList = db.GetTable<DriverRent_Charge>().Where(c => c.BookingId != null );

                var list = (from b in list1
                            join b1 in rentList on b.Id equals b1.BookingId into table2

                            join b2 in db.Fleet_VehicleTypes on b.VehicleTypeId equals b2.Id
                            join b3 in db.Gen_Companies on b.CompanyId equals b3.Id

                            from b1 in table2.DefaultIfEmpty()

                            where (b1 == null)
                            select new
                            {
                                Id = b.Id,
                                BookingDate = b.BookingDate,
                                PickupDate = b.PickupDateTime,
                                RefNo = b.BookingNo,
                                Vehicle = b2.VehicleType,
                                OrderNo = b.OrderNo,
                                PupilNo = b.PupilNo,
                                Passenger = b.CustomerName,
                                PickupPoint =  b.FromAddress,
                                Destination =  b.ToAddress,
                                //  Charges = b.FareRate.ToDecimal(),
                                Charges = b.CompanyPrice,
                                CompanyId = b.CompanyId,
                                Account = b3.CompanyName,
                                Parking = b.CongtionCharges,
                                Waiting = b.MeetAndGreetCharges,
                                ExtraDrop = b.ParkingCharges,
                                MeetAndGreet = b.MeetAndGreetCharges,
                                Congtion = b.CongtionCharges,
                                Description = "",
                                Total = b.FareRate,  // 19 index
                                BookedBy = "",
                                Fare = b.FareRate,

                                AccountType = 0,
                                b.IsCommissionWise,
                                b.DriverCommissionType,
                                b.DriverCommission,
                                b.AgentCommission,
                                DropOffCharge = 0.00m,
                                PickupCharge = 0.00m,
                                PaymentTypeId = b.PaymentTypeId,
                                ExtrCharges = b.ExtraDropCharges
                            }).ToList();


                frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


                frm.HiddenColumns = hiddenColumns;
                frm.ShowDialog();

                frm.Dispose();
            }

            return frm.ListofData;

        }

        //public static List<object[]> ShowDriverRentTransactionExpensesBookingMultiLister(Expression<Func<Booking, bool>> _condition, string[] hiddenColumns, Func<Booking, bool> _condition2)
        //{
        //    var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);

        //    var list3 = General.GetQueryable<Gen_SysPolicy_AirportDropOffCharge>(null).ToList();
        //    var list4 = General.GetQueryable<Gen_SysPolicy_AirportPickupCharge>(null).ToList();
        //    var list = (from b in list1
        //                join d1 in list3 on b.ToLocId equals d1.AirportId into table3
        //                from d1 in table3.DefaultIfEmpty()
        //                join p1 in list4 on b.FromLocId equals p1.AirportId into table4
        //                from p1 in table4.DefaultIfEmpty()

        //                select new
        //                {
        //                    Id = b.Id,
        //                    BookingDate = b.BookingDate,
        //                    PickupDate = b.PickupDateTime,

        //                    RefNo = b.BookingNo,
        //                    Vehicle = b.Fleet_VehicleType.VehicleType,
        //                    OrderNo = b.OrderNo,
        //                    PupilNo = b.PupilNo,
        //                    Passenger = b.CustomerName,
        //                    PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
        //                    Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
        //                    //  Charges = b.FareRate.ToDecimal(),
        //                    Charges = b.CompanyPrice.ToDecimal(),
        //                    CompanyId = b.CompanyId,
        //                    Account = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
        //                    Parking = b.CongtionCharges.ToDecimal(),
        //                    Waiting = b.MeetAndGreetCharges.ToDecimal(),
        //                    ExtraDrop = b.ParkingCharges.ToDecimal(),
        //                    MeetAndGreet = b.WaitingCharges.ToDecimal(),
        //                    Congtion = b.CongtionCharges.ToDecimal(),
        //                    Description = "",
        //                    Total = b.FareRate.ToDecimal() + b.CongtionCharges.ToDecimal() + b.MeetAndGreetCharges.ToDecimal(),
        //                    BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
        //                    Fare = b.FareRate.ToDecimal(),

        //                    AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 2,
        //                    b.IsCommissionWise,
        //                    b.DriverCommissionType,
        //                    b.DriverCommission,
        //                    b.AgentCommission,
        //                     DropOffCharge = d1 != null ? d1.Charges.ToDecimal() : 0,
        //                    PickupCharge = p1 != null ? p1.Charges.ToDecimal() : 0
                        
        //                }).ToList();


        //    Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


        //    frm.HiddenColumns = hiddenColumns;
        //    frm.ShowDialog();

        //    return frm.ListofData;

        //}
        public static List<object[]> ShowDriverCommTransactionBookingMultiLister(Expression<Func<Booking, bool>> _condition, string[] hiddenColumns, Expression<Func<Booking, bool>> _condition2)
        {


            // TaxiDataContext db = new TaxiDataContext();
            // var list1 =db.GetTable<Booking>().Where(_condition).Where(_condition2);


            //var list2 =db.GetTable<Fleet_DriverCommision_Charge>();

            //var list = (from b in list1
            //            join c in list2 on b.Id equals c.BookingId into table2
            //            from c in table2.DefaultIfEmpty()
            //            where (c == null)

            //            select new
            //            {
            //                Id = b.Id,
            //                BookingDate = b.BookingDate,
            //                PickupDate = b.PickupDateTime,

            //                RefNo = b.BookingNo,
            //                Vehicle = b.Fleet_VehicleType.VehicleType,
            //                OrderNo = b.OrderNo,
            //                PupilNo = b.PupilNo,
            //                Passenger = b.CustomerName,
            //                PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
            //                Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
            //                //  Charges = b.FareRate.ToDecimal(),
            //                Charges = b.CompanyPrice!=null ? b.CompanyPrice:0.00m,
            //                CompanyId = b.CompanyId,
            //                Account = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
            //                Parking =b.CongtionCharges!=null ? b.CongtionCharges:0.00m,
            //                Waiting = b.MeetAndGreetCharges,
            //                ExtraDrop = b.ParkingCharges,
            //                MeetAndGreet = b.WaitingCharges,
            //                Congtion = b.CongtionCharges,
            //                Description = "",
            //                Total = b.FareRate + b.CongtionCharges + b.MeetAndGreetCharges,
            //                BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
            //                Fare = b.FareRate,

            //                AccountType = b.CompanyId != null && b.Gen_Company.AccountTypeId!=null ? b.Gen_Company.AccountTypeId : 0,
            //                b.IsCommissionWise,
            //                b.DriverCommissionType,
            //                b.DriverCommission,
            //                b.AgentCommission
            //            }).ToList();


            var list1 = General.GetGeneralList<Booking>(_condition2);


            var list2 = General.GetGeneralList<Fleet_DriverCommision_Charge>(null);


            var list = (from b in list1
                        join c in list2 on b.Id equals c.BookingId into table2
                        from c in table2.DefaultIfEmpty()
                        where (c == null)

                        select new
                        {
                            Id = b.Id,
                            BookingDate = b.BookingDate,
                            PickupDate = b.PickupDateTime,

                            RefNo = b.BookingNo,
                            Vehicle = b.Fleet_VehicleType.VehicleType,
                            OrderNo = b.OrderNo,
                            PupilNo = b.PupilNo,
                            Passenger = b.CustomerName,
                            PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                            Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                            //  Charges = b.FareRate.ToDecimal(),
                            Charges = b.CompanyPrice.ToDecimal(),
                            CompanyId = b.CompanyId,
                            Account = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                            Parking = b.CongtionCharges.ToDecimal(),
                            Waiting = b.MeetAndGreetCharges.ToDecimal(),
                            ExtraDrop = b.ParkingCharges.ToDecimal(),
                            MeetAndGreet = b.WaitingCharges.ToDecimal(),
                            Congtion = b.CongtionCharges.ToDecimal(),
                            Description = "",
                            Total = b.FareRate.ToDecimal() + b.CongtionCharges.ToDecimal() + b.MeetAndGreetCharges.ToDecimal(),
                            BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                            Fare = b.FareRate.ToDecimal(),

                            AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                            b.IsCommissionWise,
                            b.DriverCommissionType,
                            b.DriverCommission,
                            b.AgentCommission
                        }).ToList();


            //  db.Dispose();

            Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


            frm.HiddenColumns = hiddenColumns;
            frm.ShowDialog();

            return frm.ListofData;

        }





        public static List<object[]> ShowDriverCommTransactionExpenseBookingMultiLister(Expression<Func<Booking, bool>> condition, string[] hiddenColumns, Expression<Func<Booking, bool>> condition2)
        {


            // TaxiDataContext db = new TaxiDataContext();
            // var list1 =db.GetTable<Booking>().Where(_condition).Where(_condition2);


            //var list2 =db.GetTable<Fleet_DriverCommision_Charge>();

            //var list = (from b in list1
            //            join c in list2 on b.Id equals c.BookingId into table2
            //            from c in table2.DefaultIfEmpty()
            //            where (c == null)

            //            select new
            //            {
            //                Id = b.Id,
            //                BookingDate = b.BookingDate,
            //                PickupDate = b.PickupDateTime,

            //                RefNo = b.BookingNo,
            //                Vehicle = b.Fleet_VehicleType.VehicleType,
            //                OrderNo = b.OrderNo,
            //                PupilNo = b.PupilNo,
            //                Passenger = b.CustomerName,
            //                PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
            //                Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
            //                //  Charges = b.FareRate.ToDecimal(),
            //                Charges = b.CompanyPrice!=null ? b.CompanyPrice:0.00m,
            //                CompanyId = b.CompanyId,
            //                Account = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
            //                Parking =b.CongtionCharges!=null ? b.CongtionCharges:0.00m,
            //                Waiting = b.MeetAndGreetCharges,
            //                ExtraDrop = b.ParkingCharges,
            //                MeetAndGreet = b.WaitingCharges,
            //                Congtion = b.CongtionCharges,
            //                Description = "",
            //                Total = b.FareRate + b.CongtionCharges + b.MeetAndGreetCharges,
            //                BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
            //                Fare = b.FareRate,

            //                AccountType = b.CompanyId != null && b.Gen_Company.AccountTypeId!=null ? b.Gen_Company.AccountTypeId : 0,
            //                b.IsCommissionWise,
            //                b.DriverCommissionType,
            //                b.DriverCommission,
            //                b.AgentCommission
            //            }).ToList();

            //join patient in dc_tpatient on m.prid equals patient.prid into patientGroup from p in patientGroup.DefaultIfEmpty()

            var list1 = General.GetGeneralList<Booking>(condition2);


            var list2 = General.GetGeneralList<Fleet_DriverCommision_Charge>(null);
            var list3 = General.GetQueryable<Gen_SysPolicy_AirportDropOffCharge>(null).ToList();
            var list4 = General.GetQueryable<Gen_SysPolicy_AirportPickupCharge>(null).ToList();

            var list = (from b in list1
                        join c in list2 on b.Id equals c.BookingId into table2
                        from c in table2.DefaultIfEmpty()
                        join d1 in list3 on b.ToLocId equals d1.AirportId into table3
                        from d1 in table3.DefaultIfEmpty()
                        join p1 in list4 on b.FromLocId equals p1.AirportId into table4
                        from p1 in table4.DefaultIfEmpty()

                        where (c == null)

                        select new
                        {
                            Id = b.Id,
                            BookingDate = b.BookingDate,
                            PickupDate = b.PickupDateTime,

                            RefNo = b.BookingNo,
                            Vehicle = b.Fleet_VehicleType.VehicleType,
                            OrderNo = b.OrderNo,
                            PupilNo = b.PupilNo,
                            Passenger = b.CustomerName,
                            PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                            Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                            //  Charges = b.FareRate.ToDecimal(),
                            Charges = b.CompanyPrice.ToDecimal(),
                            CompanyId = b.CompanyId,
                            Account = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                            Parking = b.CongtionCharges.ToDecimal(),
                            Waiting = b.MeetAndGreetCharges.ToDecimal(),
                            ExtraDrop = b.ParkingCharges.ToDecimal(),
                            MeetAndGreet = b.WaitingCharges.ToDecimal(),
                            Congtion = b.CongtionCharges.ToDecimal(),
                            Description = "",
                            Total = b.FareRate.ToDecimal() + b.CongtionCharges.ToDecimal() + b.MeetAndGreetCharges.ToDecimal(),
                            BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                            Fare = b.FareRate.ToDecimal(),
                              AccountType =b.PaymentTypeId,

                          //  AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : Enums.ACCOUNT_TYPE.CASH,
                            b.IsCommissionWise,
                            b.DriverCommissionType,
                            b.DriverCommission,
                            b.AgentCommission,
                            // DropOffCharge = d1 != null ?  d1.Charges:d1.Id>0? d1.Id:01,
                            DropOffCharge = d1 != null ? d1.Charges.ToDecimal() : 0,
                            PickupCharge = p1 != null ? p1.Charges.ToDecimal() : 0,
                            PaymentTypeId=b.PaymentTypeId,
                            Payment=b.Gen_PaymentType.PaymentType,
                            ServiceCharges=b.ServiceCharges.ToDecimal(),
                            ApplyServiceCharge=b.ApplyServiceCharges.ToBool(),
                            b.CustomerPrice,

                        }).ToList();
            //eventStatus.onWaitingList ? "waitinglist" : "accepted";

            //  db.Dispose();

            Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


            frm.HiddenColumns = hiddenColumns;
            frm.ShowDialog();

            return frm.ListofData;

        }




        public static List<object[]> ShowDriverCommTransactionExpense2BookingMultiLister(Expression<Func<Booking, bool>> condition, string[] hiddenColumns, Expression<Func<Booking, bool>> condition2)
        {

         
            var list1 = General.GetGeneralList<Booking>(condition2);


            var list2 = General.GetGeneralList<Fleet_DriverCommision_Charge>(null);
            var list3 = General.GetQueryable<Gen_SysPolicy_AirportDropOffCharge>(null).ToList();
            var list4 = General.GetQueryable<Gen_SysPolicy_AirportPickupCharge>(null).ToList();

            var list = (from b in list1
                        join c in list2 on b.Id equals c.BookingId into table2
                        from c in table2.DefaultIfEmpty()
                        join d1 in list3 on b.ToLocId equals d1.AirportId into table3
                        from d1 in table3.DefaultIfEmpty()
                        join p1 in list4 on b.FromLocId equals p1.AirportId into table4
                        from p1 in table4.DefaultIfEmpty()

                        where (c == null)

                        select new
                        {
                            Id = b.Id,
                            BookingDate = b.BookingDate,
                            PickupDate = b.PickupDateTime,

                            RefNo = b.BookingNo,
                            Vehicle = b.Fleet_VehicleType.VehicleType,
                            OrderNo = b.OrderNo,
                            PupilNo = b.PupilNo,
                            Passenger = b.CustomerName,
                            PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                            Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                            //  Charges = b.FareRate.ToDecimal(),
                            Charges = b.CompanyPrice.ToDecimal(),
                            CompanyId = b.CompanyId,
                            Account = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                            Parking = b.CongtionCharges.ToDecimal(),
                            Waiting = b.MeetAndGreetCharges.ToDecimal(),
                            ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                            MeetAndGreet = b.WaitingCharges.ToDecimal(),
                            Congtion = b.CongtionCharges.ToDecimal(),
                            Description = "",
                            Total = b.FareRate.ToDecimal() + b.CongtionCharges.ToDecimal() + b.MeetAndGreetCharges.ToDecimal(),
                            BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                            Fare = b.FareRate.ToDecimal(),

                            AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : Enums.ACCOUNT_TYPE.CASH,
                            b.IsCommissionWise,
                            b.DriverCommissionType,
                            b.DriverCommission,
                            b.AgentCommission,
                            // DropOffCharge = d1 != null ?  d1.Charges:d1.Id>0? d1.Id:01,
                            DropOffCharge = d1 != null ? d1.Charges.ToDecimal() : 0,
                            PickupCharge = p1 != null ? p1.Charges.ToDecimal() : 0,
                            PaymentTypeId = b.PaymentTypeId,
                            Payment = b.Gen_PaymentType.PaymentType,
                            ServiceCharges=b.ServiceCharges.ToDecimal()
                           


                        }).OrderBy(c=>c.PickupDate).ToList();
            //eventStatus.onWaitingList ? "waitinglist" : "accepted";

            //  db.Dispose();

            Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


            frm.HiddenColumns = hiddenColumns;
            frm.ShowDialog();

            return frm.ListofData;

        }




        //public static ISingleResult<ClsFares> SP_CalculateFares(int? vehicleTypeId, int? companyId, string milesStr, DateTime? pickupTime)
        //{

        //    if (AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool())
        //    {
        //        return new TaxiDataContext().stp_CalculatePeakOffPeakFares(vehicleTypeId, companyId, milesStr, pickupTime);
        //    }
        //    else
        //    {


        //        return new TaxiDataContext().stp_CalculateFares(vehicleTypeId, companyId, milesStr);
        //    }
        //}


        public static bool SP_SendMessage(int? senderId, int? receiverId, string senderName, string receiverName, string messageBody
                 , DateTime? createdDate, string sendFrom, string receiverMobileNo, string receiverPDAMobileNo, bool sendAlsoPDA)
        {


            bool rtn = true;

            using (Taxi_Model.TaxiDataContext db = new TaxiDataContext())
            {
                if (receiverId != null && receiverId != 0 && sendAlsoPDA == true)
                {
                    db.stp_SendMessage(senderId, receiverId, senderName, receiverName, messageBody, sendFrom);
                }

                if (sendAlsoPDA == false)
                {


                    if (string.IsNullOrEmpty(receiverMobileNo))
                        return true;



                    if (Debugger.IsAttached == false)
                    {


                        int idx = -1;
                        if (receiverMobileNo.StartsWith("044") == true)
                        {
                            idx = receiverMobileNo.IndexOf("044");
                            receiverMobileNo = receiverMobileNo.Substring(idx + 3);
                            receiverMobileNo = receiverMobileNo.Insert(0, "+44");
                        }

                        if (receiverMobileNo.StartsWith("07"))
                        {
                            receiverMobileNo = receiverMobileNo.Substring(1);
                        }

                        if (receiverMobileNo.StartsWith("044") == false || receiverMobileNo.StartsWith("+44") == false)
                            receiverMobileNo = receiverMobileNo.Insert(0, "+44");
                    }


                    EuroSMS objSMS = new EuroSMS();

                    objSMS.ToNumber = receiverMobileNo;
                    objSMS.Message = messageBody;
                    objSMS.Send(ref messageBody);
                }
                else
                {

                    //if (messageBody.Contains("\r\n"))
                    //{
                    //    messageBody = messageBody.Replace("\r\n", " ").Trim();
                    //}

                    if (messageBody.Contains("&"))
                    {
                        messageBody = messageBody.Replace("&", "And");
                    }

                    if (messageBody.Contains(">"))
                        messageBody = messageBody.Replace(">", " ");


                    if (messageBody.Contains("="))
                        messageBody = messageBody.Replace("=", " ");


                    if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                    {

                        // For TCP Connection
                        if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                        {
                            rtn = General.SendMessageToPDA("request pda=" + receiverId + "=" + 0 + "="
                                            + "Message>>" + messageBody + ">>" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + "=4").Result.ToBool();
                        }

                    }
                    else
                    {
                        // For TCP Connection
                        if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                        {
                            rtn = General.SendMessageToPDA("request pda=" + receiverId + "=" + 0 + "="
                                           + "Message>>" + messageBody + ">>" + String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + "=4").Result.ToBool();
                        }


                    }




                }


            }

            return rtn;
        }




        public static void SP_SaveBid(long jobId, int? driverId, decimal bidRate, int? bidStatusId)
        {

            using (Taxi_Model.TaxiDataContext db = new TaxiDataContext())
            {
                db.stp_UpdateBid(jobId, driverId, bidRate, bidStatusId);


            }
        }





        public static string RemoveUK(ref string address)
        {


            if (address.ToUpper().EndsWith(", UK"))
            {
                address = address.Remove(address.ToUpper().LastIndexOf(", UK"));
            }

            return address;
        }




        public static void ShowGoogleMap_RouteDirections(WebBrowser map, string PickupPoint, string[] ViaLocs, string DestinationPoint)
        {

            if (map == null)
            {
                map = new WebBrowser();

            }

            PickupPoint = GetPostCodeMatch(PickupPoint);
            DestinationPoint = GetPostCodeMatch(DestinationPoint);


            bool IsPickupAlpha = PickupPoint.IsAlpha();
            bool IsDestAlpha = DestinationPoint.IsAlpha();

            if (IsPickupAlpha || IsDestAlpha)
            {
                return;

            }

            PickupPoint = PickupPoint.Replace(" ", "+") + "+UK";
            DestinationPoint = DestinationPoint.Replace(" ", "+") + "+UK";
            //  PickupPoint += "+to:";
            string via = string.Empty;
            if (ViaLocs.Count() > 0)
            {
                ViaLocs = ViaLocs.Select(c => GetPostCodeMatch(c)).Where(c => c.Length > 0).ToArray<string>();
                if (ViaLocs.Count() == 0)
                {
                    //   ENUtils.ShowMessage("Map not found");
                    return;

                }
                via = string.Join("|", ViaLocs.Select(c => GetPostCodeMatch(c) + "+UK").ToArray<string>());
            }

            if (PickupPoint == string.Empty || DestinationPoint == string.Empty)
            {

                return;
            }




            PickupPoint = PickupPoint.Replace("to:", "|").Trim();

            string src = string.Empty;

            if (via.Length > 0)
            {

                src = "<html><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"></head><body<iframe" +
                                     " width=\"960\"" +
                                     " height=\"710\"" +
                                     " \frameborder=\"0\" style=\"border:0;margin-left:-10;margin-top:-10;margin-right:-10\"" +
                                     " src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyAFkZHqTas4EKYEEsk8J3aQh0zQJ-tsWmY&origin=" +
                                        PickupPoint + "&waypoints=" + via + "&destination=" + DestinationPoint + "&avoid=tolls|highways" + "\">" +
                                   "</iframe></body></html>";
            }
            else
            {
                src = "<html><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"></head><body><iframe" +
                                   " width=\"960\"" +
                                   " height=\"710\"" +
                                   " \frameborder=\"0\" style=\"border:0;margin-left:-10;margin-top:-10;margin-right:-10\"" +
                                   " src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyAFkZHqTas4EKYEEsk8J3aQh0zQJ-tsWmY&origin=" +
                                      PickupPoint + "&destination=" + DestinationPoint + "&avoid=tolls|highways" + "\">" +
                                 "</iframe></body></html>";


            }

            map.DocumentText = src;



        }







        public static void LoadZoneList()
        {

            if (AppVars.objPolicyConfiguration.EnablePOI.ToBool())
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                  AppVars.zonesList=  db.stp_GetLocalizationDetails().Where(c => c.DetailId != null).Select(c => c.PostCode).ToArray<string>();

                }

            }
            else
            {

                AppVars.zonesList = General.GetQueryable<Gen_Zone_AssociatedPostCode>(c => c.PostCode != null && c.PostCode != "").OrderBy(c => c.Gen_Zone.OrderNo)
                                     .OrderBy(c => c.OrderNo).Select(a => a.PostCode).ToArray<string>();

            }

        }






        public static string GetPostCode(string value)
        {

            if (value.ToStr().Contains(","))
            {
                value = value.Replace(",", "").Trim();
            }

            if (value.ToStr().Contains(" "))
            {
                value = value.Replace(" ", " ").Trim();
            }

            string postCode = "";

            //   string ukAddress = @"[[:alnum:]][a-zA-Z0-9_\.\#\' \-]{2,60}$";
            string ukAddress = @"^(GIR 0AA)|((([A-PR-UWYZ][0-9][0-9]?)|(([A-PR-UWYZ][A-HK-Y][0-9][0-9]?)|(([A-PR-UWYZ][0-9][A-HJKSTUW])|([A-PR-UWYZ][A-HK-Y][0-9][ABEHMNPRVWXY])))) [0-9][A-BD-HJLNP-UW-Z]{2})$";
            // string ukAddress = @"^(GIR 0AA|[A-PR-UWYZ]([0-9]{1,2}| ([A-HK-Y][0-9]|[A-HK-Y][0-9]([0-9]| [ABEHMNPRV-Y]))|[0-9][A-HJKPS-UW]) [0-9][ABD-HJLNP-UW-Z]{2})$";


            Regex reg = new Regex(ukAddress);
            Match em = reg.Match(value);

            if (em != null)
                postCode = em.Value;

            if (em.Value == "")
            {
                ukAddress = @"[A-Z]{1,2}[0-9R][0-9A-Z]?";
                reg = new Regex(ukAddress);
                em = reg.Match(value);

                postCode = em.Value;

            }

            return postCode;

        }

        public static string GetPostCodeMatch(string value)
        {



            string postCode = "";

            General.RemoveUK(ref value);

            if (value.ToStr().Contains(","))
            {
                value = value.Replace(",", "").Trim();
            }

            if (value.ToStr().Contains(" "))
            {
                value = value.Replace(" ", " ").Trim();
            }

           


            string ukAddress = @"^(GIR 0AA)|((([A-PR-UWYZ][0-9][0-9]?)|(([A-PR-UWYZ][A-HK-Y][0-9][0-9]?)|(([A-PR-UWYZ][0-9][A-HJKSTUW])|([A-PR-UWYZ][A-HK-Y][0-9][ABEHMNPRVWXY])))) [0-9][A-BD-HJLNP-UW-Z]{2})$";


            Regex reg = new Regex(ukAddress);
            Match em = reg.Match(value);

            if (em != null)
                postCode = em.Value;

            if (em.Value == "")
            {
                ukAddress = @"[A-Z]{1,2}[0-9R][0-9A-Z]?";
                reg = new Regex(ukAddress);
                MatchCollection mat = reg.Matches(value);


                foreach (Match item in mat)
                {
                    if (item.Value.ToStr().IsAlpha() == false)
                        postCode += item.Value.ToStr() + " ";

                }

            }



            return postCode.Trim();

        }




        public static string GetPostCodeMatchOpt(string value)
        {



            string postCode = "";

            General.RemoveUK(ref value);

            if (value.ToStr().Contains(","))
            {
                value = value.Replace(",", "").Trim();
            }

            if (value.ToStr().Contains(" "))
            {
                value = value.Replace(" ", " ").Trim();
            }



            string ukAddress = @"^([A-PR-UWYZ](([0-9](([0-9]|[A-HJKSTUW])?)?)|([A-HK-Y][0-9]([0-9]|[ABEHMNPRVWXY])?)) ?[0-9][ABD-HJLNP-UW-Z]{2})$";

         //   string ukAddress = @"^(GIR 0AA)|((([A-PR-UWYZ][0-9][0-9]?)|(([A-PR-UWYZ][A-HK-Y][0-9][0-9]?)|(([A-PR-UWYZ][0-9][A-HJKSTUW])|([A-PR-UWYZ][A-HK-Y][0-9][ABEHMNPRVWXY]))))([ ]?)[0-9][A-BD-HJLNP-UW-Z]{2})$";


            Regex reg = new Regex(ukAddress);
            Match em = reg.Match(value);

            if (em != null)
                postCode = em.Value;

            if (em.Value == "")
            {

                string halfPostcode = string.Empty;

                ukAddress = @"[A-Z]{1,2}[0-9R][0-9A-Z]?";
                reg = new Regex(ukAddress);
                MatchCollection mat = reg.Matches(value);

                foreach (Match item in mat)
                {
                    if (item.Value.ToStr().IsAlpha() == false)
                        halfPostcode += item.Value.ToStr();

                }

                if (value.WordCount() == 1)
                {
                    //if(value.EndsWith(" "))
                    //{
                    //    postCode = halfPostcode + " ";

                    //}
                    //else
                         postCode = halfPostcode;

                }
                else if (halfPostcode.Length > 0 && value.WordCount() == 2)
                {
                    if ( value.Trim().Length <= 8 && value.Trim().Contains(" ") 
                        &&  value.Trim().Split(new char[] { ' ' })[1].IsAlpha()==false)
                    {

                        if (value.StartsWith(halfPostcode))
                            postCode = value.Trim();
                        else if (value.EndsWith(halfPostcode))
                            postCode = halfPostcode;
                    }
                    else if(halfPostcode.IsAlpha()==false)
                        postCode = halfPostcode;

                }

            }



            return postCode.Trim();

        }



        public static string GetFullPostCodeMatch(string value)
        {
            string postCode = "";


            General.RemoveUK(ref value);


            string ukAddress = @"([A-Z][A-Z][0-9] [0-9]?)|([A-Z][A-Z][0-9][0-9] [0-9]?)|([A-Z][0-9][0-9] [0-9]?)|([A-Z][0-9] [0-9]?)|([A-Z][A-Z][0-9][A-Z] [0-9]?)|([A-Z][0-9][A-Z] [0-9]?)";





            Regex reg = new Regex(ukAddress);
            Match em = reg.Match(value);

            if (em != null)
                postCode = em.Value;

            if (em.Value == "")
            {

                reg = new Regex(ukAddress);
                MatchCollection mat = reg.Matches(value);


                foreach (Match item in mat)
                {
                    if (item.Value.ToStr().IsAlpha() == false)
                        postCode += item.Value.ToStr() + " ";

                }

                // postCode = em.Value;

            }



            return postCode.Trim();

        }


        public static List<T> GetGeneralList<T>(Expression<Func<T, bool>> condition) where T : class
        {
            if (condition == null)
            {
                return new BLInfo<T, Taxi_Model.TaxiDataContext>().GetAll<T>().ToList();
            }
            else
            {

                return new BLInfo<T, Taxi_Model.TaxiDataContext>()
                         .GetAll<T>(condition).ToList();
            }
        }


        public static IQueryable<T> GetQueryable<T>(Expression<Func<T, bool>> condition) where T : class
        {
            if(condition==null)
                return new BLInfo<T, Taxi_Model.TaxiDataContext>().GetAll<T>();

            else
            return new BLInfo<T, Taxi_Model.TaxiDataContext>()
                     .GetAll<T>(condition);

        }

        public static void ShowBookingForm(bool showOnDialog)
        {

            if (AppVars.objPolicyConfiguration.BookingFormType.ToInt() == 2)
            {
                frmBooking2 frm = new frmBooking2();
                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm.MaximizeBox = false;

                if (showOnDialog)
                {
                    frm.ShowDialog();
                }
                else
                    frm.Show();


            }
            else
            {

                frmBooking frm = new frmBooking();


                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;

                if (showOnDialog)
                {
                    frm.ShowDialog();
                }
                else
                    frm.Show();
            }
            //   frm.AllowShowFocusCues = false;

        }



        public static void FillDataset()
        {
            try
            {

                if (Program.dtCombos == null)
                {

                    Program.dtCombos = new DataSet();
                    using (System.Data.SqlClient.SqlConnection sqlconn = new System.Data.SqlClient.SqlConnection(Cryptography.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToStr(), "softeuroconnskey", true)))
                    {

                        sqlconn.Open();

                        using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                        {

                            cmd.Connection = sqlconn;

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "stp_fillbookingcombos";

                            using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            {
                                // Fill the DataSet using default values for DataTable names, etc
                                da.Fill(Program.dtCombos);
                            }

                        }

                        Program.dtCombos.WriteXml(Application.StartupPath + "\\Booking\\booking.xml", XmlWriteMode.WriteSchema);
                    }
                }
            }
            catch
            {


            }
        }

        public static void ShowBookingForm(int id, bool showOnDialog, string name, string phone, int? bookingTypeId)
        {

            try
            {
                if (File.Exists(Application.StartupPath + "\\Booking\\TreasureBooking.exe"))
                {

                    if (Program.dtCombos == null)
                    {
                        General.FillDataset();
                    }


                    var s = Newtonsoft.Json.JsonConvert.SerializeObject(Program.dtCombos, Newtonsoft.Json.Formatting.Indented);

                    s = s.Replace(" ", "").Trim();

                    s = s.Replace(Environment.NewLine, "").Trim();
                    s = s.Replace("\"", "*");


                    ClsDataTransfer polObj = new ClsDataTransfer();

                    foreach (System.Reflection.PropertyInfo item in AppVars.objPolicyConfiguration.GetType().GetProperties())
                    {
                        try
                        {

                            if (polObj.GetType().GetProperty(item.Name) != null)
                                polObj.GetType().GetProperty(item.Name).SetValue(polObj, item.GetValue(AppVars.objPolicyConfiguration, null), null);
                        }
                        catch
                        {


                        }
                    }


                    polObj.CallerId_PhoneNo = phone;
                    polObj.CallerId_CustomerName = name;
                    polObj.CanTransferJob = AppVars.CanTransferJob;

                    polObj.CallerId_SubCompanyId = AppVars.objSubCompany.Id;

                    polObj.DataString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr().Replace(" ", "**");


                    string pol = Newtonsoft.Json.JsonConvert.SerializeObject(polObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "~").Replace(Environment.NewLine, "").Replace("\"", "*");


                    Process pp = new Process();
                    pp.StartInfo.FileName = Application.StartupPath + "\\Booking\\TreasureBooking.exe";
                    pp.StartInfo.Arguments = s + " " + pol + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.LoginObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.keyLocations, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.zonesList, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*");

                    if (id > 0)
                    {
                        pp.StartInfo.Arguments += " " + "frmBooking" + " " + id;
                    }
                    pp.Start();
                    Thread.Sleep(300);
                }
                else
                {

                    if (AppVars.objPolicyConfiguration.BookingFormType.ToInt() == 2)
                    {
                        frmBooking2 frm = new frmBooking2(name, phone, null, false);
                        frm.PickBookingTypeId = bookingTypeId;
                        if (id != 0)
                        {
                            frm.OnDisplayRecord(id);
                        }
                        frm.ControlBox = true;
                        frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                        frm.MaximizeBox = false;

                        if (showOnDialog)
                        {
                            frm.ShowDialog();
                        }
                        else
                            frm.Show();


                    }
                    else
                    {

                        frmBooking frm = new frmBooking(name, phone, null, false);
                        frm.PickBookingTypeId = bookingTypeId;
                        if (id != 0)
                        {
                            frm.OnDisplayRecord(id);
                        }
                        frm.ControlBox = true;
                        frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                        frm.MaximizeBox = false;

                        if (showOnDialog)
                        {
                            frm.ShowDialog();
                        }
                        else
                            frm.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }


        }


        public static void ShowBookingForm(int id, bool showOnDialog, string name, string phone, string doorNo, string address, int? bookingTypeId)
        {
            if (AppVars.objPolicyConfiguration.BookingFormType.ToInt() == 2)
            {
                frmBooking2 frm = new frmBooking2(name, phone, doorNo, address, null, false);
                frm.PickBookingTypeId = bookingTypeId;

                if (id != 0)
                {
                    frm.OnDisplayRecord(id);
                }
                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;

                if (showOnDialog)
                {
                    frm.ShowDialog();
                }
                else
                    frm.Show();


            }
            else
            {


                frmBooking frm = new frmBooking(name, phone, doorNo, address, null, false);
                frm.PickBookingTypeId = bookingTypeId;

                if (id != 0)
                {
                    frm.OnDisplayRecord(id);
                }
                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;

                if (showOnDialog)
                {
                    frm.ShowDialog();
                }
                else
                    frm.Show();

            }

            //    frm.AllowShowFocusCues = false;

        }


        public static void ShowBookingForm(int id, bool showOnDialog, string name, string phone, string mobileNo, string doorNo, string address, string email)
        {

            if (AppVars.objPolicyConfiguration.BookingFormType.ToInt() == 2)
            {
                frmBooking2 frm = new frmBooking2(name, phone, mobileNo, doorNo, address, email, null, false);

                if (id != 0)
                {
                    frm.OnDisplayRecord(id);
                }
                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;

                if (showOnDialog)
                {
                    frm.ShowDialog();
                }
                else
                    frm.Show();


            }
            else
            {

                frmBooking frm = new frmBooking(name, phone, mobileNo, doorNo, address, email, null, false);

                if (id != 0)
                {
                    frm.OnDisplayRecord(id);
                }
                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;

                if (showOnDialog)
                {
                    frm.ShowDialog();
                }
                else
                    frm.Show();
            }
            //     frm.AllowShowFocusCues = false;

        }

        public static bool CheckCompanyInformation()
        {
            bool exist = true;
            if (AppVars.objSubCompany.TelephoneNo.ToStr().Trim() == string.Empty)
            {
                exist = false;

                ENUtils.ShowMessage("InComplete Company Information.." + Environment.NewLine +
                                    "Please Enter Company Information");


                frmSysPolicy frm = new frmSysPolicy(1);
                frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm.ControlBox = true;
                frm.MaximizeBox = false;
                frm.Size = new Size(750, 600);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }

            return exist;

        }

        public static void ShowBookingForm(int id, bool showOnDialog, string name, string phone, int? fromLocTypeId, int? toLocTypeId,
                                           int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse, string doorNo)
        {

            if (AppVars.objPolicyConfiguration.BookingFormType.ToInt() == 2)
            {
                frmBooking2 frm = new frmBooking2(name, phone, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse, doorNo);
                frm.OnDisplayRecord(id);

                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;

                if (showOnDialog)
                {
                    frm.ShowDialog();
                }
                else
                    frm.Show();


            }
            else
            {

                frmBooking frm = new frmBooking(name, phone, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse, doorNo);
                frm.OnDisplayRecord(id);

                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;

                if (showOnDialog)
                {
                    frm.ShowDialog();
                }
                else
                    frm.Show();
            }

            //   frm.AllowShowFocusCues = false;

        }



        public static void ShowBookingForm(int id, bool showOnDialog, string name, string phone, int? fromLocTypeId, int? toLocTypeId,
                                           int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse, string fromdoorNo, string toDoorNo, int? bookingTypeId, string email)
        {

            if (AppVars.objPolicyConfiguration.BookingFormType.ToInt() == 2)
            {
                frmBooking2 frm = new frmBooking2(name, phone, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse, fromdoorNo, toDoorNo, email, null, false);
                frm.PickBookingTypeId = bookingTypeId;

                if (id != 0)
                {
                    // Booking objB=  General.GetObject<Booking>(c => c.Id == id);
                    frm.OnDisplayRecord(id);
                }
                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;

                if (showOnDialog)
                {
                    frm.ShowDialog();
                }
                else
                    frm.Show();


            }
            else
            {

                frmBooking frm = new frmBooking(name, phone, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse, fromdoorNo, toDoorNo, email, null, false);
                frm.PickBookingTypeId = bookingTypeId;

                if (id != 0)
                {
                    // Booking objB=  General.GetObject<Booking>(c => c.Id == id);
                    frm.OnDisplayRecord(id);
                }
                frm.ControlBox = true;
                frm.FormBorderStyle = FormBorderStyle.Fixed3D;
                frm.MaximizeBox = false;

                if (showOnDialog)
                {
                    frm.ShowDialog();
                }
                else
                    frm.Show();
            }

            //     frm.AllowShowFocusCues = false;

        }






        public static T GetObject<T>(Expression<Func<T, bool>> condition) where T : class
        {
           
                return new TaxiDataContext().GetTable<T>().FirstOrDefault(condition);
     

            //return new BLInfo<T, Taxi_Model.TaxiDataContext>()
            //         .Get<T>(condition);

        }



        public static void RefreshList<T>(string formName) where T : SetupBase
        {

            DockWindow dock = UI.MainMenuForm.MainMenuFrm.GetDockByName(formName);
            if (dock != null)
            {
                dock.Select();

                if (dock.ActiveControl != null)
                {
                    SetupBase frm = (T)dock.ActiveControl;
                    frm.PopulateData();
                }
            }

        }


        public static void RefreshListWithoutSelected<T>(string formName) where T : SetupBase
        {

            DockWindow dock = UI.MainMenuForm.MainMenuFrm.GetDockByName(formName);
            if (dock != null)
            {


                if (dock.Controls.Count == 1 && dock.Controls[0] != null)
                {
                    SetupBase frm = (T)dock.Controls[0];
                    frm.PopulateData();
                }
            }

        }

        public static void RefreshListWithoutSelectedOnRefreshData<T>(string formName) where T : SetupBase
        {

            DockWindow dock = UI.MainMenuForm.MainMenuFrm.GetDockByName(formName);
            if (dock != null)
            {


                if (dock.Controls.Count == 1 && dock.Controls[0] != null)
                {
                    SetupBase frm = (T)dock.Controls[0];
                    frm.RefreshData();
                }
            }

        }





        public static SetupBase GetForm<T>(string formName) where T : SetupBase
        {
            SetupBase frm = null;
            DockWindow dock = UI.MainMenuForm.MainMenuFrm.GetDockByName(formName);
            if (dock != null)
            {
                dock.Select();

                if (dock.ActiveControl != null)
                {
                    frm = (T)dock.ActiveControl;
                    //frm.PopulateData();
                }
            }

            return frm;
        }


        public static List<object[]> ShowFormMultiLister(IList list, string pkColumn)
        {

            Taxi_AppMain.frmLister frm = new Taxi_AppMain.frmLister(list, pkColumn, true);

            frm.ShowDialog();


            return frm.ListofData;

        }




        public static List<object[]> ShowMultiLister(IList list, string pkColumn)
        {

            UI.frmLister frm = new UI.frmLister(list, pkColumn, true);

            frm.ShowDialog();


            return frm.ListofData;

        }


        public static object[] ShowLister(IList list, string pkColumn)
        {

            UI.frmLister frm = new UI.frmLister(list, pkColumn, false);

            frm.ShowDialog();


            return frm.RowData;

        }


        public static object[] ShowFormLister(IList list, string pkColumn)
        {

            frmLister frm = new frmLister(list, pkColumn, false);

            frm.ShowDialog();


            return frm.RowData;

        }



        public static object[] ShowFormLister(IList list, string pkColumn, bool IsAutoSizeRows)
        {

            frmLister frm = new frmLister(list, pkColumn, false);
            frm.IsAutoSizeRows = true;
            frm.ShowDialog();


            return frm.RowData;

        }




        public static object[] ShowLister(IList list, string pkColumn, string[] hiddenColumns)
        {

            UI.frmLister frm = new UI.frmLister(list, pkColumn, false, hiddenColumns);

            frm.ShowDialog();


            return frm.RowData;

        }


        public static object[] ShowLister(IList list, string pkColumn, string[] hiddenColumns, string[] bestfitCols)
        {

            UI.frmLister frm = new UI.frmLister(list, pkColumn, false, hiddenColumns, bestfitCols);

            frm.ShowDialog();


            return frm.RowData;

        }



        public static string GetSharedNetworkPath()
        {
            string path = @"";
            Gen_SysPolicy_Configuration obj = GetQueryable<Gen_SysPolicy_Configuration>(null).FirstOrDefault();

            if (obj != null && !string.IsNullOrEmpty(obj.SharedNetworkPath))
                path += obj.SharedNetworkPath;

            return path;


        }




        public static string GetDocumentsFolderPath(int id)
        {
            string fullPath = GetSharedNetworkPath() + "\\TAXI\\Driver" + id.ToStr();

            return fullPath;


        }

    


        public static decimal CalculateDistanceFromBaseFull(string destination)
        {
            decimal miles = 0.00m;

            try
            {
                string origin = General.GetPostCodeMatch(AppVars.objPolicyConfiguration.BaseAddress.ToStr().ToUpper().Trim());


                if (origin.Contains("&"))
                    origin = origin.Replace("&", "AND").Trim();


                if (destination.Contains("&"))
                    destination = destination.Replace("&", "AND").Trim();

                destination = General.GetPostCodeMatch(destination.ToStr().ToUpper().Trim());

                if (origin == string.Empty || destination == string.Empty || origin.ContainsNoSpaces() || destination.ContainsNoSpaces())
                    return miles;


                if ((LastCalcDestination.Length > 0 && LastCalcDestination.Length > 0
                      && origin == LastCalcOrigin && destination == LastCalcDestination) && LastCalcMileage > 0)
                {


                    miles = LastCalcMileage;

                    return miles;

                }


                Gen_Coordinate objC = General.GetObject<Gen_Coordinate>(c => c.PostCode == destination);

                if (objC != null && objC.DistanceMiles != null)
                    miles = objC.DistanceMiles.ToDecimal();
                else
                {

                   
                    bool exist = false;

                    // offlinedistance
                    if (AppVars.objPolicyConfiguration.EnableOfflineDistance.ToBool() && exist == false)
                    {
                        string time = string.Empty;
                        miles = AppVars.frmMDI.GetDistanceAndTime(origin, destination, ref time);
                        exist = true;
                        LastCalcOrigin = origin;
                        LastCalcDestination = destination;
                        LastCalcMileage = miles;
                        LastCalEstTime = time;

                    }

                    //if (exist == false)
                    //{
                    //    try
                    //    {


                    //        if (objTaxiService != null)
                    //        {
                    //            string estimatedTime = string.Empty;
                    //            miles = objTaxiService.GetDistanceAndTime(origin, destination, ref estimatedTime);

                    //            if (miles > 0)
                    //            {
                    //                exist = true;


                    //                LastCalcOrigin = origin;
                    //                LastCalcDestination = destination;
                    //                LastCalcMileage = miles;
                    //                LastCalEstTime = estimatedTime;

                    //            }
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        if (ex.Message.StartsWith("Could not connect"))
                    //        {

                    //            StartServiceWCF();

                    //        }

                    //    }
                    //}


                    if (exist == false)
                    {

                        if (string.IsNullOrEmpty(directionKey))
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {

                                directionKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='google'").FirstOrDefault().ToStr().Trim();


                                if (directionKey.Length == 0)
                                    directionKey = " ";
                                else
                                    directionKey = "&key=" + directionKey;
                            }

                        }

                        string applyShortesDistance = AppVars.objPolicyConfiguration.PreferredShortestDistance.ToBool() ? "&avoid=highways" : "";
                      //  string url2 = "https://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + ", UK&destination=" + destination + ", UK" + applyShortesDistance + "&sensor=false&key=AIzaSyB5bNY_Yn3lZNy0FB7WxgAz7dgbE29jr50";

                        string url2 = string.Empty;
                        //string url2 = "https://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + ", UK&destination=" + destination + ", UK" + applyShortesDistance + "&sensor=false&key=AIzaSyB5bNY_Yn3lZNy0FB7WxgAz7dgbE29jr50";

                        //if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr().Trim() == "AnzHawaiifive3Ztzx")
                        //{
                        //    url2 = "https://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + ", UK&destination=" + destination + ", UK" + applyShortesDistance + "&sensor=false&key=AIzaSyB5bNY_Yn3lZNy0FB7WxgAz7dgbE29jr50";
                        //}
                        //else
                        //{
                            url2 = "https://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + ", UK&destination=" + destination + ", UK" + applyShortesDistance + directionKey + "&sensor=false";

                      //  }


                        using (XmlTextReader reader = new XmlTextReader(url2))
                        {
                            reader.WhitespaceHandling = WhitespaceHandling.Significant;
                            using (System.Data.DataSet ds = new System.Data.DataSet())
                            {
                                ds.ReadXml(reader);
                                DataTable dt = ds.Tables["distance"];
                                if (dt != null)
                                {

                                    decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
                                    decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

                                    decimal milKM = 0.621m;
                                    decimal milMeter = 0.00062137119m;

                                    miles = (milKM * distanceKm) + (milMeter * distanceMeter);

                                    dt.Dispose();
                                    dt = null;
                                    exist = true;
                                }
                            }


                            reader.Close();
                        }

                        // string url2 = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&mode=driving&units=imperial";


                        //using (XmlTextReader reader = new XmlTextReader(url2))
                        //{
                        //    reader.WhitespaceHandling = WhitespaceHandling.Significant;

                        //    using (System.Data.DataSet ds = new System.Data.DataSet())
                        //    {
                        //        ds.ReadXml(reader);
                        //        DataTable dt = ds.Tables["distance"];
                        //        if (dt != null)
                        //        {

                        //            miles = dt.Rows[0][1].ToStr().Replace("mi", "").ToStr().Trim().ToDecimal();
                        //            dt.Dispose();
                        //            dt = null;
                        //            exist = true;


                        //        }
                        //    }

                        //    reader.Close();
                        //}


                        if (exist && miles > 0 && miles <= AppVars.objPolicyConfiguration.AutoDespatchElapsedTime.ToDecimal() + 1)
                        {
                            try
                            {
                                using (TaxiDataContext db = new TaxiDataContext())
                                {
                                    var objgen = db.Gen_Coordinates.FirstOrDefault(c => c.PostCode == destination);

                                    if (objgen != null)
                                    {
                                        objgen.DistanceMiles = miles;
                                        db.SubmitChanges();



                                    }
                                }
                            }
                            catch
                            {


                            }


                        }


                    }


                    if (exist == false)
                    {
                        Gen_Coordinate objBaseCoordinate = General.GetObject<Gen_Coordinate>(c => c.PostCode == origin);


                        double? originLat = objBaseCoordinate.Latitude;
                        double? originLng = objBaseCoordinate.Longitude;

                        if (string.IsNullOrEmpty(BingKey))
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {

                                BingKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='bing'").FirstOrDefault().ToStr().Trim();


                                if (BingKey.Length == 0)
                                    BingKey = " ";
                            }
                        }

                        if (BingKey.Trim().Length > 0)
                        {


                            using (XmlTextReader reader2 = new XmlTextReader("http://dev.virtualearth.net/REST/V1/Routes/Driving?o=xml&wp.0=" + originLat + "," + originLng + "&wp.1=" + destination + ", UK" + "&DistanceUnit=Mile&key=" + BingKey))
                            {
                                reader2.WhitespaceHandling = WhitespaceHandling.Significant;


                                using (System.Data.DataSet ds = new System.Data.DataSet())
                                {




                                    ds.ReadXml(reader2);
                                    DataTable dt = ds.Tables["RouteLeg"];


                                    if (dt != null)
                                    {
                                        DataRow dRow = dt.Rows.OfType<DataRow>().FirstOrDefault();

                                        if (dRow != null)
                                        {
                                            miles = dRow["TravelDistance"].ToDecimal();

                                            LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                            LastCalcDestination = destination.Replace(", UK", "").Trim();
                                            LastCalcMileage = miles;
                                            LastCalEstTime = string.Empty;
                                        }

                                        dt.Dispose();
                                        dt = null;
                                    }


                                }


                                reader2.Close();
                            }
                        }




                        //using (XmlTextReader reader = new XmlTextReader("http://dev.virtualearth.net/REST/V1/Routes/Driving?o=xml&wp.0=" + originLat + "," + originLng + "&wp.1=" + destination + ", UK" + "&DistanceUnit=Mile&key=At9uWeg3Sk_C611VLD0cc6i9oYu7IioNxvjNUN6-blcjKIX_L2n5G2ObOgEVlNZ_"))
                        //{
                        //    reader.WhitespaceHandling = WhitespaceHandling.Significant;
                        //    using (System.Data.DataSet ds = new System.Data.DataSet())
                        //    {




                        //        ds.ReadXml(reader);

                        //        DataTable dt = ds.Tables["RouteLeg"];

                        //        if (dt != null)
                        //        {
                        //            DataRow dRow = dt.Rows.OfType<DataRow>().FirstOrDefault();

                        //            if (dRow != null)
                        //            {

                        //                miles = dRow["TravelDistance"].ToDecimal();


                        //                //if (!string.IsNullOrEmpty(time))
                        //                //{
                        //                //    time = (time.ToLong() / 60).ToStr() + " mins";

                        //                //    time += " , " + Math.Round(Convert.ToDouble(dRow["TravelDistance"].ToStr()), 2) + " mi";
                        //                //}
                        //                exist = true;
                        //            }
                        //        }


                        //    }

                        //    reader.Close();
                        //}
                    }


                    if (exist == false)
                    {
                        var objCoord = General.GetObject<Booking>(c => c.ExtraMile != null && c.ExtraMile > 0 && c.FromAddress != null && c.FromAddress.Contains(destination));

                        if (objCoord != null)
                        {

                            miles = objCoord.ExtraMile.ToDecimal();

                        }



                    }
                }




            }
            catch
            {



            }

            return miles;
        }


        public static decimal CalculateDistanceVia(string origin, string destination)
        {


            if (origin.ToStr().Trim().Length == 0 || destination.ToStr().Trim().Length == 0)
                return 0.00m;

            decimal miles = 0.00m;


            if (origin.Contains("&"))
                origin = origin.Replace("&", "AND").Trim();


            if (destination.Contains("&"))
                destination = destination.Replace("&", "AND").Trim();



            if ((LastCalcDestination.Length > 0 && LastCalcDestination.Length > 0
              && origin == LastCalcOrigin && destination == LastCalcDestination) && LastCalcMileage > 0)
            {


                miles = LastCalcMileage;

                return miles;

            }


            try
            {

                bool exist = false;





                // offlinedistance
                if (AppVars.objPolicyConfiguration.EnableOfflineDistance.ToBool() && exist == false)
                {
                    string time = string.Empty;
                    miles = AppVars.frmMDI.GetDistanceAndTime(origin, destination, ref time);
                    exist = true;
                    LastCalcOrigin = origin;
                    LastCalcDestination = destination;
                    LastCalcMileage = miles;
                    LastCalEstTime = time;

                }

                if (exist == false)
                {
                    if (origin.Contains(".") && origin.Contains(",") && origin.Split(new char[] { ',' }).Count() == 2)
                    {

                    }
                 
                    else
                    {
                        origin += ", UK";
                    }


                    if (destination.Contains(".") && destination.Contains(",") && destination.Split(new char[] { ',' }).Count() == 2)
                    {


                    }
                  
                    else
                    {
                        destination += ", UK";
                    }

                  //  origin += ", UK";
                   // destination += ", UK";

                    if (string.IsNullOrEmpty(directionKey))
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {

                            directionKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='google'").FirstOrDefault().ToStr().Trim();


                            if (directionKey.Length == 0)
                                directionKey = " ";
                            else
                                directionKey = "&key=" + directionKey;
                        }
                    }

                    if (AppVars.objPolicyConfiguration.PreferredShortestDistance.ToBool())
                    {
                     


                        string URL = "";
                        //if (via == null || via.Length == 0)
                        //{


                        URL = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&units=imperial" + directionKey + "&sensor=false";
                        URL = string.Format(URL, origin, destination);



                        try
                        {
                            WebRequest request = HttpWebRequest.Create(URL);

                            request.Headers.Add("Authorization", "");
                            System.Net.WebRequest.DefaultWebProxy = null;
                            request.Proxy = System.Net.WebRequest.DefaultWebProxy;



                            WebResponse response = request.GetResponse();
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer parser = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string responseStringData = reader.ReadToEnd();
                                RootObject responseData = parser.Deserialize<RootObject>(responseStringData);
                                if (responseData != null && responseData.routes != null && responseData.routes.Count > 0)
                                {

                                    var objShortest = responseData.routes.OrderBy(x => x.legs[0].distance.value).FirstOrDefault();

                                    if (objShortest.legs[0].distance.text.ToStr().EndsWith(" mi"))
                                    {
                                        miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.text.Replace(" mi", "").Trim())))), 1);

                                    }
                                    else
                                    {
                                        miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.value)) / 1609.344)), 1);
                                    }

                                    //  miles = objShortest.legs[0].distance.value;
                                    LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                    LastCalcDestination = destination.Replace(", UK", "").Trim();
                                    LastCalcMileage = miles;
                                    LastCalEstTime = string.Empty;


                                }
                            }
                        }
                        catch
                        {



                        }

                        if (miles == 0 && AppVars.objPolicyConfiguration.DeadMileage.ToDecimal() > 0)
                        {

                            try
                            {

                                URL = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&alternatives=true&units=imperial&sensor=false&key=AIzaSyDWEp4huW6xGf9bWOY9QwxcctKY5V2cVFQ";
                                URL = string.Format(URL, origin, destination);


                                WebRequest request = HttpWebRequest.Create(URL);

                                WebResponse response = request.GetResponse();
                                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                                {
                                    System.Web.Script.Serialization.JavaScriptSerializer parser = new System.Web.Script.Serialization.JavaScriptSerializer();
                                    string responseStringData = reader.ReadToEnd();
                                    RootObject responseData = parser.Deserialize<RootObject>(responseStringData);
                                    if (responseData != null && responseData.routes != null && responseData.routes.Count > 0)
                                    {
                                        var objShortest = responseData.routes.OrderBy(x => x.legs[0].distance.value).FirstOrDefault();

                                        miles = Math.Round(Convert.ToDecimal((Convert.ToDouble((objShortest.legs[0].distance.value)) / 1609.344)), 1);

                                        //  miles = objShortest.legs[0].distance.value;
                                        LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                        LastCalcDestination = destination.Replace(", UK", "").Trim();
                                        LastCalcMileage = miles;
                                        LastCalEstTime = string.Empty;

                                        if (miles > 0)
                                        {
                                            File.AppendAllText("directionoverlimit", "DISTANCE WITH NEW KEY : " + DateTime.Now.ToStr() + miles);
                                        }
                                    }
                                }
                            }
                            catch
                            {


                            }
                        }
                    }
                    else
                    {


                        string applyShortesDistance = AppVars.objPolicyConfiguration.PreferredShortestDistance.ToBool() ? "&avoid=highways" : "";
                        string url2 = string.Empty;

                        if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr().Trim() == "AnzHawaiifive3Ztzx")
                        {
                            url2 = "https://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + applyShortesDistance + "&sensor=false&key=AIzaSyB5bNY_Yn3lZNy0FB7WxgAz7dgbE29jr50";
                        }
                        else
                        {
                            url2 = "https://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + applyShortesDistance + directionKey + "&sensor=false";

                        }

                        using (XmlTextReader reader = new XmlTextReader(url2))
                        {
                            reader.WhitespaceHandling = WhitespaceHandling.Significant;


                            using (System.Data.DataSet ds = new System.Data.DataSet())
                            {
                                ds.ReadXml(reader);
                                DataTable dt = ds.Tables["distance"];
                                if (dt != null)
                                {



                                    decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
                                    decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

                                    decimal milKM = 0.621m;
                                    decimal milMeter = 0.00062137119m;

                                    miles = (milKM * distanceKm) + (milMeter * distanceMeter);

                                    dt.Dispose();
                                    dt = null;
                                    exist = true;
                                    LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                    LastCalcDestination = destination.Replace(", UK", "").Trim();
                                    LastCalcMileage = miles;
                                    LastCalEstTime = string.Empty;




                                }
                            }

                            reader.Close();

                        }


                    }


                    if (exist == false)
                    {
                        if (string.IsNullOrEmpty(BingKey))
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {

                                BingKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='bing'").FirstOrDefault().ToStr().Trim();


                                if (BingKey.Length == 0)
                                    BingKey = " ";
                            }
                        }

                        if (BingKey.Trim().Length > 0)
                        {


                            using (XmlTextReader reader2 = new XmlTextReader("http://dev.virtualearth.net/REST/V1/Routes/Driving?o=xml&wp.0=" + origin + "&wp.1=" + destination + "&DistanceUnit=Mile&key=" + BingKey))
                            {
                                reader2.WhitespaceHandling = WhitespaceHandling.Significant;


                                using (System.Data.DataSet ds = new System.Data.DataSet())
                                {




                                    ds.ReadXml(reader2);
                                    DataTable dt = ds.Tables["RouteLeg"];


                                    if (dt != null)
                                    {
                                        DataRow dRow = dt.Rows.OfType<DataRow>().FirstOrDefault();

                                        if (dRow != null)
                                        {
                                            miles = dRow["TravelDistance"].ToDecimal();

                                            LastCalcOrigin = origin.Replace(", UK", "").Trim();
                                            LastCalcDestination = destination.Replace(", UK", "").Trim();
                                            LastCalcMileage = miles;
                                            LastCalEstTime = string.Empty;
                                        }

                                        dt.Dispose();
                                        dt = null;
                                    }


                                }


                                reader2.Close();
                            }
                        }
                    }

                    if (miles > 500)
                    {
                        miles = GetDistance.BetweenTwoPostCodes(origin, destination, "GB", GetDistance.Units.Miles).ToDecimal();
                    }
                }

                // }

            }
            catch
            {



            }

            return miles;
        }
    
     


        public static int? GetZoneId(string address)
        {

            if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() == false)
                return null;

            if (address != "AS DIRECTED" && string.IsNullOrEmpty(General.GetPostCodeMatch(address)))
                return null;

            if (address.Contains(", UK"))
                address = address.Remove(address.LastIndexOf(", UK"));



            int? zoneId = null;

            try
            {
                if (address == "AS DIRECTED")
                {

                    zoneId = General.GetObject<Gen_Zone>(c => c.ZoneName == address).DefaultIfEmpty().Id;

                    if (zoneId == 0)
                        zoneId = null;
                }
                else
                {

                    zoneId = AppVars.listOfAddress.FirstOrDefault(c => c.AddressLine1.Contains(address.ToStr().ToUpper())).DefaultIfEmpty().ZoneId;
                    if (zoneId == null)
                    {


                        string postCode = General.GetPostCode(address);


                        if (address.Contains(","))
                        {

                            string addr = address.Substring(0, address.LastIndexOf(',')).Trim();

                            if (addr.ToStr().Trim() != string.Empty)
                            {

                                zoneId = General.GetObject<Gen_Location>(c => c.PostCode == postCode && c.LocationName == addr).DefaultIfEmpty().ZoneId;
                            }
                        }


                        if (zoneId == null)
                        {

                            Gen_Coordinate objCoord = General.GetObject<Gen_Coordinate>(c => c.PostCode == postCode);


                            if (objCoord != null)
                            {

                                double latitude = 0, longitude = 0;

                                latitude = Convert.ToDouble(objCoord.Latitude);
                                longitude = Convert.ToDouble(objCoord.Longitude);



                                var plot = (from a in General.GetQueryable<Gen_Zone>(c =>(c.ShapeType!=null && c.ShapeType=="circle")
                                  ||  ( c.MinLatitude != null && (latitude >= c.MinLatitude && latitude <= c.MaxLatitude)
                                                                   && (longitude <= c.MaxLongitude && longitude >= c.MinLongitude)))
                                            orderby a.PlotKind

                                            select a.Id).ToArray<int>();


                                if (plot.Count() > 0)
                                {
                                    var list = (from p in plot
                                                join a in General.GetQueryable<Gen_Zone_PolyVertice>(null) on p equals a.ZoneId
                                                select a).ToList();




                                    foreach (int plotId in plot)
                                    {
                                        if (FindPoint(latitude, longitude, list.Where(c => c.ZoneId == plotId).ToList()))
                                        {
                                            zoneId = plotId;
                                            break;

                                        }
                                    }
                                }
                                else
                                {

                                    if (AppVars.objPolicyConfiguration.PriorityPostCodes.ToStr().Length > 0)
                                    {
                                        string[] arr = AppVars.objPolicyConfiguration.PriorityPostCodes.Split(new char[] { ',' });



                                        if (objCoord.PostCode.ToStr().Contains(" ") && arr.Contains(objCoord.PostCode.Split(new char[] { ' ' })[0]))
                                        {


                                            var zone = (from a in General.GetQueryable<Gen_Zone_PolyVertice>(null).AsEnumerable()


                                                        select new
                                                        {

                                                            a.Gen_Zone.Id,
                                                            a.Gen_Zone.ZoneName,
                                                            DistanceMin = new LatLng(Convert.ToDouble(a.Latitude), Convert.ToDouble(a.Longitude)).DistanceMiles(new LatLng(Convert.ToDouble(objCoord.Latitude), Convert.ToDouble(objCoord.Longitude))),


                                                        }).OrderBy(c => c.DistanceMin).FirstOrDefault();



                                            if (zone != null)
                                                zoneId = zone.Id;
                                        }


                                    }


                                }

                            }
                        }
                    }


                }


            }
            catch (Exception ex)
            {


            }

            return zoneId;

        }

        //public static bool FindPoint(double pointLat, double pointLng, List<Gen_Zone_PolyVertice> PontosPolig)
        //{//                             X               y               
        //    int sides = PontosPolig.Count();
        //    int j = sides - 1;
        //    bool pointStatus = false;

        //    for (int i = 0; i < sides; i++)
        //    {
        //        if (PontosPolig[i].Longitude < pointLng && PontosPolig[j].Longitude >= pointLng ||
        //            PontosPolig[j].Longitude < pointLng && PontosPolig[i].Longitude >= pointLng)
        //        {
        //            if (PontosPolig[i].Latitude + (pointLng - PontosPolig[i].Longitude) /
        //                (PontosPolig[j].Longitude - PontosPolig[i].Longitude) * (PontosPolig[j].Latitude - PontosPolig[i].Latitude) < pointLat)
        //            {
        //                pointStatus = !pointStatus;
        //            }
        //        }
        //        j = i;
        //    }
        //    return pointStatus;
        //}


        public static List<Gen_Coordinate> GetCoordinatesListNullDistance()
        {
            List<Gen_Coordinate> list = new List<Gen_Coordinate>();

            using (TaxiDataContext db = new TaxiDataContext())
            {
                var postcodes = db.Bookings.Where(c => c.FromPostCode.StartsWith("CO")).Select(ARGS => ARGS.FromPostCode).Distinct().ToArray<string>();

             list=   db.Gen_Coordinates.Where(c => c.DistanceMiles == null).Where(a => postcodes.Contains(a.PostCode)).ToList();


            }

            return list;



        }


        public static void CancelWebBooking(string mobileNo,string refNo)
        {


            try
            {

                //if (AppVars.objPolicyConfiguration.PDANewWeekMessageByDay.ToStr().Trim().ToLower() == "new")
                //{
                //    new DataClassesOnlineVehicleDataContext().spUpdateBookingConfirmationFromApp3(0, refNo.ToLong(), "cancelfromsystem", 0.00m, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0.00m);
                //}
                //else
                //{
                //    new WebDataClassesDataContext().spUpdateBookingConfirmationFromApp3(0, refNo.ToLong(), "cancelfromsystem", 0.00m, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0.00m);
                //}



                if (mobileNo.Length > 6)
                {

                    string rtnMsg = string.Empty;
                    EuroSMS objSMS = new EuroSMS();
                    objSMS.Message = "Your Booking " + refNo + " has been Cancelled from our System";

                    string mobNo = mobileNo;
                    if (Debugger.IsAttached == false)
                    {

                        int idx = -1;
                        if (mobNo.StartsWith("044") == true)
                        {
                            idx = mobNo.IndexOf("044");
                            mobNo = mobNo.Substring(idx + 3);
                            mobNo = mobNo.Insert(0, "+44");
                        }

                        if (mobNo.StartsWith("07"))
                        {
                            mobNo = mobNo.Substring(1);
                        }

                        if (mobNo.StartsWith("044") == false || mobNo.StartsWith("+44") == false)
                            mobNo = mobNo.Insert(0, "+44");
                    }

                    objSMS.ToNumber = mobNo.Trim();
                    objSMS.Send(ref rtnMsg);
                }

            }
            catch (Exception ex)
            {


            }


        }



        public static decimal CalculateAndSaveDistance(string origin, string destination)
        {

            if (origin.ToStr().Trim().Length == 0 || destination.ToStr().Trim().Length == 0)
                return 0.00m;

            decimal miles = 0.00m;


            if (origin.Contains("&"))
                origin = origin.Replace("&", "AND").Trim();


            if (destination.Contains("&"))
                destination = destination.Replace("&", "AND").Trim();




            try
            {

                bool exist = false;



                if (File.Exists(@Application.StartupPath + "\\PostCodeDistance\\" + origin + "_" + destination + ".csv"))
                {
                    miles = File.ReadAllText(@"C:\Program Files\Eurosoft Tech\PostCodeDistance\" + GetPostCodeMatch(origin) + "_" + GetPostCodeMatch(destination) + ".csv").ToDecimal();
                    exist = true;

                }


                //else
                //{
                if (exist == false)
                {



                    string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + ", UK&destination=" + destination + ", UK&avoid=highways&sensor=false";


                    using (XmlTextReader reader = new XmlTextReader(url2))
                    {
                        reader.WhitespaceHandling = WhitespaceHandling.Significant;





                        using (System.Data.DataSet ds = new System.Data.DataSet())
                        {
                            ds.ReadXml(reader);
                            DataTable dt = ds.Tables["distance"];
                            if (dt != null)
                            {

                                decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
                                decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

                                decimal milKM = 0.621m;
                                decimal milMeter = 0.00062137119m;

                                miles = (milKM * distanceKm) + (milMeter * distanceMeter);

                                dt.Dispose();
                                dt = null;
                                exist = true;

                                string estimatedTime = " ";
                                try
                                {
                                    DataTable dtTime = ds.Tables["duration"];
                                    if (dtTime != null)
                                    {

                                        var rows = ds.Tables["duration"].Rows.OfType<DataRow>().Where(c => c[2].ToStr() == string.Empty);

                                        estimatedTime = (Math.Round((rows.Sum(c => Convert.ToDouble(c[0])) / 60), 0)).ToStr();
                                    
                                    }
                                }
                                catch
                                {


                                }


                                if (miles > 0.4m)
                                {

                                    File.WriteAllText(@Application.StartupPath + "\\PostCodeDistance\\" + origin + "_" + destination + ".csv", miles.ToStr() + "," + estimatedTime);
                                }
                                else
                                {


                                }
                            }
                            else
                            {

                                miles = -1m;
                            }
                           
                        }

                        reader.Close();



                    
                    }
                }

            }
            catch
            {



            }

            return miles;
        }


        public static void UpdateOnlineBookingFares(long onlineBookingId,decimal fares, int bookingTypeId)
        {
            //if (bookingTypeId==Enums.BOOKING_TYPES.ONLINE && onlineBookingId > 0)
            //{

            //    if (AppVars.objPolicyConfiguration.PDANewWeekMessageByDay.ToStr() == "new")
            //    {

            //        new Thread(delegate()
            //        {
            //            using (DataClassesOnlineVehicleDataContext db = new DataClassesOnlineVehicleDataContext())
            //            {
            //                db.ExecuteQuery<int>("update booking set fares=" + fares + " where id=" + onlineBookingId);
            //            }
            //        }).Start();
            //    }
            //}

        }



        //public static void StopServiceWCF()
        //{
        //    var processlist = Process.GetProcesses().Where(c => c.ProcessName == "Taxi_Services").ToList();

        //    foreach (Process theprocess in processlist)
        //    {

        //        theprocess.Kill();

        //    }

        //    if (objTaxiService != null)
        //    {


        //        objTaxiService.Close();
        //        objTaxiService = null;
        //    }


        //}


        //WCF SERVICE PROCESS CALL Coded By Adeel Aijaz //Start
       // public static void StartServiceWCF()
       // {
       //     try
       //     {

       //         if (objTaxiService != null)
       //         {


       //             objTaxiService.Close();
       //             objTaxiService = null;
       //         }

       //         string processName = "Taxi_Services";

       //         var processlist = Process.GetProcesses().Where(c => c.ProcessName == processName).ToList();

       //         foreach (Process theprocess in processlist)
       //         {

       //             theprocess.Kill();
                  
       //         }

       //         Process[] processes = Process.GetProcessesByName("MapPoint");
       //         foreach (Process pc in processes)
       //         {
       //             pc.Kill();
                 

       //         }




       //         Process process = new Process();
       //         process.StartInfo.Verb = "runas";
       //         // Configure the process using the StartInfo properties.
       //         //   process.StartInfo.FileName = @"C:\Program Files (x86)\Eurosoft Tech\Treasure Cab System\Taxi_Services.exe";
       //         process.StartInfo.FileName = System.Windows.Forms.Application.StartupPath + "\\Taxi_Services.exe";
         
       //         process.StartInfo.Arguments = "-n";
       //         process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
       //         process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
       //         process.Start();
               
                
       //         process.Close();
       //         InitializeServiceData();


       //     }
       //     catch (Exception ex)
       //     {


       //     }

       // }

       // public static bool RunWCFService()
       // {
       //     try
       //     {

       //         if (objTaxiService == null)
       //         {
       //             StartServiceWCF();
                 
       //         }
              

       //         return true;
       //     }
       //     catch (Exception ex)
       //     {
       //         return false;

       //     }

       // }

       //public static TreasureService.TaxiService1Client objTaxiService;
       // public static void InitializeServiceData()
       // {
       //     objTaxiService = new Taxi_AppMain.TreasureService.TaxiService1Client("BasicHttpBinding_ITaxiService1");
       //     objTaxiService.Start();

       // }


        
       


        #region  ROUTE API Classes and Properties
        public class Northeast
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Southwest
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Bounds
        {
            public Northeast northeast { get; set; }
            public Southwest southwest { get; set; }
        }

        public class Distance
        {
            public string text { get; set; }
            public int value { get; set; }
        }

        public class Duration
        {
            public string text { get; set; }
            public int value { get; set; }
        }

        public class EndLocation
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class StartLocation
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Distance2
        {
            public string text { get; set; }
            public int value { get; set; }
        }

        public class Duration2
        {
            public string text { get; set; }
            public int value { get; set; }
        }

        public class EndLocation2
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Polyline
        {
            public string points { get; set; }
        }

        public class StartLocation2
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Step
        {
            public Distance2 distance { get; set; }
            public Duration2 duration { get; set; }
            public EndLocation2 end_location { get; set; }
            public string html_instructions { get; set; }
            public Polyline polyline { get; set; }
            public StartLocation2 start_location { get; set; }
            public string travel_mode { get; set; }
            public string maneuver { get; set; }
        }

        public class Leg
        {
            public Distance distance { get; set; }
            public Duration duration { get; set; }
            public string end_address { get; set; }
            public EndLocation end_location { get; set; }
            public string start_address { get; set; }
            public StartLocation start_location { get; set; }
            public List<Step> steps { get; set; }
            public List<object> via_waypoint { get; set; }
        }

        public class OverviewPolyline
        {
            public string points { get; set; }
        }

        public class Route
        {
            public Bounds bounds { get; set; }
            public string copyrights { get; set; }
            public List<Leg> legs { get; set; }
            public OverviewPolyline overview_polyline { get; set; }
            public string summary { get; set; }
            public List<object> warnings { get; set; }
            public List<object> waypoint_order { get; set; }
            public int RouteIndex { get; set; }
        }

        public class RootObject
        {
            public List<Route> routes { get; set; }
            public string status { get; set; }
        }
        public class RouteInformation
        {
            public List<string> Duration;
            public List<string> Distance;
            public List<double> Price;
        }

        #endregion

    }
}
