using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Taxi_AppMain
{
    public partial class frmShowSentEmailMessage : Form
    {

        private string _EmailBody;

        public string EmailBody
        {
            get { return _EmailBody; }
            set { _EmailBody = value; }
        }

        public frmShowSentEmailMessage(string body)
        {
            InitializeComponent();

            this.EmailBody = body;
            this.Load += new EventHandler(frmShowSentEmailMessage_Load);
        }

        void frmShowSentEmailMessage_Load(object sender, EventArgs e)
        {
          
            //webBrowser1.DocumentText ="<html>"+
            
            //"<table border='1' style='border-style: solid; border-color: #BDC8FF; height: 360px;' cellpadding='0' cellspacing='0' width='95%' align='center'><tr><td colspan='7'  align='center'><tr style='font-size: small'><td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; background-repeat: no-repeat;background-position: center top; background-color: #4d8fef; font-weight: bold;font-size: 18px; font-style: normal; line-height: normal; font-variant: normal;text-transform: none; color: White; text-decoration: none;'>Pinkberry Cars</td></tr><tr style='font-size: small'><td align='left' colspan='2'><p style='font-weight: bold'>Booking Reference:TX74866/1</p><p style='font-weight: bold'>Dear GDF,</p><p>Thank you for booking with Pinkberry Cars. Following are the details of the booking.</p></td></tr><tr style='font-size: small'><td colspan='2' align='center'><table width='99%' border='1' style='border-style: solid;'><tr><td style='background-color: #09a1c4;color:White' align='left'><b>From</b></td><td style='background-color: #09a1c4;color:White' align='left'><b>To</b></td></tr><tr><td>GSDDSFDSSDSDSDSSS HA2 0DU</td><td>SUDBURY HEIGHTS AVENUE GREENFORD UB6 0LY</td></tr></table></td></tr><br/></td></tr><tr valign='top'><td style='background-color: #e8e8e8; height:48px; width:50%' ><table border='0' cellspacing='0' cellpadding='0'  style='height: 354px; width: 100%'><tr height='40'><td align='center' style='height: 50px; background-color:#09a1c4' colspan='2'><span style='color:White'><b>Journey Detail</b></span></td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Pickup Point</td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;From : </td><td align='left'>  GSDDSFDSSDSDSDSSS HA2 0DU </td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>      </td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>    </td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Via Point(s)</td></tr><tr><td align='center' style='width:30%'></td><td align='left'>  1. HEATHROW TERMINAL 4, TW6 2GA</br>2. THE GREEN TWICKENHAM TW2 5AA</br>3. FRAMPTON ROAD HOUNSLOW TW4 5AD </td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Destination</td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;To : </td><td align='left'>  SUDBURY HEIGHTS AVENUE GREENFORD UB6 0LY</td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>      </td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>    </td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'></td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;Vehicle Type:</td><td align='left'>     Saloon</td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;Fares:</td><td align='left'>     £ 7.00 </td></tr><tr><td align='left' style:width:30%>&nbsp;&nbsp;Payment Type : </td><td>Cash</td></tr></table></td><td style='background-color: #e8e8e8; height:48px' ><table width='100%' border='0' cellspacing='0' cellpadding='0' style='height: 357px'><tr height='40'><td align='center' style='height: 50px; background-color: #09a1c4' colspan='7'><span style='color:White'><b>Your Detail</b></span></td></tr><tr><td align='right' bgcolor='#B5BCCD' colspan='7'></tr><tr ><td align='left' style='width:30%'>          &nbsp;&nbsp;Pickup Date :&nbsp;</td><td>12/10/2015 12:35 </td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;Name :&nbsp;</td><td>GDF </td></tr><tr><td align='left' >&nbsp;&nbsp;Mobile Phone :&nbsp;</td> <td>43242</td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;Telephone No :&nbsp;</td><td></td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;Passenger :&nbsp;</td><td>0</td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;Luggage :&nbsp;</td><td>0</td></tr><tr><td style='width:40%' >&nbsp;&nbsp;</td></tr>"+

            //"<tr><td align='left'   bgcolor='#B5BCCD' colspan='7' >&nbsp;&nbsp;Special Requirement</td></tr>"+

            //"<tr><td align='left' colspan='2'    >&nbsp;&nbsp;dgsdgsdgsdgsdgsdihgiohsdgiosdghsdgiohsdiohgsofsaihfiohsfioashfioashfioashfioash</br>foasihfasifhasfhasofhasfioashihgsdiohgsdiohgsdiohgiofhgerwerwe</td></tr>"+

            //"</table></table><table border='1' style='border-style: solid; border-color: #BDC8FF; height: 360px;' cellpadding='0' cellspacing='0' width='95%' align='center'><tr><td colspan='7'  align='center'><tr style='font-size: small'><td colspan='2' align='center' style='font-family: Arial, Helvetica, sans-serif; background-repeat: no-repeat;background-position: center top; background-color: #4d8fef; font-weight: bold;font-size: 18px; font-style: normal; line-height: normal; font-variant: normal;text-transform: none; color: White; text-decoration: none;'>Return Journey Details</td></tr><tr style='font-size: small'><td align='left' colspan='2'><p style='font-weight: bold'>Booking Reference:TX74866/2</p></td></tr><tr style='font-size: small'><td colspan='2' align='center'><table width='99%' border='1' style='border-style: solid;'><tr><td style='background-color: #09a1c4;color:White' align='left'><b>From</b></td><td style='background-color: #09a1c4;color:White' align='left'><b>To</b></td></tr><tr><td>SUDBURY HEIGHTS AVENUE GREENFORD UB6 0LY</td><td>GSDDSFDSSDSDSDSSS HA2 0DU</td></tr></table></td></tr><br/></td></tr><tr valign='top'><td style='background-color: #e8e8e8; height:48px; width:50%' ><table border='0' cellspacing='0' cellpadding='0'  style='height: 354px; width: 100%'><tr height='40'><td align='center' style='height: 50px; background-color:#09a1c4' colspan='2'><span style='color:White'><b>Journey Detail</b></span></td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Pickup Point</td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;From : </td><td align='left'>  SUDBURY HEIGHTS AVENUE GREENFORD UB6 0LY </td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>      </td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>    </td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Via Point(s)</td></tr><tr><td align='center' style='width:30%'></td><td align='left'>  1. FRAMPTON ROAD HOUNSLOW TW4 5AD</br>2. THE GREEN TWICKENHAM TW2 5AA</br>3. HEATHROW TERMINAL 4, TW6 2GA </td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'>&nbsp;&nbsp;Destination</td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;To : </td><td align='left'>  GSDDSFDSSDSDSDSSS HA2 0DU</td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>      </td></tr><tr><td style='width:30%' align='center'> </td><td align='left'>    </td></tr><tr><td align='left' bgcolor='#B5BCCD' colspan='7'></td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;Vehicle Type:</td><td align='left'>     MPV</td></tr><tr><td style='width:30%' align='left'>&nbsp;&nbsp;Fares:</td><td align='left'>     £ 10.00 </td></tr><tr><td align='left' style:width:30%>&nbsp;&nbsp;Payment Type : </td><td>Cash</td></tr></table></td><td style='background-color: #e8e8e8; height:48px' ><table width='100%' border='0' cellspacing='0' cellpadding='0' style='height: 357px'><tr height='40'><td align='center' style='height: 50px; background-color: #09a1c4' colspan='7'><span style='color:White'><b>Your Detail</b></span></td></tr><tr><td align='right' bgcolor='#B5BCCD' colspan='7'></tr><tr ><td align='left' style='width:30%'>          &nbsp;&nbsp;Pickup Date :&nbsp;</td><td>13/10/2015 13:39 </td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;Name :&nbsp;</td><td>GDF </td></tr><tr><td align='left' >&nbsp;&nbsp;Mobile Phone :&nbsp;</td> <td>43242</td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;Passenger :&nbsp;</td><td>0</td></tr><tr><td align='left' style='width:30%'>&nbsp;&nbsp;Luggage :&nbsp;</td><td>0</td></tr><tr><td style='width:40%' >&nbsp;&nbsp;</td></tr><tr><td align='left'   bgcolor='#B5BCCD' colspan='7' >&nbsp;&nbsp;Special Requirement</td></tr><tr><td align='left'  style='width:40%'  >&nbsp;&nbsp;yrtyrtjhj</td></tr></table></table><tr><td colspan='5'><table width='100%' border='0' ><tr style='font-size: small' ><td align='center' style='font-weight: bold'>Thank You for using our service.</td></tr><tr style='font-size: small'><td  style='font-weight: bold'><p><br />Regards,<br />Pinkberry Cars,<br />0207 458 4446<br />www.PinkBerryCars.com</p></td></tr><tr style='font-size: small'><td  align='center'></td></tr></table></td></tr></td></tr></table>"

            //+"</html>";

            webBrowser1.DocumentText = this.EmailBody;

        }
    }
}
