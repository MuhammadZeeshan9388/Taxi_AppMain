using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Windows.Forms;
using UI;

namespace UIX
{
    public delegate void DropDownRefereshEventHandler(object sender, EventArgs e);

    public class MyDropDownList : ComboBox
    {
        private string _Caption;

     
        private string _Property;

        public event DropDownRefereshEventHandler OnRefreshing;

        public MyDropDownList()
            : base()
        {
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(c_KeyDown);
           this.SelectedValueChanged+=new EventHandler(MyDropDownList_SelectedValueChanged);

         
        }

      


       
        void MyDropDownList_SelectedValueChanged(object sender, EventArgs e)
        {
          
           // this.SelectedText = this.Text;
        }

        private bool _ShowDownArrow = true;

        public bool ShowDownArrow
        {
            get { return _ShowDownArrow; }
            set { _ShowDownArrow = value;

           
               // this.DropDownListElement.ArrowButton.Visibility =value ? ElementVisibility.Visible: ElementVisibility.Collapsed;

         
            }            
             
        }

       

        public void RefreshDataSource()
        {
            if(OnRefreshing!=null)
                OnRefreshing(this, new EventArgs());
        }

       

        private void c_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RefreshDataSource();
            }
        }

      

    }
}

