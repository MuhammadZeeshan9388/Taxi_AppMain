using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Windows.Forms;
using Utils;
namespace UIX
{
   public class MyDatePicker : DateTimePicker
    {
       public MyDatePicker()
       {

        
         //  this.KeyDown += new KeyEventHandler(MyDatePicker_KeyDown);
       
    
        
        
       }

      

       //void MyDatePicker_KeyDown(object sender, KeyEventArgs e)
       //{
       //    if (e.KeyCode == Keys.F2)
       //    {
       //        this.Value = DateTime.Now;

       //    }
       //    else if (base.Value.Year <= 1753)
       //    {
       //        this.Value = new DateTime(DateTime.Now.Year,1, 1);

       //    }
        
       //}

    






       private string previousText = String.Empty;
       private char keyChar = '0';
       private bool Updating = false;
       private bool TypedFirstDigit = false;

       protected override void OnKeyDown(KeyEventArgs e)
       {
           base.OnKeyDown(e);

           if (Updating)
               return;

           previousText = this.Text;

           keyChar = '0';

           if ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9))
           {
               TypedFirstDigit = false;
               return;
           }

           Updating = true;

           if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
               keyChar = (char)((int)e.KeyCode - 48);
           else
               keyChar = (char)e.KeyCode;

           if (TypedFirstDigit)
           {
               TypedFirstDigit = false;
               SendKeys.Send("{right}");
               Application.DoEvents();
               Updating = false;
               return;
           }

           SendKeys.Send("{right}");
           SendKeys.Send("{left}");
           SendKeys.Send("{" + keyChar + "}");
           Application.DoEvents();
           TypedFirstDigit = true;
           Updating = false;

           OnTextChanged(EventArgs.Empty);
       }

       protected override void OnMouseUp(MouseEventArgs e)
       {
           TypedFirstDigit = false;
           base.OnMouseUp(e);
       }

       protected override void OnValidated(EventArgs e)
       {
           TypedFirstDigit = false;
           base.OnValidated(e);
       }

       protected override void OnTextChanged(EventArgs e)
       {
           if (Updating)
               return;

           base.OnTextChanged(e);

           if (keyChar == '0')
               return;

           if (keyChar >= '3' && keyChar <= '9' && previousText.IndexOf(':') != -1 && this.Text.IndexOf(':') != -1)
           {
               if (this.Text[this.Text.IndexOf(':') - 1] != previousText[previousText.IndexOf(':') - 1])
               {
                   Updating = true;
                   TypedFirstDigit = false;
                   SendKeys.Send("{right}");
                   Application.DoEvents();
                   Updating = false;
                   keyChar = '0';
                   return;
               }
           }
       }








       private DateTime? _Value;

       public new DateTime? Value
       {


           get
           {
               if (base.Value.Year <= 1753 && base.Value.TimeOfDay == TimeSpan.Zero)
               {
                   //  base.Value = new DateTime(DateTime.Now.Year, 1, 1);

                   return null;
               }


               else
                   return _Value;

           }
           set
           {
               _Value = value;

               if (value != null)
                   base.Value = Convert.ToDateTime(value);

           }


       } 

    }
}
