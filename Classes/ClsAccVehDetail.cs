using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taxi_AppMain
{
   public class ClsAccVehDetail
    {

       //private string vehicleType;

       //public string VehicleType
       // {
       //     get { return vehicleType; }
       //     set { vehicleType = value; }
       // }
       public int Id { get; set; }

       public string VehicleType { get; set; }

       public string SubCompany { get; set; }
       public string SubCompanyTelephoneNo { get; set; }
       public string SubCompanyAddress { get; set; }

      // public string TyreChanged { get; set; }

       

       public decimal CostofTyres { get; set; }

        private string vehicleNo;

        public string VehicleNo
        {
            get { return vehicleNo; }
            set { vehicleNo = value; }
        }


       private int? vehicleTypeId;

       public int? VehicleTypeId
       {
           get { return vehicleTypeId; }
           set { vehicleTypeId = value; }
       }


       private string vehicleColor;

       public string VehicleColor
       {
           get { return vehicleColor; }
           set { vehicleColor = value; }
       }

       private string vehicleMake;

       public string VehicleMake
        {
            get { return vehicleMake; }
            set { vehicleMake = value; }
        }


        private string vehicleModel;

        public string VehicleModel
        {
            get { return vehicleModel; }
            set { vehicleModel = value; }
        }

        private string vehicleOwner;

        public string VehicleOwner
        {
            get { return vehicleOwner; }
            set { vehicleOwner = value; }
        }



        private DateTime ? manufactureDate;

        public DateTime ? ManufactureDate
        {
            get { return manufactureDate; }
            set { manufactureDate = value; }
        }


        private DateTime ? servicesDate;

        public DateTime ? ServicesDate
        {
            get { return servicesDate; }
            set { servicesDate = value; }
        }


        private DateTime ? roadTaxExpDate;

        public DateTime ? RoadTaxExpDate
        {
            get { return roadTaxExpDate; }
            set { roadTaxExpDate = value; }
        }


        private DateTime ? motExpiryDate;

        public DateTime ? MOTExpiryDate
        {
            get { return motExpiryDate; }
            set { motExpiryDate = value; }
        }


        private DateTime ? pLateExpiryDate;

        public DateTime ? PLateExpiryDate
        {
            get { return pLateExpiryDate; }
            set { pLateExpiryDate = value; }
        }


        private string partDetails;

        public string PartDetails
        {
            get { return partDetails; }
            set { partDetails = value; }
        }

        private string plateno;

        public string Plateno
        {
            get { return plateno; }
            set { plateno = value; }
        }

        private string fuel;

        public string Fuel
        {
            get { return fuel; }
            set { fuel = value; }
        }


        private DateTime ? insuranceExpiry;

        public DateTime ? InsuranceExpiry
        {
            get { return insuranceExpiry; }
            set { insuranceExpiry = value; }
        }
                 
        private string companyName;

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }


        private decimal tyreChangeMileage;

        public decimal TyreChangeMileage
        {
            get { return tyreChangeMileage; }
            set { tyreChangeMileage = value; }
        }

        private decimal labour;

        public decimal Labour
        {
            get { return labour; }
            set { labour = value; }
        }
        private decimal parts;

        public decimal Parts
        {
            get { return parts; }
            set { parts = value; }
        }
        private string tyreChanged;

        public string TyreChanged
        {
            get { return tyreChanged; }
            set { tyreChanged = value; }
        }




    }
}
