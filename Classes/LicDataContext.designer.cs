﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Taxi_AppMain.Classes
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SysGenSetting789CommandTab")]
	public partial class LicDataContextDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSytemGenePolicyTabsConditions758(SytemGenePolicyTabsConditions758 instance);
    partial void UpdateSytemGenePolicyTabsConditions758(SytemGenePolicyTabsConditions758 instance);
    partial void DeleteSytemGenePolicyTabsConditions758(SytemGenePolicyTabsConditions758 instance);
    #endregion
		
		public LicDataContextDataContext() : 
				base(global::Taxi_AppMain.Properties.Settings.Default.SysGenSetting789CommandTabConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public LicDataContextDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LicDataContextDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LicDataContextDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LicDataContextDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SytemGenePolicyTabsConditions758> SytemGenePolicyTabsConditions758s
		{
			get
			{
				return this.GetTable<SytemGenePolicyTabsConditions758>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.stp_SysPolicyAuth")]
		public ISingleResult<stp_SysPolicyAuthResult> stp_SysPolicyAuth([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ClientName", DbType="VarChar(100)")] string clientName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), clientName);
			return ((ISingleResult<stp_SysPolicyAuthResult>)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SytemGenePolicyTabsConditions758")]
	public partial class SytemGenePolicyTabsConditions758 : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _ID;
		
		private string _ConditionTabPolicy;
		
		private bool _SystemLoadBalance;
		
		private System.Nullable<bool> _ConditionType;
		
		private System.Nullable<int> _ScriptBaseForm;
		
		private string _ScriptType;
		
		private System.Nullable<System.DateTime> _ConditionApplyDate;
		
		private System.Nullable<System.DateTime> _LastCondition;
		
		private System.Nullable<System.DateTime> _FullFormConditionTabPolicyApply;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(long value);
    partial void OnIDChanged();
    partial void OnConditionTabPolicyChanging(string value);
    partial void OnConditionTabPolicyChanged();
    partial void OnSystemLoadBalanceChanging(bool value);
    partial void OnSystemLoadBalanceChanged();
    partial void OnConditionTypeChanging(System.Nullable<bool> value);
    partial void OnConditionTypeChanged();
    partial void OnScriptBaseFormChanging(System.Nullable<int> value);
    partial void OnScriptBaseFormChanged();
    partial void OnScriptTypeChanging(string value);
    partial void OnScriptTypeChanged();
    partial void OnConditionApplyDateChanging(System.Nullable<System.DateTime> value);
    partial void OnConditionApplyDateChanged();
    partial void OnLastConditionChanging(System.Nullable<System.DateTime> value);
    partial void OnLastConditionChanged();
    partial void OnFullFormConditionTabPolicyApplyChanging(System.Nullable<System.DateTime> value);
    partial void OnFullFormConditionTabPolicyApplyChanged();
    #endregion
		
		public SytemGenePolicyTabsConditions758()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="BigInt NOT NULL", IsPrimaryKey=true)]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ConditionTabPolicy", DbType="VarChar(250) NOT NULL", CanBeNull=false)]
		public string ConditionTabPolicy
		{
			get
			{
				return this._ConditionTabPolicy;
			}
			set
			{
				if ((this._ConditionTabPolicy != value))
				{
					this.OnConditionTabPolicyChanging(value);
					this.SendPropertyChanging();
					this._ConditionTabPolicy = value;
					this.SendPropertyChanged("ConditionTabPolicy");
					this.OnConditionTabPolicyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SystemLoadBalance", DbType="Bit NOT NULL")]
		public bool SystemLoadBalance
		{
			get
			{
				return this._SystemLoadBalance;
			}
			set
			{
				if ((this._SystemLoadBalance != value))
				{
					this.OnSystemLoadBalanceChanging(value);
					this.SendPropertyChanging();
					this._SystemLoadBalance = value;
					this.SendPropertyChanged("SystemLoadBalance");
					this.OnSystemLoadBalanceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ConditionType", DbType="Bit")]
		public System.Nullable<bool> ConditionType
		{
			get
			{
				return this._ConditionType;
			}
			set
			{
				if ((this._ConditionType != value))
				{
					this.OnConditionTypeChanging(value);
					this.SendPropertyChanging();
					this._ConditionType = value;
					this.SendPropertyChanged("ConditionType");
					this.OnConditionTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScriptBaseForm", DbType="Int")]
		public System.Nullable<int> ScriptBaseForm
		{
			get
			{
				return this._ScriptBaseForm;
			}
			set
			{
				if ((this._ScriptBaseForm != value))
				{
					this.OnScriptBaseFormChanging(value);
					this.SendPropertyChanging();
					this._ScriptBaseForm = value;
					this.SendPropertyChanged("ScriptBaseForm");
					this.OnScriptBaseFormChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScriptType", DbType="VarChar(250)")]
		public string ScriptType
		{
			get
			{
				return this._ScriptType;
			}
			set
			{
				if ((this._ScriptType != value))
				{
					this.OnScriptTypeChanging(value);
					this.SendPropertyChanging();
					this._ScriptType = value;
					this.SendPropertyChanged("ScriptType");
					this.OnScriptTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ConditionApplyDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> ConditionApplyDate
		{
			get
			{
				return this._ConditionApplyDate;
			}
			set
			{
				if ((this._ConditionApplyDate != value))
				{
					this.OnConditionApplyDateChanging(value);
					this.SendPropertyChanging();
					this._ConditionApplyDate = value;
					this.SendPropertyChanged("ConditionApplyDate");
					this.OnConditionApplyDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastCondition", DbType="DateTime")]
		public System.Nullable<System.DateTime> LastCondition
		{
			get
			{
				return this._LastCondition;
			}
			set
			{
				if ((this._LastCondition != value))
				{
					this.OnLastConditionChanging(value);
					this.SendPropertyChanging();
					this._LastCondition = value;
					this.SendPropertyChanged("LastCondition");
					this.OnLastConditionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FullFormConditionTabPolicyApply", DbType="DateTime")]
		public System.Nullable<System.DateTime> FullFormConditionTabPolicyApply
		{
			get
			{
				return this._FullFormConditionTabPolicyApply;
			}
			set
			{
				if ((this._FullFormConditionTabPolicyApply != value))
				{
					this.OnFullFormConditionTabPolicyApplyChanging(value);
					this.SendPropertyChanging();
					this._FullFormConditionTabPolicyApply = value;
					this.SendPropertyChanged("FullFormConditionTabPolicyApply");
					this.OnFullFormConditionTabPolicyApplyChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	public partial class stp_SysPolicyAuthResult
	{
		
		private long _ID;
		
		private string _ConditionTabPolicy;
		
		private bool _SystemLoadBalance;
		
		private System.Nullable<bool> _ConditionType;
		
		private System.Nullable<int> _ScriptBaseForm;
		
		private System.DateTime _ConditionApplyDate;
		
		private string _ScriptType;
		
		private System.Nullable<System.DateTime> _LastCondition;
		
		private System.Nullable<int> _FullFormConditionTabPolicyApply;
		
		public stp_SysPolicyAuthResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="BigInt NOT NULL")]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this._ID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ConditionTabPolicy", DbType="VarChar(250) NOT NULL", CanBeNull=false)]
		public string ConditionTabPolicy
		{
			get
			{
				return this._ConditionTabPolicy;
			}
			set
			{
				if ((this._ConditionTabPolicy != value))
				{
					this._ConditionTabPolicy = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SystemLoadBalance", DbType="Bit NOT NULL")]
		public bool SystemLoadBalance
		{
			get
			{
				return this._SystemLoadBalance;
			}
			set
			{
				if ((this._SystemLoadBalance != value))
				{
					this._SystemLoadBalance = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ConditionType", DbType="Bit")]
		public System.Nullable<bool> ConditionType
		{
			get
			{
				return this._ConditionType;
			}
			set
			{
				if ((this._ConditionType != value))
				{
					this._ConditionType = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScriptBaseForm", DbType="Int")]
		public System.Nullable<int> ScriptBaseForm
		{
			get
			{
				return this._ScriptBaseForm;
			}
			set
			{
				if ((this._ScriptBaseForm != value))
				{
					this._ScriptBaseForm = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ConditionApplyDate", DbType="DateTime NOT NULL")]
		public System.DateTime ConditionApplyDate
		{
			get
			{
				return this._ConditionApplyDate;
			}
			set
			{
				if ((this._ConditionApplyDate != value))
				{
					this._ConditionApplyDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ScriptType", DbType="VarChar(7) NOT NULL", CanBeNull=false)]
		public string ScriptType
		{
			get
			{
				return this._ScriptType;
			}
			set
			{
				if ((this._ScriptType != value))
				{
					this._ScriptType = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastCondition", DbType="DateTime")]
		public System.Nullable<System.DateTime> LastCondition
		{
			get
			{
				return this._LastCondition;
			}
			set
			{
				if ((this._LastCondition != value))
				{
					this._LastCondition = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FullFormConditionTabPolicyApply", DbType="Int")]
		public System.Nullable<int> FullFormConditionTabPolicyApply
		{
			get
			{
				return this._FullFormConditionTabPolicyApply;
			}
			set
			{
				if ((this._FullFormConditionTabPolicyApply != value))
				{
					this._FullFormConditionTabPolicyApply = value;
				}
			}
		}
	}
}
#pragma warning restore 1591