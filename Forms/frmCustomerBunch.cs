using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Collections;
using Telerik.WinControls.UI;
using System.Linq;
using Utils;
using Telerik.WinControls.Enumerations;
using UI;

namespace Taxi_AppMain
{
    public partial class frmCustomerBunch : Form
    {
        private List<object[]> _listofData;

        public List<object[]> ListofData
        {
            get { return _listofData; }
            set { _listofData = value; }
        }


        private string _pkField;

        public string PkField
        {
            get { return _pkField; }
            set { _pkField = value; }
        }

        public frmCustomerBunch()
        {
            InitializeComponent();
        }
        public frmCustomerBunch(IList datasource, string primaryfield, bool MultiSelect)
        {

            InitializeComponent();
         //   this.IsMultiSelect = MultiSelect;
          //  chkSelectAll.Visible = MultiSelect;

            this.grdCustomerBunch.DataSource = datasource;
            if(grdCustomerBunch.Columns[primaryfield]!=null)
                this.grdCustomerBunch.Columns[primaryfield].IsVisible = false;


            this.grdCustomerBunch.AllowAutoSizeColumns = true;
            this.grdCustomerBunch.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

            this.PkField = primaryfield;
            this.ListofData = new List<object[]>();

            GridFunctions.SetFilter(this.grdCustomerBunch);

            grdCustomerBunch.MasterTemplate.AllowEditRow = true;
        }
    }
}
