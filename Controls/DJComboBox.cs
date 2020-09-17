using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Windows.Forms;

namespace UIX
{
    public delegate void StartDelegate(object sender, EventArgs e);
    public  class DJComboBox :Telerik.WinControls.UI.RadComboBox
    {
      
    
        private  DJComboBox c;

        public event StartDelegate Refreshing;
        public DJComboBox()
        {
            c = this;
       
         c.KeyDown+=new System.Windows.Forms.KeyEventHandler(c_KeyDown);
         c.ComboBoxElement.ValueChanging += new ValueChangingEventHandler(ComboBoxElement_ValueChanging);
       
      
        }

        private object oldValue;

        public object OldValue
        {
            get { return oldValue; }
            set { oldValue = value; }
        }

        private object newValue;

        public object NewValue
        {
            get { return newValue; }
            set { newValue = value; }
        }


        void ComboBoxElement_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            this.oldValue = e.OldValue;
            this.newValue = e.NewValue;
        }
     
        public void RefreshData()
        {
            Refreshing(this, new EventArgs());
        }

        public void OnRefresh(object sender, EventArgs e)
        {
         
        }

       


        public ElementVisibility ShowDropDownArrow
        {
            set { this.ComboBoxElement.ArrowButton.Visibility = value; }
            get { return this.ComboBoxElement.ArrowButton.Visibility; }
        }

        private void c_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F5)
            {
                RefreshData();
            }
        }
 


 
       
    }
}
