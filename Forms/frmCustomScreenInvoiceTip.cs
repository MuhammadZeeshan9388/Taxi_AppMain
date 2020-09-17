using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Taxi_Model;
using Utils;
using System.Linq;
using System.Data.Linq;

namespace Taxi_AppMain.Forms
{
    public partial class frmCustomScreenInvoiceTip : Form
    {
        public frmCustomScreenInvoiceTip(string Text)
        {
            InitializeComponent();



                richTextBox1.AddHTML(Text);
              //  richTextBox1.Rtf = text;
              //  richTextBox1.Rtf = "<b>ok</b>";
             //   richTextBox1.Text = text;
               
            
        }

        private KeyEventArgs _LastSendEventArgs;

        public KeyEventArgs LastSendEventArgs
        {
            get { return _LastSendEventArgs; }
            set { _LastSendEventArgs = value; }
        }

      

        private void frmCustomScreenTip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.I && e.KeyCode != Keys.Escape)
            {

                this.LastSendEventArgs = e;
            }

                this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
