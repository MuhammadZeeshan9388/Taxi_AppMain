using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi_AppMain
{
    public partial class stp_QuotationReportResult
    {

        private long _Id;

        private string _BookingNo;

        private System.Nullable<System.DateTime> _PickupDateTime;

        private string _CustomerName;

        private string _CustomerMobileNo;

        private string _FromAddress;

        private string _ToAddress;

        private System.Nullable<decimal> _FareRate;

        private System.Nullable<decimal> _CompanyPrice;

        private System.Nullable<decimal> _TotalCharges;

        private string _CompanyName;

        private string _UpdatedBy;

        private System.Nullable<int> _BookingStatusId;

        private string _SpecialRequirements;

        private string _VehicleType;

        private string _Status;

        private bool _IsQuotation;

        public stp_QuotationReportResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", DbType = "BigInt NOT NULL")]
        public long Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this._Id = value;
                }
            }
        }



        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsQuotation", DbType = "bit NULL")]
        public bool IsQuotation
        {
            get
            {
                return this._IsQuotation;
            }
            set
            {
                if ((this._IsQuotation != value))
                {
                    this._IsQuotation = value;
                }
            }
        }



        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BookingNo", DbType = "VarChar(50)")]
        public string BookingNo
        {
            get
            {
                return this._BookingNo;
            }
            set
            {
                if ((this._BookingNo != value))
                {
                    this._BookingNo = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PickupDateTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> PickupDateTime
        {
            get
            {
                return this._PickupDateTime;
            }
            set
            {
                if ((this._PickupDateTime != value))
                {
                    this._PickupDateTime = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerName", DbType = "VarChar(100)")]
        public string CustomerName
        {
            get
            {
                return this._CustomerName;
            }
            set
            {
                if ((this._CustomerName != value))
                {
                    this._CustomerName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CustomerMobileNo", DbType = "VarChar(50)")]
        public string CustomerMobileNo
        {
            get
            {
                return this._CustomerMobileNo;
            }
            set
            {
                if ((this._CustomerMobileNo != value))
                {
                    this._CustomerMobileNo = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FromAddress", DbType = "VarChar(200)")]
        public string FromAddress
        {
            get
            {
                return this._FromAddress;
            }
            set
            {
                if ((this._FromAddress != value))
                {
                    this._FromAddress = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ToAddress", DbType = "VarChar(200)")]
        public string ToAddress
        {
            get
            {
                return this._ToAddress;
            }
            set
            {
                if ((this._ToAddress != value))
                {
                    this._ToAddress = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FareRate", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> FareRate
        {
            get
            {
                return this._FareRate;
            }
            set
            {
                if ((this._FareRate != value))
                {
                    this._FareRate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CompanyPrice", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> CompanyPrice
        {
            get
            {
                return this._CompanyPrice;
            }
            set
            {
                if ((this._CompanyPrice != value))
                {
                    this._CompanyPrice = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TotalCharges", DbType = "Decimal(18,2)")]
        public System.Nullable<decimal> TotalCharges
        {
            get
            {
                return this._TotalCharges;
            }
            set
            {
                if ((this._TotalCharges != value))
                {
                    this._TotalCharges = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CompanyName", DbType = "VarChar(100)")]
        public string CompanyName
        {
            get
            {
                return this._CompanyName;
            }
            set
            {
                if ((this._CompanyName != value))
                {
                    this._CompanyName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UpdatedBy", DbType = "VarChar(50)")]
        public string UpdatedBy
        {
            get
            {
                return this._UpdatedBy;
            }
            set
            {
                if ((this._UpdatedBy != value))
                {
                    this._UpdatedBy = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BookingStatusId", DbType = "Int")]
        public System.Nullable<int> BookingStatusId
        {
            get
            {
                return this._BookingStatusId;
            }
            set
            {
                if ((this._BookingStatusId != value))
                {
                    this._BookingStatusId = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SpecialRequirements", DbType = "VarChar(MAX)")]
        public string SpecialRequirements
        {
            get
            {
                return this._SpecialRequirements;
            }
            set
            {
                if ((this._SpecialRequirements != value))
                {
                    this._SpecialRequirements = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_VehicleType", DbType = "VarChar(100)")]
        public string VehicleType
        {
            get
            {
                return this._VehicleType;
            }
            set
            {
                if ((this._VehicleType != value))
                {
                    this._VehicleType = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    this._Status = value;
                }
            }
        }
    }
}
