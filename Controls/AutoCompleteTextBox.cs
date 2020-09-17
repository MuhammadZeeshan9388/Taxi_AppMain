using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using Telerik.WinControls.UI;

namespace UIX
{
    public class AutoCompleteTextBox : TextBox
    {


        private string _SelectedItem;

        public string SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; }
        }



        private ListBox _listBox;

        public ListBox ListBoxElement
        {
            get { return _listBox; }
            set { _listBox = value; }
        }
        private bool _isAdded;
        private String[] _values;
        private String _formerValue = String.Empty;

        public String FormerValue
        {
            get { return _formerValue; }
            set { _formerValue = value; }
        }

        public AutoCompleteTextBox()
        {
            InitializeComponent();
            ResetListBox();
        }

        private void InitializeComponent()
        {
            _listBox = new ListBox();
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.this_KeyDown);
        
            this._listBox.SelectedValueChanged += new EventHandler(_listBox_SelectedValueChanged);
            this._listBox.KeyDown += new KeyEventHandler(_listBox_KeyDown);
            this.Leave += new EventHandler(AutoCompleteTextBox_Leave);
           

        }

       

        void AutoCompleteTextBox_Leave(object sender, EventArgs e)
        {
            if (this._listBox.Visible && this._listBox.ContainsFocus == false)
            {
                ResetListBox();

            }
      
        }

        void _listBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (onUpdating) return;
            OnSelectItem();
        }

        void _listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnSelectItem();


            }
        }

        private bool _ForceListBoxToUpdate = false;

        public bool ForceListBoxToUpdate
        {
            get { return _ForceListBoxToUpdate; }
            set { _ForceListBoxToUpdate = value; }
        }

       
       
        public void ShowListBox()
        {
            if (!_isAdded)
            {
                Parent.Controls.Add(_listBox);
                _listBox.Left = this.Left;
                _listBox.Top = this.Top + this.Height;
                _isAdded = true;
            }
            _listBox.BringToFront();
            _listBox.Visible = true;

            if (this.DefaultHeight == 0)
                _listBox.Height = 300;
            else
                _listBox.Height = this.DefaultHeight;
     
        }

        private int _DefaultWidth = 0;

        public int DefaultWidth
        {
            get { return _DefaultWidth; }
            set { _DefaultWidth = value; }
        }


        private int _DefaultHeight = 0;

        public int DefaultHeight
        {
            get { return _DefaultHeight; }
            set { _DefaultHeight = value; }
        }

        public void ResetListBox()
        {
            _listBox.Visible = false;
        }

       

    
        
        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                   
                    case Keys.Down:
                        {
                            if ((_listBox.Visible) && (_listBox.SelectedIndex < _listBox.Items.Count - 1))
                            {
                                onUpdating = true;
                                _listBox.SelectedIndex++;
                                onUpdating = false;
                            }
                            break;
                        }
                    case Keys.Up:
                        {
                            if ((_listBox.Visible) && (_listBox.SelectedIndex > 0))
                            {
                                onUpdating = true;
                                _listBox.SelectedIndex--;
                                onUpdating = false;
                            }
                            break;
                        }

                    case Keys.Enter:
                        {
                           
                            OnSelectItem();
                            break;
                        }
                }
             
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        private void OnSelectItem()
        {

            if ((_listBox.Visible) && (_listBox.SelectedItem != null))
            {
                this.SelectedItem = this._listBox.SelectedItem.ToString();


                string prefix = string.Empty;


                if (this.Text.Length > 2)
                {

                    for (int i = 0; i <= 2; i++)
                    {
                        if (char.IsNumber(this.Text[i]))
                            prefix += this.Text[i];
                        else
                            break;

                    }
                   
                }

                if (this.SelectedItem.Contains("."))
                {
                    this.SelectedItem = (prefix + " " + this.SelectedItem.Remove(0, this.SelectedItem.IndexOf('.') + 1).Trim()).Trim(); 

                 

                }
                else
                {
                    this.SelectedItem = (prefix + " " + this.SelectedItem.Trim()).Trim();


                }


                try
                {

                this.Text=this.SelectedItem;

                   // if (_listBox.SelectedIndex != -1)
                  //  {
                        this.ListBoxElement.Visible = false;

                        _listBox.Items.Clear();
                        ResetListBox();
                  //  }
                  
                }
                catch (Exception ex)
                {


                }
            }

        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    return true;
                default:
                    return base.IsInputKey(keyData);
            }
        }


       public  bool onUpdating = false;
       

       
        public String[] Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
            }
        }

       

    }
}
